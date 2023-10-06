using INFOIBV.Framework;

namespace INFOIBV.Filters;

public partial class FilterCollectionExtensions
{
    public static FilterCollection AddClosingFilter(this FilterCollection filterCollection, StructureElement.Type type,
        int size, bool isBinary = false)
    {
        return filterCollection.AddProcess(
            new FilterCollection("Closing")
                .AddDilationFilter(type, size, isBinary)
                .AddErosionFilter(type, size, isBinary)
        );
    }
}