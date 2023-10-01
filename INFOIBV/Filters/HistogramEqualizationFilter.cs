using INFOIBV.Framework;

namespace INFOIBV.Filters;

public class HistogramEqualizationFilter : Filter
{
    private int _highest, _lowest;
    private int[]? _lookUpTable;
    private int _m, _n, _k;

    public override string Name => "Equalization";

    protected override void BeforeConvert(byte[,] input)
    {
        _m = input.GetLength(0);
        _n = input.GetLength(1);

        _highest = input.Cast<byte>().Max();
        _lowest = input.Cast<byte>().Min();
        _k = _highest - _lowest;

        _lookUpTable = FilterHelper.CreateCumulativeHistogram(input);
    }

    protected override byte ConvertPixel(int u, int v, byte[,] input)
    {
        if (_lookUpTable is null)
            throw new NullReferenceException("Lookup-table is not set");

        var a = input[u, v];
        var equalizedIntensity = (byte)(_lookUpTable[a] * (_k - 1) / (_m * _n));
        return equalizedIntensity;
    }
}

public static partial class PipelineExtensions
{
    public static PipeLine AddHistogramEqualization(this PipeLine pipeLine)
    {
        return pipeLine.AddFilter(new HistogramEqualizationFilter());
    }
}