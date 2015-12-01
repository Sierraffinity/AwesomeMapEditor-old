using System;
using System.Collections.Generic;
using System.IO;
using Nintenlord.Event_Assembler.Core.Code.Preprocessors;
using Nintenlord.Event_Assembler.Core.IO.Input;
using Nintenlord.Collections;
using Nintenlord.Utility;
using Nintenlord.IO;

namespace Nintenlord.Event_Assembler.Core.Code
{
    public class PreprocessingInputStream : IInputStream
    {
        IPreprocessor preprocessor;
        Stack<PrimitiveStream> positions;
        LinkedArrayList<string> unreadLines;

        public PreprocessingInputStream(TextReader reader, IPreprocessor preprocessor)
        {
            this.preprocessor = preprocessor;
            this.positions = new Stack<PrimitiveStream>();
            this.unreadLines = new LinkedArrayList<string>();
            PrimitiveStream newData = new PrimitiveStream(reader);
            positions.Push(newData);
        }

        private class PrimitiveStream
        {
            public int LineNumber
            {
                get;
                private set;
            }
            public string OriginalLine
            {
                get;
                private set;
            }
            public string Name
            {
                get;
                private set;
            }

            private IEnumerator<string> lines;

            public PrimitiveStream(TextReader reader)
                : this(reader.LineEnumerator(), reader.GetReaderName())
            {
            }
            
            public PrimitiveStream(IEnumerable<string> reader, string name)
            {
                Name = name;
                lines = reader.GetEnumerator();
                LineNumber = 0;
            }

            public string ReadLine()
            {
                LineNumber++;
                bool moved = lines.MoveNext();
                OriginalLine = lines.Current;
                return moved ? lines.Current : null;
            }

            public bool ReadLine(out string line)
            {
                LineNumber++;
                bool moved = lines.MoveNext();
                line = lines.Current;
                OriginalLine = lines.Current;
                return moved;
            }

            public void Close()
            {
                lines.Dispose();
                OriginalLine = null;
                LineNumber = -1;
            }

            public void Reset()
            {
                lines.Reset();
                LineNumber = 0;
                OriginalLine = null;
            }
        }

        #region IInputStream Members
        
        public string ReadLine()
        {
            if (positions.Count == 0)
                return null;

            if (unreadLines.Count > 0)
            {
                string line = unreadLines.First;
                unreadLines.RemoveFirst();
                return line;
            }
            else
            {
                PrimitiveStream currentData = positions.Peek();
                string line;

                if (currentData.ReadLine(out line))
                {
                    string pros = preprocessor.Process(line, this);
                    var codes = pros.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 1; i < codes.Length; i++)
                    {
                        unreadLines.Add(codes[0]);
                    }
                    
                    return codes.Length == 0 ? ReadLine() : codes[0];
                }
                else //End of file
                {
                    positions.Pop();
                    if (positions.Count > 0)
                    {
                        currentData.Close();
                    }
                    return ReadLine();
                }
            }
        }

        public string PeekOriginalLine()
        {
            var top = positions.Peek();
            string temp = top.OriginalLine;
            
            if (temp != null)
            {
                return temp;
            }
            else
            {
                throw new InvalidOperationException("No lines have been read or stream has passed the end.");
            }
        }
        
        public int LineNumber
        {
            get { return positions.Peek().LineNumber; }
        }

        public string CurrentFile
        {
            get { return positions.Peek().Name; }
        }

        public void OpenSourceFile(string path)
        {
            if (unreadLines.Count > 0)
            {
                throw new InvalidOperationException();
            }
            PrimitiveStream newData = new PrimitiveStream(new StreamReader(path));
            positions.Push(newData);
        }

        public void OpenBinaryFile(string path)
        {
            if (unreadLines.Count > 0)
            {
                throw new InvalidOperationException();
            }
            byte[] data = File.ReadAllBytes(path);
            unreadLines.AddLast(Array.ConvertAll<byte, string>(data, 
                        x => x.ToString()
                    ).ToElementWiseString(" ","CODE ","")
                );
        }

        public void AddNewLines(IEnumerable<string> lines)
        {
            foreach (var item in lines)
            {
                unreadLines.AddLast(item);
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            foreach (var item in positions)
            {
                item.Close();
            }
            positions.Clear();
        }

        #endregion
    }
}
