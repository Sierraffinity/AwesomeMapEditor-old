using System;
using System.Collections.Generic;
using System.Linq;
using Nintenlord.Event_Assembler.Core.Code.Templates;
using Nintenlord.Event_Assembler.Core.IO.Logs;
using Nintenlord.Collections;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Code.Language
{
    /// <summary>
    /// To be generealized.
    /// </summary>
    sealed class EACodeLanguageDisassembler
    {
        private const string offsetChanger = "ORG";
        private const string currentOffset = "CURRENTOFFSET";
        private const string messagePrinter = "MESSAGE";

        readonly ICodeTemplateStorer codeStorage;
        readonly IPointerMaker pointerMaker;
        readonly Tuple<string, List<Priority>>[][] pointerList;
        //readonly int longestTemplate;

        public bool AddEndingLines
        {
            get { return addEndingLines; }
            set { addEndingLines = value; }
        }

        public EACodeLanguageDisassembler(
            ICodeTemplateStorer codeStorage,
            IPointerMaker pointerMaker,
            Tuple<string, List<Priority>>[][] pointerList)
        {
            this.codeStorage = codeStorage;
            this.pointerMaker = pointerMaker;
            this.pointerList = pointerList;

            //longestTemplate = (from template in codeStorage
            //                   where template is CodeTemplate
            //                   select (template as CodeTemplate).Length).Max();
        }

        bool addEndingLines;
        public IEnumerable<string[]> Disassemble(byte[] code, int offset, int length, Priority priority, ILog log, bool addEndingLines)
        {
            //this.log = log;
            string[] emptyLine = new string[0];
            SortedDictionary<int, Code> lines = new SortedDictionary<int, Code>();

            ICollection<Priority> priorities = new List<Priority>();
            priorities.Add(priority);
            priorities.Add(Priority.low);

            FindTemplates(code, offset, length, lines, priorities, log);

            SortedDictionary<int, string> labels = new SortedDictionary<int, string>();
            FindLables(lines, labels);

            MergeRepeatableCodes(lines, x => !labels.ContainsKey(x));
            
            return GetLines(lines, labels, addEndingLines);
        }

        public IEnumerable<string[]> DisassembleChapter(byte[] code, int offset, ILog log, bool addEndingLines)
        {
            List<int> pointerlistValues = new List<int>();
            SortedDictionary<int, Code> lines = new SortedDictionary<int, Code>();
            SortedDictionary<int, string> labels = new SortedDictionary<int, string>();
            labels[offset] = "PointerList";
            
            foreach (var item in this.pointerList.Flatten().Index())
            {
                int pointerOffset = offset + 4 * item.Item1;
                int pointer = BitConverter.ToInt32(code, pointerOffset);
                if (this.pointerMaker.IsAValidPointer(pointer))
                {
                    int offsetVal = this.pointerMaker.MakeOffset(pointer);
                    pointerlistValues.Add(offsetVal);
                    if (offsetVal > 0 && !labels.ContainsKey(offsetVal))
                    {
                        labels.Add(offsetVal, item.Item2.Item1);
                        FindTemplatesUntil(code, offsetVal, lines, 
                            item.Item2.Item2, x => x.EndingCode, log);
                    }
                }
                else
                {
                    log.AddError("Invalid pointer at pointer list: " + pointer.ToHexString("$"));
                    return Enumerable.Empty<string[]>();
                }
            }

            FindPointedCodes(code, lines, log);

            FindLables(lines, labels);

            MergeRepeatableCodes(lines, x => !labels.ContainsKey(x));//Can cause labels to get omitted, needs to be fixed.

            //After merging because I want custom format
            AddPointerListCodes(offset, pointerlistValues.ToArray(), lines, log);

            KeyValuePair<int, Code> last = lines.Last();

            int length = last.Key + last.Value.Length - offset;

            return GetLines(lines, labels, addEndingLines);
        }

        public IEnumerable<string[]> DisassembleToEnd(byte[] code, int offset, Priority priority, ILog log, bool addEndingLines)
        {
            string[] emptyLine = new string[0];
            SortedDictionary<int, Code> lines = new SortedDictionary<int, Code>();

            ICollection<Priority> priorities = new List<Priority>();
            priorities.Add(priority);
            priorities.Add(Priority.low);

            FindTemplatesUntil(code, offset, lines, priorities, x => x.EndingCode, log);

            SortedDictionary<int, string> labels = new SortedDictionary<int, string>();
            FindLables(lines, labels);

            MergeRepeatableCodes(lines, x => !labels.ContainsKey(x));

            return GetLines(lines, labels, addEndingLines);
        }


        private string[][] GetEnderLines(int endingOffset)
        {
            return new string[][]
            {
             "//The next line is for re-assembling purposes. Do not delete!".GetArray(),
             new string[]{ messagePrinter,   "Original ending offset is " 
                + endingOffset.ToHexString("$")
                + " and the new ending offset is", currentOffset},

            };
        }


        private IEnumerable<string[]> GetLines(IEnumerable<KeyValuePair<int, Code>> lines, IDictionary<int, string> lables,
            bool addEndingMessages)
        {
            string[] emptyLine = new string[0];
            bool addedLine = false;
            bool enderLineAdded = false;
            int latestOffset = 0;

            foreach (var line in lines)
            {
                int currentOffset = line.Key;
                Code code = line.Value;
                if (line.Key != latestOffset)
                {
                    if (addEndingMessages && !enderLineAdded && latestOffset > 0)
                    {
                        if (!addedLine) yield return emptyLine;
                        addedLine = true;
                        foreach (var item in GetEnderLines(latestOffset))
                        {
                            yield return item;
                        }
                        yield return emptyLine;
                        enderLineAdded = true;
                    }
                    if (!addedLine) yield return emptyLine;
                    addedLine = true;
                    yield return new string[] { offsetChanger, currentOffset.ToHexString("$") };
                }
                string labelName;
                if (lables.TryGetValue(currentOffset, out labelName))
                {
                    if (!addedLine) yield return emptyLine;
                    addedLine = true;
                    yield return (labelName + ":").GetArray();
                }
                enderLineAdded = false;
                yield return code.ReplaceOffsetsWithLables(lables);

                if (code.Template.EndingCode)
                {
                    yield return emptyLine;
                    addedLine = true;
                }
                else addedLine = false;
                latestOffset = currentOffset + code.Length;
            }

            if (addEndingMessages && !enderLineAdded)
            {
                if (!addedLine) yield return emptyLine;
                foreach (var item in GetEnderLines(latestOffset))
                {
                    yield return item;
                }
            }
        }

        private void AddPointerListCodes(int offset, int[] pointerList,
            SortedDictionary<int, Code> lines, ILog log)
        {
            var pointerTemplateResult = codeStorage.FindTemplate("POIN", Priority.pointer);
            if (pointerTemplateResult.CausedError)
            {
                log.AddError(pointerTemplateResult.ErrorMessage);
            }
            else
            {
                var pointerTemplate = pointerTemplateResult.Result;
                int totalIndex = 0;
                for (int i = 0; i < this.pointerList.Length; i++)
                {
                    List<string> line = new List<string>();
                    line.Add(pointerTemplate.Name);
                    int thisOffset = offset + 4 * totalIndex;
                    for (int j = 0; j < this.pointerList[i].Length; j++)
                    {
                        line.Add(pointerList[totalIndex].ToHexString("$"));
                        totalIndex++;
                    }
                    lines[thisOffset] = new Code(line.ToArray(), pointerTemplate, this.pointerList[i].Length * 4);
                }
            }
        }

        private void FindLables(IDictionary<int, Code> lines,
            IDictionary<int, string> lables)
        {
            foreach (KeyValuePair<int, Code> line in lines)
            {
                foreach (var item in line.Value)
                {
                    int offset = item.Item1;
                    bool hasLine = lines.ContainsKey(offset);
                    bool noLable = !lables.ContainsKey(offset);
                    if (hasLine && noLable)
                    {
                        lables.Add(offset, "label" + (lables.Count + 1));
                    }

                }
            }
        }

        private void FindTemplates(byte[] code, int offset, int length,
            SortedDictionary<int, Code> lines, IEnumerable<Priority> prioritiesToUse, ILog log)
        {
            int currOffset = offset;
            while (currOffset - length < offset)
            {
                Code ccode;
                ICodeTemplate template = null;
                int lengthCode;
                if (!lines.TryGetValue(currOffset, out ccode))
                {
                    var templateResult = codeStorage.FindTemplate(code, currOffset, prioritiesToUse);
                    if (templateResult.CausedError)
                    {
                        log.AddError(templateResult.ErrorMessage);
                        break;
                    }
                    else
                    {
                        template = templateResult.Result;
                        lengthCode = template.GetLengthBytes(code, currOffset);
                        var line = template.GetAssembly(code, currOffset);
                        if (line.CausedError)
                        {
                            log.AddError(line.ErrorMessage);
                        }
                        else
                        {
                            lines.Add(currOffset, new Code(line.Result, template, lengthCode));
                        }
                    }
                }
                else
                {
                    lengthCode = ccode.Template.GetLengthBytes(code, currOffset); 
                }
                currOffset += lengthCode;
            }
        }

        private void FindTemplatesUntil(byte[] code, int offset,
            SortedDictionary<int, Code> lines, IEnumerable<Priority> prioritiesToUse,
            Predicate<ICodeTemplate> predicate, ILog log)
        {
            int currOffset = offset;
            while (currOffset < code.Length)
            {
                Code ccode;
                ICodeTemplate template;
                int length;
                if (!lines.TryGetValue(currOffset, out ccode))
                {
                    var templateResult = codeStorage.FindTemplate(code, currOffset, prioritiesToUse);
                    if (templateResult.CausedError)
                    {
                        log.AddError(templateResult.ErrorMessage);
                        break;
                    }
                    else
                    {
                        template = templateResult.Result;
                        length = template.GetLengthBytes(code, currOffset);
                        var line = template.GetAssembly(code, currOffset);
                        if (line.CausedError)
                        {
                            log.AddError(line.ErrorMessage);
                        }
                        else
                        {
                            lines.Add(currOffset, new Code(line.Result, template, length));
                        }
                    }
                }
                else
                {
                    template = ccode.Template;
                    length = template.GetLengthBytes(code, currOffset); 
                }
                if (predicate(template))
                {
                    break;
                }
                currOffset += length;
            }
        }

        private void FindPointedCodes(byte[] code, SortedDictionary<int, Code> lines, ILog log)
        {
            SortedDictionary<int, Priority> pointerOffsets = new SortedDictionary<int, Priority>();
            foreach (KeyValuePair<int, Code> line in lines)
            {
                foreach (var item in line.Value)
                {
                    if( !(pointerOffsets.ContainsKey(item.Item1) ||
                        lines.ContainsKey(item.Item1)))
                    {
                        pointerOffsets.Add(item.Item1, item.Item2);
                    }
                }                
            }

            Priority[] usedPriorities = new Priority[2];
            usedPriorities[0] = Priority.none;
            usedPriorities[1] = Priority.low;
            var offsetsToHandle = from pointerOffset in pointerOffsets
                                  where pointerOffset.Key >= 0x100000 
                                     && pointerOffset.Key < code.Length
                                     && HandlePriority(pointerOffset.Value)
                                     && !lines.ContainsKey(pointerOffset.Key)
                                  select pointerOffset;

            int handledCount = 0;
            foreach (KeyValuePair<int, Priority> item in offsetsToHandle)
            {
                usedPriorities[0] = item.Value;
                FindTemplatesUntil(code, item.Key, lines, usedPriorities, x => x.EndingCode, log);
                handledCount++;
            }

            if (handledCount > 0)
            {
                FindPointedCodes(code, lines, log);
            }
        }

        private bool HandlePriority(Priority priority)
        {
            return priority != Priority.pointer
                && priority != Priority.unknown
                && priority != Priority.ASM
                && priority != Priority.reinforcementData;
        }

        private void MergeRepeatableCodes(SortedDictionary<int, Code> lines, Predicate<int> isAllowed)
        {
            int[] codeOffsets = lines.Keys.ToArray();
            for (int i = 0; i < codeOffsets.Length; i++)
            {
                int currentOffset = codeOffsets[i];
                Code line = lines[currentOffset];
                int maxRepetition = line.Template.MaxRepetition;
                if (maxRepetition > 1)
                {
                    int tempOffset = currentOffset + line.Length;
                    List<Code> toJoin = new List<Code>();

                    while (lines.ContainsKey(tempOffset) &&
                        isAllowed(tempOffset) &&
                        toJoin.Count < maxRepetition - 1 &&
                        lines[tempOffset] == line)
                    {
                        Code lineToJoin = lines[tempOffset];
                        toJoin.Add(lineToJoin);
                        lines.Remove(tempOffset);
                        tempOffset += lineToJoin.Length;
                    }

                    if (toJoin.Count > 0)
                    {
                        List<string> newText = new List<string>((toJoin.Count + 1) *
                                                                (line.Text.Length - 1) + 1);
                        newText.AddRange(line.Text);
                        int length = 0;
                        foreach (Code codeToJoin in toJoin)
                        {
                            length += codeToJoin.Length; 
                                 
                            for (int j = 1; j < codeToJoin.Text.Length; j++)
                            {
                                newText.Add(codeToJoin.Text[j]);
                            }
                        }
                        line = new Code(newText.ToArray(), line.Template, length);
                        lines[currentOffset] = line;
                    }
                    i += toJoin.Count;
                }
            }
        }
    }
}
