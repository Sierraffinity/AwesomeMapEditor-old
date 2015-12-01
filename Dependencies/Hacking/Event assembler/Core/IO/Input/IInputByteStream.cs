using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Event_Assembler.Core.IO.Input
{
    public interface IInputByteStream
    {
        int Offset { get; set; }
        int Length { get; }
        int BytesLeft { get; }

        byte[] ReadBytes(int amount);
        byte[] PeekBytes(int amount);

        int ReadInt32();
    }
}
