using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Nintenlord.Collections;

namespace Nintenlord.IO
{
    public class ChangeStream : Stream
    {
        IDataChange<byte> changes;
        int position;

        public ChangeStream()
        {
            position = 0;
            changes = new DataChange<byte>();
        }

        public override bool CanRead
        {
            get { return false; }
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
            //No operation required
        }

        public override long Length
        {
            get { return changes.LastOffset; }
        }

        public override long Position
        {
            get
            {
                return position;
            }
            set
            {
                position = (int)value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("Reading not supported.");
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    position = (int)offset;
                    break;
                case SeekOrigin.Current:
                    position += (int)offset;
                    break;
                case SeekOrigin.End:
                    position = changes.LastOffset + (int)offset;
                    break;
                default:
                    break;
            }
            return position;
        }

        public override void SetLength(long value)
        {
            //No operation required
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            changes.AddChangedData(position, buffer, offset, count);
            position += count;
        }

        public void WriteToFile(string file)
        {
            var bytes = File.ReadAllBytes(file);
            changes.Apply(ref bytes);
            File.WriteAllBytes(file, bytes);
        }
    }
}
