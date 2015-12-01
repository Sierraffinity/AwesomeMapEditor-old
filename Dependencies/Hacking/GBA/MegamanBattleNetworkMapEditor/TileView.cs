using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Nintenlord.MegamanBattleNetworkMapEditor
{
    [Flags]
    public enum TileOrder
    {
        LeftToRight = 0,
        TopToBottom = 0,
        RightToLeft = 1,
        BottomToTop = 2,
        RightToLeftAndBottomToTop = 3
    }

    public partial class TileView : UserControl
    {
        private Bitmap tiles;
        private TextureBrush texBrush;
        private Size tileSize;
        private Size amountOfTiles;

        private TileOrder orderOfTiles = TileOrder.LeftToRight | TileOrder.TopToBottom;
        private TileOrder orderToDraw = TileOrder.LeftToRight | TileOrder.TopToBottom;
        private float tileScale = 1;
        private Stack<Matrix> matrixStack = new Stack<Matrix>(10);

        public Bitmap Tiles
        {
            get { return tiles; }
            set 
            { 
                tiles = value;
                if (value != null)
                {
                    texBrush = new TextureBrush(value);
                }
                UpdateAmountOfTiles();
            }
        }
        public Size TileSize
        {
            get { return tileSize; }
            set 
            {
                tileSize = value;
                UpdateAmountOfTiles();
            }
        }
        public Size AmountOfTiles
        {
            get { return amountOfTiles; }
        }
        public TileOrder OrderOfTiles
        {
            get { return orderOfTiles; }
            set { orderOfTiles = value; }
        }
        public TileOrder OrderToDraw
        {
            get { return orderToDraw; }
            set { orderToDraw = value; }
        }
        public float TileScale
        {
            get { return tileScale; }
            set { tileScale = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public TileView()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(TileView_Paint);
            this.Resize += new EventHandler(TileView_Resize);
            this.ResizeRedraw = true;            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tiles"></param>
        /// <param name="tileSize"></param>
        public TileView(Bitmap tiles, Size tileSize)
        {
            this.tileSize = tileSize;
            this.Tiles = tiles;

            InitializeComponent();
            this.Paint += new PaintEventHandler(TileView_Paint);
            this.Resize += new EventHandler(TileView_Resize);
            this.ResizeRedraw = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TileView_Resize(object sender, EventArgs e)
        {
            UpdateAmountOfTiles();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void TileView_Paint(object sender, PaintEventArgs e)
        {
            if (!(tileSize.IsEmpty || (tiles == null) || amountOfTiles.IsEmpty))
            {
                texBrush.WrapMode = WrapMode.Tile;
                matrixStack.Push(texBrush.Transform);
                Graphics graphics = e.Graphics;
                Rectangle toPaint = new Rectangle(new Point(), this.Size);
                Rectangle destRect = new Rectangle(new Point(), tileSize);
                PointF source = new PointF();

                int destDx, destDy;
                float srcDx, srcDy;
                destDx = tileSize.Width;
                destDy = tileSize.Height;
                if (amountOfTiles.Width == 1)
                {
                    srcDx = 0;
                    srcDy = tileSize.Height;
                }
                else if (amountOfTiles.Height == 1)
                {
                    srcDx = tileSize.Width;
                    srcDy = 0;
                }
                else throw new NotImplementedException("Multi row/column tilesets not supported.");

                int tile = amountOfTiles.Height * amountOfTiles.Width;

                int blitWidth = toPaint.Width / destDx;
                int blitHeigth = toPaint.Height / destDy;

                for (int y = 0; y < blitHeigth; y++)
                {
                    for (int x = 0; x < blitWidth; x++)
                    {
                        texBrush.Transform = matrixStack.Peek();
                        texBrush.TranslateTransform(source.X, source.Y);
                        graphics.FillRectangle(texBrush, destRect);
                        destRect.X += destDx;
                        source.X -= srcDx; //-= due to way matrix works
                        source.Y -= srcDy;
                        if (--tile == 0)
                        {
                            goto label1;
                        }
                    }
                    source.X += srcDx; //don't ask
                    source.Y += srcDy; //seriously, don't ask
                    destRect.X = 0;
                    destRect.Y += destDy;
                }
            label1:
                texBrush.Transform = matrixStack.Pop();
            }
        }

        protected bool CheckTileSize()
        {
            return (tiles.Width % tileSize.Width == 0) && (tiles.Height % tileSize.Height == 0);
        }

        protected void UpdateAmountOfTiles()
        {
            if (!(tileSize.IsEmpty || (tiles == null)))
                amountOfTiles = new Size(tiles.Width / tileSize.Width, tiles.Height / tileSize.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="toDrawTo"></param>
        /// <param name="scale"></param>
        /// <param name="tiles"></param>
        /// <param name="mapSize"></param>
        public void DrawMap(Graphics graphics, Rectangle toDrawTo, float scale, int[] tiles, Size mapSize)
        {
            if (tiles.Length < mapSize.Height * mapSize.Width)
                throw new ArgumentException("Map size doesn't match the amount of tiles.");
            
            matrixStack.Push(texBrush.Transform);

            if (mapSize.Width <= 0)
                mapSize.Width = (toDrawTo.Width + toDrawTo.X) / tileSize.Width;
            if (mapSize.Height <= 0)
                mapSize.Height = (toDrawTo.Height + toDrawTo.Y) / tileSize.Height;

            unsafe
            {
                fixed (int* pointer = &tiles[0])
                {
                    DrawMap(graphics, toDrawTo, scale, pointer, mapSize);
                }
            }

            texBrush.Transform = matrixStack.Pop();
        }

        private unsafe void DrawMap(Graphics graphics, Rectangle toDrawTo, float scale, int* tiles, Size mapSize)
        {
            Rectangle tilesToDraw = GetDrawingRectangle(ref toDrawTo, ref mapSize);

            //texBrush.ScaleTransform(scale, scale);
            matrixStack.Push(texBrush.Transform);
            int tileToDraw;
            Rectangle rect = new Rectangle(Point.Empty, tileSize);
            for (int j = tilesToDraw.Y; j < tilesToDraw.Bottom; j++)
            {
                for (int i = tilesToDraw.X; i < tilesToDraw.Right; i++)
                {
                    texBrush.Transform = matrixStack.Peek();
                    tileToDraw = tiles[i + j * mapSize.Width];

                    texBrush.TranslateTransform(-(tileToDraw % amountOfTiles.Width - i % amountOfTiles.Width) * tileSize.Width,
                                                -(tileToDraw / amountOfTiles.Width - j % amountOfTiles.Height) * tileSize.Height);
                    rect.X = i * tileSize.Width;
                    rect.Y = j * tileSize.Height;
                    graphics.FillRectangle(texBrush, rect);

                }
            }
            matrixStack.Pop();
        }

        private Rectangle GetDrawingRectangle(ref Rectangle toDrawTo, ref Size mapSize)
        {
            Rectangle tilesToDraw = new Rectangle();

            tilesToDraw.X = toDrawTo.X / tileSize.Width;
            tilesToDraw.Width = toDrawTo.Right / tileSize.Width;
            if (toDrawTo.Right % tileSize.Width != 0) tilesToDraw.Width++;
            tilesToDraw.Width -= tilesToDraw.X;
            if (tilesToDraw.Width > mapSize.Width) tilesToDraw.Width = mapSize.Width;

            tilesToDraw.Y = toDrawTo.Y / tileSize.Height;
            tilesToDraw.Height = toDrawTo.Bottom / tileSize.Height;
            if (toDrawTo.Bottom % tileSize.Height != 0) tilesToDraw.Height++;
            tilesToDraw.Height -= tilesToDraw.Y;
            if (tilesToDraw.Height > mapSize.Height) tilesToDraw.Height = mapSize.Height;
            return tilesToDraw;
        }
    }
}
