using INFOIBV.Filters;

namespace INFOIBV;

public enum StructureType
{
    Square,
    Plus
}

public static class StructureElement
{
    private static readonly byte[,] BasePlus =
    {
        { 0, 1, 0 },
        { 2, 2, 1 },
        { 0, 1, 0 }
    };

    public static byte[,] Create(StructureType type, int size)
    {
        if (size % 2 == 0)
            throw new ArgumentException($"{size} is not an odd size");

        return type switch
        {
            StructureType.Square => CreateSquare(size),
            StructureType.Plus => CreatePlus(size),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    private static byte[,] CreatePlus(int size)
    {
        const int baseSize = 3;

        if (size <= baseSize)
            return BasePlus;

        var plus = new byte[size, size];

        var padding = (size - baseSize) / 2;

        for (var i = 0; i < baseSize; i++)
        {
            for (var j = 0; j < baseSize; j++)
            {
                var x = i + padding;
                var y = j + padding;
                plus[x, y] = BasePlus[i, j];
            }
        }

        var dilationFilter = new DilationFilter(StructureType.Plus, 3);

        for (var i = 0; i < padding; i++)
        {
            plus = dilationFilter.Process(plus);
        }

        return plus;
    }

    private static byte[,] CreateSquare(int size)
    {
        var square = new byte [size, size];

        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                square[i, j] = 1;
            }
        }

        return square;
    }
}