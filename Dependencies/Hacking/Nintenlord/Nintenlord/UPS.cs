using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace Nintenlord.ROMHacking
{
    public unsafe class UPSfile : ICloneable
    {
        bool validPatch;
        public bool ValidPatch
        {
            get { return validPatch; }
        }
        uint originalFileCRC32;
        uint newFileCRC32;
        uint patchCRC32;
        ulong oldFileSize;
        ulong newFileSize;
        ulong[] changedOffsets;
        byte[][] XORbytes;

        /// <summary>
        /// Creates a new UPS patch from UPS file
        /// </summary>
        /// <param name="filePath">A path to an existing, valid UPS path.</param>
        public UPSfile(string filePath)
        {
            List<ulong> changedOffsetsList = new List<ulong>();
            List<byte[]> XORbytesList = new List<byte[]>();

            validPatch = false;

            if (!File.Exists(filePath))
                return;

            byte[] UPSfile;
            try
            {
                BinaryReader br = new BinaryReader(File.OpenRead(filePath));
                UPSfile = br.ReadBytes((int)br.BaseStream.Length);
                br.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

            fixed (byte* UPSptr = &UPSfile[0])
            {
                //header
                byte* currentPtr = UPSptr;
                string header = new string((sbyte*)currentPtr,0,4,Encoding.ASCII);
                if (header != "UPS1")
                    return;
                currentPtr += 4;
                oldFileSize = Decrypt(&currentPtr);
                newFileSize = Decrypt(&currentPtr);

                //body
                ulong filePosition = 0;
                while (currentPtr - UPSptr + 1 < UPSfile.Length - 12)
                {
                    filePosition += Decrypt(&currentPtr);
                    changedOffsetsList.Add(filePosition);
                    List<byte> newXORdata = new List<byte>();

                    while (*currentPtr != 0)
                    {
                        newXORdata.Add(*(currentPtr++));
                    }
                    XORbytesList.Add(newXORdata.ToArray());
                    filePosition += (ulong)newXORdata.Count + 1;
                    currentPtr++;
                }

                //end
                originalFileCRC32 = *((uint*)(currentPtr));
                newFileCRC32 = *((uint*)(currentPtr + 4));
                patchCRC32 = *((uint*)(currentPtr + 8));                            
            }


            changedOffsets = changedOffsetsList.ToArray();
            XORbytes = XORbytesList.ToArray();

            if (patchCRC32 != CalculatePatchCRC32())
                return;

            validPatch = true;
        }

        public UPSfile(byte[] originalFile, byte[] newFile)
        {
            List<ulong> changedOffsetsList = new List<ulong>();
            List<byte[]> XORbytesList = new List<byte[]>();
            validPatch = true;
            oldFileSize = (ulong)originalFile.Length;
            newFileSize = (ulong)newFile.Length;

            ulong maxSize;
            if (oldFileSize > newFileSize)
                maxSize = oldFileSize;
            else
                maxSize = newFileSize;

            for (ulong i = 0; i < maxSize; i++)
            {
                byte x = i < oldFileSize ? originalFile[i] : (byte)0x00;
                byte y = i < newFileSize ? newFile[i] : (byte)0x00;

                if (x != y)
                {
                    changedOffsetsList.Add((ulong)i);
                    List<byte> newXORbytes = new List<byte>();
                    while (x != y && i < maxSize)
                    {
                        newXORbytes.Add((byte)(x ^ y));
                        i++;
                        x = i < oldFileSize ? originalFile[i] : (byte)0x00;
                        y = i < newFileSize ? newFile[i] : (byte)0x00;
                    }
                    XORbytesList.Add(newXORbytes.ToArray());
                }
            }
            originalFileCRC32 = CRC32.crc32_calculate(originalFile);
            newFileCRC32 = CRC32.crc32_calculate(newFile);
            changedOffsets = changedOffsetsList.ToArray();
            XORbytes = XORbytesList.ToArray();
            patchCRC32 = CalculatePatchCRC32();
        }

        private UPSfile(ulong[] changedOffsets, byte[][] XORbytes, uint originalFileCRC32, uint newFileCRC32, ulong oldFileSize, ulong newFileSize)
        {
            this.changedOffsets = changedOffsets.Clone() as ulong[];
            this.XORbytes = new byte[XORbytes.Length][];
            for (int i = 0; i < this.XORbytes.Length; i++)
            {
                this.XORbytes[i] = XORbytes[i].Clone() as byte[];
            }
            this.originalFileCRC32 = originalFileCRC32;
            this.newFileCRC32 = newFileCRC32;
            this.oldFileSize = oldFileSize;
            this.newFileSize = newFileSize;
            this.patchCRC32 = this.CalculatePatchCRC32();
        }

        static byte[] Encrypt(ulong offset)
        {
            List<byte> bytes = new List<byte>(8);

            ulong x = offset & 0x7f;
            offset >>= 7;
            while (offset != 0)
            {
                bytes.Add((byte)x);
                offset--;
                x = offset & 0x7f;
                offset >>= 7;
            }
            bytes.Add((byte)(0x80 | x));
            return bytes.ToArray();
        }
        
        static ulong Decrypt(byte** pointer)
        {
            ulong value = 0;
            int shift = 1;
            byte x = *((*pointer)++);
            value += (ulong)((x & 0x7F) * shift);
            while ((x & 0x80) == 0)
            {
                shift <<= 7;
                value += (ulong)shift;
                x = *((*pointer)++);
                value += (ulong)((x & 0x7F) * shift);
            }
            return value;
        }

        private uint CalculatePatchCRC32()
        {
            return CRC32.crc32_calculate(ToBinary());
        }
        
        public bool ValidToApply(byte[] file)
        {
            uint fileCRC32 = CRC32.crc32_calculate(file);
            bool fitsAsOld = oldFileSize == (ulong)file.Length && fileCRC32 == originalFileCRC32;
            bool fitsAsNew = newFileSize == (ulong)file.Length && fileCRC32 == newFileCRC32;

            return validPatch && (fitsAsOld || fitsAsNew);
        }

        public byte[] Apply(byte[] file)
        {
            ulong lenght = (ulong)file.LongLength;
            if (lenght < newFileSize)
                lenght = newFileSize;

            byte[] result = new byte[lenght];

            fixed (byte* resultPtr = &result[0])
            {                
                Marshal.Copy(file, 0, new IntPtr(resultPtr), Math.Min(file.Length, result.Length));

                for (int i = 0; i < changedOffsets.LongLength; i++)
                    for (ulong u = 0; u < (ulong)XORbytes[i].LongLength; u++)
                        resultPtr[changedOffsets[i] + u] ^= XORbytes[i][u];
            }
            return result;
        }

        public byte[] Apply(string path)
        {
            if (!validPatch || !File.Exists(path))
                return null;

            BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open));
            byte[] file = br.ReadBytes((int)br.BaseStream.Length);
            br.Close();
            return Apply(file);
        }

        private byte[] ToBinary()
        {
            List<byte> file = new List<byte>();
            file.Add((byte)'U');
            file.Add((byte)'P');
            file.Add((byte)'S');
            file.Add((byte)'1');
            file.AddRange(Encrypt(oldFileSize));
            file.AddRange(Encrypt(newFileSize));

            for (int i = 0; i < changedOffsets.LongLength; i++)
            {
                ulong relativeOffset = changedOffsets[i];
                if (i != 0)
                    relativeOffset -= changedOffsets[i - 1] + (ulong)XORbytes[i - 1].Length + 1;

                file.AddRange(Encrypt(relativeOffset));
                file.AddRange(XORbytes[i]);
                file.Add(0);
            }

            file.AddRange(BitConverter.GetBytes(originalFileCRC32));
            file.AddRange(BitConverter.GetBytes(newFileCRC32));

            return file.ToArray();
        }

        public void WriteToFile(string path)
        {
            BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.Create));
            byte[] file = ToBinary();
            bw.Write(file);
            bw.Write(CRC32.crc32_calculate(file));
            bw.Close();
        }

        public int[,] GetData()
        {
            int[,] result = new int[changedOffsets.Length, 2];
            for (int i = 0; i < changedOffsets.Length; i++)
            {
                result[i, 0] = (int)changedOffsets[i];
                result[i, 1] = XORbytes[i].Length;
            }
            return result;
        }

        public bool ChangesOffset(ulong offset)
        {
            for (int i = 0; changedOffsets[i] <= offset && i < changedOffsets.Length; i++)
            {
                if (changedOffsets[i] <= offset && offset < changedOffsets[i] + (ulong)XORbytes[i].Length)
                    return true;
            }
            return false;
        }

        public bool ChangeOffsets(ulong offset, int length)
        {
            for (int i = 0; changedOffsets[i] <= offset + (ulong)length && i < changedOffsets.Length; i++)
            {
                if (changedOffsets[i] <= offset && changedOffsets[i] + (ulong)XORbytes[i].LongLength > offset)
                    return true;
                else if (changedOffsets[i] <= offset + (ulong)length && offset + (ulong)length < changedOffsets[i] + (ulong)XORbytes[i].Length)
                    return true;
            }
            return false;
        }

        public static UPSfile operator +(UPSfile a, UPSfile b)
        {
            byte[] emptyFile = new byte[Math.Max(a.newFileSize, b.newFileSize)];
            byte[] OrigEmptyFile = emptyFile.Clone() as byte[];
            emptyFile = b.Apply(a.Apply(emptyFile));

            UPSfile result = new UPSfile(OrigEmptyFile, emptyFile);
            result.patchCRC32 = result.CalculatePatchCRC32();
            result.originalFileCRC32 = a.originalFileCRC32;
            result.newFileCRC32 = 0;

            return result;
        }

        #region ICloneable Members
        /// <summary>
        /// Creates a deeb copy of the object
        /// </summary>
        /// <returns>A deeb copy of the object</returns>
        public object Clone()
        {
            return new UPSfile(this.changedOffsets, this.XORbytes, this.originalFileCRC32, this.newFileCRC32, this.oldFileSize, this.newFileSize);
        }
        #endregion
    }
    
}
