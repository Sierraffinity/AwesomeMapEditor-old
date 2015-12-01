using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class BetweenParser<TIn, TStart, TEnd, TOut> : Parser<TIn, TOut>
    {
        readonly IParser<TIn, TOut> valueParser;
        readonly IParser<TIn, TStart> startParser;
        readonly IParser<TIn, TEnd> endParser;

        public BetweenParser(IParser<TIn, TStart> startParser, IParser<TIn, TOut> valueParser, IParser<TIn, TEnd> endParser)
        {
            this.valueParser = valueParser;
            this.startParser = startParser;
            this.endParser = endParser;
        }

        protected override TOut ParseMain(IO.Scanners.IScanner<TIn> scanner, out Match<TIn> match)
        {
            Match<TIn> latestMatch;
            match = new Match<TIn>(scanner);

            startParser.Parse(scanner, out latestMatch);
            match += latestMatch;
            if (!match.Success)
            {
                return default(TOut);
            }

            TOut result = valueParser.Parse(scanner, out latestMatch);
            match += latestMatch;
            if (!match.Success)
            {
                return default(TOut);
            }

            endParser.Parse(scanner, out latestMatch);
            match += latestMatch;
            if (!match.Success)
            {
                return default(TOut);
            }

            return result;
        }
    }
}
