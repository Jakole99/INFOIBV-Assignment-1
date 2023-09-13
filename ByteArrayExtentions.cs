using System;

public static class ByteArrayExtentions
{
    public static void Apply(this byte[,] input, Func<byte[,], byte[,]> func)
    {
        func(input);
    }

    public static void PointOperation(this byte[,] array, Func<byte, byte> func)
    {
        var width = array.GetLength(0);
        var height = array.GetLength(1);

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                array[x, y] = func(array[x, y]);
            }
    }
}