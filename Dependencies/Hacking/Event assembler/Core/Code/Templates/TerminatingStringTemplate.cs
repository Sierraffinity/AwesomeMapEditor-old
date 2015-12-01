using System;
using System.Collections.Generic;
using System.Linq;
using Nintenlord.Event_Assembler.Core.IO.Logs;
using Nintenlord.Collections;
using Nintenlord.Utility;
using Nintenlord.Event_Assembler.Core.Code.Language.Expression;

namespace Nintenlord.Event_Assembler.Core.Code.Templates
{
    /// <summary>
    /// Template for terminating string of data
    /// </summary>
    sealed class TerminatingStringTemplate : ICodeTemplate
    {
        TemplateParameter parameter;
        byte[] endingValue;
        string name;
        int offsetMod;
        StringComparer comparer;

        public TemplateParameter Parameter
        {
            get { return parameter; }
        }

        public TerminatingStringTemplate(string name, IEnumerable<TemplateParameter> parameters,
            int endingValue, int offsetMod, StringComparer stringComparer)
        {
            this.offsetMod = offsetMod;;
            this.parameter = parameters.First();
            this.endingValue = BitConverter.GetBytes(endingValue).Take(parameter.LenghtInBytes).ToArray();
            this.name = name;
            this.comparer = stringComparer;
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
            get { return true; }
        }
        public int OffsetMod
        {
            get { return offsetMod; }
        }
        public int AmountOfFixedCode
        {
            get { return 0; }
        }


        public bool Matches(byte[] data, int offset)
        {
            return true;
        }

        public int GetLengthBytes(byte[] code, int offset)
        {
            int currentOffset = offset;
            while (IsNotEnding(code,currentOffset))
            {
                currentOffset += parameter.LenghtInBytes;
            }
            return currentOffset - offset;
        }

        public CanCauseError<string[]> GetAssembly(byte[] code, int offset)
        {
            List<string> assemly = new List<string>();
            assemly.Add(name);
            while (IsNotEnding(code, offset))
            {
                int[] value = parameter.GetValues(code, offset);
                assemly.Add(parameter.conversion(value[0]));
                offset += parameter.LenghtInBytes;
            }
            return assemly.ToArray();
        }
        
        public bool Matches(Language.Types.Type[] code)
        {
            foreach (var item in code)
            {
                if (!parameter.CompatibleType(item))
                {
                    return false;
                }
            }
            return true;
        }

        public int GetLengthBytes(Parameter<int>[] code)
        {
            return (code.Length + 1) * parameter.lenght / 8;
        }

        public CanCauseError<byte[]> GetData(Parameter<int>[] code, Func<string, int?> getSymbolValue)
        {
            List<byte> bytes = new List<byte>(0x20);
            for (int i = 0; i < code.Length; i++)
            {
                int[] values = null;
                if (code[i].IsVector)
                {
                    values = new int[code[i].CompCount];
                    for (int j = 0; j < code[i].CompCount; j++)
                    {
                        var error = Folding.Fold(code[i].Only, getSymbolValue);
                        if (error.CausedError)
                        {
                            return CanCauseError<byte[]>.Error(error.ErrorMessage);
                        }
                        else
                        {
                            values[j] = error.Result;
                        }
                    }
                }
                else
                {
                    var error = Folding.Fold(code[i].Only, getSymbolValue);
                    if (error.CausedError)
                    {
                        return CanCauseError<byte[]>.Error(error.ErrorMessage);
                    }
                    else
                    {
                        values = new int[] { error.Result };
                    }
                }
                byte[] temp = new byte[parameter.LenghtInBytes];
                parameter.InsertValues(values, temp);
                bytes.AddRange(temp);
            }
            bytes.AddRange(endingValue);

            return bytes.ToArray();
        }

        #endregion


        private bool IsNotEnding(byte[] code, int currentOffset)
        {
            return !code.Equals(currentOffset, endingValue, 0, parameter.LenghtInBytes);
        }

        public static void WriteDoc(System.IO.TextWriter writer, TerminatingStringTemplate template)
        {
            writer.WriteLine("{0} {1}1 {1}2 ... {1}N", template.name, template.parameter.name);
        }

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

    }
}
