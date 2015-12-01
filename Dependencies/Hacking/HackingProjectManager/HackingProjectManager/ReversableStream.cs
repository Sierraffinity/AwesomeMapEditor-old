using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Nintenlord.ROMHacking.Collections;

namespace Nintenlord.HackingProjectManager
{
    public class ReversableStream : Stream
    {
        Stream baseStream;
        long realOffset;
        IDataChange<byte> changes;
        
        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanSeek
        {
            get { return baseStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public ReversableStream(Stream baseStream)
        {
            if (!baseStream.CanWrite)
            {
                throw new ArgumentException("Stream must be writable.");
            }
            this.baseStream = baseStream;
            this.changes = new DataChange<byte>();
        }

        protected override void Dispose(bool disposing)
        {
            changes = null;
            realOffset = 0;
            baseStream = null;
            base.Dispose(disposing);
        }
        
        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    realOffset = offset;
                    break;
                case SeekOrigin.Current:
                    realOffset += offset;
                    break;
                case SeekOrigin.End:
                    realOffset = GetRealLength() - offset;
                    break;
                default:
                    break;
            }
            return realOffset;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }


        public override void Flush()
        {
            ApplyChanges();
            baseStream.Flush();
        }

        public override long Length
        {
            get 
            {
                return GetRealLength();
            }
        }

        public override long Position
        {
            get
            {
                return realOffset;
            }
            set
            {
                realOffset = value;
            }
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            changes.AddChangedData((int)realOffset, buffer, offset, count);
            realOffset += count;
        }


        public void ApplyChanges()
        {
            foreach (var item in (changes as IEnumerable<KeyValuePair<int, byte[]>>))
            {
                baseStream.Position = item.Key;
                baseStream.Write(item.Value, 0, item.Value.Length);
            }

            changes.Clear();
        }

        public void DiscardChanges()
        {
            changes.Clear();
        }

        private long GetRealLength()
        {
            return Math.Max(baseStream.Length, changes.LastOffset);
        }
    }
}
