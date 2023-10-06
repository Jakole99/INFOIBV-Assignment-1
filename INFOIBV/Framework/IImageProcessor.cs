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