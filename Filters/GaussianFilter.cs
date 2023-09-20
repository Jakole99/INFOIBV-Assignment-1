using System;
using System.Linq;
using INFOIBV.Extensions;
using INFOIBV.Framework;

namespace INFOIBV.Filters
{
    public class GaussianFilter : Filter
    {
        public override string Identifier => "Gaussian";

        private readonly float[,] _kernel;

        private readonly float _kernelTotal;

        /// <summary>
        /// Create a Gaussian filter of specific square size and with a specified sigma
        /// </summary>
        /// <param name="size">Length and width of the Gaussian filter (only odd sizes)</param>
        /// <param name="sigma">Standard deviation of the Gaussian distribution</param>
        /// <exception cref="ArgumentException"><see cref="size"/> is odd</exception>
        public GaussianFilter(byte size, float sigma)
        {
            _kernel = CreateGaussianKernel(size, sigma);
            _kernelTotal = _kernel.Cast<float>().Sum();
        }
        
        protected override byte ExecuteStep(int u, int v, byte[,] input)
        {
            var value = 0;

            // For every filter index
            for (var i = 0; i < _kernel.GetLength(0); i++)
            {
                for (var j = 0; j < _kernel.GetLength(1); j++)
                {
                    var du = MathExtensions.Clamp(u - i, 0, input.GetLength(0) - 1);
                    var dv = MathExtensions.Clamp(v - j, 0, input.GetLength(1) - 1);

                    value += (byte)(input[du, dv] * _kernel[i, j]);
                }
            }
            return (byte)(value / _kernelTotal);
        }
        
        private static float[,] CreateGaussianKernel(byte size, float sigma)
        {
            if (size % 2 == 0)
                throw new ArgumentException($"{size} is not an odd size");

            var filter = new float[size, size];
            var k = (size - 1) / 2;


            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    filter[i, j] = Gaussian(i - k, j - k);
                }
            }

            var sum = filter.Cast<float>().Sum();

            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    filter[i, j] /= sum;
                }
            }

            return filter;


            float Gaussian(int x, int y)
            {
                var sigma2 = sigma * sigma;
                var a1 = Math.Exp(-(x * x + y * y) / (2 * sigma2));
                var a0 = 1 / (2 * Math.PI * sigma2);
                return (float)(a0 * a1);
            }
        }
    }
}