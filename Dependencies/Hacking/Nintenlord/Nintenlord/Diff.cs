using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.GBA.Compressions
{
    static unsafe class Diff
    {
        enum UnitSize
        {
            bit8 =1,
            bit16=2
        }

        static byte[] Decompress(byte[] data, UnitSize unitSize)
        {
            fixed (byte* pointer = &data[0])
            {
                return Decompress(pointer, data.Length, unitSize);
            }
        }

        static byte[] Decompress(byte* data, int length, UnitSize unitSize)
        {
            byte[] result = new byte[(*(int*)data) >> 8];



            return result;
        }

        static void Decompress(byte* source, byte* destination)
        {
            int length = (*(int*)source) >> 8;
            source += 4;
            byte prev = 0;
            for (int i = 0; i < length; i++)
            {
            }
        }

        static void Decompress(short* source, short* destination)
        {
            int length = (*(int*)source) >> 8;

        }
    }
}
