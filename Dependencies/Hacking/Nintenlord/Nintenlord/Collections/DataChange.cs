using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;

namespace Nintenlord.Event_assembler.Collections
{
    /// <summary>
    /// Collection to keep track of changes to a array of data
    /// </summary>
    /// <typeparam name="T">Type whose array is to be changed</typeparam>
    class DataChange<T> : IDataChange<T>
    {
        SortedList<int, T[]> dataToChange;

        public int LastOffset
        {
            get
            {
                int lastKey = dataToChange.Keys[dataToChange.Count-1];
                return lastKey + dataToChange[lastKey].Length;
            }
        }

        public int FirstOffset
        {
            get
            {
                int firstKey = dataToChange.Keys[0];
                return firstKey;
            }

        }

        public bool ChangesAnything
        {
            get
            {
                return dataToChange.Count != 0;
            }
        }

        /// <summary>
        /// Creates a new DataChange.
        /// </summary>
        public DataChange()
        {
            dataToChange = new SortedList<int, T[]>();
        }

        public void AddChangedData(int offset, T[] data)
        {
            if (offset < 0)
                throw new IndexOutOfRangeException("Negative offset was passed.");
            if (data == null || data.Length == 0)
                return;

            int endOffset = offset + data.Length;

            int[] temp = new int[dataToChange.Count];
            dataToChange.Keys.CopyTo(temp, 0);
            IList<int> intersectedKeys = new List<int>(temp);
            int index = 0;

            while (index < intersectedKeys.Count && intersectedKeys[index] < endOffset )
            {
                index++;
            }
            while (intersectedKeys.Count > index)
            {
                intersectedKeys.RemoveAt(index);//removes offsets after
            }

            while (0 < intersectedKeys.Count && intersectedKeys[0] + dataToChange[intersectedKeys[0]].Length < offset)
            {
                intersectedKeys.RemoveAt(0);//removes offsets before
            }

            if (intersectedKeys.Count > 0)
            {
                int newOffset = Math.Min(intersectedKeys[0], offset);
                int lastKey = intersectedKeys[intersectedKeys.Count - 1];
                int newLastOffset = Math.Max(lastKey + dataToChange[lastKey].Length, endOffset);

                T[] newData = new T[newLastOffset - newOffset];
                foreach (int item in intersectedKeys)
                {
                    T[] oldData = dataToChange[item];
                    Array.Copy(oldData, 0, newData, item - newOffset, oldData.Length);
                    dataToChange.Remove(item);
                }

                Array.Copy(data, 0, newData, offset - newOffset, data.Length);
                dataToChange.Add(newOffset, newData);
            }
            else
            {
                dataToChange.Add(offset, data);
            }
        }

        public T[] Apply(T[] data)
        {
            if (ChangesAnything)
            {
                int maxOffset = Math.Max(this.LastOffset, data.Length);
                T[] newData = new T[maxOffset];
                Array.Copy(data, newData, data.Length);
                ApplyTo(newData);

                return newData;                
            }
            else
            {
                return data;
            }
        }

        private void ApplyTo(T[] data)
        {
            foreach (KeyValuePair<int, T[]> item in dataToChange)
            {
                Array.Copy(item.Value, 0, data, item.Key, item.Value.Length);
            }
        }

        /// <summary>
        /// Returns The string representation of this instance.
        /// </summary>
        /// <returns>The string representation of this instance.</returns>
        public override string ToString()
        {
            string text = "";
            foreach (KeyValuePair<int, T[]> item in dataToChange)
            {
                text += item.Key + ": ";
                foreach (T item2 in item.Value)
                {
                    text += item2 + " ";
                }
                text += "\n";
            }
            return text;
        }
    }
}
//Test code:
/*
            byte[] data = new byte[100];
            DataChange<byte> changedData = new DataChange<byte>();
            Random rand = new Random(DateTime.Now.Millisecond);
            rand.NextBytes(data);
            for (int i = 0; i < 10; i++)
            {
                int offset = rand.Next(data.Length);
                int lenght = rand.Next(data.Length / 10);
                byte[] dataToChange = new byte[lenght];
                rand.NextBytes(dataToChange);
                changedData.AddChangedData(offset, dataToChange);
            }
            changedData.AddChangedData(data.Length, new byte[] { 0, 0, 0, 0, 0 });
            byte[] newData = changedData.Apply(data);

            StreamWriter sw = new StreamWriter(@"C:\Users\Timo\Desktop\text.txt");
            sw.WriteLine("Original data:");
            foreach (byte item in data)
            {
                sw.Write(item + " ");
            }
            sw.WriteLine();

            sw.WriteLine("Changed data:");
            sw.WriteLine(changedData);
            sw.WriteLine("Result:");
            foreach (byte item in newData)
            {
                sw.Write(item + " ");
            }
            sw.WriteLine();
            sw.WriteLine();
            sw.Close();
            MessageBox.Show("Finished.");
 */