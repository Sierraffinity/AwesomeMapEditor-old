// -----------------------------------------------------------------------
// <copyright file="ScopeParser.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Event_Assembler.Core.Code.Language.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nintenlord.Parser;
    using Nintenlord.Event_Assembler.Core.Code.Language.Lexer;
    using Nintenlord.Event_Assembler.Core.Code.Language.Expression;
    using Nintenlord.IO.Scanners;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    class ScopeParser<T> : Parser<Token, Scope<T>>
    {
        readonly IParser<Token, IExpression<T>> codeParser;
        readonly IParser<Token, Token> codeDividerParser;
        readonly IParser<Token, Token> scopeStartParser;
        readonly IParser<Token, Token> scopeEndParser;

        public ScopeParser(
            IParser<Token, IExpression<T>> codeParser,
            IParser<Token, Token> codeDividerParser,
            IParser<Token, Token> scopeStartParser,
            IParser<Token, Token> scopeEndParser)
        {
            this.codeParser = codeParser;
            this.codeDividerParser = codeDividerParser;
            this.scopeStartParser = scopeStartParser;
            this.scopeEndParser = scopeEndParser;
        }

        protected override Scope<T> ParseMain(IScanner<Token> scanner, out Match<Token> match)
        {
            List<IExpression<T>> scopedStuff = new List<IExpression<T>>();
            match = new Match<Token>(scanner);

            Match<Token> latest;
            while (true)
            {
                bool oneSucceeded;
                do
                {
                    oneSucceeded = false;
                    var code = codeParser.Parse(scanner, out latest);
                    if (latest.Success)
                    {
                        scopedStuff.Add(code);
                        match += latest;
                        oneSucceeded = true;
                    }

                    codeDividerParser.Parse(scanner, out latest);
                    if (latest.Success)
                    {
                        match += latest;
                        oneSucceeded = true;
                    }
                }
                while (oneSucceeded);

                scopeStartParser.Parse(scanner, out latest);
                if (!latest.Success) break;//Only succesful exit point
                match += latest;

                var scope =  this.Parse(scanner, out latest);
                match += latest;
                if (match.Success) scopedStuff.Add(scope);                
                else return null;

                scopeEndParser.Parse(scanner, out latest);
                match += latest;

                if (!match.Success) return null;
	        }

            return new Scope<T>(scopedStuff, default(Nintenlord.IO.FilePosition));
        }
    }
}
