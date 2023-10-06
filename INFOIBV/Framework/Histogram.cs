namespace INFOIBV.Framework;

public readonly struct Histogram
{
    public int[] Values { get; }

    public int UniqueCount { get; }

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

    private Bitmap GetBitmap(int width, int height)
    {
        throw new NotImplementedException();
    }
}