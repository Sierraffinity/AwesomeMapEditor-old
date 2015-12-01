using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors.BuiltInMacros
{
    class VectorLength : IMacro
    {


        #region IMacro Members

        public bool IsCorrectAmountOfParameters(int amount)
        {
            return amount == 1;
        }

        public string Replace(string[] parameters)
        {
            return "";
        }

        #endregion

        #region IEquatable<IMacro> Members

        public bool Equals(IMacro other)
        {
            return typeof(VectorLength) == other.GetType();
        }

        #endregion

        static string[] GetVector(string vector)
        {
            return null;
        }
    }
}
