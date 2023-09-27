using INFOIBV.Framework;

namespace INFOIBV.Filters;

/// <summary>
/// Calculate the image derivative of an input image and a provided edge kernel
/// </summary>
public class EdgeMagnitudeFilter : Filter
{
    private readonly sbyte[,] _horizontalKernel =
    {
        { -1, -2, -1 },
        { 0, 0, 0 },
        { 1, 2, 1 }
    };

    private readonly sbyte[,] _verticalKernel =
    {
        { -1, 0, 1 },
        { -2, 0, 2 },
        { -1, 0, 1 }
    };

    public override string Name => "Edge Magnitude";

    protected override byte ConvertPixel(int u, int v, byte[,] input)
    {
        // For every Horizontal filter index
        var horizontalValue = FilterHelper.ConvolvePixel(input, _horizontalKernel, u, v);

        // For every Vertical filter index
        var verticalValue = FilterHelper.ConvolvePixel(input, _verticalKernel, u, v);

        return (byte)Math.Sqrt(horizontalValue * horizontalValue + verticalValue * verticalValue);
    }
}

public static partial class PipelineExtensions
{
    /// <inheritdoc cref="EdgeMagnitudeFilter" />
    public static PipeLine AddEdgeMagnitudeFilter(this PipeLine pipeLine)
    {
        return pipeLine.AddFilter(new EdgeMagnitudeFilter());
    }
}