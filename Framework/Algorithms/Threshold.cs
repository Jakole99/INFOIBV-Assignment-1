using System.Buffers;
using System.Collections.Concurrent;

namespace Framework.Algorithms;

public static partial class Algorithm
{
    public static byte[] Threshold(byte[] input, byte threshold)
    {
        const int maxLength = 20000;
        var n = input.Length;

        var output = ArrayPool<byte>.Shared.Rent(n);

        // Due to thread spin-up, below the maxLength is faster single threaded
        if (n < maxLength)
        {
            var inputSpan = new ReadOnlySpan<byte>(input);
            var outputSpan = output.AsSpan();
            for (var i = 0; i < n; i++)
            {
                outputSpan[i] = inputSpan[i] < threshold ? Byte.MinValue : Byte.MaxValue;
            }

            return output;
        }

        var partitioner = Partitioner.Create(0, n);
        Parallel.ForEach(partitioner, range =>
        {
            var inputSpan = new ReadOnlySpan<byte>(input);
            var outputSpan = output.AsSpan();
            for (var i = range.Item1; i < range.Item2; i++)
            {
                outputSpan[i] = inputSpan[i] < threshold ? Byte.MinValue : Byte.MaxValue;
            }
        });

        return output;
    }
}