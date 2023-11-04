using INFOIBV.Filters;
using INFOIBV.Framework;
using INFOIBV.InputForms;
using INFOIBV.SIFT;

namespace INFOIBV
{
    public partial class FormImageProcessing1 : Form
    {
        private Bitmap ReferenceImage { get; set; }
        private Bitmap InputImage { get; set; }
        public FormImageProcessing1()
        {
            InitializeComponent();

            // Set combo boxes
            cbMode.DataSource = Enum.GetValues(typeof(SIFTModes));
            cbDetectionImage.DataSource = Enum.GetValues((typeof(DetectionInputs)));

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

        public void SetReferenceImage(System.Drawing.Image value)
        {
            if (value.Size.Height <= 0 || value.Size.Width <= 0 ||
                value.Size.Height > 512 || value.Size.Width > 512)
            {
                MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                return;
            }

            inputReferenceImageBox.Image?.Dispose();
            inputReferenceImageBox.Image = value;
        }

        public void SetOutputImage(System.Drawing.Image value)
        {
            if (value.Size.Height <= 0 || value.Size.Width <= 0 ||
                value.Size.Height > 512 || value.Size.Width > 512)
            {
                MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                return;
            }

            outputImageBox.Image?.Dispose();
            outputImageBox.Image = value;
        }

        public void SetOutputImageP1(System.Drawing.Image value)
        {
            if (value.Size.Height <= 0 || value.Size.Width <= 0 ||
                value.Size.Height > 512 || value.Size.Width > 512)
            {
                MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                return;
            }

            outputImageBoxP1.Image?.Dispose();
            outputImageBoxP1.Image = value;
        }
        public void SetOutputImageP2(System.Drawing.Image value)
        {
            if (value.Size.Height <= 0 || value.Size.Width <= 0 ||
                value.Size.Height > 512 || value.Size.Width > 512)
            {
                MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                return;
            }

            outputImageBoxP2.Image?.Dispose();
            outputImageBoxP2.Image = value;
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
            InputImage = new Bitmap(file);
        }

        private void LoadReferenceButton_Click(object sender, EventArgs e)
        {
            if (openImageDialog.ShowDialog() != DialogResult.OK)
                return;

            var file = openImageDialog.FileName;
            ReferenceImageFileName.Text = file;

            SetReferenceImage(new Bitmap(file));
            ReferenceImage = new Bitmap(file);
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

            applyButton.Enabled = false;

            var singleChannel = inputImageBox.Image.ToSingleChannel();
            
            var histogram = new Histogram(singleChannel);

            SetOutputImage(singleChannel, mode);

            filterLabel.Text =
                $"{histogram.UniqueCount} Unique values | {histogram.NonBackgroundCount} Number of non background values";

            applyButton.Enabled = true;
        }

        private void SetOutputImage(byte[,] input, SIFTModes mode)
        {
            switch (mode)
            {
                case SIFTModes.SiftDog:
                    SiftDoG(input, true);
                    break;
                case SIFTModes.SiftFeatures:
                    outputImageBox.Visible = true;
                    outputImageBox.Image = KeyPointSelection.DrawFeatures(input);
                    break;
                case SIFTModes.SiftFeaturesBoth:
                    outputImageBox.Visible = false;
                    outputImageBoxP1.Image = KeyPointSelection.DrawFeatures(inputImageBox.Image.ToSingleChannel());
                    outputImageBoxP2.Image = KeyPointSelection.DrawFeatures(inputReferenceImageBox.Image.ToSingleChannel());
                    break;
                case SIFTModes.SiftTopKeyPointMatches:
                    if (inputReferenceImageBox.Image == null)
                        break;
                    var (outputReference, output) = KeyPointSelection.DrawMatchFeatures(
                        ReferenceImage.ToSingleChannel(), InputImage.ToSingleChannel(),
                        inputReferenceImageBox.Image.ToSingleChannel(), inputImageBox.Image.ToSingleChannel());
                    outputImageBox.Visible = false;
                    SetOutputImageP1(output);
                    SetOutputImageP2(outputReference);
                    break;
                case SIFTModes.SiftDrawBorder:
                    if (inputReferenceImageBox.Image == null)
                        break;
                    outputImageBox.Visible = true;
                    SetOutputImage(KeyPointSelection.DrawBoundingBox(inputReferenceImageBox.Image.ToSingleChannel(), inputImageBox.Image.ToSingleChannel(), InputImage.ToSingleChannel()));
                    break;
            }
        }

        #region Tests
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
            if (outputImageBoxP1.Image == null)
                return;

            if (saveImageDialog.ShowDialog() == DialogResult.OK)
                outputImageBoxP1.Image.Save(saveImageDialog.FileName);
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
                    InputImage = new Bitmap("Images/TestingImages/UnoReference.jpeg");
                    break;
                case DetectionInputs.Image1:
                    SetInputImage(new Bitmap("Images/TestingImages/Test1.jpeg"));
                    InputImage = new Bitmap("Images/TestingImages/Test1.jpeg");
                    break;
                case DetectionInputs.Image2:
                    SetInputImage(new Bitmap("Images/TestingImages/Test2.jpg"));
                    InputImage = new Bitmap("Images/TestingImages/Test2.jpg");
                    break;
                case DetectionInputs.Image3:
                    SetInputImage(new Bitmap("Images/TestingImages/Test3.jpg"));
                    InputImage = new Bitmap("Images/TestingImages/Test3.jpg");
                    break;
                case DetectionInputs.Image4:
                    SetInputImage(new Bitmap("Images/TestingImages/Test4.jpg"));
                    InputImage = new Bitmap("Images/TestingImages/Test4.jpg");
                    break;
                case DetectionInputs.Image5:
                    SetInputImage(new Bitmap("Images/TestingImages/Test5.jpg"));
                    InputImage = new Bitmap("Images/TestingImages/Test5.jpg");
                    break;
                case DetectionInputs.Image6:
                    SetInputImage(new Bitmap("Images/TestingImages/Test6.jpg"));
                    InputImage = new Bitmap("Images/TestingImages/Test6.jpg");
                    break;
                case DetectionInputs.Image7:
                    SetInputImage(new Bitmap("Images/TestingImages/Test7.jpg"));
                    InputImage = new Bitmap("Images/TestingImages/Test7.jpg");
                    break;
                case DetectionInputs.Image8:
                    SetInputImage(new Bitmap("Images/TestingImages/Test8.jpg"));
                    InputImage = new Bitmap("Images/TestingImages/Test8.jpg");
                    break;
                case DetectionInputs.Image9:
                    SetInputImage(new Bitmap("Images/TestingImages/Test9.jpg"));
                    InputImage = new Bitmap("Images/TestingImages/Test9.jpg");
                    break;
                case DetectionInputs.Image10:
                    SetInputImage(new Bitmap("Images/TestingImages/Test10.jpg"));
                    InputImage = new Bitmap("Images/TestingImages/Test10.jpg");
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

        private void preprocessRefButton_Click(object sender, EventArgs e)
        {
            if (inputReferenceImageBox.Image == null)
                return;

            var preProcessForm = new FormPreprocessing(inputReferenceImageBox.Image);
            preProcessForm.ShowDialog();

            if (preProcessForm.processedImageBox.Image == null)
                return;

            SetReferenceImage(preProcessForm.processedImageBox.Image);

        }

        private void loadUnoButton_Click(object sender, EventArgs e)
        {
            SetReferenceImage(new Bitmap("Images/TestingImages/UnoReference.jpeg"));
            ReferenceImage = new Bitmap("Images/TestingImages/UnoReference.jpeg");
        }
    }
}