using System;
using System.Drawing;
using System.Windows.Forms;
using INFOIBV.Framework;

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
        /// Process when user clicks on the "
        /// " button
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

            var filters = new FilterMethods(progressBar);
            outputImageBox.Image = filters.ApplyFilter(filter, (Bitmap)inputImageBox.Image);
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

        private void PipeLineButton1_Click(object sender, EventArgs e)
        {
            if (inputImageBox.Image == null)
                return;

            var filters = new FilterMethods(progressBar);
            var pipeline = new PipeLine()
                .AddFilter(filters.AdjustContrast)
                .AddFilter(image => filters.ConvolveImage(image, FilterMethods.CreateGaussianFilter(5, 1)))
                .AddFilter(image => filters.EdgeMagnitude(image, FilterMethods.HorizontalKernel(), FilterMethods.VerticalKernel()))
                .AddFilter(image => filters.ThresholdImage(image, 70));

            // Execute pipeline
            outputImageBox.Image?.Dispose();
            outputImageBox.Image = pipeline.Build((Bitmap)inputImageBox.Image);
        }

        private void pipeline2Button_Click(object sender, EventArgs e)
        {
            if (inputImageBox.Image == null)
                return;

            var filters = new FilterMethods(progressBar);
            var pipeline = new PipeLine()
                .AddFilter(filters.AdjustContrast)
                .AddFilter(image => filters.MedianFilter(image,5))
                .AddFilter(image => filters.EdgeMagnitude(image, FilterMethods.HorizontalKernel(), FilterMethods.VerticalKernel()))
                .AddFilter(image => filters.ThresholdImage(image, 85));

            // Execute pipeline
            outputImageBox.Image?.Dispose();
            outputImageBox.Image = pipeline.Build((Bitmap)inputImageBox.Image);
        }
    }
}