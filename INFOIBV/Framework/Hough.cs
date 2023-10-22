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
    }

    public readonly struct ParameterSpace
    {
        public const int Width = 640;
        public const int Height = 640;

        public int[,] Frequency { get; }

        private readonly double _pixelPerTheta;
        private readonly double _pixelPerR;
        private readonly double _absMin;

        public ParameterSpace(int[,] frequency, double pixelPerTheta, double pixelPerR, double absMin)
        {
            Frequency = frequency;
            _pixelPerTheta = pixelPerTheta;
            _pixelPerR = pixelPerR;
            _absMin = absMin;
        }

        public HessianLine ToHessian(int rho, int theta) =>
            new()
            {
                Rho = rho / _pixelPerR - _absMin,
                Theta = theta / _pixelPerTheta
            };

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

    public static ParameterSpace HoughTransform(byte[,] input) => HoughTransformAngleLimits(input, 0, Math.PI);

    public static ParameterSpace HoughTransformAngleLimits(byte[,] input, double lower, double upper)
    {
        // The amount of steps between each pixel
        const int angleSteps = 639;

        var frequency = new int[ParameterSpace.Width, ParameterSpace.Height];

        var inputWidth = input.GetLength(0);
        var inputHeight = input.GetLength(1);

        var maxR = Double.NegativeInfinity;
        var minR = Double.PositiveInfinity;

        var hessianLines = new List<HessianLine>();

        var stepSize = (upper - lower) / (angleSteps - 1);
        for (var i = 0; i < angleSteps; i++)
        {
            var theta = stepSize * i;

            for (var v = 0; v < inputHeight; v++)
            {
                for (var u = 0; u < inputWidth; u++)
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

        var absMin = Math.Abs(minR);
        var pixelPerR = (ParameterSpace.Height - 1) / (maxR + absMin);
        var pixelPerTheta = (ParameterSpace.Width - 1) / (upper - lower);

        foreach (var hessianLine in hessianLines)
        {
            var u = (int)(hessianLine.Theta * pixelPerTheta);
            var v = (int)Math.Round((absMin + hessianLine.Rho) * pixelPerR);
            frequency[u, v] += 1;
        }

        return new ParameterSpace(frequency, pixelPerTheta, pixelPerR, absMin);
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

    private static List<((int, int), (int, int))> HoughLineDetection(byte[,] input, HessianLine hessianLine,
        int minThreshold, int minLength, int maxGap)
    {
        var segmentList = new List<((int, int), (int, int))>();

        var width = input.GetLength(0);
        var height = input.GetLength(1);

        var x1 = 0;
        var y1 = 0;

        var xG = 0;
        var yG = 0;

        var gapLength = 0;

        var trackingLine = false;
        var previousWasGap = false;

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
                if (!previousWasGap)
                {
                    // Start a new gap
                    gapLength = 0;
                    xG = x;
                    yG = y;
                }
                else
                {
                    gapLength++;
                }

                if (gapLength > maxGap && trackingLine)
                {
                    // End the line at gap start
                    if (Math.Sqrt(Math.Pow(x1 - xG, 2) + Math.Pow(y1 - yG, 2)) >= minLength)
                        segmentList.Add(((x1, y1), (xG, yG)));

                    trackingLine = false;
                }

                previousWasGap = true;
            }
            else // Is line
            {
                if (previousWasGap && !trackingLine)
                {
                    // Start a new line
                    x1 = x;
                    y1 = y;

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

        var redPen = new Pen(Color.Red, 1);
        var bluePen = new Pen(Color.Blue, 1);

        // Draw line to screen.
        using var graphics = Graphics.FromImage(bitmap);

        foreach (var hessianLine in hessianLines)
        {
            // Calculate y
            var y1 = (int)hessianLine.CalculateY(0);
            var y2 = (int)hessianLine.CalculateY(bitmap.Width);

            graphics.DrawLine(bluePen, 0, y1, bitmap.Width, y2);

            foreach (var ((xS, yS), (xE, yE)) in
                     HoughLineDetection(input, hessianLine, minThreshold, minLength, maxGap))
            {
                graphics.DrawLine(redPen, xS, yS, xE, yE);
            }
        }

        return bitmap;
    }
}