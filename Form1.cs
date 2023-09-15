using System;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace INFOIBV
{
    public partial class INFOIBV : Form
    {
        public INFOIBV()
        {
            InitializeComponent();
            cbFilter.DataSource = Enum.GetValues(typeof(FilterType));

            SetInputImage(new Bitmap("images/lena_color.jpg"));
        }

        /// <summary>
        /// Checks, sets and displays the input image
        /// </summary>
        /// <param name="value">Bitmap to display</param>
        private void SetInputImage(Bitmap value)
        {
            if (value.Size.Height <= 0 || value.Size.Width <= 0 ||
                value.Size.Height > 512 || value.Size.Width > 512)
            {
                MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                return;
            }
            inputImageBox.Image?.Dispose();
            inputImageBox.Image = value;
        }

        /// <summary>
        /// Process when the user clicks on the "Load" button
        /// </summary>
        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            if (openImageDialog.ShowDialog() != DialogResult.OK)
                return;

            var file = openImageDialog.FileName;
            imageFileName.Text = file;

            SetInputImage(new Bitmap(file));
        }

        /// <summary>
        /// Process when user clicks on the "Apply" button
        /// </summary>
        private void ApplyButton_Click(object sender, EventArgs e)
        {
            if (inputImageBox.Image == null)
                return;

            if (!Enum.TryParse<FilterType>(cbFilter.SelectedValue.ToString(), out var filter))
                return;

            if (filter == FilterType.None)
            {
                MessageBox.Show("Please specify a filter");
                return;
            }

            outputImageBox.Image?.Dispose();
            outputImageBox.Image = ApplyFilter(filter, (Bitmap)inputImageBox.Image);
        }

        /// <summary>
        /// Applies a specified filter
        /// </summary>
        /// <param name="filter">Type of filter</param>
        /// <param name="image">Three-channel image</param>
        /// <returns>One-channel image</returns>
        private Bitmap ApplyFilter(FilterType filter, Bitmap image)
        {
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
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Process when the user clicks on the "Save" button
        /// </summary>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (outputImageBox.Image == null)
                return;

            if (saveImageDialog.ShowDialog() == DialogResult.OK)
                outputImageBox.Image.Save(saveImageDialog.FileName);
        }

        // ====================================================================
        // ============= YOUR FUNCTIONS FOR ASSIGNMENT 1 GO HERE ==============
        // ====================================================================

        /// <summary>
        /// Invert a single channel (grayscale) image
        /// </summary>
        /// <param name="inputImage">single-channel (byte) image</param>
        /// <returns>single-channel (byte) image</returns>
        private byte[,] InvertImage(byte[,] inputImage)
        {
            progressBar.Setup(inputImage.Length);

            // Process all pixels in the image
            inputImage.PointOperation( bijt =>
            {
                progressBar.PerformStep();
                return (byte)(255 - bijt);
            });

            progressBar.Visible = false;

            return inputImage;
        }

        /// <summary>
        /// Create an image with the full range of intensity values used
        /// </summary>
        /// <param name="inputImage">single-channel (byte) image</param>
        /// <returns>single-channel (byte) image</returns>
        private byte[,] AdjustContrast(byte[,] inputImage)
        {
            progressBar.Setup(inputImage.Length);

            int aHigh = inputImage.Cast<byte>().Max();
            int aLow = inputImage.Cast<byte>().Min();

            // process all pixels in the image
            inputImage.PointOperation(bijt =>
            {
                progressBar.PerformStep();
                return ContrastAdjustmentFunction(bijt);
            });

            progressBar.Visible = false;

            return inputImage;


            byte ContrastAdjustmentFunction(byte pixelIntensity)
            {
                return (byte)(0 + (pixelIntensity - aLow) * (255 - 0) / (aHigh - aLow));
            }
        }


        /// <summary>
        /// Create a Gaussian filter of specific square size and with a specified sigma
        /// </summary>
        /// <param name="size">Length and width of the Gaussian filter (only odd sizes)</param>
        /// <param name="sigma">Standard deviation of the Gaussian distribution</param>
        /// <returns>Gaussian filter</returns>
        private float[,] CreateGaussianFilter(byte size, float sigma)
        {

            // create temporary grayscale image
            var filter = new float[size, size];

            var k = (size - 1) / 2;

            // TODO: add your functionality and checks
            if (size%2 == 0)
            {
                throw new ArgumentException("only odd sizes");
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    filter[i,j] = GaussianFunction(i - k, j - k);
                }
            }
            
            return filter;


            float GaussianFunction(int x, int y)
            {
                var x2 = Math.Pow(x, 2);
                var y2 = Math.Pow(y, 2);
                var sigma2 = Math.Pow(sigma, 2);
                return (float)((1 / (2 * Math.PI * sigma2)) * Math.Pow(Math.E,-(x2+y2)/2*sigma2));
            }
        }

        /// <summary>
        /// Apply linear filtering of an input image
        /// </summary>
        /// <param name="inputImage">Single-channel (byte) image</param>
        /// <param name="filter">Linear kernel</param>
        /// <returns>Single-channel (byte) image</returns>
        private byte[,] ConvolveImage(byte[,] inputImage, float[,] filter)
        {
            // create temporary grayscale image
            var tempImage = new byte[inputImage.GetLength(0), inputImage.GetLength(1)];

            // TODO: add your functionality and checks, think about border handling and type conversion

            return tempImage;
        }

        /// <summary>
        /// Apply median filtering on an input image with a kernel of specified size
        /// </summary>
        /// <param name="inputImage">Single-channel (byte) image</param>
        /// <param name="size">Length/width of the median filter kernel</param>
        /// <returns>Single-channel (byte) image</returns>
        private byte[,] MedianFilter(byte[,] inputImage, byte size)
        {
            // create temporary grayscale image
            var  tempImage = new byte[inputImage.GetLength(0), inputImage.GetLength(1)];

            // TODO: add your functionality and checks, think about border handling and type conversion

            return tempImage;
        }

        /// <summary>
        /// Calculate the image derivative of an input image and a provided edge kernel
        /// </summary>
        /// <param name="inputImage">Single-channel (byte) image</param>
        /// <param name="horizontalKernel">Horizontal edge kernel</param>
        /// <param name="verticalKernel">Vertical edge kernel</param>
        /// <returns>Single-channel (byte) image</returns>
        private byte[,] EdgeMagnitude(byte[,] inputImage, sbyte[,] horizontalKernel, sbyte[,] verticalKernel)
        {
            // create temporary grayscale image
            var tempImage = new byte[inputImage.GetLength(0), inputImage.GetLength(1)];

            // TODO: add your functionality and checks, think about border handling and type conversion (negative values!)

            return tempImage;
        }

        /// <summary>
        /// Threshold a grayscale image
        /// </summary>
        /// <param name="inputImage">Single-channel (byte) image</param>
        /// <returns>Single-channel (byte) image with on/off values</returns>
        private byte[,] ThresholdImage(byte[,] inputImage)
        {
            // create temporary grayscale image
            var tempImage = new byte[inputImage.GetLength(0), inputImage.GetLength(1)];

            // TODO: add your functionality and checks, think about how to represent the binary values

            return tempImage;
        }


        // ====================================================================
        // ============= YOUR FUNCTIONS FOR ASSIGNMENT 2 GO HERE ==============
        // ====================================================================


        // ====================================================================
        // ============= YOUR FUNCTIONS FOR ASSIGNMENT 3 GO HERE ==============
        // ====================================================================

    }
}