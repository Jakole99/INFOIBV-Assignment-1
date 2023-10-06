using INFOIBV.Framework;

namespace INFOIBV.Filters;

[Obsolete("This is not the correct way")]
public class DisplayHistogramFilter : Filter
{
    private readonly bool _isCumulative;
    private float _columnWidth;
    private int[]? _values;
    private float _valueHeight;

    public DisplayHistogramFilter(bool isCumulative = false)
    {
        _isCumulative = isCumulative;
    }

    public override string DisplayName => "Display histogram";

    protected override void BeforeConvert(byte[,] input)
    {
        SetOutputDimensions(512, 300);

        _values = _isCumulative
            ? new Histogram(input).GetCumulativeValues()
            : new Histogram(input).Values;

        _valueHeight = (float)Height / _values.Max();
        _columnWidth = (float)Width / (Byte.MaxValue + 1);
    }

    protected override byte ConvertPixel(int u, int v, byte[,] input)
    {
        if (_values is null)
            throw new NullReferenceException("Histogram is not set");

        // check horizontal which histogram entry were at
        var value = _values[(int)(u / _columnWidth)];

        return Height - value * _valueHeight >= v ? Byte.MaxValue : Byte.MinValue;
    }
}