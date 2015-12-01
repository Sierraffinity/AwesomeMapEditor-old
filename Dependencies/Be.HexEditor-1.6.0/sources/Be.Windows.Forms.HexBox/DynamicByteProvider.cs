using System;
using System.Collections.Generic;

namespace Be.Windows.Forms
{
    /// <summary>
    /// Byte provider for a small amount of data.
    /// </summary>
    public class DynamicByteProvider : IByteProvider
    {
        /// <summary>
        /// Contains information about changes.
        /// </summary>
        bool _hasChanges;
        /// <summary>
        /// Contains a byte collection.
        /// </summary>
        List<byte> _bytes;

        /// <summary>
        /// Supports overwriting bytes.
        /// </summary>
        bool _supportsWriteByte;

        /// <summary>
        /// Supports inserting bytes.
        /// </summary>
        bool _supportsInsertBytes;

        /// <summary>
        /// Supports deleting bytes.
        /// </summary>
        bool _supportsDeleteBytes;

        /// <summary>
        /// Initializes a new instance of the DynamicByteProvider class.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="supportsWriteByte"></param>
        /// <param name="supportsInsertByte"></param>
        /// <param name="supportsDeleteByte"></param>
        public DynamicByteProvider(byte[] data, bool supportsWriteByte, bool supportsInsertByte, bool supportsDeleteByte) : this(new List<Byte>(data))
        {
            _supportsWriteByte = supportsWriteByte;
            _supportsInsertBytes = supportsInsertByte;
            _supportsDeleteBytes = supportsDeleteByte;
        }

        /// <summary>
        /// Initializes a new instance of the DynamicByteProvider class.
        /// </summary>
        /// <param name="bytes"></param>
        public DynamicByteProvider(List<Byte> bytes)
        {
            _bytes = bytes;
        }

        /// <summary>
        /// Raises the Changed event.
        /// </summary>
        void OnChanged(EventArgs e)
        {
            _hasChanges = true;

            if (Changed != null)
                Changed(this, e);
        }

        /// <summary>
        /// Raises the LengthChanged event.
        /// </summary>
        void OnLengthChanged(EventArgs e)
        {
            if (LengthChanged != null)
                LengthChanged(this, e);
        }

        /// <summary>
        /// Gets the byte collection.
        /// </summary>
        public List<Byte> Bytes
        {
            get { return _bytes; }
        }

        #region IByteProvider Members
        /// <summary>
        /// True, when changes are done.
        /// </summary>
        public bool HasChanges()
        {
            return _hasChanges;
        }

        /// <summary>
        /// Applies changes.
        /// </summary>
        public void ApplyChanges()
        {
            _hasChanges = false;
        }

        /// <summary>
        /// Occurs, when the write buffer contains new changes.
        /// </summary>
        public event EventHandler Changed;

        /// <summary>
        /// Occurs, when InsertBytes or DeleteBytes method is called.
        /// </summary>
        public event EventHandler LengthChanged;


        /// <summary>
        /// Reads a byte from the byte collection.
        /// </summary>
        /// <param name="index">the index of the byte to read</param>
        /// <returns>the byte</returns>
        public byte ReadByte(long index)
        { return _bytes[(int)index]; }

        /// <summary>
        /// Write a byte into the byte collection.
        /// </summary>
        /// <param name="index">the index of the byte to write.</param>
        /// <param name="value">the byte</param>
        public void WriteByte(long index, byte value)
        {
            _bytes[(int)index] = value;
            OnChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Deletes bytes from the byte collection.
        /// </summary>
        /// <param name="index">the start index of the bytes to delete.</param>
        /// <param name="length">the length of bytes to delete.</param>
        public void DeleteBytes(long index, long length)
        {
            int internal_index = (int)Math.Max(0, index);
            int internal_length = (int)Math.Min((int)Length - internal_index, length);

            _bytes.RemoveRange(internal_index, internal_length);

            OnLengthChanged(EventArgs.Empty);
            OnChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Inserts byte into the byte collection.
        /// </summary>
        /// <param name="index">the start index of the bytes in the byte collection</param>
        /// <param name="bs">the byte array to insert</param>
        public void InsertBytes(long index, byte[] bs)
        {
            _bytes.InsertRange((int)index, bs);

            OnLengthChanged(EventArgs.Empty);
            OnChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Gets the length of the bytes in the byte collection.
        /// </summary>
        public long Length
        {
            get
            {
                return _bytes.Count;
            }
        }

        /// <summary>
        /// Returns whether or not bytes can be written.
        /// </summary>
        public bool SupportsWriteByte()
        {
            return _supportsWriteByte;
        }

        /// <summary>
        /// Returns whether or not bytes can be inserted
        /// </summary>
        public bool SupportsInsertBytes()
        {
            return _supportsInsertBytes;
        }

        /// <summary>
        /// Returns whether or not bytes can be deleted
        /// </summary>
        public bool SupportsDeleteBytes()
        {
            return _supportsDeleteBytes;
        }
        #endregion

    }
}
