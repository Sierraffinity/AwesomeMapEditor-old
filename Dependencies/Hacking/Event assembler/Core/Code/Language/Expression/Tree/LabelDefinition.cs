// -----------------------------------------------------------------------
// <copyright file="LabelDefinition.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Event_Assembler.Core.Code.Language.Expression.Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nintenlord.IO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    sealed class LabelDefinition<T> : IExpression<T>
    {
        readonly string labelName;
        readonly FilePosition position;

        public string LabelName
        {
            get { return labelName; }
        }

        public LabelDefinition(FilePosition position, string labelName)
        {
            this.position = position;
            this.labelName = labelName;
        }

        #region IExpression<T> Members

        public EAExpressionType Type
        {
            get { return EAExpressionType.Label; }
        }

        public Nintenlord.IO.FilePosition Position
        {
            get { return position; }
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
    }
}
