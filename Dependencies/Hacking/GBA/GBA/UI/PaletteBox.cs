using System.Drawing;
using System.Windows.Forms;

namespace Nintenlord.GBA_Graphics_Editor.Forms.Controls
{
    public partial class PaletteBox : UserControl
    {
        private Size tileSize;

        public Size TileSize
        {
            get { return tileSize; }
        }
        public Size AmountOfTiles
        {
            get 
            {
                return new Size(
                    this.Width / TileSize.Width,
                    this.Height / TileSize.Height);
            }
        }

        private Color[] colorsToDraw;
        public Color[] ColorsToDraw
        {
            get { return colorsToDraw; }
            set 
            {
                if (colorsToDraw != value)
                {
                    colorsToDraw = value;
                    this.OnPaint(
                        new PaintEventArgs(
                            CreateGraphics(),
                            new Rectangle(new Point(), this.Size)));
                }
            }
        }

        public PaletteBox()
        {
            InitializeComponent();
        }

        public void SetColor(int index, Color color)
        {
            ColorsToDraw[index] = color;
            this.OnPaint(
                new PaintEventArgs(
                    CreateGraphics(),
                    new Rectangle(
                        new Point(index % AmountOfTiles.Width, index / AmountOfTiles.Width), 
                        TileSize)));
        }

        public Color GetColor(int index)
        {
            return ColorsToDraw[index];
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Rectangle rect = e.ClipRectangle;
            int tilesVert, tilesHori;
            tilesHori = this.Width / TileSize.Width;
            tilesVert = this.Height / TileSize.Height;

            if (rect.Bottom % TileSize.Height != 0)
            {
                rect.Height += TileSize.Height - rect.Bottom % TileSize.Height;
            }
            if (rect.Right % TileSize.Width != 0)
            {
                rect.Width += TileSize.Width - rect.Right % TileSize.Width;
            }

            if (rect.Y % TileSize.Height != 0)
            {
                rect.Height += TileSize.Height - rect.Y % TileSize.Height;
                rect.Y -= rect.Y % TileSize.Height;
            }
            if (rect.X % TileSize.Width != 0)
            {
                rect.Width += TileSize.Width - rect.X % TileSize.Width;
                rect.X -= rect.X % TileSize.Width;
            }

            Rectangle tilesToDraw = new Rectangle();
            tilesToDraw.X = rect.X / TileSize.Width;
            tilesToDraw.Y = rect.Y / TileSize.Height;
            tilesToDraw.Width = rect.Width / TileSize.Width;
            tilesToDraw.Height = rect.Height / TileSize.Height;

            Rectangle drawRect = new Rectangle(new Point(), TileSize);
            for (int y = tilesToDraw.Y; y < tilesToDraw.Bottom; y++)
            {
                drawRect.Y = y * TileSize.Height;
                for (int x = tilesToDraw.X; x < tilesToDraw.Right; x++)
                {
                    drawRect.X = x * TileSize.Width;
                    Color color = ColorsToDraw[x + y * this.AmountOfTiles.Width];
                    SolidBrush brush = new SolidBrush(color);
                    graphics.FillRectangle(brush, drawRect);
                }
            }

            //if (palette.Length > 0)
            //{
            //    Color antiColor = Color.FromArgb(0xFF - currentColor.R, 0xFF - currentColor.G, 0xFF - currentColor.B);
            //    Pen pen = new Pen(antiColor);
            //    Rectangle rect2 = new Rectangle();
            //    rect2.X = (int)(numericUpDown1.Value % 16) * 8 - 1;
            //    rect2.Y = (int)(numericUpDown1.Value / 16) * 8 - 1;
            //    rect2.Height = widht + 1;
            //    rect2.Width = widht + 1;
            //    e.Graphics.DrawRectangle(pen, rect2);
            //}
            //e.Graphics.EndContainer(container);

            base.OnPaint(e);
        }
    }
}
