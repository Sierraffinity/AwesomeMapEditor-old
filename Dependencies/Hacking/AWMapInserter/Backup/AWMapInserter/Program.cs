using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Nintenlord.ROMHacking.GBA.Compressions;

namespace Nintenlord.AWMapInserter
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

        public static void Run(string MARfile, string ROMfile, int offset, int[] mapSize, 
            bool writePointer, int pointerOffset)
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

            if (Total(mapSize) * 2 != MarData.Length)
            {
                MessageBox.Show("The size of the map does not match the file.");
                return;
            }

            List<byte> mapData = new List<byte>();
            for (int i = 0; i < mapSize.Length; i++)
                mapData.Add((byte)mapSize[i]);

            for (int i = 0; i < MarData.Length; i += 2)
            {
                int tileVal = MarData[i] + (MarData[i + 1] << 8);
                tileVal >>= 5;
                mapData.Add((byte)(tileVal & 0xFF));
                mapData.Add((byte)((tileVal >> 8) & 0xFF));
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

            if (writePointer)
            {
                writer.BaseStream.Position = pointerOffset;
                writer.Write(GetPointer(offset));
            }
            writer.Close();

            MessageBox.Show("Finished.");
        }

        static int GetPointer(int offset)
        {
            return offset | 0x8000000;
        }

        static int Total(int[] array)
        {
            int result = 1;
            foreach (var item in array)
            {
                result *= item;
            }
            return result;
        }
    }
}
