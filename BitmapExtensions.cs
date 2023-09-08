using System.Drawing;

public static class BitmapExtensions
{
    public static byte[,] ToGrayScale(this Bitmap source)
    {
        var height = source.Size.Height;
        var width = source.Size.Width;

        var grayScale = new byte[width, height];
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                var color = source.GetPixel(x, y);
                grayScale[x, y] = (byte)((color.R + color.B + color.G) / 3);
            }

        return grayScale;
    }

    public static Bitmap FromGrayScale(byte[,] grayScale)
    {
        var width = grayScale.GetLength(0);
        var height = grayScale.GetLength(1);

        var output = new Bitmap(width, height);

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                var value = grayScale[x, y];
                var newColor = Color.FromArgb(value, value, value);
                output.SetPixel(x, y, newColor);
            }

        return output;
    }
}