using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors.BuiltInMacros
{
    public struct VectorToParameter : IMacro
    {
        #region IReplacer Members

        public bool IsCorrectAmountOfParameters(int amount)
        {
            return amount == 1;
        }

        public string Replace(string[] parameters)
        {
            return parameters[0].Trim('[', ']');
        }

        #endregion

        #region IEquatable<IReplacer> Members

        public bool Equals(IMacro other)
        {
            return other.GetType() == typeof(VectorToParameter);
        }

        #endregion
    }
}
