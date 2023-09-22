using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using INFOIBV.Filters;
using INFOIBV.Framework;

namespace INFOIBV
{
    // ReSharper disable once InconsistentNaming
    public partial class INFOIBV : Form
    {

        public INFOIBV()
        {
            InitializeComponent();
            cbFilter.DataSource = Enum.GetValues(typeof(FilterType));
        }

        /// <summary>
        /// Checks, sets and displays the input image
        /// </summary>
        /// <param name="value">Bitmap to display</param>
        private void SetInputImage(Image value)
        {
            if (value.Size.Height <= 0 || value.Size.Width <= 0 ||
                value.Size.Height > 512 || value.Size.Width > 512)
            {
                MessageBox.Show(@"Error in image dimensions (have to be > 0 and <= 512)");
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
                MessageBox.Show(@"Please specify a filter");
                return;
            }

            outputImageBox.Image?.Dispose();
            outputImageBox.Image = FilterMethods.ApplyFilter(filter, (Bitmap)inputImageBox.Image);
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

        private async void PipeLineButton1_Click(object sender, EventArgs e)
        {
            var pipeline = new PipeLine()
                .AddContrastAdjustment()
                .AddGaussian(5, 1)
                .AddEdgeMagnitudeFilter()
                .AddThresholdFilter(70);

            // Execute pipeline
            await ProcessPipeline(pipeline);
        }

        private async void Pipeline2Button_Click(object sender, EventArgs e)
        {
            var pipeline = new PipeLine()
                .AddContrastAdjustment()
                .AddMedianFilter(5)
                .AddEdgeMagnitudeFilter()
                .AddThresholdFilter(85);

            await ProcessPipeline(pipeline);
        }

        private async Task ProcessPipeline(PipeLine pipeLine)
        {
            if (inputImageBox.Image == null)
                return;

            var progress = new Progress<(string, int)>(x =>
            {
                filterLabel.Text = x.Item1;
                progressBar.Value = x.Item2;
            });

            progressBar.Show();
            outputImageBox.Image = await pipeLine.Build((Bitmap)inputImageBox.Image, progress);
            progressBar.Hide();
        }

        private void LenaButton_Click(object sender, EventArgs e)
        {
            SetInputImage(new Bitmap("Images/lena_color.jpg"));
        }

        private void GridButton_Click(object sender, EventArgs e)
        {
            SetInputImage(new Bitmap("Images/grid.jpg"));
        }

        private void CubeHousesButton_Click(object sender, EventArgs e)
        {
            SetInputImage(new Bitmap("Images/cube_houses.jpg"));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}