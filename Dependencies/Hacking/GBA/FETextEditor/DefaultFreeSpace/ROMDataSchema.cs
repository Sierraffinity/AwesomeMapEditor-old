using System.Collections.Generic;
using System.Xml.Serialization;
using Nintenlord.Collections;
using Nintenlord.Utility;

namespace FETextEditor.DefaultFreeSpace
{
    partial class ROM
    {
        public static implicit operator ROM(GameSpecificData a)
        {
            ROM result = new ROM();

            result.GameCode = a.GameCode;
            result.CRC32 = a.DefaultCRC32;
            //result.ROM_space.Add(new Space("HuffmanTree"), 
                
            //    );

            return result;
        }
    }

    partial class ROM_space
    {

    }

    partial class Space : /*IEnumerable<OffsetSizePair>, IEnumerable<int>,*/  INamed<string>
    {
        public Space(string usage, IEnumerable<OffsetSizePair> offsets)
        {
            this.usageField = usage;
            this.spaceAreaField = new List<OffsetSizePair>(offsets);
        }

        public IndexOverlay GetIndexes()
        {
            var result = new IndexOverlay();
            foreach (var item in this.SpaceArea)
            {
                result.AddIndexes(item.Offset, item.Size);
            }
            return result;
        }

        //#region IEnumerable<int> Members

        //public IEnumerator<OffsetSizePair> GetEnumerator()
        //{
        //    return this.spacesField.GetEnumerator();
        //}

        //#endregion

        //#region IEnumerable Members

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return this.GetEnumerator();
        //}

        //#endregion

        //#region IEnumerable<int> Members

        //IEnumerator<int> IEnumerable<int>.GetEnumerator()
        //{
        //    foreach (var item in spaces)
        //    {
        //        for (int i = 0; i < item.Size; i++)
        //        {
        //            yield return i + item.Offset;
        //        }
        //    }
        //}

        //#endregion

        #region INamed<string> Members

        [XmlIgnore]
        public string Name
        {
            get { return this.usageField; }
        }

        #endregion
    }

    partial class OffsetSizePair
    {
        public OffsetSizePair()
        {
            this.offsetField = 0;
            this.sizeField = 0;
        }

        public OffsetSizePair(int offset, int length)
        {
            this.offsetField = offset;
            this.sizeField = length;
        }

        public static implicit operator Nintenlord.MemoryManagement.OffsetSizePair(OffsetSizePair a)
        {
            return new Nintenlord.MemoryManagement.OffsetSizePair(a.offsetField, a.sizeField);
        }

        public static implicit operator KeyValuePair<int, int>(OffsetSizePair a)
        {
            return new KeyValuePair<int, int>(a.offsetField, a.sizeField);
        }

        public static implicit operator OffsetSizePair(Nintenlord.MemoryManagement.OffsetSizePair a)
        {
            return new OffsetSizePair(a.Offset, a.Size);
        }

    }
}
