using System;
using System.Linq;
using System.Collections.Generic;
using Nintenlord.Event_Assembler.Core.IO.Logs;
using Nintenlord.Collections;
using Nintenlord.Utility;
using Nintenlord.Event_Assembler.Core.Code.Language.Expression;

namespace Nintenlord.Event_Assembler.Core.Code.Templates
{
    /// <summary>
    /// Template for code filler
    /// </summary>
    /// <remarks>TODO: Replace with macro</remarks>
    class CodeFillerTemplate : ICodeTemplate, IFixedDocString
    {
        const string name = "FILL";
        const string docString =
@"
FILL Amount
FILL Amount Size
FILL Amount Size Value

 Fills current offset with Amount of Size Values.
 If Value doesn't fit into the Size, topmost bytes
 are ignored. Size is in bytes.
 
 Default values:
  Size = 1
  Value = 0
";
        public string DocString
        {
            get { return docString; }
        }
        
        StringComparer comparer;

        public CodeFillerTemplate() : this(StringComparer.OrdinalIgnoreCase)
        {
        }

        public CodeFillerTemplate(StringComparer comparer)
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
            get { return 1; }
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
            return false;
        }

        public int GetLengthBytes(byte[] code, int offset)
        {
            return 0;
        }

        public CanCauseError<string[]> GetAssembly(byte[] code, int offset)
        {
            return string.Empty.GetArray();
        }


        
        public bool Matches(Language.Types.Type[] code)
        {
            return code.Length.IsInRange(1, 3) && code.All(x => x.type == Language.Types.MetaType.Atom); 
        }

        public int GetLengthBytes(Parameter<int>[] code)
        {
            int amount, size = 1;

            var amountE = Folding.TryFold(code[1]);
            if (amountE.CausedError)
            {
                return 0;
            }
            else
            {
                amount = amountE.Result;
            }

            if (code.Length > 2)
            {
                var sizeE = Folding.TryFold(code[2]);
                if (sizeE.CausedError)
                {
                    return 0;
                }
                else
                {
                    size = sizeE.Result;
                }
            }

            return size * amount;            
        }

        public CanCauseError<byte[]> GetData(Parameter<int>[] code, Func<string, int?> getSymbolValue)
        {
            int amount, size, value;

            var amountE = Folding.TryFold(code[1]);
            if (amountE.CausedError)
            {
                return (amountE.ConvertError<int, byte[]>());
            }
            else
            {
                amount = amountE.Result;
            }

            if (code.Length > 2)
            {
                var sizeE = Folding.TryFold(code[2]);
                if (sizeE.CausedError)
                {
                    return (sizeE.ConvertError<int, byte[]>());
                }
                else
                {
                    size = sizeE.Result;
                }
            }
            else size = 1;

            if (code.Length > 3)
            {
                var valueE = Folding.TryFold(code[3]);
                if (valueE.CausedError)
                {
                    return (valueE.ConvertError<int, byte[]>());
                }
                else
                {
                    value = valueE.Result;
                }
            }
            else value = 0;

            byte[] byteCode = new byte[amount * size];
            byte[] bytes = BitConverter.GetBytes(value);

            for (int i = 0; i < amount; i++)
            {
                for (int u = 0; u < size && u + i * size < bytes.Length; u++)
                {
                    byteCode[u + i * size] = bytes[u];
                }
            }
            return byteCode;
        }

        #endregion

        #region IParameterized Members

        public int MinAmountOfParameters
        {
            get { return 1; }
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
