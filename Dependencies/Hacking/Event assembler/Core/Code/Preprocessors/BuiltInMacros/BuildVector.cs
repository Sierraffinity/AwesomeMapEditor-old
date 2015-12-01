using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Event_Assembler.Core.Collections;

using Nintenlord.Collections;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors.BuiltInMacros
{
    public struct BuildVector : IMacro
    {
        #region IMacro Members

        public bool IsCorrectAmountOfParameters(int amount)
        {
            return true;
        }

        public string Replace(string[] parameters)
        {
            return parameters.ToElementWiseString(",", "[", "]");
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
