﻿using INFOIBV;
using System;
using System.Drawing;

public static class ByteArrayExtentions
{
    /// <summary>
    /// Apply a filter funtion to the entire array
    /// </summary>
    /// <param name="input"></param>
    /// <param name="func"></param>
    public static void Apply(this byte[,] input, Func<byte[,], byte[,]> func)
    {
        func(input);
    }

    /// <summary>
    /// Apply byte to byte function on the entire array
    /// </summary>
    /// <param name="array"></param>
    /// <param name="func"></param>
    public static void PointOperation(this byte[,] array, Func<byte, byte> func)
    {
        var width = array.GetLength(0);
        var height = array.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                array[x, y] = func(array[x, y]);
            }
        }
    }

    public static Bitmap ToBitmap(this byte[,] singleChannel)
    {
        var width = singleChannel.GetLength(0);
        var height = singleChannel.GetLength(1);
        var output = new Bitmap(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var value = singleChannel[x, y];
                var newColor = Color.FromArgb(value, value, value);
                output.SetPixel(x, y, newColor);
            }
        }

        return output;
    }
}