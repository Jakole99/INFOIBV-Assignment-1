using INFOIBV.Filters;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace INFOIBV.Framework;

public static class Hough
{
    private const int PixelWidth = 640;
    private const int PixelHeight = 640;

    private static double AbsMin;
    private static double PixelPerR;
    private static double PixelPerTheta;

    //The amount of steps between each pixel
    private const int AngleSteps = 639;

    public static Bitmap ToBitmap(byte[,] input)
    {

        var output = new byte[PixelWidth, PixelHeight];

        var inputWidth = input.GetLength(0);
        var inputHeight = input.GetLength(1);

        var thetaSet = CreateThetaSet(AngleSteps);

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

        AbsMin = Math.Abs(minR);
        PixelPerR = (PixelHeight - 1) / (maxR + AbsMin);
        PixelPerTheta = (PixelWidth - 1) / Math.PI;

        foreach (var (r, theta) in tupleList)
        {
            var u = (int)(theta * PixelPerTheta);
            var v = (int)((AbsMin + r) * PixelPerR);

            if (output[u, v] < 255)
                output[u, v] += 1;
        }

        return output.ToBitmap();
    }


    private static List<(int, int)> PeakFinding(byte[,] input, int threshold)
    {
        var houghTransform = ToBitmap(input).ToSingleChannel();
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
        b.AddClosingFilter(StructureType.Plus, 5, false);
        var closedHough = b.Process(houghTransform);

        //Get the peaks from the closed image.
        for (var v = 0; v < PixelHeight; v++)
        {
            for (var u = 0; u < PixelWidth; u++)
            {
                if (closedHough[u, v] > threshold)
                    houghPairs.Add((u, v));
            }
        }

        return houghPairs;
    }


    private static List<((int, int), (int, int))> HoughLineDetection(byte[,] input, (int, int) houghPair, int minThreshold, int minLength, int maxGap)
    {
        var segmentList = new System.Collections.Generic.List<((int, int), (int, int))>();

        var inputWidth = input.GetLength(0);

        var (indexR, indexTheta) = houghPair;

        var xSet = CreateStepSet(inputWidth);
        var segment = new Stack<(int, int)>();
        var count = 0;

        foreach (var x in xSet)
        {
            //revert r and theta
            var theta = indexTheta / PixelPerTheta;
            var r = indexR / PixelPerR - AbsMin;

            var y = (int)((r - x * Math.Cos(theta)) / Math.Sin(theta));

            segment.Push((x, y));

            if (input[x, y] < minThreshold)
            {
                count++;
                if (count <= maxGap) 
                    continue;

                for (var i = 0; i < maxGap; i++)
                {
                    segment.Pop();
                }

                var (sX, sY) = segment.First();
                var (eX, eY) = segment.Pop();
                var dX = sX - eX;
                var dY = sY - eY;
                var distance = Math.Sqrt(dX * dX + dY * dY);
                if (distance >= minLength)
                    segmentList.Add(((sX,sY),(eX,eY)));

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

        var houghPairs = PeakFinding(input, minThreshold);
        foreach (var houghPair in houghPairs)
        {
            var list = HoughLineDetection(input, houghPair, minThreshold, minLength, maxGap);

            foreach (var ((xS,yS), (xE,yE)) in list)
            {
                var newColor = Color.FromArgb(255, 0, 0);
                    output.SetPixel(xS, yS, newColor);
            }
        }

        return output;
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

    private static int[] CreateStepSet(int totalSteps)
    {
        var stepSet = new int[totalSteps+1];

        for (var i = 0; i <= totalSteps; i++)
        {
            stepSet[i] = i;
        }

        return stepSet;
    }

}