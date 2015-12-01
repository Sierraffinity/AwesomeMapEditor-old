using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Event_Assembler.Core.Collections;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors.BuiltInMacros
{
    public struct IsDefined : IMacro
    {
        IDefineCollection defCol;

        public IsDefined(IDefineCollection defCol)
        {
            this.defCol = defCol;
        }

        #region IReplacer Members

        public bool IsCorrectAmountOfParameters(int amount)
        {
            return amount == 1;
        }

        public string Replace(string[] parameters)
        {
            if (defCol.ContainsName(parameters[0]))
            {
                return "1";
            }
            else
            {
                return "0";
            }
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
