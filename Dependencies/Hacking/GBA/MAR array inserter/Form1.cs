using System;
using System.Collections.Generic;

using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Nintenlord.MAR_array_inserter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            numericUpDown3.Maximum = 0x2000000;
            numericUpDown1.Maximum = 0x2000000;
            this.MinimumSize = this.Size;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MarArrayDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = MarArrayDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ROMFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = ROMFileDialog.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                Program.Run(textBox1.Text, textBox2.Text, 
                    (int)numericUpDown3.Value,
                    new int[2] { (int)WidthNumericUpDown.Value, 
                                 (int)HeightNumericUpDown.Value }, 
                    checkBox1.Checked, 
                    (int)numericUpDown1.Value);
            }
            else
            {
                MessageBox.Show("Please select both ROM and MAR file before proceeding.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.numericUpDown1.Enabled = checkBox1.Checked;
        }
    }
}