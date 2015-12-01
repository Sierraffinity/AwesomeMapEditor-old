using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.GBA.Compressions
{
    static unsafe class RLE
    {
        static public byte[] Decompress(byte[] data)
        {
            if (data[0] != 0x30)
                return null;

            int dataLength = data[1] + data[2] << 8 + data[3] << 16;
            List<byte> decompData = new List<byte>(dataLength);

            int i = 4;
            while (i < data.Length)
            {
                int length = data[i] & 0x7F;
                if ((data[i++] & 0x80) == 0)
                {
                    length++;
                    for (int u = 0; u < length; u++)
                        decompData.Add(data[i + u]);
                    i += length;
                }
                else
                {
                    length += 3;
                    for (int u = 0; u < length; u++)
                        decompData.Add(data[i]);
                    i++;
                }
            }

            return decompData.ToArray();
        }
    }
}
