using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.IO.Scanners;

namespace Nintenlord.Parser.UnaryParsers
{
    public sealed class TryParser<TIn, TOut> : Parser<TIn, TOut>
    {
        readonly IParser<TIn, TOut> parserToTry;

        public TryParser(IParser<TIn, TOut> parserToTry)
        {
            this.parserToTry = parserToTry;
        }

        protected override TOut ParseMain(IScanner<TIn> scanner, out Match<TIn> match)
        {
            if (!scanner.CanSeek)
            {
                throw new ArgumentException("Scanner can't seek.");
            }

            long offset = scanner.Offset;
            TOut result = parserToTry.Parse(scanner, out match);
            if (!match.Success)
            {
                scanner.Offset = offset;
            }
            return result;
        }
    }
}
