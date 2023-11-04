namespace INFOIBV
{
    partial class FormImageProcessing1
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
            outputImageBoxP1 = new PictureBox();
            cbMode = new ComboBox();
            label2 = new Label();
            cbDetectionImage = new ComboBox();
            label4 = new Label();
            button1 = new Button();
            progressBar = new ProgressBar();
            filterLabel = new Label();
            inputReferenceImageBox = new PictureBox();
            inputLabel = new Label();
            referenceLabel = new Label();
            outputImageBoxP2 = new PictureBox();
            outputImageBox = new PictureBox();
            outputLabel = new Label();
            ReferenceImageFileName = new TextBox();
            LoadReferenceButton = new Button();
            preprocessRefButton = new Button();
            label1 = new Label();
            loadUnoButton = new Button();
            ((System.ComponentModel.ISupportInitialize)inputImageBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)outputImageBoxP1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)inputReferenceImageBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)outputImageBoxP2).BeginInit();
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
            imageFileName.Size = new Size(361, 27);
            imageFileName.TabIndex = 1;
            // 
            // inputImageBox
            // 
            inputImageBox.Location = new Point(16, 108);
            inputImageBox.Margin = new Padding(4, 5, 4, 5);
            inputImageBox.Name = "inputImageBox";
            inputImageBox.Size = new Size(506, 788);
            inputImageBox.SizeMode = PictureBoxSizeMode.CenterImage;
            inputImageBox.TabIndex = 2;
            inputImageBox.TabStop = false;
            // 
            // applyButton
            // 
            applyButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            applyButton.Location = new Point(1389, 62);
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
            saveButton.Location = new Point(1389, 22);
            saveButton.Margin = new Padding(4, 5, 4, 5);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(137, 35);
            saveButton.TabIndex = 4;
            saveButton.Text = "Save as BMP...";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += SaveButton_Click;
            // 
            // outputImageBoxP1
            // 
            outputImageBoxP1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            outputImageBoxP1.Location = new Point(1044, 108);
            outputImageBoxP1.Margin = new Padding(4, 5, 4, 5);
            outputImageBoxP1.Name = "outputImageBoxP1";
            outputImageBoxP1.Size = new Size(486, 386);
            outputImageBoxP1.SizeMode = PictureBoxSizeMode.Zoom;
            outputImageBoxP1.TabIndex = 5;
            outputImageBoxP1.TabStop = false;
            // 
            // cbMode
            // 
            cbMode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMode.FormattingEnabled = true;
            cbMode.Location = new Point(1245, 66);
            cbMode.Margin = new Padding(3, 4, 3, 4);
            cbMode.Name = "cbMode";
            cbMode.Size = new Size(137, 28);
            cbMode.TabIndex = 15;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.Location = new Point(1185, 69);
            label2.Name = "label2";
            label2.Size = new Size(54, 29);
            label2.TabIndex = 16;
            label2.Text = "Show:";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // cbDetectionImage
            // 
            cbDetectionImage.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDetectionImage.FormattingEnabled = true;
            cbDetectionImage.Location = new Point(204, 67);
            cbDetectionImage.Margin = new Padding(3, 4, 3, 4);
            cbDetectionImage.Name = "cbDetectionImage";
            cbDetectionImage.Size = new Size(137, 28);
            cbDetectionImage.TabIndex = 23;
            cbDetectionImage.SelectedIndexChanged += cbDetectionImage_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 72);
            label4.Name = "label4";
            label4.Size = new Size(176, 20);
            label4.TabIndex = 24;
            label4.Text = "Choose Detection Image:";
            // 
            // button1
            // 
            button1.Location = new Point(385, 62);
            button1.Margin = new Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new Size(137, 35);
            button1.TabIndex = 25;
            button1.Text = "Preprocess Image";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Preprocess_Click;
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar.Location = new Point(16, 905);
            progressBar.Margin = new Padding(4, 5, 4, 5);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(1514, 31);
            progressBar.TabIndex = 6;
            progressBar.Visible = false;
            // 
            // filterLabel
            // 
            filterLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            filterLabel.BackColor = Color.Transparent;
            filterLabel.Location = new Point(574, 941);
            filterLabel.Name = "filterLabel";
            filterLabel.Size = new Size(398, 25);
            filterLabel.TabIndex = 14;
            filterLabel.Text = "Pipeline";
            filterLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // inputReferenceImageBox
            // 
            inputReferenceImageBox.Anchor = AnchorStyles.Top;
            inputReferenceImageBox.Location = new Point(530, 107);
            inputReferenceImageBox.Margin = new Padding(4, 5, 4, 5);
            inputReferenceImageBox.Name = "inputReferenceImageBox";
            inputReferenceImageBox.Size = new Size(506, 788);
            inputReferenceImageBox.SizeMode = PictureBoxSizeMode.CenterImage;
            inputReferenceImageBox.TabIndex = 26;
            inputReferenceImageBox.TabStop = false;
            // 
            // inputLabel
            // 
            inputLabel.Location = new Point(213, 905);
            inputLabel.Name = "inputLabel";
            inputLabel.Size = new Size(54, 29);
            inputLabel.TabIndex = 27;
            inputLabel.Text = "Input";
            // 
            // referenceLabel
            // 
            referenceLabel.Anchor = AnchorStyles.Top;
            referenceLabel.Location = new Point(739, 907);
            referenceLabel.Name = "referenceLabel";
            referenceLabel.Size = new Size(85, 29);
            referenceLabel.TabIndex = 28;
            referenceLabel.Text = "Reference";
            // 
            // outputImageBoxP2
            // 
            outputImageBoxP2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            outputImageBoxP2.Location = new Point(1044, 510);
            outputImageBoxP2.Margin = new Padding(4, 5, 4, 5);
            outputImageBoxP2.Name = "outputImageBoxP2";
            outputImageBoxP2.Size = new Size(486, 386);
            outputImageBoxP2.SizeMode = PictureBoxSizeMode.Zoom;
            outputImageBoxP2.TabIndex = 30;
            outputImageBoxP2.TabStop = false;
            // 
            // outputImageBox
            // 
            outputImageBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            outputImageBox.Location = new Point(1044, 107);
            outputImageBox.Margin = new Padding(4, 5, 4, 5);
            outputImageBox.Name = "outputImageBox";
            outputImageBox.Size = new Size(498, 788);
            outputImageBox.SizeMode = PictureBoxSizeMode.CenterImage;
            outputImageBox.TabIndex = 31;
            outputImageBox.TabStop = false;
            // 
            // outputLabel
            // 
            outputLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            outputLabel.Location = new Point(1287, 907);
            outputLabel.Name = "outputLabel";
            outputLabel.Size = new Size(58, 29);
            outputLabel.TabIndex = 32;
            outputLabel.Text = "Output";
            // 
            // ReferenceImageFileName
            // 
            ReferenceImageFileName.Anchor = AnchorStyles.Top;
            ReferenceImageFileName.Location = new Point(751, 27);
            ReferenceImageFileName.Margin = new Padding(4, 5, 4, 5);
            ReferenceImageFileName.Name = "ReferenceImageFileName";
            ReferenceImageFileName.ReadOnly = true;
            ReferenceImageFileName.Size = new Size(283, 27);
            ReferenceImageFileName.TabIndex = 34;
            // 
            // LoadReferenceButton
            // 
            LoadReferenceButton.Anchor = AnchorStyles.Top;
            LoadReferenceButton.Location = new Point(534, 22);
            LoadReferenceButton.Margin = new Padding(4, 5, 4, 5);
            LoadReferenceButton.Name = "LoadReferenceButton";
            LoadReferenceButton.Size = new Size(209, 35);
            LoadReferenceButton.TabIndex = 33;
            LoadReferenceButton.Text = "Load Reference image...";
            LoadReferenceButton.UseVisualStyleBackColor = true;
            LoadReferenceButton.Click += LoadReferenceButton_Click;
            // 
            // preprocessRefButton
            // 
            preprocessRefButton.Anchor = AnchorStyles.Top;
            preprocessRefButton.Location = new Point(897, 62);
            preprocessRefButton.Margin = new Padding(4, 5, 4, 5);
            preprocessRefButton.Name = "preprocessRefButton";
            preprocessRefButton.Size = new Size(137, 35);
            preprocessRefButton.TabIndex = 35;
            preprocessRefButton.Text = "Preprocess Image";
            preprocessRefButton.UseVisualStyleBackColor = true;
            preprocessRefButton.Click += preprocessRefButton_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Location = new Point(534, 72);
            label1.Name = "label1";
            label1.Size = new Size(86, 20);
            label1.TabIndex = 36;
            label1.Text = "Quick Load:";
            // 
            // loadUnoButton
            // 
            loadUnoButton.Anchor = AnchorStyles.Top;
            loadUnoButton.Location = new Point(627, 64);
            loadUnoButton.Margin = new Padding(4, 5, 4, 5);
            loadUnoButton.Name = "loadUnoButton";
            loadUnoButton.Size = new Size(137, 35);
            loadUnoButton.TabIndex = 37;
            loadUnoButton.Text = "Uno";
            loadUnoButton.UseVisualStyleBackColor = true;
            loadUnoButton.Click += loadUnoButton_Click;
            // 
            // FormImageProcessing1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1543, 993);
            Controls.Add(loadUnoButton);
            Controls.Add(label1);
            Controls.Add(preprocessRefButton);
            Controls.Add(ReferenceImageFileName);
            Controls.Add(LoadReferenceButton);
            Controls.Add(outputLabel);
            Controls.Add(outputImageBox);
            Controls.Add(outputImageBoxP2);
            Controls.Add(referenceLabel);
            Controls.Add(inputLabel);
            Controls.Add(inputReferenceImageBox);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(cbDetectionImage);
            Controls.Add(label2);
            Controls.Add(cbMode);
            Controls.Add(filterLabel);
            Controls.Add(progressBar);
            Controls.Add(outputImageBoxP1);
            Controls.Add(saveButton);
            Controls.Add(applyButton);
            Controls.Add(inputImageBox);
            Controls.Add(imageFileName);
            Controls.Add(LoadImageButton);
            Location = new Point(10, 10);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FormImageProcessing1";
            ShowIcon = false;
            Text = "INFOIBV";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)inputImageBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)outputImageBoxP1).EndInit();
            ((System.ComponentModel.ISupportInitialize)inputReferenceImageBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)outputImageBoxP2).EndInit();
            ((System.ComponentModel.ISupportInitialize)outputImageBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private ComboBox cbMode;
        private Label label2;

        #endregion

        private Button LoadImageButton;
        private OpenFileDialog openImageDialog;
        private TextBox imageFileName;
        private PictureBox inputImageBox;
        private Button applyButton;
        private SaveFileDialog saveImageDialog;
        private Button saveButton;
        private ComboBox cbDetectionImage;
        private Label label4;
        private Button button1;
        private ProgressBar progressBar;
        private Label filterLabel;
        private Label inputLabel;
        private Label referenceLabel;
        private Label outputLabel;

        private PictureBox inputReferenceImageBox;
        private PictureBox outputImageBoxP1;
        private PictureBox outputImageBoxP2;
        private PictureBox outputImageBox;

        private TextBox ReferenceImageFileName;
        private Button LoadReferenceButton;
        private Button preprocessRefButton;
        private Label label1;
        private Button loadUnoButton;
    }
}