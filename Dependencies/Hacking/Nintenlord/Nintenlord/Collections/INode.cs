using System;

namespace Nintenlord.Collections
{
    interface INode<T>
    {
        INode<T> Parent { get; }
        INode<T>[] Children { get; }
        INode<T>[] Descendants { get; }
        ITree<T> Tree { get; }
        T Value { get; }

        int GetDepth();
        void ForEachDescendant(Action<T> action);
    }
}
