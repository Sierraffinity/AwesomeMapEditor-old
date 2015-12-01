using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Nintenlord.Event_assembler
{
    /// <summary>
    /// Extensions and helper methods to string class
    /// </summary>
    static class StringExtensions
    {
        static public bool IsLable(this string parameter)
        {
            return parameter.StartsWith("@") || parameter.EndsWith(":");
        }
        static public bool IsCoordinate(this string parameter)
        {
            return ((parameter.StartsWith("[")) && parameter.EndsWith("]") && parameter.Contains(","));
        }
        static public bool IsHexNumber(this string parameter)
        {
            return (parameter.StartsWith("0x") || parameter.StartsWith("$"));
        }
        static public bool IsHexByte(this string parameter)
        {
            return parameter.StartsWith("0x");
        }
        static public bool IsHexWord(this string parameter)
        {
            return parameter.StartsWith("$");
        }
        static public bool IsBinary(this string parameter)
        {
            return parameter.EndsWith("b", StringComparison.OrdinalIgnoreCase);
        }
        static public bool IsParenthesis(this string parameter)
        {
            int beginning = parameter.IndexOf('(');
            int end = parameter.IndexOf(')');
            return (beginning >= 0) && (end > beginning);
        }
        static public bool IsValidNumber(this string parameter)
        {
            bool hex = false;
            if (parameter.IsHexByte())
            {
                hex = true;
                parameter = parameter.Substring(2);
            }
            else if (parameter.IsHexWord())
            {
                hex = true;
                parameter = parameter.Substring(1);
            }

            if (hex)
            {
                for (int i = 0; i < parameter.Length; i++)
                {
                    if (!parameter[i].IsHexDigit())
                    {
                        return false;
                    }
                }
            }
            else if (parameter.IsBinary())
            {
                for (int i = 0; i < parameter.Length - 1; i++)
                {
                    if (!(parameter[i] == '0' || parameter[i] == '1'))
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < parameter.Length; i++)
                {
                    if (!char.IsNumber(parameter, i))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static bool IsHexDigit(this char c)
        {
            if (char.IsNumber(c))
            {
                return true;
            }
            switch (c)
            {
                case 'a':
                case 'b':
                case 'c':
                case 'd':
                case 'e':
                case 'f':
                case 'A':
                case 'B':
                case 'C':
                case 'D':
                case 'E':
                case 'F':
                    return true;
                default:
                    return false;
            }
        }
        static bool IsNewLine(this char s)
        {
            return s == '\n' || s == '\r';
        }

        //static public bool Test(this string text, StringTester comparer, int position, int length)
        //{
        //    return comparer(text, position, length);
        //}
        //static public bool Test(this string text, StringTester comparer, int position)
        //{
        //    return comparer(text, position, text.Length - position);
        //}

        static public bool ContainsWhiteSpace(this string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsWhiteSpace(text[i]))
                {
                    return true;
                }
            }
            return false;
        }
        static public bool ContainsNonWhiteSpace(this string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (!char.IsWhiteSpace(text[i]))
                {
                    return true;
                }
            }
            return false;
        }
        
        static public bool TryGetValue(this string s, out int value)
        {
            value = 0;
            if (s.IsValidNumber())
            {
                value = s.GetValue();
                return true;
            }
            else return false;
        }

        static public bool Same(string a, int index1, string b, int index2, int length)
        {
            if (index1 < 0 || index2 < 0 || index1 + length > a.Length || index2 + length > b.Length)
            {
                throw new IndexOutOfRangeException();
            }
            for (int i = 0; i < length; i++)
            {
                if (a[index1 + i] != b[index2 + i])
                {
                    return false;
                }
            }
            return true;
        }

        static public int GetValue(this string parameter)
        {
            int code = 0;
            if (parameter.IsHexNumber())
            {
                if (parameter.StartsWith("$"))
                    parameter = parameter.Substring(1);
                
                code = Convert.ToInt32(parameter, 16);
            }
            else if (parameter.IsBinary())
            {
                code = Convert.ToInt32(parameter.Substring(0, parameter.Length - 1), 2);                
            }
            else
            {
                code = Convert.ToInt32(parameter);
            }

            return code;
        }
        static public string GetLableName(this string parameter)
        {
            return parameter.Trim('@', ':');
        }
        static public int[] GetCoordinates(this string parameter)
        {
            string Xcoordinate, Ycoordinate;

            int position = parameter.IndexOf(',');

            Xcoordinate = parameter.Substring(1, position - 1);
            Ycoordinate = parameter.Substring(position + 1, parameter.Length - position - 2);

            int x, y;

            x = GetValue(Xcoordinate);
            y = GetValue(Ycoordinate);

            return new int[] { x, y };
        }
        static public int[] GetIndexes(this string text, string toFind)
        {
            List<int> results = new List<int>(text.Length / 10);
            int lastIndex = text.IndexOf(toFind);
            while (lastIndex >= 0)
            {
                results.Add(lastIndex);
                lastIndex = text.IndexOf(toFind, lastIndex + 1);
            }
            return results.ToArray();
        }

        static public int IndexOf(this string text, Predicate<char> match)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (match(text[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        static public int LastIndexOf(this string text, Predicate<char> match)
        {
            int i;
            for (i = text.Length - 1; i >= 0 && match(text[i]); i--) ;
            return i;
        }

        static public string[] Split(this string text, params int[] indexes)
        {
            string[] result = new string[indexes.Length + 1];

            result[0] = text.Substring(0, indexes[0]);
            for (int i = 0; i < indexes.Length - 1; i++)
            {
                result[i + 1] = text.Substring(indexes[i] + 1, indexes[i + 1] - indexes[i] - 1);
            }
            result[result.Length - 1] = text.Substring(indexes[indexes.Length - 1] + 1);

            return result;
        }
        static public string[] Split(this string line, char[] separators, char[][] uniters)
        {
            List<string> parameters = new List<string>();

            int begIndex = 0;
            Stack<int> uniterIndexs = new Stack<int>();
            
            for (int i = 0; i < line.Length; i++)
            {
                for (int j = 0; j < uniters.Length; j++)
                {
                    if (line[i] == uniters[j][0])
                    {
                        uniterIndexs.Push(j);
                        break;
                    }
                    else if (uniterIndexs.Count > 0 && line[i] == uniters[uniterIndexs.Peek()][1])
                    {
                        uniterIndexs.Pop();
                        break;
                    }
                }

                if (uniterIndexs.Count == 0 && 
                    separators.Contains<char>(line[i]))
                {
                    parameters.Add(line.Substring(begIndex, i - begIndex));
                    begIndex = i + 1;
                }
            }

            if (begIndex < line.Length)
            {
                parameters.Add(line.Substring(begIndex));
            }

            parameters.RemoveAll(string.IsNullOrEmpty);

            return parameters.ToArray();
        }

        static public string ReplaceEach(this string text, string[] toReplace, string[] with)
        {
            if (toReplace.Length != with.Length)
            {
                throw new ArgumentException("toReplace and with need to contain same amount of items.");
            }

            //The laxy way
            for (int i = 0; i < toReplace.Length; i++)
            {
                text = text.Replace(toReplace[i], with[i]);
            }

            return text;
        }

        static public bool ContainsAnyOf(this string text, char[] toContain)
        {
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < toContain.Length; j++)
                {
                    if (text[i] == toContain[j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static public int AmountInTheBeginning(this string text, char value)
        {
            int i = 0;
            while (i < text.Length && text[i] == value)
            {
                i++;
            }
            return i;
        }

        static public int AmountInTheEnd(this string text, char value)
        {
            int i = text.Length - 1;
            while (i >= 0 && text[i] == value)
            {
                i--;
            }
            return text.Length - 1 - i;
        }


        static public int Amount(this string text, char value)
        {
            int result = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == value)
                {
                    result++;
                }
            }
            return result;
        }

        static public string Repeat(this string text, int toLength)
        {
            char[] rawText = new char[toLength];
            for (int i = 0; i < toLength; i++)
            {
                rawText[i] = text[i % text.Length];
            }
            return new string(rawText);
        }
    }
}