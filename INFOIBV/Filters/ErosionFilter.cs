using INFOIBV.Framework;

namespace INFOIBV.Filters;

public class ErosionFilter : Filter
{
    protected override string Name => "Erosion";

    private readonly bool _isBinary;
    private readonly (int x, int y, int value)[] _structureElement;

    private readonly (int x, int y, int value)[] _plus =
    {
        (0, 0, 2), (0, 1, 1), (0, -1, 1), (1, 0, 1), (-1, 0, 1)
    };

    private byte[,]? _binaryImage;

    public ErosionFilter(StructureElement.Type type, int size, bool isBinary = false)
    {
        _isBinary = isBinary;
        _structureElement = _plus;

        // TODO: Use this method
        _ = StructureElement.Create(type, size);
    }

    protected override async Task BeforeConvert(byte[,] input)
    {
        if (_isBinary)
        {
            var threshold = new ThresholdFilter(128);
            _binaryImage = await threshold.ConvertParallel(input);
        }
    }

    protected override byte ConvertPixel(int u, int v, byte[,] input)
    {
        var returnValue = Byte.MaxValue;

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
                returnValue = Math.Min(returnValue, sum);
                continue;
            }

            if (input[du, dv] < Byte.MaxValue / 2)
                return Byte.MinValue;
        }

        return returnValue;

        if (_isBinary)
        {
            foreach (var (x, y, _) in _structureElement)
            {
                var du = u + x;
                var dv = v + y;

                if (du < 0 || du >= Width)
                    continue;

                if (dv < 0 || dv >= Height)
                    continue;

                var value = _binaryImage?[du, dv];
                if (value == Byte.MinValue)
                    return Byte.MinValue;
            }

            return Byte.MaxValue;
        }

        var min = Byte.MinValue;
        foreach (var (x, y, value) in _structureElement)
        {
            var du = u + x;
            var dv = v + y;

            if (du < 0 || du >= Width)
                continue;

            if (dv < 0 || dv >= Height)
                continue;

            var sum = (byte)Math.Clamp(value + input[du, dv], 0, 255);
            min = Math.Min(min, sum);
        }

        return min;
    }
}

public partial class PipelineExtensions
{
    public static PipeLine AddErosionFilter(this PipeLine pipeLine, StructureElement.Type type, int size, bool isBinary = false)
    {
        return pipeLine.AddFilter(new ErosionFilter(type, size, isBinary));
    }
}