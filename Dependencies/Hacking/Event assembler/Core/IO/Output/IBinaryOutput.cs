using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Event_Assembler.Core.IO.Output
{
    interface IBinaryOutput : IDisposable
    {
        int Offset { get; set; }

        void Write(byte[] data);
        void Write(byte[] data, int index);
        void Write(byte[] data, int index, int length);        
    }
}
