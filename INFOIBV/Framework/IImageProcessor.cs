namespace INFOIBV.Framework;

public interface IImageProcessor
{
    /// <summary>
    /// User-friendly name of the process
    /// </summary>
    string DisplayName { get; }

    /// <summary>
    /// Convert an image to the processed image
    /// </summary>
    /// <param name="input">Single-channel image</param>
    /// <returns>Processed single-channel image</returns>
    byte[,] Process(byte[,] input);
}

public static class ImageProcessorExtensions
{
    /// <summary>
    /// Overload for directly processing from image to single-channel
    /// </summary>
    public static byte[,] Process(this IImageProcessor processor, Image image)
    {
        return processor.Process(image.ToSingleChannel());
    }
}