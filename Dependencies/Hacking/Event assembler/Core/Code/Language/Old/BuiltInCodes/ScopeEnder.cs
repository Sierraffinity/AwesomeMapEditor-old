using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Code.Language.BuiltInCodes
{
    class ScopeEnder : IBuiltInCode
    {
        #region IBuiltInCode Members

        public string Name
        {
            get { return "}"; }
        }

        public int MinAmountOfParameters
        {
            get { return 0; }
        }

        public int MaxAmountOfParameters
        {
            get { return 0; }
        }

        public CanCauseError FirstPass(string[] code, Context context)
        {
            if (context.ScopesOnStack == 0)
            {
                return CanCauseError.Error("No scope to end");
            }
            else
            {
                context.EndScope();
                return CanCauseError.NoError;
            }
        }

        public CanCauseError<bool> SecondPass(string[] code, Context context)
        {
            if (context.ScopesOnStack != 0)
            {
                context.EndScope();
            }
            return false;
        }

        #endregion
    }
}
