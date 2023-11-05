namespace INFOIBV
{
    partial class DoGTest
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
            G1 = new PictureBox();
            label1 = new Label();
            G2 = new PictureBox();
            G3 = new PictureBox();
            G4 = new PictureBox();
            D1 = new PictureBox();
            label2 = new Label();
            D2 = new PictureBox();
            D3 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)G1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)G2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)G3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)G4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)D1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)D2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)D3).BeginInit();
            SuspendLayout();
            // 
            // G1
            // 
            G1.Location = new Point(80, 70);
            G1.Margin = new Padding(4, 5, 4, 5);
            G1.Name = "G1";
            G1.Size = new Size(263, 159);
            //G1.SizeMode = PictureBoxSizeMode.AutoSize;
            G1.SizeMode = PictureBoxSizeMode.CenterImage;
            G1.TabIndex = 3;
            G1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(80, 26);
            label1.Name = "label1";
            label1.Size = new Size(70, 20);
            label1.TabIndex = 14;
            label1.Text = "Gaussian:";
            // 
            // G2
            // 
            G2.Location = new Point(80, 239);
            G2.Margin = new Padding(4, 5, 4, 5);
            G2.Name = "G2";
            G2.Size = new Size(263, 159);
            G2.SizeMode = PictureBoxSizeMode.CenterImage;
            G2.TabIndex = 15;
            G2.TabStop = false;
            // 
            // G3
            // 
            G3.Location = new Point(80, 408);
            G3.Margin = new Padding(4, 5, 4, 5);
            G3.Name = "G3";
            G3.Size = new Size(263, 159);
            G3.SizeMode = PictureBoxSizeMode.CenterImage;
            G3.TabIndex = 16;
            G3.TabStop = false;
            // 
            // G4
            // 
            G4.Location = new Point(80, 577);
            G4.Margin = new Padding(4, 5, 4, 5);
            G4.Name = "G4";
            G4.Size = new Size(263, 159);
            G4.SizeMode = PictureBoxSizeMode.CenterImage;
            G4.TabIndex = 17;
            G4.TabStop = false;
            // 
            // D1
            // 
            D1.Location = new Point(400, 157);
            D1.Margin = new Padding(4, 5, 4, 5);
            D1.Name = "D1";
            D1.Size = new Size(263, 159);
            D1.SizeMode = PictureBoxSizeMode.CenterImage;
            D1.TabIndex = 18;
            D1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(400, 26);
            label2.Name = "label2";
            label2.Size = new Size(42, 20);
            label2.TabIndex = 19;
            label2.Text = "DoG:";
            // 
            // D2
            // 
            D2.Location = new Point(400, 326);
            D2.Margin = new Padding(4, 5, 4, 5);
            D2.Name = "D2";
            D2.Size = new Size(263, 159);
            D2.SizeMode = PictureBoxSizeMode.CenterImage;
            D2.TabIndex = 20;
            D2.TabStop = false;
            // 
            // D3
            // 
            D3.Location = new Point(400, 495);
            D3.Margin = new Padding(4, 5, 4, 5);
            D3.Name = "D3";
            D3.Size = new Size(263, 159);
            D3.SizeMode = PictureBoxSizeMode.CenterImage;
            D3.TabIndex = 21;
            D3.TabStop = false;
            // 
            // DoGTest
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(763, 774);
            Controls.Add(D3);
            Controls.Add(D2);
            Controls.Add(label2);
            Controls.Add(D1);
            Controls.Add(G4);
            Controls.Add(G3);
            Controls.Add(G2);
            Controls.Add(label1);
            Controls.Add(G1);
            Name = "DoGTest";
            Text = "DoGTest";
            ((System.ComponentModel.ISupportInitialize)G1).EndInit();
            ((System.ComponentModel.ISupportInitialize)G2).EndInit();
            ((System.ComponentModel.ISupportInitialize)G3).EndInit();
            ((System.ComponentModel.ISupportInitialize)G4).EndInit();
            ((System.ComponentModel.ISupportInitialize)D1).EndInit();
            ((System.ComponentModel.ISupportInitialize)D2).EndInit();
            ((System.ComponentModel.ISupportInitialize)D3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public PictureBox G1;
        private Label label1;
        public PictureBox G2;
        public PictureBox G3;
        public PictureBox G4;
        public PictureBox D1;
        private Label label2;
        public PictureBox D2;
        public PictureBox D3;
    }
}