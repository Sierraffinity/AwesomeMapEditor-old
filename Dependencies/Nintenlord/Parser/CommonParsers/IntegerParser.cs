using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.IO.Scanners;
using Nintenlord.Utility.Primitives;

namespace Nintenlord.Parser.CommonParsers
{
    class IntegerParser : Parser<char, int>
    {
        char specialHexPrefix;

        public IntegerParser(char specialHexPrefix)
        {
            this.specialHexPrefix = specialHexPrefix;
        }

        protected override int ParseMain(IScanner<char> scanner, out Match<char> match)
        {
            StringBuilder bldr = new StringBuilder();
            int intBase = 0;
            char c = scanner.Current;
            if (c == specialHexPrefix)
                intBase = 16;

            while (c.IsHexDigit())
            {
                bldr.Append(c);
                if (!scanner.MoveNext())
                {
                    break;
                }
            }

            if (intBase == 0)
            {

            }
            throw new NotImplementedException();
        }
    }
}
