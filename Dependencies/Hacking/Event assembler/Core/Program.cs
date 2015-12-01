using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Nintenlord.Event_Assembler.Core.Code;
using Nintenlord.Event_Assembler.Core.Code.Language;
using Nintenlord.Event_Assembler.Core.Code.Preprocessors;
using Nintenlord.Event_Assembler.Core.Code.Templates;
using Nintenlord.Event_Assembler.Core.GBA;
using Nintenlord.Event_Assembler.Core.IO.Input;
using Nintenlord.Event_Assembler.Core.IO.Logs;
using Nintenlord.Collections;
using Nintenlord.Utility;
using Nintenlord.IO.Scanners;
using Nintenlord.Event_Assembler.Core.Code.Language.Lexer;
using Nintenlord.Event_Assembler.Core.Code.Language.Expression;
using Nintenlord.Parser;
using Nintenlord.Parser.UnaryParsers;

namespace Nintenlord.Event_Assembler.Core
{
    /// <summary>
    /// Done:
    ///       Fix problem with merging codes making label positions vanish.
    ///       Fix template comparing to return UNIT instead of UNIT 0.
    ///       Fix problem with 1 bit long parameters.
    ///       Fix problem with bits getting reversed when reading/writing. *A FEATURE, NOT A BUG*
    ///       Make preprocessor handle stacked block comments properly.
    ///       Make sure paths like \Test\test.txt are processed correctly.
    ///       Fix Template choosing 0 0 0 0 over [0,0,0,0]
    ///       Make EACodeLanguage to reveal it's codes somehow.
    ///       Add pool ability to preprocessor, with second parameter as optional label name.
    ///       Add built-in macros like ?(), >(), =(), cond(), vector buiding and unbuilding, etc.
    ///       Rewrite macro storing to make searching faster.
    ///       Rewrite code template storing to make searching faster.
    ///       Remove ChooseEnum from IMEssageLog.
    ///       Move LanguageRawsAnalyzer to Core.
    ///       Make codes give better error codes if code exists but amount of parameters is right.
    ///       Make $XX008001 fail properly.
    ///
    /// Later releases:
    ///       Make preprocessor properly report errors with line "#"
    ///       Make Disassembly use BinaryReader or some sort of input-stream somehow.
    ///       Make recursive macros work properly.
    ///       Move game specific things to separate files.
    ///       Add support for disassembling fixed (other parameters affect) amount of pointed code.
    ///       Make structure disassembly more modular.
    ///       More options to disassembly.
    ///       Add list/vector processing macros aka Head, Tail and Cons, then implement EAstdlib
    ///         macros based on them like Map, Fold and etc.
    ///       Add built-in macro for getting lists/vectors length.
    ///       Make IDefineCollection to reveal it's defines and macros somehow.
    ///       Make language raws processing and code assembling more modular for possible future IDE.
    ///       Type system(class, character, position etc)
    ///       Disassembly rewrite to use types(meaning Eliwood will appear in disassembled chapters)
    ///       All the remaining language specific things to raws, meaning you can add custom languages
    ///       Make macros give better error codes if macro exists but amount of parameters is right.
    /// </summary>
    public static class Program
    {
        static IDictionary<string, EACodeLanguage> languages;
        static TextWriterMessageLog messageLog;
        static StringComparer stringComparer;
        
        //StdOut - output
        //StdIn - input
        //StdError - errors and messages
        //0 - D, A or Doc, assemble, disassemble or doc generation
        //1 - language (assembly or disassembly only)
        //2 - disassembly mode (disassembly only)
        //3 - offset to disassemble (disassembly only)
        //4 - priority to disassemble (disassembly only)
        //5 - length to disassemble (disassembly only)
        //flags: -addEndGuards
        //       -raws:Folder or file
        //       -rawsExt:extension
        //       -output:File
        //       -input:File
        //       -error:File
        //       -docHeader:File
        //       -docFooter:File
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                return;
            }

            messageLog = new TextWriterMessageLog(Console.Error);
            stringComparer = StringComparer.OrdinalIgnoreCase;
            List<string> flags = new List<string>(args.Length);
            List<string> parameters = new List<string>(args.Length);
            foreach (var item in args)
            {
                if (item.StartsWith("-"))
                    flags.Add(item.TrimStart('-'));
                else
                    parameters.Add(item);
            }

            string rawsFolder = "Language raws";
            string extension = ".txt";
            bool isDirectory = true;
            bool addEndGuards = false;
            string inputFile = null,
                   outputFile = null,
                   errorFile = null,
                   docHeader = null,
                   docFooter = null;

            HandleFlags(flags, ref rawsFolder, ref extension, ref isDirectory, ref addEndGuards,
                ref inputFile, ref outputFile, ref errorFile, ref docHeader, ref docFooter);
            if (errorFile != null)
            {
                StreamWriter writer = new StreamWriter(errorFile);
                messageLog.Writer = writer;
            }


            if (stringComparer.Compare(parameters[0], "doc") == 0)
            {
                MakeDoc(outputFile, rawsFolder, extension, isDirectory, docHeader, docFooter);
            }
            else
            {
                LoadCodes(rawsFolder, extension, isDirectory, false);
                if (languages.ContainsKey(parameters[1]))
                {
                    EACodeLanguage language = languages[parameters[1]];
                    if (stringComparer.Compare(parameters[0], "A") == 0)
                    {
                        Assemble(inputFile, outputFile, language);
                    }
                    else if (stringComparer.Compare(parameters[0], "D") == 0)
                    {
                        DisassemblyMode mode;
                        if (parameters[2].TryGetEnum(out mode))
                        {
                            int offset;
                            if (parameters[3].TryGetValue(out offset))
                            {
                                int size = 0;
                                Priority priority = Priority.none;
                                if (mode != DisassemblyMode.Structure)
                                {
                                    if (!parameters[4].TryGetEnum(out priority))
                                    {
                                        messageLog.AddError(parameters[4] + " is not a valid priority");
                                        goto end;
                                    }
                                }

                                if (mode == DisassemblyMode.Block)
                                {
                                    if (!parameters[5].TryGetValue(out size) || size < 0)
                                    {
                                        messageLog.AddError(parameters[5] + " is not a valid size");
                                        goto end;
                                    }
                                }

                                Disassemble(inputFile, outputFile, language, addEndGuards,
                                    mode, offset, priority, size);


                            }
                            else messageLog.AddError(parameters[3] + " is not a valid number");
                        }
                        else messageLog.AddError(parameters[2] + "is not a valid disassembly mode");
                    }
                    else messageLog.AddError(parameters[0] + "is not a valid action to do");
                }
                else messageLog.AddError(parameters[1] + "is not a valid language");
            }
            end:
            messageLog.PrintAll();
            messageLog.Clear();
        }

        static private CanCauseError<int> Div(this int i, int j)
        {
            Console.WriteLine("Dividing {0} by {1}", i, j);
            if (j == 0)
            {
                return CanCauseError<int>.Error("Divide by zero");
            }
            else
            {
                return i / j;
            }
        }

        private static void Test()
        {
            //string text = "";
            string text =
@"CODE test $eeeeeef eeeeeef 12 15*7 [13,16]
TURN 15 ahhahahahahaaa 0xcc9c8 $efabcdc (-456/dfg)*9+546
WEE tettte
{
{ TEST 5 }
TEST {
TUEST 678 564
} TEST 15 [5,offset] 
}
";
            //string text = "Symbol [,, ]";

            var tokens = Tokeniser.Tokenise(
                new PreprocessingInputStream(
                    new StringReader(text),
                    new NopPreprocessor()));

            Console.WriteLine(text);

            var parser = new TokenParser<int>(x => x.GetValue());
            Match<Token> match;
            var scanner = new EnumerableScanner<Token>(tokens);
            var result = parser.Parse(scanner, out match);
            
            Console.WriteLine(match);
            Console.WriteLine(
                scanner.IsAtEnd || scanner.Current.Type == TokenType.EndOfStream 
                ? "Reached end" 
                : "Didn't reach end, at position " + scanner.Offset + " current token is " + scanner.Current);
            Console.ReadLine();
        }

        private static void Test2()
        {
            string text = "aaabbbaaa";

            var parser = (new AlwaysParser<char>()).Many1();
            Match<char> match;
            var result = parser.Parse(new StringScanner(text), out match);
            result.ToList();

            Console.WriteLine(match);
            Console.ReadLine();
        }

        private static void Test3()
        {
            string text = "4 + 5 * (21 / 7 + 13) - 6";

            var tokens = Tokeniser.Tokenise(
                new PreprocessingInputStream(
                    new StringReader(text),
                    new NopPreprocessor()));
            //var tokens = Tokeniser.TokeniseLine(text, "", 1);
            Console.WriteLine(text);

            var parser = new Nintenlord.Event_Assembler.Core.Code.Language.Parser.MathParser<int>(x => x.GetValue());
            Match<Token> match;
            var result = parser.Parse(new EnumerableScanner<Token>(tokens), out match);

            Console.WriteLine(match);
            if (match.Success)
            {
                Console.WriteLine(Folding.Fold(result));
            }
            Console.ReadLine();
        }

        private static IEnumerable<T> GetTokens<T>(IScanner<T> scanner)
        {
            while (scanner.MoveNext())
            {
                yield return scanner.Current;
            } 
        }


        public static void LoadCodes(string rawsFolder, string extension, bool isDirectory, bool collectDocCodes)
        {
            languages = new Dictionary<string, EACodeLanguage>();
            LanguageProcessor pro = new LanguageProcessor(collectDocCodes, 
                new TemplateComparer(), stringComparer);
            IPointerMaker ptrMaker = new GBAPointerMaker();
            if (isDirectory)
            {
                pro.ProcessCode(rawsFolder, extension);
            }
            else
            {
                pro.ProcessCode(rawsFolder);
            }
            foreach (KeyValuePair<string, ICodeTemplateStorer> item in pro.Languages)
            {
                Tuple<string, List<Priority>>[][] pointerList;

                switch (item.Key)
                {
                    case "FE6":
                        pointerList = FE6CodeLanguage.PointerList;
                        break;
                    case "FE7":
                        pointerList = FE7CodeLanguage.PointerList;
                        break;
                    case "FE8":
                        pointerList = FE8CodeLanguage.PointerList;
                        break;
                    default:
                        throw new NotSupportedException("Language " + item.Key + " not supported.");
                }
                ICodeTemplateStorer storer = item.Value;
                if (item.Key == "FE8")
                {
                    storer.AddCode(new GenericFE8Template(), Priority.none);
                }
                EACodeLanguage language = new EACodeLanguage(
                    item.Key, ptrMaker,
                    pointerList,
                    storer, stringComparer
                    );
                language.MessageLog = messageLog;
                languages[item.Key] = language;
            }

        }

        private static void Assemble(string inputFile, string outputFile, EACodeLanguage language)
        {
            TextReader reader;
            bool close;
            if (inputFile != null)
            {
                reader = File.OpenText(inputFile);
                close = true;
            }
            else
            {
                reader = Console.In;
                close = false;
            }

            if (outputFile != null)
            {
                using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(outputFile)))
                {
                    Assemble(language, reader, writer, messageLog);
                }
            }
            else
            {
                messageLog.AddError("outputFile needs to be specified for assembly.");
            }

            if (close)
                reader.Close();
        }

        private static void Disassemble(string inputFile, string outputFile, EACodeLanguage language,
            bool addEndGuards, DisassemblyMode mode, int offset, Priority priority, int size)
        {
            if (!File.Exists(inputFile))
            {
                messageLog.AddError("File " + inputFile + " doesn't exist.");
                return;
            }
            else if (File.Exists(outputFile))
            {
                if ((File.GetAttributes(outputFile) & FileAttributes.ReadOnly)
                    == FileAttributes.ReadOnly)
                {
                    messageLog.AddError("Output cannot be written to. It is read-only.");
                    return;
                }
            }

            byte[] data = File.ReadAllBytes(inputFile);

            if (offset > data.Length)
            {
                messageLog.AddError("Offset is larger than size of file.");
            }
            else
            {
                if (size <= 0 || size + offset > data.Length)
                {
                    size = data.Length - offset;
                }
                IEnumerable<string[]> code;
                string[] defaultLines;
                switch (mode)
                {
                    case DisassemblyMode.Block:
                        code = language.Disassemble(data, offset, size, priority, addEndGuards);
                        defaultLines = CoreInfo.DefaultLines(language.Name,
                            Path.GetFileName(inputFile), offset, size);
                        break;
                    case DisassemblyMode.ToEnd:
                        code = language.DisassembleToEnd(data, offset, priority, addEndGuards);
                        defaultLines = CoreInfo.DefaultLines(language.Name,
                            Path.GetFileName(inputFile), offset, null);
                        break;
                    case DisassemblyMode.Structure:
                        code = language.DisassembleChapter(data, offset, addEndGuards);
                        defaultLines = CoreInfo.DefaultLines(language.Name,
                            Path.GetFileName(inputFile), offset, null);
                        break;
                    default:
                        throw new ArgumentException();
                }

                using (StreamWriter sw = new StreamWriter(outputFile))
                {
                    sw.WriteLine();
                    sw.WriteLine(Frame(defaultLines, "//", 1));
                    sw.WriteLine();

                    foreach (string[] line in code)
                    {
                        sw.WriteLine(line.ToElementWiseString(" ", "", ""));
                    }
                }
            }
        }


        public static void Assemble(EACodeLanguage language, TextReader input, BinaryWriter output, ILog log)
        {
            List<string> predefined = new List<string>();
            predefined.Add("_" + language.Name + "_");
            predefined.Add("_EA_");
            using (IPreprocessor preprocessor = new Preprocessor(log))
            {
                preprocessor.AddReserved(language.GetCodeNames());
                preprocessor.AddDefined(predefined.ToArray());

                using (IInputStream stream = new PreprocessingInputStream(
                    input, preprocessor))
                {
                    EAExpressionAssembler ass = new EAExpressionAssembler(
                        language.CodeStorage,
                        new TokenParser<int>(StringExtensions.GetValue));
                    ass.Assemble(stream, output, log);
                }
            }
        }

        public static void MakeDoc(string output, string rawsFolder, 
            string extension, bool isDirectory, string header, string footer)
        {
            LanguageProcessor pro = new LanguageProcessor(true,
                new TemplateComparer(), stringComparer);
            IPointerMaker ptrMaker = new GBAPointerMaker();
            if (isDirectory)
            {
                pro.ProcessCode(rawsFolder, extension);
            }
            else
            {
                pro.ProcessCode(rawsFolder);
            }
            using (StreamWriter writer = File.CreateText(output))
            {
                if (header != null)
                {
                    writer.WriteLine(File.ReadAllText(header));
                    writer.WriteLine();
                }

                pro.WriteDocs(writer);

                if (footer != null)
                {
                    writer.WriteLine(File.ReadAllText(footer));
                    writer.WriteLine();
                }
            }
        }


        private static void HandleFlags(List<string> flags, ref string rawsFolder, ref string rawsExtension,
            ref bool isDirectory, ref bool addEndGuards, ref string inputFile, ref string outputFile,
            ref string errorFile, ref string docHeader, ref string docFooter)
        {
            foreach (var flag in flags)
            {
                int index = flag.IndexOf(':');
                string flagName;
                string option;
                if (index >= 0)
                {
                    flagName = flag.Substring(0, index);
                    option = flag.Substring(index + 1);
                }
                else
                {
                    flagName = flag;
                    option = "";
                }

                switch (flagName)
                {
                    case "addEndGuards":
                        addEndGuards = true;
                        break;
                    case "raws":
                        if (File.Exists(option))
                        {
                            rawsFolder = option;
                            isDirectory = false;
                        }
                        else if (Directory.Exists(option))
                        {
                            rawsFolder = option;
                            isDirectory = true;
                        }
                        else
                        {
                            messageLog.AddError("File or folder " + option + " doesn't exist.");
                        }
                        break;
                    case "rawsExt":
                        if (!option.ContainsAnyOf(Path.GetInvalidFileNameChars()))
                        {
                            rawsExtension = option;
                        }
                        else
                        {
                            messageLog.AddError("Extension " + option + " is not valid.");
                        }
                        break;
                    case "input":
                        if (File.Exists(option))
                        {
                            inputFile = option;
                        }
                        else
                        {
                            messageLog.AddError("File " + option + " doesn't exist.");
                        }
                        break;
                    case "output":
                        if (IsValidFileName(option))
                        {
                            outputFile = option;
                        }
                        else
                        {
                            messageLog.AddError("Name " + option + " isn't valid for a file.");
                        }
                        break;
                    case "error":
                        if (IsValidFileName(option))
                        {
                            errorFile = option;
                        }
                        else
                        {
                            messageLog.AddError("Name " + option + " isn't valid for a file.");
                        }
                        break;
                    case "docHeader":
                        if (IsValidFileName(option))
                        {
                            docHeader = option;
                        }
                        else
                        {
                            messageLog.AddError("Name " + option + " isn't valid for a file.");
                        }
                        break;
                    case "docFooter":
                        if (IsValidFileName(option))
                        {
                            docFooter = option;
                        }
                        else
                        {
                            messageLog.AddError("Name " + option + " isn't valid for a file.");
                        }
                        break;
                    default:
                        messageLog.AddError("Flag " + flagName + " doesn't exist.");
                        break;
                }
            }
        }

        private static bool IsValidFileName(string name)
        {
            return true;
        }

        static string Frame(string[] lines, string toFrameWith, int padding)
        {
            int longestLine = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                longestLine = Math.Max(longestLine, lines[i].Length);
            }
            string frame = toFrameWith.Repeat(padding * 2 + toFrameWith.Length * 2 + longestLine);
            string frame2 = toFrameWith + " ".Repeat(padding * 2 + longestLine) + toFrameWith;
            string paddingText = " ".Repeat(padding);

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(frame);
            builder.AppendLine(frame2);

            foreach (string line in lines)
            {
                builder.AppendLine(toFrameWith +
                    paddingText +
                    line.PadRight(longestLine, ' ') +
                    paddingText +
                    toFrameWith
                    );
            }

            builder.AppendLine(frame2);
            builder.AppendLine(frame);
            return builder.ToString();
        }
    }
}
