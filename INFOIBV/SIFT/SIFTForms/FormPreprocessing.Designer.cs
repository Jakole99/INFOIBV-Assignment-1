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
            erosionButton = new Button();
            openingButton = new Button();
            closingButton = new Button();
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
            erosionStructure = new ComboBox();
            erosionUpDown = new NumericUpDown();
            erosionBinary = new CheckBox();
            openingStructure = new ComboBox();
            openingUpDown = new NumericUpDown();
            openingBinary = new CheckBox();
            closingStructure = new ComboBox();
            closingUpDown = new NumericUpDown();
            closingBinary = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)processedImageBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)thresholdUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gaussianSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gaussianUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dilationUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)medianUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)erosionUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)openingUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)closingUpDown).BeginInit();
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
            // erosionButton
            // 
            erosionButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            erosionButton.Location = new Point(730, 515);
            erosionButton.Margin = new Padding(4, 5, 4, 5);
            erosionButton.Name = "erosionButton";
            erosionButton.Size = new Size(137, 35);
            erosionButton.TabIndex = 13;
            erosionButton.Text = "Erosion";
            erosionButton.UseVisualStyleBackColor = true;
            erosionButton.Click += erosionButton_Click;
            // 
            // openingButton
            // 
            openingButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            openingButton.Location = new Point(730, 560);
            openingButton.Margin = new Padding(4, 5, 4, 5);
            openingButton.Name = "openingButton";
            openingButton.Size = new Size(137, 35);
            openingButton.TabIndex = 14;
            openingButton.Text = "Opening";
            openingButton.UseVisualStyleBackColor = true;
            openingButton.Click += openingButton_Click;
            // 
            // closingButton
            // 
            closingButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closingButton.Location = new Point(730, 605);
            closingButton.Margin = new Padding(4, 5, 4, 5);
            closingButton.Name = "closingButton";
            closingButton.Size = new Size(137, 35);
            closingButton.TabIndex = 15;
            closingButton.Text = "Closing";
            closingButton.UseVisualStyleBackColor = true;
            closingButton.Click += closingButton_Click;
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
            // erosionStructure
            // 
            erosionStructure.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            erosionStructure.DropDownStyle = ComboBoxStyle.DropDownList;
            erosionStructure.FormattingEnabled = true;
            erosionStructure.Location = new Point(889, 519);
            erosionStructure.Name = "erosionStructure";
            erosionStructure.Size = new Size(59, 28);
            erosionStructure.TabIndex = 32;
            // 
            // erosionUpDown
            // 
            erosionUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            erosionUpDown.Increment = new decimal(new int[] { 2, 0, 0, 0 });
            erosionUpDown.Location = new Point(969, 519);
            erosionUpDown.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            erosionUpDown.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            erosionUpDown.Name = "erosionUpDown";
            erosionUpDown.Size = new Size(57, 27);
            erosionUpDown.TabIndex = 33;
            erosionUpDown.Value = new decimal(new int[] { 3, 0, 0, 0 });
            erosionUpDown.ValueChanged += erosionUpDown_ValueChanged;
            // 
            // erosionBinary
            // 
            erosionBinary.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            erosionBinary.AutoSize = true;
            erosionBinary.Location = new Point(1043, 521);
            erosionBinary.Name = "erosionBinary";
            erosionBinary.Size = new Size(72, 24);
            erosionBinary.TabIndex = 34;
            erosionBinary.Text = "Binary";
            erosionBinary.UseVisualStyleBackColor = true;
            // 
            // openingStructure
            // 
            openingStructure.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            openingStructure.DropDownStyle = ComboBoxStyle.DropDownList;
            openingStructure.FormattingEnabled = true;
            openingStructure.Location = new Point(889, 564);
            openingStructure.Name = "openingStructure";
            openingStructure.Size = new Size(59, 28);
            openingStructure.TabIndex = 35;
            // 
            // openingUpDown
            // 
            openingUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            openingUpDown.Increment = new decimal(new int[] { 2, 0, 0, 0 });
            openingUpDown.Location = new Point(969, 564);
            openingUpDown.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            openingUpDown.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            openingUpDown.Name = "openingUpDown";
            openingUpDown.Size = new Size(57, 27);
            openingUpDown.TabIndex = 36;
            openingUpDown.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // openingBinary
            // 
            openingBinary.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            openingBinary.AutoSize = true;
            openingBinary.Location = new Point(1043, 565);
            openingBinary.Name = "openingBinary";
            openingBinary.Size = new Size(72, 24);
            openingBinary.TabIndex = 37;
            openingBinary.Text = "Binary";
            openingBinary.UseVisualStyleBackColor = true;
            // 
            // closingStructure
            // 
            closingStructure.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closingStructure.DropDownStyle = ComboBoxStyle.DropDownList;
            closingStructure.FormattingEnabled = true;
            closingStructure.Location = new Point(889, 609);
            closingStructure.Name = "closingStructure";
            closingStructure.Size = new Size(59, 28);
            closingStructure.TabIndex = 38;
            // 
            // closingUpDown
            // 
            closingUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closingUpDown.Increment = new decimal(new int[] { 2, 0, 0, 0 });
            closingUpDown.Location = new Point(969, 609);
            closingUpDown.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            closingUpDown.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            closingUpDown.Name = "closingUpDown";
            closingUpDown.Size = new Size(57, 27);
            closingUpDown.TabIndex = 39;
            closingUpDown.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // closingBinary
            // 
            closingBinary.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closingBinary.AutoSize = true;
            closingBinary.Location = new Point(1043, 609);
            closingBinary.Name = "closingBinary";
            closingBinary.Size = new Size(72, 24);
            closingBinary.TabIndex = 40;
            closingBinary.Text = "Binary";
            closingBinary.UseVisualStyleBackColor = true;
            // 
            // FormPreprocessing
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1116, 862);
            Controls.Add(closingBinary);
            Controls.Add(closingUpDown);
            Controls.Add(closingStructure);
            Controls.Add(openingBinary);
            Controls.Add(openingUpDown);
            Controls.Add(openingStructure);
            Controls.Add(erosionBinary);
            Controls.Add(erosionUpDown);
            Controls.Add(erosionStructure);
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
            Controls.Add(closingButton);
            Controls.Add(openingButton);
            Controls.Add(erosionButton);
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
            ((System.ComponentModel.ISupportInitialize)erosionUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)openingUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)closingUpDown).EndInit();
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
        private Button erosionButton;
        private Button openingButton;
        private Button closingButton;
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
        private ComboBox erosionStructure;
        private NumericUpDown erosionUpDown;
        private CheckBox erosionBinary;
        private ComboBox openingStructure;
        private NumericUpDown openingUpDown;
        private CheckBox openingBinary;
        private ComboBox closingStructure;
        private NumericUpDown closingUpDown;
        private CheckBox closingBinary;
    }
}