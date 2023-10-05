using INFOIBV.Filters;
using System.Drawing;

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


                return convertByteToTuple(squareElement);
            case Type.Plus:
                var dilationFilter = new DilationFilter(Type.Plus, 3, false);

                var resultingPlusElement = dilationFilter.ConvertParallel(convertTupleToByte(baseStructure, size));

                var rounds = ((size - 3) / 2) - 1; //since we already did one round above

                while (rounds > 0)
                {
                    resultingPlusElement = dilationFilter.ConvertParallel(resultingPlusElement);
                    rounds -= 1;
                }

                return convertByteToTuple(resultingPlusElement);
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    private static (int x, int y, int value)[] convertByteToTuple(byte[,] input)
    {
        List<(int x, int y, int value)> elements = new List<(int x, int y, int value)>();

        for (int i = 0; i < input.GetLength(0); i++)
        {
            for (int j = 0; j < input.GetLength(1); j++)
            {
                if (input[i, j] == 0)
                    continue;

                elements.Add((i, j, input[i, j]));

            }
        }

        return elements.ToArray();
    }

    private static byte[,] convertTupleToByte((int x, int y, int value)[] tuple, int size)
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