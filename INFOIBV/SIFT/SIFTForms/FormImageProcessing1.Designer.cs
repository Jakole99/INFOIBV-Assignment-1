﻿namespace INFOIBV
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
            outputImageBox = new PictureBox();
            progressBar = new ProgressBar();
            filterLabel = new Label();
            cbMode = new ComboBox();
            label2 = new Label();
            cbDetectionImage = new ComboBox();
            label4 = new Label();
            button1 = new Button();
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
            imageFileName.Size = new Size(538, 27);
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
            applyButton.Location = new Point(1248, 58);
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
            // outputImageBox
            // 
            outputImageBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            outputImageBox.Location = new Point(847, 108);
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
            progressBar.Size = new Size(1514, 31);
            progressBar.TabIndex = 6;
            progressBar.Visible = false;
            // 
            // filterLabel
            // 
            filterLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            filterLabel.BackColor = Color.Transparent;
            filterLabel.Location = new Point(574, 870);
            filterLabel.Name = "filterLabel";
            filterLabel.Size = new Size(398, 25);
            filterLabel.TabIndex = 14;
            filterLabel.Text = "Pipeline";
            filterLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cbMode
            // 
            cbMode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMode.FormattingEnabled = true;
            cbMode.Location = new Point(1104, 62);
            cbMode.Margin = new Padding(3, 4, 3, 4);
            cbMode.Name = "cbMode";
            cbMode.Size = new Size(137, 28);
            cbMode.TabIndex = 15;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.Location = new Point(1044, 65);
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
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.Location = new Point(1248, 22);
            button1.Margin = new Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new Size(137, 35);
            button1.TabIndex = 25;
            button1.Text = "Preprocess Image";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Preprocess_Click;
            // 
            // FormImageProcessing1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1543, 942);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(cbDetectionImage);
            Controls.Add(label2);
            Controls.Add(cbMode);
            Controls.Add(filterLabel);
            Controls.Add(progressBar);
            Controls.Add(outputImageBox);
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
            ((System.ComponentModel.ISupportInitialize)outputImageBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

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
        private ComboBox cbDetectionImage;
        private Label label4;
        private Button button1;
    }
}