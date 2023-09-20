using INFOIBV.Framework;

namespace INFOIBV.Filters
{
    public class ThresholdFilter : Filter
    {
        public override string Identifier => "Threshold";
        
        protected override byte ExecuteStep(int u, int v, byte[,] input)
        {
            throw new System.NotImplementedException();
        }
    }
}