using INFOIBV.Framework;

namespace INFOIBV.Filters;

public class OrFilter : Filter
{
    public override string DisplayName => "OR";

    private readonly IImageProcessor _a;
    private readonly IImageProcessor _b;

    private byte[,] _inputA = default!;
    private byte[,] _inputB = default!;

    public OrFilter(IImageProcessor a, IImageProcessor b)
    {
        _a = a;
        _b = b;
    }

    protected override void BeforeConvert(byte[,] input)
    {
        _inputA = _a.Process(input);
        _inputB = _b.Process(input);
    }

    protected override byte ConvertPixel(int u, int v, byte[,] _)
    {
        //With a Threshold "check", so if an user doesn't give a binary image then we will just make it binary.
        if (_inputA[u, v] < Byte.MaxValue / 2 || _inputB[u, v] > Byte.MaxValue / 2)
            return Byte.MinValue;
        return Byte.MaxValue;
    }
}