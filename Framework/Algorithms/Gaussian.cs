using System.Buffers;
using System.Collections.Concurrent;

namespace Framework.Algorithms;

// TODO: Finish this mess
public static partial class Algorithm
{
    public static byte[] Gaussian(byte[] input, int width, int height, int kernelWidth, float sigma)
    {
        ThrowHelper.ArgumentIsEven(kernelWidth, nameof(kernelWidth));

        const int maxLength = 20000;
        var n = input.Length;

        var output = ArrayPool<byte>.Shared.Rent(n);

        var kernelLength = kernelWidth * kernelWidth;
        var kernel = ArrayPool<float>.Shared.Rent(kernelLength);
        var k = (kernelWidth - 1) / 2;
        var sigma2 = sigma * sigma;
        float sum = 0;

        // Create gaussian kernel
        var kernelSpan = kernel.AsSpan();
        for (var i = 0; i < kernelLength; i++)
        {
            var x = i % kernelWidth - k;
            var y = i / kernelWidth - k;

            var gaussian = (float)(1 / (2 * Math.PI * sigma2) * Math.Exp(-(x * x + y * y) / (2 * sigma2)));

            kernelSpan[i] = gaussian;
            sum += gaussian;
        }
        // Normalize gaussian
        for (var i = 0; i < kernelLength; i++)
        {
            kernelSpan[i] /= sum;
        }

        var partitioner = Partitioner.Create(0, n);

        // Loop over the partitions in parallel.
        Parallel.ForEach(partitioner, range =>
        {
            // Loop over each range element without a delegate invocation.
            for (var i = range.Item1; i < range.Item2; i++)
            {
                var u = i % width;
                var v = i / width;

                byte value = 0;
                for (var j = 0; j < kernelLength; j++)
                {
                    var ku = j % kernelWidth;
                    var kv = j / kernelWidth;

                    // Clamping is the same as extending the border values
                    var du = Math.Clamp(u - ku + kernelWidth / 2, 0, width - 1);
                    var dv = Math.Clamp(v - kv + kernelWidth / 2, 0, height - 1);

                    value += (byte)Math.Round(input[i] * kernel[j]);
                }

                output[i] = value;
            }
        });

        return output;
    }
}