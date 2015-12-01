using System;
using System.Collections.Generic;
using System.Text;
using Nintenlord.Event_Assembler.Core;

namespace Nintenlord.Event_Assembler.Core.GBA
{
    /// <summary>
    /// Makes GBA ROM pointers from ROM offsets
    /// </summary>
    public class GBAPointerMaker : IPointerMaker
    {
        #region IPointerMaker Members

        public int MakePointer(int offset)
        {
            if (offset == 0)
                return 0;
            return offset | 0x8000000;
        }

        public int MakeOffset(int pointer)
        {
            return pointer & 0x1FFFFFF;
        }

        public bool IsAValidPointer(int pointer)
        {
            return (pointer == 0) || (pointer >> 25 == 0x04);
        }

        #endregion
    }
}
