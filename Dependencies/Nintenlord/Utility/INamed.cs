using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Utility
{
    /// <summary>
    /// Object that has a unique name 
    /// </summary>
    /// <typeparam name="T">Type of the name</typeparam>
    public interface INamed<out T>
    {
        /// <summary>
        /// Name of the object. Must not change in equals mind
        /// </summary>
        T Name { get; }
    }

    public static class NamedHelper
    {
        public static Dictionary<T, NamedT> GetDictionary<T, NamedT>(this IEnumerable<NamedT> named)
            where NamedT : INamed<T>
        {
            Dictionary<T, NamedT> namedPairs = new Dictionary<T, NamedT>();
            foreach (var item in named)
            {
                namedPairs.Add(item.Name, item);
            }
            return namedPairs;
        }

        public static bool AllAreUnique<T, NamedT>(IEnumerable<NamedT> items)
            where NamedT : INamed<T>
        {
            ISet<T> set = new HashSet<T>();
            foreach (var item in items)
            {
                if (set.Contains(item.Name))
                {
                    return false;
                }
                set.Add(item.Name);
            }
            return true;
        }

        public static IEnumerable<KeyValuePair<T, NamedT>> GetEnumerator<T, NamedT>(this IEnumerable<NamedT> named)
            where NamedT : INamed<T>
        {
            foreach (var item in named)
            {
                yield return new KeyValuePair<T, NamedT>(item.Name, item);
            }
        }

        public class NamedEqualityComparer<T, NamedT> 
            : IEqualityComparer<NamedT>
            where NamedT : INamed<T>
        {
            IEqualityComparer<T> coreComp;

            public NamedEqualityComparer() 
                : this(EqualityComparer<T>.Default)
            {

            }

            public NamedEqualityComparer(IEqualityComparer<T> comparer)
            {
                this.coreComp = comparer;
            }

            #region IEqualityComparer<NamedT> Members

            public bool Equals(NamedT x, NamedT y)
            {
                return coreComp.Equals(x.Name, y.Name);
            }

            public int GetHashCode(NamedT obj)
            {
                return coreComp.GetHashCode(obj.Name);
            }

            #endregion
        }
    }
}
