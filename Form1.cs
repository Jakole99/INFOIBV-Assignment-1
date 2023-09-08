using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace INFOIBV
{
    public partial class INFOIBV : Form
    {
        private Bitmap _inputImage;

        public INFOIBV()
        {
            InitializeComponent();
            cbFilter.DataSource = Enum.GetValues(typeof(Filter));

            SetInputImage(new Bitmap("images/lena_color.jpg"));
        }

        /// <summary>
        /// Type of filters to perform on an image
        /// </summary>
        public enum Filter
        {
            None,
            GrayScale,
            Invert,
            AdjustContrast,
            Gaussian,
            Convolve,
            Median,
            EdgeMagnitude,
            Threshold
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

            _inputImage?.Dispose();
            _inputImage = value;

            inputImageBox.Image = _inputImage;
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
            if (_inputImage == null)
                return;

            if (!Enum.TryParse<Filter>(cbFilter.SelectedValue.ToString(), out var filter))
                return;

            if (filter == Filter.None)
            {
                MessageBox.Show("Please specify a filter");
                return;
            }

            outputImageBox.Image?.Dispose();
            outputImageBox.Image = ApplyFilter(filter, _inputImage);
        }

        /// <summary>
        /// Applies a specified filter
        /// </summary>
        /// <param name="filter">Type of filter</param>
        /// <param name="image">Three-channel image</param>
        /// <returns>One-channel image</returns>
        private Bitmap ApplyFilter(Filter filter, Bitmap image)
        {
            switch (filter)
            {
                case Filter.GrayScale:
                    return new PipeLine()
                        .Build(image);

                case Filter.Invert:
                    return new PipeLine()
                        .AddFilter(InvertImage)
                        .Build(image);

                case Filter.AdjustContrast:
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
            // setup progress bar
            progressBar.Visible = true;
            progressBar.Minimum = 1;
            progressBar.Maximum = _inputImage.Size.Width * _inputImage.Size.Height;
            progressBar.Value = 1;
            progressBar.Step = 1;

            // process all pixels in the image
            for (int x = 0; x < _inputImage.Size.Width; x++)
                for (int y = 0; y < _inputImage.Size.Height; y++)
                {
                    inputImage[x, y] = (byte)(255 - inputImage[x, y]);
                    progressBar.PerformStep();
                }

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
            // setup progress bar
            progressBar.Visible = true;
            progressBar.Minimum = 1;
            progressBar.Maximum = _inputImage.Size.Width * _inputImage.Size.Height;
            progressBar.Value = 1;
            progressBar.Step = 1;

            int aHigh = inputImage.Cast<byte>().Max();
            int aLow = inputImage.Cast<byte>().Min();



            // process all pixels in the image
            for (int x = 0; x < _inputImage.Size.Width; x++)
                for (int y = 0; y < _inputImage.Size.Height; y++)
                { 
                    inputImage[x, y] = ContrastAdjustmentFunction(aHigh, aLow, inputImage[x, y]);
                    progressBar.PerformStep();
                }

            progressBar.Visible = false;

            return inputImage;
        }

        private byte ContrastAdjustmentFunction(int aHigh, int aLow, byte pixelIntensity)
        {
            return (byte)(0 + (pixelIntensity - aLow) * (255 - 0) / (aHigh - aLow));
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

            // TODO: add your functionality and checks

            return filter;
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