using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.HackingProjectManager.IO
{
    interface IReader
    {
        int Read();
        int Read(byte[] buffer, int index, int count);
        int Read(char[] buffer, int index, int count);
        bool ReadBoolean();
        byte ReadByte();
        byte[] ReadBytes(int count);
        char ReadChar();
        char[] ReadChars(int count);
        decimal ReadDecimal();
        double ReadDouble();
        short ReadInt16();
        int ReadInt32();
        long ReadInt64();
        sbyte ReadSByte();
        float ReadSingle();
        string ReadString();
        ushort ReadUInt16();
        uint ReadUInt32();
        ulong ReadUInt64();
    }
}
