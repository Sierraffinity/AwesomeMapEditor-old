using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Utility;
using Nintenlord.Event_Assembler.Core.Code.Language.Expression;

namespace Nintenlord.Event_Assembler.Core.Code.Language.BuiltInCodes
{
    interface IBuiltInCode : INamed<string>, IParameterized
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        CanCauseError FirstPass(string[] code, Context context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="context"></param>
        /// <returns>True if offset was changed, else false.</returns>
        CanCauseError<bool> SecondPass(string[] code, Context context);
    }
}
