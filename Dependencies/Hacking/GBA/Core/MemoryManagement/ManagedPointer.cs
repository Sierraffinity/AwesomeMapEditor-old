using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.MemoryManagement;

namespace Nintenlord.Feditor.Core.MemoryManagement
{
    public sealed class ManagedPointer : IComparable<ManagedPointer>, IEquatable<ManagedPointer>, IMemoryPointer
    {
        int offset;
        int size;
        bool pinned;

        public int Offset
        {
            get { return offset; }
            internal set
            {
                if (!pinned)
                {
                    throw new NotSupportedException("Pointers referance can't be moved.");
                }

                offset = value;
            }
        }
        public int Size
        {
            get { return size; }
        }
        public bool Pinned { get { return pinned; } }
        public int OffsetAfter { get { return offset + size; } }
        public bool IsNull { get { return offset == -1; } }

        static public readonly ManagedPointer NullPointer = new ManagedPointer();

        private ManagedPointer()
        {
            offset = -1;
            size = 0;
            pinned = false;
        }

        internal ManagedPointer(int offset, int size, bool pinned)
        {
            if (size < 0)
            {
                throw new ArgumentException("Size is negative.");
            }
            this.offset = offset;
            this.size = size;
            this.pinned = pinned;
        }

        private ManagedPointer(ManagedPointer copyMe)
        {
            this.offset = copyMe.offset;
            this.size = copyMe.size;
            this.pinned = copyMe.pinned;
        }

        #region IComparable<ManagedPointer> Members

        public int CompareTo(ManagedPointer other)
        {
            return this.offset - other.offset;
        }

        #endregion

        #region IEquatable<ManagedPointer> Members

        public bool Equals(ManagedPointer other)
        {
            return this.size == other.size &&
                this.offset == other.offset;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is ManagedPointer)
            {
                return this.Equals((ManagedPointer)obj);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return offset;
        }

        public override string ToString()
        {
            return string.Format("{{Offset: ${0} Size: 0x{1}}}", offset.ToString("X6"), size.ToString("X"));
        }
    }
}
