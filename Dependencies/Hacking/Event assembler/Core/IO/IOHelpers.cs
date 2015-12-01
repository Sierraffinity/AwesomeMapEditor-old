using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Nintenlord.Event_Assembler.Core.Collections;

namespace Nintenlord.Event_Assembler.Core.IO
{
    static class IOHelpers
    {
        public static string FindFile(string currentFile, string newFile)
        {
            newFile = newFile.Trim('\"');

            if (File.Exists(newFile))
            {
                return newFile;
            }
            else if (!String.IsNullOrEmpty(currentFile))
            {
                string path = Path.GetDirectoryName(currentFile);
                path = Path.Combine(path, newFile);
                if (File.Exists(path))
                {
                    return path;
                }
            }
            return string.Empty;
        }

        public static void DefineFile(string path, IDefineCollection defCol)
        {
            StreamReader sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (line.Length > 0)
                {
                    string[] dividedLine = null;// = line.Split(parameterSplitCharacters, parameterUniterCharacters);
                    for (int i = 1; i < dividedLine.Length; i++)
                    {
                        defCol.Add(dividedLine[i], dividedLine[0]);
                    }
                }
            }
            sr.Close();
        }

        public static char? ReadCharacter(this TextReader reader)
        {
            int value = reader.Read();
            if (value == -1)
            {
                return null;
            }
            else
            {
                return Convert.ToChar(value);
            }
        }

        public static char? PeekCharacter(this TextReader reader)
        {
            int value = reader.Peek();
            if (value == -1)
            {
                return null;
            }
            else
            {
                return Convert.ToChar(value);
            }
        }
    }
}
