using INFOIBV.Framework;

namespace INFOIBV.Filters;

public class HistogramEqualizationFilter : Filter
{
    private int _highest, _lowest;
    private int[] _histogram = default!;
    private int _m, _n, _k;

    public override string DisplayName => "Equalization";

    protected override void BeforeConvert(byte[,] input)
    {
        _m = input.GetLength(0);
        _n = input.GetLength(1);

        _highest = input.Cast<byte>().Max();
        _lowest = input.Cast<byte>().Min();
        _k = _highest - _lowest;

        _histogram = new Histogram(input).GetCumulativeValues();
    }

    protected override byte ConvertPixel(int u, int v, byte[,] input)
    {
        var a = input[u, v];
        var equalizedIntensity = (byte)(_histogram[a] * (_k - 1) / (_m * _n));
        return equalizedIntensity;
    }
}

public static partial class FilterCollectionExtensions
{
    public static FilterCollection AddHistogramEqualization(this FilterCollection filterCollection)
    {
        return filterCollection.AddProcess(new HistogramEqualizationFilter());
    }
}