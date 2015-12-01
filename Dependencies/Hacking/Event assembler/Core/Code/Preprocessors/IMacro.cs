using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors
{
    public interface IMacro : IEquatable<IMacro>
    {
        bool IsCorrectAmountOfParameters(int amount);
        string Replace(string[] parameters);
    }

}
