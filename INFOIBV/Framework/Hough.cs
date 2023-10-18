namespace INFOIBV.Framework;

public static class Hough
{
    private const int PixelWidth = 640;
    private const int PixelHeight = 640;

    public static byte[,] PeakFinding(byte[,] input, int angleSteps)
    {
        var transForm = HoughTransform(input, angleSteps);
        throw new NotImplementedException();
    }

    public static byte[,] HoughTransform(byte[,] input, int angleSteps)
    {
        throw new NotImplementedException();
    }

    public static byte[,] HoughTransformAngleLimits(byte[,] input, int angleSteps, int minAngle, int maxAngle)
    {
        throw new NotImplementedException();
    }

    public static List<(int a, int b)> HoughLineDetection(byte[,] input, (int r, int theta) pair, int minIntensity, int minLength, int maxGap)
    {
        throw new NotImplementedException();
    }

    public static byte[,] VisualizeHoughLineSegments(byte[,] input, List<(int a, int b)> lineSegments)
    {
        throw new NotImplementedException();
    }

    public static Bitmap ToBitmap(byte[,] input, int angleSteps)
    {
        var output = new byte[PixelWidth, PixelHeight];

        var inputWidth = input.GetLength(0);
        var inputHeight = input.GetLength(1);

        var thetaSet = CreateThetaSet(angleSteps);

        var maxR = Double.NegativeInfinity;
        var minR = Double.PositiveInfinity;
        var maxFrequency = 0;

        var frequencyTable = new Dictionary<(double distance, double angle), int>();
        var tupleList = new List<Tuple<double, double>>();

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

                    tupleList.Add((r,theta).ToTuple());
                    //if (!frequencyTable.ContainsKey((r, theta)))
                    //    frequencyTable[(r, theta)] = 1;
                    //else
                    //    frequencyTable[(r, theta)] += 1;

                    //maxFrequency = Math.Max(maxFrequency, frequencyTable[(r, theta)]);
                }
            }
        }

        var absMin = Math.Abs(minR);
        var pixelPerR = (PixelHeight-1) / (maxR + absMin);
        double pixelPerTheta = (PixelWidth-1) / Math.PI;

        foreach (var (r, theta) in tupleList)
        {
            var u = (int)(theta * pixelPerTheta);
            var v = (int)((absMin + r) * pixelPerR);

            if (output[u, v] < 255)
                output[u, v] += 1;
        }
        //foreach (var ((r, theta), frequency) in frequencyTable)
        //{
        //    var u = (int)(theta * pixelPerTheta);
        //    var v = (int)((absMin + r) * pixelPerR);

        //    output[u, v] = (byte)frequency; // (byte)(frequency * ((double)Byte.MaxValue / maxFrequency));
        //}

        return output.ToBitmap();
    }


    private static double[] CreateThetaSet(int totalSteps)
    {
        var thetaSet = new double[totalSteps];
        var stepSize = Math.PI / (totalSteps - 1);

        for (var i = 0; i < totalSteps; i++)
        {
            thetaSet[i] = stepSize * i;
        }

        return thetaSet;
    }
}