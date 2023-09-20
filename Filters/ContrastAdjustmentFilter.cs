using System;
using System.Linq;
using INFOIBV.Framework;

namespace INFOIBV.Filters
{
    public class ContrastAdjustmentFilter : Filter
    {
        public override string Identifier => "Contrast Adjustment";
        
        protected override byte ExecuteStep(int u, int v, byte[,] input)
        {
            int aHigh = input.Cast<byte>().Max();
            int aLow = input.Cast<byte>().Min();

            return (byte)(Byte.MinValue + (input[u, v] - aLow) * Byte.MaxValue / (aHigh - aLow));
        }
    }
}