using System;
using Nintenlord.Event_Assembler.Core.Collections;
using System.Collections.Generic;
using Nintenlord.Event_Assembler.Core.Code.Preprocessors;

namespace Nintenlord.Event_Assembler.Core.Code.StringReplacers
{
    interface IStringReplacer
    {
        IDictionary<string, IDictionary<int, IMacro>> Values
        {
            set;
        }
        IDictionary<string, IMacro> BuiltInValues
        {
            set;
        }
        int MaxIter
        {
            set;
        }

        bool Replace(string s, out string newString);
    }
}
