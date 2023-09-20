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
            byte value = 0;
            
            // For every filter index
            for (var i = 0; i < kernel.GetLength(0); i++)
            {
                for (var j = 0; j < kernel.GetLength(1); j++)
                {
                    // Clamping is the same as extending the border values 
                    var du = MathExtensions.Clamp(u - i, 0, input.GetLength(0) - 1);
                    var dv = MathExtensions.Clamp(v - j, 0, input.GetLength(1) - 1);

                    value += (byte)(input[du, dv] * kernel[i, j]);
                }
            }

            return value;
        }

        /// <inheritdoc cref="Convolution(byte[,],float[,],int,int)"/>
        public static sbyte Convolution(byte[,] input, sbyte[,] kernel, int u, int v)
        {
            sbyte value = 0;
            
            // For every filter index
            for (var i = 0; i < kernel.GetLength(0); i++)
            {
                for (var j = 0; j < kernel.GetLength(1); j++)
                {
                    // Clamping is the same as extending the border values 
                    var du = MathExtensions.Clamp(u - i, 0, input.GetLength(0) - 1);
                    var dv = MathExtensions.Clamp(v - j, 0, input.GetLength(1) - 1);

                    value += (sbyte)(input[du, dv] * kernel[i, j]);
                }
            }

            return value;
        }
    }
}