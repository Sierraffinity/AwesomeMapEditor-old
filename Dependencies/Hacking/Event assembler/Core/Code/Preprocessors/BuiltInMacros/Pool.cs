using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors.BuiltInMacros
{
    public class Pool : IMacro
    {
        IDictionary<string, string> lines;
        int used = 1;

        public int AmountOfLines
        {
            get { return lines.Count; }
        }

        public Pool()
        {
            lines = new Dictionary<string, string>();
        }

        #region IReplacer Members

        public bool IsCorrectAmountOfParameters(int amount)
        {
            return amount == 1 || amount == 2;
        }

        public string Replace(string[] parameters)
        {
            string label;
            switch (parameters.Length)
            {
                case 1:
                    label = "poolLabel" + (used).ToString();
                    used++;
                    break;
                case 2:
                    label = parameters[1];
                    break;
                default:
                    label = "";
                    //Error
                    break;
            }
            lines[label] = parameters[0];
            return label;
        }

        #endregion

        #region IEquatable<IReplacer> Members

        public bool Equals(IMacro other)
        {
            return this == other;
        }

        #endregion

        public void DumpPool(System.IO.TextWriter output)
        {
            foreach (var item in lines)
            {
                output.WriteLine(string.Format("{0}:\n {1}", item.Key, item.Value));
            }
            lines.Clear();
        }

        public string[] DumpPool()
        {
            List<string> lines = new List<string>();
            foreach (var item in this.lines)
            {
                foreach (var line in string.Format("{0}:\n {1}", item.Key, item.Value).Split(';'))
                {
                    lines.Add(line);
                }
            }
            this.lines.Clear();
            return lines.ToArray();
        }
    }
}
