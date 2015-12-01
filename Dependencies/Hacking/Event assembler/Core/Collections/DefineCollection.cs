using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Collections
{
    class DefineCollection : IDefineCollection, IEnumerable<KeyValuePair<string, int>>
    {
        private readonly int maxIter = 100;

        private struct Replacer : IEquatable<Replacer>
        {
            private string toReplaceWith;

            private string[] parameters;

            public int AmountOfParameters
            {
                get { return parameters.Length; }
            }

            public Replacer(string toReplaceWith, string[] parameters)
            {
                this.toReplaceWith = toReplaceWith;
                this.parameters = parameters;
            }

            public string GetReplacer()
            {
                return toReplaceWith;
            }
            
            public string Replace(string[] parameters)
            {
                if (parameters.Length != this.parameters.Length)
                {
                    throw new ArgumentException();
                }
                StringBuilder builder = new StringBuilder(toReplaceWith);

                for (int i = 0; i < parameters.Length; i++)
                {
                    builder.Replace(this.parameters[i], parameters[i].Trim());
                }
                return builder.ToString();
            }


            public static explicit operator KeyValuePair<string,string[]>(Replacer item)
            {
                return new KeyValuePair<string, string[]>(item.toReplaceWith, item.parameters);
            }

            #region IEquatable<Replacer> Members

            public bool Equals(Replacer other)
            {
                return toReplaceWith.Equals(other.toReplaceWith)
                    && parameters.Length == other.parameters.Length;
            }

            #endregion
        }

        private Dictionary<KeyValuePair<string, int>, Replacer> values;

        private HashSet<string> originals;

        public DefineCollection()
        {
            values = new Dictionary<KeyValuePair<string, int>, Replacer>();
            originals = new HashSet<string>();
        }

        #region IDefineCollection Members

        public void Add(string original, string replacer, params string[] parameters)
        {
            Replacer replacerNew = new Replacer(replacer, parameters);
            values[new KeyValuePair<string, int>(original, parameters.Length)] = replacerNew;
            originals.Add(original);
        }

        public void Add(string original, string replacer)
        {
            Replacer replacerNew = new Replacer(replacer, new string[0]);
            values.Add(new KeyValuePair<string, int>(original, 0), replacerNew);
            originals.Add(original);
        }

        public bool ContainsName(string item, params string[] parameters)
        {
            return values.ContainsKey(new KeyValuePair<string, int>(item, parameters.Length));
        }

        public bool ContainsName(string item)
        {
            foreach (var value in values)
            {
                if (value.Key.Key.Equals(item) && value.Value.AmountOfParameters == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public string GetReplacer(string item, string[] parameters)
        {
            foreach (var value in values)
            {
                if (value.Key.Key.Equals(item) && value.Value.AmountOfParameters == parameters.Length)
                {
                    return value.Value.GetReplacer();
                }
            }
            throw new KeyNotFoundException();
        }

        public string GetReplacer(string item)
        {
            foreach (var value in values)
            {
                if (value.Key.Key.Equals(item) && value.Value.AmountOfParameters == 0)
                {
                    return value.Value.GetReplacer();
                }
            }
            throw new KeyNotFoundException();
        }

        public void Remove(string original)
        {
            values.Remove(new KeyValuePair<string, int>(original, 0));
            originals.Remove(original);
        }

        public void Remove(string original, params string[] parameters)
        {
            values.Remove(new KeyValuePair<string, int>(original, parameters.Length));
            originals.Remove(original);
        }

        public bool ApplyDefines(string s, out string newString)
        {
            StringBuilder builder = new StringBuilder(s);

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
                        while (depth > 0 && ++endIndex < s.Length)
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
                    Replacer replacer;
                    KeyValuePair<string, int> key = new KeyValuePair<string, int>(name, parameters.Length);
                    if (!values.TryGetValue(key, out replacer))
                    {
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
            //return s;
            //throw new NotImplementedException();
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
                
        private int GetContainedOriginals(string s, IDictionary<int, string> containedOriginals)
        {
            foreach (var original in originals)
            {
                int index = s.IndexOf(original);
                while (index >= 0)// && index + original.Length < s.Length && s[index - 1]
                {
                    bool can1;
                    if (index > 0)
                    {
                        char c = s[index - 1];
                        can1 = !IsValidCharacter(c);
                    }
                    else can1 = true;//in the beginning of the line

                    bool can2;
                    if (index + original.Length < s.Length)
                    {
                        char c = s[index + original.Length];
                        can2 = !IsValidCharacter(c);
                    }
                    else can2 = true;//in the beginning end of the line

                    if (can1 && can2 && !containedOriginals.ContainsKey(index))
                    {
                        containedOriginals[index] = original;
                        break;
                    }
                    index = s.IndexOf(original, index + 1);
                }
            }
            //containedOriginals((y, x) => x.Length - y.Length);
            return containedOriginals.Count;
        }



        #region IEnumerable<KeyValuePair<string,int>> Members

        public IEnumerator<KeyValuePair<string, int>> GetEnumerator()
        {
            return this.values.Keys.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

    }
}
