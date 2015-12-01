using System;
using System.Collections.Generic;
 

namespace Nintenlord.Event_assembler.Collections
{
    public interface IDefineCollection
    {
        void Add(string original, string replacer, params string[] parameters);
        void Add(string original, string replacer);
        void AddRange(string[] original);
        bool ContainsOriginal(string item, int numberOfParameters);
        bool ContainsOriginal(string item);
        string GetReplacer(string item, string[] parameters);
        string GetReplacer(string item);
        KeyValuePair<string, string[]> GetReplacerAndParameters(string item, string[] parameters);
        void Remove(string original);
        string ApplyDefines(string original);
    }
}
