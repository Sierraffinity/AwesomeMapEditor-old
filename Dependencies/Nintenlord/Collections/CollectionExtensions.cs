using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Nintenlord.Collections
{
    /// <summary>
    /// Extensions and helper methods to .NET collections
    /// </summary>
    public static class CollectionExtensions
    {
        public static bool Or(this IEnumerable<bool> collection)
        {
            return collection.Any(x => x);
        }

        public static bool And(this IEnumerable<bool> collection)
        {
            return collection.All(x => x);
        }

        public static string ToElementWiseString<T>(this IEnumerable<T> collection)
        {
            StringBuilder text = new StringBuilder("{");

            foreach (T item in collection)
            {
                text.AppendFormat("{0}, ", item.ToString());
            }

            if (text.Length > 1)
            {
                text.Remove(text.Length-2, 2);
            }
            text.Append("}");

            return text.ToString();
        }

        public static string ToElementWiseString<T>(this IEnumerable<T> collection, string separator, string beginning, string end)
        {
            StringBuilder text = new StringBuilder(beginning);

            foreach (T item in collection)
            {
                text.Append(item.ToString() + separator);
            }

            if (text.Length > 1)
            {
                text.Remove(text.Length - separator.Length, separator.Length);
            }
            text.Append(end);

            return text.ToString();
        }

        public static TValue GetValue<TKey, TValue>(this IEnumerable<Dictionary<TKey, TValue>> scopes, TKey kay)
        {
            TValue result = default(TValue);

            foreach (Dictionary<TKey, TValue> item in scopes)
            {
                if (item.TryGetValue(kay, out result))
                {
                    break;
                }
            }
            return result;
        }

        public static bool ContainsKey<TKey, TValue>(this IEnumerable<Dictionary<TKey, TValue>> scopes, TKey kay)
        {
            foreach (Dictionary<TKey, TValue> item in scopes)
            {
                if (item.ContainsKey(kay))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool TryGetKey<TKey, TValue>(this IEnumerable<Dictionary<TKey, TValue>> scopes, TKey kay, out TValue value)
        {
            bool result = false;
            value = default(TValue);

            foreach (Dictionary<TKey, TValue> item in scopes)
            {
                if (item.TryGetValue(kay, out value))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        
        public static bool Contains<T>(this IEnumerable<T> array, Predicate<T> test)
        {
            foreach (T item2 in array)
            {
                if (test(item2))
                {
                    return true;
                }
            }
            return false;
        }

        public static int AmountOf<T>(this IEnumerable<T> array, T item)
        {
            int result = 0;
            foreach (T item2 in array)
            {
                if (item.Equals(item2))
                {
                    result++;
                }
            }
            return result;
        }
        
        public static string GetString(this IEnumerable<char> enume)
        {
            StringBuilder bldr;
            if (enume is ICollection<char>)
                bldr = new StringBuilder((enume as ICollection<char>).Count);
            else
                bldr = new StringBuilder();

            foreach (var item in enume)
            {
                bldr.Append(item);
            }
            return bldr.ToString();
        }

        public static string ToHumanString<T>(this IEnumerable<T> list)
        {
            T[] array = list.ToArray();
            if (array.Length > 1)
            {
                StringBuilder bldr = new StringBuilder();
                for (int i = 0; i < array.Length - 2; i++)
                {
                    bldr.Append(array[i].ToString());
                    bldr.Append(", ");
                }
                bldr.Append(array[array.Length - 2].ToString());
                bldr.Append(" & ");
                bldr.Append(array[array.Length - 1].ToString());
                return bldr.ToString();
            }
            else if (array.Length == 1)
            {
                return array[0].ToString();
            }
            else
            {
                return "";
            }

        }


        public static IEnumerable<T> Repeat<T>(this IEnumerable<T> toRepeat)
        {
            while (true)
            {
                foreach (var item in toRepeat)
                {
                    yield return item;
                }
            }
        }

        public static void AddAll<TKey, Tvalue>(this IDictionary<TKey, Tvalue> a,
            IEnumerable<KeyValuePair<TKey, Tvalue>> values)
        {
            foreach (var item in values)
            {
                a.Add(item.Key, item.Value);
            }
        }

        public static TValue GetOldOrSetNew<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
            where TValue : new()
        {
            TValue value;
            if (!dict.TryGetValue(key, out value))
            {
                value = new TValue();
                dict[key] = value;
            }
            return value;
        }

        public static IEnumerable<Tuple<int, T>> Index<T>(this IEnumerable<T> items)
        {
            int index = 0;
            foreach (var item in items)
            {
                yield return Tuple.Create(index, item);
                index++;
            }
        }

        public static int GetEqualsInBeginning<T>(this IList<T> a, IList<T> b, IEqualityComparer<T> comp)
        {
            int max = Math.Min(a.Count, b.Count);
            int count;
            for (count = 0; count < max; count++)
            {
                if (!comp.Equals(a[count], b[count]))
                {
                    break;
                }
            }
            return count;
        }
        public static int GetEqualsInBeginning<T>(this IList<T> a, IList<T> b)
        {
            return a.GetEqualsInBeginning(b, EqualityComparer<T>.Default);
        }
        

        public static IndexOverlay GetOverlay<T>(this IDictionary<int, T> dict, Func<T, int> measurement)
        {
            IndexOverlay result = new IndexOverlay();

            foreach (var item in dict)
            {
                int length = measurement(item.Value);
                result.AddIndexes(item.Key, length);
            }

            return result;
        }

        public static bool CanFit<T>(this IDictionary<int, T> dict, Func<T, int> measurement,
            int index, T item)
        {
            int lastIndex = index + measurement(item);

            for (int i = index; i < lastIndex; i++)
            {
                if (dict.ContainsKey(i))
                {
                    return false;
                }
            }
            for (int i = index - 1; i >= 0; i--)
            {
                T oldItem;
                if (dict.TryGetValue(i, out oldItem) && i + measurement(oldItem) > index)
                {
                    return false;
                }
            }

            return true;
        }

        public static IEnumerable<T> Flatten<T, TEnumarable>(this IEnumerable<TEnumarable> collection)
            where TEnumarable : IEnumerable<T> 
        {
            foreach (var item in collection)
            {
                foreach (var item2 in item)
                {
                    yield return item2;
                }
            }
        }

        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> collection)
        {
            foreach (var item in collection)
            {
                foreach (var item2 in item)
                {
                    yield return item2;
                }
            }
        }

        public static IEnumerable<TOut> ConvertAll<TIn, TOut>(this IEnumerable<TIn> enume, Func<TIn, TOut> conversion)
        {
            foreach (var item in enume)
            {
                yield return conversion(item);
            }
        }

    }
}