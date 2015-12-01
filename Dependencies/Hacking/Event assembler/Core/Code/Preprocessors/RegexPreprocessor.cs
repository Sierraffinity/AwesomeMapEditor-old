using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Nintenlord.Event_Assembler.Collections;
using Nintenlord.Event_Assembler.Utility;
using Nintenlord.Event_Assembler.IO;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors
{
    class RegexPreprocessor// : IPreprocessor
    {
        Regex comments;
        IMessageLog messageLog;
        List<string> predefined;
        List<string> reserved;
        Dictionary<char, char> uniters;
        List<char> macroSeparators;
        List<char> parameterSeparators;
        IDefineCollection defCol;
        StringBuilder output;
        Stack<bool> include;

        const bool includedFilesAsNewScope = true;
        const string lineDefine = "_line_";
        const string fileDefine = "_file_";

        public RegexPreprocessor(IMessageLog messageHandler)
        {
            this.messageLog = messageHandler;
            this.predefined = new List<string>();
            //this.defCol = new DefineCollectionNew();
            this.reserved = new List<string>();
            this.uniters = new Dictionary<char, char>();
            this.macroSeparators = new List<char>();
            this.parameterSeparators = new List<char>();

            uniters['"'] = '"';
            parameterSeparators.AddRange(" \t");
            uniters['('] = ')';
            uniters['['] = ']';
            macroSeparators.Add(',');
            comments = new Regex(@"(//.*)|(/\*((.|\n)*?)\*/)", RegexOptions.Compiled | RegexOptions.Multiline);
        }

        private string ReplaceMultiLineComment(Match match)
        {
            string text = match.Value;
            int lineAmount = text.AmountOfLines();
            if (lineAmount == 0)
            {
                return " ";
            }
            else return "".PadLeft(lineAmount, '\n');
        }

        #region IPreprocessor Members

        public string Process(string path)
        {
            defCol = new DefineCollection();
            output = new StringBuilder(1000);
            include = new Stack<bool>();

            string text = File.ReadAllText(path);

            text = ReplaceComments(text);

            include.Push(true);
            using (StringReader reader = new StringReader(text))
            {
                Preprocess(reader, Path.GetFullPath(path));
            }
            output.Replace(';', '\n');

            return output.ToString();
        }

        public void AddDefined(string[] original)
        {
            predefined.AddRange(original);
        }

        public void AddReserved(string[] reserved)
        {
            this.reserved.AddRange(reserved);
        }

        #endregion

        protected void Preprocess(StringReader reader, string path)
        {
            string line;
            int lineNumber = 1;
            while ((line = reader.ReadLine()) != null)
            {
                int amonutOfLinesRead = 1;
                List<string> splitLine = new List<string>(line.Split(parameterSeparators, uniters));
                if (splitLine.Count > 0)
                {
                    if (splitLine[0][0] == '#')
                    {
                        string newLine;
                        while (splitLine.Last().EndsWith("/") && (newLine = reader.ReadLine()) != null)
                        {
                            line += newLine;
                            splitLine.AddRange(newLine.Split(parameterSeparators, uniters));
                            amonutOfLinesRead++;
                        }

                        PreprocessorDirective(line, splitLine[0].Substring(1), splitLine.ToArray().GetRange(1),
                            path, lineNumber);
                    }
                    else if (line.Length > 0 && include.And())
                    {
                        if (!defCol.ApplyDefines(line, out line))//do define replacement
                        {
                            messageLog.AddError(path, line, "Error applying defines or macros.");
                        }
                        //Replace _line_ and _file_
                        line = line.Replace(lineDefine, lineNumber.ToString());
                        line = line.Replace(fileDefine, "\"" + path + "\"");
                        foreach (string item in predefined)
                        {
                            line = line.Replace(item, " ");
                        }
                        output.AppendLine(line);
                    }

                }
                lineNumber += amonutOfLinesRead;
            }
        }

        private void PreprocessorDirective(string line, string name, string[] parameters,
            string path, int lineNumber)
        {
            switch (name)
            {
                case "ifdef":
                    if (parameters.Length > 0)
                    {
                        include.Push(defCol.ContainsName(parameters[0]) || predefined.Contains(parameters[0]));
                    }
                    else
                    {
                        messageLog.AddError(path, line, "#ifdef requires 1 parameter");
                    }
                    break;
                case "ifndef":
                    if (parameters.Length > 0)
                    {
                        include.Push(!(defCol.ContainsName(parameters[0]) || predefined.Contains(parameters[0])));
                    }
                    else
                    {
                        messageLog.AddError(path, line, "#ifndef requires 1 parameter");
                    }
                    break;
                case "else":
                    if (include.Count > 0)
                    {
                        include.Push(!include.Pop());
                    }
                    else
                    {
                        messageLog.AddError(path, line, "#else used without #ifdef or #ifndef.");
                    }
                    break;
                case "endif":
                    if (include.Count > 0)
                    {
                        include.Pop();
                    }
                    else
                    {
                        messageLog.AddError(path, line, "#endif used without #ifdef or #ifndef.");
                    }
                    break;
                case "define":
                    if (include.And())
                    {
                        if (parameters.Length > 1)
                        {
                            string[] macroParam;
                            string mname;
                            int startIndex = parameters[0].IndexOf('(');
                            int endIndex = parameters[0].LastIndexOf(')');
                            if (startIndex != -1 && endIndex != -1 && startIndex < endIndex)
                            {
                                string paramString = parameters[0].Substring(
                                    startIndex + 1, endIndex - startIndex - 1);
                                macroParam = paramString.Split(macroSeparators, uniters);
                                mname = parameters[0].Substring(0, startIndex);
                            }
                            else
                            {
                                macroParam = new string[0];
                                mname = parameters[0];
                            }
                            for (int j = 0; j < macroParam.Length; j++)
                            {
                                macroParam[j] = macroParam[j].Trim();
                            }


                            if (mname.Equals(parameters[1]))
                            {
                                messageLog.AddWarning(path, line, "Defining something as itself.");
                            }
                            else if (!defCol.IsValidName(mname))
                            {
                                messageLog.AddError(path, line, mname + " is not valid name to define.");
                            }
                            else if (predefined.Contains(mname))
                            {
                                messageLog.AddError(path, line, mname + " cannot be redefined.");
                            }
                            else if (reserved.Contains(mname))
                            {
                                messageLog.AddError(path, line, mname + " is reserved.");
                            }
                            else
                            {
                                if (defCol.ContainsName(mname, macroParam))
                                {
                                    messageLog.AddWarning(path, line, "Redefining " + mname);
                                }
                                defCol.Add(mname, parameters[1].Trim('"'), macroParam);
                            }
                        }
                        else if (parameters.Length == 1)
                        {
                            defCol.Add(parameters[0], "");
                        }
                        else
                        {
                            messageLog.AddError(path, line, "#define requires 1 parameters");
                        }
                    }
                    break;
                case "undef":
                    if (include.And())
                    {
                        if (parameters.Length > 0)
                        {
                            //Check amount of macro parameters
                            defCol.Remove(parameters[0]);
                        }
                        else
                        {
                            messageLog.AddError(path, line, "#undef requires 1 parameters");
                        }
                    }
                    break;
                case "include":
                    if (include.And())
                    {
                        if (parameters.Length > 0)
                        {
                            string file = IOHelpers.FindFile(path, parameters[0]);
                            if (file.Length > 0)
                            {
                                if (file.Equals(path))
                                {
                                    messageLog.AddError(path, line, "File including itself.");
                                }
                                else
                                {
                                    string newAssembly = File.ReadAllText(file);
                                    newAssembly = ReplaceComments(newAssembly);

                                    if (includedFilesAsNewScope) output.AppendLine("{");
                                    using (StringReader reader = new StringReader(newAssembly))
                                    {
                                        Preprocess(reader, Path.GetFullPath(file));
                                    }
                                    if (includedFilesAsNewScope) output.AppendLine("}");
                                }
                            }
                            else
                            {
                                messageLog.AddFileNotFoundError(path, line, parameters[0]);
                            }
                        }
                        else
                        {
                            messageLog.AddError(path, line, "#include requires 1 parameters");
                        }
                    }
                    break;
                case "incbin":
                    if (include.And())
                    {
                        if (parameters.Length > 0)
                        {
                            string file = IOHelpers.FindFile(path, parameters[0]);
                            if (file.Length > 0)
                            {
                                byte[] data = File.ReadAllBytes(file);
                                output.Append("CODE");
                                for (int i = 0; i < data.Length; i++)
                                {
                                    output.Append(data[i].ToHexString(" 0x"));
                                }
                                output.AppendLine();
                            }
                            else
                            {
                                messageLog.AddFileNotFoundError(path, line, parameters[0]);
                            }
                        }
                    }
                    break;
                default:
                    messageLog.AddError(path, line, name + " is not usable preprocessor command.");
                    break;
            }
        }

        protected string ReplaceComments(string text)
        {
            return comments.Replace(text, ReplaceMultiLineComment);
        }

        #region IPreprocessor Members


        public void Process(TextReader input, TextWriter output)
        {
            throw new NotImplementedException();
        }

        #endregion
    }


}
