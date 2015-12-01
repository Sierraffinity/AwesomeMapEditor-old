using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Event_Assembler.Core.Code.Language.Expression
{
    public enum EAExpressionType
    {
        Scope,
        Code,
        Parameter,
        Label,

        XOR,
        AND,
        OR,

        LeftShift,
        RightShift,

        Division,
        Multiply,
        Modulus,

        Minus,
        Sum,

        Value,
        Symbol,
    }
}
