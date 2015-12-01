using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Event_Assembler.Core.Collections;

using Nintenlord.Event_Assembler.Core.Code.Preprocessors;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Code.StringReplacers
{
    class OriginalReplacer : IStringReplacer
    {
        private IDictionary<string, IDictionary<int, IMacro>> values;
        private IDictionary<string, IMacro> builtInValues;
        private int maxIter;
        
        public IDictionary<string, IDictionary<int, IMacro>> Values
        {
            set { values = value; }
        }
        public IDictionary<string, IMacro> BuiltInValues
        {
            set { builtInValues = value; }
        }
        public int MaxIter
        {
            set { maxIter = value; }
        }


        public bool Replace(string s, out string newString)
        {
            StringBuilder builder = new StringBuilder(s);

            IDictionary<int, string> containedOriginals = new SortedDictionary<int, string>(
                new ReverseComparer<int>(Comparer<int>.Default));

            int iter = 0;
            while (GetContainedOriginals(builder.ToString(), containedOriginals) > 0 && iter < maxIter)
            {
                foreach (var item in containedOriginals)
                {
                    string[] parameters;
                    string name = item.Value;
                    int paramStart = item.Key + name.Length;

                    string paramString;
                    //Get parameters
                    if (paramStart < builder.Length && builder[paramStart] == '(')
                    {
                        int depth = 1;
                        int endIndex = paramStart + 1;
                        while (endIndex < s.Length)
                        {
                            if (builder[endIndex] == ')')
                                depth--;
                            else if (builder[endIndex] == '(')
                                depth++;

                            if (depth == 0)
                                break;

                            endIndex++;
                        }
                        parameters = builder.ToString(paramStart + 1, endIndex - paramStart - 1).Split(',');
                        paramString = builder.ToString(paramStart, endIndex - paramStart + 1);

                    }
                    else
                    {
                        parameters = new string[0];
                        paramString = "";
                    }

                    for (int j = 0; j < parameters.Length; j++)
                    {
                        parameters[j] = parameters[j].Trim();
                    }

                    //Find correct macro
                    IMacro replacer;
                    if (!builtInValues.TryGetValue(name, out replacer))
                    {
                        IDictionary<int, IMacro> dic = values[name];
                        if (!dic.TryGetValue(parameters.Length, out replacer))
                            continue;
                    }

                    string toReplace = name + paramString;
                    string replaceWith = replacer.Replace(parameters);

                    builder.Replace(toReplace, replaceWith, item.Key, toReplace.Length);
                }
                containedOriginals.Clear();
                iter++;
            }
            newString = builder.ToString();
            return containedOriginals.Count == 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="containedOriginals"></param>
        /// <returns></returns>
        /// <remarks>Surpricingly, the fastest way I've managed to do this.
        /// Make this ingore makro parameters in search.</remarks>
        private int GetContainedOriginals(string s,
            IDictionary<int, string> containedOriginals)
        {
            foreach (var item in builtInValues.Keys)
            {
                FindString(s, containedOriginals, item);
            }
            foreach (var original in values.Keys)
            {
                FindString(s, containedOriginals, original);
            }
            return containedOriginals.Count;
        }

        private void FindString(string s, IDictionary<int, string> containedOriginals, string toFind)
        {
            int index = s.IndexOf(toFind);
            while (index >= 0)// && index + original.Length < s.Length && s[index - 1]
            {
                bool can1;
                if (index > 0)
                {
                    char c = s[index - 1];
                    can1 = !DefineCollectionOptimized.IsValidCharacter(c);
                }
                else can1 = true;//in the beginning of the line

                bool can2;
                if (index + toFind.Length < s.Length)
                {
                    char c = s[index + toFind.Length];
                    can2 = !DefineCollectionOptimized.IsValidCharacter(c);
                }
                else can2 = true;//in the end of the line

                if (can1 && can2 && !containedOriginals.ContainsKey(index))
                {
                    containedOriginals[index] = toFind;
                    break;
                }
                index = s.IndexOf(toFind, index + 1);
            }
        }
    }
}
