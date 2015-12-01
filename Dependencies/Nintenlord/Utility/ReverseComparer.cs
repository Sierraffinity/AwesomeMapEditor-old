using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Utility
{
    public class ReverseComparer<T> : IComparer<T>
    {
        private static ReverseComparer<T> defaultComparer;
        public static ReverseComparer<T> Default
        {
            get
            {
                if (defaultComparer == null)
                {
                    defaultComparer = new ReverseComparer<T>(Comparer<T>.Default);
                }
                return defaultComparer;
            }
        }

        IComparer<T> baseComparer;

        public ReverseComparer(IComparer<T> baseComparer)
        {
            this.baseComparer = baseComparer;
        }

        #region IComparer<T> Members

        public int Compare(T x, T y)
        {
            return baseComparer.Compare(y, x);
        }

        #endregion
    }
}
