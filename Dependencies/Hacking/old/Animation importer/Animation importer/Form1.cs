using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Nintenlord.Animation_importer
{
    public partial class Form1 : Form
    {
        string inputFile, outputFile;
        int offset, customInputOffset, customOutputOffset, animationNumber ;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = inputFile;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            if (inputFile != null)
            {
                int index = inputFile.LastIndexOf('\\');
                openDialog.FileName = inputFile.Substring(index + 1, inputFile.Length - index - 1);
            }            
            openDialog.Filter = "GBA ROMs|*.gba|All files|*";
            openDialog.Multiselect = false;
            openDialog.ShowDialog();

            if (openDialog.FileNames.Length < 1)
                return;

            inputFile = openDialog.FileName;
            textBox3.Text = inputFile;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            if (outputFile != null)
            {
                int index = outputFile.LastIndexOf('\\');
                openDialog.FileName = outputFile.Substring(index + 1, outputFile.Length - index - 1);
            }
            openDialog.Filter = "GBA ROMs|*.gba|All files|*";
            openDialog.Multiselect = false;
            openDialog.ShowDialog();

            if (openDialog.FileNames.Length < 1)
                return;

            outputFile = openDialog.FileName;
            textBox4.Text = outputFile;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.Text = outputFile;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            customInputOffset = ToDecUInt(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            customOutputOffset = ToDecUInt(textBox2.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.Run(inputFile, outputFile, offset, animationNumber, checkBox1.Checked, checkBox2.Checked, customInputOffset, customOutputOffset);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            offset = ToDecUInt(textBox5.Text);
        }
        private Int32 ToDecUInt(string a)
        {
            int add;
            //if (a[0].ToString() == "0" && a[1].ToString() == "x")
            //{
            //    add = 2;
            //}
            //else if (a[0].ToString() == "$")
            //{
            //    add = 1;
            //}
            //else
            //{
            add = 0;
            //}
            Int64 lenght = a.Length;
            Int64 loop = 0;
            Int32 result = 0;
            while (loop + add < lenght)
            {
                Int64 u = 0;
                string b = Convert.ToString(a[(int)loop + add]);
                if (b == "0") { u = 0; }
                else if (b == "1") { u = 1; }
                else if (b == "2") { u = 2; }
                else if (b == "3") { u = 3; }
                else if (b == "4") { u = 4; }
                else if (b == "5") { u = 5; }
                else if (b == "6") { u = 6; }
                else if (b == "7") { u = 7; }
                else if (b == "8") { u = 8; }
                else if (b == "9") { u = 9; }
                else if (b == "A" || b == "a") { u = 10; }
                else if (b == "B" || b == "b") { u = 11; }
                else if (b == "C" || b == "c") { u = 12; }
                else if (b == "D" || b == "d") { u = 13; }
                else if (b == "E" || b == "e") { u = 14; }
                else if (b == "F" || b == "f") { u = 15; }
                else { Console.Write("Format error."); loop = lenght; result = 0; }
                try
                {
                    result += (int)(u << (int)(4 * (lenght - loop - add - 1)));
                }
                catch (OverflowException)
                {
                    MessageBox.Show("The Value is too large.");
                }
                loop++;
            }
            return result;
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            animationNumber = ToDecUInt(textBox6.Text);
        }

    }
}
