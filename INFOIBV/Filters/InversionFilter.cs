using INFOIBV.Framework;

namespace INFOIBV.Filters;

/// <summary>
/// Invert a single channel (grayscale) image
/// </summary>
public class InversionFilter : Filter
{
    public override string Name => "Inversion";

    protected override byte ConvertPixel(int u, int v, byte[,] input)
    {
        return (byte)(byte.MaxValue - input[u, v]);
    }
}

public static partial class PipelineExtensions
{
    public static PipeLine AddInversionFilter(this PipeLine pipeLine)
    {
        return pipeLine.AddFilter(new InversionFilter());
    }
}