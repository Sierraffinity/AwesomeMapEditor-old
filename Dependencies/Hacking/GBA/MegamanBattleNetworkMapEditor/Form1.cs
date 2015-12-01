using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using Nintenlord.ROMHacking.GBA;

namespace Nintenlord.MegamanBattleNetworkMapEditor
{
    public partial class Form1 : Form
    {
        Size padding;
        List<GBAROM> openedROMs;

        public Form1()
        {
            openedROMs = new List<GBAROM>();


            InitializeComponent();
            this.Resize += new EventHandler(Form1_Resize);
            this.ResizeRedraw = true;

            tileView1.Tiles = new Bitmap(SelectFile("Select tileset", "PNG files|*.png"));
            int min = Math.Min(tileView1.Tiles.Height, tileView1.Tiles.Width);
            tileView1.TileSize = new Size(min, min);            

            BinaryReader reader = new BinaryReader(File.OpenRead(SelectFile("Select MAR file", "MAR files|*.mar")));
            List<int> map = new List<int>();
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                map.Add((reader.ReadUInt16() >> 5) - 1);
            }

            this.mapEditor1.Map = map.ToArray();
            map.Clear();
            this.mapEditor1.MapSize = new Size(25, 26);
            this.mapEditor1.TileView = tileView1;

            padding = this.Size - this.panel2.Size - this.panel1.Size;
        }

        string SelectFile(string title, string filter)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = title;
            open.CheckFileExists = true;
            open.CheckPathExists = true;
            open.Filter = filter;
            DialogResult result = open.ShowDialog();
            if (result == DialogResult.Yes || result == DialogResult.OK)
                return open.FileName;
            else
                return null;
        }

        void Form1_Resize(object sender, EventArgs e)
        {
            Size availableSize = this.Size - padding;
            this.panel2.Size = new Size(availableSize.Width / 2, availableSize.Height);
            this.panel1.Size = new Size(availableSize.Width / 2, availableSize.Height);
            this.Refresh();
        }
    }
}
