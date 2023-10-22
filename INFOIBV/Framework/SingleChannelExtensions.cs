namespace INFOIBV.Framework;

public static class SingleChannelExtensions
{
    /// <summary>
    /// Convert a <see cref="Bitmap" /> to a single-channel image
    /// </summary>
    /// <param name="source">
    ///     <see cref="Bitmap" />
    /// </param>
    /// <returns>Single-channel image</returns>
    public static byte[,] ToSingleChannel(this Image source)
    {
        if (source is not Bitmap bitmap)
            throw new Exception("WTF");

        var height = source.Size.Height;
        var width = source.Size.Width;
        var singleChannel = new byte[width, height];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var color = bitmap.GetPixel(x, y);
                singleChannel[x, y] = (byte)((color.R + color.B + color.G) / 3);
            }
        }

        return singleChannel;
    }

    /// <summary>
    /// Convert a single-channel image to a <see cref="Bitmap" />
    /// </summary>
    /// <param name="singleChannel">Single-channel image</param>
    /// <returns>
    ///     <see cref="Bitmap" />
    /// </returns>
    public static Bitmap ToBitmap(this byte[,] singleChannel)
    {
        var width = singleChannel.GetLength(0);
        var height = singleChannel.GetLength(1);
        var output = new Bitmap(width, height);

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var value = singleChannel[x, y];
                var newColor = Color.FromArgb(value, value, value);
                output.SetPixel(x, y, newColor);
            }
        }

        return output;
    }
}