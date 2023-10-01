namespace INFOIBV.Framework;

/// <summary>
/// Abstract definition of a filter
/// </summary>
public abstract class Filter
{
    /// <summary>
    /// User friendly name of the filter
    /// </summary>
    public abstract string Name { get; }

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
    /// <returns>Filtered single-channel image</returns>
    public byte[,] ConvertParallel(byte[,] input)
    {
        Width = input.GetLength(0);
        Height = input.GetLength(1);

        BeforeConvert(input);

        // Origin of small object heap size but is allowed
        var output = new byte[Width, Height];

        Parallel.For(0, Height, v =>
        {
            for (var u = 0; u < Width; u++)
            {
                output[u, v] = ConvertPixel(u, v, input);
            }
        });

        return output;
    }
}