using System.Collections.Concurrent;

namespace Framework.Algorithms;

public static partial class Algorithm
{
    public static byte[] Threshold(byte[] input, byte threshold)
    {
        var output = new byte[input.Length];

        var partitioner = Partitioner.Create(0, output.Length);

        // Loop over the partitions in parallel.
        Parallel.ForEach(partitioner, range =>
        {
            // Loop over each range element without a delegate invocation.
            for (var i = range.Item1; i < range.Item2; i++)
            {
                output[i] = input[i] < threshold ? Byte.MinValue : Byte.MaxValue;
            }
        });

        return output;
    }
}