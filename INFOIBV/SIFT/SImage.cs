namespace INFOIBV.SIFT;

/// <summary>
/// Signed image needed for Difference of Gaussians
/// </summary>
public readonly struct SImage
{
    public short[,] Data { get; }
    private short Max { get; }
    private short Min { get; }

    public SImage(short[,] data, short max, short min)
    {
        Data = data;
        Max = max;
        Min = min;
    }
    public static Image Visualize(SImage a)
    {
        var width = a.Data.GetLength(0);
        var height = a.Data.GetLength(1);
        var lowest = a.Min;
        var highest = a.Max;

        var differences = a.Data;
        var output = new byte[width, height];

        for (var v = 0; v < height; v++)
        {
            for (var u = 0; u < width; u++)
            {
                if (differences[u, v] == 0)
                    output[u, v] = 128;
                else
                    output[u, v] = (byte)(Byte.MinValue + (differences[u, v] - lowest) * Byte.MaxValue / (highest - lowest));
            }
        }

        return new(output);
    }
}