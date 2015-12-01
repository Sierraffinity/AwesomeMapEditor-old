using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Utility
{
    /// <summary>
    /// Extensions and helper methods to integers
    /// </summary>
    public static class IntegerExtensions
    {
        public static bool IsInRange(this int i, int min, int max)
        {
            return i <= max && i >= min;
        }

        public static bool IsInRangeHO(this int i, int min, int max)
        {
            return i < max && i >= min;
        }

        public static void Clamp(ref int i, int min, int max)
        {
            if (i < min)
            {
                i = min;
            }
            else if (i > max)
            {
                i = max;
            }
        }
        public static int Clamp(this int i, int min, int max)
        {
            if (i < min)
            {
                return min;
            }
            if (i > max)
            {
                return max;
            }
            return i;
        }

        public static int ToMod(this int i, int mod)
        {
            if (i % mod != 0)
            {
                i += mod - i % mod;
            }
            return i;
        }

        public static void ToMod(ref int i, int mod)
        {
            if (i % mod != 0)
            {
                i += mod - i % mod;
            }
        }

        public static string ToHexString(this int i, string prefix)
        {
            return ToHexString(i, prefix, "");
        }
        public static string ToHexString(this int i, string prefix, string postfix)
        {
            return prefix + Convert.ToString(i, 16).ToUpper() + postfix;
        }

        public static string ToBinString(this int i, string postfix)
        {
            return ToBinString(i, "", postfix);
        }
        public static string ToBinString(this int i, string prefix, string postfix)
        {
            return prefix + Convert.ToString(i, 2) + postfix;
        }

        public static bool Intersects(int index1, int length1, int index2, int length2)
        {
            if (length1 == 0 || length2 == 0)
                return false;
            return (index1 < index2 + length2 && index1 >= index2) ||
                   (index2 < index1 + length1 && index2 >= index1);
        }

        public static int ToPower2(this int value)
        {
            ToPower2(ref value);
            return value;
        }

        public static void ToPower2(ref int value)
        {
            value--;
            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            value |= value >> 16;
            value++;
        }
    }
}
