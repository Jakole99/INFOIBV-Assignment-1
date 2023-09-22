using System;
using INFOIBV.Extensions;

namespace INFOIBV.Framework
{
    public static class FilterHelper
    {
        /// <summary>
        /// Helper function to perform basic convolution
        /// </summary>
        public static byte Convolution(byte[,] input, float[,] kernel, int u, int v)
        {
            return Convolution(input, kernel, u, v, (b, f) => (byte)Math.Round(b * f));
        }

        /// <inheritdoc cref="Convolution(byte[,],float[,],int,int)"/>
        public static byte Convolution(byte[,] input, sbyte[,] kernel, int u, int v)
        {
            return Convolution(input, kernel, u, v, (b, f) => (byte)(b * f));
        }

        public static byte Convolution<T>(byte[,] input, T[,] kernel, int u, int v, Func<byte, T, byte> aggregator) where T : new()
        {
            byte value = 0;

            var kernelSize = kernel.GetLength(0);

            // For every filter index
            for (var i = 0; i < kernelSize; i++)
            {
                for (var j = 0; j < kernelSize; j++)
                {
                    var i2 = u - i + kernelSize / 2;
                    var j2 = v - j + kernelSize / 2;

                    // Clamping is the same as extending the border values
                    var du = i2.Clamp(0, input.GetLength(0) - 1);
                    var dv = j2.Clamp(0, input.GetLength(1) - 1);

                    value += aggregator(input[du, dv], kernel[i, j]);
                }
            }

            return value;
        }
    }
}