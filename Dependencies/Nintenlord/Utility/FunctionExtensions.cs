using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Utility
{
    public static class FunctionExtensions
    {
        public static T Curry<T, TIn1, TIn2>(this Tuple<TIn1, TIn2> tupple, Func<TIn1, TIn2, T> func)
        {            
            return func.Invoke(tupple.Item1, tupple.Item2);
        }

        public static T Curry<T, TIn1, TIn2, TIn3>(this Tuple<TIn1, TIn2, TIn3> tupple, Func<TIn1, TIn2, TIn3, T> func)
        {
            return func.Invoke(tupple.Item1, tupple.Item2, tupple.Item3);
        }

        public static T Curry<T, TIn1, TIn2, TIn3, TIn4>(this Tuple<TIn1, TIn2, TIn3, TIn4> tupple, Func<TIn1, TIn2, TIn3, TIn4, T> func)
        {
            return func.Invoke(tupple.Item1, tupple.Item2, tupple.Item3, tupple.Item4);
        }

        public static T Curry<T, TIn1, TIn2, TIn3, TIn4, TIn5>(this Tuple<TIn1, TIn2, TIn3, TIn4, TIn5> tupple,
            Func<TIn1, TIn2, TIn3, TIn4, TIn5, T> func)
        {
            return func.Invoke(tupple.Item1, tupple.Item2, tupple.Item3, tupple.Item4, tupple.Item5);
        }

        public static T Curry<T, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(this Tuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6> tupple,
            Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, T> func)
        {
            return func.Invoke(tupple.Item1, tupple.Item2, tupple.Item3, tupple.Item4, tupple.Item5, tupple.Item6);
        }

        public static T Curry<T, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>(this Tuple<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7> tupple,
            Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, T> func)
        {
            return func.Invoke(tupple.Item1, tupple.Item2, tupple.Item3, tupple.Item4, tupple.Item5, tupple.Item6, tupple.Item7);
        }
        public static Func<TIn2, T> Curry<T, TIn1, TIn2>(this TIn1 item, Func<TIn1, TIn2, T> func)
        {
            return x => func(item, x);
        }

        public static Func<TIn2, TIn3, T> Curry<T, TIn1, TIn2, TIn3>(
            this TIn1 item, Func<TIn1, TIn2, TIn3, T> func)
        {
            return (x, y) => func(item, x, y);
        }

        public static Func<TIn2, TIn3, TIn4, T> Curry<T, TIn1, TIn2, TIn3, TIn4>(
            this TIn1 item, Func<TIn1, TIn2, TIn3, TIn4, T> func)
        {
            return (x, y, z) => func(item, x, y, z);
        }

        public static Func<TIn2, TIn3, TIn4, TIn5, T> Curry<T, TIn1, TIn2, TIn3, TIn4, TIn5>(
            this TIn1 item, Func<TIn1, TIn2, TIn3, TIn4, TIn5, T> func)
        {
            return (x, y, z, w) => func(item, x, y, z, w);
        }

        public static Func<TIn2, TIn3, TIn4, TIn5, TIn6, T> Curry<T, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(
            this TIn1 item, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, T> func)
        {
            return (x, y, z, w, f) => func(item, x, y, z, w, f);
        }

        public static Func<TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, T> Curry<T, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>(
            this TIn1 item, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, T> func)
        {
            return (x, y, z, w, f, k) => func(item, x, y, z, w, f, k);
        }


        public static Tuple<T1, T2, T3> Flatten<T1, T2, T3>(this Tuple<T1, Tuple<T2, T3>> tuple)
        {
            return Tuple.Create(tuple.Item1, tuple.Item2.Item1, tuple.Item2.Item2);
        }

        public static Tuple<T1, T2, T3> Flatten<T1, T2, T3>(this Tuple<Tuple<T1, T2>, T3> tuple)
        {
            return Tuple.Create(tuple.Item1.Item1, tuple.Item1.Item2, tuple.Item2);
        }

        public static Func<CanCauseError<TIn>, CanCauseError<TOut>> Map<TIn, TOut>(this Func<TIn, TOut> f)
        {
            return x => f.Map(x);
        }

        public static Func<CanCauseError<TIn1>, CanCauseError<TIn2>, CanCauseError<TOut>> Map<TIn1, TIn2, TOut>(
            this Func<TIn1, TIn2, TOut> f)
        {
            return (x, y) => f.Map(x, y);
        }

        public static CanCauseError<TOut> Map<TIn, TOut>(
            this Func<TIn, TOut> f, CanCauseError<TIn> x)
        {
            if (x.CausedError)
            {
                return CanCauseError<TOut>.Error(x.ErrorMessage);
            }
            else
            {
                return f(x.Result);
            }
            
        }

        public static CanCauseError<TOut> Map<TIn1, TIn2, TOut>(
            this Func<TIn1, TIn2, TOut> f, CanCauseError<TIn1> x, CanCauseError<TIn2> y)
        {
            if (x.CausedError)
            {
                return CanCauseError<TOut>.Error(x.ErrorMessage);
            }
            else if (y.CausedError)
            {
                return CanCauseError<TOut>.Error(y.ErrorMessage);
            }
            else
            {
                return f(x.Result, y.Result);
            }            
        }
    }
}
