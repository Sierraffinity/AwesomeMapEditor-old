using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
// TODO: replace these with the processor input and output types.

using TInput = System.String;

using System.Diagnostics;
using System.ComponentModel;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using ContentPipelineExtension_MapFromMappy.Mappy;

namespace ContentPipelineExtension_MapFromMappy
{
    
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentProcessor attribute to specify the correct
    /// display name for this processor.
    /// </summary>
    [ContentProcessor(DisplayName = "MappyMap Processor")]
    public class MapContentProcessor : ContentProcessor<TInput, MapContent>
    {

        [DisplayName("Tile Rows")]
        [Description("How many rows Do the Image of the Tiles have?")]
        [DefaultValue(typeof(int), "0")]
        [System.ComponentModel.DesignOnly(true)]
        public int TileRows { get; set; }

        [DisplayName("Tile Columns")]
        [Description("How many columns Do the Image of the Tiles have?")]
        [DefaultValue(typeof(int), "0")]
        [System.ComponentModel.DesignOnly(true)]
        public int TileColumns { get; set; }

        [DisplayName("Tile Image Name")]
        [Description("Image Name of the Tiles")]
        [DefaultValue(typeof(string), "")]
        [System.ComponentModel.DesignOnly(true)]
        public string TileImageName { get; set; }

        public override MapContent Process(TInput input, ContentProcessorContext context)
        {
            if (TileRows <= 0)
            {
                throw new PipelineException(@"Invalid Parameter from .FMP Content Processor;
Name of Parameter: Tile Rows;
Description: The Number Of Rows must be bigger than 0(zero).");
            }

            if (TileColumns <= 0)
            {
                throw new PipelineException(@"Invalid Parameter from .FMP Content Processor;
Name of Parameter: Tile Columns;
Description: The Number Of Columns must be bigger than 0(zero).");
            }
            
            if (TileImageName == null || TileImageName == "")
            {
                throw new PipelineException(@"Invalid Parameter from .FMP Content Processor;
Name of Parameter: Tile Image Name;
Description: Write the name of the Image Archive of the Tiles.");
            }

            // TODO: process the input object, and return the modified data.           

            MapContent mapContent = new MapContent();

            ExternalReference<TextureContent> external =
                    new ExternalReference<TextureContent>(TileImageName);           
            
            ExternalReference<TextureContent> textureRef = 
                context.BuildAsset<TextureContent, TextureContent>(
                                    external, "TextureProcessor");
                
            mapContent.TextureRef = textureRef;
            
            mapContent.LoadMap(input);

            mapContent.TileRows = this.TileRows;

            mapContent.TileColumns = this.TileColumns;
            
            return mapContent;
        }
    }
}