using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Nintenlord.IO.Scanners;
using Nintenlord.IO;
using Nintenlord.Collections;
using Nintenlord.Event_Assembler.Core.Code.Preprocessors;
using Nintenlord.Event_Assembler.Core.IO.Input;

namespace Nintenlord.Event_Assembler.Core.Code.Language.Lexer
{
    class TokenScanner : IScanner<Token>
    {
        List<Token> readTokens;
        IPositionableInputStream input;
        int tokenOffset;

        public TokenScanner(IPositionableInputStream input)
        {
            this.input = input;
            readTokens = new List<Token>(0x1000);
            tokenOffset = -1;
            IsAtEnd = false;
        }

        #region IScanner<Token> Members

        public bool IsAtEnd
        {
            get;
            private set;
        }

        public long Offset
        {
            get
            {
                return tokenOffset;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public bool MoveNext()
        {
            if (IsAtEnd)
                return false;

            string line;
            tokenOffset++;
            while (tokenOffset >= readTokens.Count)
            {
                line = input.ReadLine();
                if (line == null)
                {
                    break;
                }
                readTokens.AddRange(Tokeniser.TokeniseLine(
                    line, input.CurrentFile, input.LineNumber));
            }
            
            IsAtEnd = tokenOffset >= readTokens.Count;

            return !IsAtEnd;
        }

        public Token Current
        {
            get
            {
                if (tokenOffset > readTokens.Count)
                {
                    throw new InvalidOperationException("End of tokens to read.");
                }
                else if (tokenOffset == readTokens.Count)
                {
                    return new Token();
                }
                return readTokens[tokenOffset];
            }
        }
        
        public bool CanSeek
        {
            get { return false; }
        }
        
        #endregion
    }
}
