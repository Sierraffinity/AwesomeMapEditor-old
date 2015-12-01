using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryWriter br = new BinaryWriter(File.Create(@"C:\Users\Timo\Documents\testPalette"));

            byte colorSeed = 0;
            for (int i = 0; i < 256; i++)
            {
                ushort color = (ushort)(colorSeed);
                if (colorSeed > 0)
                    color += (ushort)((colorSeed - 1) << 5);
                else
                    color += (ushort)(colorSeed << 5);

                if (colorSeed < 31)
                    color += (ushort)((colorSeed + 1) << 10);
                else
                    color += (ushort)(colorSeed << 10);

                br.Write(color);
                if (colorSeed >= 31)
                {
                    colorSeed = 0;
                }
                else
                {
                    colorSeed++;
                }
            }
            br.Close();
        }
    }
}
