using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Nintenlord.ROMHacking
{
    //Inherit from stream?
    public abstract class AbstractROM : IROM
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
        public abstract void OpenROM(Stream stream);
        public abstract void CloseROM();
        public void SaveROM()
        {
            SaveROM(path);
        }
        public abstract void SaveROM(string path);
        public abstract void SaveROM(Stream stream);
        public abstract void SaveBackup();

        public void InsertData(int offset, int value)
        {
            InsertData(offset, BitConverter.GetBytes(value));
        }
        public void InsertData(int offset, uint value)
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

        public void Move(int odlOffset, int newOffset, int length)
        {
            if (Math.Abs(odlOffset - newOffset) > length)
            {
                Array.Copy(ROMdata, odlOffset, ROMdata, newOffset, length);
            }
            else
            {
                byte[] temp = new byte[length];
                Array.Copy(ROMdata, odlOffset, temp, 0, length);
                Array.Copy(temp, 0, ROMdata, newOffset, length);
            }
        }

        private void ChangeROMSize(int newSize)
        {
            if (newSize > MaxLength)
                throw new ArgumentOutOfRangeException("New size is larger than allowed.");
            Array.Resize(ref ROMdata, newSize);
        }

        public byte[] GetData(int offset, int length)
        {
            if (length == 0)
                return new byte[0];
            else if (offset + length > ROMdata.Length)
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
        
        private class ROMStream : Stream
        {
            AbstractROM ROM;
            long position;

            public ROMStream(AbstractROM ROM)
            {
                this.ROM = ROM;
                this.position = 0;
            }

            public override bool CanRead
            {
                get { return true; }
            }

            public override bool CanSeek
            {
                get { return true; }
            }

            public override bool CanWrite
            {
                get { return true; }
            }

            public override void Flush()
            {

            }

            public override long Length
            {
                get { return ROM.Length; }
            }

            public override long Position
            {
                get
                {
                    return position;
                }
                set
                {
                    position = value;
                }
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                long amountToCopy = Math.Min(ROM.Length - position, count);
                Array.Copy(this.ROM.ROMdata, position, buffer, offset, amountToCopy);

                return (int)amountToCopy;
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                switch (origin)
                {
                    case SeekOrigin.Begin:
                        position = offset;
                        break;
                    case SeekOrigin.Current:
                        position += offset;
                        break;
                    case SeekOrigin.End:
                        position = ROM.Length - offset;
                        break;
                    default:
                        break;
                }
                return offset;
            }

            public override void SetLength(long value)
            {
                ROM.ChangeROMSize((int)value);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                ROM.InsertData((int)position, buffer, offset, count);
                position += count;
            }

            public override void Close()
            {
                ROM = null;
                position = 0;
                base.Close();
            }
        }

        public Stream GetStream()
        {
            return new MemoryStream(this.ROMdata);
            //return new ROMStream(this);
        }
    }
}
