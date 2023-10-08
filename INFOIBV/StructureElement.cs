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
        {0, 1, 0},
        {2, 2, 1},
        {0, 1, 0}
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
        if (size <= 3)
            return BasePlus;

        var dilationFilter = new DilationFilter(StructureType.Plus, 3);

        var currentPlus = BasePlus;

        var rounds = (size - 3) / 2;

        for (var i = 0; i < rounds; i++)
        {
            currentPlus = dilationFilter.Process(currentPlus);
        }

        return currentPlus;
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