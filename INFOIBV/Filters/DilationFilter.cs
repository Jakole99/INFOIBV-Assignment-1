﻿using INFOIBV.Framework;

namespace INFOIBV.Filters;

public class DilationFilter : Filter
{
    public override string DisplayName => "Dilation";

    private readonly bool _isBinary;
    private readonly byte[,] _structureElement;
    private readonly int _size;

    public DilationFilter(StructureType type, int size, bool isBinary = false)
    {
        _isBinary = isBinary;
        _structureElement = StructureElement.Create(type, size);
        _size = size;
    }

    protected override byte ConvertPixel(int u, int v, byte[,] input)
    {
        var returnValue = Byte.MinValue;

        // For every filter index
        for (var i = 0; i < _size; i++)
        {
            for (var j = 0; j < _size; j++)
            {
                var du = u + i - _size / 2;
                var dv = v + j - _size / 2;

                if (du < 0 || du >= Width)
                    continue;

                if (dv < 0 || dv >= Height)
                    continue;


                if (!_isBinary)
                {
                    // Grayscale
                    var sum = (byte)Math.Clamp(_structureElement[i, j] + input[du, dv], 0, 255);
                    returnValue = Math.Max(returnValue, sum);
                    continue;
                }

                if (input[du, dv] > Byte.MaxValue / 2)
                    return Byte.MaxValue;
            }
        }

        return returnValue;
    }
}

public partial class FilterCollectionExtensions
{
    public static FilterCollection AddDilationFilter(this FilterCollection filterCollection, StructureType type,
        int size, bool isBinary = false)
    {
        return filterCollection.AddProcess(new DilationFilter(type, size, isBinary));
    }
}