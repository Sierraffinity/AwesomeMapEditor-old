using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.IO.Scanners;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class SeparatedBy1Parser<TIn, TSeb, TOut> : Parser<TIn, List<TOut>>
    {
        readonly IParser<TIn, TSeb> separator;
        readonly IParser<TIn, TOut> results;

        public SeparatedBy1Parser(IParser<TIn, TSeb> separator, IParser<TIn, TOut> results)
        {
            this.results = results;
            this.separator = separator;
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
            TOut prim = results.Parse(scanner, out latestMatch);
            if (latestMatch.Success)
            {
                innerMatch += latestMatch;
                yield return prim;
                while (true)
                {
                    separator.Parse(scanner, out latestMatch);
                    if (latestMatch.Success)
                        innerMatch += latestMatch;
                    else yield break;

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
                }
            }
            else
            {
                innerMatch = latestMatch;
            }

        }
    }
}
