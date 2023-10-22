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
    public partial class PeakForm : Form
    {
        public int Threshold;
        public PeakForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Threshold = (int)numericUpDown1.Value;
            Close();
        }
    }
}
