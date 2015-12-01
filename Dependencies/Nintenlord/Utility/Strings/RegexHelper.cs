using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nintenlord.Utility
{
    static class RegexHelper
    {
        public static string[] Substrings(this Group group)
        {
            return Substrings(group.Captures);
        }

        public static string[] Substrings(this CaptureCollection collection)
        {
            string[] result = new string[collection.Count];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = collection[i].Value;
            }
            return result;
        }

    }
}
