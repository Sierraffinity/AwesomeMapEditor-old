using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Nintenlord.Collections;
using Nintenlord.Utility;
using Nintenlord.ROMHacking.GBA.Compressions;
using DSDecmp.Formats.Nitro;

namespace DSTextFileEditor
{
    class Program
    {
        static Encoding encoding = Encoding.GetEncoding("shift_jis");

        static void Main(string[] args)
        {
#if DEBUG
            Test();
#else
            if (args.Length >= 3)
            {
                string inputFile = args[1];
                string outputFile = args[2];
                if (args[0].Equals("dissect", StringComparison.OrdinalIgnoreCase))
                {
                    using (Stream input = File.OpenRead(inputFile))
                    {
                        using (StreamWriter output = new StreamWriter(File.Create(outputFile), encoding))
                        {
                            int test = input.ReadByte();
                            input.Seek(-1, SeekOrigin.Current);
                            byte[] data;
                            switch (test)
                            {
                                case 0x10:
                                    byte[] data2 = new byte[input.Length];
                                    input.Read(data2, 0, data2.Length);
                                    data = LZ77.Decompress(data2, 0);
                                    break;
                                case 0x11:
                                    using(MemoryStream mem = new MemoryStream(1000))
                                    {
                                        LZ11.Decompress(input, mem);
                                        data = mem.ToArray();
                                    }
                                    break;
                                default:
                                    data = new byte[input.Length];
                                    input.Read(data, 0, data.Length);
                                    break;
                            }

                            using (MemoryStream input2 = new MemoryStream(data))
                            {
                                WriteText(input2, output);
                            }
                        }
                    }
                }
                else if (args[0].Equals("combine", StringComparison.OrdinalIgnoreCase))
                {
                    using (StreamReader input = new StreamReader(inputFile, encoding))
                    {
                        byte[] raw;
                        using (MemoryStream rawOutput = new MemoryStream(1000))
                        {
                            CompileText(input, rawOutput);
                            raw = rawOutput.ToArray();
                        }
                        byte[] lz11, lz77;
                        lz77 = LZ77.Compress(raw);
                        using (MemoryStream temp = new MemoryStream(raw))
                        {
                            using (MemoryStream temp2 = new MemoryStream(0x100))
                            {
                                LZ11.CompressWithLA(temp, raw.Length, temp2);
                                lz11 = temp2.ToArray();
                            }
                        }

                        using (Stream output = File.Create(outputFile))
                        {
                            if (lz11.Length < lz77.Length)
                            {
                                output.Write(lz11, 0, lz11.Length);
                            }
                            else
                            {
                                output.Write(lz77, 0, lz77.Length);
                            }
                        }
                    }
                }
            }
#endif
        }

        [System.Diagnostics.Conditional("DEBUG")]
        private static void Test()
        {
            using (StreamWriter writer = new StreamWriter(@"C:\Users\Timo\Documents\Pelitiedot\Fire Emblem\FE12\Text editing\test\decomd 000.txt"))
            {
                using (Stream stream = File.OpenRead(@"C:\Users\Timo\Documents\Pelitiedot\Fire Emblem\FE12\Text editing\test\decomd 000"))
                {
                    WriteText(stream, writer);
                }
            }
            using (Stream output = File.OpenWrite(@"C:\Users\Timo\Documents\Pelitiedot\Fire Emblem\FE12\Text editing\test\decomd 000.bin"))
            {
                using (StreamReader reader = new StreamReader(@"C:\Users\Timo\Documents\Pelitiedot\Fire Emblem\FE12\Text editing\test\decomd 000.txt"))
                {
                    CompileText(reader, output);
                }
            }
            using (StreamWriter writer = new StreamWriter(@"C:\Users\Timo\Documents\Pelitiedot\Fire Emblem\FE12\Text editing\test\decomd 000 result.txt"))
            {
                using (Stream stream = File.OpenRead(@"C:\Users\Timo\Documents\Pelitiedot\Fire Emblem\FE12\Text editing\test\decomd 000.bin"))
                {
                    WriteText(stream, writer);
                }
            }
        }

        static public void WriteText(Stream input, TextWriter output)
        {
            BinaryReader reader = new BinaryReader(input, encoding);
            Dictionary<string, string> results = GetTextEntries(reader);

            foreach (var item in results)
            {
                output.WriteLine(item.Key);
                output.Write(item.Value);
                output.WriteLine("[X]");
                output.WriteLine();
            }
        }

        private static Dictionary<string, string> GetTextEntries(BinaryReader reader)
        {
            long start = reader.BaseStream.Position;

            //Header
            int fileSize = reader.ReadInt32();
            int lengthOfTextArea = reader.ReadInt32();
            reader.ReadInt32(); //Always zero
            int amountOfTextEntries = reader.ReadInt32();
            reader.ReadInt32(); //Always zero
            reader.ReadInt32(); //Always zero
            reader.ReadInt32(); //Always zero
            reader.ReadInt32(); //Always zero

            reader.BaseStream.Seek(lengthOfTextArea, SeekOrigin.Current);

            Tuple<int, int>[] textEntries = new Tuple<int, int>[amountOfTextEntries];

            for (int i = 0; i < textEntries.Length; i++)
            {
                int textOffset = reader.ReadInt32();
                int textIDOffset = reader.ReadInt32();
                textEntries[i] = new Tuple<int, int>(textOffset, textIDOffset);
            }

            string[] textIDs = new string[amountOfTextEntries];
            StringBuilder bldr = new StringBuilder();
            {
                long textIDStart = reader.BaseStream.Position;
                for (int i = 0; i < textIDs.Length; i++)
                {
                    reader.BaseStream.Position = textIDStart + textEntries[i].Item2;
                    GetText2(reader, bldr);
                    textIDs[i] = bldr.ToString();
                    bldr.Clear();
                }
            }

            string[] rawTexts = new string[amountOfTextEntries];
            for (int i = 0; i < textIDs.Length; i++)
            {
                reader.BaseStream.Position = start + 0x20 + textEntries[i].Item1;
                GetText2(reader, bldr);
                rawTexts[i] = bldr.ToString();
                bldr.Clear();
            }

            Dictionary<string, string> results = new Dictionary<string, string>(rawTexts.Length);
            for (int i = 0; i < textIDs.Length; i++)
            {
                results[textIDs[i]] = rawTexts[i];
            }
            return results;
        }

        private static void GetText(BinaryReader reader, StringBuilder bldr)
        {
            byte latest;
            while ((latest = reader.ReadByte()) != 0)
            {
                if (latest == 0xA)
                {
                    bldr.AppendLine();
                }
                else if (latest == 0xB)
                {
                    byte second = reader.ReadByte();
                    bldr.AppendFormat("[SpecialCommand0xB=0x{0}]", second.ToString("X2"));
                }
                else if (latest == 0x1)
                {
                    byte second = reader.ReadByte();
                    bldr.AppendFormat("[YourFace=0x{0}]", second.ToString("X2"));
                }
                else if (latest < 0x20)
                {
                    bldr.AppendFormat("[0x{0}]", latest.ToString("X2"));
                }
                else
                {
                    byte[] bytes;
                    if (reader.PeekChar() < 0x20)
                    {
                        bytes = new byte[] { latest };
                    }
                    else
                    {
                        bytes = new byte[] { latest, reader.ReadByte() };
                    }
                    bldr.Append(encoding.GetChars(bytes));
                }
            }
        }

        private static void GetText2(BinaryReader reader, StringBuilder bldr)
        {
            List<byte> bytes = new List<byte>(100);
            while (true)
            {
                byte latest = reader.ReadByte();
                if (latest == 0)
                    break;
                
                if (latest == 0xA)
                {
                    bldr.Append(encoding.GetChars(bytes.ToArray()));
                    bytes.Clear();
                    bldr.AppendLine();
                }
                else if (latest == 0xB)
                {
                    bldr.Append(encoding.GetChars(bytes.ToArray()));
                    bytes.Clear();
                    byte second = reader.ReadByte();
                    bldr.AppendFormat("[SpecialCommand0xB=0x{0}]", second.ToString("X2"));
                }
                else if (latest == 0x1)
                {
                    bldr.Append(encoding.GetChars(bytes.ToArray()));
                    bytes.Clear();
                    byte second = reader.ReadByte();
                    bldr.AppendFormat("[YourFace=0x{0}]", second.ToString("X2"));
                }
                else if (latest < 0x20)
                {
                    bldr.Append(encoding.GetChars(bytes.ToArray()));
                    bytes.Clear();
                    bldr.AppendFormat("[0x{0}]", latest.ToString("X2"));
                }
                else
                {
                    bytes.Add(latest);
                }
            }
            if (bytes.Count > 0)
            {
                bldr.Append(encoding.GetChars(bytes.ToArray()));
            }
        }

        static public void CompileText(TextReader input, Stream output)
        {
            var entries = GetEntries(input);

            var rawEntries = new Dictionary<byte[], byte[]>(entries.Count);

            foreach (var item in entries)
            {
                var codes = FindCodes(item.Value);
                using (BinaryWriter writer = new BinaryWriter(new MemoryStream(), encoding))
                {
                    WriteText(writer, codes);
                    rawEntries[encoding.GetBytes(item.Key)] = 
                        ((writer.BaseStream) as MemoryStream).ToArray();
                }
            }

            int textLength = rawEntries.Values.ConvertAll(x => x.Length).Sum().ToMod(4);
            int textIDLength = rawEntries.Keys.ConvertAll(x => x.Length + 1).Sum();

            int fileLength = 0x20 + textLength + entries.Count * 8 + textIDLength;
            
            int textIDPos = 0;
            int textPos = 0;
            List<Tuple<int, int>> offsets = new List<Tuple<int, int>>();
            foreach (var item in rawEntries)
            {
                offsets.Add(new Tuple<int, int>(textPos, textIDPos));
                textPos += item.Value.Length;
                textIDPos += item.Key.Length + 1;
            }

            BinaryWriter writer2 = new BinaryWriter(output);
            //Header
            writer2.Write(fileLength);
            writer2.Write(textLength);
            writer2.Write(0); //Always zero
            writer2.Write(entries.Count);
            writer2.Write(0); //Always zero
            writer2.Write(0); //Always zero
            writer2.Write(0); //Always zero
            writer2.Write(0); //Always zero

            foreach (var item in rawEntries.Values)
            {
                writer2.Write(item);
            }
            writer2.BaseStream.Position = ((int)writer2.BaseStream.Position).ToMod(4);
            foreach (var item in offsets)
            {
                writer2.Write(item.Item1);
                writer2.Write(item.Item2);
            }

            foreach (var item in rawEntries.Keys)
            {
                writer2.Write(item);
                writer2.Write((byte)0);
            }
        }

        private static List<Text> FindCodes(string text)
        {
            List<Text> codes = new List<Text>(text.Length);
            StringBuilder bldr = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '[')
                {
                    if (bldr.Length > 0)
                    {
                        Text textCode = new Text();
                        textCode.isText = true;
                        textCode.text = bldr.ToString();
                        codes.Add(textCode);
                    }
                    bldr.Clear();

                    bldr.Append(text[i]);
                }
                else if (text[i] == ']')
                {
                    bldr.Append(text[i]);
                    if (bldr.Length > 0)
                    {
                        Text textCode = new Text();
                        textCode.isText = false;
                        textCode.text = bldr.ToString();
                        codes.Add(textCode);
                    }
                    bldr.Clear();
                }
                else if (text[i] != '\r') //Screw MAC users
                {
                    bldr.Append(text[i]);
                }
            }

            if (bldr.Length > 0)
            {
                Text textCode = new Text();
                textCode.isText = true;
                textCode.text = bldr.ToString();
                codes.Add(textCode);
            }
            return codes;
        }

        private static void WriteText(BinaryWriter output, IEnumerable<Text> codes)
        {
            foreach (Text item in codes)
            {
                if (item.isText)
                {
                    //causes length of the string to 
                    //be written before the string itself
                    //output.Write(item.text); 

                    output.Write(item.text.ToCharArray());
                }
                else
                {
                    int val;
                    string code = item.text.Trim('[', ']');
                    if (code.TryGetValue(out val))
                    {
                        output.Write((byte)val);
                    }
                    else if (code == "X")
                    {
                        output.Write((byte)0);
                    }
                    else if (code.StartsWith("SpecialCommand0xB="))
                    {
                        output.Write((byte)0xB);
                        int eqIndex = code.IndexOf('=');
                        string end = code.Substring(eqIndex + 1);
                        if (end.TryGetValue(out val))
                        {
                            output.Write((byte)val);
                        }
                    }
                    else if (code.StartsWith("YourFace="))
                    {
                        output.Write((byte)0x1);
                        int eqIndex = code.IndexOf('=');
                        string end = code.Substring(eqIndex + 1);
                        if (end.TryGetValue(out val))
                        {
                            output.Write((byte)val);
                        }
                    }
                }
            }
        }
        
        private static IDictionary<string, string> GetEntries(TextReader input)
        {
#if DEBUG
            var entries = new ArrayDictionary<string, string>();
#else
            var entries = new Dictionary<string, string>();
#endif
            string line;

            StringBuilder bldr = new StringBuilder();
            while ((line = input.ReadLine()) != null)
            {
                if (line.Length == 0)
                    continue;

                string textID = line.Trim();

                int rawChar;
                bool isCode = false;
                bool stopAtNext = false;
                while ((rawChar = input.Read()) != -1)
                {
                    char c = Convert.ToChar(rawChar);
                    if (c == '[')
                    {
                        isCode = true;
                    }
                    else if (c == ']')
                    {
                        isCode = false;
                        if (stopAtNext)
                        {
                            bldr.Append(c);
                            break; //Only valid exit point
                        }
                    }
                    else if (c == 'X' && isCode)
                    {
                        stopAtNext = true;
                    }

                    bldr.Append(c);
                }
                entries[textID] = bldr.ToString();
                bldr.Clear();
            }
            return entries;
        }

        private struct Text
        {
            public string text;
            public bool isText;

            public override string ToString()
            {
                return text;
            }
        }
    }
}
