using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Utility;
using Nintenlord.MemoryManagement;
using Nintenlord.Collections;

namespace Nintenlord.ROMHacking.GBA.MemoryManagement
{
    public sealed class GBAMemoryManager : IFreeMemoryManager<GBAPointer>
    {
        SortedSet<GBAPointer> managedPointers;
        IIndexOverlay managedSpace;

        public GBAMemoryManager()
        {
            managedPointers = new SortedSet<GBAPointer>();
            managedSpace = new IndexOverlay();
        }

        private int FindSpace(int size, int padding)
        {
            foreach (KeyValuePair<int, int> item in (IEnumerable<KeyValuePair<int, int>>)managedSpace)
            {
                if (item.Key > managedPointers.Max.OffsetAfter)
                {
                    int padOffset = item.Key.ToMod(padding);
                    int padLoss = padOffset - item.Key;
                    int padSize = item.Value - padLoss;
                    if (padSize >= size)
                    {
                        return padOffset;
                    }
                }
            }
            return -1;
        }
                
        #region IMemoryManager<GBAPointer> Members

        public GBAPointer Allocate(int size)
        {
            return Allocate(size, 1);
        }

        public GBAPointer Allocate(int size, int padding)
        {
            if (!managedSpace.ContainsIndexes)
            {
                return GBAPointer.NullPointer;
            }

            int offset = managedPointers.Max.OffsetAfter.ToMod(padding);
            if (managedSpace.ContainsAllIndexes(offset, size))
            {
                var newPtr = new GBAPointer(this, offset, size);
                managedPointers.Add(newPtr);
                return newPtr;
            }
            else
            {
                offset = FindSpace(size, padding);
                if (offset != -1)
                {
                    var newPtr = new GBAPointer(this, offset, size);
                    managedPointers.Add(newPtr);
                    return newPtr;
                }
                else
                {
                    return GBAPointer.NullPointer;
                }
            }
        }

        public void Deallocate(GBAPointer pointer)
        {
            if (pointer.IsNull)
            {
                throw new ArgumentException("Null pointer");
            }
            if (!managedPointers.Remove(pointer))
            {
                throw new ArgumentException("Memory not allocated");
            }
        }

        public bool IsAllocated(GBAPointer pointer)
        {
            if (pointer.IsNull)
            {
                return false;
            }
            else
            {
                return managedPointers.Contains(pointer);
            }
        }


        public void AddManagedSpace(int offset, int size)
        {
            AddManagedSpace(offset, size, false);
        }

        public GBAPointer AddManagedSpace(int offset, int size, bool used)
        {
            managedSpace.AddIndexes(offset, size);
          
            if (used)
            {
                var ptr = new GBAPointer(this, offset, size);
                managedPointers.Add(ptr);
                return ptr;
            }
            else
            {
                return GBAPointer.NullPointer;
            }
        }

        public void RemoveManagedSpace(int offset, int size)
        {
            //Check if any memory is allocated?
            managedSpace.RemoveIndexes(offset, size);
        }

        public bool IsManaged(int offset, int size)
        {
            return managedSpace.ContainsAllIndexes(offset, size);
        }
        
        #endregion
        
        #region IEnumerable<GBAPointer> Members

        public IEnumerator<GBAPointer> GetEnumerator()
        {
            return managedPointers.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion


        //Packs data tightly
        public void CleanUp(GBAROM rom)
        {
            int newFreeOffset = managedSpace.FirstIndex;

            foreach (var pointerToMove in this)
            {
                if (pointerToMove.Offset != newFreeOffset)
                {
                    while (!managedSpace.ContainsAllIndexes(newFreeOffset, pointerToMove.Size))
                    {
                        newFreeOffset = managedSpace.First<int>(x => (x > newFreeOffset));
                        //Won't throw an exception, since memory already fits
                    }
                    rom.Move(pointerToMove.Offset, newFreeOffset, pointerToMove.Size);
                    pointerToMove.Offset = newFreeOffset;
                    newFreeOffset = pointerToMove.OffsetAfter;
                }
            }
            SortedSet<GBAPointer> second = new SortedSet<GBAPointer>(managedPointers);
            managedPointers.Clear();
            managedPointers = second;
        }
        
        public IEnumerable<GBAPointer> GetFreeSpace()
        {
            int previous = managedSpace.FirstIndex;
            foreach (var item in managedPointers)
            {
                if (item.Offset != previous)
                {
                    yield return new GBAPointer(null, previous, item.Offset - previous);
                }
                previous = item.OffsetAfter;
            }
        }
    }
}