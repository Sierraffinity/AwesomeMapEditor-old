using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Event_Assembler.Core.Code.Templates
{
    public class TemplateComparer : IComparer<ICodeTemplate>, IEqualityComparer<ICodeTemplate>
    {
        #region IComparer<ICodeTemplate> Members

        /// <summary>
        /// Compares two templates and finds the better match.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Positive if a is better match, negative if b is better, else 0.</returns>
        public int Compare(ICodeTemplate a, ICodeTemplate b)
        {
            if (a == b) return 0;

            if (a.GetType() == typeof(RawCodeTemplate) &&
                b.GetType() == typeof(RawCodeTemplate)) return 0;
            else if (b.GetType() == typeof(RawCodeTemplate)) return 1;
            else if (a.GetType() == typeof(RawCodeTemplate)) return -1;
            else if (a.GetType() == typeof(CodeFillerTemplate)) return -1;
            else if (b.GetType() == typeof(CodeFillerTemplate)) return 1;
            else if (a.GetType() == typeof(GenericFE8Template)) return -1;
            else if (b.GetType() == typeof(GenericFE8Template)) return 1;

            if (a.AmountOfFixedCode != b.AmountOfFixedCode)
            {
                return a.AmountOfFixedCode - b.AmountOfFixedCode;
            }

            if (a.OffsetMod != b.OffsetMod)
            {
                return a.OffsetMod - b.OffsetMod;
            }

            if (a.EndingCode != b.EndingCode)
            {
                if (a.EndingCode) return 1;
                else return -1;
            }

            CodeTemplate Atemp = a as CodeTemplate;
            CodeTemplate Btemp = b as CodeTemplate;

            if (Atemp != null && Btemp != null)
            {
                if (Atemp.Length != Btemp.Length)
                {
                    return Atemp.Length - Btemp.Length;
                }
                int aTotalCoords = Atemp.Aggregate(1, (x, y) => x * y.maxDimensions);
                int bTotalCoords = Btemp.Aggregate(1, (x, y) => x * y.maxDimensions);

                if (aTotalCoords != bTotalCoords)
                {
                    return aTotalCoords - bTotalCoords;
                }


                if (Atemp.AmountOfParams != Btemp.AmountOfParams)
                {
                    return -Atemp.AmountOfParams + Btemp.AmountOfParams;
                }
            }
            else
            {
                if (Atemp != null) return 1;
                if (Btemp != null) return -1;
            }

            return 0;
        }

        #endregion

        #region IEqualityComparer<ICodeTemplate> Members

        public bool Equals(ICodeTemplate x, ICodeTemplate y)
        {
            return this.Compare(x, y) == 0;
        }

        public int GetHashCode(ICodeTemplate obj)
        {
            if (obj is CodeTemplate)
            {
                return obj.GetHashCode();
            }
            else
            {
                return obj.Name.GetHashCode();
            }
        }

        #endregion
    }
}
