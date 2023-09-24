using System;
using System.Threading.Tasks;
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

        public static async Task<int[]> CreateHistogram(byte[,] input)
        {
            var histogramTable = new int[Byte.MaxValue+1];
            var width = input.GetLength(0);

            // Run the mapping on another thread
            await Task.Run(() =>
            {
                Parallel.For(0, input.Length, i =>
                {
                    var u = i % width;
                    var v = i / width;

                    var intensity = input[u,v];
                    histogramTable[intensity] += 1;
                });
            });

            return histogramTable;
        }

        public static async Task<int[]> CreateCumulativeHistogram(byte[,] input)
        {
            var histogram = await CreateHistogram(input);
            return Accumulation(histogram);
        }

        public static int[] Accumulation(int[] input)
        {
            for (var i = 1; i < input.Length; i++)
            {
                input[i] += input[i - 1];
            }

            return input;
        }
    }
}