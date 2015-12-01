using System;
using System.Collections.Generic;
using System.Text;
using Mappy_Map2D;
using Microsoft.Xna.Framework.Content;

namespace Mappy_Map2D
{
    internal class TileCustomContentTypeReader : ContentTypeReader<Tile>
    {
        protected override Tile Read(ContentReader input, Tile existingInstance)
        {
            Tile tile = new Tile();

            tile.Walkable = input.ReadBoolean();
            tile.Id = input.ReadInt32();
            tile.Width = input.ReadInt32();
            tile.Height = input.ReadInt32();            

            return tile;
        }
    }
}
