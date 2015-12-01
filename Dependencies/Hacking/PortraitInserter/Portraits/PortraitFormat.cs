using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Nintenlord.Feditor.Core.GameData;
using Nintenlord.Feditor.Core.MemoryManagement;
using Nintenlord.MemoryManagement;
using Nintenlord.ROMHacking.GBA.Compressions;
using Nintenlord.ROMHacking.GBA.Graphics;
using Nintenlord.Utility;
using Nintenlord.Forms;
using Nintenlord.Forms.Utility;
using PortraitInserter.Portraits;

namespace PortraitInserter
{
    internal class PortraitFormat
    {
        static readonly Size FormatSizePixels = new Size(128, 112);
        static readonly Size TileSize = new Size(8, 8);
        static readonly PortraitFormat FE6Format;
        static readonly PortraitFormat FE7Format;
        static readonly PortraitFormat FE8Format;
        
        /// <summary>
        /// Initializes built-in formats
        /// </summary>
        static PortraitFormat()
        {
            //All positions and sizes in 8x8 tile coordinates
            Rectangle miniPortrait = new Rectangle(12, 2, 4, 4);
            Size genericCardSize = new Size(10, 9);
            
            FE6Format = new PortraitFormat();
            FE6Format.PortraitSize = new Size(32, 5);
            FE6Format.BlockAdd = 24;
            FE6Format.MouthMapping = new Dictionary<Rectangle, Point>()
            {
                { new Rectangle(2, 0, 8, 4), new Point(0, 0) },
                { new Rectangle(2, 4, 8, 4), new Point(8, 0) },
                { new Rectangle(2, 8, 4, 2), new Point(16, 0) },
                { new Rectangle(6, 8, 4, 2), new Point(16, 2) },
                { new Rectangle(0, 6, 2, 4), new Point(20, 0) },
                { new Rectangle(10, 6, 2, 4), new Point(22, 0) },
                { new Rectangle(0, 10, 8, 4), new Point(24, 0) }, //Mouth frames
                { new Rectangle(8, 10, 4, 1), new Point(0, 4) }, 
                { new Rectangle(8, 11, 4, 1), new Point(4, 4) },
            };
            FE6Format.CompressedPortrait = true;

            FE6Format.SeparateMouthFrames = false;
            FE6Format.MouthMapping = null;

            FE6Format.MiniMapping = new Dictionary<Rectangle, Point>()
            {
                { miniPortrait, new Point(0, 0) },
            };
            FE6Format.MiniSize = miniPortrait.Size;
            FE6Format.CompressedMini = false;
            FE6Format.GenericSize = genericCardSize;



            FE7Format = new PortraitFormat();
            FE7Format.PictureMapping= new Dictionary<Rectangle, Point>()
            {
                { new Rectangle(2, 0, 8, 4), new Point(0, 0) },
                { new Rectangle(2, 4, 8, 4), new Point(8, 0) },
                { new Rectangle(2, 8, 4, 2), new Point(16, 0) },
                { new Rectangle(6, 8, 4, 2), new Point(16, 2) },
                { new Rectangle(0, 6, 2, 4), new Point(20, 0) },
                { new Rectangle(10, 6, 2, 4), new Point(22, 0) },
                { new Rectangle(12, 6, 4, 4), new Point(24, 0) },//Blinking frames
                { new Rectangle(12, 10, 4, 2), new Point(28, 0) },//The extra thing
            };
            FE7Format.PortraitSize = new Size(32, 4);
            FE7Format.CompressedPortrait= true;
            FE7Format.BlockAdd = 0;

            FE7Format.SeparateMouthFrames = true;
            FE7Format.MouthMapping = new Dictionary<Rectangle, Point>()
            {
                { new Rectangle(0, 10, 4, 2),  new Point(0, 0) },
                { new Rectangle(4, 10, 4, 2),  new Point(0, 2) },
                { new Rectangle(8, 10, 4, 2),  new Point(0, 4) },
                { new Rectangle(0, 12, 4, 2),  new Point(0, 6) },
                { new Rectangle(4, 12, 4, 2),  new Point(0, 8) },
                { new Rectangle(8, 12, 4, 2),  new Point(0, 10) },
            };
            FE7Format.MouthSize = new Size(4, 12);

            FE7Format.MiniMapping = new Dictionary<Rectangle, Point>()
            {
                { miniPortrait, new Point(0, 0) },
            };
            FE7Format.MiniSize = miniPortrait.Size;
            FE7Format.CompressedMini = true;
            FE7Format.GenericSize = genericCardSize;


            FE8Format = new PortraitFormat();
            FE8Format.PictureMapping= new Dictionary<Rectangle, Point>()
            {
                { new Rectangle(2, 0, 8, 4), new Point(0, 0) },
                { new Rectangle(2, 4, 8, 4), new Point(8, 0) },
                { new Rectangle(2, 8, 4, 2), new Point(16, 0) },
                { new Rectangle(6, 8, 4, 2), new Point(16, 2) },
                { new Rectangle(0, 6, 2, 4), new Point(20, 0) },
                { new Rectangle(10, 6, 2, 4), new Point(22, 0) },
                { new Rectangle(12, 6, 4, 4), new Point(24, 0) },//Blinking frames
                { new Rectangle(12, 10, 4, 2), new Point(28, 0) },//The extra thing
            };
            FE8Format.PortraitSize = new Size(32, 4);
            FE8Format.CompressedPortrait = false;
            FE8Format.BlockAdd = 0;

            FE8Format.SeparateMouthFrames = true;
            FE8Format.MouthMapping= new Dictionary<Rectangle, Point>()
            {
                { new Rectangle(0, 10, 4, 2),  new Point(0, 0) },
                { new Rectangle(4, 10, 4, 2),  new Point(0, 2) },
                { new Rectangle(8, 10, 4, 2),  new Point(0, 4) },
                { new Rectangle(0, 12, 4, 2),  new Point(0, 6) },
                { new Rectangle(4, 12, 4, 2),  new Point(0, 8) },
                { new Rectangle(8, 12, 4, 2),  new Point(0, 10) },
            };;
            FE8Format.MouthSize = new Size(4, 12);

            FE8Format.MiniMapping = new Dictionary<Rectangle, Point>()
            {
                { miniPortrait, new Point(0, 0) },
            };
            FE8Format.MiniSize = miniPortrait.Size;
            FE8Format.CompressedMini = true;
            FE8Format.GenericSize = genericCardSize;
        }

        private static PortraitFormat GetFormat(string game)
        {
            PortraitFormat result;
            switch (game)
            {
                case "AFEJ":
                    result = FE6Format;
                    break;
                case "AE7J":
                case "AE7E":
                    result = FE7Format;
                    break;
                case "BE8J":
                case "BE8E":
                    result = FE8Format;
                    break;
                default:
                    throw new ArgumentException("");
            }
            return result;
        }

        Dictionary<Rectangle, Point> PictureMapping;
        Size PortraitSize;
        bool CompressedPortrait;
        int BlockAdd;

        bool SeparateMouthFrames; //Always decomp when separate
        Dictionary<Rectangle, Point> MouthMapping;
        Size MouthSize;

        Dictionary<Rectangle, Point> MiniMapping;
        Size MiniSize;
        bool CompressedMini;
        
        Size GenericSize;

        private Size PortraitSizePixels
        {
            get
            {
                return new Size(
                    PortraitSize.Width * TileSize.Width,
                    PortraitSize.Height * TileSize.Height);
            }
        }
        private Size MouthSizePixels
        {
            get
            {
                return new Size(
                    MouthSize.Width * TileSize.Width,
                    MouthSize.Height * TileSize.Height);
            }
        }
        private int MainPortraitSizeBytes
        {
            get
            {
                return (PortraitSize.Width * PortraitSize.Height - BlockAdd) * 32;
            }
        }
        private int MiniPortraitSizeBytes
        {
            get
            {
                return (MiniSize.Width * MiniSize.Height) * 32;
            }
        }
        private int PaletteSizeBytes
        {
            get
            {
                return 0x20;
            }
        }
        private int MouthSizeBytes
        {
            get
            {
                return MouthSize.Width * MouthSize.Height * 32;
            }
        }
        private int GenericSizeBytes
        {
            get { return GenericSize.Width * GenericSize.Height * 32; }
        }

        private PortraitFormat() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="allocator"></param>
        /// <param name="rom"></param>
        /// <returns>
        /// first is main portrait offset, 
        /// second is miniportrait, 
        /// third is palette and
        /// fourth is mouth frames if separate
        /// </returns>
        public static CanCauseError<ManagedPointer[]> WriteData(Bitmap picture,
            IAllocator<ManagedPointer> allocator, IROM rom)
        {
            var format = GetFormat(rom.GameCode);
            return WriteData(format, picture, allocator, rom);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="format"></param>
        /// <param name="allocator"></param>
        /// <param name="rom"></param>
        /// <returns>
        /// first is main portrait offset, 
        /// second is miniportrait, 
        /// third is palette and
        /// fourth is mouth frames if separate
        /// </returns>
        private static CanCauseError<ManagedPointer[]> WriteData(PortraitFormat format, Bitmap picture, 
            IAllocator<ManagedPointer> allocator, IROM rom)
        {
            List<ManagedPointer> pointers = new List<ManagedPointer>(4);

            //Get palette
            Color[] palette;
            bool palettedBitmap = picture.PixelFormat.HasFlag(PixelFormat.Indexed);
            if (!palettedBitmap)
            {
                var colors = picture.GetColors();
                colors = GBAPalette.GetGBAColors(colors);
                if (colors.Count > 16)
                {
                    return CanCauseError<ManagedPointer[]>.Error("Over 16 colours.");
                }
                palette = colors.ToArray();
            }
            else
            {
                palette = picture.Palette.Entries;
            }

            //Split to separate portraits
            Bitmap mainPortrait = new Bitmap(
                format.PortraitSize.Width,
                format.PortraitSize.Height, picture.PixelFormat);

            Move(picture, mainPortrait, format.PictureMapping);

            Bitmap mouthbm;
            if (format.SeparateMouthFrames)
            {
                mouthbm = new Bitmap(format.MouthSize.Width, format.MouthSize.Height, picture.PixelFormat);
            }
            else
            {
                mouthbm = mainPortrait;
            }

            Move(picture, mouthbm, format.MouthMapping);

            Bitmap mini = new Bitmap(format.MiniSize.Width, format.MiniSize.Height, picture.PixelFormat);

            Move(picture, mouthbm, format.MiniMapping);



            //Write data
            pointers.Add(Insert(mainPortrait, palette, format.BlockAdd, format.CompressedPortrait, rom, allocator));
            
            pointers.Add(Insert(mini, palette, 0, format.CompressedMini, rom, allocator));
            
            byte[] rawPalette = GBAPalette.toRawGBAPalette(palette);
            var ptr = allocator.Allocate(rawPalette.Length, 4);
            if (!ptr.IsNull)
            {
                rom.WriteData(ptr, rawPalette, 0, rawPalette.Length);
            }
            pointers.Add(ptr);


            if (mouthbm != mainPortrait)
            {
                pointers.Add(Insert(mouthbm, palette, 0, true, rom, allocator));
            }

            return pointers.ToArray();
        }

        private static ManagedPointer Insert(Bitmap toInsert, Color[] palette, int blockAdded, bool compressed, 
            IROM rom, IAllocator<ManagedPointer> allocator)
        {
            var bytes = GBAGraphics.ToGBARaw(toInsert, palette, GraphicsMode.Tile4bit);

            int insertedLength;
            int bytesAdded = blockAdded * 32;

            if (compressed)
            {
                bytes = LZ77.Compress(bytes, 0, bytes.Length - bytesAdded);
                insertedLength = bytes.Length;
            }
            else
            {
                insertedLength = bytes.Length - bytesAdded;
            }

            var ptr = allocator.Allocate(insertedLength, 4);
            if (!ptr.IsNull)
            {
                rom.WriteData(ptr, bytes, 0, insertedLength);
            }

            return ptr;
        }

        public static CanCauseError<Bitmap> GetPortrait(GeneralizedPointerTableEntry entry, IROM rom)
        {
            var format = GetFormat(rom.GameCode);

            return format.GetBitmat(entry, rom);
        }
        
        private CanCauseError<Bitmap> GetBitmat(GeneralizedPointerTableEntry entry, IROM rom)
        {
            return from face in entry.MainPortraitOffset == 0 ? CanCauseError<byte[]>.NoError(null) :
                                GetData(rom,
                                    entry.MainPortraitOffset,
                                    CompressedPortrait,
                                    this.MainPortraitSizeBytes,
                                    "Main portrait")
                   from mini in entry.MiniPortraitOffset == 0 ? CanCauseError<byte[]>.NoError(null) :
                                GetData(rom,
                                    entry.MiniPortraitOffset,
                                    this.CompressedMini,
                                    this.MiniPortraitSizeBytes,
                                    "Mini portrait")
                   from generic in entry.GenericOffset == 0 ? CanCauseError<byte[]>.NoError(null) :
                                GetData(rom,
                                    entry.GenericOffset,
                                    true,
                                    this.GenericSizeBytes,
                                    "Generic portrait")
                   from palette in entry.PaletteOffset == 0 ? CanCauseError<byte[]>.NoError(null) :
                                GetData(rom,
                                    entry.PaletteOffset,
                                    false,
                                    this.PaletteSizeBytes,
                                    "Palette")
                   from mouth in entry.MouthOffset == 0 || !this.SeparateMouthFrames ? CanCauseError<byte[]>.NoError(null) :
                                GetData(rom,
                                    entry.MouthOffset,
                                    false,
                                    this.MouthSizeBytes,
                                    "Mouth")
                   from result in GetPortraitFromRaw(face, mini, palette, mouth, generic)
                   select result;
        }
        
        private CanCauseError<Bitmap> GetPortraitFromRaw(byte[] rawPortrait, byte[] rawMini, byte[] rawPalette, byte[] rawMouth, byte[] rawGeneric)
        {
            Color[] palette = GBAPalette.ToPalette(rawPalette, 0, rawPalette.Length / 2);

            CanCauseError<Bitmap> result;
            if (rawPortrait != null)
            {
                result = GetNormalFormat(rawPortrait, rawMini, rawMouth, palette);
            }
            else
            {
                int temp;
                result = GBAGraphics.ToBitmap(
                   rawGeneric, rawGeneric.Length, 0, palette, this.GenericSize.Width * TileSize.Width,
                   GraphicsMode.Tile4bit, out temp);
                if (temp != 0) return CanCauseError<Bitmap>.Error("Raw generic card is wrong size");
            }
            return result;
        }

        private CanCauseError<Bitmap> GetNormalFormat(byte[] rawPortrait, byte[] rawMini, byte[] rawMouth, Color[] palette)
        {
            int temp;
            var result = new Bitmap(FormatSizePixels.Width, FormatSizePixels.Height, PixelFormat.Format8bppIndexed);
            result.InsertColorsToPalette(palette);

            Bitmap mainPortrait = GBAGraphics.ToBitmap(
                rawPortrait, rawPortrait.Length, 0, palette, this.PortraitSize.Width * TileSize.Width,
                GraphicsMode.Tile4bit, out temp);
            if (temp != this.BlockAdd) return CanCauseError<Bitmap>.Error("Raw mainportrait is wrong size");

            Bitmap miniPortrait;
            if (rawMini != null)
            {
                miniPortrait = GBAGraphics.ToBitmap(
                    rawMini, rawMini.Length, 0, palette, this.MiniSize.Width * TileSize.Width,
                    GraphicsMode.Tile4bit, out temp);
                if (temp != 0) return CanCauseError<Bitmap>.Error("Raw miniportrait is wrong size");
            }
            else miniPortrait = null;

            Bitmap mouthPortrait;
            if (this.SeparateMouthFrames)
            {
                mouthPortrait = GBAGraphics.ToBitmap(
                    rawMouth, rawMouth.Length, 0, palette, this.MouthSize.Width * TileSize.Width,
                    GraphicsMode.Tile4bit, out temp);
                if (temp != 0) return CanCauseError<Bitmap>.Error("Raw mouth is wrong size");
            }
            else
            {
                mouthPortrait = mainPortrait;
            }

            Move(mainPortrait, result, ReverseMapping(this.PictureMapping));
            Move(mouthPortrait, result, ReverseMapping(this.MouthMapping));
            Move(mainPortrait, result, ReverseMapping(this.MiniMapping));
            return result;
        }


        private static void Move(Bitmap source, Bitmap dest, IEnumerable<KeyValuePair<Rectangle, Point>> mapping)
        {
            foreach (var rect in mapping)
            {
                Rectangle scaledRect = rect.Key;
                scaledRect.Width *= 8;
                scaledRect.Height *= 8;
                scaledRect.X *= 8;
                scaledRect.Y *= 8;

                Point scaledPoint = rect.Value;
                scaledPoint.X *= 8;
                scaledPoint.Y *= 8;
                
                source.Copy(dest, scaledRect, scaledPoint);
            }
        }

        private static IEnumerable<KeyValuePair<Rectangle, Point>> ReverseMapping(
            IEnumerable<KeyValuePair<Rectangle, Point>> mapping)
        {
            foreach (var pair in mapping)
            {
                Point point = pair.Key.Location;
                Rectangle rect = pair.Key;
                rect.Location = pair.Value;
                yield return new KeyValuePair<Rectangle, Point>(rect, point);
            }
        }
        

        static CanCauseError<byte[]> GetData(IROM rom, int offset, bool compressed, int size, string error)
        {
            CanCauseError<byte[]> result;
            if (compressed)
            {
                result = Decomp(rom, offset);
                if (result.Result == null)
                {
                    result = CanCauseError<byte[]>.Error(error + " failed to decompress");
                }
                else if (result.Result.Length != size)
                {
                    result = CanCauseError<byte[]>.Error(error + " is wrong size");
                }
            }
            else
            {
                result = rom.ReadData(offset, size);
            }

            return result;
        }

        static int LZ77Length(int offset, IROM rom)
        {
            int result;
            using (var reader = new System.IO.BinaryReader(new ROMReaderStream(rom)))
            {
                result = LZ77.GetCompressedDataLenght(reader, offset);
            }
            return result;
        }

        static byte[] Decomp(IROM rom, int offset)
        {
            byte[] result;
            using (var input = new ROMReaderStream(rom))
            {
                input.Position = offset;
                using (MemoryStream output = new MemoryStream(0x1000))
                {
                    if (LZ77.Decompress(input, output))
                    {
                        result = output.ToArray();
                    }
                    else
                    {
                        result = null;
                    }
                }
            }
            return result;
        }
    }
}
