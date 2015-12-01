using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class SkipMany1Parser<TIn, TOut> : Parser<TIn, TOut>
    {
        readonly IParser<TIn, TOut> toRepeat;

        public SkipMany1Parser(IParser<TIn, TOut> toRepeat)
        {
            this.toRepeat = toRepeat;
        }
        
        Match<TIn> innerMatch;
        protected override TOut ParseMain(IO.Scanners.IScanner<TIn> scanner, out Match<TIn> match)
        {
            match = new Match<TIn>(scanner, 0);
            Match<TIn> latestMatch;

            toRepeat.Parse(scanner, out latestMatch);
            if (!latestMatch.Success)
            {
                match = latestMatch;
            }
            else
            {
                while (true)
                {
                    toRepeat.Parse(scanner, out latestMatch);
                    if (latestMatch.Success)
                    {
                        match += latestMatch;
                    }
                    else break;
                }
            }

            return default(TOut);
        }
    }
}
