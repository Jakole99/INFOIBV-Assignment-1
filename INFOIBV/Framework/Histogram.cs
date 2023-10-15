namespace INFOIBV.Framework;

public readonly struct Histogram
{
    public int[] Values { get; }

    public int UniqueCount { get; }

    public int NonBackgroundCount { get; }

    public int[] GetCumulativeValues()
    {
        for (var i = 1; i < Values.Length; i++)
        {
            Values[i] += Values[i - 1];
        }

        return Values;
    }

    public Histogram(byte[,] input)
    {
        Values = CreateHistogram(input);
        UniqueCount = Values.Count(x => x != 0);
        NonBackgroundCount = input.Cast<byte>().Count(x => x != 0);
    }

    private static int[] CreateHistogram(byte[,] input)
    {
        var histogramTable = new int[Byte.MaxValue + 1];
        var width = input.GetLength(0);
        var height = input.GetLength(1);

        for (var u = 0; u < width; u++)
        {
            for (var v = 0; v < height; v++)
            {
                var intensity = input[u, v];
                histogramTable[intensity] += 1;
            }
        }

        return histogramTable;
    }

    public Bitmap ToBitmap(int width, int height, bool isCumulative = false)
    {
        var valueHeight = (float)height / Values.Max();
        var columnWidth = (float)width / (Byte.MaxValue + 1);

        var output = new byte[width, height];
        var values = isCumulative ? GetCumulativeValues() : Values;

        Parallel.For(0, height, v =>
        {
            for (var u = 0; u < width; u++)
            {
                var value = values[(int)(u / columnWidth)];
                output[u, v] = height - value * valueHeight >= v ? Byte.MaxValue : Byte.MinValue;
            }
        });

        return output.ToBitmap();
    }
}