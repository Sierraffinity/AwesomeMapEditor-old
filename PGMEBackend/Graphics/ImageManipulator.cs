// Awesome Map Editor
// A map editor for GBA Pokémon games.

// Copyright (C) 2015 Diegoisawesome

// This file is part of Awesome Map Editor.
// Awesome Map Editor is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Awesome Map Editor is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with Awesome Map Editor. If not, see <http://www.gnu.org/licenses/>.

using Nintenlord.ROMHacking.GBA;
using Nintenlord.ROMHacking.GBA.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGMEBackend.Graphics
{
    static class ImageManipulator
    {
        public static Bitmap imageLoader(GBAROM ROM, int imageOffset, int paletteOffset, int width, int height, bool transparent)
        {
            return imageLoader(ROM, imageOffset, paletteOffset, width, height, false, false, transparent, GraphicsMode.Tile4bit);
        }

        public static Bitmap imageLoader(GBAROM ROM, int imageOffset, int paletteOffset, int width, int height, bool isImageCompressed, bool isPaletteCompressed)
        {
            return imageLoader(ROM, imageOffset, paletteOffset, width, height, isImageCompressed, isPaletteCompressed, false, GraphicsMode.Tile4bit);
        }

        public static Bitmap imageLoader(GBAROM ROM, int imageOffset, int paletteOffset, int width, int height, bool isImageCompressed, bool isPaletteCompressed, bool transparent)
        {
            return imageLoader(ROM, imageOffset, paletteOffset, width, height, isImageCompressed, isPaletteCompressed, transparent, GraphicsMode.Tile4bit);
        }

        public static Bitmap imageLoader(GBAROM ROM, int imageOffset, int paletteOffset, int width, int height, bool isImageCompressed, bool isPaletteCompressed, bool transparent, GraphicsMode mode)
        {
            DataBuffer rawGraphics = new DataBuffer(0x8000);
            DataBuffer rawPalette = new DataBuffer(0x100);
            if (isImageCompressed)
            {
                rawGraphics.ReadCompressedData(ROM, imageOffset);
            }
            else
            {
                int gfxlength = GBAGraphics.RawGraphicsLength(new Size(width * 8, height * 8), mode);
                rawGraphics.ReadData(ROM, imageOffset, gfxlength);
            }

            if (isPaletteCompressed)
            {
                rawPalette.ReadCompressedData(ROM, paletteOffset);
            }
            else
            {
                rawPalette.ReadData(ROM, paletteOffset, 0x200);
            }

            byte[] graphics = rawGraphics.ToArray();
            int bitsPerPixel = GBAGraphics.BitsPerPixel(mode);

            int length = Math.Min(bitsPerPixel * width * height / 8, graphics.Length);
            Color[] palette;

            if (rawPalette.Length > 0 && paletteOffset != 0)
                palette = GBAPalette.ToPalette(rawPalette.ToArray(), 0, rawPalette.Length / 2);
            else
            {
                palette = new Color[16];
                for (int i = 0; i < palette.Length; i++)
                    palette[i] = Color.FromArgb(i * (256 / palette.Length), i * (256 / palette.Length), i * (256 / palette.Length));
            }

            int empty;
            if(transparent)
                palette[0] = Color.FromArgb(0, palette[0]);
            return GBAGraphics.ToBitmap(graphics, length, 0, palette, width, mode, out empty);
        }
    }
}
