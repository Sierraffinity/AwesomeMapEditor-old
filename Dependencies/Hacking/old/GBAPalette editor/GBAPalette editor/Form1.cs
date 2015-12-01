using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int offset, selectedColor;
        Size BlockSize = new Size(16, 16);
        int ColorHeight = 16;
        int ColorWidht = 16;
        Color[] colors;
        ushort[] data;
        Stream stream;
        Size PBsize;
        int amountOfColours
        {
            get { return ColorWidht * ColorHeight; }
        }

        public Form1()
        {
            offset = 0;
            selectedColor = 0;
            InitializeComponent();
            MinimumSize = this.Size;
            PBsize = pictureBox1.Size;
            pictureBox1.Image = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            this.Resize += new EventHandler(resize);
        }        

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {

        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {            
            if (colors != null)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                selectedColor = (me.X * ColorWidht / pictureBox1.Width) + (me.Y * ColorWidht / pictureBox1.Height) * ColorWidht;
                UpdateControls();
            }            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                offset = (int)numericUpDown1.Value;
                loadColor();
            }
            catch (FormatException)
            {
                return;
            }
            
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.CheckFileExists = true;
            open.Multiselect = false;
            open.Filter = "GBA ROMs|*.gba|SNES ROMs|*.smc|All files|*";
            open.Title = "Select a ROM to edit.";
            open.ShowDialog();
            if (open.FileNames.Length < 1)
                return;

            stream = open.OpenFile();
            numericUpDown1.Maximum = stream.Length - 2;
            loadColor();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loadColor()
        {
            colors = new Color[amountOfColours];
            data = new ushort[amountOfColours];
            stream.Position = offset;
            BinaryReader br = new BinaryReader(stream);
            for (int i = 0; i < colors.Length; i++)
            {
                if (br.BaseStream.Position + 1 < br.BaseStream.Length)
                {
                    ushort value = br.ReadUInt16();
                    colors[i] = Nintenlord.GBA.Graphics.GBAColor(value);
                    data[i] = value;
                }
                else
                {
                    colors[i] = this.BackColor;
                    data[i] = 0;
                }
            }
            DrawPictureBox();
        }

        private void UpdateControls()
        {
            Color selectedColor = colors[this.selectedColor];
            byte Red = (byte)(selectedColor.R / 8);
            byte Green = (byte)(selectedColor.G / 8);
            byte Blue = (byte)(selectedColor.B / 8);
            trackBar1.Value = Red;
            trackBar2.Value = Green;
            trackBar3.Value = Blue;
            textBox1.Text = Convert.ToString(Red);
            textBox2.Text = Convert.ToString(Green);
            textBox3.Text = Convert.ToString(Blue);
            textBox4.Text = Convert.ToString(Red*8);
            textBox5.Text = Convert.ToString(Green*8);
            textBox6.Text = Convert.ToString(Blue*8);
        }

        private void UpdateColors()
        {

        }

        private void DrawPictureBox()
        {
            Bitmap bitmap = new Bitmap(pictureBox1.Image);
            ColorHeight = bitmap.Height / BlockSize.Height;
            ColorWidht = bitmap.Width / BlockSize.Width;
            for (int i = 0; i < amountOfColours; i++)
            {
                Point corner;
                {
                    int x = (i % ColorWidht) * BlockSize.Width;
                    int y = (i / ColorWidht) * BlockSize.Height;

                    corner = new Point(x, y);
                }

                if (bitmap.GetPixel(corner.X, corner.Y) != colors[i])
                {
                    for (int u = 0; u < BlockSize.Width * BlockSize.Width; u++)
                    {
                        int x = corner.X + (u % BlockSize.Width);
                        int y = corner.Y + (u / BlockSize.Height);

                        bitmap.SetPixel(x, y, colors[i]);
                    }
                }
            }

            pictureBox1.Image = bitmap;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void resize(object sender, EventArgs e)
        {
            //if (colors != null)
            //{
            //    this.pictureBox1.Size = this.Size - this.MinimumSize + PBsize;
            //    loadColor();
            //    DrawPictureBox(); 
            //}
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                numericUpDown1.Increment = 1;
            }
            else 
            {
                numericUpDown1.Increment = 2;
            }
        }
    }
}
