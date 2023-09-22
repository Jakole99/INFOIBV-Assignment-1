namespace INFOIBV
{
    partial class INFOIBV
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
            this.LoadImageButton = new System.Windows.Forms.Button();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.imageFileName = new System.Windows.Forms.TextBox();
            this.inputImageBox = new System.Windows.Forms.PictureBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveButton = new System.Windows.Forms.Button();
            this.outputImageBox = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.pipeline1Button = new System.Windows.Forms.Button();
            this.pipeline2Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.inputImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadImageButton
            // 
            this.LoadImageButton.Location = new System.Drawing.Point(16, 15);
            this.LoadImageButton.Margin = new System.Windows.Forms.Padding(4);
            this.LoadImageButton.Name = "LoadImageButton";
            this.LoadImageButton.Size = new System.Drawing.Size(131, 28);
            this.LoadImageButton.TabIndex = 0;
            this.LoadImageButton.Text = "Load image...";
            this.LoadImageButton.UseVisualStyleBackColor = true;
            this.LoadImageButton.Click += new System.EventHandler(this.LoadImageButton_Click);
            // 
            // openImageDialog
            // 
            this.openImageDialog.Filter = "Bitmap files (*.bmp;*.gif;*.jpg;*.png;*.tiff;*.jpeg)|*.bmp;*.gif;*.jpg;*.png;*.ti" + "ff;*.jpeg";
            this.openImageDialog.InitialDirectory = "..\\..\\images";
            // 
            // imageFileName
            // 
            this.imageFileName.Location = new System.Drawing.Point(155, 17);
            this.imageFileName.Margin = new System.Windows.Forms.Padding(4);
            this.imageFileName.Name = "imageFileName";
            this.imageFileName.ReadOnly = true;
            this.imageFileName.Size = new System.Drawing.Size(420, 22);
            this.imageFileName.TabIndex = 1;
            // 
            // inputImageBox
            // 
            this.inputImageBox.Location = new System.Drawing.Point(16, 86);
            this.inputImageBox.Margin = new System.Windows.Forms.Padding(4);
            this.inputImageBox.Name = "inputImageBox";
            this.inputImageBox.Size = new System.Drawing.Size(683, 630);
            this.inputImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.inputImageBox.TabIndex = 2;
            this.inputImageBox.TabStop = false;
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(729, 17);
            this.applyButton.Margin = new System.Windows.Forms.Padding(4);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(137, 28);
            this.applyButton.TabIndex = 3;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // saveImageDialog
            // 
            this.saveImageDialog.Filter = "Bitmap file (*.bmp)|*.bmp";
            this.saveImageDialog.InitialDirectory = "..\\..\\images";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(1264, 14);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(127, 28);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save as BMP...";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // outputImageBox
            // 
            this.outputImageBox.Location = new System.Drawing.Point(707, 86);
            this.outputImageBox.Margin = new System.Windows.Forms.Padding(4);
            this.outputImageBox.Name = "outputImageBox";
            this.outputImageBox.Size = new System.Drawing.Size(683, 630);
            this.outputImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.outputImageBox.TabIndex = 5;
            this.outputImageBox.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(888, 15);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(368, 25);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 6;
            this.progressBar.Visible = false;
            // 
            // cbFilter
            // 
            this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Location = new System.Drawing.Point(601, 19);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(121, 24);
            this.cbFilter.TabIndex = 7;
            // 
            // pipeline1Button
            // 
            this.pipeline1Button.Location = new System.Drawing.Point(974, 50);
            this.pipeline1Button.Margin = new System.Windows.Forms.Padding(4);
            this.pipeline1Button.Name = "pipeline1Button";
            this.pipeline1Button.Size = new System.Drawing.Size(137, 28);
            this.pipeline1Button.TabIndex = 8;
            this.pipeline1Button.Text = "Pipeline 1";
            this.pipeline1Button.UseVisualStyleBackColor = true;
            this.pipeline1Button.Click += new System.EventHandler(this.PipeLineButton1_Click);
            // 
            // pipeline2Button
            // 
            this.pipeline2Button.Location = new System.Drawing.Point(1119, 50);
            this.pipeline2Button.Margin = new System.Windows.Forms.Padding(4);
            this.pipeline2Button.Name = "pipeline2Button";
            this.pipeline2Button.Size = new System.Drawing.Size(137, 28);
            this.pipeline2Button.TabIndex = 9;
            this.pipeline2Button.Text = "Pipeline 2";
            this.pipeline2Button.UseVisualStyleBackColor = true;
            this.pipeline2Button.Click += new System.EventHandler(this.pipeline2Button_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(611, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(356, 22);
            this.label1.TabIndex = 10;
            this.label1.Text = "label1";
            // 
            // INFOIBV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1403, 729);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pipeline2Button);
            this.Controls.Add(this.pipeline1Button);
            this.Controls.Add(this.cbFilter);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.outputImageBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.inputImageBox);
            this.Controls.Add(this.imageFileName);
            this.Controls.Add(this.LoadImageButton);
            this.Location = new System.Drawing.Point(10, 10);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "INFOIBV";
            this.ShowIcon = false;
            this.Text = "INFOIBV";
            ((System.ComponentModel.ISupportInitialize)(this.inputImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;

        #endregion

        private System.Windows.Forms.Button LoadImageButton;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.TextBox imageFileName;
        private System.Windows.Forms.PictureBox inputImageBox;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.SaveFileDialog saveImageDialog;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.PictureBox outputImageBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.Button pipeline1Button;
        private System.Windows.Forms.Button pipeline2Button;
    }
}

