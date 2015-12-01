using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Event_Assembler.Core.Code.Language.Lexer
{
    public enum TokenType
    {
        EndOfStream,
        LeftParenthesis,
        RightParenthesis,
        LeftCurlyBracket,
        RightCurlyBracket,
        LeftSquareBracket,
        RightSquareBracket,
        //DoubleQuote,
        IntegerLiteral,
        Symbol,
        MathOperator,
        CodeEnder,
        Comma
    }
}
