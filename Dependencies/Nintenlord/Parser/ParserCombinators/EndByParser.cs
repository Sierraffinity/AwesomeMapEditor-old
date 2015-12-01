using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.IO.Scanners;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class EndByParser<TIn, TEnd, TOut> : Parser<TIn, List<TOut>>
    {
        readonly IParser<TIn, TOut> results;
        readonly IParser<TIn, TEnd> separator;

        public EndByParser(IParser<TIn, TOut> results, IParser<TIn, TEnd> separator)
        {
            this.results = results;
            this.separator = separator;
        }

        Match<TIn> innerMatch;
        protected override List<TOut> ParseMain(IScanner<TIn> scanner, out Match<TIn> match)
        {
            innerMatch = new Match<TIn>(scanner, 0);
            var result = Enumarate(scanner).ToList();
            match = innerMatch;
            return result;
        }

        private IEnumerable<TOut> Enumarate(IScanner<TIn> scanner)
        {
            Match<TIn> latestMatch;

            TOut outRes;
            while (true)
            {
                outRes = results.Parse(scanner, out latestMatch);
                if (latestMatch.Success)
                {
                    innerMatch += latestMatch;
                    yield return outRes;
                }
                else yield break;

                separator.Parse(scanner, out latestMatch);
                if (latestMatch.Success)
                    innerMatch += latestMatch;
                else
                {
                    innerMatch = latestMatch;
                    yield break;
                }
            }
        }
    }
}
