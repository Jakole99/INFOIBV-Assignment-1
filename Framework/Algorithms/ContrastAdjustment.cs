using System.Collections.Concurrent;

namespace Framework.Algorithms;

public static partial class Algorithm
{
    public static byte[] ContrastAdjustment(byte[] input)
    {
        var output = new byte[input.Length];

        var span = input.AsSpan();
        var max = Byte.MinValue;
        var min = Byte.MaxValue;
        foreach (var pixel in span)
        {
            max = Math.Max(max, pixel);
            min = Math.Min(min, pixel);
        }
        var coefficient = Byte.MaxValue / (max - min);

        var partitioner = Partitioner.Create(0, output.Length);

        // Loop over the partitions in parallel.
        Parallel.ForEach(partitioner, range =>
        {
            // Loop over each range element without a delegate invocation.
            for (var i = range.Item1; i < range.Item2; i++)
            {
                output[i] = (byte)((input[i] - max) * coefficient);
            }
        });

        return output;
    }
}