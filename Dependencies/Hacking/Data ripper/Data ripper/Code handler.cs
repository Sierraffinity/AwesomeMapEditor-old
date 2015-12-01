using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Data_ripper
{
    static class StringManipulation
    {
        static public bool HasHexNumber(string parameter)
        {
            return (parameter.StartsWith("0x") || parameter.StartsWith("$"));
        }

        static public bool HasHexByte(string parameter)
        {
            return parameter.StartsWith("0x");
        }

        static public bool HasHexWord(string parameter)
        {
            return parameter.StartsWith("$");
        }

        static public bool IsComment(string parameter)
        {
            return (parameter.StartsWith("#"));
        }


        static public int GenericNumericConverter(string parameter)
        {
            int code = 0;

            if (HasHexNumber(parameter))
            {
                if (parameter.StartsWith("$"))
                    parameter = parameter.Substring(1);

                try
                {
                    code = Convert.ToInt32(parameter, 16);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Format error in: " + parameter);
                }
            }
            else
            {
                try
                {
                    code = Convert.ToInt32(parameter);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Format error in: " + parameter);
                }
            }

            return code;
        }


        //String manipulation
        static public string[] getLines(string text)
        {
            List<string> lines = new List<string>();
            int index = 0;
            while (index < text.Length)
            {
                int endLine = text.IndexOfAny(new char[2]{ '\r', '\n'}, index);

                if (endLine < 0)
                    endLine = text.Length;

                string line = text.Substring(index, endLine - index);
                if (line != null && line.Length > 0 && line[0] != '#')
                    lines.Add(line);

                index = endLine + 1;
            }

            return lines.ToArray();
        }

        static void removeCarriageReturns(string text)
        {
            int index = text.IndexOf('\r');
            while (index >= 0)
            {
                text = text.Remove(index, 1);
                index = text.IndexOf('\r');
            }
        }
    }
}
