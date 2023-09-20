using INFOIBV.Framework;

namespace INFOIBV.Filters
{
    public class EdgeMagnitudeFilter : Filter
    {
        public override string Identifier => "Edge Magnitude";
        
        protected override byte ExecuteStep(int u, int v, byte[,] input)
        {
            throw new System.NotImplementedException();
        }
    }
}