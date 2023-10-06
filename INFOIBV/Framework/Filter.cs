namespace INFOIBV.Framework;

/// <summary>
/// Abstract definition of a filter
/// </summary>
public abstract class Filter : IImageProcessor
{
    public abstract string DisplayName { get; }

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
        // TODO: Make this different
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

    public byte[,] Process(byte[,] input)
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

    public override string ToString()
    {
        return DisplayName;
    }
}