﻿namespace INFOIBV.Framework;

public static class FilterHelper
{
    /// <summary>
    /// Helper function to perform basic convolution
    /// </summary>
    public static byte ConvolvePixel(byte[,] input, float[,] kernel, int u, int v)
    {
        byte value = 0;
        var kernelSize = kernel.GetLength(0);
        var width = input.GetLength(0);
        var height = input.GetLength(1);

        // For every filter index
        for (var i = 0; i < kernelSize; i++)
        {
            for (var j = 0; j < kernelSize; j++)
            {
                var i2 = u - i + kernelSize / 2;
                var j2 = v - j + kernelSize / 2;

                // Clamping is the same as extending the border values
                var du = Math.Clamp(i2, 0, width - 1);
                var dv = Math.Clamp(j2, 0, height - 1);

                value += (byte)Math.Round(input[du, dv] * kernel[i, j]);
            }
        }


        return value;
    }

    /// <inheritdoc cref="ConvolvePixel(byte[,],float[,],int,int)" />
    public static sbyte ConvolvePixel(byte[,] input, sbyte[,] kernel, int u, int v)
    {
        sbyte value = 0;
        var kernelSize = kernel.GetLength(0);
        var width = input.GetLength(0);
        var height = input.GetLength(1);

        // For every filter index
        for (var i = 0; i < kernelSize; i++)
        {
            for (var j = 0; j < kernelSize; j++)
            {
                var i2 = u - i + kernelSize / 2;
                var j2 = v - j + kernelSize / 2;

                // Clamping is the same as extending the border values
                var du = Math.Clamp(i2, 0, width - 1);
                var dv = Math.Clamp(j2, 0, height - 1);

                value += (sbyte)(input[du, dv] * kernel[i, j]);
            }
        }

        return value;
    }
}