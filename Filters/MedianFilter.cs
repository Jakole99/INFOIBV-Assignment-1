using System.Linq;
using INFOIBV.Extensions;
using INFOIBV.Framework;

namespace INFOIBV.Filters
{
    public class MedianFilter : Filter
    {
        public override string Identifier => "Median";

        private readonly int _size;
        
        /// <summary>
        /// Apply median filtering on an input image with a kernel of specified size
        /// </summary>
        /// <param name="size">Length/width of the median filter kernel</param>
        public MedianFilter(int size)
        {
            _size = size;
        }
        
        protected override byte ConvertPixel(int u, int v, byte[,] input)
        {
            var values = new byte[_size*_size];

            // For every median size
            for (var n = 0; n < _size*_size; n++)
            {
                var i = n / _size - _size/2;
                var j = n % _size - _size/2;

                var du = (u - i).Clamp(0, input.GetLength(0) - 1);
                var dv = (v - j).Clamp(0, input.GetLength(1) - 1);

                values[n] = input[du, dv];
            }

            return values.OrderBy(x => x).ElementAt(_size / 2);
        }
    }
    
    public static partial class  PipelineExtensions
    {
        /// <inheritdoc cref="MedianFilter(int)"/>
        public static PipeLine AddMedianFilter(this PipeLine pipeLine, int size)
        {
            return pipeLine.AddFilter(new MedianFilter(size));
        }
    }
}