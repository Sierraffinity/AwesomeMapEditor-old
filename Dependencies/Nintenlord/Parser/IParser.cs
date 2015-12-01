using System;
using Nintenlord.IO.Scanners;

namespace Nintenlord.Parser
{
    public interface IParser<TIn, out TOut>
    {
        TOut Parse(IScanner<TIn> scanner, out Match<TIn> match);
    }
}
