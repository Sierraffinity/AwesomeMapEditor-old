using System;
using System.Collections.Generic;
using System.Text;

namespace ContentPipelineExtension1.Animation
{
    public struct Animation
    {
        public enum AnimationType
        {
            END = -1,
            NONE = 0,
            LOOPF = 1,
            LOOPR = 2,
            ONCE = 3,
            ONCEH = 4,
            PPFF = 5,
            PPRR = 6,
            PPRF = 7,
            PPFR = 8,
            ONCES = 9
        }

        AnimationType type;
        byte delay;
        byte count;
        byte userInfo;
        int currentOffset;
        int startOffset;
        int endOffset;
    }
}
