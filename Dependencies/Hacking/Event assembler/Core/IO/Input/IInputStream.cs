using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Event_Assembler.Core.IO.Input
{
    /// <summary>
    /// Only used for preprocessing.
    /// </summary>
    public interface IInputStream : IPositionableInputStream
    {
        void OpenSourceFile(string path);

        void OpenBinaryFile(string path);

        void AddNewLines(IEnumerable<string> lines);
    }
}
