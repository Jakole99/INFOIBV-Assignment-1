using INFOIBV.Framework;

namespace INFOIBV.Filters;

public class DilationFilter : Filter
{
    protected override string Name => "Dilation";

    private readonly bool _isBinary;
    private readonly (int x, int y, int value)[] _structureElement;

    private readonly (int x, int y, int value)[] _plus =
    {
        (0, 0, 2), (0, 1, 1), (0, -1, 1), (1, 0, 1), (-1, 0, 1)
    };

    public DilationFilter(StructureElementType type, int size, bool isBinary = false)
    {
        _isBinary = isBinary;
        _structureElement = _plus;

        // TODO: Use this method
        _ = FilterHelper.CreateStructuringElement(type, size);
    }

    protected override byte ConvertPixel(int u, int v, byte[,] input)
    {
        var returnValue = Byte.MinValue;

        foreach (var (x, y, value) in _structureElement)
        {
            var du = u + x;
            var dv = v + y;

            if (du < 0 || du >= Width)
                continue;

            if (dv < 0 || dv >= Height)
                continue;

            if (!_isBinary)
            {
                // Grayscale
                var sum = (byte)Math.Clamp(value + input[du, dv], 0, 255);
                returnValue = Math.Max(returnValue, sum);
                continue;
            }

            if (input[du, dv] > Byte.MaxValue / 2)
                return Byte.MaxValue;
        }

        return returnValue;
    }
}

public partial class PipelineExtensions
{
    public static PipeLine AddDilationFilter(this PipeLine pipeLine, StructureElementType type, int size, bool isBinary = false)
    {
        return pipeLine.AddFilter(new DilationFilter(type, size, isBinary));
    }
}