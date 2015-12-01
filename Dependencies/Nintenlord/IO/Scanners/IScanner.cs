using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Nintenlord.IO.Scanners
{
    /// <summary>
    /// Scans tokens
    /// </summary>
    /// <typeparam name="T">Type of tokens to scan</typeparam>
    public interface IScanner<out T>
    {
        /// <summary>
        /// True if scanner has run out of tokens and can't Peek or Advance anymore, else false.
        /// </summary>
        bool IsAtEnd { get; }

        /// <summary>
        /// Gets or sets the current offset. Setting is only allowed if CanSeek == True.
        /// </summary>
        long Offset { get; set; }
        
        /// <summary>
        /// True if offset can be set, else false.
        /// </summary>
        bool CanSeek { get; }

        T Current { get; }
        bool MoveNext();
    }
}
