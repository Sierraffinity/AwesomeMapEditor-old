using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1(int maxValue)
        {
            InitializeComponent();
            this.numericUpDown1.Maximum = maxValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.offset = (int)numericUpDown1.Value;
            this.Close();
        }
    }
}
