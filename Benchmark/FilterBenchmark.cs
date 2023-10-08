using BenchmarkDotNet.Attributes;
using INFOIBV.Filters;

namespace Benchmark;

public class FilterBenchmark
{
    [Params(100, 1000)]
    public int N;

    private byte[,] _2dArray = default!;
    private byte[] _1dArray = default!;
    private IReadOnlyCollection<byte> _readOnly = default!;

    private readonly InversionFilter _inversionFilter = new();
    private readonly Framework.InversionFilter _newInversion = new();

    [GlobalSetup]
    public void Setup()
    {
        _1dArray = new byte[N * N];
        new Random(42).NextBytes(_1dArray);

        _2dArray = new byte[N, N];
        Buffer.BlockCopy(_1dArray, 0, _2dArray, 0, N * N);

        _readOnly = Array.AsReadOnly(_1dArray);
    }

    //[Benchmark(Baseline = true)]
    public byte[,] OldImplementation() => _inversionFilter.Process(_2dArray);

    [Benchmark]
    public byte[] Normal() => _newInversion.ProcessAsArray(_1dArray);
}