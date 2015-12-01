using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Utility;

namespace Nintenlord.Collections
{
    /// <summary>
    /// List based collection allowing fast additions and removals from start and end
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //[Obsolete("Doesn't work", true)]
    public sealed class LinkedArrayList<T> : ICollection<T>
    {
        T[] items;
        int count;
        int firstIndex;//The first reserved index
        int lastIndex;//The first free index

        public T First
        {
            get 
            {
                if (count == 0)
                {
                    throw new InvalidOperationException("Can't take the first of empty collection.");
                }
                return items[firstIndex];
            }
        }
        public T Last
        {
            get
            {
                if (count == 0)
                {
                    throw new InvalidOperationException("Can't take the last of empty collection.");
                }
                return lastIndex != 0 ? items[lastIndex - 1] : items[items.Length - 1];
            }
        }

        public T this[int index]
        {
            get
            {
                if (index >= count || index < 0)
                    throw new IndexOutOfRangeException();
                return items[InternalIndex(index)];
            }
            set
            {
                if (index >= count || index < 0)
                    throw new IndexOutOfRangeException();
                items[InternalIndex(index)] = value;
            }
        }

        public LinkedArrayList() : this(4)
        {

        }

        public LinkedArrayList(int capacity)
        {
            IntegerExtensions.ToPower2(ref capacity);
            items = new T[capacity];
            count = 0;
            firstIndex = 0;
            lastIndex = 0;
        }

        public LinkedArrayList(IEnumerable<T> collection) : this(collection.Count())
        {
            foreach (var item in collection)
            {
                AddLast(item);
            }
        }


        public void AddFirst(T item)
        {
            if (count == 0)//Empty collection
            {
                items[0] = item;
                count = 1;
                firstIndex = 0;
                lastIndex = 1;
            }
            else
            {
                if (count == items.Length)//Full collection
                    Resize(items.Length * 2);

                if (firstIndex == 0)//Reached end, looping
                    firstIndex = items.Length;

                firstIndex--;
                items[firstIndex] = item;
                count++;
            }
        }

        public void AddLast(T item)
        {
            if (count == 0)//Empty collection
            {
                items[0] = item;
                count = 1;
                firstIndex = 0;
                lastIndex = 1;
            }
            else
            {
                if (count == items.Length)//Full collection
                    Resize(items.Length * 2);

                if (lastIndex == items.Length)//Reached end, looping
                    lastIndex = 0;

                items[lastIndex] = item;
                count++;
                lastIndex++;
            }
        }

        public void RemoveFirst()
        {
            if (count > 0)
            {
                items[firstIndex] = default(T);
                firstIndex++;
                count--;

                if (firstIndex == items.Length)
                    firstIndex = 0;
            }
        }

        public void RemoveLast()
        {
            if (count > 0)
            {
                if (lastIndex == 0)
                    lastIndex = items.Length;

                lastIndex--;
                items[lastIndex] = default(T);
                count--;
            }
        }


        private int Find(T item)
        {
            if (count == 0)
            {
                return -1;
            }
            else if (firstIndex < lastIndex)
            {
                for (int i = firstIndex; i < lastIndex; i++)
                {
                    if (EqualityComparer<T>.Default.Equals(items[i], item))
                    {
                        return i;
                    }
                }
                return -1;
            }
            else
            {
                for (int i = firstIndex; i < items.Length; i++)
                {
                    if (EqualityComparer<T>.Default.Equals(items[i], item))
                    {
                        return i;
                    }
                }
                for (int i = 0; i < lastIndex; i++)
                {
                    if (EqualityComparer<T>.Default.Equals(items[i], item))
                    {
                        return i;
                    }
                }
                return -1;
            }
        }

        private void RemoveAt(int internalIndex)
        {
            if (count > 0)
            {
                if (IsProperInternalIndex(internalIndex))
                {
                    if (firstIndex < lastIndex)
                    {                        
                        for (int i = internalIndex; i < lastIndex; i++)
                        {
                            items[i] = items[i + 1];
                        }
                        lastIndex--;
                        items[lastIndex] = default(T);
                    }
                    else
                    {
                        if (internalIndex < firstIndex)
                        {
                            for (int i = internalIndex; i < lastIndex; i++)
                            {
                                items[i] = items[i + 1];
                            }
                            lastIndex--;
                            items[lastIndex] = default(T);
                        }
                        else
                        {
                            for (int i = internalIndex; i >= firstIndex; i--)
                            {
                                items[i] = items[i - 1];
                            }
                            items[firstIndex] = default(T);
                            firstIndex++;
                        }
                    }
                    count--;
                }
            }
        }

        private void Resize(int newLength)
        {
            T[] newItems = new T[newLength];
            if (count > 0)
            {
                if (firstIndex < lastIndex) //Items do not loop
                {
                    Array.Copy(items, firstIndex, newItems, 0, lastIndex - firstIndex);
                }
                else
                {
                    int interMediateIndex = items.Length;
                    Array.Copy(items, firstIndex, newItems, 0, items.Length - firstIndex);
                    Array.Copy(items, 0, newItems, items.Length - firstIndex, lastIndex);
                }
                //Since both branches copy data to the beginning
                firstIndex = 0;
                lastIndex = count;
            }
            items = newItems;
        }
        
        private int InternalIndex(int index)
        {
            if (index + firstIndex < items.Length)
            {
                return index + firstIndex;
            }
            else
            {
                return index + firstIndex - count;
            }
        }

        private bool IsProperInternalIndex(int index)
        {
            if (firstIndex < lastIndex)
            {
                return index.IsInRangeHO(firstIndex, lastIndex);
            }
            else if (firstIndex > lastIndex)
            {
                return index.IsInRangeHO(firstIndex, items.Length) 
                    || index.IsInRangeHO(0, lastIndex);
            }
            else return false;
        }

        private IEnumerator<T> InternalGetEnumerator(int start, int end)
        {
            if (start < end)
            {
                for (int i = start; i < end; i++)
                {
                    yield return items[i];
                }
            }
            else
            {
                for (int i = start; i < items.Length; i++)
                {
                    yield return items[i];
                }
                for (int i = 0; i < end; i++)
                {
                    yield return items[i];
                }
            }
        }
        
        #region ICollection<T> Members

        public void Add(T item)
        {
            AddLast(item);
        }

        public void Clear()
        {
            count = 0;
            firstIndex = 0;
            lastIndex = 0;
            Array.Clear(items, 0, items.Length);
        }

        public bool Contains(T item)
        {
            return Find(item) >= 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (var item in this)
            {
                array[arrayIndex] = item;
                arrayIndex++;
            }
        }

        public int Count
        {
            get { return count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            int index = Find(item);
            if (index >= 0)
                RemoveAt(index);
            return index >= 0;
        }
        
        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return InternalGetEnumerator(firstIndex, lastIndex);
        }

        public IEnumerator<T> GetEnumerator(int start, int length)
        {
            return InternalGetEnumerator(
                InternalIndex(start), 
                InternalIndex(length));
        }
        
        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
