// -----------------------------------------------------------------------
// <copyright file="ParserHelpers.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nintenlord.Parser.ParserCombinators;
using Nintenlord.Parser.BinaryParsers;
using Nintenlord.Parser.UnaryParsers;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    static public class ParserHelpers
    {
        static public BetweenParser<TIn, TStart, TEnd, TOut> Between<TIn, TOut, TStart, TEnd>(
            this IParser<TIn, TOut> parser, 
            IParser<TIn, TStart> start, 
            IParser<TIn, TEnd> end)
        {
            return new BetweenParser<TIn, TStart, TEnd, TOut>(start, parser, end);
        }

        static public ManyParser<TIn, TOut> Many<TIn, TOut>(this IParser<TIn, TOut> parser)
        {
            return new ManyParser<TIn, TOut>(parser);
        }

        static public Many1Parser<TIn, TOut> Many1<TIn, TOut>(this IParser<TIn, TOut> parser)
        {
            return new Many1Parser<TIn, TOut>(parser);
        }

        static public OrParser<TIn, TOut> Or<TIn, TOut>(this IParser<TIn, TOut> choise1, IParser<TIn, TOut> choise2)
        {
            return new OrParser<TIn, TOut>(choise1, choise2);
        }

        static public ChoiseParser<TIn, TOut> Choise<TIn, TOut>(params IParser<TIn, TOut>[] parsers)
        {
            return new ChoiseParser<TIn, TOut>(parsers);
        }

        static public ChoiseParser<TIn, TOut> Choise<TIn, TOut>(this IEnumerable<IParser<TIn, TOut>> parsers)
        {
            return new ChoiseParser<TIn, TOut>(parsers);
        }

        static public TransformParser<TIn, TMiddle, TOut> Transform<TIn, TMiddle, TOut>(
            this IParser<TIn, TMiddle> parser, Converter<TMiddle, TOut> f)
        {
            return new TransformParser<TIn, TMiddle, TOut>(parser, f);
        }

        static public SeparatedBy1Parser<TIn, TSeb, TOut> SepBy1<TIn, TSeb, TOut>(
            this IParser<TIn, TOut> parser, IParser<TIn, TSeb> separator)
        {
            return new SeparatedBy1Parser<TIn, TSeb, TOut>(separator, parser);
        }

        static public SeparatedByParser<TIn, TSeb, TOut> SepBy<TIn, TSeb, TOut>(
            this IParser<TIn, TOut> parser, IParser<TIn, TSeb> separator)
        {
            return new SeparatedByParser<TIn, TSeb, TOut>(separator, parser);
        }

        static public SatisfyParser<T> Satisfy<T>(this Predicate<T> predicate)
        {
            return new SatisfyParser<T>(predicate);
        }

        static public OptionalParser<TIn, TOut> Optional<TIn, TOut>(this IParser<TIn, TOut> parser)
        {
            return new OptionalParser<TIn, TOut>(parser);
        }

        static public OptionalParser<TIn, TOut> Optional<TIn, TOut>(this IParser<TIn, TOut> parser, TOut defaultVal)
        {
            return new OptionalParser<TIn, TOut>(defaultVal, parser);
        }

        static public NameParser<TIn, TOut> Name<TIn, TOut>(this IParser<TIn, TOut> parser, string name)
        {
            return new NameParser<TIn, TOut>(parser, name);
        }

        static public CombineParser<TIn, TMiddle1, TMiddle2, TOut> Combine<TIn, TMiddle1, TMiddle2, TOut>(
            this IParser<TIn, TMiddle1> first, 
            IParser<TIn, TMiddle2> second, 
            Func<TMiddle1, TMiddle2, TOut> comb)
        {
            return new CombineParser<TIn, TMiddle1, TMiddle2, TOut>(first, second, comb);
        }

        static public CombineParser<TIn, TMiddle1, TMiddle2, TMiddle3, TOut> 
            Combine<TIn, TMiddle1, TMiddle2, TMiddle3, TOut>(
            this IParser<TIn, TMiddle1> first,
            IParser<TIn, TMiddle2> second,
            IParser<TIn, TMiddle3> third,
            Func<TMiddle1, TMiddle2, TMiddle3, TOut> comb)
        {
            return new CombineParser<TIn, TMiddle1, TMiddle2, TMiddle3, TOut>(first, second, third, comb);
        }

        static public CombineParser<TIn, TMiddle1, TMiddle2, TMiddle3, TMiddle4, TOut>
            Combine<TIn, TMiddle1, TMiddle2, TMiddle3, TMiddle4, TOut>(
            this IParser<TIn, TMiddle1> first,
            IParser<TIn, TMiddle2> second,
            IParser<TIn, TMiddle3> third,
            IParser<TIn,TMiddle4> fourth,
            Func<TMiddle1, TMiddle2, TMiddle3, TMiddle4, TOut> comb)
        {
            return new CombineParser<TIn, TMiddle1, TMiddle2, TMiddle3, TMiddle4, TOut>
                (first, second, third, fourth, comb);
        }
    }
}
