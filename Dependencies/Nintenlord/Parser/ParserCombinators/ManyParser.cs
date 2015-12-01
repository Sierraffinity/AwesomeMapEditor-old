using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.IO.Scanners;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class ManyParser<TIn, TOut> : Parser<TIn, List<TOut>>
    {
        public IParser<TIn, TOut> ToRepeat
        {
            get { return toRepeat; }
            set { toRepeat = value; }
        }

        IParser<TIn, TOut> toRepeat;

        public ManyParser(IParser<TIn, TOut> toRepeat)
        {
            this.toRepeat = toRepeat;
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
            while (true)
            {
                TOut prim = toRepeat.Parse(scanner, out latestMatch);
                if (latestMatch.Success)
                {
                    yield return prim;
                    innerMatch += latestMatch;
                }
                else
                {
                    yield break;
                }
            }
        }
    }
}
