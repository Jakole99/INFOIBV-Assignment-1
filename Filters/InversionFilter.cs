using System;
using INFOIBV.Framework;

namespace INFOIBV.Filters
{
    /// <summary>
    /// Inversion of a pixel value
    /// </summary>
    public class InversionFilter : Filter
    {
        public override string Identifier => "Inversion";

        protected override byte ExecuteStep(int u, int v, byte[,] input)
        {
            return (byte)(Byte.MaxValue - input[u, v]);
        }
    }
}