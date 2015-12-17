// Surface Library
// A tiny wrapper for OpenTK.

// Copyright (C) 2015 shadowndacorner

// This file is part of Surface Library.
// Surface Library is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Surface Library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with Surface Library. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;

namespace PGMEBackend
{
    public class Spritesheet : IDisposable
    {
        public int Rows;
        public int Columns;

        public int RowSize;
        public int ColumnSize;

        public static Spritesheet Load(Bitmap TextureBitmap, int width, int height, TextureFilteringMode mode)
        {
            var ret = new Spritesheet();
            ret.Texture = Texture2D.Load(TextureBitmap, mode);
            ret.TileWidth = width;
            ret.TileHeight = height;
            ret.Rows = TextureBitmap.Width / width;
            ret.Columns = TextureBitmap.Height / height;
            ret.RowSize = TextureBitmap.Width;
            ret.ColumnSize = TextureBitmap.Height;
            return ret;
        }

        public static Spritesheet Load(string file, int width, int height, TextureFilteringMode mode)
        {
            if (System.IO.File.Exists(file))
            {
                using (var bmp = new Bitmap(file))
                    return Load(bmp, width, height, mode);
            }
            return null;
        }

        public static Spritesheet Load(Bitmap texturedbitmap, int width, int height)
        {
            return Load(texturedbitmap, width, height, TextureFilteringMode.Linear);
        }

        public static Spritesheet Load(string file, int width, int height)
        {
            return Load(file, width, height, TextureFilteringMode.Linear);
        }

        public Texture2D Texture;

        public int TileWidth = 8;
        public int TileHeight = 8;

        public int Length {
            get {
                return (Texture.Width * Texture.Height) / (TileWidth * TileHeight);
            }
        }

        public void Draw(int sprite, double x, double y, double scale)
        {
            Draw(sprite, x, y, false, false, scale);
        }

        public void Draw(int sprite, double x, double y, double scale, int alpha)
        {
            Draw(sprite, x, y, false, false, scale, alpha);
        }
        public void Draw(int sprite, double x, double y, bool xFlip, bool yFlip, double scale)
        {
            Draw(sprite, x, y, xFlip, yFlip, scale, 255);
        }

        public void Draw(int sprite, double x, double y, bool xFlip, bool yFlip, double scale, int alpha)
        {
            double uvtw = ((double)TileWidth) / ((double)Texture.Width);
            double uvth = ((double)TileHeight) / ((double)Texture.Height);

            int spritey = (sprite / Rows);
            int spritex = sprite - spritey * Rows;

            double uvy = (double)(spritey) * uvth;
            double uvx = (double)(spritex) * uvtw;
            double uvxm = uvx + uvtw;
            double uvym = uvy + uvth;

            if(xFlip)
            {
                double t = uvx;
                uvx = uvxm;
                uvxm = t;
            }

            if (yFlip)
            {
                double t = uvy;
                uvy = uvym;
                uvym = t;
            }

            Surface.DrawTexturedRectUV(Texture, x, y, TileWidth * scale, TileHeight * scale, uvx, uvy, uvxm, uvy, uvxm, uvym, uvx, uvym, Color.FromArgb(alpha, Color.White));
        }

        public void DebugDraw(double scale)
        {
            for (int y = 0; y < Columns; y++)
            {
                for (int x = 0; x < Rows; x++)
                {
                    var w = TileWidth * scale;
                    var h = TileHeight * scale;
                    int i = x + y * Rows;
                    Draw(i, x * w, y * h, scale);
                }
            }
        }

        public void Dispose()
        {
            Texture.Dispose();
            Texture = null;
        }
    }
}
