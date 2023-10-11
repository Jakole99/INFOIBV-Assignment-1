using BenchmarkDotNet.Attributes;
using Framework.Algorithms;
using INFOIBV.Filters;

namespace Benchmark;

[MemoryDiagnoser]
public class InversionBenchmark
{
    [Params(64, 128, 256, 512)]
    public int N;

    private byte[,] _2dArray = default!;
    private byte[] _1dArray = default!;

    private readonly InversionFilter _oldFilter = new();

    [GlobalSetup]
    public void Setup()
    {
        _1dArray = Helper.Create1dArray(N);
        _2dArray = Helper.ConvertTo2dArray(_1dArray);
    }

    [Benchmark]
    public byte[,] Old() => _oldFilter.Process(_2dArray);

    [Benchmark(Baseline = true)]
    public byte[] New() => Algorithm.Inversion(_1dArray);
}