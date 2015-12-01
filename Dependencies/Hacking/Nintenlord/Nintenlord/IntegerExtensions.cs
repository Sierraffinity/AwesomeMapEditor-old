using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Event_assembler
{
    /// <summary>
    /// Extensions and helper methods to integers
    /// </summary>
    static class IntegerExtensions
    {
        static public bool IsInRange(this int i, int min, int max)
        {
            return i <= max && i >= min;
        }

        static public int Clamp(this int i, int min, int max)
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

        static public int ToMod(this int i, int mod)
        {
            if (i % mod != 0)
            {
                i += mod - i % mod;
            }
            return i;
        }

        static public string ToHexString(this int i, string prefix)
        {
            return prefix + Convert.ToString(i, 16).ToUpper();
        }

        static public string ToBinString(this int i, string postfix)
        {
            return Convert.ToString(i, 2) + postfix;
        }


    }
}
