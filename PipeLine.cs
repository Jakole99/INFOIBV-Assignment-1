using System;
using System.Collections.Generic;
using System.Drawing;

/// <summary>
/// Pipeline for bitmap conversion using filters
/// </summary>
public sealed class PipeLine
{
    private readonly Queue<Func<byte[,], byte[,]>> _filters;

    public PipeLine()
    {
        _filters = new Queue<Func<byte[,], byte[,]>>();
    }

    /// <summary>
    /// Queue a filter to the pipeline
    /// </summary>
    /// <returns>The same <see cref="PipeLine"/></returns>
    public PipeLine AddFilter(Func<byte[,], byte[,]> filter)
    {
        _filters.Enqueue(filter);
        return this;
    }

    /// <summary>
    /// Convert <see cref="Bitmap"/> to a single channel and apply all the filters then convert it back to a <see cref="Bitmap"/>
    /// </summary>
    /// <returns>Filtered <see cref="Bitmap"/></returns>
    public Bitmap Build(Bitmap image)
    {
        var singleChannel = image.ToSingleChannel();

        // Apply filters
        while (_filters.Count > 0)
        {
            var filter =_filters.Dequeue();
            singleChannel = filter(singleChannel);
        }

        return singleChannel.ToBitmap();
    }
}