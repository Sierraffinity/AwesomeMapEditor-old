using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Event_Assembler.Utility;
using Nintenlord.ROMHacking.Collections;

namespace Nintenlord.Event_Assembler.Collections
{
    class DefineCollectionTreeBased : IDefineCollection
    {
        private readonly int maxIter = 100;

        EnumarationTree<char, IDictionary<int, DefineInfo>> defines;

        public DefineCollectionTreeBased()
        {
            defines = new EnumarationTree<char, IDictionary<int, DefineInfo>>();
        }

        #region IDefineCollection Members

        public void Add(string name, string replacer, params string[] parameters)
        {
            IDictionary<int, DefineInfo> value;
            if (defines.TryGetValue(name, out value))
            {
                DefineInfo newInfo = new DefineInfo();
                newInfo.Parameters = parameters;
                newInfo.Replacer = replacer;
                value[parameters.Length] = newInfo;
            }
            else
            {
                var newDict = new Dictionary<int, DefineInfo>();
                defines[name] = newDict;
                DefineInfo newInfo = new DefineInfo();
                newInfo.Parameters = parameters;
                newInfo.Replacer = replacer;
                newDict[parameters.Length] = newInfo;
            }
        }

        public void Add(string name, string replacer)
        {
            Add(name, replacer, new string[] { });
        }

        public bool ContainsName(string name, params string[] parameters)
        {
            IDictionary<int, DefineInfo> value;
            if (defines.TryGetValue(name, out value))
            {
                return value.ContainsKey(parameters.Length);
            }
            else
            {
                return false;
            }
        }

        public bool ContainsName(string name)
        {
            return ContainsName(name, new string[] { });
        }

        public string GetReplacer(string name, params string[] parameters)
        {
            IDictionary<int, DefineInfo> value;
            if (defines.TryGetValue(name, out value))
            {
                return value[parameters.Length].Replacer;
            }
            else
            {
                throw new KeyNotFoundException("Press any key to continue.");
            }
        }

        public string GetReplacer(string name)
        {
            return GetReplacer(name, new string[] { });
        }

        public void Remove(string name, params string[] parameters)
        {
            IDictionary<int, DefineInfo> value;
            if (defines.TryGetValue(name, out value))
            {
                value.Remove(parameters.Length);
            }
        }

        public void Remove(string name)
        {
            Remove(name, new string[] { });
        }

        public bool ApplyDefines(string original, out string newOriginal)
        {
            StringBuilder builder = new StringBuilder(original);

            SortedDictionary<int, string> containedOriginals = new SortedDictionary<int, string>(
                new LamdaComparer<int>((x, y) => y - x));
            int iter = 0;
            while (GetContainedOriginals(builder.ToString(), containedOriginals) > 0 && iter < maxIter)
            {
                foreach (var item in containedOriginals)
                {
                    string[] parameters;
                    string name = item.Value;
                    int indexStart = item.Key + name.Length;

                    string paramString;
                    //Get parameters
                    if (indexStart < builder.Length && builder[indexStart] == '(')
                    {
                        int depth = 1;
                        int endIndex = indexStart;
                        while (depth > 0 && ++endIndex < builder.Length)
                        {
                            if (builder[endIndex] == ')')
                            {
                                depth--;
                            }
                            else if (builder[endIndex] == '(')
                            {
                                depth++;
                            }
                        }
                        parameters = builder.ToString(indexStart + 1, endIndex - indexStart - 1).Split(',');
                        paramString = builder.ToString(indexStart, endIndex - indexStart + 1);

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
                    string temp = defines.ToString();
                    IDictionary<int, DefineInfo> dict = defines[name];
                    DefineInfo info;
                    if (!dict.TryGetValue(parameters.Length, out info))
                    {
                        continue;
                    }

                    string toReplace = name + paramString;
                    string replaceWith = info.Replace(parameters);

                    builder.Replace(toReplace, replaceWith, item.Key, toReplace.Length);
                }
                containedOriginals.Clear();
                iter++;
            }
            newOriginal = builder.ToString();
            return containedOriginals.Count == 0;
        }

        private int GetContainedOriginals(string p, IDictionary<int, string> containedOriginals)
        {
            for (int i = 0; i < p.Length;)
            {
                string subString = p.Substring(i);
                SortedDictionary<string, IDictionary<int, DefineInfo>> foundDefines
                    = defines.GetTraversedValues<string>(subString, x => x.GetString());

                if (foundDefines.Count > 0)
                {
                    var item = foundDefines.First();
                    containedOriginals[i] = item.Key;
                    i += item.Key.Length;
                }
                else
                {
                    i++;
                }

            }
            return containedOriginals.Count;
        }

        public bool IsValidName(string name)
        {
            return name.Length > 0 && name.All(IsValidCharacter);
        }

        #endregion

        private bool IsValidCharacter(char c)
        {
            bool characetr = Char.IsLetterOrDigit(c);
            return characetr || c == '_';
        }

        private struct DefineInfo
        {
            public int AmountOfParameters
            {
                get { return Parameters.Length; }
            }
            public string[] Parameters
            {
                get;
                set;
            }
            public string Replacer
            {
                get;
                set;
            }

            public string Replace(string[] parameters)
            {
                if (parameters.Length != this.Parameters.Length)
                {
                    throw new ArgumentException();
                }
                StringBuilder builder = new StringBuilder(Replacer);

                for (int i = 0; i < parameters.Length; i++)
                {
                    builder.Replace(this.Parameters[i], parameters[i].Trim());
                }
                return builder.ToString();
            }
        }
    }
}
