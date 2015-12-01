// -----------------------------------------------------------------------
// <copyright file="InsertText.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors.BuiltInMacros
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class InsertText : IMacro
    {
        #region IMacro Members

        public bool IsCorrectAmountOfParameters(int amount)
        {
            return amount == 1;
        }

        public string Replace(string[] parameters)
        {
            string textToInsert = parameters[0];
            var data = Encoding.ASCII.GetBytes(textToInsert.ToCharArray());
            StringBuilder bldr = new StringBuilder(data.Length * 5 + 6);
            bldr.Append("BYTE");
            foreach (var item in data)
            {
                bldr.Append(" 0x" + item.ToString("X8"));
            }
            return bldr.ToString();
        }

        #endregion

        #region IEquatable<IMacro> Members

        public bool Equals(IMacro other)
        {
            return other.GetType() == typeof(InsertText);
        }

        #endregion
    }
}
