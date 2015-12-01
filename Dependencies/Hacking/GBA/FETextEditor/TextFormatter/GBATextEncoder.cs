using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Nintenlord.Utility;
using Nintenlord.Collections;
using Nintenlord.Collections.Lists;

namespace FETextEditor.TextFormatter
{
    public class GBATextEncoding : Encoding
    {
        Encoding baseEncoding;

        public GBATextEncoding()
        {
            baseEncoding = Encoding.GetEncoding("shift_jis");
        }

        public override int GetByteCount(char[] chars, int index, int count)
        {
            int result = 0;
            Action<byte[]> handle = x =>
            {
                result += x.Length;
            };

            Iterate(chars, index, count, handle);
            return result;
        }

        public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
        {
            int result = 0;
            Action<byte[]> handle = y =>
            {
                result += y.Length;
                for (int i = 0; i < y.Length; i++)
                {
                    bytes[byteIndex] = y[i];
                    byteIndex++;
                }
            };
            
            Iterate(chars, charIndex, charCount, handle);
            return result;
        }

        public override int GetCharCount(byte[] bytes, int index, int count)
        {
            return GetString(bytes, index, count).Length; 
        }

        public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
        {
            var s = GetString(bytes, byteIndex, byteCount);

            for (int i = 0; i < s.Length; i++)
            {
                chars[i + charIndex] = s[i];
            }

            return s.Length;
        }

        public override int GetMaxByteCount(int charCount)
        {
            return charCount * 2;
        }

        public override int GetMaxCharCount(int byteCount)
        {
            return byteCount * 12;
        }
        

        private void Iterate(char[] chars, int index, int count,
            Action<byte[]> addCode)
        {
            for (int i = 0; i < count; )
            {
                if (chars[index] == '[')
                {
                    int end = Array.FindIndex(chars, index, count - index, x => x == ']');
                    if (end == -1)
                    {
                        throw new ArgumentException();
                    }
                    else
                    {
                        addCode(GetControlCodeBytes(chars, index, end + 1 - index));
                    }
                    index = end + 1;
                }
                else
                {
                    addCode(baseEncoding.GetBytes(chars, index, 1));
                    index++;
                }
            }
        }


        public override string GetString(byte[] bytes, int index, int count)
        {
            StringWriter writer = new StringWriter();
            using (BinaryReader input = new BinaryReader(new MemoryStream(bytes, index, count)))
            {
                while (input.BaseStream.Position < input.BaseStream.Length)
                {
                    GetText(input, writer);
                }
            }
            return writer.ToString();
        }

        private void GetText(BinaryReader input, TextWriter output)
        {
            ushort val = input.ReadUInt16();
            if (val == 0x10 && input.BaseStream.Position < input.BaseStream.Length)
            {
                int val2 = input.ReadUInt16();
                output.Write("[LoadPortrait=0x{0}]", System.Convert.ToString(val2, 16));// & 0xFF
            }
            else if (val == 0x80 && input.BaseStream.Position < input.BaseStream.Length)
            {
                int val2 = input.ReadUInt16();
                output.Write("[MovePortrait=0x{0}]", System.Convert.ToString(val2, 16));// & 0xFF
            }
            else if (val == 0)
            {
                output.Write("[X]");
                output.WriteLine();
            }
            else if (val == 1)
            {
                output.Write("[NL]");
                output.WriteLine();
            }
            else if (val == 2)
            {
                output.Write("[NL2]");
                output.WriteLine();
                output.WriteLine();
            }
            else if (val == 3)
            {
                output.Write("[A]");
            }
            else if (val > 0xFF)
            {
                string s = baseEncoding.GetString(BitConverter.GetBytes(val));

                output.Write(s);
            }
            else
            {
                output.Write("[0x{0}]", System.Convert.ToString(val, 16));
            }
        }

        //private static List<string> FindCodes(string text)
        //{
        //    List<string> codes = new List<string>(text.Length);
        //    StringBuilder bldr = new StringBuilder();
        //    bool code = false;
        //    for (int i = 0; i < text.Length; i++)
        //    {
        //        if (text[i] == '[')
        //        {
        //            bldr.Append(text[i]);
        //            code = true;
        //        }
        //        else if (text[i] == ']')
        //        {
        //            bldr.Append(text[i]);
        //            codes.Add(bldr.ToString());
        //            bldr.Clear();
        //            code = false;
        //        }
        //        else if (code)
        //        {
        //            bldr.Append(text[i]);
        //        }
        //        else if (text[i] != '\n' && text[i] != '\r')
        //        {
        //            codes.Add(text[i].ToString());
        //        }
        //    }
        //    return codes;
        //}
        //private static void WriteText(BinaryWriter output, IEnumerable<string> codes)
        //{
        //    foreach (string item in codes)
        //    {
        //        if (item.Length == 1)
        //        {
        //            output.Write(item[0]);
        //        }
        //        else
        //        {
        //            int val;
        //            string code = item.Trim('[', ']');
        //            if (code.TryGetValue(out val))
        //            {
        //                output.Write((short)val);
        //            }
        //            else if (code == "X")
        //            {
        //                output.Write((short)0);
        //            }
        //            else if (code == "NL")
        //            {
        //                output.Write((short)1);
        //            }
        //            else if (code == "NL2")
        //            {
        //                output.Write((short)2);
        //            }
        //            else if (code == "A")
        //            {
        //                output.Write((short)3);
        //            }
        //            else if (code.StartsWith("LoadPortrait="))
        //            {
        //                output.Write((short)0x10);
        //                int eqIndex = code.IndexOf('=');
        //                string end = code.Substring(eqIndex + 1);
        //                if (end.TryGetValue(out val))
        //                {
        //                    output.Write((short)val);
        //                }
        //            }
        //            else if (code.StartsWith("MovePortrait="))
        //            {
        //                output.Write((short)0x80);
        //                int eqIndex = code.IndexOf('=');
        //                string end = code.Substring(eqIndex + 1);
        //                if (end.TryGetValue(out val))
        //                {
        //                    output.Write((short)val);
        //                }
        //            }
        //            else
        //            {
        //                throw new ArgumentException("Unknow code: " + code);
        //            }
        //        }
        //    }
        //}

        private byte[] GetControlCodeBytes(char[] chars, int index, int length)
        {
            byte[] result;
            string code = new string(chars, index + 1, length - 2);

            int val;
            if (code.TryGetValue(out val))
            {
                result = BitConverter.GetBytes((short)val);
            }
            else if (code == "X")
            {
                result = BitConverter.GetBytes((short)0);
            }
            else if (code == "NL")
            {
                result = BitConverter.GetBytes((short)1);
            }
            else if (code == "NL2")
            {
                result = BitConverter.GetBytes((short)2);
            }
            else if (code == "A")
            {
                result = BitConverter.GetBytes((short)3);
            }
            else if (code.StartsWith("LoadPortrait="))
            {
                result = Get2PartCode(code, 0x10);
            }
            else if (code.StartsWith("MovePortrait="))
            {
                result = Get2PartCode(code, 0x80);
            }
            else
            {
                throw new ArgumentException("Unknow code: " + code);
            }
            return result;
        }

        private static byte[] Get2PartCode(string code, short first)
        {
            int val;
            byte[] result;
            short second;
            int eqIndex = code.IndexOf('=');
            string end = code.Substring(eqIndex + 1);
            if (end.TryGetValue(out val))
            {
                second = (short)val;
            }
            else throw new ArgumentException();
            var bytes1 = BitConverter.GetBytes(first);
            var bytes2 = BitConverter.GetBytes(second);
            result = new byte[] { bytes1[0], bytes1[1], bytes2[0], bytes2[1] };
            return result;
        }
    }
}
