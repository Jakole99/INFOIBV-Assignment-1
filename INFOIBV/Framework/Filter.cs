namespace INFOIBV.Framework;

/// <summary>
/// Abstract definition of a filter
/// </summary>
public abstract class Filter
{
    /// <summary>
    /// User friendly name of the filter
    /// </summary>
    protected abstract string Name { get; }

    /// <summary>
    /// Output Width
    /// </summary>
    protected int Width { get; private set; }

    /// <summary>
    /// Output Height
    /// </summary>
    protected int Height { get; private set; }

    /// <summary>
    /// Set size of the output
    /// </summary>
    protected void SetOutputDimensions(int width, int height)
    {
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Execute a single conversion on a specific point using the input.
    /// </summary>
    /// <param name="u">Horizontal index</param>
    /// <param name="v">Vertical index</param>
    /// <param name="input">Single-channel image</param>
    /// <returns>Resulting value</returns>
    protected abstract byte ConvertPixel(int u, int v, byte[,] input);

    /// <summary>
    /// Useful for pre-computation of values needed for every pixel
    /// </summary>
    protected virtual void BeforeConvert(byte[,] input)
    {
    }

    /// <summary>
    /// Convert an image to the filtered image
    /// </summary>
    /// <param name="input">Single-channel image</param>
    /// <param name="progress">Progress tuple (Identifier, Percentage)</param>
    /// <returns>Filtered single-channel image</returns>
    public async Task<byte[,]> ConvertParallel(byte[,] input, IProgress<(string, int)>? progress = null)
    {
        // Run the convert on another thread
        return await Task.Run(() =>
        {
            Width = input.GetLength(0);
            Height = input.GetLength(1);

            BeforeConvert(input);

            // Origin of small object heap size but is allowed
            var output = new byte[Width, Height];
            var current = 0;

            progress?.Report((Name, Percentage(current)));


            Parallel.For(0, output.Length, i =>
            {
                var u = i % Width;
                var v = i / Width;
                current++;

                output[u, v] = ConvertPixel(u, v, input);

                if (Percentage(current) > Percentage(current - 1))
                    progress?.Report((Name, Percentage(current)));
            });

            return output;

            int Percentage(int value)
            {
                return value * 100 / output.Length;
            }
        });
    }
}