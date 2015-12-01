using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.Event_assembler.Collections
{
    /// <summary>
    /// Collection for containing defines and macros
    /// </summary>
    class DefineCollection : IDefineCollection
    {
        List<string> originals, replacers;
        List<string[]> parameters;

        int Count
        {
            get { return originals.Count; }
        }

        public DefineCollection()
        {
            originals = new List<string>();
            replacers = new List<string>();
            parameters = new List<string[]>();
        }

        public DefineCollection(int capacity)
        {
            originals = new List<string>(capacity);
            replacers = new List<string>(capacity);
            parameters = new List<string[]>(capacity);
        }

        public DefineCollection(string[] predefined)
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

        public bool ContainsOriginal(string item)
        {
            return ContainsOriginal(item, 0);
        }
        public bool ContainsOriginal(string item, int numberOfParameters)
        {
            return GetIndex(item, numberOfParameters) != -1;
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

        public string ApplyDefines(string original)
        {
            throw new NotImplementedException();
        }
    }
}
/*
        private struct Replacer
        {
            public string name;
            public string[] parameters;

            public Replacer(string name)
            {
                this.name = name;
                this.parameters = new string[0];
            }

            public Replacer(string name, string[] parameters)
            {
                this.name = name;
                this.parameters = parameters;
            }
        }
 */