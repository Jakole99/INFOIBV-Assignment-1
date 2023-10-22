namespace INFOIBV
{
    partial class Form2
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

            questions.Push(new Questions("What is the lower angle value?"));

            okButton = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // okButton
            // 
            okButton.Location = new Point(168, 206);
            okButton.Name = "OK";
            okButton.Size = new Size(94, 29);
            okButton.TabIndex = 0;
            okButton.Text = "okButton";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += OkButtonClick;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(151, 91);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(95, 96);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 2;
            label1.Text = "label1";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(426, 261);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(okButton);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button okButton;
        private TextBox textBox1;
        private Label label1;
    }
}