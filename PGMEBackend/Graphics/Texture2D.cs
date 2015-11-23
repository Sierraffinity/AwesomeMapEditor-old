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
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace PGMEBackend
{
    public enum TextureFilteringMode {
        Nearest,
        Linear
    }

    public class Texture2D
    {
        public static Texture2D Load(BitmapData TextureData, TextureFilteringMode mode)
        {
            int id = 0;
            GL.GenTextures(1, out id);
            GL.BindTexture(TextureTarget.Texture2D, id);

            GL.TexEnv(TextureEnvTarget.TextureEnv,
                    TextureEnvParameter.TextureEnvMode, (float)TextureEnvMode.Modulate);
            switch (mode)
            {
                case TextureFilteringMode.Nearest:
                    GL.TexParameter(TextureTarget.Texture2D,
                        TextureParameterName.TextureMinFilter, (float)TextureMinFilter.NearestMipmapNearest);
                    GL.TexParameter(TextureTarget.Texture2D,
                        TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Nearest);
                    break;
                default:
                    GL.TexParameter(TextureTarget.Texture2D,
                        TextureParameterName.TextureMinFilter, (float)TextureMinFilter.LinearMipmapLinear);
                    GL.TexParameter(TextureTarget.Texture2D,
                        TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
                    break;
            }

            GL.TexParameter(TextureTarget.Texture2D,
                TextureParameterName.GenerateMipmap, (float)1.0f);

            GL.TexImage2D(
                TextureTarget.Texture2D,
                0, // level
                PixelInternalFormat.Four,
                TextureData.Width, TextureData.Height,
                0, // border
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                PixelType.UnsignedByte,
                TextureData.Scan0
                );

            var Texture = new Texture2D();
            Texture.Width = TextureData.Width;
            Texture.Height = TextureData.Height;
            Texture.ID = id;
            if (Texture.ID == 0)
                return null;
            return Texture;
        }

        public static Texture2D Load(Bitmap TextureBitmap, TextureFilteringMode mode)
        {
            int id;
            Texture2D Texture;// = new Texture2D();
            BitmapData TextureData =
                    TextureBitmap.LockBits(
                                new Rectangle(0, 0, TextureBitmap.Width, TextureBitmap.Height),
                                ImageLockMode.ReadOnly,
                                System.Drawing.Imaging.PixelFormat.Format32bppArgb
                    );
            Texture = Load(TextureData, mode);
            TextureBitmap.UnlockBits(TextureData);
            return Texture;
        }

        public static Texture2D Load(string path, TextureFilteringMode mode)
        {
            int id;
            Console.WriteLine("Attemtping to load texture at '" + path + "'");
            if (System.IO.File.Exists(path))
            {
                Texture2D Texture = new Texture2D();
                using (Bitmap TextureBitmap = new Bitmap(path))
                    Texture = Load(TextureBitmap, mode);
                return Texture;
            }
            return null;
        }

        public static Texture2D Load(BitmapData data)
        {
            return Load(data, TextureFilteringMode.Linear);
        }

        public static Texture2D Load(Bitmap bitmap)
        {
            return Load(bitmap, TextureFilteringMode.Linear);
        }

        public static Texture2D Load(string path)
        {
            return Load(path, TextureFilteringMode.Linear);
        }

        public void Draw(double x, double y, double scale)
        {
            Surface.DrawTexturedRect(this, x, y, Width * scale, Height * scale);
        }

        public int ID = 0;
        public int Width = 0;
        public int Height = 0;

        public static implicit operator int (Texture2D tex)
        {
            if (tex == null)
                return 0;
            return tex.ID;
        }
    }
}
