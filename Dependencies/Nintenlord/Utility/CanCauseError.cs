using System;
using System.Collections.Generic;

namespace Nintenlord.Utility
{
    public sealed class CanCauseError<T, TError>
    {
        bool error;
        T result;
        TError errorState;
        public bool CausedError
        {
            get { return error; }
        }
        public T Result
        {
            get
            {
                if (error)
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    return result;
                }
            }
        }
        public TError ErrorState
        {
            get
            {
                if (!error)
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    return errorState;
                }
            }

        }

        public static CanCauseError<T, TError> NoError(T result)
        {
            var results = new CanCauseError<T, TError>();
            results.result = result;
            results.error = false;
            return results;
        }

        public static CanCauseError<T, TError> Error(TError error)
        {
            var result = new CanCauseError<T, TError>();
            result.error = true;
            result.errorState = error;            
            return result;
        }

        public static implicit operator CanCauseError<T, TError>(T value)
        {
            return NoError(value);
        }
    }

    public sealed class CanCauseError<T>
    {
        private static readonly Dictionary<string, CanCauseError<T>> cachedErrors 
            = new Dictionary<string, CanCauseError<T>>();

        T result;
        bool error;
        string errorMessage;
        public bool CausedError
        {
            get { return error; }
        }
        public string ErrorMessage
        {
            get
            {
                if (error)
                {
                    return errorMessage;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }
        public T Result
        {
            get 
            {
                if (error)
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    return result;
                }
            }
        }

        public override string ToString()
        {
            if (!error)
            {
                return string.Format("Success: {0}", result);
            }
            else
            {
                return string.Format("Error: {0}", errorMessage);
            }
        }

        public static CanCauseError<T> NoError(T result)
        {
            CanCauseError<T> results = new CanCauseError<T>();
            results.result = result;
            results.error = false;
            return results;
        }

        public static CanCauseError<T> Error(string errorMessages)
        {
            CanCauseError<T> result;
            if (!cachedErrors.TryGetValue(errorMessages, out result))
            {
                result = new CanCauseError<T>();
                result.error = true;
                result.errorMessage = errorMessages;
                cachedErrors[errorMessages] = result;
            }
            return result;
        }

        public static CanCauseError<T> Error(string errorMessages, params object[] objects)
        {
            CanCauseError<T> result;
            if (!cachedErrors.TryGetValue(errorMessages, out result))
            {
                result = new CanCauseError<T>();
                result.error = true;
                string text = string.Format(errorMessages, objects);
                result.errorMessage = text;
                cachedErrors[text] = result;
            }
            return result;
        }
        
        public static implicit operator bool(CanCauseError<T> error)
        {
            return error.CausedError;
        }
        
        public static implicit operator CanCauseError<T>(T value)
        {
            return NoError(value);
        }

        public static explicit operator CanCauseError(CanCauseError<T> error)
        {
            if (error.CausedError)
            {
                return CanCauseError.Error(error.errorMessage);
            }
            else
            {
                return CanCauseError.NoError;
            }
        }

        public static explicit operator CanCauseError<T>(CanCauseError error)
        {
            if (error.CausedError)
            {
                return CanCauseError<T>.Error(error.ErrorMessage);
            }
            else
            {
                return CanCauseError<T>.NoError(default(T));
            }
        }


    }

    public sealed class CanCauseError
    {
        private static readonly CanCauseError noError;
        private static readonly Dictionary<string, CanCauseError> cachedErrors
            = new Dictionary<string, CanCauseError>();

        static CanCauseError()
        {
            noError = new CanCauseError();
            noError.error = false;
        }

        bool error;
        string errorMessage;
        public bool CausedError
        {
            get { return error; }
        }
        public string ErrorMessage
        {
            get 
            {
                if (error)
                {
                    return errorMessage;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public static CanCauseError NoError
        {
            get { return noError; }
        }

        public static CanCauseError Error(string errorMessages)
        {
            CanCauseError result;
            if (!cachedErrors.TryGetValue(errorMessages, out result))
            {
                result = new CanCauseError();
                result.error = true;
                result.errorMessage = errorMessages;
                cachedErrors[errorMessages] = result;
            }
            return result;
        }
        
        public static implicit operator bool(CanCauseError error)
        {
            return error.CausedError;
        }
    }

    public static class CanCauseErrorHelpers
    {
        public static CanCauseError<C> SelectMany<A, B, C>(
            this CanCauseError<A> a, Func<A, CanCauseError<B>> func, Func<A, B, C> select)
        {
            if (a.CausedError)
            {
                return CanCauseError<C>.Error(a.ErrorMessage);
            }
            else
            {
                var b = func(a.Result);
                if (b.CausedError)
                {
                    return CanCauseError<C>.Error(b.ErrorMessage);
                }
                else
                {
                    return select(a.Result, b.Result);
                }
            }
        }

        public static CanCauseError<C, Error> SelectMany<A, B, C, Error>(
            this CanCauseError<A, Error> a, Func<A, CanCauseError<B, Error>> func, Func<A, B, C> select)
        {
            if (a.CausedError)
            {
                return CanCauseError<C, Error>.Error(a.ErrorState);
            }
            else
            {
                var b = func(a.Result);
                if (b.CausedError)
                {
                    return CanCauseError<C, Error>.Error(b.ErrorState);
                }
                else
                {
                    return select(a.Result, b.Result);
                }
            }
        }

        public static CanCauseError ActionIfSuccess<T>(this Func<CanCauseError<T>> function, Action<T> action)
        {
            var result = function();
            if (result.CausedError)
            {
                return (CanCauseError)result;
            }
            else
            {
                action(result.Result);
                return CanCauseError.NoError;
            }
        }

        public static CanCauseError<TOut> ConvertError<TIn, TOut>(this CanCauseError<TIn> error)
        {
            if (error.CausedError)
            {
                return CanCauseError<TOut>.Error(error.ErrorMessage);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public static CanCauseError<T> ExceptionToError<T>(this Func<T> f)
        {
            try
            {
                var result = f();
                return CanCauseError<T>.NoError(result);
            }
            catch (Exception e)
            {
                return CanCauseError<T>.Error(e.Message);
            }
        }



        public static CanCauseError<T> Bind<T>(this Func<CanCauseError<T>> first, Func<T, CanCauseError<T>> second)
        {
            CanCauseError<T> firstResult = first();
            if (firstResult.CausedError)
            {
                return firstResult;
            }
            else
            {
                return second(firstResult.Result);
            }
        }

        public static Func<TIn, CanCauseError<TOut>> Bind<TIn, TMiddle, TOut>(this Func<TIn, CanCauseError<TMiddle>> first, Func<TMiddle, CanCauseError<TOut>> second)
        {
            return x =>
            {
                var firstRes = first(x);
                if (firstRes.CausedError)
                {
                    return CanCauseError<TOut>.Error(firstRes.ErrorMessage);
                }

                return second(firstRes.Result);
            };
        }

        public static CanCauseError<T> Bind<T>(this IEnumerable<Func<T, CanCauseError<T>>> functions, T start)
        {
            CanCauseError<T> result = CanCauseError<T>.NoError(start);
            foreach (var func in functions)
            {
                result = func(result.Result);
                if (result.CausedError)
                {
                    break;
                }
            }
            return result;
        }
        
        public static CanCauseError Bind(this Func<CanCauseError> first, Func<CanCauseError> second)
        {
            CanCauseError firstResult = first();
            if (firstResult.CausedError)
            {
                return firstResult;
            }
            else
            {
                return second();
            }
        }

        public static CanCauseError Bind(this IEnumerable<Func<CanCauseError>> functions)
        {
            CanCauseError result = null;
            foreach (var func in functions)
            {
                result = func();
                if (result.CausedError)
                {
                    break;
                }
            }
            return result;
        }
    }
}
