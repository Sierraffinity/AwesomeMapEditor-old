// -----------------------------------------------------------------------
// <copyright file="TokenTypeParser.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Event_Assembler.Core.Code.Language.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nintenlord.Event_Assembler.Core.Code.Language.Lexer;
    using Nintenlord.Parser;
    using Nintenlord.IO.Scanners;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    sealed class TokenTypeParser : Parser<Token, Token>
    {
        readonly TokenType type;

        private TokenTypeParser(TokenType type)
        {
            this.type = type;
        }

        protected override Token ParseMain(IScanner<Token> scanner, out Match<Token> match)
        {
            Token token = scanner.Current;
            if (token.Type == type)
            {
                match = new Match<Token>(scanner, 1);
                scanner.MoveNext();
            }
            else
            {
                match = new Match<Token>(scanner, string.Format("Got {0}, expected {1}, {3}", 
                    token.HasValue ? token.Value : token.Type.ToString(), 
                    type, 
                    token,
                    token.Position));
            }
            return token;
        }

        static Dictionary<TokenType, TokenTypeParser> Parsers = new Dictionary<TokenType, TokenTypeParser>();
        static public TokenTypeParser GetTypeParser(TokenType type)
        {
            TokenTypeParser parser;
            if (!Parsers.TryGetValue(type, out parser))
            {
                parser = Parsers[type] = new TokenTypeParser(type);
            }
            return parser;
        }
    }
}
