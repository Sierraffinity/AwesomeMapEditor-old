using System;
using System.Collections.Generic;

using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GBA_ASM_editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            numericUpDown1.Maximum = 0x2000000;
            numericUpDown2.Maximum = 0x2000000;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.CheckFileExists = true;
            open.ShowDialog();
            if (open.FileNames.Length > 0)
            {
                textBox1.Text = open.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.readFile(textBox1.Text, textBox2.Text, (int)numericUpDown1.Value, checkBox1.Checked);
            MessageBox.Show("Finished.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text))
            {
                BinaryReader br = new BinaryReader(File.Open(textBox1.Text, FileMode.Open));

                uint length = (uint)numericUpDown2.Value;
                if (length == 0)
                    length = (uint)br.BaseStream.Length;

                byte[] data = br.ReadBytes((int)br.BaseStream.Length);
                br.Close();
                string[] text = Thumb.disassemble(data, (uint)numericUpDown1.Value, length);

                StreamWriter sw = new StreamWriter(textBox2.Text, false);
                for (int i = 0; i < text.Length; i++)
                {
                    sw.WriteLine(text[i]);
                }
                sw.Close();
                MessageBox.Show("Finished.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog open = new SaveFileDialog();
            open.CheckFileExists = false;
            open.Filter = "Text files|*.txt";
            open.ShowDialog();
            if (open.FileNames.Length > 0)
            {
                textBox2.Text = open.FileName;
            }
        }
    }
}