using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mappy_Map2D
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content
    /// Pipeline to read the specified data type from binary .xnb format.
    /// 
    /// Unlike the other Content Pipeline support classes, this should
    /// be a part of your main game project, and not the Content Pipeline
    /// Extension Library project.
    /// </summary>
    public class MapContentReader : ContentTypeReader<Map2D>
    {
        protected override Map2D Read(ContentReader input, Map2D existingInstance)
        {
            string header = ReadString(input, 4);
            if (header != "FORM")
                return null;

            int fileSize = readValue(input, 4, true);
            long startingPosition = input.BaseStream.Position;

            header = ReadString(input, 4);
            if (header != "FMAP")
                return null;

            List<Chunk> chunks = new List<Chunk>(10);

            while (input.BaseStream.Position - startingPosition < fileSize)
            {
                Chunk chunk;
                chunk.name = ReadString(input, 4);
                int length = readValue(input, 4, true);
                chunk.data = input.ReadBytes(length);
            }

            int HeaderOffset = 0;
            while (HeaderOffset < chunks.Count && chunks[HeaderOffset].name != "MPHD")
                HeaderOffset++;

            if (HeaderOffset == chunks.Count)
                return null;

            

            return null;
        }

        int readValue(ContentReader reader, int amountOfBytes, bool bigEndian)
        {
            int value = 0;
            if (bigEndian)
            {
                for (int i = 0; i < amountOfBytes; i++)
                {
                    value >>= 8;
                    value += reader.ReadByte() << ((amountOfBytes - 1) * 8);
                }
            }
            else
            {
                for (int i = 0; i < amountOfBytes; i++)
                {
                    value <<= 8;
                    value += reader.ReadByte();
                }
            }
            return value;
        }

        string ReadString(ContentReader reader, int length)
        {
            char[] characters = reader.ReadChars(4);
            return new string(characters);
        }


        private struct Chunk
        {
            public string name;
            public byte[] data;
        }
    }
}
