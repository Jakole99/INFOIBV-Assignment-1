using System;
using INFOIBV.Extensions;

namespace INFOIBV.Framework
{
    public static class FilterHelper
    {
        /// <summary>
        /// Helper function to perform basic convolution
        /// </summary>
        public static byte FloatToByteConvolution(byte[,] input, float[,] kernel, int u, int v)
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

                    value += (byte)Math.Round(input[du, dv] * kernel[i, j]);
                }
            }

            return value;
        }

        /// <inheritdoc cref="FloatToByteConvolution"/>
        public static sbyte Convolution(byte[,] input, sbyte[,] kernel, int u, int v)
        {
            sbyte value = 0;

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

                    value += (sbyte)(input[du, dv] * kernel[i, j]);
                }
            }

            return value;
        }

        public static byte[] Cumulation(byte[] input)
        {
            for (int i = 1; i < input.Length; i++)
            {
                input[i] = (byte)(input[i] + input[i - 1]);
            }

            return input;
        }
    }
}