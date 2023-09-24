using System;
using INFOIBV.Framework;

namespace INFOIBV.Filters
{
    public class ThresholdFilter : Filter
    {
        public override string Identifier => "Threshold";

        private readonly int _threshold;
        
        /// <summary>
        /// Threshold a single-channel image
        /// </summary>
        /// <param name="threshold">threshold value</param>
        public ThresholdFilter(int threshold)
        {
            _threshold = threshold;
        }
        
        protected override byte TransformPixel(int u, int v, byte[,] input)
        {
            return input[u, v] < _threshold ? Byte.MinValue : Byte.MaxValue;
        }
    }

    public static partial class PipelineExtensions
    {
        public static PipeLine AddThresholdFilter(this PipeLine pipeLine, int threshold)
        {
            return pipeLine.AddFilter(new ThresholdFilter(threshold));
        }
    }
}