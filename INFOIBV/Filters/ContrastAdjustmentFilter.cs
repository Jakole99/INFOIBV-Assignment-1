using INFOIBV.Framework;

namespace INFOIBV.Filters;

/// <summary>
/// Create an image with the full range of intensity values used
/// </summary>
public class ContrastAdjustmentFilter : Filter
{
    private int _highest;
    private int _lowest;
    protected override string Name => "Contrast Adjustment";

    protected override Task BeforeConvert(byte[,] input)
    {
        _highest = input.Cast<byte>().Max();
        _lowest = input.Cast<byte>().Min();
        return Task.CompletedTask;
    }

    protected override byte ConvertPixel(int u, int v, byte[,] input)
    {
        return (byte)(Byte.MinValue + (input[u, v] - _highest) * Byte.MaxValue / (_highest - _lowest));
    }
}

public static partial class PipelineExtensions
{
    /// <summary>
    /// Adds contrast adjustment filter to the pipeline
    /// </summary>
    public static PipeLine AddContrastAdjustment(this PipeLine pipeLine)
    {
        return pipeLine.AddFilter(new ContrastAdjustmentFilter());
    }
}