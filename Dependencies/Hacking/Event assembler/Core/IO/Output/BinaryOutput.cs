using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Nintenlord.Event_Assembler.Core.IO.Output
{
    class BinaryStreamOutput : IBinaryOutput
    {
        Stream stream;

        public BinaryStreamOutput(Stream stream)
        {
            this.stream = stream;
        }

        #region IBinaryOutput Members

        public int Offset
        {
            get
            {
                return (int)stream.Position;
            }
            set
            {
                stream.Position = value;
            }
        }

        public void Write(byte[] data)
        {
            this.Write(data, 0, data.Length);
        }

        public void Write(byte[] data, int index)
        {
            this.Write(data, index, data.Length - index);
        }

        public void Write(byte[] data, int index, int length)
        {
            stream.Write(data, index, length);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            stream.Flush();
            stream.Dispose();
        }

        #endregion
    }
}
