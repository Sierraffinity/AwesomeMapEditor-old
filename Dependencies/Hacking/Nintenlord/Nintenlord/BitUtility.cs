using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.ROMHacking
{
    static public class BitUtility
    {
        public static bool IsValidIntOffset(int value)
        {
            return (value & 3) == 0;
        }
        public static bool IsInvalidIntOffset(int value)
        {
            return !IsValidIntOffset(value);
        }
        public static bool IsValidShortOffset(int value)
        {
            return (value & 1) == 0;
        }
        public static bool IsInvalidShortOffset(int value)
        {
            return !IsValidShortOffset(value);
        }
    }
}
