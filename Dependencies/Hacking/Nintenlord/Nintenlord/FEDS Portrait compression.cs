using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.DS.Compressions.GameSpecific
{
    static unsafe class FEDS_Portrait_compression
    {
        static public byte[] Decompress(byte[] data, int offset)
        {
            fixed (byte* pointer = &data[offset])
            {
                return Decompress(pointer);
            }
        }

        static public byte[] Decompress(byte* data)
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

        static public byte[] Compress(byte[] data)
        {
            fixed (byte* pointer = &data[0])
            {
                return Compress(pointer, data.Length);
            }
        }

        static public byte[] Compress(byte[] data, int offset, int length)
        {
            fixed (byte* pointer = &data[offset])
            {
                return Compress(pointer, length);
            }
        }

        static public byte[] Compress(byte* data, int length)
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
    }
}
