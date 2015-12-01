// -----------------------------------------------------------------------
// <copyright file="Modulus.cs" company="">
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
    public class Modulus<T> : BinaryOperator<T>
    {
        public Modulus(IExpression<T> first, IExpression<T> second, FilePosition position)
            : base(first, second, EAExpressionType.Modulus, position)
        {

        }
    }
}
