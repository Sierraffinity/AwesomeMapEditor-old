using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Fire_Emblem_Map_Editor 
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Graphics());
        }

        public static void test(string originalFile, string outputFile, int offset, int size, bool Compress)
        {
            LZ77 test = new LZ77();
            BinaryReader br = new BinaryReader(File.Open(originalFile, FileMode.Open));
            BinaryWriter bw = new BinaryWriter(File.Open(outputFile, FileMode.Create));
            if (size == 0)
            {
                size = (int)br.BaseStream.Length - offset;
            }
            //StreamWriter sw = new StreamWriter(outputFile);
            if (Compress)
            {
                bw.Write(test.Compress(br, offset, size));
            }
            else
            {
                bw.Write(test.UnCompress(br, offset));
            }
            MessageBox.Show("Finished.");
            bw.Close();
            //sw.Close();
            br.Close();
        }
        static char[] hexDigits = {
            '0', '1', '2', '3', '4', '5', '6', '7',
            '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};

        static public UInt32 ToDecUInt(string a)
        {
            int add;
            if (a[0].ToString() == "0" && a[1].ToString() == "x")
            {
                add = 2;
            }
            else if (a[0].ToString() == "$")
            {
                add = 1;
            }
            else
            {
                add = 0;
            }
            Int64 lenght = a.Length;
            Int64 loop = 0;
            UInt32 result = 0;
            while (loop + add < lenght)
            {
                Int64 u = 0;
                string b = Convert.ToString(a[(int)loop + add]);
                if (b == "0") { u = 0; }
                else if (b == "1") { u = 1; }
                else if (b == "2") { u = 2; }
                else if (b == "3") { u = 3; }
                else if (b == "4") { u = 4; }
                else if (b == "5") { u = 5; }
                else if (b == "6") { u = 6; }
                else if (b == "7") { u = 7; }
                else if (b == "8") { u = 8; }
                else if (b == "9") { u = 9; }
                else if (b == "A" || b == "a") { u = 10; }
                else if (b == "B" || b == "b") { u = 11; }
                else if (b == "C" || b == "c") { u = 12; }
                else if (b == "D" || b == "d") { u = 13; }
                else if (b == "E" || b == "e") { u = 14; }
                else if (b == "F" || b == "f") { u = 15; }
                else { Console.Write("Format error."); loop = lenght; result = 0; }
                try
                {
                    result += Convert.ToUInt32(u << (int)(4 * (lenght - loop - add - 1)));
                }
                catch (OverflowException)
                {
                    MessageBox.Show("The Value is too large.");
                }
                loop++;
            }
            return result;
        }

        static string ToHexString(UInt32 dec)
        {
            char[] chars = new char[10];
            chars[0] = hexDigits[dec >> 28 & 0xF];
            chars[1] = hexDigits[dec >> 24 & 0xF];
            chars[2] = hexDigits[dec >> 20 & 0xF];
            chars[3] = hexDigits[dec >> 16 & 0xF];
            chars[4] = hexDigits[dec >> 12 & 0xF];
            chars[5] = hexDigits[dec >> 8 & 0xF];
            chars[6] = hexDigits[dec >> 4 & 0xF];
            chars[7] = hexDigits[dec & 0xF];
            return new string(chars);
        }

    }
}