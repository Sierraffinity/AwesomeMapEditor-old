using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Nintenlord.Compressor.Compressions;
using Nintenlord.Utility;


namespace Nintenlord.Compressor
{
    static class Program
    {
        static Action<string> messagePoster;
        static IUserInterface UI;
        static List<Compression> compressions;
        static Compression currentCompression;

        static readonly string compressionFolder = "Compressions";

        static void userInterface_OnEvent(CompressionOperation obj)
        {
            Compression comp = currentCompression;
            if (!comp.SupportsOperation(obj))
            {
                throw new NotSupportedException("Compression " + comp.GetType().Name 
                    + " doesn't support " + Enum.GetName(typeof(CompressionOperation),obj) + ".");
            }
            IUserInterface UI = Program.UI;
            int inputOffset = UI.InputOffset;
            int outputOffset = UI.OutputOffset;
            int inputLength = UI.InputLength;
            string inputFile = UI.InputFile;
            string outputFile = UI.OutputFile;
            switch (obj)
            {
                case CompressionOperation.None:
                    throw new NotSupportedException("No action not suported");
                case CompressionOperation.Decompress:
                    Decompress(comp, inputFile, outputFile, inputOffset, outputOffset);
                    break;
                case CompressionOperation.Compress:
                    Compress(comp, inputFile, outputFile, inputOffset, inputLength, outputOffset);
                    break;
                case CompressionOperation.Scan:
                    Scan(comp, inputFile, outputFile, inputOffset, outputOffset);
                    break;
                case CompressionOperation.Check:
                    Check(comp, inputFile, inputOffset);
                    break;
                case CompressionOperation.CompLength:
                    CompLength(comp, inputFile, inputOffset);
                    break;
                case CompressionOperation.DecompLength:
                    DecompLength(comp, inputFile, inputOffset);
                    break;
                default:
                    throw new NotSupportedException("Unknown action not suported");
            }
        }

        static void userInterface_OnCompressionChanged(int obj)
        {
            currentCompression = compressions[obj];
            UI.CheckEnabled = currentCompression.SupportsOperation(CompressionOperation.Check);
            UI.CompressEnabled = currentCompression.SupportsOperation(CompressionOperation.Compress);
            UI.DecompLenghtEnabled = currentCompression.SupportsOperation(CompressionOperation.DecompLength);
            UI.DecompressEnabled = currentCompression.SupportsOperation(CompressionOperation.Decompress);
            UI.LengthEnabled = currentCompression.SupportsOperation(CompressionOperation.CompLength);
            UI.ScanEnabled = currentCompression.SupportsOperation(CompressionOperation.Scan);
            UI.SetFileExtensions(currentCompression.fileExtensions);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            messagePoster = x => MessageBox.Show(x);
            LoadAssemblies(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), compressionFolder));

            if (args.Length < 5)
            {
                GUI(args);
            }
            else
            {
                CommandLine(args);
            }
        }

        private static void CommandLine(string[] args)
        {
            try
            {
                Compression compression = null;
                foreach (Compression toTest in compressions)
                {
                    if (args[0].Equals(toTest.ToString()))
                    {
                        compression = toTest;
                        break;
                    }
                }

                CompressionOperation toPerform = (CompressionOperation)Enum.Parse(
                    typeof(CompressionOperation), args[1]);
               
                string inputFile = args[2];

                string outputFile = null;
                int inputOffset = 0, outputOffset = 0, inputLenght = 0;
                if (!compression.SupportsOperation(toPerform))
                {
                    throw new NotImplementedException();
                }

                switch (toPerform)
                {
                    case CompressionOperation.Decompress:
                        outputFile = args[3];
                        inputOffset = args[4].GetValue();
                        outputOffset = args[5].GetValue();
                        Decompress(compression, inputFile, outputFile, inputOffset, outputOffset);
                        break;

                    case CompressionOperation.Compress:
                        outputFile = args[3];
                        inputOffset = args[4].GetValue();
                        inputLenght = args[5].GetValue();
                        outputOffset = args[6].GetValue();
                        Compress(compression, inputFile, outputFile, inputOffset, inputLenght, outputOffset);
                        break;

                    case CompressionOperation.Scan:
                        outputFile = args[3];
                        inputOffset = args[4].GetValue();
                        inputLenght = args[5].GetValue();
                        Scan(compression, inputFile, outputFile, inputOffset, inputLenght);
                        break;

                    case CompressionOperation.Check:
                        inputOffset = args[3].GetValue();
                        Check(compression, inputFile, inputOffset);
                        break;

                    case CompressionOperation.CompLength:
                        inputOffset = args[3].GetValue();
                        CompLength(compression, inputFile, inputOffset);
                        break;

                    case CompressionOperation.DecompLength:
                        inputOffset = args[3].GetValue();
                        CompLength(compression, inputFile, inputOffset);
                        DecompLength(compression, inputFile, inputOffset);
                        break;
                }
            }
            catch (Exception)
            {
                messagePoster("");
            }
        }

        private static void GUI(string[] args)
        {
            using (Form1 form = new Form1())
            {
                UI = form;
                if (args.Length > 0)
                {
                    UI.InputFile = args[0];
                }
                if (args.Length > 1)
                {
                    UI.OutputFile = args[1];
                }

                List<string> compressionNames = new List<string>(compressions.Count);
                foreach (Compression compression in compressions)
                {
                    compressionNames.Add(compression.ToString());
                }
                UI.SetDecompressionNames(compressionNames.ToArray());
                UI.OnCompressionChanged +=
                      new Action<int>(userInterface_OnCompressionChanged);
                UI.OnRun += new Action<CompressionOperation>(userInterface_OnEvent);
                userInterface_OnCompressionChanged(0);
                Application.Run(form);
            }
        }

        static void LoadAssemblies(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                //throw new IOException("Path " + folderPath + " doesn't exist.\nCreate it.");
            }
            string[] files = Directory.GetFiles(folderPath, "*.dll", SearchOption.AllDirectories);
            compressions = new List<Compression>(files.Length);
            for (int i = 0; i < files.Length; i++)
            {
                Assembly compressionAssembly = Assembly.LoadFile(files[i]);
                Type[] types = compressionAssembly.GetTypes();
                foreach (Type item in types)
                {
                    if (item.InheritsFrom(typeof(Compression)))
                    {
                        object obj = item.GetConstructor(System.Type.EmptyTypes).Invoke(null);
                        compressions.Add(obj as Compression);
                    }
                }
            }
        }
        
        static void Compress(Compression compression, string input, string output, int inputOffset, int length, int outputOffset)
        {
            if ((compression.supportedModes & CompressionOperation.Compress) == CompressionOperation.None)
            {
                messagePoster("Compression type doesn't support compressing.");
                return;
            }
            byte[] data;
            try
            {
                BinaryReader br = new BinaryReader(File.OpenRead(input));
                br.BaseStream.Position = inputOffset;
                if (length == 0 || (br.BaseStream.Length - br.BaseStream.Position) < length)
                {
                    length = (int)(br.BaseStream.Length - br.BaseStream.Position);
                }

                data = br.ReadBytes(length);
                br.Close();
            }
            catch (IOException)
            {
                messagePoster("Error while opening the file.");
                return;
            }

            byte[] compData = compression.Compress(data);

            try
            {
                BinaryWriter bw = new BinaryWriter(File.Open(output, FileMode.OpenOrCreate));
                bw.BaseStream.Position = outputOffset;
                bw.Write(compData);
                bw.Close();
            }
            catch (IOException)
            {
                messagePoster("Error while opening output file.");
                return;
            }
            messagePoster("Finished");
        }

        static void Check(Compression compression, string input, int offset)
        {
            if ((compression.supportedModes & CompressionOperation.Check) == CompressionOperation.None)
            {
                messagePoster("Compression type doesn't support checking.");
                return;
            }
            byte[] file;
            try
            {
                BinaryReader br = new BinaryReader(File.OpenRead(input));
                file = br.ReadBytes((int)br.BaseStream.Length);
                br.Close();
            }
            catch (IOException)
            {
                messagePoster("Error while opening the file.");
                return;
            }
            if (offset > file.Length)
            {
                messagePoster("Offset is larger than the size of the file.");
                return;
            }

            if (compression.CanBeDecompressed(file, offset, 0, 0xFFFFFF))
            {
                messagePoster("Data can be decompressed.");
            }
            else
            {
                messagePoster("Data can be decompressed.");
            }
        }

        static void Decompress(Compression compression, string input, string output, int inputOffset, int outputOffset)
        {
            if ((compression.supportedModes & CompressionOperation.Decompress) == CompressionOperation.None)
            {
                messagePoster("Compression type doesn't support decompression.");
                return;
            }
            byte[] file;
            try
            {
                BinaryReader br = new BinaryReader(File.OpenRead(input));
                file = br.ReadBytes((int)br.BaseStream.Length);
                br.Close();
            }
            catch (IOException)
            {
                messagePoster("Error while opening input file.");
                return;
            }
            if (inputOffset > file.Length)
            {
                messagePoster("Offset is larger than the size of the file.");
                return;
            }


            byte[] decompData = compression.Decompress(file, inputOffset);
            if (decompData == null)
            {
                messagePoster("Data can't be decompressed.");
                return;
            }

            try
            {
                BinaryWriter bw = new BinaryWriter(File.Open(output, FileMode.OpenOrCreate));
                bw.BaseStream.Position = outputOffset;
                bw.Write(decompData);
                bw.Close();
            }
            catch (IOException)
            {
                messagePoster("Error while opening output file.");
                return;
            }
            messagePoster("Finished");
        }

        static void Scan(Compression compression, string input, string output, int offset, int lenght)
        {
            if ((compression.supportedModes & CompressionOperation.Scan) == CompressionOperation.None)
            {
                messagePoster("Compression type doesn't support scanning.");
                return;
            }
            byte[] file;
            try
            {
                BinaryReader br = new BinaryReader(File.OpenRead(input));
                file = br.ReadBytes((int)br.BaseStream.Length);
                br.Close();
            }
            catch (IOException)
            {
                messagePoster("Error while opening input file.");
                return;
            }

            if (lenght == 0 || lenght + offset > file.Length)
                lenght = file.Length - offset;

            int[] result = compression.Scan(file, offset, lenght, 4, 0, 0x100000);

            if (result.Length == 0)
                messagePoster("No compressed offsets were found.");
            else
            {
                StreamWriter sw = new StreamWriter(output);
                for (int i = 0; i < result.Length; i++)
                    sw.WriteLine(Convert.ToString(result[i], 16));
                sw.Close();
                messagePoster("Scanning was successful.");
            }
        }

        static void CompLength(Compression compression, string input, int offset)
        {
            if ((compression.supportedModes & CompressionOperation.CompLength) == CompressionOperation.None)
            {
                messagePoster("Compression type doesn't support length checking.");
                return;
            }
            byte[] file;
            try
            {
                BinaryReader br = new BinaryReader(File.OpenRead(input));
                file = br.ReadBytes((int)br.BaseStream.Length);
                br.Close();
            }
            catch(IOException)
            {
                messagePoster("Error while opening the file.");
                return;
            }

            if (offset > file.Length)
                messagePoster("Offset is larger than the size of the file.");
            else
            {
                int result = compression.CompressedLength(file, offset);
                if (result == -1)
                    messagePoster("Data can't be decompressed");
                else
                    messagePoster("Length of the compressed data is: 0x" + Convert.ToString(result,16));
            }

        }

        static void DecompLength(Compression compression, string input, int offset)
        {
            if ((compression.supportedModes & CompressionOperation.DecompLength) == CompressionOperation.None)
            {
                messagePoster("Compression type doesn't support length checking.");
                return;
            }
            byte[] file;
            try
            {
                BinaryReader br = new BinaryReader(File.OpenRead(input));
                file = br.ReadBytes((int)br.BaseStream.Length);
                br.Close();
            }
            catch (IOException)
            {
                messagePoster("Error while opening the file.");
                return;
            }

            if (offset > file.Length)
                messagePoster("Offset is larger than the size of the file.");
            else
            {
                int result = compression.DecompressedDataLenght(file, offset);
                if (result == -1)
                    messagePoster("Data can't be decompressed");
                else
                    messagePoster("Length of the decompressed data is: 0x" + Convert.ToString(result, 16));
            }

        }
        

        static void Test(Compression compression, string originalFile, string report)
        {
            BinaryReader br = new BinaryReader(File.OpenRead(originalFile));
            byte[] file = br.ReadBytes((int)br.BaseStream.Length);
            br.Close();

            int[] offsetsToTest = compression.Scan(file, 0, file.Length, 4, 0, file.Length);
            int i;
            List<int> differentOffsets = new List<int>(50);

            for (i = 0; i < offsetsToTest.Length; i++)
            {
                bool success = true;
                byte[] data = compression.Compress(compression.Decompress(file, offsetsToTest[i]));
                byte[] data2 = new byte[compression.CompressedLength(file, offsetsToTest[i])];
                Array.Copy(file, offsetsToTest[i], data2, 0, data2.Length);
                success = data.Length <= data2.Length;
                /*for (int j = 0; j < data.Length && success; j++)
                        success = data[j] == data2[j];*/
                if (!success)
                    differentOffsets.Add(offsetsToTest[i]);

            }

            if (differentOffsets.Count == 0)
            {
                messagePoster("No differences were found.");
            }
            else
            {
                messagePoster("There is a difference.");
                StreamWriter sw = new StreamWriter(report, false);
                for (int j = 0; j < differentOffsets.Count; j++)
                {
                    sw.WriteLine(Convert.ToString(differentOffsets[j], 16));
                }
                sw.Close();
            }
        }
    }
}