using BenchmarkDotNet.Attributes;
using Framework.Algorithms;
using INFOIBV.Filters;

namespace Benchmark;

public class GaussianBenchmark
{
    [Params(200)]
    public int N;

    private byte[,] _2dArray = default!;
    private byte[] _1dArray = default!;

    private readonly GaussianFilter _oldFilter = new(5, 1);

    [GlobalSetup]
    public void Setup()
    {
        _1dArray = Helper.Create1dArray(N);
        _2dArray = Helper.ConvertTo2dArray(_1dArray);
    }

    [Benchmark]
    public byte[,] Old() => _oldFilter.Process(_2dArray);

    [Benchmark(Baseline = true)]
    public byte[] New() => Algorithm.Gaussian(_1dArray, N, N, 5, 1);
}