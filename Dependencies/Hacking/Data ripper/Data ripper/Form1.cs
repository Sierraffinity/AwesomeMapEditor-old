using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Data_ripper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "ROM file|*.gba";
            open.CheckFileExists = true;
            open.ShowDialog();
            if (open.FileNames.Length > 0)
            {
                textBox1.Text = open.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Nightmare module|*.nmm";
            open.CheckFileExists = true;
            open.ShowDialog();
            if (open.FileNames.Length > 0)
            {
                textBox2.Text = open.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text) && File.Exists(textBox2.Text))
            {
                NightmareModule nightmare = new NightmareModule(textBox2.Text);

                BinaryReader br = new BinaryReader(File.Open(textBox1.Text, FileMode.Open));
                byte[] ROM = br.ReadBytes((int)br.BaseStream.Length);
                br.Close();

                string data = nightmare.GetData(ROM);

                using (StreamWriter sw = new StreamWriter(textBox3.Text))
                {
                    sw.Write(data);                    
                }
                MessageBox.Show("Finished");
            }
            else
            {
                MessageBox.Show("A file path was invalid");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Text File|*.txt";
            open.CheckFileExists = false;
            open.AddExtension = true;
            open.ShowDialog();
            if (open.FileNames.Length > 0)
            {
                textBox3.Text = open.FileName;
            }
        }
    }
}
