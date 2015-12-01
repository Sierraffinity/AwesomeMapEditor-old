using System;
using Nintenlord.IO.Scanners;

namespace Nintenlord.Parser
{
    /// <summary>
    /// Immutable object that describes the match of a parser.
    /// </summary>
    /// <typeparam name="T">Token type of the scanner.</typeparam>
    public sealed class Match<T> : ICloneable, IEquatable<Match<T>>
    {
        /// <summary>
        /// Scanner whose output was getting parsed.
        /// </summary>
        public IScanner<T> Scanner
        {
            get;
            private set;
        }
        /// <summary>
        /// Offset of the match in scanner.
        /// </summary>
        public long Offset
        {
            get;
            private set;
        }
        /// <summary>
        /// Length of a succesful match.
        /// </summary>
        public int Length
        {
            get;
            private set;
        }
        /// <summary>
        /// If the parser matched succesfully.
        /// </summary>
        public bool Success
        {
            get;
            private set;
        }
        /// <summary>
        /// First offset after a succesful match.
        /// Offset + Length == OffsetAfter
        /// </summary>
        public long OffsetAfter
        {
            get
            {
                return Offset + Length;
            }
        }
        /// <summary>
        /// True if successful matches length was 0.
        /// False if succesful matches length isn't 0.
        /// </summary>
        public bool Empty
        {
            get
            {
                return Length == 0; 
            }
        }
        /// <summary>
        /// Error string of an unsuccesful match.
        /// </summary>
        public string Error
        {
            get 
            {
                if (this.Success)
                {
                    throw new InvalidOperationException("Cannot get error text for successful match.");
                }
                else
                {
                    return error;
                }
            }
        }

        private readonly string error;

        /// <summary>
        /// Creates a succesful match of length 0 in
        /// scanners current offset.
        /// </summary>
        /// <param name="scanner">Scanner in which the match was made in.</param>
        public Match(IScanner<T> scanner) 
            : this(scanner, scanner.Offset, 0)
        {

        }
        /// <summary>
        /// Creates a succesful match of length <paramref name="length"/> in
        /// scanners current offset.
        /// </summary>
        /// <param name="scanner">Scanner in which the match was made in.</param>
        /// <param name="length">Length of the match.</param>
        public Match(IScanner<T> scanner, int length)
            : this(scanner, scanner.Offset, length)
        {

        }
        private Match(IScanner<T> scanner, long offset, int length)
        {
            if (scanner == null)
                throw new ArgumentNullException("scanner");
            if (length < 0)
                throw new IndexOutOfRangeException("Length was negative.");

            this.Scanner = scanner;
            this.Offset = offset;
            this.Length = length;
            this.error = null;
            this.Success = true;
        }
        /// <summary>
        /// Creates unsuccesful match in current offset
        /// </summary>
        /// <param name="scanner">Scanner in which the match was made in.</param>
        /// <param name="errorString">String describing the reason why the match was unsuccesful.</param>
        public Match(IScanner<T> scanner, string errorString)
        {
            if (scanner == null)
                throw new ArgumentNullException("scanner");
            if (errorString == null)
                throw new ArgumentNullException("errorString");

            this.Scanner = scanner;
            this.Offset = scanner.Offset;
            this.Length = -1;
            this.error = errorString;
        }
        private Match(Match<T> copyMe)
        {
            this.Scanner = copyMe.Scanner;
            this.Offset = copyMe.Offset;
            this.Length = copyMe.Length;
            this.error = copyMe.error;
            this.Success = copyMe.Success;
        }

        private void Concat(Match<T> anotherMatch)
        {            
            if (anotherMatch == null)
                throw new ArgumentNullException("Argument anotherMatch was null.");
            if (!anotherMatch.Success || !this.Success)
                throw new ArgumentException("One of the matches was not successful.");
            if (anotherMatch.Scanner != this.Scanner)
                throw new ArgumentException("Matches are from different scanner.");

            if (anotherMatch.Empty)
                return;

            if (this.OffsetAfter != anotherMatch.Offset &&
                this.Offset != anotherMatch.OffsetAfter)
            {
                throw new ArgumentException("Matches are disjoint.");
            }
            long newFirstOffset = Math.Min(this.Offset, anotherMatch.Offset);
            long newLastOffset = Math.Max(this.OffsetAfter, anotherMatch.OffsetAfter);

            this.Offset = newFirstOffset;
            this.Length = (int)(newLastOffset - newFirstOffset);
        }

        /// <summary>
        /// Creates a new match by combining 2 other.
        /// If one of <paramref name="a"/> or <paramref name="b"/> is error,
        /// earlier error is returned. If both are succesful, return
        /// a match that covers the area of both matches.
        /// </summary>
        /// <param name="a">First match to combine.</param>
        /// <param name="b">Second match to combine.</param>
        /// <returns>Combined match</returns>
        /// <exception cref="ArgumentException">If matches are from different scanner.</exception>
        public static Match<T> operator +(Match<T> a, Match<T> b)
        {
            if (a.Scanner != b.Scanner)
            {
                throw new ArgumentException("Matches are from different scanners.");
            }

            if (!a.Success && !b.Success)
            {
                if (a.Offset < b.Offset)
                {
                    return a;
                }
                else
                {
                    return b;
                }
            }
            else if (!a.Success)
            {
                return a;
            }
            else if (!b.Success)
            {
                return b;
            }
            else
            {
                Match<T> result = a.Clone() as Match<T>;
                result.Concat(b);
                return result;
            }
        }

        /// <summary>
        /// Increases a length of succesful match by one.
        /// Return unsuccesful match as-is.
        /// </summary>
        /// <param name="a">Match to increment</param>
        /// <returns>Incramented match.</returns>
        public static Match<T> operator ++(Match<T> a)
        {
            if (a.Success)
            {
                Match<T> result = a.Clone() as Match<T>;
                result.Length++;
                return result;
            }
            else
            {
                return a;
            }
        }

        #region ICloneable Members

        /// <summary>
        /// Creates a shallow copy of this match.
        /// </summary>
        /// <returns>A copy of this object.</returns>
        public object Clone()
        {
            return new Match<T>(this);
        }

        #endregion

        #region IEquatable<Match<T>> Members

        public bool Equals(Match<T> other)
        {
            return other.Scanner == this.Scanner 
                && other.Success == this.Success
                && other.Offset == this.Offset 
                && other.Length == this.Length
                && other.error == this.error;
        }

        #endregion

        /// <summary>
        /// Returns the string representation of this match.
        /// </summary>
        /// <returns>String representation of this match</returns>
        public override string ToString()
        {
            if (Success)
            {
                return string.Format("Success at {0} size {1}", Offset, Length);
            }
            else
            {
                return string.Format("Error {0} at {1}", error, Offset);
            }
        }
    }
}
