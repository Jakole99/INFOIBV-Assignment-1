using INFOIBV.Framework;

namespace INFOIBV.Filters;

public class DisplayHistogramFilter : Filter
{
    private readonly bool _isCumulative;
    private float _columnWidth;
    private int[]? _histogram;
    private float _valueHeight;

    public DisplayHistogramFilter(bool isCumulative = false)
    {
        _isCumulative = isCumulative;
    }

    public override string Name => "Display histogram";

    protected override void BeforeConvert(byte[,] input)
    {
        SetOutputDimensions(512, 300);

        _histogram = _isCumulative
            ? FilterHelper.CreateCumulativeHistogram(input)
            : FilterHelper.CreateHistogram(input);

        _valueHeight = (float)Height / _histogram.Max();
        _columnWidth = (float)Width / (Byte.MaxValue + 1);
    }

    protected override byte ConvertPixel(int u, int v, byte[,] input)
    {
        if (_histogram is null)
            throw new NullReferenceException("Histogram is not set");

        // check horizontal which histogram entry were at
        var value = _histogram[(int)(u / _columnWidth)];

        return Height - value * _valueHeight >= v ? Byte.MaxValue : Byte.MinValue;
    }
}

public static partial class PipelineExtensions
{
    /// <summary>
    /// Convert an <see cref="Bitmap" /> to single-channel byte array and apply all the filters then display the histogram
    /// </summary>
    public static Bitmap DisplayHistogram(this PipeLine pipeLine, Bitmap image,
        IProgress<(string, int)> progress)
    {
        return pipeLine.AddFilter(new DisplayHistogramFilter()).Build(image, progress);
    }

    /// <summary>
    /// Convert an <see cref="Bitmap" /> to single-channel byte array and apply all the filters then display the cumulative
    /// histogram
    /// </summary>
    public static Bitmap DisplayCumulativeHistogram(this PipeLine pipeLine, Bitmap image,
        IProgress<(string, int)> progress)
    {
        return pipeLine.AddFilter(new DisplayHistogramFilter(true)).Build(image, progress);
    }

    /// <summary>
    /// Convert an <see cref="Bitmap" /> to single-channel byte array and apply all the filters then display based on the <see cref="ModeType"/>
    /// </summary>
    public static Bitmap DisplayMode(this PipeLine pipeLine, ModeType mode, Bitmap image,
        IProgress<(string, int)> progress)
    {
        return mode switch
        {
            ModeType.Normal => pipeLine.Build(image, progress),
            ModeType.Histogram => pipeLine.DisplayHistogram(image, progress),
            ModeType.CumulativeHistogram => pipeLine.DisplayCumulativeHistogram(image, progress),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };
    }
}