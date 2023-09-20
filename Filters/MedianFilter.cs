using INFOIBV.Framework;

namespace INFOIBV.Filters
{
    public class MedianFilter : Filter
    {
        public override string Identifier => "Median";
        
        protected override byte ExecuteStep(int u, int v, byte[,] input)
        {
            throw new System.NotImplementedException();
        }
    }
}