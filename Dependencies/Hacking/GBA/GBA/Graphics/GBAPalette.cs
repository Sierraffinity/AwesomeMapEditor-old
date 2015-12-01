using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Nintenlord.Utility;
using System.Runtime.InteropServices;
using Nintenlord.Forms.Vectors;

namespace Nintenlord.ROMHacking.GBA.Graphics
{
    public unsafe class GBAPalette
    {
        Color[] palette;
        
        public Color this[int i]
        {
            get { return palette[i]; }
            set { palette[i] = value; }
        }
        public Color this[int pal, int c]
        {
            get { return palette[pal * 0x10 + c]; }
            set { palette[pal * 0x10 + c] = value; }
        }

        public int Length
        {
            get { return palette.Length; }
        }
        public int SmallPaletteCount
        {
            get { return palette.Length >> 4; }
        }

        private GBAPalette()
        {
        }

        public GBAPalette(Color[] palette)
            : this(palette, 0, palette.Length)
        {

        }

        public GBAPalette(Color[] palette, int index)
            : this(palette, index, palette.Length - index)
        {

        }

        public GBAPalette(Color[] palette, int index, int length)
        {
            length.Clamp(0x10, 0x100).ToMod(0x10);
            this.palette = new Color[length];
            Array.Copy(palette, this.palette, Math.Min(palette.Length, this.palette.Length));
            int i = palette.Length;
            while (i < this.palette.Length)
            {
                this.palette[i] = Color.Black;
                i++;
            }
            ValidateColors(index, length);
        }        

        private void ValidateColors(int index, int length)
        {
            for (int i = index; i < index + length; i++)
            {
                Color c = palette[i];

                if (IsInvalidGBAColour(ref c))
                {
                    NormalizeToGBA(ref c);
                    palette[i] = c;
                }
            }
        }

        public static bool IsInvalidGBAColour(ref Color c)
        {
            return 
                (c.R & 7) != 0 ||
                (c.G & 7) != 0 ||
                (c.B & 7) != 0;
        }

        public static GBAPalette GetFromBinary(byte[] data)
        {
            return GetFromBinary(data, 0, data.Length);
        }
        public static GBAPalette GetFromBinary(byte[] data, int index)
        {
            return GetFromBinary(data, index, data.Length - index);
        }
        public static GBAPalette GetFromBinary(byte[] data, int index, int length)
        {
            Color5bpc[] colors = ToGBAColors(data, index, length);

            Color[] palette =  Array.ConvertAll<Color5bpc, Color>(colors, x => ToColor(x));

            GBAPalette gbaPalette = new GBAPalette();

            gbaPalette.palette = palette;

            return gbaPalette;
        }

        public static HashSet<Color> GetGBAColors(IEnumerable<Color> colors)
        {
            var result = new HashSet<Color>();
            foreach (var item in colors)
            {
                Color newC = item;
                NormalizeToGBA(ref newC);
                result.Add(newC);
            }
            return result;
        }

        public static void NormalizeToGBA(ref Color color)
        {
            int r = color.R & 0xF8;
            int g = color.G & 0xF8;
            int b = color.B & 0xF8;
            color = Color.FromArgb(r, g, b);
        }





        public static Color[] ToPalette(byte[] data, int offset, int amountOfColours)
        {
            fixed (byte* ptr = &data[offset])
            {
                return toPalette((ushort*)ptr, amountOfColours);
            }
        }

        private static Color[] toPalette(ushort* GBAPalette, int amountOfColours)
        {
            Color[] palette = new Color[amountOfColours];

            for (int i = 0; i < palette.Length; i++)
            {
                palette[i] = toColor(GBAPalette);
                GBAPalette++;
            }
            return palette;
        }

        public static byte[] toRawGBAPalette(Color[] palette)
        {
            byte[] result = new byte[palette.Length * 2];
            fixed (byte* pointer = &result[0])
            {
                ushort* upointer = (ushort*)pointer;
                for (int i = 0; i < palette.Length; i++)
                {
                    *upointer = toGBAcolor(palette[i]);
                    upointer++;
                }
            }
            return result;
        }

        private static Color toColor(ushort* GBAColor)
        {
            int red = ((*GBAColor) & 0x1F) * 8;
            int green = (((*GBAColor) >> 5) & 0x1F) * 8;
            int blue = (((*GBAColor) >> 10) & 0x1F) * 8;
            return Color.FromArgb(red, green, blue);
        }

        public static ushort toGBAcolor(Color color)
        {
            byte red = (byte)(color.R >> 3);
            byte blue = (byte)(color.B >> 3);
            byte green = (byte)(color.G >> 3);
            return (ushort)(red + (green << 5) + (blue << 10));
        }

        public static ushort toGBAcolor(int red, int green, int blue)
        {
            byte GBAred = (byte)(red >> 3);
            byte GBAblue = (byte)(green >> 3);
            byte GBAgreen = (byte)(blue >> 3);
            return (ushort)(GBAred + (GBAgreen << 5) + (GBAblue << 10));
        }

        public static Color ToColor(IRGBColor color)
        {
            return Color.FromArgb(color.R, color.G, color.B);
        }

        public static Color5bpc[] ToGBAColors(byte[] data, int index, int colorAmount)
        {
            if (index + colorAmount * sizeof(Color5bpc) > data.Length ||
                index < 0 ||
                colorAmount < 0)
            {
                throw new IndexOutOfRangeException();
            }

            Color5bpc[] result = new Color5bpc[colorAmount];
            fixed (Color5bpc* ptr = result)
            {
                IntPtr ptr2 = (IntPtr)ptr;
                Marshal.Copy(data, index, ptr2, colorAmount * sizeof(Color5bpc));
            }
            return result;
        }


    }
}
