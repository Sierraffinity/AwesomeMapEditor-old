using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.IO.Scanners
{
    public sealed class StringScanner : IScanner<char>
    {
        string s;
        int i;
        int length;
        int startIndex;


        public StringScanner(string s) : this(s, 0, s.Length)
        {

        }
        public StringScanner(string s, int index) : this(s, index, s.Length - index)
        {

        }
        public StringScanner(string s, int index, int length)
        {
            this.s = s;
            this.i = index;
            this.length = length;
            this.startIndex = index;
        }

        #region IScanner<char> Members

        public bool IsAtEnd
        {
            get { return i - startIndex >= length; }
        }

        public long Offset
        {
            get
            {
                return i - startIndex;
            }
            set
            {
                i = (int)(value - startIndex);
            }
        }

        public bool MoveNext()
        {
            return ++i < startIndex + length;
        }

        public char Current
        {
            get
            {
                return s[i];
            }
        }

        public bool CanSeek
        {
            get { return true; }
        }
        
        public IEnumerable<char> Substring(long Offset, int Length)
        {
            return s.Substring((int)Offset, length);
        }

        public bool CanTakeSubstring
        {
            get { return true; }
        }

        #endregion

    }
}
