using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Nintenlord.Event_Assembler.Core.IO;
using Nintenlord.Event_Assembler.Core.IO.Input;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors
{
    /// <summary>
    /// Preproserror that does nothing.
    /// </summary>
    public class NopPreprocessor : IPreprocessor
    {
        #region IPreprocessor Members

        public void AddDefined(string[] original)
        {
            
        }

        public void AddReserved(string[] reserved)
        {
            
        }

        public string Process(string line, IInputStream inputStream)
        {
            return line;
        }

        public void Dispose()
        {
            
        }

        #endregion
    }
}
