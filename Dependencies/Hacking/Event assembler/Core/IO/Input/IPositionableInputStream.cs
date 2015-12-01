using System;
namespace Nintenlord.Event_Assembler.Core.IO.Input
{
    public interface IPositionableInputStream : IDisposable
    {
        int LineNumber { get; }
        string CurrentFile { get; }

        string PeekOriginalLine();

        string ReadLine();
    }
}
