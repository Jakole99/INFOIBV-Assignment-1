using INFOIBV.Framework;

namespace INFOIBV.Filters;

public class GaussianFilter : Filter
{
    private readonly float[,] _gaussian;

    /// <summary>
    ///     Create a Gaussian filter of specific square size and with a specified sigma
    /// </summary>
    /// <param name="size">Length and width of the Gaussian filter (only odd sizes)</param>
    /// <param name="sigma">Standard deviation of the Gaussian distribution</param>
    /// <exception cref="ArgumentException"><see cref="size" /> is not an odd size</exception>
    public GaussianFilter(int size, float sigma)
    {
        if (size % 2 == 0)
            throw new ArgumentException($"{size} is not an odd size");

        _gaussian = CreateGaussianKernel((byte)size, sigma);
    }

    public override string Name => "Gaussian";

    protected override byte ConvertPixel(int u, int v, byte[,] input)
    {
        return FilterHelper.ConvolvePixel(input, _gaussian, u, v);
    }

    private static float[,] CreateGaussianKernel(byte size, float sigma)
    {
        var kernel = new float[size, size];
        var k = (size - 1) / 2;
        var sum = 0f;

        // Create gaussian
        for (var i = 0; i < size; i++)
        for (var j = 0; j < size; j++)
        {
            var gaussian = Gaussian(i - k, j - k);

            kernel[i, j] = gaussian;
            sum += gaussian;
        }

        // Normalize gaussian
        for (var i = 0; i < size; i++)
        for (var j = 0; j < size; j++)
            kernel[i, j] /= sum;

        return kernel;


        float Gaussian(int x, int y)
        {
            var sigma2 = sigma * sigma;
            var a1 = Math.Exp(-(x * x + y * y) / (2 * sigma2));
            var a0 = 1 / (2 * Math.PI * sigma2);
            return (float)(a0 * a1);
        }
    }
}

public static partial class PipelineExtensions
{
    /// <inheritdoc cref="GaussianFilter(int, float)" />
    public static PipeLine AddGaussian(this PipeLine pipeLine, int size, int sigma)
    {
        return pipeLine.AddFilter(new GaussianFilter(size, sigma));
    }
}