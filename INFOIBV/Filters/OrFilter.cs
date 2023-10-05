using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INFOIBV.Framework;

namespace INFOIBV.Filters;

public class OrFilter : Filter
{
    public override string Name => "orImage";

    private readonly byte[,] _input2;

    public OrFilter(byte[,] input)
    {
        _input2 = input;
    }
    protected override byte ConvertPixel(int u, int v, byte[,] input)
    {
        //With a Threshold "check", so if an user doesn't give a binary image then we will just make it binary.
        if (input[u, v] < Byte.MaxValue / 2 || _input2[u, v] > Byte.MaxValue / 2)
            return Byte.MinValue;
        return Byte.MaxValue;
    }
}


public partial class PipelineExtensions
{
    public static PipeLine AddOrFilter(this PipeLine pipeLine, byte[,] input)
    {
        return pipeLine.AddFilter(new OrFilter(input));
    }
}