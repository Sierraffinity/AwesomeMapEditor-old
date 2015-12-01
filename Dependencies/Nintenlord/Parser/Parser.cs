using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.IO.Scanners;
using Nintenlord.Parser.ParserCombinators;
using Nintenlord.Parser.UnaryParsers;
using Nintenlord.Parser.BinaryParsers;

namespace Nintenlord.Parser
{
    public abstract class Parser<TIn, TOut> : IParser<TIn,TOut>
    {
        public Parser()
        {
#if DEBUG
            //ParseEvent += new EventHandler<ParsingEventArgs<TIn, TOut>>(
            //    (sender, e) => Console.WriteLine("{0} Parser: {1}", sender, e.Match)
            //    );
#endif
        }

        protected abstract TOut ParseMain(IScanner<TIn> scanner, out Match<TIn> match);

        /// <summary>
        /// Parses input.
        /// </summary>
        /// <param name="scanner">Object to scan data from.</param>
        /// <param name="match">Match of the parser in scanner.</param>
        /// <returns>Result of parsing if match.Success is true, else undefined.</returns>
        public virtual TOut Parse(IScanner<TIn> scanner, out Match<TIn> match)
        {
            //if (scanner.IsAtEnd)
            //{
            //    match = new Match<TIn>(scanner, "End of stream.");
            //    return default(TOut);
            //}
            var result = ParseMain(scanner, out match);
            if (ParseEvent != null)
            {
                ParseEvent(this, new ParsingEventArgs<TIn, TOut>(match, result));
            }
            return result;
        }
        
        public event EventHandler<ParsingEventArgs<TIn, TOut>> ParseEvent;
        
        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}
