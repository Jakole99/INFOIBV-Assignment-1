using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using INFOIBV.Framework;

namespace INFOIBV
{
    public class Filters
    {
        private readonly ProgressBar _progressBar;

        public Filters(ProgressBar progressBar)
        {
            _progressBar = progressBar;
        }

        /// <summary>
        /// Applies a specified filter
        /// </summary>
        /// <param name="filter">Type of filter</param>
        /// <param name="image">Three-channel image</param>
        /// <returns>One-channel image</returns>
        public Bitmap ApplyFilter(FilterType filter, Bitmap image)
        {
            var singleChannel = image.ToSingleChannel();
            var gaussianFilter = CreateGaussianFilter(5, 1);
            var newSingleChannel = ConvolveImage(singleChannel, gaussianFilter);

            switch (filter)
            {
                case FilterType.GrayScale:
                    return new PipeLine()
                        .Build(image);

                case FilterType.Invert:
                    return new PipeLine()
                        .AddFilter(InvertImage)
                        .Build(image);

                case FilterType.AdjustContrast:
                    return new PipeLine()
                        .AddFilter(AdjustContrast)
                        .Build(image);

                case FilterType.Gaussian:
                    return newSingleChannel.ToBitmap();

                case FilterType.Median:
                    var median = MedianFilter(singleChannel, 5);
                    return median.ToBitmap();

                case FilterType.EdgeMagnitude:
                    var edge = EdgeMagnitude(newSingleChannel, HorizontalKernel(), VerticalKernel());
                    return edge.ToBitmap();

                case FilterType.Threshold:
                    var threshold = ThresholdImage(singleChannel, 128);
                    return threshold.ToBitmap();


                default:
                    throw new NotImplementedException();
            }
        }

        private static int Clamp(int value, int min, int max)
        {
            if (value < min)
                value = min;

            if (value > max)
                value = max;

            return value;
        }

        public static sbyte[,] HorizontalKernel()
        {
            var kernel = new sbyte[3, 3];
            kernel[0, 0] = -1;
            kernel[0, 1] = 0;
            kernel[0, 2] = 1;

            kernel[1, 0] = -2;
            kernel[1, 1] = 0;
            kernel[1, 2] = 2;

            kernel[2, 0] = -1;
            kernel[2, 1] = 0;
            kernel[2, 2] = 1;

            return kernel;
        }

        public static sbyte[,] VerticalKernel()
        {
            var kernel = new sbyte[3, 3];
            kernel[0, 0] = -1;
            kernel[0, 1] = -2;
            kernel[0, 2] = -1;

            kernel[1, 0] = 0;
            kernel[1, 1] = 0;
            kernel[1, 2] = 0;

            kernel[2, 0] = 1;
            kernel[2, 1] = 2;
            kernel[2, 2] = 1;

            return kernel;
        }


        // ====================================================================
        // ============= YOUR FUNCTIONS FOR ASSIGNMENT 1 GO HERE ==============
        // ====================================================================

        /// <summary>
        /// Invert a single channel (grayscale) image
        /// </summary>
        /// <param name="inputImage">single-channel (byte) image</param>
        /// <returns>single-channel (byte) image</returns>
        public byte[,] InvertImage(byte[,] inputImage)
        {
            _progressBar.Setup(inputImage.Length);

            // Process all pixels in the image
            inputImage.PointOperation(pixel =>
            {
                _progressBar.PerformStep();
                return (byte)(Byte.MaxValue - pixel);
            });

            _progressBar.Visible = false;

            return inputImage;
        }

        /// <summary>
        /// Create an image with the full range of intensity values used
        /// </summary>
        /// <param name="inputImage">single-channel (byte) image</param>
        /// <returns>single-channel (byte) image</returns>
        public byte[,] AdjustContrast(byte[,] inputImage)
        {
            _progressBar.Setup(inputImage.Length);

            int aHigh = inputImage.Cast<byte>().Max();
            int aLow = inputImage.Cast<byte>().Min();

            // process all pixels in the image

            inputImage.PointOperation(pixel =>
            {
                _progressBar.PerformStep();
                return ContrastAdjustmentFunction(pixel);
            });

            _progressBar.Visible = false;

            return inputImage;


            byte ContrastAdjustmentFunction(byte pixelIntensity)
            {
                return (byte)(Byte.MinValue + (pixelIntensity - aLow) * Byte.MaxValue / (aHigh - aLow));
            }
        }

        /// <summary>
        /// Create a Gaussian filter of specific square size and with a specified sigma
        /// </summary>
        /// <param name="size">Length and width of the Gaussian filter (only odd sizes)</param>
        /// <param name="sigma">Standard deviation of the Gaussian distribution</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>Gaussian filter</returns>
        public static float[,] CreateGaussianFilter(byte size, float sigma)
        {
            if (size % 2 == 0)
                throw new ArgumentException($"{size} is not an odd size");

            var filter = new float[size, size];
            var k = (size - 1) / 2;


            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    filter[i, j] = Gaussian(i - k, j - k);
                }
            }

            var sum = filter.Cast<float>().Sum();

            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    filter[i, j] = filter[i, j] / sum;
                }
            }

            return filter;


            float Gaussian(int x, int y)
            {
                var sigma2 = sigma * sigma;
                var a1 = Math.Exp(-(x * x + y * y) / (2 * sigma2));
                var a0 = 1 / (2 * Math.PI * sigma2);
                return (float)(a0 * a1);
            }
        }

        /// <summary>
        /// Apply linear filtering of an input image
        /// </summary>
        /// <param name="inputImage">Single-channel (byte) image</param>
        /// <param name="filter">Linear kernel</param>
        /// <returns>Single-channel (byte) image</returns>
        public byte[,] ConvolveImage(byte[,] inputImage, float[,] filter)
        {
            var outputImage = new byte[inputImage.GetLength(0), inputImage.GetLength(1)];

            var width = outputImage.GetLength(0);
            var height = outputImage.GetLength(1);
            
            // For every pixel
            for (var u = 0; u < width; u++)
            {
                for (var v = 0; v < height; v++)
                {
                    var values = new List<byte>();
                    var kernelValues = new List<float>();

                    // For every filter index
                    for (var i = 0; i < filter.GetLength(0); i++)
                    {
                        for (var j = 0; j < filter.GetLength(1); j++)
                        {
                            var du = Clamp(u - i, 0, width - 1);
                            var dv = Clamp(v - j, 0, height - 1);

                            values.Add((byte)(inputImage[du, dv] * filter[i, j]));
                            kernelValues.Add((filter[i, j]));

                        }
                    }
                    var totalKernelValue = kernelValues.Aggregate((x, y) => (x + y));
                    outputImage[u, v] = values.Aggregate((x, y) => (byte)((x + y)/totalKernelValue));
                }
            }

            return outputImage;
        }

        /// <summary>
        /// Apply median filtering on an input image with a kernel of specified size
        /// </summary>
        /// <param name="inputImage">Single-channel (byte) image</param>
        /// <param name="size">Length/width of the median filter kernel</param>
        /// <returns>Single-channel (byte) image</returns>
        public byte[,] MedianFilter(byte[,] inputImage, byte size)
        {
            var outputImage = new byte[inputImage.GetLength(0), inputImage.GetLength(1)];

            var width = outputImage.GetLength(0);
            var height = outputImage.GetLength(1);

            // For every pixel
            for (var u = 0; u < width; u++)
            {
                for (var v = 0; v < height; v++)
                {
                    var values = new List<byte>();

                    // For every median size
                    for (var i = 0; i < size; i++)
                    {
                        for (var j = 0; j < size; j++)
                        {
                            var du = Clamp(u - i, 0, width - 1);
                            var dv = Clamp(v - j, 0, height - 1);

                            values.Add(inputImage[du, dv]);
                        }
                    }

                    outputImage[u, v] = values.OrderBy(x => x).ElementAt(values.Count / 2);
                }
            }

            return outputImage;
        }

        /// <summary>
        /// Calculate the image derivative of an input image and a provided edge kernel
        /// </summary>
        /// <param name="inputImage">Single-channel (byte) image</param>
        /// <param name="horizontalKernel">Horizontal edge kernel</param>
        /// <param name="verticalKernel">Vertical edge kernel</param>
        /// <returns>Single-channel (byte) image</returns>
        public byte[,] EdgeMagnitude(byte[,] inputImage, sbyte[,] horizontalKernel, sbyte[,] verticalKernel)
        {
            // create temporary grayscale image
            var outputImage = new byte[inputImage.GetLength(0), inputImage.GetLength(1)];

            var width = inputImage.GetLength(0);
            var height = inputImage.GetLength(1);

            // For every pixel
            for (var u = 0; u < width; u++)
            {
                for (var v = 0; v < height; v++)
                {
                    var values = new List<sbyte>();

                    // For every Horizontal filter index
                    for (var i = 0; i < horizontalKernel.GetLength(0); i++)
                    {
                        for (var j = 0; j < horizontalKernel.GetLength(1); j++)
                        {
                            var du = Clamp(u - i, 0, width - 1);
                            var dv = Clamp(v - j, 0, height - 1);

                            values.Add((sbyte)(inputImage[du, dv] * horizontalKernel[i, j]));
                        }
                    }

                    var totalHorziontalValue = values.Aggregate((x, y) => { return (sbyte)(x + y); });
                    
                    values.Clear();
                    
                    // For every Vertical filter index
                    for (var i = 0; i < verticalKernel.GetLength(0); i++)
                    {
                        for (var j = 0; j < verticalKernel.GetLength(1); j++)
                        {
                            var du = Clamp(u - i, 0, width - 1);
                            var dv = Clamp(v - j, 0, height - 1);

                            values.Add((sbyte)(inputImage[du, dv] * verticalKernel[i, j]));
                        }
                    }
                    var totalVerticalValue= values.Aggregate((x, y) => { return (sbyte)(x + y); });
                    outputImage[u, v] = (byte)Math.Sqrt(totalHorziontalValue * totalHorziontalValue +
                                                  totalVerticalValue * totalVerticalValue);

                }
            }
            
            return outputImage;
        }

        /// <summary>
        /// Threshold a grayscale image
        /// </summary>
        /// <param name="inputImage">Single-channel (byte) image</param>
        /// <param name="thresholdValue"></param>
        /// <returns>Single-channel (byte) image with on/off values</returns>
        public byte[,] ThresholdImage(byte[,] inputImage, int thresholdValue)
        {
            // create temporary grayscale image
            var outputImage = new byte[inputImage.GetLength(0), inputImage.GetLength(1)];
            
            var width = inputImage.GetLength(0);
            var height = inputImage.GetLength(1);

            // For every pixel
            for (var u = 0; u < width; u++)
            {
                for (var v = 0; v < height; v++)
                {
                    if (inputImage[u, v] < thresholdValue)
                    {
                        outputImage[u, v] = Byte.MinValue;
                    }
                    else
                        outputImage[u, v] = Byte.MaxValue;
                }
            }

            return outputImage;
        }


        // ====================================================================
        // ============= YOUR FUNCTIONS FOR ASSIGNMENT 2 GO HERE ==============
        // ====================================================================


        // ====================================================================
        // ============= YOUR FUNCTIONS FOR ASSIGNMENT 3 GO HERE ==============
        // ====================================================================
    }
}

/// <summary>
/// Inversion of a pixel value
/// </summary>
public class InversionFilter : Filter
{
    public override string Identifier => "Inversion";

    protected override byte ExecuteStep(int u, int v, byte[,] input)
    {
        return (byte)(Byte.MaxValue - input[u, v]);
    }
}
