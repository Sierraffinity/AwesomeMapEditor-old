using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Feditor.Core.MemoryManagement
{
    public class Lazy : IMemoryManager
    {
        #region IMemoryManager Members

        public ManagedPointer Reserve(int offset, int size)
        {
            return new ManagedPointer(offset, size, true);
        }

        public void Pin(ManagedPointer ptr)
        {

        }

        public void Unpin(ManagedPointer ptr)
        {

        }

        #endregion

        #region IAllocator<ManagedPointer> Members

        public ManagedPointer Allocate(int size)
        {
            return ManagedPointer.NullPointer;
        }

        public ManagedPointer Allocate(int size, int padding)
        {
            return ManagedPointer.NullPointer;
        }

        public void Deallocate(ManagedPointer pointer)
        {

        }

        public bool IsAllocated(ManagedPointer pointer)
        {
            return false;
        }

        #endregion
    }
}
