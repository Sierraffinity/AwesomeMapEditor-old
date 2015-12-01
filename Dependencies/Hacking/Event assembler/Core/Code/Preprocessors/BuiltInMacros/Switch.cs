using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Event_Assembler.Core.Collections;

using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors.BuiltInMacros
{
    public struct Switch : IMacro
    {
        #region IReplacer Members

        public bool IsCorrectAmountOfParameters(int amount)
        {
            return amount > 1;
        }

        public string Replace(string[] parameters)
        {
            int value;
            if (parameters[0].GetMathStringValue(out value))
            {
                if (value < parameters.Length && value != 0)
                {
                    return parameters[value];
                }
                else
                {
                    return "";
                }
            }
            else return "";//Raise error
        }

        #endregion

        #region IEquatable<IReplacer> Members

        public bool Equals(IMacro other)
        {
            return other.GetType() == typeof(Switch);
        }

        #endregion
    }
}
