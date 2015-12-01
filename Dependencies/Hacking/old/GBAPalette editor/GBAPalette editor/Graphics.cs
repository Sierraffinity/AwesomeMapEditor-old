using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Nintenlord.GBA
{
    public static class Graphics
    {
        static public Color GBAColor(ushort colorGBA)
        {
            int red = (colorGBA & 0x1F) * 8;
            int green = ((colorGBA >> 5) & 0x1F) * 8;
            int blue = ((colorGBA >> 10) & 0x1F) * 8;
            return Color.FromArgb(red, green, blue);
        }

        static public ushort ToGBAValue(Color color)
        {
            int red = color.R / 8;
            int blue = color.B / 8;
            int green = color.G / 8;
            if (red > 31 || blue > 31 || green > 31)
            {
                throw new FormatException("The color values are over 31.");
            }
            return (ushort)(red + (green << 5) + (blue << 10));
        }

        static public Color[] GBAPalette(byte[] GBAPalette)
        {
            Color[] palette = new Color[16];

            for (int i = 0; i < palette.Length; i++)
            {
                palette[i] = GBAColor((ushort)(GBAPalette[i * 2] + (GBAPalette[i * 2 + 1] << 8)));
            }

            return palette;
        }

        static public byte[] ToGBAPalette(Color[] palette)
        {
            List<byte> result = new List<byte>();
            foreach (Color item in palette)
            {
                result.AddRange(BitConverter.GetBytes(ToGBAValue(item)));
            }

            return result.ToArray();
        }
    }
}
