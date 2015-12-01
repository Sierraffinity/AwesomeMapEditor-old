using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Event_Assembler.Core.Code.Templates;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Code.Language
{
    /// <summary>
    /// Helper class for templates and codes
    /// </summary>
    class Code : IEnumerable<Tuple<int, Priority>>
    {
        readonly private ICodeTemplate template;
        readonly private string[] text;
        readonly private int length;

        /// <summary>
        /// The template of this code
        /// </summary>
        public ICodeTemplate Template
        {
            get { return template; }
        }
        /// <summary>
        /// The text of this code
        /// </summary>
        public string[] Text
        {
            get { return text; }
        }
        /// <summary>
        /// Lenght of this code in bytes
        /// </summary>
        public int Length
        {
            get { return length; }
        }

        /// <summary>
        /// Creates a new Code from template and matching text.
        /// </summary>
        /// <param name="line">Code split to parameters</param>
        /// <param name="template">Template of this code</param>
        public Code(string[] line, ICodeTemplate template, int length)
        {
            this.template = template;
            this.text = line;
            this.length = length;

        }

        /// <summary>
        /// Checks if Codes have the same template
        /// </summary>
        /// <param name="a">Instance of Code</param>
        /// <param name="b">Instance of Code</param>
        /// <returns>Returns true codes use the same template, else false</returns>
        public static bool operator ==(Code a, Code b)
        {
            return a.template == b.template;
        }

        /// <summary>
        /// Checks if Codes do not have the same template
        /// </summary>
        /// <param name="a">Instance of Code</param>
        /// <param name="b">Instance of Code</param>
        /// <returns>Returns true codes use different template, else false</returns>
        public static bool operator !=(Code a, Code b)
        {
            return a.template != b.template;
        }

        /// <summary>
        /// Return templates hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return template.GetHashCode();
        }

        /// <summary>
        /// Checks if obj is a Code and uses the same template
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True if obj is Code and has same themplate, else false</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Code))
            {
                return false;
            }
            return this == (Code)obj;
        }

        #region IEnumerable<Tuple<int,Priority>> Members

        public IEnumerator<Tuple<int, Priority>> GetEnumerator()
        {
            var templ = template as CodeTemplate;
            if (templ != null)
            {
                for (int i = 0; i < templ.AmountOfParams; i++)
                {
                    if (templ[i].pointer)
                    {
                        yield return Tuple.Create(text[i + 1].GetValue(), templ[i].pointedPriority);
                    }
                }
            }            
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        public string[] ReplaceOffsetsWithLables(IDictionary<int, string> lables)
        {
            string[] result = new string[text.Length];
            Array.Copy(text, result, text.Length);

            for (int i = 1; i < result.Length; i++)
            {
                int val;
                string labelName;
                if (result[i].TryGetValue(out val) && lables.TryGetValue(val, out labelName))
                {
                    result[i] = labelName;
                }
            }
            return result;
        }
    }
}
