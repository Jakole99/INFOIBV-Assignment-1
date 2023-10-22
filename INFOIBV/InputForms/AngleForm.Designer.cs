namespace INFOIBV.InputForms
{
    partial class AngleForm
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
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            numericUpDown1 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(137, 174);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 0;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(53, 52);
            label1.Name = "label1";
            label1.Size = new Size(165, 20);
            label1.TabIndex = 1;
            label1.Text = "Give Lower Angle Limit:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(53, 83);
            label2.Name = "label2";
            label2.Size = new Size(166, 20);
            label2.TabIndex = 2;
            label2.Text = "Give Upper Angle Limit:";
            // 
            // numericUpDown1
            // 
            numericUpDown1.DecimalPlaces = 2;
            numericUpDown1.Increment = new decimal(new int[] { -412474237, 73145, 0, 917504 });
            numericUpDown1.Location = new Point(235, 45);
            numericUpDown1.Maximum = new decimal(new int[] { -412474237, 73145, 0, 917504 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(50, 27);
            numericUpDown1.TabIndex = 3;
            numericUpDown1.Value = new decimal(new int[] { -412474237, 73145, 0, 917504 });
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // numericUpDown2
            // 
            numericUpDown2.DecimalPlaces = 2;
            numericUpDown2.Location = new Point(235, 81);
            numericUpDown2.Increment = (decimal)(Math.PI / 8);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Maximum = (decimal)(Math.PI/2);
            numericUpDown2.Size = new Size(50, 27);
            numericUpDown2.TabIndex = 4;
            numericUpDown2.Value = 0;
            numericUpDown2.ValueChanged += numericUpDown2_ValueChanged;
            // 
            // AngleForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(356, 217);
            Controls.Add(numericUpDown2);
            Controls.Add(numericUpDown1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Name = "AngleForm";
            Text = "AngleForm";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private Label label2;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
    }
}