using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace Nintenlord.GBA
{
    public static class GBAGraphics
    {
        static unsafe public Bitmap ToBitmap(byte* GBAGraphics, int length, int Width, Color[] palette, out int emptyGraphicPlocks, PixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case PixelFormat.DontCare:
                    goto case PixelFormat.Format4bppIndexed;
                case PixelFormat.Format32bppArgb:
                    return ToBitmap(GBAGraphics, length, Width, palette, out emptyGraphicPlocks);
                case PixelFormat.Format32bppRgb:
                    return ToBitmap(GBAGraphics, length, Width, palette, out emptyGraphicPlocks);
                case PixelFormat.Format4bppIndexed:
                    return ToIndexedBitmap(GBAGraphics, length, Width, palette, out emptyGraphicPlocks);
                case PixelFormat.Format8bppIndexed:
                    return ToIndexedBitmap(GBAGraphics, length, Width, palette, out emptyGraphicPlocks);
                case PixelFormat.Indexed:
                    return ToIndexedBitmap(GBAGraphics, length, Width, palette, out emptyGraphicPlocks);
                default:
                    throw new Exception("Bitmap format not supported.");
            }
        }

        static unsafe Bitmap ToBitmap(byte* GBAGraphics, int length, int Width, Color[] palette, out int emptyGraphicPlocks)
        {
            int Height, add;
            add = 0;
            if (length % (32 * Width) != 0)
            {
                add = 1;
            }

            Height = (((length / 32) - (length / 32) % Width) / Width + add);

            emptyGraphicPlocks = (Width * Height) - (length / 32);

            Bitmap bmp = new Bitmap(Width * 8, Height * 8, PixelFormat.Format32bppArgb);

            for (int i = 0; i < length; i++)
            {
                Point coordinates = tiledCoordinate(i * 2, Width * 8, 8);

                bmp.SetPixel(coordinates.X, coordinates.Y, palette[*(GBAGraphics + i) & 0xF]);
                bmp.SetPixel(coordinates.X + 1, coordinates.Y, palette[(*(GBAGraphics + i) >> 4) & 0xF]);
            }
            return bmp;
        }

        static unsafe Bitmap ToIndexedBitmap(byte* GBAGraphics, int length, int Width, Color[] palette, out int emptyGraphicPlocks)
        {
            //throw new NotImplementedException("Not made yet.");
            int Height, add;
            add = 0;
            if (length % (32 * Width) != 0)
            {
                add = 1;
            }
            Height = (((length / 32) - (length / 32) % Width) / Width + add);
            emptyGraphicPlocks = (Width * Height) - (length / 32);

            Bitmap bitmap = new Bitmap(Width * 8, Height * 8, PixelFormat.Format8bppIndexed);
            Rectangle rectangle = new Rectangle(new Point(), bitmap.Size);

            bitmap.Palette = paletteMaker(palette, bitmap.Palette);

            BitmapData sourceData = bitmap.LockBits(rectangle, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            unsafe
            {                
                for (int x = 0; x < bitmap.Width; x += 2)
                {
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        int PositionBitmap = bitmapPosition(new Point(x, y), Width * 8);
                        int PositionGBA = tiledPosition(new Point(x, y), Width * 8, 8) / 2;
                        byte data = *(GBAGraphics + PositionGBA);
                        *((byte*)sourceData.Scan0 + PositionBitmap) = (byte)(data & 0xF);
                        *((byte*)sourceData.Scan0 + PositionBitmap + 1) = (byte)((data >> 4) & 0xF);
                    }
                }
            }
            bitmap.UnlockBits(sourceData);
            return bitmap;
        }

        static private ColorPalette paletteMaker(Color[] palette, ColorPalette original)
        {
            for (int i = 0; i < palette.Length; i++)
            {
                original.Entries[i] = palette[i];
            }
            for (int i = palette.Length; i < original.Entries.Length; i++)
            {
                original.Entries[i] = Color.FromArgb(0, 0, 0);
            }
            return original;
        }

        static unsafe public Color[] GBAPalette(ushort* GBAPalette, int amountOfColours)
        {
            Color[] palette = new Color[amountOfColours];

            for (int i = 0; i < palette.Length; i++)
            {
                ushort colorGBA = *(GBAPalette);
                GBAPalette++;
                int red = (colorGBA & 0x1F) * 8;
                int green = ((colorGBA >> 5) & 0x1F) * 8;
                int blue = ((colorGBA >> 10) & 0x1F) * 8;
                palette[i] = Color.FromArgb(red, green, blue);
            }

            return palette;
        }

        static public Color[] GBAPalette(byte[] GBAPalette)
        {
            if ((GBAPalette.Length % 32) != 0)
                return null;

            unsafe
            {
                fixed (byte* pointer = &GBAPalette[0])
                {
                    return GBAGraphics.GBAPalette((ushort*)pointer, 16);
                }
            }
        }

        static private Point bitmapCoordinate(int position, int widht)
        {
            Point point = new Point();
            point.X = position / widht;
            point.Y = position % widht;
            return point;
        }

        static private Point tiledCoordinate(int position, int widht, int tileDimension)
        {
            if (widht % tileDimension != 0)
                throw new ArgumentException("Bitmaps widht needs to be multible of tile's widht.");

            Point point = new Point();
            point.X = (position % tileDimension) + ((position / (tileDimension * tileDimension)) % (widht / tileDimension)) * tileDimension;
            point.Y = ((position % (tileDimension * tileDimension)) / tileDimension) + ((position / (tileDimension * tileDimension)) * tileDimension / widht) * tileDimension;
            return point;
        }
        
        static private int bitmapPosition(Point coordinate, int widht)
        {
            return coordinate.X + coordinate.Y * widht;
        }

        static private int tiledPosition(Point coordinate, int widht, int tileDimension)
        {
            if (widht % tileDimension != 0)
            {
                throw new ArgumentException("Widht needs to be dividable with tiles dimennsion.");
            }
            return (coordinate.X % tileDimension + (coordinate.Y % tileDimension) * tileDimension +
                   (coordinate.X / tileDimension) * (tileDimension * tileDimension) +
                   (coordinate.Y / tileDimension) * (tileDimension * widht));
        }

        static unsafe public byte[] ToGBARawFromIndexed(Bitmap bitmap, int emptyGraphicsBlocks)
        {
            //int block = 8;
            int Abpp = 4;

            byte[] result = new byte[bitmap.Width * bitmap.Height / 2 - emptyGraphicsBlocks * 8 * Abpp];
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(new Point(), bitmap.Size), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            for (int i = 0; i < result.Length; i++)
            {
                Point coordinates = tiledCoordinate(i * 2, bitmap.Width, 8);

                switch (bitmap.PixelFormat)
                {
                    case PixelFormat.Format1bppIndexed:
                        throw new NotImplementedException();
                    case PixelFormat.Format4bppIndexed:
                        {
                            int pB = bitmapPosition(new Point(coordinates.X, coordinates.Y), bitmap.Width) / 2;
                            byte root = *((byte*)bitmapData.Scan0 + pB);
                            byte first = (byte)(root & 0xF);
                            byte second = (byte)((root >> 4) & 0xF);
                            result[i] = (byte)((first << 4) + second);
                        }
                        break;
                    case PixelFormat.Format8bppIndexed:
                        {
                            int pB = bitmapPosition(new Point(coordinates.X, coordinates.Y), bitmap.Width);
                            byte first = *((byte*)bitmapData.Scan0 + pB);
                            byte second = *((byte*)bitmapData.Scan0 + pB + 1);
                            first &= 0xF;
                            second &= 0xF;
                            result[i] = (byte)((second << 4) + first);
                        }
                        break;
                    default:
                        throw new System.BadImageFormatException("Wrong image format.");
                }
            }

            bitmap.UnlockBits(bitmapData);
            return result;
        }

        static public byte[] ToGBARaw(Bitmap bitmap, int emptyGraphicsBlocks, bool largePalette, List<Color> palette)
        {
            byte[] result = new byte[bitmap.Width * bitmap.Height / 2 - emptyGraphicsBlocks * 32];

            for (int i = 0; i < result.Length; i++)
            {
                int x = (i % 4) * 2 + (i % (bitmap.Width * 4) - i % 32) / 4;
                int y = (i % 32 - i % 4) / 4 + (i - (i % (4 * bitmap.Width))) / (bitmap.Width / 2);
                Color color1 = bitmap.GetPixel(x, y);
                Color color2 = bitmap.GetPixel(x + 1, y);

                result[i] = (byte)((palette.IndexOf(color1) % 0xF) + ((palette.IndexOf(color2) % 0xF) << 4));
            }

            return result;
        }

        static public byte[] ToGBARaw(Bitmap bitmap, int emptyGraphicsBlocks, bool largePalette, Color[] palette)
        {
            List<Color> list = new List<Color>(palette);
            return ToGBARaw(bitmap, emptyGraphicsBlocks, largePalette, list);
        }

        static public List<Color> getPalette(Bitmap bitmap)
        {
            List<Color> palette = new List<Color>();

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color color = bitmap.GetPixel(i, j);
                    if (!palette.Contains(color))
                    {
                        palette.Add(color);
                    }
                }
            }
            return palette;
        }
    }
}
