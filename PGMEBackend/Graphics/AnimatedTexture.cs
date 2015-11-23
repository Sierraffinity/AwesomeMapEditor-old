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

namespace PGMEBackend
{
    public class AnimatedTexture
    {
        public int CurrentFrame = 0;
        public float Framerate = 30;
        public int Width;
        public int Height;
        private DateTime lastUpdate = DateTime.Now;
        public Spritesheet Sheet;

        public void FrameAdvance(int num)
        {
            //Console.WriteLine("Advancing " + num + " frames");
            while (num > 0)
            {
                CurrentFrame++;
                while (CurrentFrame >= Sheet.Length)
                    CurrentFrame -= Sheet.Length;
                num--;
            }
        }

        public static AnimatedTexture Load(Bitmap bmp, int width, int height, TextureFilteringMode mode)
        {
            var sheet = Spritesheet.Load(bmp, width, height, mode);
            if (sheet != null)
            {
                var anim = new AnimatedTexture();
                anim.Width = width;
                anim.Height = height;
                anim.Sheet = sheet;
                return anim;
            }
            return null;
        }

        public static AnimatedTexture Load(string path, int width, int height, TextureFilteringMode mode)
        {
            var sheet = Spritesheet.Load(path, width, height, mode);
            if (sheet != null)
            {
                var anim = new AnimatedTexture();
                anim.Width = width;
                anim.Height = height;
                anim.Sheet = sheet;
                return anim;
            }
            return null;
        }

        public static AnimatedTexture Load(Bitmap bmp, int width, int height)
        {
            return Load(bmp, width, height, TextureFilteringMode.Linear);
        }

        public static AnimatedTexture Load(string path, int width, int height)
        {
            return Load(path, width, height, TextureFilteringMode.Linear);
        }

        public void Draw(double x, double y, double scale)
        {
            if ((DateTime.Now - lastUpdate).TotalSeconds > 1 / Framerate)
            {
                FrameAdvance((int)Math.Round((DateTime.Now - lastUpdate).TotalSeconds * Framerate));
                lastUpdate = DateTime.Now;
            }
            Sheet.Draw(CurrentFrame, x, y, scale);
            //Sheet.Textures[CurrentFrame].Draw(x, y, scale);
        }
    }
}
