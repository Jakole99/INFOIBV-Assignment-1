namespace INFOIBV.SIFT
{
    partial class SiftOutput
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
            inputImageBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)inputImageBox).BeginInit();
            SuspendLayout();
            // 
            // inputImageBox
            // 
            inputImageBox.Location = new Point(13, 14);
            inputImageBox.Margin = new Padding(4, 5, 4, 5);
            inputImageBox.Name = "inputImageBox";
            inputImageBox.Size = new Size(937, 496);
            inputImageBox.SizeMode = PictureBoxSizeMode.CenterImage;
            inputImageBox.TabIndex = 3;
            inputImageBox.TabStop = false;
            // 
            // SiftOutput
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(976, 524);
            Controls.Add(inputImageBox);
            Name = "SiftOutput";
            Text = "SiftOutput";
            ((System.ComponentModel.ISupportInitialize)inputImageBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox inputImageBox;
    }
}