namespace INFOIBV.SIFT;

public readonly struct ImageS
{
    public int[,] Ints { get; init; }
    public int Max { get; init; }
    public int Min { get; init; }

    public ImageS(int[,] ints, int max, int min)
    {
        Ints = ints;
        Max = max;
        Min = min;
    }
    public Image Visualize(ImageS a)
    {
        var width = a.Ints.GetLength(0);
        var height = a.Ints.GetLength(1);
        var lowest = a.Min;
        var highest = a.Max;

        var differences = a.Ints;
        var output = new byte[width, height];

        //if (highest - lowest == 0)
        //{
        //    for (var v = 0; v < height; v++)
        //    {
        //        for (var u = 0; u < width; u++)
        //        {
        //            output[u, v] = (byte)differences[u, v];
        //        }
        //    }

        //    return new(output);
        //}

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

        return new Image(output);
    }

}