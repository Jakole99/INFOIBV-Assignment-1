using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INFOIBV
{
    public partial class Form2 : Form
    {
        public double LowerAngle;
        public double UpperAngle;
        public int MinThreshold;
        public int MinGap;
        public int MinLength;
        public int PeakThreshold;

        private Stack<Questions> questions;

        public Form2(ModeType mode)
        {
            InitializeComponent();
        }
        public void OkButtonClick(object sender, EventArgs e)
        {
           // var question = questions.Pop();

            if (double.TryParse(textBox1.Text, out double number))
            {
                LowerAngle = number;
                Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid number."); }
        }
    }

    internal class Questions
    {
        string text;
        private int? value;

        public Questions(string text)
        {
            this.text = text;
        }

    }
}
