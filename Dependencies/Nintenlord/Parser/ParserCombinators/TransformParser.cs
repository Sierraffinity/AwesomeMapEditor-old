using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Parser.ParserCombinators
{
    public sealed class TransformParser<TIn, TMiddle, TOut> : Parser<TIn, TOut>
    {
        IParser<TIn, TMiddle> parser;
        Converter<TMiddle, TOut> converter;

        public TransformParser(IParser<TIn, TMiddle> parser, Converter<TMiddle, TOut> converter)
        {
            this.parser = parser;
            this.converter = converter;
        }

        protected override TOut ParseMain(IO.Scanners.IScanner<TIn> scanner, out Match<TIn> match)
        {
            TMiddle middle = parser.Parse(scanner, out match);
            if (match.Success)
            {
                return converter(middle);
            }
            else
            {
                return default(TOut);
            }
        }
    }
}
