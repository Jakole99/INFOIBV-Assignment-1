using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INFOIBV.InputForms
{
    public partial class AngleForm : Form
    {
        public double LowerAngle;
        public double UpperAngle;

        public AngleForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpperAngle = (double)numericUpDown1.Value;
            LowerAngle = (double)numericUpDown2.Value;
            Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
