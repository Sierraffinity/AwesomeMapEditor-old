using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Event_Assembler.Core.Collections;

using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors.BuiltInMacros
{
    public struct Signum : IMacro
    {
        #region IReplacer Members

        public bool IsCorrectAmountOfParameters(int amount)
        {
            return amount == 1;
        }

        public string Replace(string[] parameters)
        {
            int value;
            if (parameters[0].GetMathStringValue(out value))
            {
                if (value < 0)
                {
                    return "-1";
                }
                else if (value == 0)
                {
                    return "0";
                }
                else
                {
                    return "1";
                }
            }
            else return "";//Raise error            
        }

        #endregion

        #region IEquatable<IReplacer> Members

        public bool Equals(IMacro other)
        {
            return other.GetType() == typeof(Signum);
        }

        #endregion
    }
}
