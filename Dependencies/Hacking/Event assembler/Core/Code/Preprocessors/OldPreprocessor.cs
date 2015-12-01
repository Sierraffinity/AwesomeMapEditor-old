using System;
using System.Collections.Generic;
using System.Text;
using Nintenlord.Event_assembler.Collections;
using System.IO;
using System.Linq;
using Nintenlord.Event_assembler.UserInterface;
using Nintenlord.Event_assembler.Utility;

namespace Nintenlord.Event_assembler.Code.Processors
{
    /// <summary>
    /// Normal preprocessor
    /// </summary>
    class OldPreprocessor : IPreprocessor
    {
        IMessageLog messageHandler;

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

        char[] allowedDefinitionCharacters = "".ToCharArray();

        char[] wordSplitCharacters = "+-*/%()[]\",; \t".ToCharArray();
        char[] wordArithmetchic = "+-*/%()&|".ToCharArray();
        char[][] parameterUniterCharacters = { "()".ToCharArray(), 
                                               "[]".ToCharArray(), 
                                               "\"\"".ToCharArray() };
        char[] parameterSplitCharacters = " \t".ToCharArray();
        char[] macroParameterSplitCharacters = " \t,".ToCharArray();
        char[] extraLineSplitters = ";".ToCharArray();

        List<string> predefined;

        public OldPreprocessor(IMessageLog messageHandler, string[] predefined)
        {
            this.messageHandler = messageHandler;
            this.predefined = new List<string>();
            this.predefined.AddRange(predefined);
        }
        
        public string Process(string path)
        {
            string assembly = File.ReadAllText(path);
            assembly = this.RemoveComments(assembly);
            DefineCollection defCol = new DefineCollection(predefined.ToArray());
            
            string[] linesArray = Preprocess(assembly, defCol, path);

            linesArray = this.ApplyDefines(linesArray, defCol);
            string[][] parameters = SplitToParameters(linesArray);
            StringBuilder builder = new StringBuilder();
            foreach (var line in parameters)
            {
                foreach (var para in line)
                {
                    builder.Append(para);
                    builder.Append(" ");
                }
                builder.AppendLine();
            }
            return builder.ToString(); ;
        }

        private string RemoveComments(string text)
        {
            char[] rawText = text.ToCharArray();
            int empty = 0;
            for (int i = 0; i < rawText.Length - 1 - empty; i++)//Less to copy when going backwards.
            {
                if (rawText[i] == '/')
                {
                    int lengthToRemove = 0;
                    char c = rawText[i + 1];
                    switch (c)
                    {
                        case '/':
                            int indexOfNew = Array.IndexOf<char>(rawText, '\n',
                                i + 2, rawText.Length - i - 2);
                            int indexOfCarriage = Array.IndexOf<char>(rawText, '\r',
                                i + 2, rawText.Length - i - 2);
                            int index;
                            if (InvalidIndex(indexOfNew, rawText.Length, empty))
                            {
                                indexOfNew = -1;
                            }
                            if (InvalidIndex(indexOfCarriage, rawText.Length, empty))
                            {
                                indexOfCarriage = -1;
                            }

                            if (indexOfNew == -1 && indexOfCarriage == -1)
                            {
                                index = rawText.Length - empty + 1;
                            }
                            else if (indexOfNew == -1)
                            {
                                index = indexOfCarriage;
                            }
                            else if (indexOfCarriage == -1)
                            {
                                index = indexOfNew;
                            }
                            else
                            {
                                index = Math.Min(indexOfCarriage, indexOfNew);
                            }

                            lengthToRemove = index - i;
                            break;
                        case '*':
                            int indexOfNextStar = Array.IndexOf<char>(rawText, '*', i + 2,
                                rawText.Length - i - 2);
                            while (rawText[indexOfNextStar + 1] != '/' && indexOfNextStar >= 0)
                            {
                                indexOfNextStar = Array.IndexOf<char>(rawText, '*',
                                    indexOfNextStar + 1, rawText.Length - indexOfNextStar - 1);
                            }
                            if (indexOfNextStar > 0)
                            {
                                lengthToRemove = indexOfNextStar + 2 - i;
                            }
                            else
                            {
                                lengthToRemove = rawText.Length - i;
                            }
                            break;
                        case '\n':
                            lengthToRemove = 2;
                            if (i + 2 < rawText.Length && rawText[i + 2] == '\r')
                            {
                                lengthToRemove++;
                            }
                            break;
                        case '\r':
                            lengthToRemove = 2;
                            if (i + 2 < rawText.Length && rawText[i + 2] == '\n')
                            {
                                lengthToRemove++;
                            }
                            break;
                        default:
                            break;
                    }
                    //lengthToRemove = Math.Min(lengthToRemove, rawText.Length - empty - i);
                    if (lengthToRemove > 0)
                    {
                        if (empty < rawText.Length - lengthToRemove - i)
                        {
                            rawText.Move(lengthToRemove + i, i,
                                rawText.Length - empty - lengthToRemove - i);
                        }
                        empty += lengthToRemove;
                    }
                }
            }

            return new string(rawText, 0, rawText.Length - empty);
        }

        private bool InvalidIndex(int index, int length, int empty)
        {
            return index < 0 || index >= length - empty;
        }

        private string[] Preprocess(string text, DefineCollection defCol, string path)
        {
            List<string> lines = new List<string>();
            Stack<bool> ifStack = new Stack<bool>();
            ifStack.Push(true);
            using (StringReader reader = new StringReader(text))
            {
                Preprocess(reader, path, defCol, lines, ifStack);
            }

            return lines.ToArray();
        }

        private void Preprocess(TextReader reader, string file, DefineCollection defCol, 
            ICollection<string> lines, Stack<bool> ifStack)
        {
            int stackDepth = ifStack.Count;  
            string line;
            bool include = ifStack.And();

            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.Length > 0)
                {
                    if (line[0] == '#')
                    {
                        string[] splitLine = StringExtensions.Split(line, 
                            parameterSplitCharacters, parameterUniterCharacters);
                        //string[] splitLine = StringHelper.DivideLine(line);

                        switch (splitLine[0].ToLower())
                        {
                            case define:
                                if (include)
                                {
                                    if (splitLine.Length > 1)
                                    {
                                        if (splitLine[1].Equals(defineFile))
                                        {
                                            string path = IOHelpers.FindFile(file, splitLine[2]);
                                            if (!string.IsNullOrEmpty(path))
                                            {
                                                IOHelpers.DefineFile(path, defCol);
                                            }
                                            else
                                            {
                                                messageHandler.AddFileNotFoundError(file, line, splitLine[2]);
                                            }
                                        }
                                        else
                                        {
                                            int indexBeg = splitLine[1].IndexOf('(');
                                            int indexEnd = splitLine[1].IndexOf(')');
                                            if (indexBeg >= 0 && indexEnd >= 0 && indexBeg < indexEnd)
                                            {
                                                string parametersList = splitLine[1].Substring(indexBeg + 1, indexEnd - indexBeg - 1);
                                                string[] parameters = parametersList.Split(',');
                                                string original = splitLine[1].Substring(0, indexBeg);
                                                for (int i = 0; i < parameters.Length; i++)
                                                {
                                                    parameters[i] = parameters[i].Trim();
                                                }
                                                defCol.Add(original, splitLine[2], parameters);
                                            }
                                            else if (indexBeg == -1 && indexEnd == -1)
                                            {
                                                if (splitLine.Length > 2)
                                                {
                                                    defCol.Add(splitLine[1], splitLine[2]);
                                                }
                                                else
                                                {
                                                    defCol.Add(splitLine[1], "");
                                                }
                                            }
                                            else
                                            {
                                                messageHandler.AddError(define + " is improperly defined: " + line);                                                
                                            }
                                        }
                                    }
                                    else
                                    {
                                        messageHandler.AddNotEnoughParametersError(file, line, define, 1);
                                    }
                                }
                                break;
                            case unDefine:
                                if (include)
                                {
                                    if (splitLine.Length > 1)
                                    {
                                        defCol.Remove(splitLine[1]);
                                    }
                                    else
                                    {
                                        messageHandler.AddNotEnoughParametersError(file, line, unDefine, 1);
                                    }
                                }
                                break;
                            case includeFile:
                                if (include)
                                {
                                    if (splitLine.Length > 1)
                                    {
                                        string path = IOHelpers.FindFile(file, splitLine[1]);
                                        if (!string.IsNullOrEmpty(path))
                                        {
                                            string moreText = File.ReadAllText(path);
                                            moreText = this.RemoveComments(moreText);
                                            lines.Add("{");
                                            using (StringReader newReader = new StringReader(moreText))
                                            {
                                                Preprocess(newReader, path, defCol, lines, ifStack);
                                                //Preprocess(moreText, path, defCol, lines, ifStack);
                                            }
                                            lines.Add("}");
                                        }
                                        else
                                        {
                                            messageHandler.AddFileNotFoundError(file, line, splitLine[1]);
                                        }
                                    }
                                    else
                                    {
                                        messageHandler.AddNotEnoughParametersError(file, line, includeFile, 1);
                                    }
                                }
                                break;
                            case includeBinary:
                                if (include)
                                {
                                    if (splitLine.Length > 1)
                                    {
                                        string path = IOHelpers.FindFile(file, splitLine[1]);
                                        if (!string.IsNullOrEmpty(path))
                                        {
                                            byte[] data = File.ReadAllBytes(path);
                                            StringBuilder newLine = new StringBuilder("CODE");
                                            for (int i = 0; i < data.Length; i++)
                                            {
                                                newLine.Append(data[i].ToHexString(" 0x"));
                                            }
                                            lines.Add(newLine.ToString());
                                        }
                                        else
                                        {
                                            messageHandler.AddFileNotFoundError(file, line, splitLine[1]); 
                                        }
                                    }
                                    else
                                    {
                                        messageHandler.AddNotEnoughParametersError(file, line, includeBinary, 1);
                                    }
                                }
                                break;
                            case ifDefined:
                                if (splitLine.Length > 1)
                                {
                                    ifStack.Push(defCol.ContainsName(splitLine[1]));
                                    include = ifStack.And();
                                }
                                else
                                {
                                    messageHandler.AddNotEnoughParametersError(file, line, ifDefined, 1);
                                }
                                break;
                            case ifNotDefined:
                                if (splitLine.Length > 1)
                                {
                                    ifStack.Push(!defCol.ContainsName(splitLine[1]));
                                    include = ifStack.And();
                                }
                                else
                                {
                                    messageHandler.AddNotEnoughParametersError(file, line, ifNotDefined, 1);
                                }
                                break;
                            case ifElse:
                                bool top = ifStack.Pop();
                                ifStack.Push(!top);
                                include = ifStack.And();
                                break;
                            case ifEnd:
                                ifStack.Pop();
                                include = ifStack.And();
                                break;
                            case "#org":
                                lines.Add(line.Substring(1));
                                messageHandler.AddWarning("#ORG no longer exists. Use ORG instead.");
                                break;
                            default:
                                messageHandler.AddError(splitLine[0] + " is not usable preprocessor command: " + line);
                                break;
                        }
                    }
                    else if (include)
                    {
                        string[] newLines = line.Split(extraLineSplitters);
                        for (int i = 0; i < newLines.Length; i++)
                        {
                            lines.Add(newLines[i]);
                        }
                    }
                }
            }
            
            if (ifStack.Count != stackDepth)
            {
                messageHandler.AddWarning("#IFDEF stack unbalanced in file " + file + ".");
            }
        }

        private string[] ApplyDefines(string[] text, DefineCollection defCol)
        {
            List<string> lines = new List<string>();

            for (int i = 0; i < text.Length; i++)
            {
                string line = text[i];
                string[] parameters = line.Split(parameterSplitCharacters, parameterUniterCharacters);

                for (int j = 0; j < parameters.Length; j++)
                {
                    parameters[j] = ApplyDefines(parameters[j], defCol, j == 0);
                }

                StringBuilder newLine = new StringBuilder(parameters[0]);
                for (int j = 1; j < parameters.Length; j++)
                {
                    newLine.Append(" " + parameters[j]);
                }
                line = newLine.ToString();
                
                //Split lines with ;
                string[] splitLine = line.Split(extraLineSplitters, 
                    StringSplitOptions.RemoveEmptyEntries);

                lines.AddRange(splitLine);
            }


            return lines.ToArray();
        }

        private string ApplyDefines(string parameter, DefineCollection defcol, bool firstParam)
        {
            parameter = parameter.Trim();
            if (parameter.StartsWith("@"))
            {
                messageHandler.AddWarning("Do not use @ in front of defined parameter or lable: " + parameter);
                parameter = parameter.TrimStart('@');
                if (firstParam)
                {
                    parameter += ":";
                }
            }
            if (parameter.ContainsAnyOf(extraLineSplitters))
            {
                string[] splitLine = parameter.Split(extraLineSplitters,
                    StringSplitOptions.RemoveEmptyEntries);

                StringBuilder newline = new StringBuilder();
                for (int i = 0; i < splitLine.Length; i++)
                {
                    splitLine[i] = ApplyDefines("\"" + splitLine[i] + "\"", defcol, firstParam && i == 0);
                    newline.Append(splitLine[i] + ";");
                }
                return newline.ToString(0, newline.Length - 1);
            }
            
            int index = parameter.IndexOf('(');
            int arithIndex = parameter.IndexOfAny(wordArithmetchic);
            bool startsWith = parameter.StartsWith("[");
            bool endsWith = parameter.EndsWith("]");
            bool startsWithQuote = parameter.StartsWith("\"");
            bool endsWithQuote = parameter.EndsWith("\"");

            if (startsWith && endsWith)//Coordinate handling
            {
                string[] parameters = parameter.Trim('[', ']').Split(',');
                StringBuilder newLine = new StringBuilder();
                if (parameters.Length > 1)
                {
                    newLine.Append("[");
                    for (int i = 0; true; i++)
                    {
                        newLine.Append(ApplyDefines(parameters[i], defcol, false));
                        if (i == parameters.Length - 1)
                            break;
                        newLine.Append(",");
                    }
                    newLine.Append("]");
                }
                else
                {
                    newLine.Append(ApplyDefines(parameters[0], defcol, false));
                }
                return newLine.ToString();
            }
            else if (startsWith != endsWith)//Vector calculus: 1*[8,9] one day?
            {
                messageHandler.AddError("Error with parameter: " + parameter + ". Coordinate problem.");
                return "0";
            }
            else if (startsWithQuote && endsWithQuote)
            {
                string[] parameters = parameter.Trim('\"').Split(' ');
                StringBuilder newLine = new StringBuilder();
                if (parameters.Length > 1)
                {
                    for (int i = 0; true; i++)
                    {
                        newLine.Append(ApplyDefines(parameters[i], defcol, false));
                        if (i == parameters.Length - 1)
                            break;
                        newLine.Append(" ");
                    }
                }
                else
                {
                    newLine.Append(ApplyDefines(parameters[0], defcol, false));
                }
                return newLine.ToString();
            }
            else if (startsWithQuote != endsWithQuote)
            {
                messageHandler.AddError("Error with parameter: " + parameter + ". Quote problem.");
                return "0";
            }
            else if (index > 0 && (index <= arithIndex || arithIndex < 0))//handle macros
            {
                string macroName = parameter.Substring(0, index);
                string macroParameters = parameter.Substring(index + 1, parameter.Length - index - 2);
                string[] macroParametersSplit = macroParameters.Split(
                    macroParameterSplitCharacters, parameterUniterCharacters);

                List<string> appliedParameters = new List<string>();

                for (int i = 0; i < macroParametersSplit.Length; i++)
                {
                    macroParametersSplit[i] = ApplyDefines(macroParametersSplit[i], defcol, false);
                    appliedParameters.AddRange(macroParametersSplit[i].Trim().Split(','));
                }

                string[] macroParametersArray = appliedParameters.ToArray();
                KeyValuePair<string, string[]> replacer = defcol.GetReplacerAndParameters(
                    macroName, macroParametersArray);
                if (replacer.Key == null || replacer.Value == null)
                {
                    messageHandler.AddError("Match for macro not found: " + parameter);
                    return parameter;
                }
                parameter = replacer.Key.ReplaceEach(replacer.Value, macroParametersArray);
                parameter = parameter.Trim('\"');
                return ApplyDefines(parameter, defcol, false);
            }
            else if (arithIndex >= 0)//Arithmetic
            {
                #region Mess and lots of it
                int inBeg = parameter.AmountInTheBeginning('(');
                int inEnd = parameter.AmountInTheEnd(')');
                int min = Math.Min(inBeg, inEnd);
                if (min > 0)
                {
                    parameter = parameter.Substring(min, parameter.Length - 2 * min);
                }

                int parenthIndex = parameter.IndexOf('(');
                int multIndex = parameter.IndexOfAny(new char[] { '*', '/', '%' });
                int additionIndex = parameter.IndexOfAny(new char[] { '+', '-' });
                int binaryIndex = parameter.IndexOfAny(new char[] { '&', '|', '^' });
                if (parenthIndex >= 0)
                {
                    int endIndex = parameter.IndexOf(')', parenthIndex);
                    if (endIndex < 0)
                    {
                        //throw error
                    }

                    string first = parameter.Substring(parenthIndex + 1, endIndex - parenthIndex - 1);

                    int parenthValue = StringExtensions.GetValue(ApplyDefines(first, defcol, firstParam));
                    return ApplyDefines(parameter.Replace("(" + first + ")", parenthValue.ToString()), defcol, false);
                }
                else
                {
                    //int index;
                    if (binaryIndex >= 0)
                        index = binaryIndex;
                    else if (additionIndex >= 0)
                        index = additionIndex;
                    else if (multIndex >= 0)
                        index = multIndex;
                    else
                        index = 0;

                    string first = parameter.Substring(0, index);
                    string second = parameter.Substring(index + 1);
                    int value1 = 0;
                    int value2 = 0;
                    if (first.IsValidNumber())
                    {
                        value1 = StringExtensions.GetValue(ApplyDefines(first, defcol, firstParam));
                    }
                    if (second.IsValidNumber())
                    {
                        value2 = StringExtensions.GetValue(ApplyDefines(second, defcol, false));
                    }
                    int value;
                    switch (parameter[index])
                    {
                        case '*':
                            value = value1 * value2;
                            break;
                        case '/':
                            value = value1 / value2;
                            break;
                        case '%':
                            value = value1 % value2;
                            break;
                        case '+':
                            value = value1 + value2;
                            break;
                        case '-':
                            value = value1 - value2;
                            break;
                        case '&':
                            value = value1 & value2;
                            break;
                        case '|':
                            value = value1 | value2;
                            break;
                        case '^':
                            value = value1 ^ value2;
                            break;
                        default:
                            throw new InvalidOperationException();
                    }
                    return value.ToString();
                }
                #endregion
            }
            else if (defcol.ContainsName(parameter))//The most common case
            {
                parameter = ApplyDefines(defcol.GetReplacer(parameter), defcol, firstParam);
                parameter = parameter.Trim('\"');
                return parameter;
            }
            
            
            return parameter;
        }



        private string[][] SplitToParameters(string[] lines)
        {
            string[][] result = new string[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                result[i] = lines[i].Split(parameterSplitCharacters, StringSplitOptions.RemoveEmptyEntries);
            }
            return result;
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