using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Nintenlord.MegamanBattleNetworkMapEditor
{
    public partial class MapEditor : UserControl
    {
        private TileView tileView;
        public TileView TileView
        {
            get { return tileView; }
            set { tileView = value; UpdateSize(); }
        }
        private int[] map;
        public int[] Map
        {
            get { return map; }
            set { map = value; }
        }
        private Size mapSize;
        public Size MapSize
        {
            get { return mapSize; }
            set 
            {
                mapSize = value; 
                UpdateSize();
            }
        }
        private Size RealMapSize
        {
            get
            {
                return new Size(mapSize.Width * tileView.TileSize.Width, mapSize.Height * tileView.TileSize.Height);
            }
        }

        public MapEditor()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(MapEditor_Paint);
        }

        void MapEditor_Paint(object sender, PaintEventArgs e)
        {
            if (tileView != null && map != null && !mapSize.IsEmpty)
            {
                tileView.DrawMap(e.Graphics, e.ClipRectangle, 2, map, mapSize);
            }
        }

        void UpdateSize()
        {
            if (tileView != null && !tileView.TileSize.IsEmpty && !mapSize.IsEmpty)
            {
                this.Size = RealMapSize;
            }
        }
    }
}
