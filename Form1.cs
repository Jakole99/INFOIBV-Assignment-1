﻿using System;
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

            // Set combo boxes
            cbFilter.DataSource = Enum.GetValues(typeof(FilterType));
            cbMode.DataSource = Enum.GetValues(typeof(ModeType));
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
        private async void ApplyButton_Click(object sender, EventArgs e)
        {
            if (!Enum.TryParse<FilterType>(cbFilter.SelectedValue.ToString(), out var filter))
                return;

            var pipeline = new PipeLine();

            switch (filter)
            {
                case FilterType.GrayScale: // Pipeline already converts to grayscale
                    break;
                case FilterType.Invert:
                    pipeline.AddInversionFilter();
                    break;
                case FilterType.AdjustContrast:
                    pipeline.AddContrastAdjustment();
                    break;
                case FilterType.Gaussian:
                    pipeline.AddGaussian(5, 1);
                    break;
                case FilterType.Median:
                    pipeline.AddMedianFilter(5);
                    break;
                case FilterType.EdgeMagnitude:
                    pipeline.AddEdgeMagnitudeFilter();
                    break;
                case FilterType.Threshold:
                    pipeline.AddThresholdFilter(128);
                    break;
                case FilterType.HistogramEqualization:
                    pipeline.AddHistogramEqualization();
                    break;

                case FilterType.None:
                default:
                    return;
            }

            await ProcessPipeline(pipeline);
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
                .AddThresholdFilter(80);

            // Execute pipeline
            await ProcessPipeline(pipeline);
        }

        private async void Pipeline2Button_Click(object sender, EventArgs e)
        {
            //var pipeline = new PipeLine()
              //  .AddContrastAdjustment()
               // .AddMedianFilter(5)
               // .AddEdgeMagnitudeFilter()
               // .AddThresholdFilter(100);

               var pipeline = new PipeLine()
                   .AddContrastAdjustment()
                   .AddGaussian(3, 1)
                   .AddGaussian(5, (int)(1*Math.Sqrt(2)))
                   .AddGaussian(7, (int)(1 * Math.Sqrt(3)))
                   .AddGaussian(9, (int)(1 * Math.Sqrt(4)))
                   .AddGaussian(11, (int)(1 * Math.Sqrt(5)))
                   .AddEdgeMagnitudeFilter()
                   .AddThresholdFilter(80);

            await ProcessPipeline(pipeline);
        }

        private async Task ProcessPipeline(PipeLine pipeLine)
        {
            if (inputImageBox.Image == null)
                return;

            if (!Enum.TryParse<ModeType>(cbMode.SelectedValue.ToString(), out var mode))
                return;

            var progress = new Progress<(string, int)>(x =>
            {
                filterLabel.Text = x.Item1;
                progressBar.Value = x.Item2;
            });

            pipeline1Button.Enabled = pipeline2Button.Enabled = applyButton.Enabled = false;
            progressBar.Visible = filterLabel.Visible = true;
            switch (mode)
            {
                case ModeType.Normal:
                    outputImageBox.Image =
                        await pipeLine.Build((Bitmap)inputImageBox.Image, progress);
                    break;
                case ModeType.Histogram:
                    outputImageBox.Image =
                        await pipeLine.DisplayHistogram((Bitmap)inputImageBox.Image, progress);
                    break;
                case ModeType.CumulativeHistogram:
                    outputImageBox.Image =
                        await pipeLine.DisplayCumulativeHistogram((Bitmap)inputImageBox.Image, progress);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            progressBar.Visible = filterLabel.Visible = false;
            pipeline1Button.Enabled = pipeline2Button.Enabled = applyButton.Enabled = true;
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
    }
}