using System;
using System.Collections.Generic;
using Nintenlord.Event_Assembler.Core.IO.Logs;
using Nintenlord.Utility;
using Nintenlord.Event_Assembler.Core.Code.Language.Expression;

namespace Nintenlord.Event_Assembler.Core.Code.Templates
{
    /// <summary>
    /// Template for raw hex code
    /// </summary>
    [Obsolete()]
    class RawCodeTemplate : ICodeTemplate, IFixedDocString
    {
        const string name = "CODE";
        const string docString =
@"
CODE 0xByte1 0xByte2 ... 0xByteN
CODE $HexWord1 $HexWord2 ... $HexWordN
CODE DecimalWord1 DecimalWord2 ... DecimalWordN
CODE *Any mixture of previous parameters*

 Writes raw code the the current offset. 
 Parameters starting with 0x are considered to be bytes, while
 all other parameters are considered to be words (4 bytes).
";
        public string DocString
        {
            get { return docString; }
        }

        StringComparer comparer;

        public RawCodeTemplate() : this(StringComparer.OrdinalIgnoreCase)
        {
        }

        public RawCodeTemplate(StringComparer comparer)
        {
            this.comparer = comparer;
        }

        #region ICodeTemplate Members

        public string Name
        {
            get { return name; }
        }
        public int MaxRepetition
        {
            get { return 4; }
        }
        public bool EndingCode
        {
            get { return false; }
        }
        public int OffsetMod
        {
            get { return 1; }
        }
        public int AmountOfFixedCode
        {
            get { return 0; }
        }


        public bool Matches(byte[] data, int offset)
        {
            return offset < data.Length;
        }

        public CanCauseError<string[]> GetAssembly(byte[] code, int offset)
        {
            string[] assembly = new string[GetLengthBytes(code, offset) + 1];
            assembly[0] = name;

            for (int i = 0; i < assembly.Length - 1; i++)
            {
                assembly[1 + i] = code[offset + i].ToHexString("0x");
            }
            return assembly;
        }
        
        public int GetLengthBytes(byte[] code, int offset)
        {
            return Math.Min(4, code.Length - offset);
        }

        
        public bool Matches(Language.Types.Type[] code)
        {
            throw new NotImplementedException();
        }

        public int GetLengthBytes(Parameter<int>[] code)
        {
            throw new NotImplementedException();
        }

        public CanCauseError<byte[]> GetData(Parameter<int>[] code, Func<string, int?> getSymbolValue)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IParameterized Members

        public int MinAmountOfParameters
        {
            get { return -1; }
        }

        public int MaxAmountOfParameters
        {
            get { return -1; }
        }

        #endregion

        public override string ToString()
        {
            return name;
        }
    }
}
