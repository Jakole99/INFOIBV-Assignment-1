namespace INFOIBV.SIFT;

public readonly struct Image
{
    public byte[,] Bytes { get; }

    public Image(byte[,] bytes)
    {
        Bytes = bytes;
    }

    public static SImage operator -(Image a, Image b)
    {
        var width = a.Bytes.GetLength(0);
        var height = a.Bytes.GetLength(1);

        if (width != b.Bytes.GetLength(0) || height != b.Bytes.GetLength(1))
            throw new InvalidOperationException("Images are not the same size");

        var difference = new short[width, height];
        var min = Int16.MaxValue;
        var max = Int16.MinValue;

        for (var v = 0; v < height; v++)
        {
            for (var u = 0; u < width; u++)
            {
                difference[u, v] = (short)(a.Bytes[u, v] - b.Bytes[u, v]);
                min = Math.Min(min, difference[u, v]);
                max = Math.Max(max, difference[u, v]);
            }
        }

        return new(difference, max, min);
    }
}