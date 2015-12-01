using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Xml.Serialization;

namespace Nintenlord.ROMHacking
{
    public class BinaryNLZfile
    {
        int version;

        const string freeSpaceS = "FreeSpace";
        SortedDictionary<int, int> freeSpace;

        const string checkSumS = "CheckSum";
        uint checkSum;

        //const string appliedPatchesS = "AppliedPatches";
        //List<string> appliedPatches;
        
        public BinaryNLZfile(string file)
        {
            Stream stream = File.OpenRead(file);
            ReadData(stream);
            stream.Close();
        }

        public BinaryNLZfile(Stream stream)
        {
            ReadData(stream);
        }

        private void ReadData(Stream stream)
        {
            BinaryReader reader = new BinaryReader(stream);
            string header = new string(reader.ReadChars(4));
            if (header.StartsWith("NLZ") && char.IsNumber(header[3]))
            {
                version = header[3] - '0';
                string blockName = reader.ReadString();
                uint blockSize = reader.ReadUInt32();

                long currentPosition = reader.BaseStream.Position;
                switch (blockName)
                {
                    case freeSpaceS:
                        FreeSpace(reader);
                        break;
                    case checkSumS:
                         reader.ReadUInt32();
                        break;
                    default:
                        reader.BaseStream.Position += blockSize;
                        break;
                }

            }
            else
            {
                throw new InvalidDataException("Data isn't in NLZ format");
            }
        }

        private void FreeSpace(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public BinaryNLZfile(int version)
        {
            this.version = version;
        }

        public void WriteData(string file)
        {
            BinaryWriter writer = new BinaryWriter(File.OpenWrite(file));
        }

        struct DataBlock<T>
        {
            string name;
            Action<T> writer;

        }
    }
}
