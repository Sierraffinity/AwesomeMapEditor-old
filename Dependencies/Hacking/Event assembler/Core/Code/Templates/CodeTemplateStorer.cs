using System;
using System.Collections.Generic;
using System.Linq;
using Nintenlord.Event_Assembler.Core.Code.Language;
using Nintenlord.Event_Assembler.Core.IO.Input;
using Nintenlord.Collections;
using Nintenlord.Utility;
using EAType = Nintenlord.Event_Assembler.Core.Code.Language.Types.Type;

namespace Nintenlord.Event_Assembler.Core.Code.Templates
{
    public class CodeTemplateStorer : ICodeTemplateStorer, IEnumerable<KeyValuePair<Priority, ICodeTemplate>>
    {
        IDictionary<string, List<ICodeTemplate>> assemblyCodes;
        IDictionary<KeyValuePair<Priority, int>, List<ICodeTemplate>> disassemblyCodes;
        ICollection<ICodeTemplate> codes;
        IComparer<ICodeTemplate> templateComparer;

        public CodeTemplateStorer(IComparer<ICodeTemplate> templateComparer)
            :this(templateComparer, StringComparer.OrdinalIgnoreCase) { }

        public CodeTemplateStorer(IComparer<ICodeTemplate> templateComparer, IEqualityComparer<string> nameComparer)
        {
            this.templateComparer = templateComparer;
            this.assemblyCodes = new Dictionary<string, List<ICodeTemplate>>(nameComparer);
            this.disassemblyCodes = new Dictionary<KeyValuePair<Priority, int>, List<ICodeTemplate>>();
            this.codes = new List<ICodeTemplate>();
        }

        #region ICodeTemplateStorer Members

        public void AddCode(ICodeTemplate code, Priority priority)
        {
            assemblyCodes.GetOldOrSetNew(code.Name).Add(code);
            disassemblyCodes.GetOldOrSetNew(
                new KeyValuePair<Priority, int>(priority, GetID(code))).Add(code);
            codes.Add(code);
        }

        public CanCauseError<ICodeTemplate> FindTemplate(string name, Priority priority)
        {
            var codes = assemblyCodes[name];
            foreach (var item in disassemblyCodes)
            {
                if (item.Key.Key == priority)
                {
                    foreach (var template in item.Value)
                    {
                        if (codes.Contains(template))
                        {
                            return CanCauseError<ICodeTemplate>.NoError(template);
                        }
                    }
                }
            }
            return CanCauseError<ICodeTemplate>.Error("No code named {0} found in priority {1}", name, priority);
        }
        
        public CanCauseError<ICodeTemplate> FindTemplate(byte[] code, int index, 
            IEnumerable<Priority> allowedPriorities)
        {
            List<ICodeTemplate> codes;
            int id = code[index] + code[index + 1] * 0x100; 
            foreach (var priority in allowedPriorities)
            {
                if (disassemblyCodes.TryGetValue(new KeyValuePair<Priority, int>(priority, id), out codes))
                {
                    List<ICodeTemplate> matchingCodes = new List<ICodeTemplate>();
                    foreach (var template in codes)
                    {
                        if (template.Matches(code, index))
                        {
                            matchingCodes.Add(template);
                        }
                    }
                    if (matchingCodes.Count > 0)
                    {
                        if (matchingCodes.Count > 1)
                        {
                            matchingCodes.Sort(templateComparer);
                        }
                        return CanCauseError<ICodeTemplate>.NoError(matchingCodes.Last());
                    }
                }
                else if (id != 0 && disassemblyCodes.TryGetValue(new KeyValuePair<Priority, int>(priority, 0), out codes))
                {
                    List<ICodeTemplate> matchingCodes = new List<ICodeTemplate>();
                    foreach (var template in codes)
                    {
                        if (template.Matches(code, index))
                        {
                            matchingCodes.Add(template);
                        }
                    }
                    if (matchingCodes.Count > 0)
                    {
                        matchingCodes.Sort(templateComparer);
                        return CanCauseError<ICodeTemplate>.NoError(matchingCodes.Last());
                    }
                }

            }
            return CanCauseError<ICodeTemplate>.Error("No code found.");
        }

        public CanCauseError<ICodeTemplate> FindTemplate(IInputByteStream reader, IEnumerable<Priority> allowedPriorities)
        {
            throw new NotImplementedException();
            //ICodeTemplate result = null;
            //foreach (var priority in allowedPriorities)
            //{
            //    var codes = disassemblyCodes[priority];
            //    foreach (var code in codes)
            //    {

            //    }
            //}
            //return result;
        }

        public CanCauseError<ICodeTemplate> FindTemplate(string codeName, EAType[] parameterTypes)
        {
            List<ICodeTemplate> templates;
            if (assemblyCodes.TryGetValue(codeName, out templates))
            {
                var sortedTemplates = (from template in templates
                                       where template.Matches(parameterTypes)
                                       select template)
                                      .OrderByDescending(x => x, templateComparer);

                if (sortedTemplates.Any())
                {
                    return CanCauseError<ICodeTemplate>.NoError(sortedTemplates.First());
                }
                else
                {
                    string format;
                    if (parameterTypes.Length == 0)
                    {
                        format = "Incorrect parameters in code {0}";
                    }
                    else
                    {
                        format = "Incorrect parameters in code {0} {1}";
                    }
                    return CanCauseError<ICodeTemplate>.Error(
                        format, 
                        codeName, 
                        parameterTypes.ToElementWiseString(" ", " ", ""));
                }                
            }
            else
            {
                return CanCauseError<ICodeTemplate>.Error("No code named {0} found.", codeName);
            }
        }
        
        public string[] GetNames()
        {
            return assemblyCodes.Keys.ToArray();
        }

        public bool IsUsedName(string name)
        {
            return assemblyCodes.ContainsKey(name);
        }

        #endregion

        #region IEnumerable<ICodeTemplate> Members

        public IEnumerator<ICodeTemplate> GetEnumerator()
        {
            return codes.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IEnumerable<KeyValuePair<Priority,ICodeTemplate>> Members

        IEnumerator<KeyValuePair<Priority, ICodeTemplate>> IEnumerable<KeyValuePair<Priority, ICodeTemplate>>.GetEnumerator()
        {
            foreach (var item in disassemblyCodes)
            {
                foreach (var item2 in item.Value)
                {
                    yield return new KeyValuePair<Priority, ICodeTemplate>(
                        item.Key.Key, item2);
                }
            }
        }

        #endregion

        private static int GetID(ICodeTemplate code)
        {
            int id = 0;
            if (code is INamed<int>)
            {
                id = (code as INamed<int>).Name;
            }
            return id;
        }
    }
}
