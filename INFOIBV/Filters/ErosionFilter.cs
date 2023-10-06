using INFOIBV.Framework;

namespace INFOIBV.Filters;

public class ErosionFilter : Filter
{
    public override string DisplayName => "Erosion";

    private readonly bool _isBinary;
    private readonly (int x, int y, int value)[] _structureElement;

    private readonly (int x, int y, int value)[] _plus =
    {
        (0, 0, 2), (0, 1, 1), (0, -1, 1), (1, 0, 1), (-1, 0, 1)
    };

    public ErosionFilter(StructureElement.Type type, int size, bool isBinary = false)
    {
        _isBinary = isBinary;
        _structureElement = _plus;

        if (size != 3)
            _structureElement = StructureElement.Create(type, size, _plus);
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
    }
}

public partial class FilterCollectionExtensions
{
    public static FilterCollection AddErosionFilter(this FilterCollection filterCollection, StructureElement.Type type, int size, bool isBinary = false)
    {
        return filterCollection.AddProcess(new ErosionFilter(type, size, isBinary));
    }
}