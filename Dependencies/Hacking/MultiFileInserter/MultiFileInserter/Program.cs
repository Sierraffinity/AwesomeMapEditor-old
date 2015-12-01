using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    static class Program
    {
        static public int offset;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string ROMPath = openROM();

            if (ROMPath == null || !File.Exists(ROMPath))
                return;

            BinaryWriter bw = new BinaryWriter(File.Open(ROMPath, FileMode.Open));

            string[] files = getFiles();
            bw.BaseStream.Position = getOffset((int)bw.BaseStream.Length);
            int[] offsets = new int[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                if (File.Exists(files[i]))
                {
                    offsets[i] = (int)bw.BaseStream.Position;
                    BinaryReader br = new BinaryReader(File.Open(files[i], FileMode.Open));
                    byte[] data = br.ReadBytes((int)br.BaseStream.Length);
                    bw.Write(data);
                    if (bw.BaseStream.Position % 4 != 0)
                    {
                        bw.BaseStream.Position += 4 - (bw.BaseStream.Position % 4);
                    }
                }
            }
            writeOffsets(offsets);
        }

        static string[] getFiles()
        {
            List<string> files = new List<string>();
            do
            {
                OpenFileDialog open = new OpenFileDialog();
                open.CheckFileExists = true;
                open.Multiselect = true;
                open.Title = "Choose files to insert.";
                open.Filter = "All files|*";
                open.ShowDialog();
                files.AddRange(open.FileNames);
            }
            while (MessageBox.Show("Select more files to insert?", "More?", MessageBoxButtons.YesNo) == DialogResult.Yes);

            return files.ToArray();            
        }

        static string openROM()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.CheckFileExists = true;
            open.Title = "Choose file to insert to.";
            open.Filter = "GBA ROMs|*.gba|All files|*";
            open.ShowDialog();
            if (open.FileNames.Length > 0)
                return open.FileNames[0];
            else
                return null;
        }

        static int getOffset(int maxValue)
        {
            Form1 form = new Form1(maxValue);
            form.ShowDialog();
            return offset;
        }

        static void writeOffsets(int[] offsets)
        {
            string message = "Offsets where the data was inserted.";
            for (int i = 0; i < offsets.Length; i++)
            {
                message += Convert.ToString(offsets[i], 16) + "\n";
            }
            MessageBox.Show(message);
        }
    }
}
