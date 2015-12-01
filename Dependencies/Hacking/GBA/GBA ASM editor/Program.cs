using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace GBA_ASM_editor
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

        public static unsafe void readFile(string path, string textFile, int offset, bool details)
        {
            const uint BLoffsetMask = 0x07FF;
            const uint BLmask = 0xF800;
            const uint BLidentifier = 0xF000;
            const uint BLHidentifier = 0xF800;

            BinaryReader br = new BinaryReader(File.OpenRead(path));
            byte[] data = br.ReadBytes((int)br.BaseStream.Length);
            br.Close();

            StreamWriter sw = null;
            sw = new StreamWriter(textFile);
            sw.WriteLine("Details of the scan"); 

            List<int> offsets = new List<int>();

            int startingOffset = offset - 4 - 0x40000;
            if (startingOffset < 0)
                startingOffset = 0;
            int endOffset = offset + 4 + 0x3FFFFE;
            if (endOffset > data.Length)
                endOffset = data.Length;
            fixed (byte* dataOffset = &data[0])
            {
                ushort* endPointer, startPointer;
                endPointer = (ushort*)(dataOffset + endOffset);
                for (startPointer = (ushort*)(dataOffset); startPointer < endPointer; startPointer++)
                {
                    ushort code1 = *startPointer;
                    if ((code1 & BLmask) == BLidentifier)
                    {
                        ushort code2 = startPointer[1];
                        if ((code2 & BLmask) == BLHidentifier)
                        {
                            int i = (int)((byte*)startPointer - dataOffset);
                            int BLoffset = (int)(BLoffsetMask & code1) << 12;
                            BLoffset += (int)(BLoffsetMask & code2) << 1;
                            if (BLoffset >> 22 == 1) //the offset is signed
                                BLoffset -= 0x800000;

                            if (i + BLoffset + 4 == offset)
                            {
                                offsets.Add(i);
                                if (details)
                                {
                                    sw.WriteLine(Convert.ToString(i, 16) + " " + Convert.ToString(code1, 16) + " " + Convert.ToString(code2, 16) + " " + Convert.ToString(i + BLoffset, 16));
                                }
                                else
                                {
                                    sw.WriteLine(Convert.ToString(i, 16));
                                }
                            }
                        }
                    }
                }
            }
            sw.Close();
        }   
    }
}
