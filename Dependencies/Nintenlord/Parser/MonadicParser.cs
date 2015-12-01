// -----------------------------------------------------------------------
// <copyright file="MonadicParser.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nintenlord.IO.Scanners;

    public delegate Tuple<TOut, Match<TIn>> MonadicParser<TIn, TOut>(IScanner<TIn> scanner);
    
    //static public class MonadicParserHelpers<TIn>
    //{
    //    static public MonadicParser<TIn, TOut> SelectMany<TOut, TOut1, TOut2>(
    //        this MonadicParser<TIn, TOut1> parser1,
    //        Func<TOut1, MonadicParser<TIn, TOut2>> collectionSelector,
    //        Func<TOut1, TOut2, TOut> resultSelector)
    //    {
    //        return scanner =>
    //            {
    //                var firsstResult = parser1(scanner);
    //                if (!firsstResult.Item2.Success)
    //                {
    //                    return Tuple.Create(default(TOut), firsstResult.Item2);
    //                }
    //                else
    //                {
    //                    var secParser = collectionSelector(firsstResult.Item1);
    //                    var secResult = secParser(scanner);
    //                    if (!secResult.Item2.Success)
    //                    {
    //                        return Tuple.Create(default(TOut), secResult.Item2);
    //                    }
    //                    else
    //                    {                            
    //                        return Tuple.Create(
    //                            resultSelector(firsstResult.Item1, secResult.Item1),
    //                            firsstResult.Item2 + secResult.Item2);
    //                    }
    //                }
    //            };
    //    }

    //    static public MonadicParser<TIn, TOut> GetFromParser<TOut>(this IParser<TIn, TOut> parser)
    //    {
    //        return scanner =>
    //            {
    //                Match<TIn> match;
    //                var result = parser.Parse(scanner, out match);
    //                return Tuple.Create(result, match);
    //            };
    //    }

    //    static public MonadicParser<TIn, TOut> Always<TOut>()
    //    {
    //        return scanner => Tuple.Create(default(TOut), new Match<TIn>(scanner, 0));
    //    }

    //    static public MonadicParser<TIn, TOut> Never<TOut>()
    //    {
    //        return scanner => Tuple.Create(default(TOut), new Match<TIn>(scanner, "Epic fail."));
    //    }

    //    static public MonadicParser<TIn, TOut> Or<TOut>(
    //        this MonadicParser<TIn, TOut> first, 
    //        MonadicParser<TIn, TOut> second)
    //    {
    //        //return from a in first
    //        //       select null;
    //        return null;
    //    }
    //}
}
