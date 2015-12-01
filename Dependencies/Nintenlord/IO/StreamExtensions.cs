using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Nintenlord.IO
{
    public static class StreamExtensions
    {
        public static bool IsAtEnd(this Stream current)
        {
            return current.Position >= current.Length;
        }

        public static IEnumerable<string> LineEnumerator(this TextReader reader)
        {
            string line;
            while (true)
            {
                line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }
                yield return line;
            }
        }

        public static string GetReaderName(this TextReader reader)
        {
            string name;
            if (reader is StreamReader)
            {
                StreamReader reader2 = (StreamReader)reader;
                if (reader2.BaseStream is FileStream)
                {
                    name = ((FileStream)reader2.BaseStream).Name;
                }
                else
                {
                    name = reader2.BaseStream.GetType().Name;
                }
            }
            else
            {
                name = "Standard input";
            }
            return name;
        }

    }
}
