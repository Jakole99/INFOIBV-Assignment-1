using BenchmarkDotNet.Attributes;
using Framework;
using INFOIBV.Filters;
using Algorithm = Framework.Algorithms.Algorithm;

namespace Benchmark;

public class ThresholdBenchmark
{
    [Params(5000)]
    public int N;

    private byte[,] _2dArray = default!;
    private byte[] _1dArray = default!;

    private readonly ThresholdFilter _oldFilter = new(128);

    [GlobalSetup]
    public void Setup()
    {
        _1dArray = Helper.Create1dArray(N);
        _2dArray = Helper.ConvertTo2dArray(_1dArray);
    }

    [Benchmark]
    public byte[,] Old() => _oldFilter.Process(_2dArray);

    [Benchmark(Baseline = true)]
    public byte[] New() => Algorithm.Threshold(_1dArray, 128);
}