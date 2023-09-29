using INFOIBV.Framework;

namespace INFOIBV.Filters;

public partial class PipelineExtensions
{
    public static PipeLine AddOpeningFilter(this PipeLine pipeLine, StructureElement.Type type, int size, bool isBinary = false)
    {
        return pipeLine
            .AddErosionFilter(type, size, isBinary)
            .AddDilationFilter(type, size, isBinary);
    }
}