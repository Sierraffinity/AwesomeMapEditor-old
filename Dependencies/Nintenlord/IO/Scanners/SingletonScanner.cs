using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.IO.Scanners
{
    public sealed class SingletonScanner<T> : IScanner<T>
    {
        T item;
        bool read;

        public SingletonScanner(T item)
        {
            this.item = item;
            read = false;
        }

        #region IScanner<T> Members

        public bool IsAtEnd
        {
            get { return read; }
        }

        public long Offset
        {
            get
            {
                if (read)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                switch (value)
                {
                    case 0:
                        read = false;
                        break;
                    case 1:
                        read = true;
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
        }

        public bool MoveNext()
        {
            if (!read)
            {
                read = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public T Current
        {
            get
            {
                return item;
            }
        }

        public bool CanSeek
        {
            get { return true; }
        }
        
        public IEnumerable<T> Substring(long Offset, int Length)
        {
            if (Offset != 0 || Length < 0 || Length > 1)
            {
                throw new ArgumentException();
            }

            if (Length == 1)
            {
                yield return item;
            }
        }
        
        public bool CanTakeSubstring
        {
            get { return true; }
        }

        #endregion
    }
}
