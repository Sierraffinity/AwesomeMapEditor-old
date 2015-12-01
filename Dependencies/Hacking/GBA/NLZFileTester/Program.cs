using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Nintenlord.ROMHacking;
using System.Runtime.Serialization.Formatters.Binary;

namespace NLZFileTester
{
    class Program
    {
        static void Main(string[] args)
        {
            NLZFile file = new NLZFile();
            file.AddAppData<int>("test", 666);
            file.AddFreeSpace(0, 15);
            file.CRC32 = 47474747;
            file.FileSize = 245;

            NLZFile copy;

            using (Stream stream = File.OpenWrite(@"C:\Users\Timo\Desktop\test.txt"))//new MemoryStream(1000))
            {
                IFormatter formatter = new BinaryFormatter();

                formatter.Serialize(stream, file);
            }

            Console.WriteLine("Done");
        }
    }
}
