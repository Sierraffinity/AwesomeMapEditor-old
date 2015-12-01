using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.IO.Scanners;

namespace Nintenlord.Parser.BinaryParsers
{
    sealed class PassParser<TIn, TMiddle, TOut> : Parser<TIn,TOut>
    {
        readonly IParser<TIn, TMiddle> first;
        readonly IParser<TMiddle, TOut> second;

        public PassParser(IParser<TIn, TMiddle> first, IParser<TMiddle, TOut> second)
        {
            this.first = first;
            this.second = second;
        }

        protected override TOut ParseMain(IScanner<TIn> scanner, out Match<TIn> match)
        {
            TMiddle middle = first.Parse(scanner, out match);

            if (!match.Success)
                return default(TOut);

            //TOut reslt = second.Parse();



            throw new NotImplementedException();
        }
    }
}
