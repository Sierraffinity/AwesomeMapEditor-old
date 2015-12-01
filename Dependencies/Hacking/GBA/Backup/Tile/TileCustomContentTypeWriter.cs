using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Mappy_Map2D;

namespace ContentPipelineExtension_MapFromMappy
{
    class TileCustomContentTypeWriter : ContentTypeWriter<Tile>
    {
        public override string GetRuntimeReader(Microsoft.Xna.Framework.TargetPlatform targetPlatform)
        {
            return "Mappy_Map2D.Tile, Mappy_Map2D";
        }

        protected override void Write(ContentWriter output, Tile value)
        {
            output.Write(value.Walkable);
            output.Write(value.Id);
            output.Write(value.Width);
            output.Write(value.Height);   
        }
    }
}
