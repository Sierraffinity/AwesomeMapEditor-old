using System;
using System.Collections.Generic;
using Nintenlord;

namespace Nintenlord.Compressor.Compressions
{
    public unsafe class FEDS_portrait_compression : Compression
    {
        public FEDS_portrait_compression()
            : base(
                //CompressionModes.Checkeble
                CompressionOperation.Compress
                | CompressionOperation.Decompress
                //| CompressionModes.Lengthable
                //| CompressionModes.Scannable)//delete unsupported abilities
            , new string[]{".nds"})
        {

        }

        public override byte[] Decompress(byte[] data)
        {
            fixed (byte* pointer = &data[0])
            {
                return Compress(pointer, data.Length);
            }
        }

        public override byte[] Decompress(byte[] data, int offset)
        {
            fixed (byte* pointer = &data[offset])
            {
                return Decompress(pointer);
            }
        }

        public byte[] Decompress(byte* data)
        {
            int length = (*(int*)data) >> 8;
            byte[] result = new byte[length];

            int i = 0;
            fixed (byte* pointer = &result[0])
            {
                data += 4;
                while (i < length)
                {
                    if (data[0] == 0)
                    {
                        uint temp = data[1];
                        if ((temp & 0x80) == 0)
                            data += 2;
                        else
                        {
                            uint temp2 = data[2];
                            data += 3;
                            temp <<= 0x19;
                            temp = temp2 + (temp >> 0x11);
                        }
                        i += (int)temp + 1;
                    }
                    else
                        pointer[i++] = *(data++);
                }
            }
            return result;
        }

        public override byte[] Compress(byte[] data)
        {
            fixed (byte* pointer = &data[0])
            {
                return Compress(pointer, data.Length);
            }
        }

        public override byte[] Compress(byte[] data, int offset)
        {
            fixed (byte* pointer = &data[offset])
            {
                return Compress(pointer, data.Length - offset);
            }
        }

        public override byte[] Compress(byte[] data, int offset, int length)
        {
            fixed (byte* pointer = &data[offset])
            {
                return Compress(pointer, length);
            }
        }

        public byte[] Compress(byte* data, int length)
        {
            List<byte> compressedData = new List<byte>(length + 4);
            compressedData.Add(0x40);
            compressedData.Add((byte)length);
            compressedData.Add((byte)((length >> 8) & 0xFF));
            compressedData.Add((byte)((length >> 16) & 0xFF));
            int i = 0;
            while (i < length)
            {
                compressedData.Add(data[i]);
                if (data[i] == 0)
                {
                    int amount = 0;
                    while (data[i + amount + 1] == 0 && amount < 0x8000 && i + amount + 1 < length)
                        amount++;

                    if (amount > 0x7F)
                    {
                        compressedData.Add((byte)((amount >> 8) | 0x80));
                        compressedData.Add((byte)amount);
                    }
                    else
                    {
                        compressedData.Add((byte)amount);
                    }
                    i += amount + 1;
                }
                else
                    i++;
            }

            return compressedData.ToArray();
        }
    

        public override int[] Scan(byte[] data, int sizeMax, int sizeMin, int sizeModulus)
        {
            throw new NotImplementedException();
        }

        public override int[] Scan(byte[] data, int offset, int sizeMax, int sizeMin, int sizeModulus)
        {
            throw new NotImplementedException();
        }

        public override int[] Scan(byte[] data, int offset, int length, int sizeMax, int sizeMin, int sizeModulus)
        {
            throw new NotImplementedException();
        }

        public override bool CanBeDecompressed(byte[] data)
        {
            throw new NotImplementedException();
        }

        public override bool CanBeDecompressed(byte[] data, int offset)
        {
            throw new NotImplementedException();
        }

        public override bool CanBeDecompressed(byte[] data, int offset, int maxLength, int minLength)
        {
            throw new NotImplementedException();
        }

        public override int CompressedLength(byte[] data)
        {
            throw new NotImplementedException();
        }

        public override int CompressedLength(byte[] data, int offset)
        {
            throw new NotImplementedException();
        }

        public override int DecompressedDataLenght(byte[] data)
        {
            throw new NotImplementedException();
        }

        public override int DecompressedDataLenght(byte[] data, int offset)
        {
            throw new NotImplementedException();
        }
    }
}
