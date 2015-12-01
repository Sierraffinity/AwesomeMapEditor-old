using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Nintenlord.ROMHacking
{
    public abstract class AbstractROM
    {
        private bool edited;
        private int maxLength;
        private string path;
        protected byte[] ROMdata;
        public bool Edited
        {
            get { return edited; }
            protected set { edited = value; }
        }
        public int Length
        {
            get
            {
                if (ROMdata == null)
                    return 0;
                else
                    return ROMdata.Length; 
            }
            set { ChangeROMSize(value); }
        }
        public bool Opened
        {
            get { return ROMdata != null && path != null; }
        }
        public string ROMPath
        {
            get { return path; }
            protected set { path = value; }
        }
        protected int MaxLength
        {
            get { return maxLength; }
        }

        public AbstractROM(int maxLenght)
        {
            this.maxLength = maxLenght;
        }

        public abstract void OpenROM(string path);
        public abstract void CloseROM();
        public void SaveROM()
        {
            SaveROM(path);
        }
        public abstract void SaveROM(string path);
        public abstract void SaveBackup();

        public void InsertData(int offset, int value)
        {
            InsertData(offset, BitConverter.GetBytes(value));
        }
        public void InsertData(int offset, short value)
        {
            InsertData(offset, BitConverter.GetBytes(value));
        }
        public void InsertData(int offset, byte value)
        {
            InsertData(offset, BitConverter.GetBytes(value));
        }
        public void InsertData(int offset, byte[] data)
        {
            InsertData(offset, data, 0, data.Length);
        }
        public void InsertData(int offset, byte[] data, int index)
        {
            InsertData(offset, data, index, data.Length);
        }
        public void InsertData(int offset, byte[] data, int index, int length)
        {
            edited = true;
            if (length == 0 || index + length > data.Length)
                length = data.Length - index;
            if (offset + length > ROMdata.Length)
            {
                ChangeROMSize(offset + length);
                length = this.ROMdata.Length - offset;
            }
            Array.Copy(data, index, ROMdata, offset, length);
        }

        private void ChangeROMSize(int newSize)
        {
            if (newSize > MaxLength)
                newSize = MaxLength;
            Array.Resize(ref ROMdata, newSize);
        }

        public byte[] GetData(int offset, int length)
        {
            if (length == 0 || offset + length > ROMdata.Length)
                length = ROMdata.Length - offset;
            byte[] data = new byte[length];
            Array.Copy(ROMdata, offset, data, 0, length);
            return data;
        }

        public int[] SearchForValue(int value)
        {
            return SearchForValue(BitConverter.GetBytes(value));
        }
        public int[] SearchForValue(int value, int offset, int area)
        {
            return SearchForValue(BitConverter.GetBytes(value), offset, area);
        }
        public int[] SearchForValue(short value)
        {
            return SearchForValue(BitConverter.GetBytes(value));
        }
        public int[] SearchForValue(short value, int offset, int area)
        {
            return SearchForValue(BitConverter.GetBytes(value), offset, area);
        }
        public int[] SearchForValue(byte[] value)
        {
            return SearchForValue(value, 0, ROMdata.Length - value.Length); 
        }
        public int[] SearchForValue(byte[] value, int offset, int area)
        {
            List<int> offsets = new List<int>();
            int index = offset;
            int maxIndex = area + offset;
            while (index < maxIndex)
            {
                int foundIndex = 0;
                while (index + foundIndex < ROMdata.Length
                    && foundIndex < value.Length
                    && ROMdata[index + foundIndex] == value[foundIndex])
                {
                    foundIndex++;
                }
                if (foundIndex == value.Length)
                    offsets.Add(index);

                index++;
            }
            return offsets.ToArray();
        }
    }
}
