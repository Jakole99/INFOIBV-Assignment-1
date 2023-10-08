using System.Collections.Concurrent;

namespace Framework;

public class InversionFilter
{
    public byte[] ProcessAsArray(byte[] input)
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