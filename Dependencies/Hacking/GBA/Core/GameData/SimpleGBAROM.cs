using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.ROMHacking.GBA;

namespace Nintenlord.Feditor.Core.GameData
{
    public class SimpleGBAROM : GBAROM, IROM
    {
        #region IROM Members

        public void WriteData(MemoryManagement.ManagedPointer ptr, byte[] data, int index, int length)
        {
            if (length > ptr.Size)
            {
                throw new IndexOutOfRangeException();
            }
            base.InsertData(ptr.Offset, data, index, length);
        }

        public byte[] ReadData(int offset, int length)
        {
            return base.GetData(offset, length);
        }

        public byte[] ReadData(MemoryManagement.ManagedPointer ptr)
        {
            return base.GetData(ptr.Offset, ptr.Size);
        }

        #endregion

        #region IROM Members


        public void AddCustomData(string name, string data)
        {
            throw new NotImplementedException();
        }

        public string GetCustomData(string name)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
