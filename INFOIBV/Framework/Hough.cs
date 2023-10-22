using INFOIBV.Filters;

namespace INFOIBV.Framework;

public static class Hough
{
    private const int PixelWidth = 640;
    private const int PixelHeight = 640;

    private static double _absMin;
    private static double _pixelPerR;
    private static double _pixelPerTheta;

    private static double _upper = Math.PI;
    private static double _lower = 0;

    //The amount of steps between each pixel
    private const int AngleSteps = 639;

    public static Bitmap HoughTransform(byte[,] input)
    {
        var outputInt = new int[PixelWidth, PixelHeight];

        var inputWidth = input.GetLength(0);
        var inputHeight = input.GetLength(1);

        var thetaSet = CreateThetaSet(AngleSteps, _lower, _upper);

        var maxR = Double.NegativeInfinity;
        var minR = Double.PositiveInfinity;

        var tupleList = new List<(double, double)>();

        foreach (var theta in thetaSet)
        {
            for (var v = 0; v < inputHeight; v++)
            {
                for (var u = 0; u < inputWidth; u++)
                {
                    if (input[u, v] == 0)
                        continue;

                    var r = u * Math.Cos(theta) + v * Math.Sin(theta);
                    maxR = Math.Max(maxR, r);
                    minR = Math.Min(minR, r);

                    tupleList.Add((r, theta));
                }
            }
        }

        _absMin = Math.Abs(minR);
        _pixelPerR = (PixelHeight - 1) / (maxR + _absMin);
        _pixelPerTheta = (PixelWidth - 1) / _upper;

        foreach (var (r, theta) in tupleList)
        {
            var u = (int)(theta * _pixelPerTheta);
            var v = (int)((_absMin + r) * _pixelPerR);
            outputInt[u, v] += 1;
        }

        float maxIntesity = outputInt.Cast<int>().Max();
        var output = new byte[PixelWidth, PixelHeight];

        for (var v = 0; v < PixelHeight; v++)
        {
            for (var u = 0; u < PixelWidth; u++)
            {
                output[u,v] = (byte)((outputInt[u,v] / maxIntesity) * Byte.MaxValue);
            }
        }

        return output.ToBitmap();
    }

    public static Bitmap HoughTransformAngleLimits(byte[,] input, double lower, double upper)
    {
        _upper = upper;
        _lower = lower;
        return HoughTransform(input);
    }

    public static (List<(int, int)>, Bitmap) PeakFinding(byte[,] input, int threshold)
    {
        _upper = Math.PI;
        _lower = 0;
        var houghTransform = HoughTransform(input).ToSingleChannel();
        var houghPairs = new List<(int, int)>();

        //Option A: Thresh holding

        //Turn pixels under threshold to background
        Parallel.For(0, PixelHeight, v =>
        {
            for (var u = 0; u < PixelWidth; u++)
            {
                if (houghTransform[u, v] < threshold)
                    houghTransform[u, v] = 0;
            }
        });

        //Apply closing to merge regions of high scores
        var b = new FilterCollection();
        b.AddClosingFilter(StructureType.Plus, 5);
        var closedHough = b.Process(houghTransform);

        //Get the peaks from the closed image.
        for (var v = 0; v < PixelHeight; v++)
        {
            for (var u = 0; u < PixelWidth; u++)
            {
                if (closedHough[u, v] > 0)
                    houghPairs.Add((u, v));
            }
        }

        return (houghPairs, closedHough.ToBitmap());
    }

    private static List<((int, int), (int, int))> HoughLineDetection(byte[,] input, (int, int) houghPair,
        int minThreshold, int minLength, int maxGap)
    {
        var segmentList = new List<((int, int), (int, int))>();

        var inputWidth = input.GetLength(0);
        var inputHeight = input.GetLength(1);

        var (indexR, indexTheta) = houghPair;

        var xSet = CreateStepSet(inputWidth);
        var segment = new Stack<(int, int)>();
        var count = 0;

        //revert r and theta back
        var theta = indexTheta / _pixelPerTheta;
        var r = indexR / _pixelPerR - _absMin;

        foreach (var x in xSet)
        {
            var y = (int)((r - x * Math.Cos(theta)) / Math.Sin(theta));

            if (y >= inputHeight || y < 0)
                continue;

            if (input[x, y] < minThreshold)
            {
                count++;
                if (count < maxGap)
                {
                    segment.Push((x, y));
                    continue;
                }

                for (var i = 1; i < maxGap; i++)
                {
                    segment.Pop();
                }

                count = 0;

                if (segment.Count == 0)
                    continue;

                var (sX, sY) = segment.First();
                var (eX, eY) = segment.Last();
                var dX = sX - eX;
                var dY = sY - eY;
                var distance = Math.Sqrt(dX * dX + dY * dY);
                if (distance >= minLength)
                    segmentList.Add(((sX, sY), (eX, eY)));

                segment.Clear();
            }
            else
            {
                count = 0;
                segment.Push((x, y));
            }
        }

        return segmentList;
    }

    public static Bitmap VisualizeHoughLineSegments(byte[,] input, int minThreshold, int minLength, int maxGap)
    {
        var output = input.ToBitmap();
        var redColor = Color.FromArgb(255, 0, 0);
        var houghPairs = PeakFinding(input, minThreshold).Item1;

        foreach (var houghPair in houghPairs)
        {
            var list = HoughLineDetection(input, houghPair, minThreshold, minLength, maxGap);

            foreach (var ((xS, yS), (xE, yE)) in list)
            {
                var line = LineIndices(xS, yS, xE, yE);

                foreach (var (x, y) in line)
                {
                    output.SetPixel(x, y, redColor);
                }
            }
        }

        return output;
    }


    private static List<(int, int)> LineIndices(int xS, int yS, int xE, int yE)
    {
        var line = new List<(int, int)>();

        double dY = yE - yS;
        double dX = xE - xS;
        var m = dY / dX;
        var b = yS - m * xS;

        var steps = Math.Abs(dX);
        var x = xS;

        if (dX < 0)
        {
            for (var i = 0; i < steps; i++)
            {
                var y = m * x + b;
                line.Add((x, (int)y));
                --x;
            }
        }
        else
        {
            for (var i = 0; i < steps; i++)
            {
                var y = m * x + b;
                line.Add((x, (int)y));
                ++x;
            }
        }


        return line;
    }


    private static double[] CreateThetaSet(int totalSteps, double lower, double upper)
    {
        var thetaSet = new double[totalSteps];
        var stepSize = (upper - lower) / (totalSteps - 1);

        for (var i = 0; i < totalSteps; i++)
        {
            thetaSet[i] = stepSize * i;
        }

        return thetaSet;
    }

    private static int[] CreateStepSet(int totalSteps)
    {
        var stepSet = new int[totalSteps + 1];

        for (var i = 0; i < totalSteps; i++)
        {
            stepSet[i] = i;
        }

        return stepSet;
    }
}