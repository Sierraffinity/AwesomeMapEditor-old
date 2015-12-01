using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Collections;
using Nintenlord.Event_Assembler.Core.Code.Language;
using Nintenlord.Event_Assembler.Core.Code.Language.Expression;
using Nintenlord.Event_Assembler.Core.IO.Logs;
using Nintenlord.Utility;
using EAType = Nintenlord.Event_Assembler.Core.Code.Language.Types.Type;

namespace Nintenlord.Event_Assembler.Core.Code.Templates
{
    /// <summary>
    /// Template for stored text code
    /// </summary>
    sealed class CodeTemplate : ICodeTemplate, IEnumerable<TemplateParameter>, INamed<int>
    {
        readonly string name;
        readonly int lenght;
        readonly int id;
        readonly byte[] baseData;
        readonly bool canBeRepeated;
        readonly bool checkForProblems;
        readonly bool isEndingCode;
        readonly int offsetMod;
        readonly int amountOfFixedCode;
        readonly bool canBeAssembled;
        readonly bool canBeDisassembled;

        readonly List<TemplateParameter> parameters;
        readonly List<TemplateParameter> fixedParameters;
        IPointerMaker pointerMaker;
        
        readonly StringComparer comparer;

        public IPointerMaker PointerMaker
        {
            get { return pointerMaker; }
            set { pointerMaker = value; }
        }
        
        public int Length
        {
            get { return lenght; }
        }
        public int LengthInBytes
        {
            get
            {
                return lenght / 8;
            }
        }
        public int AmountOfParams
        {
            get { return parameters.Count; }
        }
        public TemplateParameter this[int i]
        {
            get 
            {
                if (i < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                else if (canBeRepeated)
                {
                    return parameters[i % AmountOfParams]; 
                }
                return parameters[i]; 
            }
        }
        public bool CanBeDisassembled
        {
            get { return canBeDisassembled; }
        }
        public bool CanBeAssembled
        {
            get { return canBeAssembled; }
        }

        public CodeTemplate(string name, int id, int lenght, IEnumerable<TemplateParameter> parameters, 
            bool canBeRepeated, bool chechForProblems, bool end, int offsetMode, 
            bool canBeAssembled, bool canBeDisassembled, StringComparer stringComparer)
        {
            this.offsetMod = offsetMode;
            this.isEndingCode = end;
            this.checkForProblems = chechForProblems;
            this.canBeRepeated = canBeRepeated;
            this.lenght = lenght;
            this.name = name;
            this.id = id;
            this.canBeAssembled = canBeAssembled;
            this.canBeDisassembled = canBeDisassembled;
            this.comparer = stringComparer;

            this.parameters = new List<TemplateParameter>(parameters.Count());
            this.fixedParameters = new List<TemplateParameter>(parameters.Count());

            baseData = new byte[LengthInBytes];
            if (id != 0)
            {
                baseData[0] = (byte)(id & 0xFF);
                baseData[1] = (byte)((id >> 8) & 0xFF);
            }

            foreach (var parameter in parameters)//apply and remove fixed parameters
            {
                if (parameter.isFixed)
                {
                    fixedParameters.Add(parameter);
                    if (parameter.name.IsValidNumber())
                    {
                        int value = parameter.name.GetValue();
                        parameter.InsertValues(value.GetArray(), baseData);
                    }
                    else
                    {
                        throw new ArgumentException("The name of fixed parameter is" +
                                                " not a number: " + parameter.name);
                    }
                }
                else
                {
                    this.parameters.Add(parameter);
                }
            }
            this.amountOfFixedCode = 0;
            if (id != 0) amountOfFixedCode += 2;
            foreach (var item in fixedParameters)
            {
                amountOfFixedCode += item.LenghtInBytes;
            }

            if (checkForProblems)
            {
                if (!CheckIfWorks())
                {
                    throw new ArgumentException("Argumenst are not valid in code: " + name + " " 
                        + parameters.ToElementWiseString(", ","{","}"));
                }
            }
        }

        private bool CheckIfWorks()
        {
            if (id != 0 && this.LengthInBytes < 2) //Template with ID must have atleast 2 bytes 
            {
                return false;
            }

            if (this.lenght < 0)
            {
                return false;
            }

            if (canBeRepeated && (parameters.Count != 1))
            {
                return false;
            }

            bool[] usedBits = new bool[this.lenght];
            for (int i = 0; i < usedBits.Length; i++)
            {
                usedBits[i] = false;
            }
            if (id != 0)//ID uses first 2 bytes 
            {
                for (int i = 0; i < 16; i++)
                {
                    usedBits[i] = true;
                }
            }

            foreach (TemplateParameter parameter in parameters)
            {
                if (parameter.LastPosition > this.lenght 
                    || parameter.position < 0)//make sure there are no overflows
                {
                    return false;
                }
                for (int i = parameter.position; i < parameter.LastPosition; i++)
                {
                    if (usedBits[i])//make sure no parameter collisions
                    {
                        return false;
                    }
                    usedBits[i] = true;
                }
            }

            return true;
        }

        private byte[] GetDataUnit(string[] text, ILog messageLog)
        {
            byte[] data = baseData.Clone() as byte[];
            for (int i = 1; i < text.Length; i++)
            {
                TemplateParameter parameter = this[i - 1];
                if (parameter.lenght > 0)
                {
                    string paramText = text[i].Trim('[', ']');
                    string[] coordinates = paramText.Split(new char[] { ',' },
                        parameter.maxDimensions, StringSplitOptions.RemoveEmptyEntries);
                    int[] values = new int[coordinates.Length];
                    for (int j = 0; j < coordinates.Length; j++)
                    {
                        if (!coordinates[j].GetMathStringValue(out values[j]))
                        {
                            messageLog.AddError(coordinates[j] + " is not a valid number.");                            
                        }

                        if (parameter.pointer)
                        {
                            values[j] = pointerMaker.MakePointer(values[j]);
                        }
                    }
                    parameter.InsertValues(values, data);
                }
            }
            return data;
        }

        private CanCauseError<byte[]> GetDataUnit(Parameter<int>[] parameters, Func<string, int?> getSymbolValue)
        {
            byte[] data = baseData.Clone() as byte[];
            for (int i = 0; i < parameters.Length; i++)
            {
                TemplateParameter parameter = this[i];
                var paramExp = parameters[i];

                if (parameter.lenght > 0)
                {
                    int[] values = null;
                    if (parameters[i].IsVector)
                    {
                        values = new int[parameters[i].CompCount];
                        for (int j = 0; j < parameters[i].CompCount; j++)
                        {
                            var error = Folding.Fold(parameters[i][j], getSymbolValue);
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
                        var error = Folding.Fold(parameters[i].Only, getSymbolValue);
                        if (error.CausedError)
                        {
                            return CanCauseError<byte[]>.Error(error.ErrorMessage);
                        }
                        else
                        {
                            if (parameter.pointer)
                            {
                                values = new int[] { pointerMaker.MakePointer(error.Result) };
                            }
                            else
                            {
                                values = new int[] { error.Result };
                            }
                        }
                    }
                    parameter.InsertValues(values, data);
                }
            }
            return data;
        }

        #region ICodeTemplate Members

        public string Name
        {
            get { return name; }
        }
        public bool EndingCode
        {
            get { return isEndingCode; }
        }
        public int MaxRepetition
        {
            get 
            {
                if (canBeRepeated)
                {
                    return 4;
                }
                else
                {
                    return 1;
                } 
            }
        }
        public int OffsetMod
        {
            get { return offsetMod; }
        }
        public int AmountOfFixedCode
        {
            get { return amountOfFixedCode; }
        }

        
        public bool Matches(byte[] data, int offset)
        {
            if (!canBeDisassembled)
                return false;

            if (offset * 8 + this.lenght > data.Length * 8)//If there isn't room
                return false;
            if (offset % offsetMod != 0)
                return false;
            if (this.id == 0 && this.fixedParameters.Count == 0 && this.parameters.Count == 0)//If test can't fail
                return true;

            if (this.checkForProblems)
            {
                if (this.id != 0 && id != data[offset] + (data[offset + 1] << 8))
                    return false;
            }

            foreach (TemplateParameter parameter in fixedParameters)
            {
                byte[] valueBytes = baseData.GetBits(parameter.position, parameter.lenght);
                byte[] toCompare = data.GetBits(offset * 8 + parameter.position, parameter.lenght);
                if (!valueBytes.SequenceEqual(toCompare))
                {
                    return false;
                }
            }
            foreach (TemplateParameter parameter in parameters)
            {
                if (parameter.pointer)
                {
                    byte[] toCompare = data.GetBits(offset * 8 + parameter.position, parameter.lenght);
                    if (!this.pointerMaker.IsAValidPointer(BitConverter.ToInt32(toCompare, 0)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public int GetLengthBytes(byte[] code, int offset)
        {
            return this.LengthInBytes;
        }

        public CanCauseError<string[]> GetAssembly(byte[] code, int offset)
        {
            string[] assembly = new string[this.AmountOfParams + 1];
            assembly[0] = this.name;

            for (int i = 0; i < parameters.Count; i++)
            {
                TemplateParameter parameter = this[i];
                StringBuilder builder = new StringBuilder(parameter.lenght / 2);

                int[] values = parameter.GetValues(code, offset);
                if (values.Length > 1)
                {
                    builder.Append("[");
                    for (int j = 0; j < values.Length; j++)
                    {
                        builder.Append(parameter.conversion(values[j]));
                        if (j != parameter.maxDimensions - 1)
                        {
                            builder.Append(",");
                        }
                    }
                    builder.Append("]");
                }
                else
                {
                    int value = values[0];
                    if (parameter.pointer)
                    {
                        if (pointerMaker.IsAValidPointer(value))
                        {
                            value = pointerMaker.MakeOffset(value);
                        }
                        else
                        {
                            value = 0;
                        }
                    }
                    builder.Append(parameter.conversion(value));
                }
                assembly[i + 1] = builder.ToString();
            }

            return assembly;
        }
        

        public bool Matches(EAType[] paramTypes)
        {
            if (!canBeAssembled)
                return false;
            
            if (canBeRepeated)
            {
                if (this.AmountOfParams == 1)
                {
                    for (int i = 0; i < paramTypes.Length; i++)
                    {
                        if (!this[i].CompatibleType(paramTypes[i]))
                        {
                            return false;
                        }
                    }
                }
                else return false;
            }
            else
            {
                if (this.AmountOfParams == paramTypes.Length)
                {
                    for (int i = 0; i < this.AmountOfParams; i++)
                    {
                        if (!this[i].CompatibleType(paramTypes[i]))
                        {
                            return false;
                        }
                    }
                }
                else return false;
            }
            return true;
        }

        public int GetLengthBytes(Parameter<int>[] code)
        {
            if (this.canBeRepeated)
            {
                return LengthInBytes * code.Length;
            }
            else
            {
                return LengthInBytes;
            }
        }

        public CanCauseError<byte[]> GetData(Parameter<int>[] code, Func<string, int?> getSymbolValue)
        {
            if (this.canBeRepeated)
            {
                List<byte> data = new List<byte>(code.Length * this.LengthInBytes);

                int repeats = code.Length / this.AmountOfParams;

                for (int i = 0; i < repeats; i++)
                {
                    var newData = this.GetDataUnit(new Parameter<int>[] { code[i] }, getSymbolValue);
                    if (newData.CausedError)
                    {
                        return CanCauseError<byte[]>.Error(newData.ErrorMessage);
                    }
                    else
                    {
                        data.AddRange(newData.Result);
                    }
                }

                return data.ToArray();
            }
            else
            {
                return GetDataUnit(code, getSymbolValue);
            }

        }

        #endregion
        
        #region IEnumerable<Parameter> Members

        public IEnumerator<TemplateParameter> GetEnumerator()
        {
            return parameters.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return parameters.GetEnumerator();
        }

        #endregion

        #region IParameterized Members

        public int MinAmountOfParameters
        {
            get { return AmountOfParams; }
        }

        public int MaxAmountOfParameters
        {
            get { return AmountOfParams; }
        }

        #endregion

        #region INamed<int> Members

        int INamed<int>.Name
        {
            get { return id; }
        }

        #endregion

        public override string ToString()
        {
            if (parameters.Count > 0)
            {
                return string.Format("{0} {1}", name, parameters.ToElementWiseString(", ", "", ""));
            }
            else
            {
                return name;
            }
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() ^ id;
        }

        public static void WriteDoc(System.IO.TextWriter writer, CodeTemplate code)
        {
            writer.Write(code.name);
            writer.Write(' ');
            foreach (var parameter in code.parameters)
            {
                if (parameter.maxDimensions > 1)
                {
                    writer.Write('[');
                    for (int i = 0; i < parameter.maxDimensions; i++)
                    {
                        WriteName(writer, parameter.name, i, parameter.maxDimensions);
                        if (i != parameter.maxDimensions - 1)
                        {
                            writer.Write(", ");
                        }
                    }
                    writer.Write("] ");
                }
                else
                {
                    WriteName(writer, parameter.name, 0, parameter.maxDimensions);
                    writer.Write(' ');
                }
            }
            writer.WriteLine();
        }

        private static void WriteName(System.IO.TextWriter writer, string name, int i, int max)
        {
            string extraName;
            bool surround;
            if (max == 1)
            {
                surround = name.Contains(' ');
                extraName = "";
            }
            else if (max == 2 && 
                (name.Contains("Position") || 
                 name.Contains("position") ||
                 name.Contains("Location") ||
                 name.Contains("location") ||
                 name.Contains("Coordinate") ||
                 name.Contains("coordinate")
                ))
            {
                surround = false;
                if (i == 0)
                {
                    extraName = " X";
                }
                else
                {
                    extraName = " Y";
                }
            }
            else
            {
                surround = false;
                extraName = " " + (i+1).ToString();
            }      

            if (surround)
            {
                writer.Write('*');
                writer.Write(name + extraName);
                writer.Write('*');
            }
            else
            {
                writer.Write(name + extraName);
            }
        }
    }
}
