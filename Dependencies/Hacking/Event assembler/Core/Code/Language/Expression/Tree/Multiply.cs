// -----------------------------------------------------------------------
// <copyright file="Multiply.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Event_Assembler.Core.Code.Language.Expression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nintenlord.IO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class Multiply<T> : BinaryOperator<T>
    {
        public Multiply(IExpression<T> first, IExpression<T> second, FilePosition position)
            : base(first, second, EAExpressionType.Multiply, position)
        {

        }
    }
}
