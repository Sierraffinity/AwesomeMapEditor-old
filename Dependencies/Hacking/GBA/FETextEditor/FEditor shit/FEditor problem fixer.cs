using System;
using System.IO;
using Nintenlord.ROMHacking.GBA;
using Nintenlord.ROMHacking.GBA.MemoryManagement;

namespace FETextEditor.FEditor_shit
{
    static class FEditorSucks
    {
        public static void RemoveAndReadFooter(GBAROM rom, GBAMemoryManager memMan)
        {
            Stream stream = rom.GetStream();
            stream.Seek(-12, SeekOrigin.End);
            int length;
            uint crc32;
            using (BinaryReader reader = new BinaryReader(stream))
            {
                length = reader.ReadInt32();
                int signature = reader.ReadInt32();
                crc32 = reader.ReadUInt32();
                if (signature != 0x1270D1FE)
                {
                    throw new Exception("No footer present");
                }
                
                stream.Seek(12 + length + 12, SeekOrigin.End);

                byte[] version = reader.ReadBytes(12); //Like we care

                while (stream.Position < stream.Length - 12)
                {
                    byte[] rawid = reader.ReadBytes(4);
                    string id = new string(Array.ConvertAll(rawid, Convert.ToChar));
                    int chunkLength = reader.ReadInt32();
                    if (id == "Free")
                    {
                        long endOffset = stream.Position + length;
                        int offset = 0;
                        while (stream.Position < endOffset)
                        {
                            int relativeOffset = reader.ReadEncodedInteger();
                            offset += relativeOffset;
                            int freeLength = reader.ReadEncodedInteger();
                            memMan.AddManagedSpace(offset, freeLength);
                            offset += freeLength;
                        }
                    }
                    else
                    {
                        stream.Seek(chunkLength, SeekOrigin.Current);
                    }
                }
            }

            rom.Length -= 12 + length + 12; //Remove footer
            
        }

        private static int ReadEncodedInteger(this BinaryReader reader)
        {
            int value = reader.ReadByte();
            int toRead = value & 0x3;
            int i = 1;
            while (toRead > 0)
            {
                value |= reader.ReadByte() << (i * 8);
                i++;
                toRead--;
            }
            return value;
        }

        public static bool HasFooter(GBAROM rom)
        {
            Stream stream = rom.GetStream();
            stream.Seek(-12, SeekOrigin.End);
            bool result = true;
            using (BinaryReader reader = new BinaryReader(stream))
            {
                int length = reader.ReadInt32();
                int signature = reader.ReadInt32();
                uint crc32 = reader.ReadUInt32();
                result = signature == 0x1270D1FE;
            }
            return result;
        }

        public static void AHCopy(Stream source, Stream dest)
        {
            var start = dest.Position;
            while (true)
            {
                int b = source.ReadByte();

                if (b == -1)
                {
                    break;
                }
                dest.WriteByte((byte)b);

                if (b == 0)
                {
                    break;
                }

                else if ((b >= 0 && b < 0x20) || b == 0x80)
                {
                    if (!dest.EvenNumberAdvanced(start))
                    {
                        dest.WriteByte((byte)0);
                    }
                }

                if (b == 0x80 || b == 0x10)//2 part codes
                {
                    int b2 = source.ReadByte();
                    int b3 = source.ReadByte();
                    dest.WriteByte((byte)b2);
                    dest.WriteByte((byte)b3);
                }
            }

            if (!dest.EvenNumberAdvanced(start))
            {
                dest.WriteByte(0);
            }
        }

        private static bool EvenNumberAdvanced(this Stream stream, long start)
        {
            return ((stream.Position - start) & 1) == 0;
        }
    }
}
