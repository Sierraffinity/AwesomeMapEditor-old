using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Code.Language.BuiltInCodes
{
    class OffsetChanger : IBuiltInCode
    {
        #region IBuiltInCode Members

        public string Name
        {
            get { return "ORG"; }
        }

        public int MinAmountOfParameters
        {
            get { return 1; }
        }

        public int MaxAmountOfParameters
        {
            get { return 1; }
        }

        public CanCauseError FirstPass(string[] code, Context context)
        {
            int newOffset;
            if (code[1].GetMathStringValue(out newOffset))
            {
                context.Offset = newOffset;
                return CanCauseError.NoError;
            }
            else
            {
                return CanCauseError.Error(code[1] + " is not a valid offset.");
            }
        }

        public CanCauseError<bool> SecondPass(string[] code, Context context)
        {
            int newOffset;
            int oldOffset = context.Offset;
            if (code[1].GetMathStringValue(out newOffset))
            {
                context.Offset = newOffset;
            }
            return oldOffset != newOffset;
        }

        #endregion
    }

}
