namespace INFOIBV.Framework;

/// <summary>
/// Adapted from "Principles of Digital Image Processing Core Algorithms" by Authors Wilhelm Burger &amp; Mark J. Burge, 2009, Pages 23-24
/// </summary>
public static class BoundaryTrace
{

    private static readonly sbyte[,] Delta =
    {
        {1, 0},
        {1, 1},
        {0, 1},
        {-1, 1},
        {-1, 0},
        {-1, -1},
        {0, -1},
        {1, -1},
    };

    public record CombinedContourLabelingResult(HashSet<Contour> Inner, HashSet<Contour> Outer, int[,] LabelMap);

    public static CombinedContourLabelingResult CombinedContourLabeling(byte[,] input)
    {
        var paddedInput = new byte[input.GetLength(0) + 2, input.GetLength(1) + 2];
        for (var v = 0; v < input.GetLength(0); v++)
        {
            for (var u = 0; u < input.GetLength(1); u++)
            {
                paddedInput[u+1, v+1] = input[u, v];
            }
        }

        var width = paddedInput.GetLength(0);
        var height = paddedInput.GetLength(1);

        var outerContours = new HashSet<Contour>();
        var innerContours = new HashSet<Contour>();

        var labelMap = new int[width, height];
        var regionCounter = 0;

        for (var v = 0; v < height; v++)
        {
            var label = 0;
            for (var u = 0; u < width; u++)
            {
                if (paddedInput[u, v] > Byte.MinValue) // is a foreground pixel
                {
                    if (label != 0)
                    {
                        labelMap[u, v] = label;
                    }
                    else
                    {
                        label = labelMap[u, v];
                        if (label != 0)
                            continue;

                        regionCounter++; // Increment the region
                        label = regionCounter; // Set the label to the new region
                        var outerContour = TraceContour((u, v), 0, label, paddedInput, labelMap);
                        outerContours.Add(outerContour);
                        labelMap[u, v] = label;
                    }
                }
                else
                {
                    if (label == 0)
                        continue;

                    if (labelMap[u, v] == 0)
                    {
                        var innerContour = TraceContour((u - 1, v), 1, label, paddedInput, labelMap);
                        innerContours.Add(innerContour);
                    }
                    label = 0;
                }
            }
        }

        return new CombinedContourLabelingResult(innerContours, outerContours, labelMap);
    }

    private static Contour TraceContour((int u, int v) start, int startDirection, int label, byte[,] input, int[,] labelMap)
    {
        var (first, nextDirection) = FindNextPoint(start, startDirection, input, labelMap);
        var c = new List<(int u, int v)> { first };
        var current = first;

        var done = start == first;
        while (!done)
        {
            labelMap[current.u, current.v] = label;
            var searchDirection = (nextDirection + 6) % 8;
            (var next, nextDirection) = FindNextPoint(current, searchDirection, input, labelMap);

            var previous = current;
            current = next;
            done = (previous == start && current == first);
            if (!done)
                c.Add(next);
        }

        return new Contour(c, label);
    }

    private static ((int u, int v) xc, int d) FindNextPoint((int u, int v) startPoint, int direction, byte[,] input, int[,] labelMap)
    {
        const int directions = 7;

        for (var i = 0; i < directions; i++)
        {
            var (du, dv) = (startPoint.u + Delta[direction, 0], startPoint.v + Delta[direction, 1]);

            if (input[du, dv] == Byte.MinValue)
            {
                labelMap[du, dv] = -1;
                direction = (direction + 1) % 8;
            }
            else
            {
                return ((du, dv), direction);
            }
        }

        return (startPoint, direction);
    }
}

public record Contour(List<(int u, int v)> Points, int Label);