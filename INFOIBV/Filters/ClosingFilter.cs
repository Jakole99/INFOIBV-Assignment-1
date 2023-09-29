using INFOIBV.Framework;

namespace INFOIBV.Filters;

public partial class PipelineExtensions
{
    public static PipeLine AddClosingFilter(this PipeLine pipeLine, StructureElement.Type type, int size, bool isBinary = false)
    {
        return pipeLine
            .AddDilationFilter(type, size, isBinary)
            .AddErosionFilter(type, size, isBinary);
    }
}