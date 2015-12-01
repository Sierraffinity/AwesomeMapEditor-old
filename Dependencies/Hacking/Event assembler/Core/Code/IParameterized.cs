using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Code
{
    /// <summary>
    /// A code that can take a certain amount of parameters. 
    /// </summary>
    public interface IParameterized //Think a better name for this.
    {
        /// <summary>
        /// Minimun amount of parameters accepted or -1 if no minimun exists.
        /// </summary>
        int MinAmountOfParameters { get; }
        /// <summary>
        /// Maximun amount of parameters accepted or -1 if no maximun exists.
        /// </summary>
        int MaxAmountOfParameters { get; }
    }

    public static class ParameterizedHelpers
    {
        public static bool Matches(this IParameterized parameterized, string name, int paramCount, out string error)
        {
            int min = parameterized.MinAmountOfParameters;
            int max = parameterized.MaxAmountOfParameters;
            if ((min != -1 && max != -1 && paramCount.IsInRange(min, max)) ||
                (min == -1 && paramCount <= max) ||
                (max == -1 && paramCount >= min) ||
                (min == -1 && max == -1)
                )
            {
                error = String.Empty;
                return true;
            }
            else
            {
                string format;
                if (min == -1)
                {
                    format = "maximun of {1}";
                }
                else if (max == -1)
                {
                    format = "minimun of {2}";
                }
                else if (min == max)
                {
                    format = "{1}";
                }
                else
                {
                    format = "range {1}-{2}";
                }
                error =
                    string.Format(
                    "{0} requires " + format + " parameters",
                    name,
                    min,
                    max
                    );
                return false;
            }
        }
    }
}
