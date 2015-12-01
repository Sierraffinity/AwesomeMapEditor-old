using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Collections;
using Nintenlord.IO;

namespace Nintenlord.Event_Assembler.Core.Code.Language.Expression
{
    public sealed class Parameter<T> : IExpression<T>//, IEnumerable<IExpression<T>>
    {
        readonly private List<IExpression<T>> components;
        readonly private IExpression<T> only;
        readonly private FilePosition position;

        public IExpression<T> this[int index]
        {
            get             
            {
                return components[index];
            }
        }

        public Parameter(IExpression<T> parameter, FilePosition position)
        {
            only = parameter;
            this.position = position;
        }

        public Parameter(List<IExpression<T>> parameters, FilePosition position)
        {
            components = parameters;
            this.position = position;
        }
        
        public IExpression<T> Only { get { return only; } }
        public bool IsVector { get { return components != null; } }
        public int CompCount { get { return components.Count; } }

        #region IExpression<T> Members

        public EAExpressionType Type
        {
            get { return EAExpressionType.Parameter; }
        }

        public bool CanEval()
        {
            return false;
        }

        public IEnumerable<IExpression<T>> GetChildren()
        {
            if (this.IsVector)
            {
                return components.AsEnumerable();                
            }
            else
            {
                return only.GetArray();
            }
        }

        public FilePosition Position
        {
            get { return position; }
        } 

        #endregion

        public override string ToString()
        {
            if (IsVector)
            {
                return components.ToElementWiseString(", ", "[", "]");
            }
            else
            {
                return only.ToString();
            }
        }
    }
}
