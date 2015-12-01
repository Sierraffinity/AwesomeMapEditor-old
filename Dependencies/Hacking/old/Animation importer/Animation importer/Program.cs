using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Nintenlord.GBA.Compressions;

namespace Nintenlord.Animation_importer
{
    static class Program
    {
        public static BinaryReader Inputbr;
        static BinaryReader Outputbr;
        public static BinaryWriter Outputbw;
        static string originalFile, outputFile;

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

        public static void Run(string originalFile, string outputFile, int offset, int animationNumber, bool isInputCustom, bool isOutputCustom, int customInputOffset, int costomOutputOffset)
        {
            Program.originalFile = originalFile;
            Program.outputFile = outputFile;

            //bool OneFile = (outputFile == originalFile);
            if (outputFile == originalFile)
            {
                MessageBox.Show("Input and Output must be different files.");
                return;
            }

            if ((originalFile == null) || (outputFile == null))
            {
                MessageBox.Show("Both input and output must be specified.");
                return;
            }

            try
            {
                Inputbr = new BinaryReader(File.Open(originalFile, FileMode.Open));
                //Outputbr = new BinaryReader(File.Open(outputFile, FileMode.Open));
                Outputbw = new BinaryWriter(File.Open(outputFile, FileMode.Open));
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("A file doesn't exist.");
                return;
            }
            catch (IOException)
            {
                MessageBox.Show("A file is being used by another program.");
                return;
            }

            Outputbw.BaseStream.Position = offset;
            Inputbr.BaseStream.Position = FindAnimationPointerTableForInput(animationNumber);
            

            uint[] dataheader = new uint[8];
            for (int i = 0; i < dataheader.Length; i++)
            {
                dataheader[i] = Inputbr.ReadUInt32();
            }

            AnimationHandler animationhandler = new AnimationHandler(dataheader);
            animationhandler.MainDataFinder();
            animationhandler.GraphicsFinder();
            animationhandler.InsertGraphics();
            animationhandler.InsertNewGraphicsPointers();
            animationhandler.InsertNewAnimation();
            Outputbw.Close();

            try
            {
                Outputbr = new BinaryReader(File.Open(outputFile, FileMode.Open));
            }
            catch (IOException)
            {
                MessageBox.Show("A file is being used by another program.");
                return;
            }
            Outputbr.BaseStream.Position = FindAnimationPointerTableForOutput();

            while (Outputbr.ReadUInt64() != 0)
            {
                Outputbr.BaseStream.Position += 0x18;
            }
            Outputbr.BaseStream.Position -= 8;
            int Offset = (int)Outputbr.BaseStream.Position;
            Outputbr.Close();

            try
            {
                Outputbw = new BinaryWriter(File.Open(outputFile, FileMode.Open));
            }
            catch (IOException)
            {
                MessageBox.Show("A file is being used by another program.");
                return;
            }

            Outputbw.BaseStream.Position = Offset;

            
            animationhandler.InsertNewAnimationHeader();

            Inputbr.Close();
            Outputbr.Close();
            Outputbw.Close();
            MessageBox.Show("Finished.");
        }

        static int FindAnimationPointerTableForInput(int animationNumber)
        {
            int Offset = 0;
            char [] identifier;

            Inputbr.BaseStream.Position = 0xAC;

            identifier = Inputbr.ReadChars(4);

            if (identifier == "AE7E".ToCharArray())
            {
                Offset = AnimationPointerTableOffsets[7];
            }
            else if (identifier == "BE8E".ToCharArray())
	        {
		         Offset = AnimationPointerTableOffsets[8];
	        }
            else if (identifier == "AFEJ".ToCharArray())
	        {
		         Offset = AnimationPointerTableOffsets[6];
	        }

            Offset += (8 + (32 * animationNumber));

            return Offset;
        }

        static int FindAnimationPointerTableForOutput()
        {
            int Offset = 0;
            uint identifier;

            Outputbr.BaseStream.Position = 0xAC;

            identifier = Outputbr.ReadUInt32();


            switch (identifier)
            {
                case 0x41453745:
                    Offset = AnimationPointerTableOffsets[7];
                    break;
                case 0x42453845:
                    Offset = AnimationPointerTableOffsets[8];
                    break;
                case 0x4146454A:
                    Offset = AnimationPointerTableOffsets[6];
                    break;
                default:
                    break;
            }
            Offset += 8;
            return Offset;
        }

        #region Offsets
        static int[] AnimationPointerTableOffsets = 
        {
            0x00,
            0x00,
            0x00,
            0x00,
            0x00,
            0x00,
            0x6A0000,
            0xE00000,
            0xC00000,
            0x00,
            0x00,
        };
        static int[] AnimationPointerTableLenghts = 
        {
            0x00,
            0x00,
            0x00,
            0x00,
            0x00,
            0x00,
            0x1000,
            0x2000,
            0x2000,
            0x00,
            0x00,
        }; 
        #endregion
    }
}
