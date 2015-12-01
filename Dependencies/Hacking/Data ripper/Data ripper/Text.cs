using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Data_ripper
{
    static class Text
    {
        public class table
        {
            byte[][] rawData;
            char[][] textData;
            int maxByteDepth = 0;
            int maxCharDebth = 0;

            public table(string path)
            {
                List<byte[]> LrawData = new List<byte[]>();
                List<char[]> LtextData = new List<char[]>();

                StreamReader sr = new StreamReader(path);
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line.Contains("="))
                    {
                        int splitValue = line.IndexOf('=');
                        int value = Convert.ToInt32(line.Substring(0, splitValue), 16);
                        string character = line.Substring(splitValue + 1, line.Length - splitValue - 1);
                        char[] characters = character.ToCharArray();
                        byte[] bytes = getBytes(value);

                        if (maxByteDepth < bytes.Length)
                            maxByteDepth = bytes.Length;
                        if (maxCharDebth < characters.Length)
                            maxCharDebth = characters.Length;

                        LrawData.Add(bytes);
                        LtextData.Add(characters);
                    }
                }
                rawData = LrawData.ToArray();
                textData = LtextData.ToArray();
                sr.Close();
            }

            public table(string[] textValues, int[] values)
            {
                if (textValues.Length != values.Length)
                    throw new ArgumentException("Arrays have different amount of items.");

                rawData = new byte[textValues.Length][];
                textData = new char[textValues.Length][];

                for (int i = 0; i < textValues.Length; i++)
                {
                    char[] characters = textValues[i].ToCharArray();
                    byte[] bytes = getBytes(values[i]);

                    if (maxByteDepth < bytes.Length)
                        maxByteDepth = bytes.Length;
                    if (maxCharDebth < characters.Length)
                        maxCharDebth = characters.Length;

                    textData[i] = characters;
                    rawData[i] = bytes;
                }
            }

            public table(byte[][] rawData, char[][] textData)
            {
                if (rawData.Length != textData.Length)
                    throw new ArgumentException("Arrays have different amount of items.");
                for (int i = 0; i < rawData.Length; i++)
                {
                    if (maxByteDepth < rawData[i].Length)
                        maxByteDepth = rawData[i].Length;
                    if (maxCharDebth < textData[i].Length)
                        maxCharDebth = textData[i].Length;
                }

                this.rawData = rawData;
                this.textData = textData;
            }


            public string getText(byte[] data)
            {
                string text = "";
                int i = 0;
                
                while (i < data.Length)
                {
                    int bestIndex = -1;
                    int highestAmountOfGoodBytes = 0;
                    for (int ii = 0; ii < this.rawData.Length; ii++)
                    {
                        List<byte> nextValuesL = new List<byte>();

                        for (int index = 0; (index < rawData.Length) && (i + index < data.Length) && (index < maxByteDepth); index++)
                        {
                            nextValuesL.Add(data[i + index]);
                        }
                        byte[] nextValues = nextValuesL.ToArray();

                        int amountOfGoodBytes = ArrayExtensions.AmountOfSame<byte>(rawData[ii], 0, nextValues, 0);// amountOfGoodbytes(ii, nextValues);

                        if (amountOfGoodBytes > highestAmountOfGoodBytes)
                        {
                            bestIndex = ii;
                            highestAmountOfGoodBytes = amountOfGoodBytes;
                        }
                    }

                    if (highestAmountOfGoodBytes > 0)
                    {
                        text += new string(textData[bestIndex]);
                        i += rawData[bestIndex].Length;
                    }
                }

                return text;
            }

            public byte[] getData(string text)
            {
                List<byte> data = new List<byte>();

                int i = 0;
                while (i < text.Length)
                {
                    int bestIndex = -1;
                    int highestAmountOfGoodChars = 0;

                    for (int u = 0; u < this.textData.Length; u++)
                    {
                        string nextValues;
                        if (i + maxCharDebth >= text.Length)
                            nextValues = text.Substring(i);
                        else
                            nextValues = text.Substring(i, maxCharDebth);
                        int amountOfGoodchars = ArrayExtensions.AmountOfSame<char>(textData[u], 0, nextValues.ToCharArray(), 0);

                        if (amountOfGoodchars > highestAmountOfGoodChars)
                        {
                            bestIndex = u;
                            highestAmountOfGoodChars = amountOfGoodchars;
                        }
                    }

                    if (highestAmountOfGoodChars > 0)
                    {
                        data.AddRange(rawData[bestIndex]);
                        i += textData[bestIndex].Length;
                    }
                }

                return data.ToArray();
            }

            private byte[] getBytes(long i)
            {
                List<byte> list = new List<byte>(BitConverter.GetBytes(i));

                while (list.Count > 1 && list[list.Count - 1] == 0)
                {
                    list.RemoveAt(list.Count - 1);
                }

                return list.ToArray();
            }
        }
    }
}
