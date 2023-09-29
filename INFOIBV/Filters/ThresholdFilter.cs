﻿using INFOIBV.Framework;

namespace INFOIBV.Filters;

public class ThresholdFilter : Filter
{
    private readonly int _threshold;

    /// <summary>
    /// Threshold a single-channel image
    /// </summary>
    /// <param name="threshold">threshold value</param>
    public ThresholdFilter(int threshold)
    {
        _threshold = threshold;
    }

    protected override string Name => "Threshold";

    protected override byte ConvertPixel(int u, int v, byte[,] input)
    {
        return input[u, v] < _threshold ? Byte.MinValue : Byte.MaxValue;
    }
}

public static partial class PipelineExtensions
{
    public static PipeLine AddThresholdFilter(this PipeLine pipeLine, int threshold)
    {
        return pipeLine.AddFilter(new ThresholdFilter(threshold));
    }
}