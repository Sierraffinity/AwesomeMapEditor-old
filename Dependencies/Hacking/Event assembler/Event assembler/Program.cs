using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Nintenlord.Event_Assembler.Core;
using Nintenlord.Event_Assembler.Core.Code;
using Nintenlord.Event_Assembler.Core.Code.Language;
using Nintenlord.Event_Assembler.Core.Code.Preprocessors;
using Nintenlord.Event_Assembler.Core.Code.Templates;
using Nintenlord.Event_Assembler.Core.GBA;
using Nintenlord.Event_Assembler.Core.IO.Logs;
using Nintenlord.Event_Assembler.UserInterface;
using Nintenlord.Collections;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler
{
    static class Program
    {
        static IDictionary<String, EACodeLanguage> languages;
        static ILog messageLog;
        static StringComparer stringComparer;

        [STAThread]
        static void Main(string[] args)
        {
            stringComparer = StringComparer.OrdinalIgnoreCase;
            messageLog = new GUIMessageLog();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(args));
        }

        public static void LoadCodes()
        {
            if (languages != null)
            {
                languages.Clear();
            }
            else
            {
                languages = new Dictionary<String, EACodeLanguage>();
            }

            LanguageProcessor processor = new LanguageProcessor(false,
                new TemplateComparer(), stringComparer);
            processor.ProcessCode("Language raws", ".txt");

            IPointerMaker ptrMaker = new GBAPointerMaker();

            foreach (KeyValuePair<string, ICodeTemplateStorer> item in processor.Languages)
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
                EACodeLanguage language = new EACodeLanguage(
                    item.Key, ptrMaker,
                    pointerList,
                    item.Value, stringComparer
                    );
                language.MessageLog = messageLog;
                languages[item.Key] = language;
            }
        }

        public static bool CodesLoaded
        {
            get { return languages != null; }
        }

        public static void Assemble(string originalFile, string outputFile, string game)
        {
            if (File.Exists(outputFile))
            {
                if ((File.GetAttributes(outputFile) & FileAttributes.ReadOnly)
                    == FileAttributes.ReadOnly)
                {
                    messageLog.AddError("Output cannot be written to. It is read-only.");
                    goto end;
                }
            }
            EACodeLanguage language = languages[game];

            using (StreamReader reader = File.OpenText(originalFile))
            {
                using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(outputFile)))
                {
                    Core.Program.Assemble(language, reader, writer, messageLog);                            
                }
            }

            end:
            messageLog.PrintAll();
            messageLog.Clear();
        }
        
        public static void Preprocess(string originalFile, string outputFile, string game)
        {
            EACodeLanguage language = languages[game];

            List<string> predefined = new List<string>();
            predefined.Add("_" + game + "_");
            predefined.Add("_EA_");
            predefined.AddRange(language.GetCodeNames());

            IPreprocessor preprocessor = new Preprocessor(messageLog);
            preprocessor.AddReserved(language.GetCodeNames());
            preprocessor.AddDefined(predefined.ToArray());

            string code = null;//preprocessor.Process(originalFile);

            //File.WriteAllText(outputFile, code);
            messageLog.AddMessage("Processed code:\n" + code + "\nEnd processed code");

            messageLog.PrintAll();
            messageLog.Clear();
        }

        public static void Disassemble(string originalFile, string outputFile, string game,
            int offset, int size, DisassemblyMode mode, bool addEndingLines)
        {
            if (!File.Exists(originalFile))
            {
                messageLog.AddError("File " + originalFile + " doesn't exist.");
                return;
            }
            if (File.Exists(outputFile))
            {
                if ((File.GetAttributes(outputFile) & FileAttributes.ReadOnly)
                    == FileAttributes.ReadOnly)
                {
                    messageLog.AddError("Output cannot be written to. It is read-only.");
                    return;
                }
            }

            EACodeLanguage language = languages[game];
            byte[] data = File.ReadAllBytes(originalFile);

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
                        Priority priority;
                        if (GetPriority(out priority))
                        {
                            code = language.Disassemble(data, offset, size, priority, addEndingLines);
                            defaultLines = CoreInfo.DefaultLines(game, Path.GetFileName(originalFile), offset, size);
                        }
                        else goto end;
                        break;
                    case DisassemblyMode.ToEnd:
                        if (GetPriority(out priority))
                        {
                            code = language.DisassembleToEnd(data, offset, priority, addEndingLines);
                            defaultLines = CoreInfo.DefaultLines(game, Path.GetFileName(originalFile), offset, null);
                        }
                        else goto end;
                        break;
                    case DisassemblyMode.Structure:
                        code = language.DisassembleChapter(data, offset, addEndingLines);
                        defaultLines = CoreInfo.DefaultLines(game, Path.GetFileName(originalFile), offset, null);
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
            messageLog.PrintAll();
            end:
            messageLog.Clear();
        }

        private static bool GetPriority(out Priority priority)
        {
            Priority[] nonValidOptions = 
            {
                Priority.ASM,
                Priority.unknown
            };
            bool result;
            using (EnumChooserForm chooser = new EnumChooserForm())
            {
                chooser.SetEnumType(typeof(Priority));
                chooser.Text = "Choose an option";
                chooser.Description = "Just choose \"none\" if you don't know what to choose.";
                foreach (Priority item in nonValidOptions)
                {
                    chooser.SetEnumEnabled(item, false);
                }
                result = chooser.ShowDialog() == DialogResult.OK;
                priority = (Priority)chooser.GetChosenEnum();                
            }
            ;
            return result;
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