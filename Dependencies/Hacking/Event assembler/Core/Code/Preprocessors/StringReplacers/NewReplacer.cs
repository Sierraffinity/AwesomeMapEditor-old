using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Event_Assembler.Core.Collections;

using Nintenlord.Event_Assembler.Core.Code.Preprocessors;
using Nintenlord.Utility;
using Nintenlord.Collections;

namespace Nintenlord.Event_Assembler.Core.Code.StringReplacers
{
    class NewReplacer : IStringReplacer
    {
        private IDictionary<string, IDictionary<int, IMacro>> values;
        private IDictionary<string, IMacro> builtInValues;
        private int maxIter;
        private int currentIter = 0;

        #region IStringReplacer Members

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
            if (currentIter == maxIter)
            {
                newString = s;
                return false;
            }
            currentIter++;
            bool result = true;
            IDictionary<int, Triplet<int, IMacro, string[]>> replace = FindMacros(s);

            StringBuilder bldr = new StringBuilder(s);

            foreach (var item in replace)
            {
                string[] parameters = item.Value.Value;
                string[] newParameters = new string[parameters.Length];
                for (int i = 0; i < parameters.Length; i++)
                {
                    result = Replace(parameters[i], out newParameters[i]) && result;
                }
                string tempString = item.Value.Key2.Replace(newParameters);
                result = Replace(tempString, out tempString) && result;
                string toReplace = s.Substring(item.Key, item.Value.Key1);
                bldr.Replace(toReplace, tempString, item.Key, toReplace.Length);
            }
            newString = bldr.ToString();
            currentIter--;
            return result;
        }

        private SortedDictionary<int, Triplet<int, IMacro, string[]>> FindMacros(string s)
        {
            var replace =
                new SortedDictionary<int, Triplet<int, IMacro, string[]>>(
                    ReverseComparer<int>.Default
                    );

            StringBuilder macroName = new StringBuilder();

            for (int i = 0; i < s.Length; )
            {
                if (DefineCollectionOptimized.IsValidCharacter(s[i]))
                {
                    macroName.Append(s[i]);
                    i++;
                }
                else
                {
                    if (macroName.Length > 0)
                    {
                        GetMacroData(s, replace, ref i, macroName.ToString());
                        macroName.Clear();
                    }
                    else i++;
                }
            }
            int last = s.Length;
            GetMacroData(s, replace, ref last, macroName.ToString());
            return replace;
        }

        private void GetMacroData(string s, SortedDictionary<int, Triplet<int, IMacro, string[]>> replace, ref int i, string name)
        {
            IMacro replacer;
            IDictionary<int, IMacro> replacers;
            bool isBuildIn = builtInValues.TryGetValue(name, out replacer);
            bool isValue = values.TryGetValue(name, out replacers);

            int paramLength;
            string[] parameters;
            if (isValue || isBuildIn)
            {
                if (i < s.Length && s[i] == '(')
                {
                    parameters = GetParameters(s, i, out paramLength);
                }
                else
                {
                    paramLength = 0;
                    parameters = new string[0];
                }
                if ((isBuildIn && replacer.IsCorrectAmountOfParameters(parameters.Length)) ||
                    (isValue && replacers.TryGetValue(parameters.Length, out replacer)))
                {
                    replace[i - name.Length] = new Triplet<int, IMacro, string[]>
                        (paramLength + name.Length, replacer, parameters);
                }

                i += paramLength;
            }
            else i++;
        }

        #endregion

        private static bool ContainsAt(string s, int index, string toSearch)
        {
            bool contains = true;
            if (toSearch.Length > s.Length - index)
                contains = false;
            else
            {
                for (int i = 0; i < toSearch.Length; i++)
                {
                    if (s[index + i] != toSearch[i])
                    {
                        contains = false;
                        break;
                    }
                }
            }
            return contains;
        }

        private static int GetParameterLength(string s, int index, out int parameters)
        {
            int depth = 1;
            parameters = 1;
            int i;
            for (i = index + 1; i < s.Length && depth != 0; i++)
            {
                switch (s[i])
                {
                    case '(':
                        depth++;
                        break;
                    case ')':
                        depth--;
                        break;
                    case ',':
                        if (depth == 1)//So that macros as macro parameters won't mess stuff up
                        {
                            parameters++;
                        }
                        break;
                    default:
                        break;
                }
            }
            return i - index;
        }

        private static string[] GetParameters(string s, int index)
        {
            int dontCare;
            return GetParameters(s, index, out dontCare);
        }

        private static string[] GetParameters(string s, int index, out int lengthInString)
        {
            List<string> parameters = new List<string>();
            int parentDepth = 1;
            int vectorDepth = 0;
            StringBuilder bldr = new StringBuilder();
            int i;
            for (i = index + 1; i < s.Length && parentDepth > 0; i++)
            {
                switch (s[i])
                {
                    case '(':
                        parentDepth++;
                        bldr.Append(s[i]);
                        break;
                    case ')':
                        parentDepth--;
                        bldr.Append(s[i]);
                        break;
                    case '[':
                        vectorDepth++;
                        bldr.Append(s[i]);
                        break;
                    case ']':
                        vectorDepth--;
                        bldr.Append(s[i]);
                        break;
                    case ',':
                        if (parentDepth == 1 && vectorDepth == 0)
                        {
                            parameters.Add(bldr.ToString());
                            bldr.Clear();
                        }
                        else
                        {
                            bldr.Append(s[i]);
                        }
                        break;
                    default:
                        bldr.Append(s[i]);
                        break;
                }
            }
            if (bldr.Length > 0)
            {
                parameters.Add(bldr.ToString(0, bldr.Length - 1));
            }
            lengthInString = i - index;
            for (i = 0; i < parameters.Count; i++)
            {
                parameters[i] = parameters[i].Trim();
            }
            return parameters.ToArray();
        }        
    }
}
