// -----------------------------------------------------------------------
// <copyright file="BitShiftLeft.cs" company="">
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
    public class BitShiftLeft<T> : BinaryOperator<T>
    {
        public BitShiftLeft(IExpression<T> first, IExpression<T> second, FilePosition position)
            : base(first, second, EAExpressionType.LeftShift, position)
        {

        }
    }
}
