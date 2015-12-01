using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.IO.Scanners;

namespace Nintenlord.Parser.BinaryParsers
{
    public sealed class OrParser<TIn, TOut> : Parser<TIn, TOut>
    {
        readonly IParser<TIn, TOut> first, second;

        public OrParser(IParser<TIn, TOut> first, IParser<TIn, TOut> second)
        {
            this.first = first;
            this.second = second;
        }

        protected override TOut ParseMain(IScanner<TIn> scanner, out Match<TIn> match)
        {
            long currentOffset = scanner.Offset;
            TOut result = first.Parse(scanner, out match);

            if (!match.Success)
            {
                if (currentOffset == scanner.Offset)
                {
                    result = second.Parse(scanner, out match);
                }
                else
                {
                    match = new Match<TIn>(scanner, "First parser consumed input.");
                }
            }

            return result;
        }
    }
}
