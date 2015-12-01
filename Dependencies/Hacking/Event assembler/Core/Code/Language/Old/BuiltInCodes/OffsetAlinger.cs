using System;
using Nintenlord.Utility;
using Nintenlord.Event_Assembler.Core.Code.Language.Expression;

namespace Nintenlord.Event_Assembler.Core.Code.Language.BuiltInCodes
{
    class OffsetAligner : IBuiltInCode
    {
        #region IBuiltInCode Members

        public string Name
        {
            get { return "ALIGN"; }
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
            int value;
            if (code[1].TryGetValue(out value))
            {
                context.Offset = context.Offset.ToMod(value);
                return CanCauseError.NoError;
            }
            else
            {
                return CanCauseError.Error(code[1] + " is not a valid number.");
            }
        }

        public CanCauseError<bool> SecondPass(string[] code, Context context)
        {
            int value;
            int oldOffset = context.Offset;
            if (code[1].TryGetValue(out value))
            {
                context.Offset = context.Offset.ToMod(value);
            }
            return oldOffset != context.Offset;
        }
        
        #endregion
    }
}
