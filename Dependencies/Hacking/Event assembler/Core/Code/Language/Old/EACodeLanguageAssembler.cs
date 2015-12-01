using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nintenlord.Event_Assembler.Core.Code.Language.BuiltInCodes;
using Nintenlord.Event_Assembler.Core.Code.Templates;
using Nintenlord.Event_Assembler.Core.IO.Input;
using Nintenlord.Event_Assembler.Core.IO.Logs;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Code.Language
{
    class EACodeLanguageAssembler
    {
        #region Built-in codes

        public static readonly string currentOffsetCode = "CURRENTOFFSET";
        private static readonly string messagePrinterCode = "MESSAGE";
        private static readonly string errorPrinterCode = "ERROR";
        private static readonly string warningPrinterCode = "WARNING";

        private void SetBuiltInCodes(StringComparer stringComparer)
        {
            ScopeStarter scopeStarter = new ScopeStarter();
            ScopeEnder scopeEnder = new ScopeEnder();
            OffsetChanger offsetChanger = new OffsetChanger();
            OffsetAligner offsetAligner = new OffsetAligner();
            Printer messagePrinter = new Printer(messagePrinterCode);
            Printer errorPrinter = new Printer(errorPrinterCode);
            Printer warningPrinter = new Printer(warningPrinterCode);

            buildInCodes = new Dictionary<string, IBuiltInCode>(stringComparer);
            buildInCodes[scopeStarter.Name] = scopeStarter;
            buildInCodes[scopeEnder.Name] = scopeEnder;
            buildInCodes[offsetChanger.Name] = offsetChanger;
            buildInCodes[offsetAligner.Name] = offsetAligner;
            buildInCodes[messagePrinter.Name] = messagePrinter;
            buildInCodes[errorPrinter.Name] = errorPrinter;
            buildInCodes[warningPrinter.Name] = warningPrinter;
        }
        IDictionary<string, IBuiltInCode> buildInCodes;
        
        #endregion

        ICodeTemplateStorer codeStorage;
        IEnumerable<string> reservedWords;

        public EACodeLanguageAssembler(
            ICodeTemplateStorer codeStorage, 
            IEnumerable<string> reservedWords,
            StringComparer stringComparer)
        {
            this.codeStorage = codeStorage;
            this.reservedWords = reservedWords;
            SetBuiltInCodes(stringComparer);
        }

        public void Assemble(IPositionableInputStream input, BinaryWriter output, ILog log)
        {
            ((Printer)buildInCodes[messagePrinterCode]).PrinterAction = log.AddMessage;
            ((Printer)buildInCodes[errorPrinterCode]).PrinterAction = log.AddError;
            ((Printer)buildInCodes[warningPrinterCode]).PrinterAction = log.AddWarning;

            Context assemblyContext = new Context();
            assemblyContext.AddNewScope();
            var codes = new List<KeyValuePair<INamed<string>, string[]>>(FirstPass(input, assemblyContext, log));

            assemblyContext.Offset = 0;
            SecondPass(codes, assemblyContext, log, output);
        }

        private IEnumerable<KeyValuePair<INamed<string>, string[]>> FirstPass(IPositionableInputStream input, 
            Context assemblyContext, ILog log)
        {
            while (true)
            {
                string line = input.ReadLine();
                if (line == null)
                    break;
                
                string[] code = Nintenlord.Utility.Parser.SplitToParameters(line);

                if (code.Length > 0)
                {
                    if (code[0].EndsWith(":"))
                    {
                        code = HandleLabels(input, assemblyContext, log, code);
                    }

                    if (code.Length == 0) continue;

                    IBuiltInCode builtIn;
                    if (buildInCodes.TryGetValue(code[0], out builtIn))
                    {
                        string error;
                        if (builtIn.Matches("Code " + code[0], code.Length - 1, out error))
                        {
                            var causedError = builtIn.FirstPass(code, assemblyContext);
                            if (causedError)
                            {
                                log.AddError(input.GetErrorString(causedError.ErrorMessage));
                            }
                            yield return new KeyValuePair<INamed<string>, string[]>(builtIn, code);
                        }
                        else
                        {
                            log.AddError(input.GetErrorString(error));
                        }
                    }
                    else
                    {
                        ICodeTemplate template = codeStorage.FindTemplate(code);

                        if (template != null)
                        {
                            if (assemblyContext.Offset % template.OffsetMod != 0)
                            {
                                log.AddError(input.GetErrorString(
                                    string.Format(
                                    "Code {0}'s offset {1} is not divisible by {2}",
                                    template.Name,
                                    assemblyContext.Offset,
                                    template.OffsetMod
                                    )));
                            }
                            assemblyContext.Offset += template.GetLengthBytes(code);
                            yield return new KeyValuePair<INamed<string>, string[]>(template, code);
                        }
                        else
                        {
                            log.AddError(input.GetErrorString(string.Format(
                                    "No code named {0} with {1} parameters found",
                                    code[0],
                                    code.Length - 1
                                    )));
                        }
                    }
                }
            }
        }

        private string[] HandleLabels(IPositionableInputStream input, Context assemblyContext, ILog log, string[] code)
        {
            string labelName = code[0].TrimEnd(':');
            if (IsValidLableName(labelName))
            {
                assemblyContext.AddLabel(labelName);
            }
            else
            {
                log.AddError(input.GetErrorString(
                    string.Format("Invalid label name {0}", labelName)));
            }
            string[] temp = new string[code.Length - 1];
            Array.Copy(code, 1, temp, 0, temp.Length);
            code = temp;
            return code;
        }

        private static void SecondPass(IEnumerable<KeyValuePair<INamed<string>, string[]>> codes, 
            Context assemblyContext, ILog log, BinaryWriter output)
        {
            foreach (var code in codes)
            {
                //Insert labels and currentOffsetCode
                for (int i = 1; i < code.Value.Length; i++)
                {
                    int offset;
                    if (code.Value[i].Equals(currentOffsetCode, StringComparison.OrdinalIgnoreCase))
                    {
                        code.Value[i] = assemblyContext.Offset.ToString();
                    }
                    else if (assemblyContext.TryGetLabelOffset(code.Value[i], out offset))
                    {
                        code.Value[i] = offset.ToString();
                    }
                }

                if (code.Key is IBuiltInCode)
                {
                    var error = ((IBuiltInCode)code.Key).SecondPass(code.Value, assemblyContext);
                    if (error)
                    {
                        log.AddError(error.ErrorMessage);
                    }
                    else if (error.Result)
                    {
                        output.Seek(assemblyContext.Offset, SeekOrigin.Begin);
                    }
                }
                else if (code.Key is ICodeTemplate)
                {
                    output.Write(((ICodeTemplate)code.Key).GetData(code.Value, log));
                    assemblyContext.Offset = (int)output.BaseStream.Position;
                }
            }
        }
                

        /// <summary>
        /// Checks if code should be undefinable. Do not raise errors based on this.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool IsReserved(string word)
        {
            if (codeStorage.IsUsedName(word))
            {
                return true;
            }
            foreach (string item in reservedWords)
            {
                if (item.Equals(word))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsValidLableName(string label)
        {
            return !this.IsReserved(label) &&
                label.All(x => char.IsLetterOrDigit(x) | x == '_') &&
                label.Any(x => char.IsLetter(x));
        }        
    }
}