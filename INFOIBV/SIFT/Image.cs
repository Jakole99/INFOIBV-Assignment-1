namespace INFOIBV.SIFT;

public readonly struct Image
{
    public byte[,] Bytes { get; init; }

    public Image(byte[,] bytes)
    {
        Bytes = bytes;
    }

    public static Image operator -(Image a, Image b)
    {
        var width = a.Bytes.GetLength(0);
        var height = a.Bytes.GetLength(1);

        if (width != b.Bytes.GetLength(0) || height != b.Bytes.GetLength(1))
            throw new InvalidOperationException("Images are not the same size");

        var difference = new int[width, height];
        var min = Int32.MaxValue;
        var max = Int32.MinValue;

        for (var v = 0; v < height; v++)
        {
            for (var u = 0; u < width; u++)
            {
                difference[u, v] = a.Bytes[u, v] - b.Bytes[u, v];
                min = Math.Min(min, difference[u, v]);
                max = Math.Max(max, difference[u, v]);
            }
        }

        var output = new byte[width, height];

        if (max - min == 0)
        {
            for (var v = 0; v < height; v++)
            {
                for (var u = 0; u < width; u++)
                {
                    output[u, v] = (byte)difference[u, v];
                }
            }

            return new(output);
        }

        for (var v = 0; v < height; v++)
        {
            for (var u = 0; u < width; u++)
            {
                output[u, v] = (byte)((difference[u, v] - min) * Byte.MaxValue / (max - min));
            }
        }

        return new(output);
    }
}