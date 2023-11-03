using INFOIBV.Filters;
using INFOIBV.Framework;
using INFOIBV.InputForms;
using INFOIBV.SIFT;

namespace INFOIBV
{
    public partial class FormImageProcessing1 : Form
    {
        private Bitmap ReferenceImage { get; init; }
        public FormImageProcessing1()
        {
            InitializeComponent();

            // Set combo boxes
            cbMode.DataSource = Enum.GetValues(typeof(SIFTModes));
            cbDetectionImage.DataSource = Enum.GetValues((typeof(DetectionInputs)));
            ReferenceImage = new Bitmap("Images/TestingImages/UnoReference.jpeg");
        }

        /// <summary>
        /// Checks, sets and displays the input image
        /// </summary>
        /// <param name="value">Bitmap to display</param>
        public void SetInputImage(System.Drawing.Image value)
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
        private async void ApplyButton_Click(object sender, EventArgs e)
        {
            if (inputImageBox.Image == null)
                return;

            if (!Enum.TryParse<SIFTModes>(cbMode.SelectedValue?.ToString(), out var mode))
                return;

            outputImageBox.Image?.Dispose();

            applyButton.Enabled = false;

            var singleChannel = inputImageBox.Image.ToSingleChannel();

            var histogram = new Histogram(singleChannel);

            outputImageBox.Image = GetShowOptions(singleChannel, histogram, mode);

            filterLabel.Text =
                $"{histogram.UniqueCount} Unique values | {histogram.NonBackgroundCount} Number of non background values";

            applyButton.Enabled = true;
        }

        private Bitmap GetShowOptions(byte[,] input, Histogram histogram, SIFTModes mode)
        {
            switch (mode)
            {
                case SIFTModes.SiftDog:
                    SiftDoG(input, true);
                    return input.ToBitmap();
                case SIFTModes.SiftFeatures:
                    return KeyPointSelection.DrawFeatures(input);
                case SIFTModes.SiftFeaturesBoth:
                    SetInputImage(KeyPointSelection.DrawFeatures(ReferenceImage.ToSingleChannel()));
                    return KeyPointSelection.DrawFeatures(input);
                case SIFTModes.SiftTopKeyPointMatches:
                    var (outputReference, output) = KeyPointSelection.DrawMatchFeatures(ReferenceImage.ToSingleChannel(), input);
                    SetInputImage(outputReference);
                    return output;
                case SIFTModes.SiftDrawBorder:
                    SetInputImage(ReferenceImage);
                    return KeyPointSelection.DrawBoundingBox(ReferenceImage.ToSingleChannel(), input);
                case SIFTModes.SIFT:
                    KeyPointSelection.GetSiftDominantOrientation(new SIFT.Image(input));
                    return Test(input);
                default:
                    return input.ToBitmap();
            }
        }

        #region Tests

        private Bitmap Test(byte[,] input)
        {
            SetInputImage(ReferenceImage);
            return KeyPointSelection.DrawBoundingBox(ReferenceImage.ToSingleChannel(), input);
        }

        private static void SiftDoG(byte[,] input, bool visualize)
        {
            if (!visualize)
                return;

            var image = new SIFT.Image(input);
            var output = KeyPointSelection.BuildSiftScaleSpace(image, visualize);

            const int octaveCount = 4;
            for (var i = 0; i < octaveCount; i++)
            {
                var doGForm = new DoGTest();
                doGForm.G1.Image = output.GaussianOctaves[i][1].Bytes.ToBitmap();
                doGForm.G2.Image = output.GaussianOctaves[i][2].Bytes.ToBitmap();
                doGForm.G3.Image = output.GaussianOctaves[i][3].Bytes.ToBitmap();
                doGForm.G4.Image = output.GaussianOctaves[i][4].Bytes.ToBitmap();

                doGForm.D1.Image = output.DifferenceOfGaussiansOctavesByte[i][1].Bytes.ToBitmap();
                doGForm.D2.Image = output.DifferenceOfGaussiansOctavesByte[i][2].Bytes.ToBitmap();
                doGForm.D3.Image = output.DifferenceOfGaussiansOctavesByte[i][3].Bytes.ToBitmap();
                doGForm.ShowDialog();
            }
        }

        #endregion

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

        private void cbDetectionImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbDetectionImage.SelectedValue)
            {
                case DetectionInputs.None:
                    inputImageBox.Image = null;
                    inputImageBox.Image?.Dispose();
                    break;
                case DetectionInputs.ReferenceImage:
                    SetInputImage(new Bitmap("Images/TestingImages/UnoReference.jpeg"));
                    break;
                case DetectionInputs.Image1:
                    SetInputImage(new Bitmap("Images/TestingImages/Test1.jpeg"));
                    break;
                case DetectionInputs.Image2:
                    SetInputImage(new Bitmap("Images/TestingImages/Test2.jpg"));
                    break;
                case DetectionInputs.Image3:
                    SetInputImage(new Bitmap("Images/TestingImages/Test3.jpg"));
                    break;
                case DetectionInputs.Image4:
                    SetInputImage(new Bitmap("Images/TestingImages/Test4.jpg"));
                    break;
                case DetectionInputs.Image5:
                    SetInputImage(new Bitmap("Images/TestingImages/Test5.jpg"));
                    break;
                case DetectionInputs.Image6:
                    SetInputImage(new Bitmap("Images/TestingImages/Test6.jpg"));
                    break;
                case DetectionInputs.Image7:
                    SetInputImage(new Bitmap("Images/TestingImages/Test7.jpg"));
                    break;
                case DetectionInputs.Image8:
                    SetInputImage(new Bitmap("Images/TestingImages/Test8.jpg"));
                    break;
                case DetectionInputs.Image9:
                    SetInputImage(new Bitmap("Images/TestingImages/Test9.jpg"));
                    break;
                case DetectionInputs.Image10:
                    SetInputImage(new Bitmap("Images/TestingImages/Test10.jpg"));
                    break;
            }
        }

        private void Preprocess_Click(object sender, EventArgs e)
        {
            if (inputImageBox.Image == null)
                return;

            var preProcessForm = new FormPreprocessing(inputImageBox.Image);
            preProcessForm.ShowDialog();

            if (preProcessForm.processedImageBox.Image == null)
                return;

            SetInputImage(preProcessForm.processedImageBox.Image);
 
        }
    }
}