using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Collections;
using Nintenlord.IO;

namespace Nintenlord.Event_Assembler.Core.Code.Language.Expression
{
    class Code<T> : IExpression<T>
    {
        readonly Symbol<T> codeName;
        readonly Parameter<T>[] parameters;
        readonly FilePosition position;


        public Symbol<T> CodeName
        {
            get { return codeName; }
        }
        public Parameter<T>[] Parameters
        {
            get { return parameters; }
        }
        public Parameter<T> this[int index]
        {
            get { return parameters[index]; }
        }
        public FilePosition Position
        {
            get { return position; }
        } 

        public int ParameterCount { get { return parameters.Length; } }

        public Code(FilePosition position, Symbol<T> codeName, List<Parameter<T>> parameters)
        {
            this.codeName = codeName;
            this.parameters = parameters.ToArray();
            this.position = position;
        }

        #region IExpression<T> Members

        public EAExpressionType Type
        {
            get { return EAExpressionType.Code; }
        }

        public bool CanEval()
        {
            return false;
        }

        public IEnumerable<IExpression<T>> GetChildren()
        {
            yield return codeName;
            foreach (var item in parameters)
            {
                yield return item;
            }
        }

        #endregion

        public override string ToString()
        {
            return codeName.ToString() + parameters.ToElementWiseString(" ", " ", "");
        }


    }
}
