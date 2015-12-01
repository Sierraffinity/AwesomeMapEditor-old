using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Nintenlord.Event_Assembler.Core.Collections;

using Nintenlord.Event_Assembler.Core.Code.Templates;
using Nintenlord.Event_Assembler.Core.Code.Language;
using Nintenlord.Collections;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Code
{
    /// <summary>
    /// Loads language raws and processes them into languages and codes
    /// </summary>
    public class LanguageProcessor
    {
        readonly bool collectDocComments;
        readonly IComparer<ICodeTemplate> templateComparer;
        readonly StringComparer stringComparer;

        IDictionary<string, ICodeTemplateStorer> languages;
        IDictionary<string, List<LanguageElement>> elements;
        IDictionary<string, List<DocCode>> docs;

        public IDictionary<string, ICodeTemplateStorer> Languages
        {
            get { return languages; }
        }

        public LanguageProcessor()
            : this(false, Comparer<ICodeTemplate>.Default, StringComparer.OrdinalIgnoreCase)
        {

        }

        public LanguageProcessor(IComparer<ICodeTemplate> templateComparer)
            : this(false, templateComparer, StringComparer.OrdinalIgnoreCase)
        {

        }

        public LanguageProcessor(bool collectDocComments)
            : this(collectDocComments, Comparer<ICodeTemplate>.Default, StringComparer.OrdinalIgnoreCase)
        {

        }

        public LanguageProcessor(bool collectDocComments, IComparer<ICodeTemplate> equalityComparer, 
            StringComparer stringComparer)
        {
            this.collectDocComments = collectDocComments;
            this.templateComparer = equalityComparer;
            this.stringComparer = stringComparer;
            this.docs = new SortedDictionary<string, List<DocCode>>(new NaturalComparer());
            this.languages = new Dictionary<string, ICodeTemplateStorer>();
        }


        public void ProcessCode(string folder, string extension)
        {
            if (Directory.Exists(folder))
            {
                DirectoryInfo directory = new DirectoryInfo(folder);
                folder = Path.GetFullPath(folder);
                FileInfo[] files = directory.GetFiles("*" + extension, SearchOption.AllDirectories);
                elements = new Dictionary<string, List<LanguageElement>>();

                foreach (FileInfo item in files)
                {
                    string file = item.FullName.Substring(folder.Length + 1,
                        item.FullName.Length - folder.Length - extension.Length - 1);

                    ParseLinesInFile(file, File.ReadLines(item.FullName));
                }
            }
            else
            {
                throw new DirectoryNotFoundException("Folder " + folder + " not found.");
            }
        }

        public void ProcessCode(string file)
        {
            if (File.Exists(file))
            {
                ParseLinesInFile(Path.GetFileName(file), File.ReadLines(file));                
            }
            else
            {
                throw new DirectoryNotFoundException("File " + file + " not found.");
            }
        }

        /// <summary>
        /// AE: file != null && !docs.ContainsKey(file) && lines != null
        /// </summary>
        /// <param name="file"></param>
        /// <param name="lines"></param>
        private void ParseLinesInFile(string file, IEnumerable<string> lines)
        {
            List<LanguageElement> elements = new List<LanguageElement>();
            LanguageElement newElement = new LanguageElement();

            foreach (var line in lines)
            {
                if (line.ContainsNonWhiteSpace())
                {
                    if (line[0] == '#')
                    {
                        if (collectDocComments && 
                            line.Length > 1 && 
                            line[1] == '#')
                        {
                            newElement.AddDoc(line.Substring(2));
                        }
                    }
                    else
                    {
                        newElement.SetMainLine(line);
                        elements.Add(newElement);
                        newElement = new LanguageElement();
                    }
                }
            }

            if (collectDocComments)
            {
                int index = 0;
                while (index < elements.Count)
                {
                    DocCode doc = MakeCode(elements, ref index);
                    foreach (var language in doc.languages)
                    {
                        AddCode(doc, language);
                    }
                    //var temp = doc.parameterDocs["ID"];
                    string key = file.Replace('\\', '.');
                    List<DocCode> docList = docs.GetOldOrSetNew(key);
                    docList.Add(doc);
                }
                this.elements[file] = elements;
            }
            else
            {
                int index = 0;
                while (index < elements.Count)
                {
                    DocCode doc = MakeCode(elements, ref index);
                    foreach (var language in doc.languages)
                    {
                        AddCode(doc, language);                        
                    }
                }
            }
        }

        private void AddCode(DocCode doc, string language)
        {
            ICodeTemplateStorer storer;
            if (!languages.TryGetValue(language, out storer))
            {
                languages[language] = new CodeTemplateStorer(templateComparer);
            }
            languages[language].AddCode(doc.code, doc.priority);
        }
        
        DocCode MakeCode(IList<LanguageElement> elements, ref int index)
        {
            List<LanguageElement> codeElements = new List<LanguageElement>();
            int startIndex = index;
            do
            {
                codeElements.Add(elements[index]);
                index++;
            } while (index < elements.Count && elements[index].IsParameter);

            List<string> languages = new List<string>();
            Priority priority;
            ICodeTemplate code = ParseCode(codeElements, languages, out priority);
            DocCode doc = new DocCode();

            doc.code = code;
            doc.priority = priority;

            if (languages.Count > 0)
            {
                doc.languages = languages.ToArray();
            }
            else
            {
                doc.languages = this.languages.Keys.ToArray();
            }

            if (collectDocComments)
            {
                doc.mainDoc = new List<string>(codeElements[0].GetDocLines());
                doc.parameterDocs = new Dictionary<string, List<string>>();
                for (int i = 1; i < codeElements.Count; i++)
                {
                    List<string> values = doc.parameterDocs.GetOldOrSetNew(codeElements[i].ParsedLine.name);
                    values.AddRange(codeElements[i].GetDocLines());
                }
            }

            return doc;
        }

        ICodeTemplate ParseCode(IList<LanguageElement> lines,
            ICollection<string> usedLanguages, out Priority priority)
        {
            ParsedLine header = lines[0].ParsedLine;

            priority = Priority.none;
            bool canBeRepeated = false;
            bool checkForProblems = true;
            bool endingCode = false;
            bool canBeDisAssembled = true;
            bool canBeAssembled = true;
            int indexMode = 1;
            bool itemList = false;
            byte endingByte = 0;
            int offsetMod = 4;

            #region Flags

            foreach (string item in header.flags)
            {
                if (item.StartsWith("language") || item.StartsWith("game"))
                {
                    string[] languagesS = item.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 1; i < languagesS.Length; i++)
                    {
                        usedLanguages.Add(languagesS[i].Trim());
                    }
                }
                else if (item.StartsWith("priority"))
                {
                    int index = item.IndexOf(':');
                    string priorityS = item.Substring(index + 1);
                    if (Enum.GetNames(typeof(Priority)).Contains(priorityS))
                    {
                        priority = (Priority)Enum.Parse(typeof(Priority), priorityS);
                    }
                    else
                    {
                        throw new FormatException("Error in enum priority: " + item);
                    }
                }
                else if (item.StartsWith("repeatable"))
                {
                    canBeRepeated = true;
                }
                else if (item.StartsWith("unsafe"))
                {
                    checkForProblems = false;
                }
                else if (item.StartsWith("end"))
                {
                    endingCode = true;
                }
                else if (item.StartsWith("noDisassembly"))
                {
                    canBeDisAssembled = false;
                }
                else if (item.StartsWith("noAssembly"))
                {
                    canBeAssembled = false;
                }
                else if (item.StartsWith("indexMode"))
                {
                    int index = item.IndexOf(':');
                    string indexModeS = item.Substring(index + 1);
                    indexMode = int.Parse(indexModeS);
                }
                else if (item.StartsWith("terminatingList"))
                {
                    int index = item.IndexOf(':');
                    string endingByteS = item.Substring(index + 1);
                    endingByte = (byte)int.Parse(endingByteS);
                    itemList = true;
                }
                else if (item.StartsWith("offsetMod"))
                {
                    int index = item.IndexOf(':');
                    string offsetModeS = item.Substring(index + 1);
                    offsetMod = (byte)int.Parse(offsetModeS);
                }
                else
                {
                    throw new FormatException("Unknown option " +
                        item + " in parameter " + header.name);
                }
            } 
            #endregion

            //if (priority == Priority.none && header.number1 == 0)
            //{
            //    throw new Exception("ID has to be non-zero for default priority.");
            //}

            header.number2 = header.number2 * indexMode;

            List<TemplateParameter> parameters = new List<TemplateParameter>();
            for (int i = 1; i < lines.Count; i++)
            {
                ParsedLine line = lines[i].ParsedLine;
                line.number1 *= indexMode;
                line.number2 *= indexMode;
                TemplateParameter param = ParsedLine.ParseParameter(line);
                parameters.Add(param);
            }

            ICodeTemplate code;
            if (itemList)
            {
                code = new TerminatingStringTemplate(header.name, parameters, endingByte, offsetMod, stringComparer);
            }
            else
            {
                code = new CodeTemplate(header.name, header.number1, header.number2,
                    parameters, canBeRepeated, checkForProblems, endingCode, offsetMod, 
                    canBeAssembled, canBeDisAssembled, stringComparer);
            }
            return code;
        }
        
        private void AddBuiltInCodeDocs()
        {
            var raw = new RawCodeTemplate(stringComparer);
            var fill = new CodeFillerTemplate(stringComparer);

            List<DocCode> docList = new List<DocCode>();
            DocCode doc = new DocCode();
            docs["Built in codes"] = docList;
            doc.code = raw;
            doc.languages = new string[0];
            doc.mainDoc = new List<string>();
            doc.parameterDocs = new Dictionary<string, List<string>>();
            docList.Add(doc);

            doc = new DocCode();
            doc.code = fill;
            doc.languages = new string[0];
            doc.mainDoc = new List<string>();
            doc.parameterDocs = new Dictionary<string, List<string>>();
            docList.Add(doc);
        }



        public void WriteDocs(TextWriter writer)
        {
            var indentedWriter = new IndentedTextWriter(writer, " ");
            List<string> names = new List<string>();
            
            foreach (var code in docs)
            {
                string[] newNames = code.Key.Split('.');
                int same = names.GetEqualsInBeginning(newNames);
                indentedWriter.Indent -= names.Count;
                indentedWriter.Indent += same;
                for (int i = same; i < newNames.Length; i++)
                {
                    indentedWriter.WriteLine(newNames[i]);
                    indentedWriter.Indent++;
                }
                
                var sortedDocs = new Dictionary<string, List<DocCode>>();
                foreach (var docCode in code.Value)
                {
                    List<DocCode> list = sortedDocs.GetOldOrSetNew(docCode.code.Name);
                    list.Add(docCode);
                }
                foreach (var list in sortedDocs.Values)
                {
                    WriteCode(list, indentedWriter);
                    writer.WriteLine();
                }

                names.Clear();
                names.AddRange(newNames);
            }
        }

        private void WriteCode(List<DocCode> list, IndentedTextWriter writer)
        {
            if (list[0].code is CodeTemplate)
            {
                CodeTemplate[] templates = Array.ConvertAll<DocCode, CodeTemplate>(
                    list.ToArray(),
                    x => x.code as CodeTemplate);

                WriteCodeTemplates(list, writer, templates);
            }
            else if (list[0].code is TerminatingStringTemplate)
            {
                TerminatingStringTemplate[] templates = Array.ConvertAll<DocCode, TerminatingStringTemplate>(
                    list.ToArray(),
                    x => x.code as TerminatingStringTemplate);
                WriteTerminatingStringTemplate(list, writer, templates);
            }
            else if (list[0].code is IFixedDocString)
            {
                string text = (list[0].code as IFixedDocString).DocString;

                foreach (var line in text.Split("\n\r".ToCharArray(), 
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    writer.WriteLine(line);
                }
            }
        }

        private static void WriteTerminatingStringTemplate(IList<DocCode> list,
            IndentedTextWriter writer, IList<TerminatingStringTemplate> templates)
        {            
            //TerminatingStringTemplate.WriteDoc(writer, template);

            var parameterDocs = new Dictionary<string, List<string>>(
                StringComparer.CurrentCultureIgnoreCase);

            var templatesSortedByGame = new Dictionary<string, List<TerminatingStringTemplate>>();

            for (int i = 0; i < templates.Count; i++)
            {
                string key = list[i].languages.ToHumanString();
                List<TerminatingStringTemplate> tempList = templatesSortedByGame.GetOldOrSetNew(key);
                tempList.Add(templates[i]);
                
                List<string> values = parameterDocs.GetOldOrSetNew(templates[i].Parameter.name);
                values.AddRange(list[i].parameterDocs[templates[i].Parameter.name]);
            }

            foreach (var item in templatesSortedByGame)
            {
                writer.WriteLine(item.Key + ":");
                writer.Indent++;
                foreach (var template in item.Value)
                {
                    TerminatingStringTemplate.WriteDoc(writer, template);
                }
                writer.Indent--;
            }
            writer.WriteLineNoTabs("");

            writer.Indent++;

            string[] mainDoc = null;
            foreach (var item in list)
            {
                if (item.mainDoc.Count > 0)
                {
                    mainDoc = item.mainDoc.ToArray();
                    break;
                }
            }

            if (mainDoc != null)
            {
                foreach (var item in list[0].mainDoc)
                {
                    writer.WriteLine(item);
                }
            }
#if DEBUG
            else
            {
                writer.WriteLine("No doc for this code found.");
            }
#endif
            if (parameterDocs.Count > 0)
            {
                WriteParameters(writer, parameterDocs);
            }
            writer.Indent--;
        }

        private static void WriteCodeTemplates(IList<DocCode> list, 
            IndentedTextWriter indentedWriter, 
            IList<CodeTemplate> templates)
        {
            var parameterDocs = new Dictionary<string, List<string>>(
                StringComparer.CurrentCultureIgnoreCase);

            var templatesSortedByGame = new Dictionary<string, List<CodeTemplate>>();
            
            for (int i = 0; i < templates.Count; i++)
            {
                string key = list[i].languages.ToHumanString();
                List<CodeTemplate> tempList = templatesSortedByGame.GetOldOrSetNew(key);
                tempList.Add(templates[i]);
                
                foreach (var parameter in templates[i])
                {
                    List<string> values = parameterDocs.GetOldOrSetNew(parameter.name);
                    values.AddRange(list[i].parameterDocs[parameter.name]);     
                }
            }

            foreach (var item in templatesSortedByGame)
            {
                indentedWriter.WriteLine(item.Key + ":");
                indentedWriter.Indent++;
                foreach (var template in item.Value)
                {
                    CodeTemplate.WriteDoc(indentedWriter, template);
                }
                indentedWriter.Indent--;
            }

            indentedWriter.Indent++;

            List<string> mainDoc = new List<string>();
            foreach (var item in list)
            {
                if (item.mainDoc.Count > 0)
                {
                    mainDoc.AddRange(item.mainDoc);
                    break;
                }
            }

            if (mainDoc.Count > 0)
            {
                indentedWriter.WriteLineNoTabs("");
                foreach (var item in mainDoc)
                {
                    indentedWriter.WriteLine(item);
                }
            }
#if DEBUG
            else
            {
                indentedWriter.WriteLine("No doc for this code found.");
            } 
#endif
            if (parameterDocs.Count > 0)
            {
                WriteParameters(indentedWriter, parameterDocs);
            }
            indentedWriter.Indent--;
        }

        private static void WriteParameters(IndentedTextWriter indentedWriter, Dictionary<string, List<string>> parameterDocs)
        {
            indentedWriter.WriteLineNoTabs("");
            indentedWriter.WriteLine("Parameters:");
            indentedWriter.Indent++;

            foreach (var item in parameterDocs)
            {
                if (item.Value.Count > 0)
                {
                    indentedWriter.WriteLine("{0} = {1}", item.Key, item.Value[0]);
                    indentedWriter.Indent += item.Key.Length + 3;
                    for (int i = 1; i < item.Value.Count; i++)
                    {
                        indentedWriter.WriteLine(item.Value[i]);
                    }
                    indentedWriter.Indent -= item.Key.Length + 3;
                }
                else
                {
                    indentedWriter.WriteLine("{0}", item.Key);
                }
            }
            indentedWriter.Indent--;
        }

        private class LanguageElement
        {
            List<string> docComments;
            string mainLine;
            ParsedLine parsedLine;

            public bool IsParameter
            {
                get;
                private set;
            }
            public ParsedLine ParsedLine
            {
                get { return parsedLine; }
            }
            public string MainLine
            {
                get { return mainLine; }
            }

            public LanguageElement()
            {
                docComments = new List<string>();
            }

            public void AddDoc(string line)
            {
                docComments.Add(line);
            }

            public void SetMainLine(string line)
            {
                IsParameter = char.IsWhiteSpace(line[0]);
                mainLine = line;
                parsedLine = ParsedLine.ParseLine(line);
            }

            public string[] GetDocLines()
            {
                return docComments.ToArray();
            }

            public override string ToString()
            {
                return mainLine;
            }
        }

        private struct ParsedLine
        {
            public string name;
            public int number1;
            public int number2;
            public string[] flags;

            public static TemplateParameter ParseParameter(ParsedLine line)
            {
                int minDimensions = 1;
                int maxDimensions = 1;

                bool pointer = false;
                bool isFixed = false;
                bool signed = false;
                int prefBase = 16;
                Priority pointedPriority = Priority.none;

                foreach (string item in line.flags)
                {
                    if (item.Length == 0)
                    {
                        continue;
                    }
                    int index = item.IndexOf(':');
                    if (item.StartsWith("pointer"))
                    {
                        pointer = true;
                        if (index > 0)
                        {
                            string priority = item.Substring(index + 1);
                            if (Enum.GetNames(typeof(Priority)).Contains(priority))
                            {
                                pointedPriority = (Priority)Enum.Parse(typeof(Priority), priority);
                            }
                        }
                    }
                    else if (item.StartsWith("coordinates") || item.StartsWith("coordinate"))
                    {
                        if (index < 0)
                            throw new FormatException("No : in option " + item);
                        string dimensionsS = item.Substring(index + 1);
                        if (dimensionsS.Contains("-"))
                        {
                            string[] dimensionsSS = dimensionsS.Split('-');

                            minDimensions = int.Parse(dimensionsSS[0]);
                            maxDimensions = int.Parse(dimensionsSS[1]);
                        }
                        else
                        {
                            int dimensions = int.Parse(dimensionsS);
                            minDimensions = dimensions;
                            maxDimensions = dimensions;
                        }
                    }
                    else if (item.StartsWith("preferredBase"))
                    {
                        if (index < 0)
                            throw new FormatException("No : in option " + item);
                        string valueS = item.Substring(index + 1);
                        prefBase = valueS.GetValue();
                    }
                    else if (item.StartsWith("fixed"))
                    {
                        isFixed = true;
                    }
                    else if (item.StartsWith("signed"))
                    {
                        signed = true;
                    }
                    else
                    {
                        throw new FormatException("Unknown option " +
                            item + " in parameter " + line.name);
                    }
                }

                TemplateParameter param = new TemplateParameter(line.name, line.number1, line.number2, minDimensions,
                    maxDimensions, pointer, pointedPriority, signed, isFixed);
                param.SetBase(prefBase);
                return param;
            }

            public static ParsedLine ParseLine(string line)
            {
                ParsedLine parsedLine = new ParsedLine();
                string[] split = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                parsedLine.name = split[0].Trim();
                parsedLine.number1 = StringExtensions.GetValue(split[1].Trim());
                parsedLine.number2 = StringExtensions.GetValue(split[2].Trim());
                if (split.Length > 3)
                {
                    List<string> options = new List<string>(split[3].Split(" -".GetArray(), StringSplitOptions.RemoveEmptyEntries));
                    for (int i = 0; i < options.Count; i++)
                    {
                        options[i] = options[i].Trim();
                    }
                    parsedLine.flags = options.ToArray();

                }
                else
                {
                    parsedLine.flags = new string[0];
                }
                return parsedLine;
            }
        }

        private struct DocCode
        {
            public List<string> mainDoc;
            public string[] languages;
            public ICodeTemplate code;
            public Priority priority;
            public Dictionary<string, List<string>> parameterDocs;

            public override string ToString()
            {
                return code.ToString();
            }
        }

    }
}
