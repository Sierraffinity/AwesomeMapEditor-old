using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mappy_Map2D
{    
    public struct Tile
    {
        public int Id { get; set; }
        public bool Walkable { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        internal Vector2 Position { get; set; }

        private Rectangle DestRect
        {
            get
            {
                rect.X = (int)this.Position.X;
                rect.Y = (int)this.Position.Y;
                rect.Width = this.Width;
                rect.Height = this.Height;

                return rect;
            }
        }
        private static Rectangle rect;

        public static bool Intersects(Rectangle objDestRect, Tile a)
        {
            if (objDestRect.Intersects(a.DestRect))
            {
                return true;
            }
            
            return false;
        }

        public static bool Contains(Vector2 point, Tile a)
        {
            return a.DestRect.Contains((int)point.X, (int)point.Y);
        }

        public override string ToString()
        {
            string text = "";

            text = string.Format(@"Tile Informations:" +
"\nId: {0}\n" +
"Walkable: {1}\n" +
"Position: {2}\n", this.Id, this.Walkable, this.Position);

            return text;
        }        
    }
}
