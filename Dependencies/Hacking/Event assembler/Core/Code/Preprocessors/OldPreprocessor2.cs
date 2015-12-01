using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Nintenlord.Event_assembler.UserInterface;
using Nintenlord.Event_assembler.Collections;
using System.Text.RegularExpressions;

namespace Nintenlord.Event_assembler.Code.Processors
{
    class Preprocessor : IPreprocessor
    {
        IMessageLog messageHandler;
        List<string> predefined;

        const string lineComment = "//";
        const string blockCommentStart = "/*";
        const string blockCommentEnd = "*/";
        const string noLine = "/\n";

        const string define = "#define";
        const string defineFile = "file";
        const string unDefine = "#undef";
        const string includeFile = "#include";
        const string includeBinary = "#incbin";

        const string ifDefined = "#ifdef";
        const string ifNotDefined = "#ifndef";
        const string ifElse = "#else";
        const string ifEnd = "#endif";

        const string lineNumber = "_line_";
        const string fileName = "_file_";

        public Preprocessor(IMessageLog messageHandler, string[] predefined)
        {
            this.messageHandler = messageHandler;
            this.predefined = new List<string>();
            this.predefined.AddRange(predefined);
        }

        public string Process(string path)
        {
            IDefineCollection defcol = new DefineCollection();
            StringBuilder output = new StringBuilder(1000);
            Stack<bool> include = new Stack<bool>();
            string text = File.ReadAllText(path);

            include.Push(true);
            using (StringReader reader = new StringReader(text))
            {
                Preprocess(reader, output, defcol, Path.GetFullPath(path), include);
            }

            return output.ToString();
        }

        private void Preprocess(TextReader reader, StringBuilder output, 
            IDefineCollection defcol, string currentFile, Stack<bool> includeStack)
        {
            int lineNumber = 1;
            string line;
            bool blockComment = true;

            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.Length > 0)
                {
                    //TOFIX?: Handling comments in unincluded lines is extra work.
                    HandleComments(ref line, ref blockComment);
                    if (line.Length > 0)
                    {
                        if (line[0] == '#')
                        {
                            PreprocessorCommand(line, ref lineNumber, includeStack, reader, defcol, currentFile);
                        }
                        else if (includeStack.And())
                        {
                            ApplyDefines(ref line, lineNumber, currentFile, defcol);
                            output.AppendLine(line);
                        }
                    }
                }
                lineNumber++;
            }

            throw new NotImplementedException();
        }

        private void HandleComments(ref string line, ref bool blockComment)
        {
            if (blockComment)
            {
                if (line.Contains(blockCommentEnd))
                {
                    int index = line.IndexOf(blockCommentEnd);
                    line = line.Substring(index + 2);
                    blockComment = false;
                }
                else
                {
                    line = string.Empty;
                }
            }

            //TODO: make handle multiple block commenst properly
            if (line.Length > 0)
            {
                int lineIndex = line.IndexOf(lineComment);
                int blockIndex = line.IndexOf(blockCommentStart);
                
                if (lineIndex >= 0 && (lineIndex < blockIndex || blockIndex < 0))
                {
                    line = line.Substring(0, lineIndex);
                }
                else if (blockIndex >= 0 && (blockIndex < lineIndex || lineIndex < 0))
                {
                    int endIndex;
                    if ((endIndex = line.IndexOf(blockCommentEnd, blockIndex)) > blockIndex)
                    {
                        line = line.Substring(blockIndex, endIndex - blockIndex);
                    }
                    else
                    {
                        line = line.Substring(0, blockIndex);
                        blockComment = true;
                    }
                }
            }
            throw new NotImplementedException();
        }


        private void PreprocessorCommand(string line, ref int lineNumber, Stack<bool> include, TextReader reader, 
            IDefineCollection defcol, string currentFile)
        {
            string newLine;
            while (line.EndsWith("/") && (newLine = reader.ReadLine()) != null)
            {
                line = line.Substring(0, line.Length - 1) + newLine.Trim();
            }

            if (line.StartsWith(ifDefined))
            {

            }
            else if (line.StartsWith(ifNotDefined))
            {

            }
            else if (line.StartsWith(ifElse))
            {

            }
            else if (line.StartsWith(ifEnd))
            {

            }
            else if (include.And())
            {
                
            }
        }

        private void ApplyDefines(ref string text, int lineNumber, string file, IDefineCollection defCol)
        {
            defCol.ApplyDefines(text, out text);
            text = text.Replace(Preprocessor.lineNumber, lineNumber.ToString());
            text = text.Replace(Preprocessor.fileName, "\"" + file + "\"");
        }

        private string FindFile(string currentFile, string newFile)
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

        private void DefineFile(string path, DefineCollection defCol)
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

        private bool InvalidIndex(int index, int length, int empty)
        {
            return index < 0 || index >= length - empty;
        }

        static bool IsValidDefinitionName(string s)
        {
            foreach (char item in s)
            {
                if (!IsValidCharacter(item))
                {
                    return false;
                }
            }
            return true;
        }

        static bool IsValidCharacter(char c)
        {
            if (char.IsLetterOrDigit(c))
            {
                return true;
            }
            if (c == '_')
            {
                return true;
            }
            return false;
        }

        #region IPreprocessor Members


        public void AddDefined(string[] original)
        {
            throw new NotImplementedException();
        }

        public void AddReserved(string[] reserved)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
