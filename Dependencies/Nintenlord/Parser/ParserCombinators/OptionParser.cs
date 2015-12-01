using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class OptionalParser<TIn, TOut> : Parser<TIn, TOut>
    {
        readonly TOut defaultVat;
        readonly IParser<TIn, TOut> parser;

        public OptionalParser(IParser<TIn, TOut> parser) : this(default(TOut), parser)
        {
        }
        public OptionalParser(TOut defaultVat, IParser<TIn, TOut> parser)
        {
            this.defaultVat = defaultVat;
            this.parser = parser;
        }

        protected override TOut ParseMain(IO.Scanners.IScanner<TIn> scanner, out Match<TIn> match)
        {
            var result = parser.Parse(scanner, out match);
            if (!match.Success)
            {
                result = defaultVat;
                match = new Match<TIn>(scanner, 0);
            }
            return result;
        }
    }
}
