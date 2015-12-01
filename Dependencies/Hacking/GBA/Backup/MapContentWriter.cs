using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

// TODO: replace these with the processor input and output types.
using TInput = System.String;
using System.Diagnostics;
using Mappy_Map2D;
using ContentPipelineExtension_MapFromMappy.Mappy;

namespace ContentPipelineExtension_MapFromMappy
{

    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to write the specified data type into binary .xnb format.
    /// 
    /// TODO: change the ContentImporter attribute to specify the correct file
    /// extension, display name, and default processor for this importer.
    /// </summary>
    [ContentTypeWriter()]
    public class MapContentWriter : ContentTypeWriter<MapContent>
    {
        protected override void Write(ContentWriter output, MapContent value)
        {

            output.WriteObject<int>(value.Blocks.Count);
            
            foreach (Block block in value.Blocks)
            {
                output.WriteObject<Block>(block, new BlockContentTypeWriter());
            }
            
            output.WriteObject<int>(value.Informations.MapWidth);
            output.WriteObject<int>(value.Informations.MapHeight);
            output.WriteObject<int>(value.Informations.BlockWidth);
            output.WriteObject<int>(value.Informations.BlockHeight);
            

            output.WriteExternalReference<TextureContent>(value.TextureRef);

            for (int i = 0; i < value.Informations.MapWidth; i++)
            {
                for (int j = 0; j < value.Informations.MapHeight; j++)
                {
                    output.WriteObject<Tile>(value.Tiles[i][j],
                        new TileCustomContentTypeWriter());
                }
            }

            output.WriteObject<int>(value.TileRows);
            output.WriteObject<int>(value.TileColumns);
            output.WriteObject<int>(value.TotalTiles);
          
        }
        
        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(Map2D).AssemblyQualifiedName;
        }
        

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Mappy_Map2D.MapContentReader, Mappy_Map2D";
        }       
    }
}