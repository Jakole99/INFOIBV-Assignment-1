using System.Collections.Generic;
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
        
        protected override byte ExecuteStep(int u, int v, byte[,] input)
        {
            var values = new List<byte>();

            // For every median size
            for (var i = 0; i < _size; i++)
            {
                for (var j = 0; j < _size; j++)
                {
                    var du = MathExtensions.Clamp(u - i, 0, input.GetLength(0) - 1);
                    var dv = MathExtensions.Clamp(v - j, 0, input.GetLength(1) - 1);

                    values.Add(input[du, dv]);
                }
            }
            
            return values.OrderBy(x => x).ElementAt(values.Count / 2);
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