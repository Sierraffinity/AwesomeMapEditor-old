using System;
using System.Collections.Generic;
using System.Text;
using Mappy_Map2D;
using Microsoft.Xna.Framework.Content;

namespace Mappy_Map2D
{
    internal class BlockContentTypeReader : ContentTypeReader<Block>
    {

        protected override Block Read(ContentReader input, Block existingInstance)
        {
            Block block = new Block();
            block.bgoff = input.ReadInt32();
            block.fgoff = input.ReadInt32();
            block.fgoff2 = input.ReadInt32();
            block.fgoff3 = input.ReadInt32();


            block.User1 = input.ReadUInt32();
            block.User2 = input.ReadUInt32();
            block.User3 = input.ReadUInt16();
            block.User4 = input.ReadUInt16();
            block.User5 = input.ReadByte();
            block.User6 = input.ReadByte();
            block.User7 = input.ReadByte();

            block.miscData = input.ReadByte();

            return block;
        }
    }
}
