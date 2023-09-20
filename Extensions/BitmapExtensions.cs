using System.Drawing;

namespace INFOIBV.Extensions
{
    public static class BitmapExtensions
    {
        public static byte[,] ToSingleChannel(this Bitmap source)
        {
            var height = source.Size.Height;
            var width = source.Size.Width;
            var singleChannel = new byte[width, height];

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var color = source.GetPixel(x, y);
                    singleChannel[x, y] = (byte)((color.R + color.B + color.G) / 3);
                }
            }

            return singleChannel;
        }
    }
}