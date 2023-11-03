namespace INFOIBV.SIFT
{
    partial class FormPreprocessing
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
            processedImageBox = new PictureBox();
            continueButton = new Button();
            resetButton = new Button();
            invertButton = new Button();
            contrastButton = new Button();
            edgeButton = new Button();
            histogramButton = new Button();
            threshButton = new Button();
            gaussianButton = new Button();
            dilationButton = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            thresholdUpDown = new NumericUpDown();
            gaussianSize = new NumericUpDown();
            gaussianUpDown = new NumericUpDown();
            dilationStructure = new ComboBox();
            dilationUpDown = new NumericUpDown();
            dilationBinary = new CheckBox();
            medianButton = new Button();
            medianUpDown = new NumericUpDown();
            label3 = new Label();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)processedImageBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)thresholdUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gaussianSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gaussianUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dilationUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)medianUpDown).BeginInit();
            SuspendLayout();
            // 
            // processedImageBox
            // 
            processedImageBox.Location = new Point(13, 47);
            processedImageBox.Margin = new Padding(4, 5, 4, 5);
            processedImageBox.Name = "processedImageBox";
            processedImageBox.Size = new Size(683, 788);
            processedImageBox.SizeMode = PictureBoxSizeMode.CenterImage;
            processedImageBox.TabIndex = 3;
            processedImageBox.TabStop = false;
            // 
            // continueButton
            // 
            continueButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            continueButton.Location = new Point(730, 745);
            continueButton.Margin = new Padding(4, 5, 4, 5);
            continueButton.Name = "continueButton";
            continueButton.Size = new Size(353, 90);
            continueButton.TabIndex = 4;
            continueButton.Text = "Continue";
            continueButton.UseVisualStyleBackColor = true;
            continueButton.Click += continueButton_Click;
            // 
            // resetButton
            // 
            resetButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            resetButton.Location = new Point(946, 47);
            resetButton.Margin = new Padding(4, 5, 4, 5);
            resetButton.Name = "resetButton";
            resetButton.Size = new Size(137, 170);
            resetButton.TabIndex = 5;
            resetButton.Text = "Reset";
            resetButton.UseVisualStyleBackColor = true;
            resetButton.Click += resetButton_Click;
            // 
            // invertButton
            // 
            invertButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            invertButton.Location = new Point(730, 47);
            invertButton.Margin = new Padding(4, 5, 4, 5);
            invertButton.Name = "invertButton";
            invertButton.Size = new Size(137, 35);
            invertButton.TabIndex = 6;
            invertButton.Text = "Invert";
            invertButton.UseVisualStyleBackColor = true;
            invertButton.Click += invertButton_Click;
            // 
            // contrastButton
            // 
            contrastButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            contrastButton.Location = new Point(730, 92);
            contrastButton.Margin = new Padding(4, 5, 4, 5);
            contrastButton.Name = "contrastButton";
            contrastButton.Size = new Size(137, 35);
            contrastButton.TabIndex = 7;
            contrastButton.Text = "Contrast Adjust";
            contrastButton.UseVisualStyleBackColor = true;
            contrastButton.Click += contrastButton_Click;
            // 
            // edgeButton
            // 
            edgeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            edgeButton.Location = new Point(730, 137);
            edgeButton.Margin = new Padding(4, 5, 4, 5);
            edgeButton.Name = "edgeButton";
            edgeButton.Size = new Size(137, 35);
            edgeButton.TabIndex = 8;
            edgeButton.Text = "Edge Magnitude";
            edgeButton.UseVisualStyleBackColor = true;
            edgeButton.Click += edgeButton_Click;
            // 
            // histogramButton
            // 
            histogramButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            histogramButton.Location = new Point(730, 182);
            histogramButton.Margin = new Padding(4, 5, 4, 5);
            histogramButton.Name = "histogramButton";
            histogramButton.Size = new Size(137, 35);
            histogramButton.TabIndex = 9;
            histogramButton.Text = "Histogram Eq";
            histogramButton.UseVisualStyleBackColor = true;
            histogramButton.Click += histogramButton_Click;
            // 
            // threshButton
            // 
            threshButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            threshButton.Location = new Point(730, 293);
            threshButton.Margin = new Padding(4, 5, 4, 5);
            threshButton.Name = "threshButton";
            threshButton.Size = new Size(137, 35);
            threshButton.TabIndex = 10;
            threshButton.Text = "Threshold";
            threshButton.UseVisualStyleBackColor = true;
            threshButton.Click += threshButton_Click;
            // 
            // gaussianButton
            // 
            gaussianButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            gaussianButton.Location = new Point(730, 402);
            gaussianButton.Margin = new Padding(4, 5, 4, 5);
            gaussianButton.Name = "gaussianButton";
            gaussianButton.Size = new Size(137, 35);
            gaussianButton.TabIndex = 11;
            gaussianButton.Text = "Gaussian";
            gaussianButton.UseVisualStyleBackColor = true;
            gaussianButton.Click += gaussianButton_Click;
            // 
            // dilationButton
            // 
            dilationButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dilationButton.Location = new Point(730, 470);
            dilationButton.Margin = new Padding(4, 5, 4, 5);
            dilationButton.Name = "dilationButton";
            dilationButton.Size = new Size(137, 35);
            dilationButton.TabIndex = 12;
            dilationButton.Text = "Dilation";
            dilationButton.UseVisualStyleBackColor = true;
            dilationButton.Click += dilationButton_Click;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button4.Location = new Point(730, 515);
            button4.Margin = new Padding(4, 5, 4, 5);
            button4.Name = "button4";
            button4.Size = new Size(137, 35);
            button4.TabIndex = 13;
            button4.Text = "Histogram Eq";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button5.Location = new Point(730, 560);
            button5.Margin = new Padding(4, 5, 4, 5);
            button5.Name = "button5";
            button5.Size = new Size(137, 35);
            button5.TabIndex = 14;
            button5.Text = "Histogram Eq";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button6.Location = new Point(730, 605);
            button6.Margin = new Padding(4, 5, 4, 5);
            button6.Name = "button6";
            button6.Size = new Size(137, 35);
            button6.TabIndex = 15;
            button6.Text = "Histogram Eq";
            button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button7.Location = new Point(730, 650);
            button7.Margin = new Padding(4, 5, 4, 5);
            button7.Name = "button7";
            button7.Size = new Size(137, 35);
            button7.TabIndex = 16;
            button7.Text = "Histogram Eq";
            button7.UseVisualStyleBackColor = true;
            // 
            // thresholdUpDown
            // 
            thresholdUpDown.Location = new Point(891, 297);
            thresholdUpDown.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            thresholdUpDown.Name = "thresholdUpDown";
            thresholdUpDown.Size = new Size(57, 27);
            thresholdUpDown.TabIndex = 17;
            thresholdUpDown.Value = new decimal(new int[] { 128, 0, 0, 0 });
            // 
            // gaussianSize
            // 
            gaussianSize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            gaussianSize.Increment = new decimal(new int[] { 2, 0, 0, 0 });
            gaussianSize.Location = new Point(891, 405);
            gaussianSize.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            gaussianSize.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            gaussianSize.Name = "gaussianSize";
            gaussianSize.Size = new Size(57, 27);
            gaussianSize.TabIndex = 22;
            gaussianSize.Value = new decimal(new int[] { 3, 0, 0, 0 });
            gaussianSize.ValueChanged += gaussianSize_ValueChanged;
            // 
            // gaussianUpDown
            // 
            gaussianUpDown.Location = new Point(969, 405);
            gaussianUpDown.Name = "gaussianUpDown";
            gaussianUpDown.Size = new Size(57, 27);
            gaussianUpDown.TabIndex = 23;
            gaussianUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // dilationStructure
            // 
            dilationStructure.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dilationStructure.DropDownStyle = ComboBoxStyle.DropDownList;
            dilationStructure.FormattingEnabled = true;
            dilationStructure.Location = new Point(889, 474);
            dilationStructure.Name = "dilationStructure";
            dilationStructure.Size = new Size(59, 28);
            dilationStructure.TabIndex = 24;
            // 
            // dilationUpDown
            // 
            dilationUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dilationUpDown.Increment = new decimal(new int[] { 2, 0, 0, 0 });
            dilationUpDown.Location = new Point(969, 474);
            dilationUpDown.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            dilationUpDown.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            dilationUpDown.Name = "dilationUpDown";
            dilationUpDown.Size = new Size(57, 27);
            dilationUpDown.TabIndex = 25;
            dilationUpDown.Value = new decimal(new int[] { 3, 0, 0, 0 });
            dilationUpDown.ValueChanged += dilationUpDown_ValueChanged;
            // 
            // dilationBinary
            // 
            dilationBinary.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dilationBinary.AutoSize = true;
            dilationBinary.Location = new Point(1043, 476);
            dilationBinary.Name = "dilationBinary";
            dilationBinary.Size = new Size(72, 24);
            dilationBinary.TabIndex = 26;
            dilationBinary.Text = "Binary";
            dilationBinary.UseVisualStyleBackColor = true;
            // 
            // medianButton
            // 
            medianButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            medianButton.Location = new Point(730, 357);
            medianButton.Margin = new Padding(4, 5, 4, 5);
            medianButton.Name = "medianButton";
            medianButton.Size = new Size(137, 35);
            medianButton.TabIndex = 27;
            medianButton.Text = "Median";
            medianButton.UseVisualStyleBackColor = true;
            medianButton.Click += medianButton_Click;
            // 
            // medianUpDown
            // 
            medianUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            medianUpDown.Increment = new decimal(new int[] { 2, 0, 0, 0 });
            medianUpDown.Location = new Point(891, 361);
            medianUpDown.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            medianUpDown.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            medianUpDown.Name = "medianUpDown";
            medianUpDown.Size = new Size(57, 27);
            medianUpDown.TabIndex = 28;
            medianUpDown.Value = new decimal(new int[] { 3, 0, 0, 0 });
            medianUpDown.ValueChanged += medianUpDown_ValueChanged;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.Location = new Point(889, 335);
            label3.Name = "label3";
            label3.Size = new Size(39, 23);
            label3.TabIndex = 29;
            label3.Text = "Size:";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.Location = new Point(965, 379);
            label1.Name = "label1";
            label1.Size = new Size(57, 23);
            label1.TabIndex = 30;
            label1.Text = "Sigma:";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.Location = new Point(965, 448);
            label2.Name = "label2";
            label2.Size = new Size(39, 23);
            label2.TabIndex = 31;
            label2.Text = "Size:";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // FormPreprocessing
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1116, 862);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(medianUpDown);
            Controls.Add(medianButton);
            Controls.Add(dilationBinary);
            Controls.Add(dilationUpDown);
            Controls.Add(dilationStructure);
            Controls.Add(gaussianUpDown);
            Controls.Add(gaussianSize);
            Controls.Add(thresholdUpDown);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(dilationButton);
            Controls.Add(gaussianButton);
            Controls.Add(threshButton);
            Controls.Add(histogramButton);
            Controls.Add(edgeButton);
            Controls.Add(contrastButton);
            Controls.Add(invertButton);
            Controls.Add(resetButton);
            Controls.Add(continueButton);
            Controls.Add(processedImageBox);
            Name = "FormPreprocessing";
            Text = "FormPreprocessing";
            ((System.ComponentModel.ISupportInitialize)processedImageBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)thresholdUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)gaussianSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)gaussianUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)dilationUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)medianUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public PictureBox processedImageBox;
        private Button continueButton;
        private Button resetButton;
        private Button invertButton;
        private Button contrastButton;
        private Button edgeButton;
        private Button histogramButton;
        private Button threshButton;
        private Button gaussianButton;
        private Button dilationButton;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private NumericUpDown thresholdUpDown;
        private NumericUpDown gaussianSize;
        private NumericUpDown gaussianUpDown;
        private ComboBox dilationStructure;
        private NumericUpDown dilationUpDown;
        private CheckBox dilationBinary;
        private Button medianButton;
        private NumericUpDown medianUpDown;
        private Label label3;
        private Label label1;
        private Label label2;
    }
}