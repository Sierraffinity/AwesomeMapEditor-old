using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Alltext_editor
{
    public partial class Form1 : Form
    {
        Stream stream;
        string[] lines;
        string stopper = "[X]";

        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.CheckFileExists = true;
            open.CheckPathExists = true;
            open.Filter = "Text files|*.txt|All files|*";
            open.Title = "Open an alltext file.";
            open.ShowDialog();
            if (open.FileNames.Length < 1) 
                return;
            stream = open.OpenFile();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadText(int value)
        {
            StreamReader sr = new StreamReader(stream);
            
            for (int i = 0; i < value; i++)
            {
                
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ReadTillChars(StreamReader sr, char[] end)
        {
            Queue<char> chars = new Queue<char>(end.Length);
            while (!sr.EndOfStream || (chars.ToArray() != end))
            {
                if (!(chars.Count < end.Length))
                {
                    chars.Dequeue();
                }
                sr.Read();
                {
                    char[] temp = new char[end.Length];
                    chars.Enqueue();
                }
            }
        }


    }
}
