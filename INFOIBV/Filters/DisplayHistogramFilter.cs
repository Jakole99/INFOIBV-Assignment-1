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

    protected override string Name => "Display histogram";

    protected override async Task BeforeConvert(byte[,] input)
    {
        SetOutputDimensions(512, 300);

        _histogram = _isCumulative
            ? await FilterHelper.CreateCumulativeHistogram(input)
            : await FilterHelper.CreateHistogram(input);

        _valueHeight = (float)Height / _histogram.Max();
        _columnWidth = (float)Width / (byte.MaxValue + 1);
    }

    protected override byte ConvertPixel(int u, int v, byte[,] input)
    {
        if (_histogram is null)
            throw new NullReferenceException("Histogram is not set");

        // check horizontal which histogram entry were at
        var value = _histogram[(int)(u / _columnWidth)];

        return Height - value * _valueHeight >= v ? byte.MaxValue : byte.MinValue;
    }
}

public static partial class PipelineExtensions
{
    /// <summary>
    /// Convert an <see cref="Bitmap" /> to single-channel byte array and apply all the filters then display the histogram
    /// </summary>
    /// <remarks>Run asynchronously because we are doing cpu bound computation</remarks>
    public static async Task<Bitmap> DisplayHistogram(this PipeLine pipeLine, Bitmap image,
        IProgress<(string, int)> progress)
    {
        return await pipeLine.AddFilter(new DisplayHistogramFilter()).Build(image, progress);
    }

    /// <summary>
    /// Convert an <see cref="Bitmap" /> to single-channel byte array and apply all the filters then display the cumulative
    /// histogram
    /// </summary>
    /// <remarks>Run asynchronously because we are doing cpu bound computation</remarks>
    public static async Task<Bitmap> DisplayCumulativeHistogram(this PipeLine pipeLine, Bitmap image,
        IProgress<(string, int)> progress)
    {
        return await pipeLine.AddFilter(new DisplayHistogramFilter(true)).Build(image, progress);
    }
}