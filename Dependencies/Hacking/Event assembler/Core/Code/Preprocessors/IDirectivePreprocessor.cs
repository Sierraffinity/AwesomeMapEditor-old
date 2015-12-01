using System.Collections.Generic;
using Nintenlord.Event_Assembler.Core.Collections;
using Nintenlord.Event_Assembler.Core.Code.Preprocessors.BuiltInMacros;
using Nintenlord.Event_Assembler.Core.IO.Input;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors
{
    interface IDirectivePreprocessor : IPreprocessor
    {
        Stack<bool> Include { get; }
        IDefineCollection DefCol { get; }
        Pool Pool { get; }
        IInputStream Input { get; }
        
        bool IsValidToDefine(string name);
        bool IsPredefined(string name);
    }
}
