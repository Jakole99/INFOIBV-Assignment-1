using INFOIBV.Framework;

namespace INFOIBV.Filters;

public partial class FilterCollectionExtensions
{
    public static FilterCollection AddOpeningFilter(this FilterCollection filterCollection, StructureElement.Type type,
        int size, bool isBinary = false)
    {
        return filterCollection.AddProcess(
            new FilterCollection("Opening")
                .AddErosionFilter(type, size, isBinary)
                .AddDilationFilter(type, size, isBinary)
        );
    }
}