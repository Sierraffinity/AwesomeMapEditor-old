using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Nintenlord.Collections;
using Nintenlord.ROMHacking.GBA.Compressions;
using Nintenlord.ROMHacking.GBA;

namespace Nintenlord.MAR_array_inserter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void Run(string MARfile, string ROMfile, int offset, int[] size, bool repoint, int ptrOffset)
        {
            byte[] MarData;
            try
            {
                MarData = File.ReadAllBytes(MARfile);
            }
            catch (IOException)
            {
                MessageBox.Show(MARfile + " cannot be read.");
                return;
            }

            if (size.Total() * 2 != MarData.Length)
            {
                MessageBox.Show("The size of the map does not match the file.");
                return;
            }

            List<byte> mapData = new List<byte>();
            for (int i = 0; i < size.Length; i++)
                mapData.Add((byte)size[i]);

            for (int i = 0; i < MarData.Length; i += 2)
            {
                int tileVal = MarData[i] + (MarData[i + 1] << 8);
                tileVal >>= 3;
                mapData.AddRange(BitConverter.GetBytes((ushort)tileVal));
            }
            byte[] compressedMapData = LZ77.Compress(mapData.ToArray());

            BinaryWriter writer;

            try
            {
                writer = new BinaryWriter(File.OpenWrite(ROMfile));
            }
            catch (IOException)
            {
                MessageBox.Show(ROMfile + " could not been written to.");
                return;
            }
            writer.BaseStream.Position = offset;
            writer.Write(compressedMapData);

            if (repoint)
            {
                writer.BaseStream.Position = ptrOffset;
                writer.Write(Pointer.MakePointer(offset));
            }
            writer.Close();

            MessageBox.Show("Finished.");
            //foreach (int item in size)
            //{
            //    if (item > 43)
            //    {
            //        MessageBox.Show("The size may be too big.\nContinuing anyway.");
            //        break;
            //    }
            //}
            //BinaryReader br;
            //BinaryWriter bw;
            //List<byte> map = new List<byte>();
            
            //try
            //{
            //    br = new BinaryReader(File.Open(MARfile, FileMode.Open));
            //    bw = new BinaryWriter(File.Open(ROMfile, FileMode.Open));
            //}
            //catch (IOException)
            //{
            //    MessageBox.Show("One of the files is being used by another program.");
            //    return;
            //}

            //if (br.BaseStream.Length != (size[1] * size[0] * 2))
            //{
            //    MessageBox.Show("The size is wrong.");
            //    bw.Close();
            //    br.Close();
            //    return;
            //}
            //foreach (int var in size)
            //{
            //    map.Add((byte)var);
            //}


            //while (br.BaseStream.Position < br.BaseStream.Length)
            //{
            //    map.AddRange(BitConverter.GetBytes((ushort)(br.ReadUInt16() / 8)));
            //}
            //br.Close();

            //bw.BaseStream.Position = offset;

            //bw.Write(LZ77.Compress(map.ToArray()));



            //bw.Close();            
            //MessageBox.Show("Finished.");
        }
    }
}