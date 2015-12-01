using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Event_Assembler.Core.Code.Language.Lexer;
using Nintenlord.Collections;
using Nintenlord.IO.Scanners;
using Nintenlord.Parser;
using Nintenlord.Parser.BinaryParsers;
using Nintenlord.Parser.ParserCombinators;
using Nintenlord.Parser.UnaryParsers;
using Nintenlord.Event_Assembler.Core.Code.Language.Parser;
using Nintenlord.Event_Assembler.Core.Code.Language.Expression.Tree;

namespace Nintenlord.Event_Assembler.Core.Code.Language.Expression
{
    internal class TokenParser<T> : Parser<Token, IExpression<T>>
    {
        IParser<Token, IExpression<T>> mainParser;

        public TokenParser(Func<string, T> eval)
        {
            var atomParser = new MathParser<T>(eval).Name("Atom");

            var vectorParser =
                atomParser.
                SepBy1(TokenTypeParser.GetTypeParser(TokenType.Comma)).
                Between(
                    TokenTypeParser.GetTypeParser(TokenType.LeftSquareBracket),
                    TokenTypeParser.GetTypeParser(TokenType.RightSquareBracket)
                ).Name("Vector");

            var parameterParser =
                vectorParser.Transform(x => new Parameter<T>(x, x[0].Position)).
                Or(atomParser.Transform(x => new Parameter<T>(x, x.Position))).
                Name("Parameter");

            var labelParser =
                    new SatisfyParser<Token>(
                        x => x.Type == TokenType.Symbol
                            && x.HasValue
                            && x.Value.EndsWith(":")).
                        Transform(x => new LabelDefinition<T>(x.Position, x.Value.TrimEnd(':'))).Name("Label");

            var codeParser =
                ParserHelpers.Combine(
                    TokenTypeParser.GetTypeParser(TokenType.Symbol).
                        Transform(x => new Symbol<T>(x.Value, x.Position)).Name("Code name"),
                    parameterParser.Many(),
                    (y, z) => new Code<T>(y.Position ,y, z)).Name("Code");

            var scopeParser = new ScopeParser<T>(
                labelParser.Or<Token, IExpression<T>>(codeParser),
                TokenTypeParser.GetTypeParser(TokenType.CodeEnder),
                TokenTypeParser.GetTypeParser(TokenType.LeftCurlyBracket),
                TokenTypeParser.GetTypeParser(TokenType.RightCurlyBracket));

            mainParser = scopeParser.Transform(x => (IExpression<T>)x);
        }

        public static IEnumerable<T2> Flatten<T2>(List<List<T2>> lists)
        {
            foreach (var list in lists)
            {
                foreach (var item in list)
                {
                    yield return item;
                }
            }
        }

        void parseEvent<T1, T2>(object sender, ParsingEventArgs<T1, T2> e)
        {
            Console.WriteLine("Parser {0}, matched {1}", sender, e.Match);
        }

        protected override IExpression<T> ParseMain(IScanner<Token> scanner, out Match<Token> match)
        {
            return mainParser.Parse(scanner, out match);
        }
    }
}
