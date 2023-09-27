namespace INFOIBV.Extensions;

public static class ByteArrayExtensions
{
    public static Bitmap ToBitmap(this byte[,] singleChannel)
    {
        var width = singleChannel.GetLength(0);
        var height = singleChannel.GetLength(1);
        var output = new Bitmap(width, height);

        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
        {
            var value = singleChannel[x, y];
            var newColor = Color.FromArgb(value, value, value);
            output.SetPixel(x, y, newColor);
        }

        return output;
    }
}