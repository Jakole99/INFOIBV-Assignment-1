namespace INFOIBV;

public static class StructureElement
{
    public enum Type
    {
        Square,
        Plus
    }

    public static byte[,] CreateOld(Type type, int size)
    {
        if (size % 2 == 0)
            throw new ArgumentException($"{size} is not an odd size");

        switch (type)
        {
            case Type.Square:
                var squareElement = new byte [size, size];
                for (var i = 0; i < size; i++)
                for (var j = 0; j < size; j++)
                    squareElement[i, j] = 1;

                return squareElement;
            case Type.Plus:
                var plusElement = new byte[size, size];
                for (var i = 0; i < size; i++)
                for (var j = 0; j < size; j++)
                    if (i == size / 2 || j == size / 2)
                        plusElement[i, j] = 1;

                return plusElement;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    public static (int x, int y, int value)[] Create(Type type, int size)
    {
        if (size % 2 == 0)
            throw new ArgumentException($"{size} is not an odd size");

        return type switch
        {
            Type.Square => CreateSquare(size),
            Type.Plus => CreatePlus(size),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    private static (int x, int y, int value)[] CreateSquare(int size)
    {
        var element = new (int x, int y, int value)[size * size];

        for (var i = 0; i < size*size; i++)
        {
            var x = i / size;
            var y = i % size;

        }

        return element;
    }

    private static (int x, int y, int value)[] CreatePlus(int size)
    {
        var element = new byte[size, size];


        return new (int x, int y, int value)[] {(0, 0, 0)};
    }
}