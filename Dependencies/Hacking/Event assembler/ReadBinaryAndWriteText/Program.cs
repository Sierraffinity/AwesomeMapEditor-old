using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace ReadBinaryAndWriteText
{
    static class Program
    {
        static void Main(string[] args)
        {
            BinaryReader reader = new BinaryReader(File.OpenRead(args[0]));
            StreamWriter writer = new StreamWriter(args[1]);

            int index = 0;
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                int offset = reader.ReadInt32();
                int lenght = reader.ReadInt32();
                writer.WriteLine("0x{0}: $0{1}, {2}", 
                    Convert.ToString(index, 16), 
                    Convert.ToString(offset, 16), 
                    lenght);
                index++;
            }

            reader.Close();
            writer.Close();
        }
    }
}
