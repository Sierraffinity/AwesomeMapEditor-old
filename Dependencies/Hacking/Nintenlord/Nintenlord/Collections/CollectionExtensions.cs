using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Nintenlord.Event_assembler.Collections
{
    /// <summary>
    /// Extensions and helper methods to .NET collections
    /// </summary>
    static class CollectionExtensions
    {
        public static bool Or(this IEnumerable<bool> collection)
        {
            foreach (bool item in collection)
            {
                if (item)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool And(this IEnumerable<bool> collection)
        {
            foreach (bool item in collection)
            {
                if (!item)
                {
                    return false;
                }
            }
            return true;
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
        
        public static int IndexOf<T>(this T[] array, Predicate<T> match)
        {
            int index = -1;

            for (int i = 0; i < array.Length; i++)
            {
                if (match(array[i]))
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        public static int LastIndexOf<T>(this T[] array, Predicate<T> match)
        {
            int i;
            for (i = array.Length - 1; i >= 0; i--)
            {
                if (match(array[i]))
                {
                    break;
                }
            }

            return i;
        }

        public static int IndexOf<T>(this T[] array, T[] toFind)
        {
            for (int i = 0; i < array.Length - toFind.Length; i++)
            {
                bool found = true;
                for (int j = 0; j < toFind.Length && found; j++)
                {
                    if (!array[i+j].Equals(toFind[j]))
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    return i;
                }
            }
            return -1;
        }

        public static bool ContainsAnyOf<T>(this T[] array, T[] toContain)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < toContain.Length; j++)
                {
                    if (array[i].Equals(toContain[j]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void Move<T>(this T[] array, int from, int to, int length)
        {
            if (from + length > array.Length || to + length > array.Length || length < 0)
            {
                throw new IndexOutOfRangeException();
            }
            T[] temp = new T[length];
            Array.Copy(array, from, temp, 0, length);
            Array.Copy(temp, 0, array, to, length);
        }

        public static T[] GetArray<T>(this T item)
        {
            return new T[] { item };
        }
        
        public static bool Contains<T>(this T[] array, T item)
        {
            foreach (T item2 in array)
            {
                if (item2.Equals(item))
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

        public static bool Equals<T>(this T[] array1, int index1, T[] arra2, int index2, int length)
        {
            for (int i = 0; i < length; i++)
            {
                if (!array1[i + index1].Equals(arra2[i+index2]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}