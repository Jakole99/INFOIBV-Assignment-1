using System.Collections.Concurrent;

namespace Framework.Algorithms;

/// <summary>
/// The idea of <see cref="Algorithms.Algorithm"/> is that we optimize purely for performance
/// </summary>
public static partial class Algorithm
{
    public static byte[] Inversion(byte[] input)
    {
        var output = new byte[input.Length];

        var partitioner = Partitioner.Create(0, output.Length);

        // Loop over the partitions in parallel.
        Parallel.ForEach(partitioner, range =>
        {
            // Loop over each range element without a delegate invocation.
            for (var i = range.Item1; i < range.Item2; i++)
            {
                output[i] = (byte)(Byte.MaxValue - input[i]);
            }
        });

        return output;
    }
}