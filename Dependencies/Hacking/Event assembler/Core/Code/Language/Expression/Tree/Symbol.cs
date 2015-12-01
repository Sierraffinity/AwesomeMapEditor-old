using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Utility;
using Nintenlord.IO;

namespace Nintenlord.Event_Assembler.Core.Code.Language.Expression
{
    sealed class Symbol<T> : IExpression<T>, INamed<string>
    {
        readonly string name;
        readonly FilePosition position;

        public string Name
        {
            get { return name; }
        }
        public FilePosition Position
        {
            get { return position; }
        }

        public Symbol(string name, FilePosition position)
        {
            this.name = name;
            this.position = position;
        }

        #region IExpression<T> Members

        public EAExpressionType Type
        {
            get { return EAExpressionType.Symbol; }
        }

        public bool CanEval()
        {
            return false;
        }

        #endregion

        #region ITree<IExpression<T>> Members

        public IEnumerable<IExpression<T>> GetChildren()
        {
            yield break;
        }

        #endregion

        public override string ToString()
        {
            return name;
        }
    }
}
