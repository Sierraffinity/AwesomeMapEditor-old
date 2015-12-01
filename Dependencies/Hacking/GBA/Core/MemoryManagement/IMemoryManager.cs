using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.MemoryManagement;

namespace Nintenlord.Feditor.Core.MemoryManagement
{
    public interface IMemoryManager : IAllocator<ManagedPointer>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <returns>A pinned pointer pointing to offset with size if succesful,
        /// else a null pointer</returns>
        ManagedPointer Reserve(int offset, int size);
        void Pin(ManagedPointer ptr);
        void Unpin(ManagedPointer ptr);
    }
}
