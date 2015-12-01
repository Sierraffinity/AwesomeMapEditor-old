using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using ContentPipelineExtension_MapFromMappy.Mappy;
using Mappy_Map2D;

namespace ContentPipelineExtension_MapFromMappy
    {
        class BlockContentTypeWriter : ContentTypeWriter<Block>
        {
            public override string GetRuntimeReader(Microsoft.Xna.Framework.TargetPlatform targetPlatform)
            {
                return "ContentPipelineExtension_MapFromMappy.Mappy.BLKSTR, ContentPipelineExtension_MapFromMappy";
            }

            protected override void Write(ContentWriter output, Block value)
            {
                output.Write(value.bgoff);
                output.Write(value.fgoff);
                output.Write(value.fgoff2);
                output.Write(value.fgoff3);

                output.Write(value.User1);
                output.Write(value.User2);
                output.Write(value.User3);
                output.Write(value.User4);
                output.Write(value.User5);
                output.Write(value.User6);
                output.Write(value.User7);

                output.Write(value.miscData);
            }
        }
    }
