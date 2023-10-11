using System.Buffers;
using System.Collections.Concurrent;

namespace Framework.Algorithms;

/// <summary>
/// The idea of <see cref="Algorithms.Algorithm"/> is that we optimize purely for performance
/// </summary>
public static partial class Algorithm
{
    public static byte[] Inversion(byte[] input)
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
                outputSpan[i] = (byte)(Byte.MaxValue ^ inputSpan[i]);
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
                outputSpan[i] = (byte)(Byte.MaxValue ^ inputSpan[i]);
            }
        });

        return output;
    }
}