// -----------------------------------------------------------------------
// <copyright file="Folding.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Event_Assembler.Core.Code.Language.Expression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nintenlord.Utility;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    static public class Folding
    {
        static public int Fold(IExpression<int> expression)
        {
            BinaryOperator<int> op = expression as BinaryOperator<int>;
            switch (expression.Type)
            {
                case EAExpressionType.Value:
                    return ((ValueExpression<int>)expression).Value;
                case EAExpressionType.Division:
                    return Fold(op.First) / Fold(op.Second);
                case EAExpressionType.Multiply:
                    return Fold(op.First) * Fold(op.Second);
                case EAExpressionType.Modulus:
                    return Fold(op.First) % Fold(op.Second);
                case EAExpressionType.Minus:
                    return Fold(op.First) - Fold(op.Second);
                case EAExpressionType.Sum:
                    return Fold(op.First) + Fold(op.Second);
                case EAExpressionType.XOR:
                    return Fold(op.First) ^ Fold(op.Second);
                case EAExpressionType.AND:
                    return Fold(op.First) & Fold(op.Second);
                case EAExpressionType.OR:
                    return Fold(op.First) | Fold(op.Second);
                case EAExpressionType.LeftShift:
                    return Fold(op.First) << Fold(op.Second);
                case EAExpressionType.RightShift:
                    return Fold(op.First) >> Fold(op.Second);
                default:
                    throw new ArgumentException();
            }
        }

        static public CanCauseError<int> TryFold(IExpression<int> expression)
        {
            BinaryOperator<int> op = expression as BinaryOperator<int>;
            Func<int, int, int> func;
            switch (expression.Type)
            {
                case EAExpressionType.Value:
                    return ((ValueExpression<int>)expression).Value;
                case EAExpressionType.Division:
                    func = (x, y) => x / y;
                    break;
                case EAExpressionType.Multiply:
                    func = (x, y) => x * y;
                    break;
                case EAExpressionType.Modulus:
                    func = (x, y) => x % y;
                    break;
                case EAExpressionType.Minus:
                    func = (x, y) => x - y;
                    break;
                case EAExpressionType.Sum:
                    func = (x, y) => x + y;
                    break;
                case EAExpressionType.XOR:
                    func = (x, y) => x ^ y;
                    break;
                case EAExpressionType.AND:
                    func = (x, y) => x & y;
                    break;
                case EAExpressionType.OR:
                    func = (x, y) => x | y;
                    break;
                case EAExpressionType.LeftShift:
                    func = (x, y) => x << y;
                    break;
                case EAExpressionType.RightShift:
                    func = (x, y) => x >> y;
                    break;
                default:
                    return CanCauseError<int>.Error("Unsupported type: {0}", expression.Type);
            }
            return func.Map(Fold(op.First), Fold(op.Second));
        }

        static public CanCauseError<int> Fold(IExpression<int> expression, Func<string, int?> symbolVals)
        {
            BinaryOperator<int> op = expression as BinaryOperator<int>;
            Func<int, int, int> func;
            switch (expression.Type)
            {
                case EAExpressionType.Value:
                    return ((ValueExpression<int>)expression).Value;
                case EAExpressionType.Symbol:
                    string name = ((Symbol<int>)expression).Name;
                    int? val = symbolVals(name);
                    return val != null ? val 
                        : CanCauseError<int>.Error("Symbol {0} isn't in scope", name);
                case EAExpressionType.Division:
                    func = (x, y) => x / y;
                    break;
                case EAExpressionType.Multiply:
                    func = (x, y) => x * y;
                    break;
                case EAExpressionType.Modulus:
                    func = (x, y) => x % y;
                    break;
                case EAExpressionType.Minus:
                    func = (x, y) => x - y;
                    break;
                case EAExpressionType.Sum:
                    func = (x, y) => x + y;
                    break;
                case EAExpressionType.XOR:
                    func = (x, y) => x ^ y;
                    break;
                case EAExpressionType.AND:
                    func = (x, y) => x & y;
                    break;
                case EAExpressionType.OR:
                    func = (x, y) => x | y;
                    break;
                case EAExpressionType.LeftShift:
                    func = (x, y) => x << y;
                    break;
                case EAExpressionType.RightShift:
                    func = (x, y) => x >> y;
                    break;
                default:
                    return CanCauseError<int>.Error("Unsupported type: {0}", expression.Type);
            }
            return func.Map(Fold(op.First), Fold(op.Second));
        }
    }
}
