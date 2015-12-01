using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Utility
{
    public struct FixedPointInteger : IEquatable<FixedPointInteger>, IComparable<FixedPointInteger>//, IConvertible
    {
        #region Static
        static readonly int[] masks;

        static FixedPointInteger()
        {
            masks = new int[sizeof(int) * 8];
            int mask = 0;
            for (int i = 0; i < masks.Length; i++)
            {
                masks[i] = mask;
                mask <<= 1;
                mask |= 1;
            }
            decimals = 16;
            decimalMask = masks[decimals];
        }

        static readonly int decimals;
        static readonly int decimalMask;
        #endregion

        int rawValue;

        private FixedPointInteger(int rawVal)
        {
            rawValue = rawVal;
        }

        static public explicit operator int(FixedPointInteger val)
        {
            return val.rawValue >> decimals;
        }

        static public implicit operator FixedPointInteger(int val)
        {
            return new FixedPointInteger(val << decimals);
        }

        static public explicit operator float(FixedPointInteger val)
        {
            float value = (float)(int)val;
            value += (float)(val.rawValue & decimalMask) / (float)(decimalMask + 1);
            return value;
        }

        static public implicit operator FixedPointInteger(float val)
        {
            int rawval = (int)val;
            rawval <<= decimals;
            double decimalval = val - Math.Floor(val);
            rawval |= (int)((float)(decimalMask + 1) * decimalval);
            return new FixedPointInteger(rawval);
        }

        static public FixedPointInteger operator +(FixedPointInteger val, FixedPointInteger val2)
        {
            return new FixedPointInteger(val.rawValue + val2.rawValue);
        }

        static public FixedPointInteger operator -(FixedPointInteger val, FixedPointInteger val2)
        {
            return new FixedPointInteger(val.rawValue - val2.rawValue);
        }

        static public FixedPointInteger operator *(FixedPointInteger val, FixedPointInteger val2)
        {
            long temp = Math.BigMul(val.rawValue, val2.rawValue);
            return new FixedPointInteger((int)(temp >> decimals));
        }

        static public FixedPointInteger operator /(FixedPointInteger val, FixedPointInteger val2)
        {
            int remainder, quatient;
            remainder = Math.DivRem(val.rawValue, val2.rawValue, out quatient);
            int result = (remainder << decimals) | (quatient / (val2.rawValue >> decimals));
            return new FixedPointInteger(result);
        }

        static public FixedPointInteger operator <<(FixedPointInteger val, int val2)
        {
            return new FixedPointInteger(val.rawValue << val2);
        }

        static public FixedPointInteger operator >>(FixedPointInteger val, int val2)
        {
            return new FixedPointInteger(val.rawValue >> val2);
        }

        static public FixedPointInteger operator &(FixedPointInteger val, FixedPointInteger val2)
        {
            return new FixedPointInteger(val.rawValue & val2.rawValue);
        }

        static public FixedPointInteger operator |(FixedPointInteger val, FixedPointInteger val2)
        {
            return new FixedPointInteger(val.rawValue | val2.rawValue);
        }

        static public FixedPointInteger operator ^(FixedPointInteger val, FixedPointInteger val2)
        {
            return new FixedPointInteger(val.rawValue ^ val2.rawValue);
        }


        #region IEquatable<FixedPointInteger> Members

        public bool Equals(FixedPointInteger other)
        {
            return this.CompareTo(other) == 0;
        }

        #endregion

        #region IComparable<FixedPointInteger> Members

        public int CompareTo(FixedPointInteger other)
        {
            return this.rawValue - other.rawValue;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is FixedPointInteger)
            {
                return this.Equals((FixedPointInteger)obj);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return rawValue;
        }

        public override string ToString()
        {
            //return Convert.ToString(rawValue, 16);
            //return rawValue.ToString();
            return ((float)this).ToString();
        }

        #region IConvertible Members

        //public TypeCode GetTypeCode()
        //{
        //    return TypeCode.Object;
        //}

        //public bool ToBoolean(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public byte ToByte(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public char ToChar(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public DateTime ToDateTime(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public decimal ToDecimal(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public double ToDouble(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public short ToInt16(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public int ToInt32(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public long ToInt64(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public sbyte ToSByte(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public float ToSingle(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public string ToString(IFormatProvider provider)
        //{
        //    ret 
        //}

        //public object ToType(Type conversionType, IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public ushort ToUInt16(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public uint ToUInt32(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        //public ulong ToUInt64(IFormatProvider provider)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
    }
}
