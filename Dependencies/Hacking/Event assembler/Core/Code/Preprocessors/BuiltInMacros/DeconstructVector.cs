using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Event_Assembler.Core.Collections;

using Nintenlord.Collections;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors.BuiltInMacros
{
    public struct DeconstructVector : IMacro
    {
        #region IReplacer Members

        public bool IsCorrectAmountOfParameters(int amount)
        {
            return amount == 1;
        }

        public string Replace(string[] parameters)
        {
            return parameters[0].Trim('[', ']').Split(',').ToElementWiseString(" ", "", "");
        }

        #endregion

        #region IEquatable<IReplacer> Members

        public bool Equals(IMacro other)
        {
            return this.GetType() == other.GetType();
        }

        #endregion
    }
}
