using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.IO;

namespace Nintenlord.Event_Assembler.Core.Code.Language.Lexer
{
    public struct Token 
    {
        FilePosition position;
        TokenType type;
        string value;

        public bool HasValue
        {
            get
            {
                return value != null;
            }
        }
        public TokenType Type
        {
            get { return type; }
        }
        public string Value
        {
            get 
            {
                if (value != null)
                {
                    return value;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }            
        }
        public FilePosition Position
        {
            get { return position; }
        }

        private Token(FilePosition position, TokenType type)
            : this(position, type, null)
        {

        }

        public Token(FilePosition position, TokenType type, string value)
        {
            this.position = position;
            this.type = type;
            this.value = value;
        }

        public override string ToString()
        {
            return this.type.ToString() + "(" + this.value + ")";
        }
    }
}
