using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.IO.Scanners;

namespace Nintenlord.Parser.CharacterParsers
{
    class WhiteSpaceParser : Parser<Char, Char>
    {
        protected override char ParseMain(IScanner<char> scanner, out Match<char> match)
        {
            char c = scanner.Current;
            if (Char.IsWhiteSpace(c))
            {
                scanner.MoveNext();
                match = new Match<char>(scanner, 1);
                return c;
            }
            else
            {
                match = new Match<char>(scanner, "Expected whitespace, got: " + c);
                return '\0';
            }
        }
    }
}
