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
            int step = 4;
            BinaryWriter bw = new BinaryWriter(File.Open(@"C:\Users\Timo\Documents\testMap",FileMode.Create));
            for (int i = 0; i <= 32 * 32 * step; i += step)
            {
                bw.Write((ushort)i);                
            }
        }
    }
}
