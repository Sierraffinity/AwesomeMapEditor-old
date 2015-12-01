using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FETextEditor.TextFormatter;
using Nintenlord.ROMHacking;
using Nintenlord.Collections.Trees;
using Nintenlord.IO;
using Nintenlord.Utility;
using Nintenlord.Collections;
using FETextEditor.DefaultFreeSpace;
using Nintenlord.ROMHacking.GBA;
using Nintenlord.ROMHacking.GBA.MemoryManagement;

namespace FETextEditor
{
    class App : IApplication
    {
        IUserInterface ui;
        Encoding encoding;

        GBAROM rom;
        GameSpecificData data;
        GBAMemoryManager memoryManager;

        IMetadataHandler metaDataHandler;
        GBAPointer[] textPointers;
        bool[] compressed;//True if Huffman comopressed, else false.
        BinaryTree<ushort> huffmanTree;
        IDictionary<ushort, bool[]> huffmanEncoding;

        public App()
        {
            rom = new GBAROM();
            encoding = new GBATextEncoding();
            metaDataHandler = new NoMetadata();
        }

        private void LoadRawTextOffsets(int offset, int length)
        {
            byte[] ptrs = rom.GetData(offset, length);
            compressed = new bool[length / 4];
            textPointers = new GBAPointer[length / 4];
            for (int i = 0; i < textPointers.Length; i++)
            {
                int val = BitConverter.ToInt32(ptrs, i * 4);
                int textOffset = Pointer.GetOffset(val & 0x7FFFFFFF);
                bool compressedText =  val >= 0;
                int size = GetLength(textOffset, compressedText);

                compressed[i] = compressedText;
                textPointers[i] = memoryManager.AddManagedSpace(textOffset, size, true);
            }
        }

        private int GetLength(int offset, bool compressed)
        {
            int result;
            Stream ROMStream = rom.GetStream();
            ROMStream.Position = offset;
            if (compressed)
            {
                result = Huffman.GetCompDataLength<ushort>(ROMStream, x => x == 0, huffmanTree);
            }
            else
            {
                result = 0;
                BinaryReader reader = new BinaryReader(ROMStream);
                while (true)
                {
                    result++;
                    byte val = reader.ReadByte();
                    if (val == 0)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        private void LoadHuffmanTree(int offset, int end)
        {
            int length = end - offset;
            byte[] data = rom.GetData(offset, length);
            huffmanTree = Program.GetHuffmanTree(data, 0, length / 4);
            huffmanEncoding = new Dictionary<ushort, bool[]>(huffmanTree.Count);
            var list = new List<bool>(huffmanTree.MaxDepth);
            huffmanTree.Head.AddLeafValues(huffmanEncoding, list);
            list.Clear();
        }
        
        private void WriteOffsets()
        {
            for (int i = 0; i < textPointers.Length; i++)
            {
                if (compressed[i])
                {
                    rom.InsertData(
                        i * 4 + data.TextPointerTableOffset,
                        Pointer.MakePointer(textPointers[i].Offset));
                }
                else
                {
                    rom.InsertData(
                        i * 4 + data.TextPointerTableOffset,
                        (uint)(Pointer.MakePointer(textPointers[i].Offset)
                        & 0x80000000));
                }
            }
        }

        private string SetHexData(byte[] data, int grouping, int lining, int offset)
        {
            var bldr = new StringBuilder(data.Length * 3);

            int groupCount = 0;
            int lineCount = 0;
            bldr.AppendLine(offset.ToHexString("$"));
            for (int i = 0; i < data.Length; i++)
            {
                bldr.Append(Convert.ToString(data[i], 16).PadLeft(2, '0'));

                groupCount++;
                if (groupCount == grouping)
                {
                    lineCount++;
                    if (lineCount == lining)
                    {
                        bldr.AppendLine();
                        lineCount = 0;
                    }
                    else
                    {
                        bldr.Append(" ");
                    }
                    groupCount = 0;
                }
            }
            return bldr.ToString();
        }


        private void WriteAllTexts(Stream input, TextWriter output)
        {
            for (int i = 0; i < textPointers.Length; i++)
            {
                int offset = textPointers[i].Offset;
                input.Position = offset;

                metaDataHandler.AddMetadata(i, textPointers[i], output);

                using (MemoryStream rawText = new MemoryStream(0x1000))
                {
                    string text = GetText(i, input, rawText);
                    output.Write(text);
                }
                output.WriteLine();
                output.WriteLine();
            }
        }
        
        private void InsertAllText(StreamReader reader, Stream ROMStream)
        {
            throw new NotImplementedException();
        }


        private void SaveXML()
        {
            DefaultFreeSpace.ROM XMLOptions = new DefaultFreeSpace.ROM();
            XMLOptions.CRC32 = CRC32.CalculateCRC32(new BinaryReader(rom.GetStream()));
            XMLOptions.GameCode = rom.GameCode;
            XMLOptions.GameTitle = rom.GameTitle;
            XMLOptions.MakerCode = rom.MakerCode;
            var freeSpace2 = memoryManager.GetFreeSpace();
            List<GBAPointer> temp2 = new List<GBAPointer>(freeSpace2);
            var freeSpace = freeSpace2.ConvertAll(
                x => 
                    (OffsetSizePair)
                    (Nintenlord.MemoryManagement.OffsetSizePair)
                    x
                );
            List<OffsetSizePair> temp = new List<OffsetSizePair>(freeSpace);
            XMLOptions.ROM_space.Add(new DefaultFreeSpace.Space("Free", freeSpace));
            XMLOptions.SaveToFile(Path.ChangeExtension(rom.ROMPath, ".xml"));
        }

        #region IApplication Members

        public void LoadText(int index)
        {
            if (rom.Opened)
            {
                Stream ROMStream = rom.GetStream();
                int offset = textPointers[index].Offset;
                ROMStream.Position = offset;
                using (MemoryStream rawText = new MemoryStream(0x1000))
                {
                    string text = GetText(index, ROMStream, rawText);

                    //ui.SetDisplayText(hex + Environment.NewLine + text);
                    ui.SetDisplayText(text);
                }
            }
        }

        private string GetText(int index, Stream ROMStream, MemoryStream rawText)
        {
            if (compressed[index])
            {
                Huffman.DecompressDataUntil(ROMStream, x => x == 0,
                    huffmanTree, new BinaryWriter(rawText));
            }
            else
            {
                FEditor_shit.FEditorSucks.AHCopy(ROMStream, rawText);
            }

            //var hex = SetHexData(rawText.ToArray(), 2, 8, offset);
            string text;
            try
            {
                text = encoding.GetString(rawText.GetBuffer(), 0,
                    (int)rawText.Position);
            }
            catch (Exception)
            {
                text = "Error";
            }
            return text;
        }

        public void LoadROM(string path)
        {
            rom.OpenROM(path);
            data = GameSpecificData.GetData(rom.GameCode);
            memoryManager = new GBAMemoryManager();

            bool ROMdataFound = false;
            if (File.Exists(Path.ChangeExtension(path, ".xml")))
            {

            }
            else
            {
                ROMdataFound = FEditor_shit.FEditorSucks.HasFooter(rom);
                if (ROMdataFound)
                {
                    FEditor_shit.FEditorSucks.RemoveAndReadFooter(rom, memoryManager);
                }
            }


            if (rom.Length < GBAROM.MaxRomSize)
            {
                memoryManager.AddManagedSpace(rom.Length, GBAROM.MaxRomSize - rom.Length);
            }
            else if (rom.Length > GBAROM.MaxRomSize)
            {
                throw new IOException("ROM is larger than allowed.");
            }

            if (!ROMdataFound)
            {
                //Check if ROM hasn't been edited. If hasn't, add default free space.
                uint fileCRC32;
                using (BinaryReader reader = new BinaryReader(rom.GetStream()))
                {
                    fileCRC32 = CRC32.CalculateCRC32(reader);
                }

                if (fileCRC32 == data.DefaultCRC32)
                {
                    foreach (var item in data.DefaultFreeSpace)
                    {
                        memoryManager.AddManagedSpace(item.Key, item.Value);
                    }
                } 
            }

            int start = rom.ReadPointer(data.TextPointerTableOffset);
            LoadHuffmanTree(
                rom.ReadPointer(data.HuffmanTreePtrOffset),
                rom.ReadPointer(data.HuffmanTreeEndPtrOffset));
            LoadRawTextOffsets(data.TextPointerTableOffset, data.TextPointerTableLength);
            ui.MaxIndex = textPointers.Length - 1;
        }
        
        public IUserInterface CurrentUI
        {
            get
            {
                return ui;
            }
            set
            {
                ui = value;
            }
        }
        
        public void SetText(string text, int index)
        {
            if (rom.Opened)
            {
                GBAPointer ptr = textPointers[index];
                var data = encoding.GetBytes(text);
                ushort[] data2 = data.RawCopy();

                using (MemoryStream compressedText = new MemoryStream(0x1000))
                {
                    byte[] dataToInsert;
                    Huffman.Compress<ushort>(huffmanEncoding, new BitWriter(compressedText), data2);
                    dataToInsert = compressedText.ToArray();
                    compressed[index] = true; //No reason to use decompressed data.

                    if (dataToInsert.Length <= ptr.Size)
                    {
                        rom.InsertData(ptr.Offset, dataToInsert);
                    }
                    else
                    {
                        memoryManager.Deallocate(ptr);
                        ptr = memoryManager.Allocate(dataToInsert.Length);
                        rom.InsertData(ptr.Offset, dataToInsert);
                        textPointers[index] = ptr;
                        WriteOffsets();
                    }
                }

            }
        }
        
        public void SaveROM()
        {
            memoryManager.CleanUp(rom);
            rom.SaveROM();
            SaveXML();
        }

        public void SaveROM(string path)
        {
            memoryManager.CleanUp(rom);
            rom.SaveROM(path);
            SaveXML();
        }
        
        public void DumbScript(string path)
        {
            if (rom.Opened)
            {
                Stream ROMStream = rom.GetStream();

                using (StringWriter writer = new StringWriter())
                {
                    WriteAllTexts(ROMStream, writer);
                    byte[] textData = Encoding.GetEncoding("shift_jis").GetBytes(writer.ToString());
                    File.WriteAllBytes(path, textData);
                }
            }       
        }

        public void InsertScript(string path)
        {
            if (rom.Opened)
            {
                Stream ROMStream = rom.GetStream();

                using (StreamReader reader = 
                    new StreamReader(File.OpenRead(path), 
                        Encoding.GetEncoding("shift_jis")))
                {
                    InsertAllText(reader, ROMStream);
                }            
                
            }
        }
        
        #endregion
    }
}
