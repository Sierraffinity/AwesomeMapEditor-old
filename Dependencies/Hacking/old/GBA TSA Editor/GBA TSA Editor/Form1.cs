using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Nintenlord.GBA.Compressions;

namespace GBA_TSA_Editor
{
    public unsafe partial class Form1 : Form
    {
        public byte[] RawGBAGraphics;
        public byte[] RawPalette;
        public ushort[] RawTSA;
        private byte[] ROM;
        Size TSAsize;
        Bitmap Screen;
        const int bytesPerTile = 32;
        const int defaulsGraphicsLenght = 0x8000;
        const int defaultPaletteLenght = 0x100;

        public Form1()
        {
            InitializeComponent();
            OpenROM();
        }

        void LoadTSA()
        {
            if (TSAsize.Height == 0 || TSAsize.Width == 0)
                return;

            fixed (byte* ROMpointer = &ROM[0])
            {
                if (radioButton5.Checked)
                {
                    RawTSA = dataUshort(ROMpointer + (int)numericUpDown3.Value, TSAsize.Height * TSAsize.Width);
                }
                else if (radioButton6.Checked)
                {
                    RawTSA = compressedUshortData(ROMpointer + (int)numericUpDown3.Value);
                }
                else
                {
                    //throw new Exception("Incorrect operation");
                }
            }
        }

        void LoadGraphics()
        {
            fixed (byte* ROMpointer = &ROM[0])
            {
                if (radioButton1.Checked)
                {
                    RawGBAGraphics = data(ROMpointer + (int)numericUpDown1.Value, defaulsGraphicsLenght);
                }
                else if (radioButton2.Checked)
                {
                    RawGBAGraphics = compressedData(ROMpointer + (int)numericUpDown1.Value);
                }
                else
                {
                    //throw new Exception("Incorrect operation");
                }
            }
        }

        void LoadPalette()
        {
            fixed (byte* ROMpointer = &ROM[0])
            {
                if (radioButton3.Checked)
                {
                    RawPalette = data(ROMpointer + (int)numericUpDown2.Value, defaultPaletteLenght);
                }
                else if (radioButton4.Checked)
                {
                    RawPalette = compressedData(ROMpointer + (int)numericUpDown2.Value);
                }
                else
                {
                    //throw new Exception("Incorrect operation");
                }
            }
        }


        byte[] data(byte* source, int lenght)
        {
            byte[] result = new byte[lenght];

            int* sourceIntPtr = (int*)source;
            fixed (byte* resultPtr = &result[0])
            {
                int* resultIntPtr = (int*)resultPtr;

                for (int i = 0; i < lenght; i += 4)
                {
                    *(resultIntPtr++) = *(sourceIntPtr++);
                }
            }

            return result;
        }

        ushort[] dataUshort(byte* source, int lenght)
        {
            ushort[] result = new ushort[lenght / 2];

            int* sourceIntPtr = (int*)source;
            fixed (ushort* resultPtr = &result[0])
            {
                int* resultIntPtr = (int*)resultPtr;

                for (int i = 0; i < lenght; i += 4)
                {
                    *(resultIntPtr++) = *(sourceIntPtr++);
                }
            }

            return result;
        }

        byte[] compressedData(byte* pointer)
        {
            int* intPtr = (int*)pointer;
            int size = (*intPtr) >> 8;
            byte[] result = new byte[size];
            fixed (byte* destination = &result[0])
            {
                LZ77.UnCompress(pointer, destination);
            }
            return result;
        }

        ushort[] compressedUshortData(byte* pointer)
        {
            int* intPtr = (int*)pointer;
            int size = *pointer >> 8;
            ushort[] result = new ushort[size / 2];
            fixed (ushort* destination = &result[0])
            {
                LZ77.UnCompress(pointer, (byte*)destination);
            }
            return result;
        }


        private void OpenROM()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "ROM to open.";
            open.Multiselect = false;
            open.CheckFileExists = true;
            open.ShowDialog();
            if (open.FileNames.Length < 1)
                return;
            BinaryReader br = new BinaryReader(open.OpenFile());
            ROM = br.ReadBytes((int)br.BaseStream.Length);
            numericUpDown1.Maximum = (decimal)br.BaseStream.Length;
            numericUpDown2.Maximum = (decimal)br.BaseStream.Length;
            numericUpDown3.Maximum = (decimal)br.BaseStream.Length;
            br.Close();
        }

        private void MakeScreen()
        {
            if (RawGBAGraphics == null || RawPalette == null || RawTSA == null || TSAsize == null)
            {
                //MessageBox.Show("All data hasn't neen loaded.");
                return;
            }

            Screen = new Bitmap(TSAsize.Width, TSAsize.Height, PixelFormat.Format32bppRgb);
            BitmapData bmd = Screen.LockBits(new Rectangle(new Point(), Screen.Size), ImageLockMode.ReadWrite, Screen.PixelFormat);

            int[] rawScreen = new int[RawTSA.Length];

            for (int i = 0; i < RawTSA.Length; i++)
            {
                Bitmap tile = makeRawTile(i);
                BitmapData tileBmd = tile.LockBits(new Rectangle(new Point(), tile.Size), ImageLockMode.ReadOnly, tile.PixelFormat);

                for (int x = 0; x < tile.Width; x++)
                {
                    for (int y = 0; y < tile.Height; y++)
                    {
                         rawScreen[i * 64 * 4 + y * 8 + x] = *((int*)tileBmd.Scan0 + y * tileBmd.Stride + x * 4);
                    }
                }

                tile.UnlockBits(tileBmd);
            }

            for (int i = 0; i < rawScreen.Length; i++)
            {
                *((int*)bmd.Scan0 + i) = rawScreen[i];
            }

            Screen.UnlockBits(bmd);
            pictureBox1.Image = Screen;
        }

        private Bitmap makeRawTile(int index)
        {
            Color[] palette;int empty; Bitmap tileRaw;

            fixed (byte* pointer = &RawPalette[0])
            {
                palette = Nintenlord.GBA.GBAGraphics.GBAPalette((ushort*)(pointer + 32 * (RawTSA[index] >> 12)), 16);
            }
            
            fixed (byte* pointer = &RawGBAGraphics[0])
            {
                tileRaw = Nintenlord.GBA.GBAGraphics.ToBitmap(pointer + bytesPerTile * (RawTSA[index] & 0x3FF), bytesPerTile, 1, palette, out empty, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }
            if (empty != 0)
            {
                throw new Exception("The amount of empty tiles is not 0.");
            }
            RotateFlipType rft = new RotateFlipType();
            bool vertical = (((RawTSA[index] >> 11) & 1) != 0);
            bool horizontical = ((RawTSA[index] >> 10) & 1) != 0;

            if (vertical && horizontical)
                rft = RotateFlipType.RotateNoneFlipXY;
            else if (vertical)
                rft = RotateFlipType.RotateNoneFlipY;
            else if (horizontical)
                rft = RotateFlipType.RotateNoneFlipX;
            else
                rft = RotateFlipType.RotateNoneFlipNone;

            tileRaw.RotateFlip(rft);

            return tileRaw;
        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            LoadGraphics();
            MakeScreen();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            LoadPalette();
            MakeScreen();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            LoadTSA();
            MakeScreen();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            LoadGraphics();
            MakeScreen();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            LoadGraphics();
            MakeScreen();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            LoadPalette();
            MakeScreen();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            LoadPalette();
            MakeScreen();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            LoadTSA();
            MakeScreen();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            LoadTSA();
            MakeScreen();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown4.Enabled = !checkBox1.Checked;
            numericUpDown5.Enabled = !checkBox1.Checked;
            LoadTSA();
            if (checkBox1.Checked)
            {
                numericUpDown4.Value = TSAsize.Width;
                numericUpDown5.Value = TSAsize.Height;
            }
            MakeScreen();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            LoadTSA();
            MakeScreen();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            LoadTSA();
            MakeScreen();
        }
    }
}
