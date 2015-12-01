using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.Collections
{
    public interface IContainer<T>
    {
        int Count { get; }
        bool IsReadOnly { get; }
        void Add(T item);
        bool Contains(T item);
        bool Remove(T item);
        void Clear();
    }
}
