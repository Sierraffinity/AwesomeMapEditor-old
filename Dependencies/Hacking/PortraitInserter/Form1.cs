using System;
using System.Collections.Generic;

using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PortraitInserter.Portraits;
using Nintenlord.Utility;
using Nintenlord.Forms.Utility;

namespace PortraitInserter
{
    public partial class Form1 : Form
    {
        Color[] palette;
        public Program Program
        {
            get;
            set;
        }
        public int MaxIndex
        {
            set
            {
                portraitIndex.Maximum = value;
            }
        }
        readonly Size paletteBoxSize = new Size(16, 16);

        public Form1()
        {
            InitializeComponent();
            palette = new Color[16];
            for (int i = 0; i < palette.Length; i++)
            {
                palette[i] = Color.FromArgb(i * 16, i * 16, i * 16);
            }
            pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);            
        }

        void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = new Rectangle(Point.Empty, new Size(1, 1));
            for (int y = e.ClipRectangle.Top; y < e.ClipRectangle.Bottom; y++)
            {
                for (int x = e.ClipRectangle.Left; x < e.ClipRectangle.Right; x++)
                {
                    int palIndex = x / 16;
                    if (y > 16)
                    {
                        palIndex += 8;
                    }
                    rect.Location = new Point(x, y);
                    Brush brush = new SolidBrush(palette[palIndex]);
                    e.Graphics.FillRectangle(brush, rect);
                }
            }
        }

        private void portraitIndex_ValueChanged(object sender, EventArgs e)
        {
            LoadPortrait();
        }

        private void LoadPortrait()
        {
            Program.CurrentIndex = (int)portraitIndex.Value;
            var result = Program.LoadPortrait();
            if (result.CausedError)
            {
                MessageBox.Show(result.ErrorMessage);
            }
            else if (result.Result.Item1 != null)
            {
                portraitPictureBox.Image = result.Result.Item1;
                InsertPalette(result.Result.Item1.Palette.Entries);
                eyesClosedCheckBox.Checked = EyeControl.Closed == result.Result.Item3;
                eyeXnumericUpDown.Value = result.Result.Item2.X;
                eyeYnumericUpDown.Value = result.Result.Item2.Y;
                mouthXnumericUpDown.Value = result.Result.Item4.X;
                mouthXnumericUpDown.Value = result.Result.Item4.Y;
            }
        }

        private void InsertPalette(Color[] pal)
        {
            for (int i = 0; i < palette.Length; i++)
            {
                palette[i] = pal[i];
            }
            pictureBox1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadPortrait();
        }

        private void eyesClosedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Program.InsertEyeControl(eyesClosedCheckBox.Checked ? 
                EyeControl.Closed : EyeControl.Open);
        }

        private void savePortraitButton_Click(object sender, EventArgs e)
        {
            if (savePortraitDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                portraitPictureBox.Image.Save(savePortraitDialog.FileName, 
                    BitmapHelpers.GetFormat(savePortraitDialog.FileName));
            }
        }

        private void loadPortraitButton_Click(object sender, EventArgs e)
        {
            if (openPortraitDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap bitmap = Bitmap.FromFile(openPortraitDialog.FileName) as Bitmap;
                if (bitmap != null)
                {
                    Program.InsertBitmap(bitmap);
                }
                else
                {
                    MessageBox.Show("Portrait failed to load.");
                }
            }
        }

        private void newPortraitButton_Click(object sender, EventArgs e)
        {
            if (openPortraitDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap bitmap = Bitmap.FromFile(openPortraitDialog.FileName) as Bitmap;
                if (bitmap != null)
                {
                    Program.AddNewPortrait(bitmap);
                }
                else
                {
                    MessageBox.Show("Portrait failed to load.");
                }
            }
        }

        private void copyToNewPortrait_Click(object sender, EventArgs e)
        {
            Program.AddNewPortraitFromCurrent();
        }


        private void eyeXnumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Program.InsertEyePosition(new Point((int)eyeXnumericUpDown.Value, (int)eyeYnumericUpDown.Value));
        }

        private void eyeYnumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Program.InsertEyePosition(new Point((int)eyeXnumericUpDown.Value, (int)eyeYnumericUpDown.Value));
        }


        private void mouthXnumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Program.InsertMouthPosition(new Point((int)mouthXnumericUpDown.Value, (int)mouthYnumericUpDown.Value));
        }

        private void mouthYnumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Program.InsertMouthPosition(new Point((int)mouthXnumericUpDown.Value, (int)mouthYnumericUpDown.Value));
        }
    }
}
