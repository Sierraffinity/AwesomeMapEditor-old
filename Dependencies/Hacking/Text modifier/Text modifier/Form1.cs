using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        string originalFile, outputFile;
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Nighmre_to_C_array(originalFile, outputFile);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Multiselect = false;
            openDialog.ShowDialog();
            if (openDialog.FileNames.Length < 1)
                return;
            originalFile = openDialog.FileName;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Multiselect = false;
            openDialog.CheckFileExists = false;
            openDialog.ShowDialog();
            if (openDialog.FileNames.Length < 1)
                return;
            outputFile = openDialog.FileName;
        }
    }
}