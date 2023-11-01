using INFOIBV.Filters;
using INFOIBV.Framework;
using INFOIBV.InputForms;
using INFOIBV.SIFT;

namespace INFOIBV;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        // Set combo boxes
        cbFilter.DataSource = GetFilters().ImageProcessors;
        cbMode.DataSource = Enum.GetValues(typeof(ModeType));
        cbStructureElement.DataSource = Enum.GetValues(typeof(StructureType));

#if DEBUG
        //cbFilter.SelectedItem = ((List<IImageProcessor>)cbFilter.DataSource).Find(x => x.DisplayName == "Edge Magnitude");
        //cbMode.SelectedItem = ModeType.HoughVisualization;
        //LenaButton_Click(null!, null!);
#endif
    }

    private FilterCollection GetFilters()
    {
        var structureElement = cbStructureElement.SelectedItem as StructureType? ?? StructureType.Plus;

        var isBinary = checkBinary.Checked;
        var size = (int)numericSize.Value;

        var availableProcessors = new FilterCollection()
            .AddProcess(new FilterCollection("Grayscale"))
            .AddInversionFilter()
            .AddGaussian(size, 1)
            .AddContrastAdjustment()
            .AddEdgeMagnitudeFilter()
            .AddDilationFilter(structureElement, size, isBinary)
            .AddErosionFilter(structureElement, size, isBinary)
            .AddClosingFilter(structureElement, size, isBinary)
            .AddOpeningFilter(structureElement, size, isBinary)
            .AddThresholdFilter(80)
            .AddHistogramEqualization()
            .AddMedianFilter(size);

        var imageA = new FilterCollection("Image A")
            .AddContrastAdjustment();
        availableProcessors.AddProcess(imageA);

        var imageB = FilterCollection.From(imageA, "Image B1")
            .AddGaussian(9, 3.2f);
        availableProcessors.AddProcess(imageB);
        var imageB2 = FilterCollection.From(imageA, "Image B2")
            .AddGaussian(9, 3.2f);
        availableProcessors.AddProcess(imageB2);
        var imageB3 = FilterCollection.From(imageA, "Image B3")
            .AddGaussian(9, 40.2f);
        availableProcessors.AddProcess(imageB3);
        var imageB4 = FilterCollection.From(imageA, "Image B4")
            .AddGaussian(9, 100f);
        availableProcessors.AddProcess(imageB4);

        var imageW = FilterCollection.From(imageB, "Image W")
            .AddThresholdFilter(128);
        availableProcessors.AddProcess(imageW);

        var imageX = FilterCollection.From(imageW, "Image X")
            .AddDilationFilter(StructureType.Plus, 3);
        availableProcessors.AddProcess(imageX);

        var imageY = FilterCollection.From(imageW, "Image Y")
            .AddErosionFilter(StructureType.Plus, 3);
        availableProcessors.AddProcess(imageY);

        var imageZ = new FilterCollection("Image Z")
            .AddProcess(new AndFilter(imageX, imageY));
        availableProcessors.AddProcess(imageZ);

        var imageE1 = FilterCollection.From(imageA, "Image E1")
            .AddDilationFilter(StructureType.Plus, 3);
        availableProcessors.AddProcess(imageE1);

        var imageE2 = FilterCollection.From(imageA, "Image E2")
            .AddDilationFilter(StructureType.Plus, 7);
        availableProcessors.AddProcess(imageE2);

        var imageE3 = FilterCollection.From(imageA, "Image E3")
            .AddDilationFilter(StructureType.Plus, 13);
        availableProcessors.AddProcess(imageE3);

        var imageE4 = FilterCollection.From(imageA, "Image E4")
            .AddDilationFilter(StructureType.Plus, 25);
        availableProcessors.AddProcess(imageE4);

        var imageE5 = FilterCollection.From(imageA, "Image E5")
            .AddDilationFilter(StructureType.Plus, 49);
        availableProcessors.AddProcess(imageE5);

        var imageGear = new FilterCollection("image G")
            .AddThresholdFilter(150);
        availableProcessors.AddProcess(imageGear);

        var imageG1 = FilterCollection.From(imageGear, "image G1")
            .AddOpeningFilter(StructureType.Square, 3, true);
        availableProcessors.AddProcess(imageG1);

        var imageG2 = FilterCollection.From(imageGear, "image G2")
            .AddOpeningFilter(StructureType.Square, 23, true);
        availableProcessors.AddProcess(imageG2);

        var imageG3 = FilterCollection.From(imageGear, "image G3")
            .AddOpeningFilter(StructureType.Square, 43, true);
        availableProcessors.AddProcess(imageG3);

        var imageG4 = FilterCollection.From(imageGear, "image G4")
            .AddOpeningFilter(StructureType.Square, 63, true);
        availableProcessors.AddProcess(imageG4);

        var imageG5 = FilterCollection.From(imageGear, "image G5")
            .AddOpeningFilter(StructureType.Square, 83, true);
        availableProcessors.AddProcess(imageG5);

        var imageEdgeThresh = FilterCollection.From(imageA, "Edge With threshold")
            .AddGaussian(5, 3)
            .AddEdgeMagnitudeFilter()
            .AddThresholdFilter(100);
        availableProcessors.AddProcess(imageEdgeThresh);

        var imageEdge = FilterCollection.From(imageA, "Edge Without Threshold")
            .AddGaussian(5, 3)
            .AddEdgeMagnitudeFilter();
        availableProcessors.AddProcess(imageEdge);


        return availableProcessors;
    }

    /// <summary>
    /// Checks, sets and displays the input image
    /// </summary>
    /// <param name="value">Bitmap to display</param>
    private void SetInputImage(System.Drawing.Image value)
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
        if (cbFilter.SelectedValue is not IImageProcessor processor)
            return;

        if (inputImageBox.Image == null)
            return;

        if (!Enum.TryParse<ModeType>(cbMode.SelectedValue?.ToString(), out var mode))
            return;

        outputImageBox.Image?.Dispose();

        applyButton.Enabled = false;

        var singleChannel = await Task.Run(() => processor.Process(inputImageBox.Image));

        var histogram = new Histogram(singleChannel);

        outputImageBox.Image = GetShowOptions(singleChannel, histogram, mode);

        filterLabel.Text =
            $"{histogram.UniqueCount} Unique values | {histogram.NonBackgroundCount} Number of non background values";

        applyButton.Enabled = true;
    }

    private Bitmap GetShowOptions(byte[,] input, Histogram histogram, ModeType mode)
    {
        switch (mode)
        {
            case ModeType.Normal:
                return input.ToBitmap();
            case ModeType.Histogram:
                return histogram.ToBitmap(512, 300);
            case ModeType.CumulativeHistogram:
                return histogram.ToBitmap(512, 300, true);
            case ModeType.HoughTransform:
                return Hough.HoughTransform(input).ToBitmap();
            case ModeType.HoughPeaks:
                var pForm = new PeakForm();
                pForm.ShowDialog();
                return Hough.PeakFinding(input, pForm.Threshold).Item2.ToBitmap();
            case ModeType.HoughVisualization:
                var vForm = new VisualizeForm();
                vForm.ShowDialog();
                return Hough.VisualizeHoughLineSegments(input, vForm.MinThreshold, vForm.MinLength, vForm.MaxGap);
            case ModeType.HoughTransformAngleLimits:
                var aForm = new AngleForm();
                aForm.ShowDialog();
                return Hough.HoughTransformAngleLimits(input, aForm.LowerAngle, aForm.UpperAngle).ToBitmap();
            case ModeType.SIFT:
                SiftDoG(input, false);
                return Test(input);
            default:
                return input.ToBitmap();
        }
    }

    private Bitmap Test(byte[,] input)
    {
        return KeyPointSelection.DrawBoundingBox(input);
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

    private void GearButton_Click(object sender, EventArgs e)
    {
        SetInputImage(new Bitmap("Images/wheels.png"));
    }

    private void HoughTestButton_Click(object sender, EventArgs e)
    {
        SetInputImage(new Bitmap("Images/HoughTest.png"));
    }

    private void checkBinary_CheckedChanged(object sender, EventArgs e)
    {
        cbFilter.DataSource = GetFilters().ImageProcessors;
    }

    private void cbStructureElement_SelectedIndexChanged(object sender, EventArgs e)
    {
        cbFilter.DataSource = GetFilters().ImageProcessors;
    }

    private void numericSize_ValueChanged(object sender, EventArgs e)
    {
        if (numericSize.Value % 2 == 0) numericSize.Value += 1;

        cbFilter.DataSource = GetFilters().ImageProcessors;
    }
}