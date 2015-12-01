using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.Event_Assembler.Core.Collections
{
    /// <summary>
    /// Collection for containing defines and macros
    /// </summary>
    class DefineCollectionOld : IDefineCollection
    {
        List<string> originals, replacers;
        List<string[]> parameters;

        int Count
        {
            get { return originals.Count; }
        }

        public DefineCollectionOld()
        {
            originals = new List<string>();
            replacers = new List<string>();
            parameters = new List<string[]>();
        }

        public DefineCollectionOld(int capacity)
        {
            originals = new List<string>(capacity);
            replacers = new List<string>(capacity);
            parameters = new List<string[]>(capacity);
        }

        public DefineCollectionOld(string[] predefined)
        {
            originals = new List<string>();
            replacers = new List<string>();
            parameters = new List<string[]>();

            originals.AddRange(predefined);
            for (int i = 0; i < predefined.Length; i++)
            {
                replacers.Add(string.Empty);
                parameters.Add(new string[0]);
            }
        }

        public void Add(string original, string replacer)
        {
            originals.Add(original);
            replacers.Add(replacer);
            parameters.Add(new string[0]);
        }
        public void Add(string original, string replacer, params string[] parameters)
        {
            originals.Add(original);
            replacers.Add(replacer);
            this.parameters.Add(parameters);
        }

        public void AddRange(string[] original)
        {
            originals.AddRange(original);
            replacers.AddRange(new string[original.Length]);
            for (int i = 0; i < original.Length; i++)
            {
                parameters.Add(new string[0]);
            }
        }

        public bool ContainsName(string item)
        {
            return GetIndex(item, 0) != -1;
        }
        public bool ContainsName(string item, params string[] parameters)
        {
            return GetIndex(item, parameters.Length) != -1;
        }

        private int GetIndex(string item, int numberOfParameters)
        {
            bool found = false;
            int index = originals.IndexOf(item);
            while (!found && index >= 0)
            {
                if (this.parameters[index].Length == numberOfParameters)
                    found = true;
                else
                    index = originals.IndexOf(item, index + 1);
            }
            return index;
        }

        public string GetReplacer(string item)
        {
            return GetReplacer(item, new string[0]);
        }
        public string GetReplacer(string item, string[] parameters)
        {
            int index;
            if ((index = GetIndex(item, parameters.Length)) > -1)
            {
                return string.Copy(replacers[index]);
            }
            else
            {
                return null;
            }
        }
        public KeyValuePair<string, string[]> GetReplacerAndParameters(string item, string[] parameters)
        {
            int index;
            if ((index = GetIndex(item, parameters.Length)) > -1)
            {
                return new KeyValuePair<string, string[]>(replacers[index], this.parameters[index]);
            }
            else
            {
                return new KeyValuePair<string, string[]>(null, null);
            }
        }

        public void Remove(string original)
        {
            int index = originals.IndexOf(original);
            if (index >= 0)
            {
                originals.RemoveAt(index);
                replacers.RemoveAt(index);
                parameters.RemoveAt(index);
            }
        }

        public bool ApplyDefines(string original, out string newString)
        {
            //Lazy, no macro support
            int index = originals.IndexOf(original);
            if (index < 0)
            {
                newString = original;
            }
            else
            {
                newString = replacers[index];
            }
            return true;
        }

        public void Remove(string original, params string[] parameters)
        {
            int index = GetIndex(original, parameters.Length);
            if (index >= 0)
            {
                this.originals.RemoveAt(index);
                this.replacers.RemoveAt(index);
                this.parameters.RemoveAt(index);
            }
        }

        #region IDefineCollection Members


        public bool IsValidName(string name)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}