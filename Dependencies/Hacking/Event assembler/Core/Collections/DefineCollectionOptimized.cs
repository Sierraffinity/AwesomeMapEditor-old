using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Event_Assembler.Core.Code.StringReplacers;
using Nintenlord.Event_Assembler.Core.Code.Preprocessors;

namespace Nintenlord.Event_Assembler.Core.Collections
{
    class DefineCollectionOptimized : IDefineCollection
    {
        IStringReplacer replacer;

        private struct UserDefinedReplacer : IEquatable<UserDefinedReplacer>, IMacro
        {
            private string toReplaceWith;

            private string[] parameters;

            public int AmountOfParameters
            {
                get { return parameters.Length; }
            }

            public UserDefinedReplacer(string toReplaceWith, string[] parameters)
            {
                this.toReplaceWith = toReplaceWith;
                this.parameters = parameters;
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
            
            public bool IsCorrectAmountOfParameters(int amount)
            {
                return AmountOfParameters == amount;
            }

            #region IEquatable<Replacer> Members

            public bool Equals(UserDefinedReplacer other)
            {
                return toReplaceWith.Equals(other.toReplaceWith)
                    && parameters.Length == other.parameters.Length;
            }

            #endregion

            #region IEquatable<IReplacer> Members

            public bool Equals(IMacro other)
            {
                if (other is UserDefinedReplacer)
                {
                    return this.Equals(other);
                }
                else
                {
                    return false;
                }
            }

            #endregion
        }

        private IDictionary<string, IDictionary<int, IMacro>> values;
        private IDictionary<string, IMacro> builtInValues;
        
        public IMacro this[string name]
        {
            set
            {
                builtInValues[name] = value;
            }
        }

        public DefineCollectionOptimized()
            : this(new NewReplacer(), new Dictionary<string, IMacro>())
        {

        }

        public DefineCollectionOptimized(IStringReplacer replacer)
            : this(replacer, new Dictionary<string, IMacro>())
        {

        }

        public DefineCollectionOptimized(Dictionary<string, IMacro> builtInMacros)
            : this(new NewReplacer(), builtInMacros)
        {

        }

        public DefineCollectionOptimized(IStringReplacer replacer, Dictionary<string, IMacro> builtInMacros)
        {
            values = new Dictionary<string, IDictionary<int, IMacro>>();
            builtInValues = new Dictionary<string, IMacro>();
            this.replacer = replacer;
            replacer.BuiltInValues = builtInValues;
            replacer.Values = values;
            replacer.MaxIter = 100;
        }

        #region IDefineCollection Members

        public void Add(string original, string replacer, params string[] parameters)
        {
            UserDefinedReplacer replacerNew = new UserDefinedReplacer(replacer, parameters);

            IDictionary<int, IMacro> dic;
            if (!values.TryGetValue(original, out dic))
            {
                dic = new SortedDictionary<int, IMacro>();
                values[original] = dic;
            }

            dic[parameters.Length] = replacerNew;
        }

        public void Add(string original, string replacer)
        {
            this.Add(original, replacer, new string[]{});
        }

        public bool ContainsName(string item, params string[] parameters)
        {
            IDictionary<int, IMacro> dic;
            if (values.TryGetValue(item, out dic))
            {
                return dic.ContainsKey(parameters.Length);
            }
            else
            {
                return builtInValues.ContainsKey(item);
            }
        }

        public bool ContainsName(string item)
        {
            return ContainsName(item, new string[] { });
        }

        public void Remove(string original)
        {
            this.Remove(original, new string[] { });
        }

        public void Remove(string original, params string[] parameters)
        {
            IDictionary<int, IMacro> dic;
            if (values.TryGetValue(original, out dic))
            {
                dic.Remove(parameters.Length);
            }
        }

        public bool ApplyDefines(string s, out string newString)
        {
            return replacer.Replace(s, out newString);
        }

        public bool IsValidName(string name)
        {
            return name.Length > 0 && name.All(IsValidCharacter);
        }

        #endregion

        public static bool IsValidCharacter(char c)
        {
            bool isCharacter = Char.IsLetterOrDigit(c);
            return isCharacter || c == '_';
        }

        private void GetSubstring(string s, int i, int length, StringBuilder output)
        {
            output.Remove(0, output.Length);
            for (int j = i; j < length; j++)
            {
                output.Append(s[j]);
            }
        }
    }
}
