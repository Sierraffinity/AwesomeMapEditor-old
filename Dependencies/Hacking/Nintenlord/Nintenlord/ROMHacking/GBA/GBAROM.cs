using System.Collections.Generic;
using System.IO;
using Nintenlord.ROMHacking;
using Nintenlord.ROMHacking.GBA.Compressions;

namespace Nintenlord.ROMHacking.GBA
{
    public class GBAROM : AbstractROM
    {
        public const int MaxRomSize = 0x2000000;

        public GBAROM() : base(MaxRomSize)
        {

        }

        public override void OpenROM(string path)
        {
            BinaryReader br = new BinaryReader(File.OpenRead(path));
            if (MaxLength > 0 && br.BaseStream.Length > MaxLength)
                ROMdata = new byte[MaxLength];
            else
                ROMdata = new byte[br.BaseStream.Length];
            br.Read(ROMdata, 0, ROMdata.Length);
            br.Close();
            this.Edited = false;
            this.ROMPath = path;
        }

        public override void CloseROM()
        {
            this.Edited = false;
            this.ROMPath = null;
            this.ROMdata = null;
        }

        public override void SaveROM(string path)
        {
            Path.ChangeExtension(path, ".gba");
            BinaryWriter rw = new BinaryWriter(File.Open(path, FileMode.Create));
            rw.Write(ROMdata);
            rw.Close();
            Edited = false;
        }

        public override void SaveBackup()
        {
            Path.ChangeExtension(ROMPath, ".bak");
            BinaryWriter rw = new BinaryWriter(File.Open(ROMPath, FileMode.Create));
            rw.Write(ROMdata);
            rw.Close();
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

    }
}
