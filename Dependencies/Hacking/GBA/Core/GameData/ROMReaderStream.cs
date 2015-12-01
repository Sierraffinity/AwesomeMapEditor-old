using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Nintenlord.Feditor.Core.GameData
{
    public sealed class ROMReaderStream : Stream
    {
        IROM rom;
        long position;

        public ROMReaderStream(IROM romToRead)
        {
            rom = romToRead;
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
            get { return false; }
        }

        public override void Flush()
        {
            //No OP
        }

        public override long Length
        {
            get { return rom.Length; }
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
            var data = rom.ReadData((int)position, count);

            Array.Copy(data, 0, buffer, offset, data.Length);
            position += count;
            return data.Length;
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
                    position = rom.Length + offset;
                    break;
                default:
                    break;
            }
            return position;
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
    }
}
