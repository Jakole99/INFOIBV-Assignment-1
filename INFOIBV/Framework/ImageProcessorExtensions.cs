namespace INFOIBV.Framework;

public static class ImageProcessorExtensions
{
    /// <summary>
    /// Convert an <see cref="Bitmap" /> to single-channel byte array and apply all the filters
    /// </summary>
    public static Bitmap Build(this IImageProcessor processor, Image image)
    {
        return processor.Process(image.ToSingleChannel()).ToBitmap();
    }
}