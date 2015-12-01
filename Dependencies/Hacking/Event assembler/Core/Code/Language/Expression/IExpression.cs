using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Collections.Trees;
using Nintenlord.IO;

namespace Nintenlord.Event_Assembler.Core.Code.Language.Expression
{
    public interface IExpression<out T> : ITree<IExpression<T>>
    {
        EAExpressionType Type { get; }
        FilePosition Position { get; } 
        bool CanEval();
    }
}
