namespace INFOIBV.Framework;

/// <summary>
/// Pipeline for bitmap conversion using filters
/// </summary>
public sealed class PipeLine
{
    private readonly Queue<Filter> _filters = new();

    /// <summary>
    /// Queue a filter to the pipeline
    /// </summary>
    public PipeLine AddFilter(Filter filter)
    {
        _filters.Enqueue(filter);
        return this;
    }

    /// <summary>
    /// Convert an <see cref="Bitmap" /> to single-channel byte array and apply all the filters
    /// </summary>
    public Bitmap Build(Bitmap image, IProgress<(string, int)> progress)
    {
        var singleChannel = image.ToSingleChannel();

        var totalFilters = _filters.Count;

        // Apply filters
        while (_filters.Count > 0)
        {
            var filter = _filters.Dequeue();
            singleChannel = filter.ConvertParallel(singleChannel);
            progress.Report((filter.Name, 100 - _filters.Count * 100 / totalFilters));
        }

        return singleChannel.ToBitmap();
    }
}