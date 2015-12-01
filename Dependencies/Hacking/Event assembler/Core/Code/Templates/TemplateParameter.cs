using System;
using Nintenlord.Event_Assembler.Core.Code.Language;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Code.Templates
{
    /// <summary>
    /// Parameter for code templates
    /// </summary>
    class TemplateParameter : IEquatable<TemplateParameter>
    {
        readonly public string name;
        readonly public int position;
        readonly public int lenght;


        readonly public int minDimensions;
        readonly public int maxDimensions;

        readonly public bool pointer;
        readonly public Priority pointedPriority;

        readonly public bool isFixed;
        readonly public bool signed;

        public Func<int, string> conversion;

        public int LenghtInBytes
        {
            get { return lenght / 8; }
        }
        public int PositionInBytes
        {
            get { return position / 8; }
        }
        public int LastPosition
        {
            get
            {
                return position + lenght;
            }
        }
        public int LastPositionInBytes
        {
            get
            {
                return (position + lenght)/8;
            }
        }
        public int BitsPerCoord
        {
            get { return lenght / maxDimensions; }
        }

        public TemplateParameter(string name, int position, int lenght, int minDimensions, 
            int maxDimensions, bool pointer, Priority pointedPriority, bool signed, bool isFixed)
        {
            this.signed = signed;
            this.pointedPriority = pointedPriority;
            this.name = name;
            this.position = position;
            this.lenght = lenght;
            this.minDimensions = minDimensions;
            this.maxDimensions = maxDimensions;
            this.pointer = pointer;
            this.isFixed = isFixed;
            this.conversion = ToHexString;
        }

        public bool Matches(string parameter)
        {
            int number = 1 + parameter.Amount(',');
            return number.IsInRange(minDimensions, maxDimensions);
        }

        public int[] GetValues(byte[] data, int codeOffset)
        {
            //throw new NotImplementedException();
            int[] result = new int[maxDimensions];
            for (int i = 0; i < result.Length; i++)
            {
                int coordOffsetBit = codeOffset * 8 + position + i * BitsPerCoord;

                byte[] value = data.GetBits(coordOffsetBit, BitsPerCoord);
                Array.Resize(ref value, 4);
                if (this.signed && value.GetBits(BitsPerCoord - 1, 1)[0] == 1)
                {
                    value.WriteTo(BitsPerCoord, new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }, 
                        sizeof(int) * 8 - BitsPerCoord);
                }
                result[i] = BitConverter.ToInt32(value, 0);
            }
            return result;
        }

        public bool InsertValues(int[] values, byte[] code)
        {
            //throw new NotImplementedException();
            for (int i = 0; i < values.Length; i++)
            {
                byte[] value = BitConverter.GetBytes(values[i]);
                code.WriteTo(position + i * BitsPerCoord, value, BitsPerCoord);
            }
            return true;
        }
        
        public void SetBase(int valueBase)
        {
            switch (valueBase)
            {
                case 16:
                    this.conversion = ToHexString; 
                    break;
                case 10:
                    this.conversion = ToDecString; 
                    break;
                case 2:
                    this.conversion = ToBinString; 
                    break;
                default:
                    throw new ArgumentException("Base must be either 2, 10 or 16.");
            }
        }

        public bool CompatibleType(Language.Types.Type type)
        {
            switch (type.type)
            {
                case Nintenlord.Event_Assembler.Core.Code.Language.Types.MetaType.Atom:
                    return (this.minDimensions == this.maxDimensions) && (this.minDimensions == 1);
                case Nintenlord.Event_Assembler.Core.Code.Language.Types.MetaType.Vector:
                    return type.ParameterCount.IsInRange(this.minDimensions, this.maxDimensions);
                default:
                    throw new ArgumentException();
            }
        }

        #region IEquatable<Parameter> Members

        public bool Equals(TemplateParameter other)
        {
            if (!this.name.Equals(other.name))
                return false;

            if (minDimensions != other.minDimensions)
                return false;

            if (maxDimensions != other.maxDimensions)
                return false;

            return true;
        }

        #endregion

        public override string ToString()
        {
            return name;
        }

        public override bool Equals(object obj)
        {
            if (obj is TemplateParameter)
            {
                return Equals(obj as TemplateParameter);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() 
                ^ minDimensions 
                ^ maxDimensions;
        }


        public static string ToHexString(int value)
        {
            return value.ToHexString("0x");
        }

        public static string ToDecString(int value)
        {
            return value.ToString();
        }

        public static string ToBinString(int value)
        {
            return value.ToBinString("b");
        }

        public static void WriteDocData(System.IO.TextWriter writer, TemplateParameter parameter)
        {
            if (parameter.maxDimensions != 1)
            {
                if (parameter.maxDimensions == parameter.minDimensions)
                {
                    writer.WriteLine("Amount of coordinates is {0}.", parameter.maxDimensions);
                }
                else
                {
                    writer.WriteLine("Amount of coordinates can range from {0} to {1}.", 
                        parameter.minDimensions, parameter.maxDimensions);
                }
            }

            long max, min;
            if (parameter.signed)
            {
                max = 1 << parameter.BitsPerCoord; 
                min = 0;
            }
            else
            {
                max = 1 << (parameter.BitsPerCoord - 1);
                min = -max - 1;
            }
            writer.WriteLine("Parameter accepts values from {0} to {1}." , min, max);

            if (parameter.pointer)
            {
                writer.WriteLine("Parameter will transform passed offsets into pointers.");
            }
        }
    }
}