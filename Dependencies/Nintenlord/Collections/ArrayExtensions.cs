using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Collections
{
    public static class ArrayExtensions
    {
        [Obsolete("Use Array.FindIndex()", true)]
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

        [Obsolete("Use Array.FindLastIndex()", true)]
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
            return array.IndexOf<T>(toFind, EqualityComparer<T>.Default);
        }

        public static int IndexOf<T>(this T[] array, T[] toFind, IEqualityComparer<T> eq)
        {
            for (int i = 0; i < array.Length - toFind.Length; i++)
            {
                bool found = true;
                for (int j = 0; j < toFind.Length && found; j++)
                {
                    if (!eq.Equals(array[i + j], toFind[j]))
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
            return array.ContainsAnyOf(toContain, EqualityComparer<T>.Default);
        }

        public static bool ContainsAnyOf<T>(this T[] array, T[] toContain, IEqualityComparer<T> eq)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < toContain.Length; j++)
                {
                    if (eq.Equals(array[i], toContain[j]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public static bool Equals<T>(this T[] array1, int index1, T[] array2, int index2, int length)
        {
            return array1.Equals(index1, array2, index2, length, EqualityComparer<T>.Default);
        }

        public static bool Equals<T>(this T[] array1, int index1, T[] array2, int index2, int length, IEqualityComparer<T> eq)
        {
            if (index1 < 0 ||
                index2 < 0 ||
                index1 + length > array1.Length ||
                index2 + length > array2.Length)
            {
                throw new IndexOutOfRangeException();
            }

            for (int i = 0; i < length; i++)
            {
                if (!eq.Equals(array1[i + index1], array2[i + index2]))
                {
                    return false;
                }
            }
            return true;
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

        public static T[] GetRange<T>(this T[] array, int index)
        {
            return array.GetRange(index, array.Length - index);
        }

        public static T[] GetRange<T>(this T[] array, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(array, index, result, 0, length);
            return result;
        }

        public static IEnumerable<T> EnumerateSublist<T>(this T[] array, int index, int length)
        {
            for (int i = index; i < index + length; i++)
            {
                yield return array[i];
            }
        }

        public static T[] ApplyToAll<T>(this T[] a, T[] b, Func<T, T, T> toApply, T defaultVal)
        {
            T[] result = new T[Math.Max(a.Length, b.Length)];
            int min = Math.Min(a.Length, b.Length);
            int i;
            for (i = 0; i < min; i++)
            {
                result[i] = toApply(a[i], b[i]);
            }
            if (a.Length > b.Length)
            {
                for (i = 0; i < result.Length; i++)
                {
                    result[i] = toApply(a[i], defaultVal);
                }
            }
            else if (a.Length < b.Length)
            {
                for (i = 0; i < result.Length; i++)
                {
                    result[i] = toApply(b[i], defaultVal);
                }
            }
            return result;
        }

        public static TOut[] ApplyToAll<TInLeft, TInRight, TOut>
            (this TInLeft[] a, TInRight[] b, Func<TInLeft, TInRight, TOut> toApply)
        {
            TOut[] result = new TOut[Math.Min(a.Length, b.Length)];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = toApply(a[i], b[i]);
            }
            return result;
        }

        public static int Total(this int[] array)
        {
            int result = 1;
            foreach (var item in array)
            {
                result *= item;
            }
            return result;
        }

        public static int AmountOfSame<T>(T[] array1, int index1, T[] array2, int index2)
        {
            int length = Math.Min(array1.Length - index1, array2.Length - index2);
            int i;
            for (i = 0; i < length; i++)
            {
                if (!array1[index1 + i].Equals(array2[index2 + i]))
                {
                    return i;
                }
            }
            return i;
        }

        public static IEnumerable<T> Flatten<T>(this T[][] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    yield return array[i][j];
                }
            }
        }
    }
}
