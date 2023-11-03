using INFOIBV.Filters;
using INFOIBV.Framework;

namespace INFOIBV.SIFT
{
    public partial class FormPreprocessing : Form
    {
        private System.Drawing.Image _initialImage;

        public FormPreprocessing(System.Drawing.Image value)
        {
            InitializeComponent();
            _initialImage = value;
            processedImageBox.Image = value;
            dilationStructure.DataSource = Enum.GetValues(typeof(StructureType));
        }

        /// <summary>
        /// Checks, sets and displays the processed image
        /// </summary>
        /// <param name="value">Bitmap to display</param>
        public void SetProcessedImage(System.Drawing.Image value)
        {
            if (value.Size.Height <= 0 || value.Size.Width <= 0 ||
                value.Size.Height > 512 || value.Size.Width > 512)
            {
                MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                return;
            }

            processedImageBox.Image?.Dispose();
            processedImageBox.Image = value;
        }

        private void invertButton_Click(object sender, EventArgs e)
        {
            DisableButtons();
            var filter = new FilterCollection().AddInversionFilter();
            var processedImage = filter.Process(processedImageBox.Image);

            SetProcessedImage(processedImage.ToBitmap());
            EnableButtons();
        }

        private void contrastButton_Click(object sender, EventArgs e)
        {
            DisableButtons();
            var filter = new FilterCollection().AddContrastAdjustment();
            var processedImage = filter.Process(processedImageBox.Image);

            SetProcessedImage(processedImage.ToBitmap());
            EnableButtons();
        }

        private void edgeButton_Click(object sender, EventArgs e)
        {
            DisableButtons();
            var filter = new FilterCollection().AddEdgeMagnitudeFilter();
            var processedImage = filter.Process(processedImageBox.Image);

            SetProcessedImage(processedImage.ToBitmap());
            EnableButtons();
        }

        private void threshButton_Click(object sender, EventArgs e)
        {
            DisableButtons();
            var filter = new FilterCollection().AddThresholdFilter((int)thresholdUpDown.Value);
            var processedImage = filter.Process(processedImageBox.Image);

            SetProcessedImage(processedImage.ToBitmap());
            EnableButtons();
        }

        private void histogramButton_Click(object sender, EventArgs e)
        {
            DisableButtons();
            var filter = new FilterCollection().AddHistogramEqualization();
            var processedImage = filter.Process(processedImageBox.Image);

            SetProcessedImage(processedImage.ToBitmap());
            EnableButtons();
        }

        private void medianButton_Click(object sender, EventArgs e)
        {
            DisableButtons();
            var filter = new FilterCollection().AddMedianFilter((int)medianUpDown.Value);
            var processedImage = filter.Process(processedImageBox.Image);

            SetProcessedImage(processedImage.ToBitmap());
            EnableButtons();
        }


        private void gaussianButton_Click(object sender, EventArgs e)
        {
            DisableButtons();
            var filter = new FilterCollection().AddGaussian((int)gaussianSize.Value, (float)gaussianUpDown.Value);
            var processedImage = filter.Process(processedImageBox.Image);

            SetProcessedImage(processedImage.ToBitmap());
            EnableButtons();
        }

        private void dilationButton_Click(object sender, EventArgs e)
        {
            DisableButtons();
            var structureElement = dilationStructure.SelectedItem as StructureType? ?? StructureType.Plus;
            var filter = new FilterCollection().AddDilationFilter(structureElement, (int)dilationUpDown.Value);
            var processedImage = filter.Process(processedImageBox.Image);

            SetProcessedImage(processedImage.ToBitmap());
            EnableButtons();
        }


        private void DisableButtons()
        {

        }

        private void EnableButtons()
        {

        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            SetProcessedImage(_initialImage);
        }

        private void gaussianSize_ValueChanged(object sender, EventArgs e)
        {
            if (gaussianSize.Value % 2 == 0) gaussianSize.Value += 1;
        }

        private void dilationUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (gaussianSize.Value % 2 == 0) gaussianSize.Value += 1;
        }

        private void medianUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (gaussianSize.Value % 2 == 0) gaussianSize.Value += 1;
        }





        //private FilterCollection GetFilters()
        //{
        //    var structureElement = cbStructureElement.SelectedItem as StructureType? ?? StructureType.Plus;

        //    var isBinary = checkBinary.Checked;
        //    var size = (int)numericSize.Value;

        //    var availableProcessors = new FilterCollection()
        //        .AddProcess(new FilterCollection("Grayscale"))
        //        .AddInversionFilter()
        //        .AddGaussian(size, 1)
        //        .AddContrastAdjustment()
        //        .AddEdgeMagnitudeFilter()
        //        .AddDilationFilter(structureElement, size, isBinary)
        //        .AddErosionFilter(structureElement, size, isBinary)
        //        .AddClosingFilter(structureElement, size, isBinary)
        //        .AddOpeningFilter(structureElement, size, isBinary)
        //        .AddThresholdFilter(80)
        //        .AddHistogramEqualization()
        //        .AddMedianFilter(size);

        //    var imageA = new FilterCollection("Image A")
        //        .AddContrastAdjustment();
        //    availableProcessors.AddProcess(imageA);

        //    var imageEdgeThresh = FilterCollection.From(imageA, "Edge With threshold")
        //        .AddGaussian(5, 3)
        //        .AddEdgeMagnitudeFilter()
        //        .AddThresholdFilter(100);
        //    availableProcessors.AddProcess(imageEdgeThresh);

        //    var imageEdge = FilterCollection.From(imageA, "Edge Without Threshold")
        //        .AddGaussian(5, 3)
        //        .AddEdgeMagnitudeFilter();
        //    availableProcessors.AddProcess(imageEdge);


        //    return availableProcessors;
        //}


        /// <summary>
        /// Process when user clicks on the "Apply" button
        /// </summary>
        //private async void ApplyButton_Click(object sender, EventArgs e)
        //{
        //    if (cbFilter.SelectedValue is not IImageProcessor processor)
        //        return;

        //    if (inputImageBox.Image == null)
        //        return;

        //    if (!Enum.TryParse<ModeType>(cbMode.SelectedValue?.ToString(), out var mode))
        //        return;

        //    outputImageBox.Image?.Dispose();

        //    applyButton.Enabled = false;

        //    var singleChannel = await Task.Run(() => processor.Process(inputImageBox.Image));

        //    var histogram = new Histogram(singleChannel);

        //    outputImageBox.Image = GetShowOptions(singleChannel, histogram, mode);

        //    filterLabel.Text =
        //        $"{histogram.UniqueCount} Unique values | {histogram.NonBackgroundCount} Number of non background values";

        //    applyButton.Enabled = true;
        //}
    }
}
