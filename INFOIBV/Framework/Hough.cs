namespace INFOIBV.Framework;

public static class Hough
{
    public readonly struct HessianLine
    {
        public required double Rho { get; init; }
        public required double Theta { get; init; }

        public static HessianLine FromPoint(double x, double y, double theta) =>
            new()
            {
                Rho = x * Math.Cos(theta) + y * Math.Sin(theta),
                Theta = theta
            };

        public double CalculateY(double x) => (Rho - x * Math.Cos(Theta)) / Math.Sin(Theta);

        public double CalculateX(double y) => (Rho - y * Math.Sin(Theta)) / Math.Cos(Theta);
    }

    public readonly struct ParameterSpace
    {
        public const int Width = 640;
        public const int Height = 640;

        public int[,] Frequency { get; }

        private readonly double _pixelPerTheta;
        private readonly double _pixelPerRho;
        private readonly double _minRho;

        public ParameterSpace(int[,] frequency, double pixelPerTheta, double pixelPerRho, double minRho)
        {
            Frequency = frequency;
            _pixelPerTheta = pixelPerTheta;
            _pixelPerRho = pixelPerRho;
            _minRho = minRho;
        }

        public HessianLine ToHessian(int rho, int theta) =>
            new()
            {
                Rho = rho / _pixelPerRho + _minRho,
                Theta = theta / _pixelPerTheta
            };

        public (int rho, int theta) FromHessian(HessianLine hessianLine) =>
            ((int)Math.Round((hessianLine.Rho - _minRho) * _pixelPerRho),
                (int)Math.Round(hessianLine.Theta * _pixelPerTheta));

        public Bitmap ToBitmap()
        {
            var output = new byte[Width, Height];
            double maxFrequency = Frequency.Cast<int>().Max();

            for (var v = 0; v < Height; v++)
            {
                for (var u = 0; u < Width; u++)
                {
                    output[u, v] = (byte)Math.Round(Frequency[u, v] / maxFrequency * Byte.MaxValue);
                }
            }

            return output.ToBitmap();
        }
    }

    public static (List<HessianLine>, ParameterSpace) PeakFinding(byte[,] input, int threshold)
    {
        // Search size for local maxima
        const int neighbourHood = 16;

        var parameterSpace = HoughTransform(input);
        var frequency = parameterSpace.Frequency;

        var hessianLines = new List<HessianLine>();

        for (var v = 0; v < ParameterSpace.Height; v++)
        {
            for (var u = 0; u < ParameterSpace.Width; u++)
            {
                for (var i = 0; i < neighbourHood; i++)
                {
                    for (var j = 0; j < neighbourHood; j++)
                    {
                        var du = u + i - neighbourHood / 2;
                        var dv = v + j - neighbourHood / 2;

                        if (du is < 0 or >= ParameterSpace.Width)
                            continue;

                        if (dv is < 0 or >= ParameterSpace.Height)
                            continue;


                        if (frequency[du, dv] <= frequency[u, v])
                            continue;

                        frequency[u, v] = 0;
                        break;
                    }
                }

                if (frequency[u, v] < threshold)
                    frequency[u, v] = 0;
                else
                    hessianLines.Add(parameterSpace.ToHessian(u, v));
            }
        }

        return (hessianLines, parameterSpace);
    }

    public static ParameterSpace HoughTransform(byte[,] input) => HoughTransformAngleLimits(input, 0, Math.PI);

    public static ParameterSpace HoughTransformAngleLimits(byte[,] input, double lower, double upper)
    {
        // The amount of steps between each pixel
        const int angleSteps = 639;

        var maxR = Double.NegativeInfinity;
        var minR = Double.PositiveInfinity;

        var hessianLines = new List<HessianLine>();

        var stepSize = (upper - lower) / (angleSteps - 1);

        for (var i = 0; i < angleSteps; i++)
        {
            var theta = stepSize * i;

            for (var v = 0; v < input.GetLength(1); v++)
            {
                for (var u = 0; u < input.GetLength(0); u++)
                {
                    if (input[u, v] == 0)
                        continue;

                    var hessianLine = HessianLine.FromPoint(u, v, theta);

                    maxR = Math.Max(maxR, hessianLine.Rho);
                    minR = Math.Min(minR, hessianLine.Rho);
                    hessianLines.Add(hessianLine);
                }
            }
        }

        var pixelPerR = (ParameterSpace.Height - 1) / (maxR - minR);
        var pixelPerTheta = (ParameterSpace.Width - 1) / (upper - lower);

        // Build frequency table
        var frequency = new int[ParameterSpace.Width, ParameterSpace.Height];

        var parameterSpace = new ParameterSpace(frequency, pixelPerTheta, pixelPerR, minR);

        foreach (var hessianLine in hessianLines)
        {
            var (rho, theta) = parameterSpace.FromHessian(hessianLine);
            frequency[rho, theta] += 1;
        }

        return parameterSpace;
    }

    private static List<((int, int), (int, int))> HoughLineDetection(byte[,] input, HessianLine hessianLine,
        int minThreshold, int minLength, int maxGap)
    {
        var segmentList = new List<((int, int), (int, int))>();

        var width = input.GetLength(0);
        var height = input.GetLength(1);

        var xL = 0;
        var yL = 0;

        var xG = 0;
        var yG = 0;

        var trackingLine = false;
        var previousWasGap = false;

        if (hessianLine.Theta is 0 or Math.PI)
            return segmentList;

        // Loop over every x
        for (var x = 0; x < width; x++)
        {
            // Calculate y
            var y = (int)Math.Round(hessianLine.CalculateY(x));

            // y is out of bounds
            if (y >= height || y < 0)
                continue;

            if (input[x, y] < minThreshold) // Is gap
            {
                // Skip if we are not tracking a line
                if (!trackingLine)
                {
                    previousWasGap = true;
                    continue;
                }

                if (!previousWasGap)
                {
                    // Start a new gap
                    xG = x;
                    yG = y;

                    previousWasGap = true;
                    continue;
                }

                var gapLength = Math.Sqrt(Math.Pow(x - xG, 2) + Math.Pow(y - yG, 2));

                if (gapLength > maxGap)
                {
                    var lineLength = Math.Sqrt(Math.Pow(xL - xG, 2) + Math.Pow(yL - yG, 2));

                    // End the line at gap start if greater than the min length
                    if (lineLength >= minLength)
                        segmentList.Add(((xL, yL), (xG, yG)));

                    trackingLine = false;
                }

                previousWasGap = true;
            }
            else // Is line
            {
                if (previousWasGap && !trackingLine)
                {
                    // Start a new line
                    xL = x;
                    yL = y;

                    trackingLine = true;
                }

                previousWasGap = false;
            }
        }

        return segmentList;
    }

    public static Bitmap VisualizeHoughLineSegments(byte[,] input, int minThreshold, int minLength, int maxGap)
    {
        var bitmap = input.ToBitmap();
        var (hessianLines, _) = PeakFinding(input, minThreshold);

        var redPen = new Pen(Color.FromArgb(128, 0, 255, 0), 3);
        var bluePen = new Pen(Color.FromArgb(10, 255, 0, 0), 1);

        // Draw line to screen.
        using var graphics = Graphics.FromImage(bitmap);

        if (true)
        {
            foreach (var hessianLine in hessianLines)
            {
                // Calculate y
                var y1 = (int)hessianLine.CalculateY(0);
                var y2 = (int)hessianLine.CalculateY(bitmap.Width);

                if (y1 is Int32.MaxValue or Int32.MinValue || y2 is Int32.MaxValue or Int32.MinValue)
                {
                    graphics.DrawLine(bluePen, (int)hessianLine.Rho, 0, (int)hessianLine.Rho, bitmap.Height);
                    continue;
                }

                graphics.DrawLine(bluePen, 0, y1, bitmap.Width, y2);
            }
        }

        foreach (var hessianLine in hessianLines)
        {
            var lines = HoughLineDetection(input, hessianLine, minThreshold, minLength, maxGap);

            foreach (var ((xS, yS), (xE, yE)) in lines)
            {
                graphics.DrawLine(redPen, xS, yS, xE, yE);
            }
        }

        return bitmap;
    }
}