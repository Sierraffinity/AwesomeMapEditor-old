using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Mappy_Map2D;

namespace ContentPipelineExtension_MapFromMappy
{
    class ListCustomContentTypeWriter : ContentTypeWriter<List<Tile>>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "System.Collections.Generic.List<T>, System";
        }

        protected override void Write(ContentWriter output, List<Tile> value)
        {
            List<Tile> tiles = (List<Tile>)value;

            foreach (Tile t in tiles)
            {
                output.WriteObject<Tile>(t, new TileCustomContentTypeWriter());
            }
        }
    }
}
