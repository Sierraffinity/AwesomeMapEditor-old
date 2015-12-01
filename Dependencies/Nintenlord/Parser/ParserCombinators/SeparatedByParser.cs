using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.IO.Scanners;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class SeparatedByParser<TIn, TSeb, TOut> : Parser<TIn, List<TOut>>
    {
        readonly IParser<TIn, TSeb> separator;
        readonly IParser<TIn, TOut> results;

        public SeparatedByParser(IParser<TIn, TSeb> results, IParser<TIn, TOut> separated)
        {
            this.results = separated;
            this.separator = results;
        }

        Match<TIn> innerMatch;
        protected override List<TOut> ParseMain(IScanner<TIn> scanner, out Match<TIn> match)
        {
            innerMatch = new Match<TIn>(scanner, 0);
            var result = Enumerate(scanner).ToList();
            match = innerMatch;
            return result;
        }

        private IEnumerable<TOut> Enumerate(IScanner<TIn> scanner)
        {
            Match<TIn> latestMatch;
            TOut prim;
            while (true)
            {
                prim = results.Parse(scanner, out latestMatch);
                if (latestMatch.Success)
                {
                    innerMatch += latestMatch;
                    yield return prim;
                }
                else
                {
                    innerMatch = latestMatch;
                    yield break;
                }

                separator.Parse(scanner, out latestMatch);
                if (latestMatch.Success)
                    innerMatch += latestMatch;
                else yield break;
            }
        }
    }
}
