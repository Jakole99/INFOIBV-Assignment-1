using INFOIBV.Filters;

namespace INFOIBV;

public static class StructureElement
{
    public enum Type
    {
        Square,
        Plus
    }

    public static (int x, int y, int value)[] Create(Type type, int size, (int x, int y, int value)[] baseStructure)
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


                return ConvertByteToTuple(squareElement);
            case Type.Plus:
                var dilationFilter = new DilationFilter(Type.Plus, 3);

                var resultingPlusElement = dilationFilter.ConvertParallel(ConvertTupleToByte(baseStructure, size));

                var rounds = ((size - 3) / 2) - 1; //since we already did one round above

                while (rounds > 0)
                {
                    resultingPlusElement = dilationFilter.ConvertParallel(resultingPlusElement);
                    rounds -= 1;
                }

                return ConvertByteToTuple(resultingPlusElement);
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    private static (int x, int y, int value)[] ConvertByteToTuple(byte[,] input)
    {
        var elements = new List<(int x, int y, int value)>();

        for (var i = 0; i < input.GetLength(0); i++)
        {
            for (var j = 0; j < input.GetLength(1); j++)
            {
                if (input[i, j] == 0)
                    continue;

                elements.Add((i, j, input[i, j]));

            }
        }

        return elements.ToArray();
    }

    private static byte[,] ConvertTupleToByte((int x, int y, int value)[] tuple, int size)
    {
        var input = new byte[size, size];

        foreach (var (i, j, value) in tuple)
        {
            var x = i + size / 2;
            var y = j + size / 2;
            input[x, y] = (byte)value;
        }

        return input;
    }
}