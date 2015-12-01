using System.Collections.Generic;
using System.IO;
using System;
using Nintenlord.ROMHacking;
using Nintenlord.ROMHacking.GBA.Compressions;
using System.Text;

namespace Nintenlord.ROMHacking.GBA
{
    public class GBAROM : AbstractROM
    {
        public const int MaxRomSize = 0x2000000;
        public const string defaultExtension = ".gba";

        public GBAROM() : base(MaxRomSize)
        {

        }

        public override void OpenROM(string path)
        {
            using (Stream stream = File.OpenRead(path))
            {
                OpenROM(stream);
            }
            this.ROMPath = path;
        }

        public override void OpenROM(Stream stream)
        {            
            BinaryReader br = new BinaryReader(stream);
            if (MaxLength > 0 && br.BaseStream.Length > MaxLength)
                ROMdata = new byte[MaxLength];
            else
                ROMdata = new byte[br.BaseStream.Length];
            br.Read(ROMdata, 0, ROMdata.Length);
            this.Edited = false;
        }

        public override void CloseROM()
        {
            this.Edited = false;
            this.ROMPath = null;
            this.ROMdata = null;
        }

        public override void SaveROM(string path)
        {
            using (Stream stream = File.OpenWrite(path))
            {
                SaveROM(stream);
            }
            this.ROMPath = path;
        }

        public override void SaveROM(Stream stream)
        {
            BinaryWriter rw = new BinaryWriter(stream);
            rw.Write(ROMdata);
            Edited = false;
        }

        public override void SaveBackup()
        {
            string backUpPath = Path.ChangeExtension(ROMPath, ".bak");
            using (BinaryWriter rw = new BinaryWriter(File.Open(backUpPath, FileMode.Create)))
            {
                rw.Write(ROMdata);
            }
        }

        #region GBA pointers

        public int[] SearchForPointer(int offset)
        {
            if (offset < MaxLength)
            {
                int[] values = this.SearchForValue(offset + 0x8000000);
                List<int> result = new List<int>(values);
                result.RemoveAll(BitUtility.IsInvalidIntOffset);
                return result.ToArray();
            }
            else
            {
                return new int[0];
            }
        }

        public int[] ReplacePointers(int oldOffset, int newOffset)
        {
            int[] offsets = SearchForPointer(oldOffset);
            for (int i = 0; i < offsets.Length; i++)
            {
                this.InsertData(offsets[i], newOffset + 0x8000000);
            }
            return offsets;
        }

        public int ReadPointer(int offset)
        {
            int ptr = BitConverter.ToInt32(ROMdata, offset);
            int val;
            if (!Pointer.GetOffset(ptr, out val))
            {
                throw new ArgumentException();
            }
            return val;
        }

        #endregion

        #region LZ77 compression

        public void InsertLZ77CompressedData(int offset, byte[] data)
        {
            InsertLZ77CompressedData(offset, data, 0, data.Length);
        }

        public void InsertLZ77CompressedData(int offset, byte[] data, int index, int length)
        {
            byte[] compressedData = LZ77.Compress(data, index, length);
            this.InsertData(offset, compressedData);
        }

        public byte[] DecompressLZ77CompressedData(int offset)
        {
            return LZ77.Decompress(ROMdata, offset);
        }

        public int[] ScanForLZ77CompressedData(int offset, int length, int maxSize, int minSize, int sizeMultible)
        {
            return LZ77.Scan(ROMdata, offset, length, sizeMultible, minSize, maxSize);
        }

        public bool CanBeLZ77Decompressed(int offset, int maxSize, int minSize)
        {
            return LZ77.CanBeUnCompressed(ROMdata, offset, minSize, maxSize);
        }

        public int LZ77CompressedDataLength(int offset)
        {
            return LZ77.GetCompressedDataLenght(ROMdata, offset);
        }

        #endregion

        #region Header data

        public byte[] CompressedBitmap
        {
            get { return this.GetData(4, 156); }
        }

        public string GameTitle
        {
            get 
            {
                StringBuilder bldr = new StringBuilder(12);

                for (int i = 0; i < 12; i++)
                {
                    byte b = this.ROMdata[0xA0 + i];

                    if (b == 0) break;

                    bldr.Append(char.ConvertFromUtf32(b));
                }

                return bldr.ToString();
            }
        }

        public string GameCode
        {
            get
            {
                string title;
                unsafe
                {
                    fixed (byte* ptr = &(this.ROMdata[0xAC]))
                    {
                        title = new string((sbyte*)ptr, 0, 4,
                            System.Text.Encoding.ASCII);
                    }
                }

                return title;
            }
        }

        public string MakerCode
        {
            get
            {
                string title;
                unsafe
                {
                    fixed (byte* ptr = &(this.ROMdata[0xB0]))
                    {
                        title = new string((sbyte*)ptr, 0, 2,
                            System.Text.Encoding.ASCII);
                    }
                }

                return title;
            }
        }
        
        #endregion
    }
}
