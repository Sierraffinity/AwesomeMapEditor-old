using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.IO.Scanners;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class Many1Parser<TIn, TOut> : Parser<TIn, List<TOut>>
    {
        readonly IParser<TIn, TOut> toRepeat;

        public Many1Parser(IParser<TIn, TOut> toRepeat)
        {
            this.toRepeat = toRepeat;
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

            TOut prim = toRepeat.Parse(scanner, out latestMatch);
            if (!latestMatch.Success)
            {
                innerMatch = latestMatch;
                yield break;
            }
            else
            {
                innerMatch += latestMatch;
                while (true)
                {
                    prim = toRepeat.Parse(scanner, out latestMatch);
                    if (latestMatch.Success)
                    {
                        innerMatch += latestMatch;
                        yield return prim;
                    }
                    else
                    {
                        yield break;
                    }
                }
            }
        }
    }
}
