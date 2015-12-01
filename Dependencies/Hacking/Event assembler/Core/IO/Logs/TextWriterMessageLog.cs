using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Nintenlord.Event_Assembler.Core.IO.Logs
{
    class TextWriterMessageLog : MessageLog
    {
        TextWriter writer;

        public TextWriter Writer
        {
            get { return writer; }
            set { writer = value; }
        }

        public TextWriterMessageLog(TextWriter writer)
        {
            this.writer = writer;
        }
        
        public override void PrintAll()
        {
            string message = this.GetText();
            writer.WriteLine(message);
        }
    }
}
