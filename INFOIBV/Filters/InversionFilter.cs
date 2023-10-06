using INFOIBV.Framework;

namespace INFOIBV.Filters;

/// <summary>
/// Invert a single channel (grayscale) image
/// </summary>
public class InversionFilter : Filter
{
    public override string DisplayName => "Inversion";

    protected override byte ConvertPixel(int u, int v, byte[,] input)
    {
        return (byte)(Byte.MaxValue - input[u, v]);
    }
}

public static partial class FilterCollectionExtensions
{
    public static FilterCollection AddInversionFilter(this FilterCollection filterCollection)
    {
        return filterCollection.AddProcess(new InversionFilter());
    }
}