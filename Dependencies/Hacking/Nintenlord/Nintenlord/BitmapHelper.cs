using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Nintenlord.ROMHacking
{
    static public class BitmapHelper
    {
        /*For testing FindMath2D
            bool passed = true;
            Rectangle rect = new Rectangle(), result = new Rectangle();
            Random rand = new Random();
            int bigWidht = 0x100;
            int bigHeight = 0x100;
            byte[] bigArray = new byte[bigWidht * bigHeight];
            rand.NextBytes(bigArray); 
            for (int i = 0; i < 10000 && passed; i++)
            {
                rect = new Rectangle(rand.Next(bigWidht), rand.Next(bigHeight), 0, 0);
                rect.Width = (byte)(rand.Next(1, bigWidht - rect.X));
                rect.Height = (byte)(rand.Next(1, bigHeight - rect.Y));

                byte[] smallArray = new byte[rect.Width * rect.Height];

                for (int y = 0; y < rect.Height; y++)
                {
                    Array.Copy(bigArray,   rect.X + (y + rect.Y) * bigWidht, 
                               smallArray, y * rect.Width, 
                               rect.Width);
                }

                unsafe
                {
                    fixed (byte* ptr = &bigArray[0])
                    {
                        fixed (byte* ptr2 = &smallArray[0])
                        {
                            result = BitmapHelper.FindMatch2D(
                                ptr, new Size(bigWidht, bigHeight), Rectangle.Empty,
                                ptr2, rect.Size, Rectangle.Empty);
                        }
                    }
                }

                if (rect != result) // can produce false negatives
                {
                    passed = false;
                }
            }

            if (passed)
            {
                MessageBox.Show("Passed.");
            }
            else
            {
                MessageBox.Show("Match not produced.\nResult: " + result.ToString() +
                    "\nOrigin: " + rect.ToString());
            }
         */

        static public unsafe Rectangle FindMatch2D(byte* bigImage, Size bigSize, Rectangle findIn, 
            byte* smallImage, Size smallSize, Rectangle findWhat)
        {
            Rectangle result = Rectangle.Empty;
            if (findIn.IsEmpty)
            {
                findIn = new Rectangle(Point.Empty, bigSize);
            }
            else if (!FitsIn(ref bigSize, ref findIn))
            {
                throw new ArgumentException("Rectangle to find in does not fit to the big image");
            }

            if (findWhat.IsEmpty)
            {
                findWhat = new Rectangle(Point.Empty, smallSize);
            }
            else if (!FitsIn(ref smallSize, ref findWhat))
            {
                throw new ArgumentException("Rectangle to find does not fit to the small image");
            }

            //add test for small rect not fitting to big rect here
                        
            for (int y = findIn.Y; y < findIn.Bottom - findWhat.Height + 1; y++)
            {
                byte* rowPointer = bigImage + y * bigSize.Width + findIn.X;
                for (int x = findIn.X; x < findIn.Right - findWhat.Width + 1; x++)
                {
                    //inner loops, could use some optimation
                    for (int y2 = findWhat.Y; y2 < findWhat.Bottom; y2++)
                    {
                        for (int x2 = findWhat.X; x2 < findWhat.Right; x2++)
                        {
                            //do the comparing
                            if (rowPointer[y2 * bigSize.Width + x2]
                                != smallImage[y2 * smallSize.Width + x2])
                            {
                                goto noMatch;
                            }
                        }
                    }
                    //only way here is to pass the test
                    result.X = x;
                    result.Y = y;
                    result.Width = findWhat.Width;
                    result.Height = findWhat.Height;
                    goto matchFound;
                noMatch:
                    rowPointer++;
                }
            }
            matchFound:
            return result;
        }

        static public unsafe Rectangle FindMatch2D(Bitmap bigImage, Rectangle findIn,
            Bitmap smallImage, Rectangle findWhat)
        {
            if (bigImage.PixelFormat != smallImage.PixelFormat)
            {
                throw new ArgumentException("Bitmaps need to be in same format.");
            }

            if (findIn.IsEmpty)
            {
                findIn = new Rectangle(Point.Empty, bigImage.Size);
            }
            if (findWhat.IsEmpty)
            {
                findWhat = new Rectangle(Point.Empty, smallImage.Size);
            }

            Rectangle result = Rectangle.Empty;

            BitmapData bmpdBig = bigImage.LockBits(findIn, ImageLockMode.ReadOnly, bigImage.PixelFormat);
            BitmapData bmpdSmall = smallImage.LockBits(findWhat, ImageLockMode.ReadOnly, smallImage.PixelFormat);

            byte* pointerBig = (byte*)bmpdBig.Scan0;
            byte* pointerSmall = (byte*)bmpdSmall.Scan0;

            int bpp = BitsPerPixel(bigImage.PixelFormat);
            Size bigSize = bigImage.Size;
            bigSize.Width = bigSize.Width * bpp / 8;
            Size smallSize = smallImage.Size; 
            smallSize.Width = smallSize.Width * bpp / 8;

            findIn.X = findIn.X * bpp / 8;
            findIn.Width = findIn.Width * bpp / 8;
            findWhat.X = findWhat.X * bpp / 8;
            findWhat.Width = findWhat.Width * bpp / 8;

            result = FindMatch2D(pointerBig, bigSize, findIn, pointerSmall, smallSize, findWhat);

            result.X = result.X * 8 / bpp;
            result.Width = result.Width * 8 / bpp;

            return result;
        }

        static public int BitsPerPixel(PixelFormat pixelFormat)
        {
/*
DontCare = 0x0,
Undefined = 0x0,
Max = 0xF,
Indexed = 0x10000,
Gdi = 0x20000,
Alpha = 0x40000,
PAlpha = 0x80000,
Extended = 0x100000,
Canonical = 0x200000,
Format1bppIndexed =     0x30101,
Format4bppIndexed =     0x30402,
Format8bppIndexed =     0x30803,
Format16bppGrayScale = 0x101004,
Format16bppRgb555 =     0x21005,
Format16bppRgb565 =     0x21006,
Format16bppArgb1555 =   0x61007,
Format24bppRgb =        0x21808,
Format32bppRgb =        0x22009,
Format32bppArgb =      0x26200A,
Format32bppPArgb =      0xE200B,
Format48bppRgb =       0x10300C,
Format64bppArgb =      0x34400D,
Format64bppPArgb =     0x1C400E, 
*/
            int value;
            switch ((int)pixelFormat & 0xF)
            {
                case 1:
                    value = 1;
                    break;
                case 2:
                    value = 4;
                    break;
                case 3:
                    value = 8;
                    break;
                case 4:
                    value = 16;
                    break;
                case 5:
                    value = 16;
                    break;
                case 6:
                    value = 16;
                    break;
                case 7:
                    value = 16;
                    break;
                case 8:
                    value = 24;
                    break;
                case 9:
                    value = 32;
                    break;
                case 10:
                    value = 32;
                    break;
                case 11:
                    value = 32;
                    break;
                case 12:
                    value = 48;
                    break;
                case 13:
                    value = 64;
                    break;
                case 14:
                    value = 64;
                    break;
                default:
                    value = 0;
                    break;
            }
            return value;
        }

        static public void Transpose(ref Rectangle rect)
        {
            int temp = rect.Y;
            rect.Y = rect.X;
            rect.X = temp; 
            
            temp = rect.Width;
            rect.Height = rect.Width;
            rect.Width = temp;
        }

        static public void Transpose(ref Point point)
        {
            int temp = point.Y;
            point.Y = point.X;
            point.X = temp;
        }

        static public void Transpose(ref RectangleF rect)
        {
            float temp = rect.Y;
            rect.Y = rect.X;
            rect.X = temp;

            temp = rect.Width;
            rect.Height = rect.Width;
            rect.Width = temp;
        }

        static public void Transpose(ref PointF point)
        {
            float temp = point.Y;
            point.Y = point.X;
            point.X = temp;
        }

        static public bool FitsIn(ref Size size, ref Rectangle rect)
        {
            return size.Height >= rect.Bottom &&
                   size.Width >= rect.Right &&
                   rect.X >= 0 &&
                   rect.Y >= 0;
        }

        static public bool IsBigger(ref Size big, ref Size small)
        {
            return big.Width >= small.Width && big.Height >= small.Height;
        }

        static public List<Color> GetPalette(Bitmap bitmap)
        {
            List<Color> palette = new List<Color>(0x100);

            if ((bitmap.PixelFormat & PixelFormat.Indexed) == PixelFormat.Indexed)
            {
                palette.AddRange(bitmap.Palette.Entries);
            }
            else
            {
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
            }
            return palette;
        }


        static public Color ToRgb(int Hue, int Saturation, int Value)
        {
            // HsvColor contains values scaled as in the color wheel:

            double h;
            double s;
            double v;

            double r = 0;
            double g = 0;
            double b = 0;

            // Scale Hue to be between 0 and 360. Saturation
            // and value scale to be between 0 and 1.
            h = (double)Hue % 360;
            s = (double)Saturation / 100;
            v = (double)Value / 100;

            if (s == 0)
            {
                // If s is 0, all colors are the same.
                // This is some flavor of gray.
                r = v;
                g = v;
                b = v;
            }
            else
            {
                double p;
                double q;
                double t;

                double fractionalSector;
                int sectorNumber;
                double sectorPos;

                // The color wheel consists of 6 sectors.
                // Figure out which sector you're in.
                sectorPos = h / 60;
                sectorNumber = (int)(Math.Floor(sectorPos));

                // get the fractional part of the sector.
                // That is, how many degrees into the sector
                // are you?
                fractionalSector = sectorPos - sectorNumber;

                // Calculate values for the three axes
                // of the color. 
                p = v * (1 - s);
                q = v * (1 - (s * fractionalSector));
                t = v * (1 - (s * (1 - fractionalSector)));

                // Assign the fractional colors to r, g, and b
                // based on the sector the angle is in.
                switch (sectorNumber)
                {
                    case 0:
                        r = v;
                        g = t;
                        b = p;
                        break;

                    case 1:
                        r = q;
                        g = v;
                        b = p;
                        break;

                    case 2:
                        r = p;
                        g = v;
                        b = t;
                        break;

                    case 3:
                        r = p;
                        g = q;
                        b = v;
                        break;

                    case 4:
                        r = t;
                        g = p;
                        b = v;
                        break;

                    case 5:
                        r = v;
                        g = p;
                        b = q;
                        break;
                }
            }
            // return an RgbColor structure, with values scaled
            // to be between 0 and 255.
            return Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));
        }
        
        static public void ToHsv(Color color, out int Hue, out int Saturation, out int Value)
        {
            // In this function, R, G, and B values must be scaled 
            // to be between 0 and 1.
            // HsvColor.Hue will be a value between 0 and 360, and 
            // HsvColor.Saturation and value are between 0 and 1.

            double min;
            double max;
            double delta;

            double r = (double)color.R / 255;
            double g = (double)color.G / 255;
            double b = (double)color.B / 255;

            double h;
            double s;
            double v;

            min = Math.Min(Math.Min(r, g), b);
            max = Math.Max(Math.Max(r, g), b);
            v = max;
            delta = max - min;

            if (max == 0 || delta == 0)
            {
                // R, G, and B must be 0, or all the same.
                // In this case, S is 0, and H is undefined.
                // Using H = 0 is as good as any...
                s = 0;
                h = 0;
            }
            else
            {
                s = delta / max;
                if (r == max)
                {
                    // Between Yellow and Magenta
                    h = (g - b) / delta;
                }
                else if (g == max)
                {
                    // Between Cyan and Yellow
                    h = 2 + (b - r) / delta;
                }
                else
                {
                    // Between Magenta and Cyan
                    h = 4 + (r - g) / delta;
                }

            }
            // Scale h to be between 0 and 360. 
            // This may require adding 360, if the value
            // is negative.
            h *= 60;

            if (h < 0)
            {
                h += 360;
            }

            // Scale to the requirements of this 
            // application. All values are between 0 and 255.
            Hue = (int)h;
            Saturation = (int)(s * 100);
            Value = (int)(v * 100);
        }

        static public Color GetAntiColor(Color color)
        {
            int h, s, v;
            ToHsv(color, out h, out s, out v);
            h = (h + 180) % 360;
            v = 100 - v;
            return ToRgb(h, s, v);
        }
    }
}
