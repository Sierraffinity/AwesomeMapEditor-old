using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Fire_Emblem_Map_Editor 
{
    public partial class Form1 : Form
    {
        String originalFile, outputFile;
        int offset, size;
        bool Compress;

        public Form1()
        {
            size = 0;
            offset = 0;
            InitializeComponent();
            radioButton1.Checked = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Multiselect = false;
            openDialog.ShowDialog();
            if (openDialog.FileNames.Length < 1)
                return;
            originalFile = openDialog.FileName;
            textBox1.Text = originalFile;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.CheckFileExists = false;
            openDialog.Multiselect = false;
            openDialog.ShowDialog();
            if (openDialog.FileNames.Length < 1)
                return;
            outputFile = openDialog.FileName;
            textBox2.Text = outputFile;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            outputFile = textBox2.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            originalFile = textBox1.Text;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Compress = radioButton1.Checked;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Compress = !(radioButton2.Checked);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.test(originalFile, outputFile, offset, size, Compress);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            offset = (int)Program.ToDecUInt(textBox3.Text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            size = (int)Program.ToDecUInt(textBox4.Text);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}