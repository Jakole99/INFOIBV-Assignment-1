namespace INFOIBV;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        LoadImageButton = new Button();
        openImageDialog = new OpenFileDialog();
        imageFileName = new TextBox();
        inputImageBox = new PictureBox();
        applyButton = new Button();
        saveImageDialog = new SaveFileDialog();
        saveButton = new Button();
        outputImageBox = new PictureBox();
        progressBar = new ProgressBar();
        cbFilter = new ComboBox();
        lenaButton = new Button();
        gridButton = new Button();
        cubeHousesButton = new Button();
        label1 = new Label();
        filterLabel = new Label();
        cbMode = new ComboBox();
        label2 = new Label();
        label3 = new Label();
        gearButton = new Button();
        ((System.ComponentModel.ISupportInitialize)inputImageBox).BeginInit();
        ((System.ComponentModel.ISupportInitialize)outputImageBox).BeginInit();
        SuspendLayout();
        // 
        // LoadImageButton
        // 
        LoadImageButton.Location = new Point(22, 21);
        LoadImageButton.Margin = new Padding(4, 5, 4, 5);
        LoadImageButton.Name = "LoadImageButton";
        LoadImageButton.Size = new Size(131, 35);
        LoadImageButton.TabIndex = 0;
        LoadImageButton.Text = "Load image...";
        LoadImageButton.UseVisualStyleBackColor = true;
        LoadImageButton.Click += LoadImageButton_Click;
        // 
        // openImageDialog
        // 
        openImageDialog.Filter = "Bitmap files (*.bmp;*.gif;*.jpg;*.png;*.tiff;*.jpeg)|*.bmp;*.gif;*.jpg;*.png;*.tiff;*.jpeg";
        openImageDialog.InitialDirectory = "..\\..\\images";
        // 
        // imageFileName
        // 
        imageFileName.Location = new Point(161, 26);
        imageFileName.Margin = new Padding(4, 5, 4, 5);
        imageFileName.Name = "imageFileName";
        imageFileName.ReadOnly = true;
        imageFileName.Size = new Size(427, 27);
        imageFileName.TabIndex = 1;
        // 
        // inputImageBox
        // 
        inputImageBox.Location = new Point(16, 108);
        inputImageBox.Margin = new Padding(4, 5, 4, 5);
        inputImageBox.Name = "inputImageBox";
        inputImageBox.Size = new Size(683, 788);
        inputImageBox.SizeMode = PictureBoxSizeMode.CenterImage;
        inputImageBox.TabIndex = 2;
        inputImageBox.TabStop = false;
        // 
        // applyButton
        // 
        applyButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        applyButton.Location = new Point(1108, 21);
        applyButton.Margin = new Padding(4, 5, 4, 5);
        applyButton.Name = "applyButton";
        applyButton.Size = new Size(137, 35);
        applyButton.TabIndex = 3;
        applyButton.Text = "Apply";
        applyButton.UseVisualStyleBackColor = true;
        applyButton.Click += ApplyButton_Click;
        // 
        // saveImageDialog
        // 
        saveImageDialog.Filter = "Bitmap file (*.bmp)|*.bmp";
        saveImageDialog.InitialDirectory = "..\\..\\images";
        // 
        // saveButton
        // 
        saveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        saveButton.Location = new Point(1253, 21);
        saveButton.Margin = new Padding(4, 5, 4, 5);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(137, 35);
        saveButton.TabIndex = 4;
        saveButton.Text = "Save as BMP...";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += SaveButton_Click;
        // 
        // outputImageBox
        // 
        outputImageBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        outputImageBox.Location = new Point(707, 108);
        outputImageBox.Margin = new Padding(4, 5, 4, 5);
        outputImageBox.Name = "outputImageBox";
        outputImageBox.Size = new Size(683, 788);
        outputImageBox.SizeMode = PictureBoxSizeMode.CenterImage;
        outputImageBox.TabIndex = 5;
        outputImageBox.TabStop = false;
        // 
        // progressBar
        // 
        progressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        progressBar.Location = new Point(16, 905);
        progressBar.Margin = new Padding(4, 5, 4, 5);
        progressBar.Name = "progressBar";
        progressBar.Size = new Size(1374, 31);
        progressBar.TabIndex = 6;
        progressBar.Visible = false;
        // 
        // cbFilter
        // 
        cbFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        cbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
        cbFilter.FormattingEnabled = true;
        cbFilter.Location = new Point(964, 24);
        cbFilter.Margin = new Padding(3, 4, 3, 4);
        cbFilter.Name = "cbFilter";
        cbFilter.Size = new Size(137, 28);
        cbFilter.TabIndex = 7;
        // 
        // lenaButton
        // 
        lenaButton.Location = new Point(155, 62);
        lenaButton.Margin = new Padding(4, 5, 4, 5);
        lenaButton.Name = "lenaButton";
        lenaButton.Size = new Size(137, 35);
        lenaButton.TabIndex = 10;
        lenaButton.Text = "Lena";
        lenaButton.UseVisualStyleBackColor = true;
        lenaButton.Click += LenaButton_Click;
        // 
        // gridButton
        // 
        gridButton.Location = new Point(300, 62);
        gridButton.Margin = new Padding(4, 5, 4, 5);
        gridButton.Name = "gridButton";
        gridButton.Size = new Size(137, 35);
        gridButton.TabIndex = 11;
        gridButton.Text = "Grid";
        gridButton.UseVisualStyleBackColor = true;
        gridButton.Click += GridButton_Click;
        // 
        // cubeHousesButton
        // 
        cubeHousesButton.Location = new Point(451, 62);
        cubeHousesButton.Margin = new Padding(4, 5, 4, 5);
        cubeHousesButton.Name = "cubeHousesButton";
        cubeHousesButton.Size = new Size(137, 35);
        cubeHousesButton.TabIndex = 12;
        cubeHousesButton.Text = "Cube Houses";
        cubeHousesButton.UseVisualStyleBackColor = true;
        cubeHousesButton.Click += CubeHousesButton_Click;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(22, 70);
        label1.Name = "label1";
        label1.Size = new Size(132, 20);
        label1.TabIndex = 13;
        label1.Text = "Quick Image Load:";
        // 
        // filterLabel
        // 
        filterLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        filterLabel.BackColor = Color.Transparent;
        filterLabel.Location = new Point(574, 870);
        filterLabel.Name = "filterLabel";
        filterLabel.Size = new Size(258, 25);
        filterLabel.TabIndex = 14;
        filterLabel.Text = "Pipeline";
        filterLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // cbMode
        // 
        cbMode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        cbMode.DropDownStyle = ComboBoxStyle.DropDownList;
        cbMode.FormattingEnabled = true;
        cbMode.Location = new Point(964, 66);
        cbMode.Margin = new Padding(3, 4, 3, 4);
        cbMode.Name = "cbMode";
        cbMode.Size = new Size(137, 28);
        cbMode.TabIndex = 15;
        // 
        // label2
        // 
        label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        label2.Location = new Point(904, 70);
        label2.Name = "label2";
        label2.Size = new Size(54, 29);
        label2.TabIndex = 16;
        label2.Text = "Show:";
        label2.TextAlign = ContentAlignment.TopRight;
        // 
        // label3
        // 
        label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        label3.Location = new Point(904, 28);
        label3.Name = "label3";
        label3.Size = new Size(54, 29);
        label3.TabIndex = 17;
        label3.Text = "Filter:";
        label3.TextAlign = ContentAlignment.TopRight;
        // 
        // gearButton
        // 
        gearButton.Location = new Point(596, 62);
        gearButton.Margin = new Padding(4, 5, 4, 5);
        gearButton.Name = "gearButton";
        gearButton.Size = new Size(137, 35);
        gearButton.TabIndex = 18;
        gearButton.Text = "Gears";
        gearButton.UseVisualStyleBackColor = true;
        gearButton.Click += GearButton_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1403, 942);
        Controls.Add(gearButton);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(cbMode);
        Controls.Add(filterLabel);
        Controls.Add(label1);
        Controls.Add(cubeHousesButton);
        Controls.Add(gridButton);
        Controls.Add(lenaButton);
        Controls.Add(cbFilter);
        Controls.Add(progressBar);
        Controls.Add(outputImageBox);
        Controls.Add(saveButton);
        Controls.Add(applyButton);
        Controls.Add(inputImageBox);
        Controls.Add(imageFileName);
        Controls.Add(LoadImageButton);
        Location = new Point(10, 10);
        Margin = new Padding(4, 5, 4, 5);
        Name = "Form1";
        ShowIcon = false;
        Text = "INFOIBV";
        WindowState = FormWindowState.Maximized;
        ((System.ComponentModel.ISupportInitialize)inputImageBox).EndInit();
        ((System.ComponentModel.ISupportInitialize)outputImageBox).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label label3;

    private ComboBox cbMode;
    private Label label2;

    private Label filterLabel;

    #endregion

    private Button LoadImageButton;
    private OpenFileDialog openImageDialog;
    private TextBox imageFileName;
    private PictureBox inputImageBox;
    private Button applyButton;
    private SaveFileDialog saveImageDialog;
    private Button saveButton;
    private PictureBox outputImageBox;
    private ProgressBar progressBar;
    private ComboBox cbFilter;
    private Button lenaButton;
    private Button gridButton;
    private Button cubeHousesButton;
    private Label label1;
    private Button gearButton;
}