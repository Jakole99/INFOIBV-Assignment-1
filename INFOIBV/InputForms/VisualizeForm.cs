using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INFOIBV.InputForms;

public partial class VisualizeForm : Form
{
    public int MinThreshold;
    public int MinLength;
    public int MaxGap;

    public VisualizeForm()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        MinThreshold = (int)numericUpDown1.Value;
        MinLength = (int)numericUpDown2.Value;
        MaxGap = (int)numericUpDown3.Value;
        Close();
    }
}