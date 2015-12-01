// -----------------------------------------------------------------------
// <copyright file="Tokeniser.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Event_Assembler.Core.Code.Language.Lexer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Nintenlord.IO;
    using Nintenlord.Event_Assembler.Core.IO.Input;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    static public class Tokeniser
    {
        static readonly Dictionary<TokenType, string> tokenRegexs; 
        static readonly Regex tokenRegex;

        static Tokeniser()
        {
            tokenRegexs = new Dictionary<TokenType, string>();
            tokenRegexs[TokenType.IntegerLiteral] = @"[0-9\$][0-9a-zA-Z_]*";
            tokenRegexs[TokenType.MathOperator] = @"\+|\-|\*|\/|\%|\&|\||\^|\~|(>>)|(<<)"; //A bit hard to read :)
            tokenRegexs[TokenType.Symbol] = "[a-zA-Z_][0-9a-zA-Z_]*:?";
            tokenRegexs[TokenType.CodeEnder] = @";";
            //tokenRegexs[TokenType.DoubleQuote] = "\"";
            tokenRegexs[TokenType.LeftParenthesis] = @"\(";
            tokenRegexs[TokenType.RightParenthesis] = @"\)";
            tokenRegexs[TokenType.LeftSquareBracket] = @"\[";
            tokenRegexs[TokenType.RightSquareBracket] = @"\]";
            tokenRegexs[TokenType.LeftCurlyBracket] = @"\{";
            tokenRegexs[TokenType.RightCurlyBracket] = @"\}";
            tokenRegexs[TokenType.Comma] = ",";

            StringBuilder rawRegex = new StringBuilder("");
            foreach (KeyValuePair<TokenType, string> item in tokenRegexs)
            {
                rawRegex.AppendFormat("(?<{0}>{1})|", item.Key, item.Value);
            }
            tokenRegex = new Regex(rawRegex.ToString(0, rawRegex.Length - 1), 
                RegexOptions.Compiled | RegexOptions.CultureInvariant);
        }
        
        static public IEnumerable<Token> TokeniseLine(string line, string file, int lineNumber)
        {
            var matches = tokenRegex.Matches(line);
            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;
                foreach (TokenType tokenType in tokenRegexs.Keys)
                {
                    Group group = groups[tokenType.ToString()];
                    if (group.Success)
                    {
                        for (int i = 0; i < group.Captures.Count; i++)
                        {
                            Capture capture = group.Captures[i];
                            Token token = new Token(
                                new FilePosition(file, lineNumber, capture.Index),
                                tokenType,
                                capture.Value);
                            yield return token;
                        }
                    }
                }
            }
            yield return new Token(new FilePosition(file,
                lineNumber, line.Length), TokenType.CodeEnder, "NL");
        }

        static public IEnumerable<Token> Tokenise(IPositionableInputStream input)
        {
            while (true)
            {
                string line = input.ReadLine();
                if (line == null)
                {
                    break;
                }
                foreach (var token in TokeniseLine(line, input.CurrentFile, input.LineNumber))
                {
                    yield return token;
                }
                yield return new Token(new FilePosition(input.CurrentFile,
                    input.LineNumber, line.Length), TokenType.CodeEnder, "NL");
            }
            yield return new Token();
        }
    }
}
