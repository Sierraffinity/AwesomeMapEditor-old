using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.IO.Scanners;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class CountParser<TIn, TOut> : Parser<TIn, List<TOut>>
    {
        readonly IParser<TIn, TOut> parser;
        readonly int count;

        public CountParser(IParser<TIn, TOut> parser, int count)
        {
            this.parser = parser;
            this.count = count;
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
            for (int i = 0; i < count; i++)
            {
                var temp = parser.Parse(scanner, out latestMatch);
                if (!latestMatch.Success)
                {
                    innerMatch = latestMatch;
                    yield break;
                }
                innerMatch += latestMatch;
                yield return temp;
            }
        }
    }
}
