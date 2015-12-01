using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Data_ripper
{
    class NightmareModule
    {
        string path;

        int moduleVersion;
        string moduleDescription;
        int rootOffset;
        int numberOfTableEntries;
        int lenghtOfEntries;
        string[] entrySelectorFile;
        Text.table tableFile;

        ModuleEntry[] moduleEntries;

        public NightmareModule(string path)
        {
            this.path = path;
            StreamReader sr = new StreamReader(path);
            string[] lines = StringManipulation.getLines(sr.ReadToEnd());
            sr.Close();

            moduleVersion =         StringManipulation.GenericNumericConverter(lines[0]);
            moduleDescription =                                                lines[1];
            rootOffset =            StringManipulation.GenericNumericConverter(lines[2]);
            numberOfTableEntries =  StringManipulation.GenericNumericConverter(lines[3]);
            lenghtOfEntries =       StringManipulation.GenericNumericConverter(lines[4]);

            if (lines[5] != "NULL")
                entrySelectorFile = File.ReadAllLines(Path.GetDirectoryName(path) + "\\" + lines[5]);

            if (lines[6] != "NULL")
                tableFile = new Text.table(Path.GetDirectoryName(path) + "\\" + lines[6]);

            moduleEntries = new ModuleEntry[(lines.Length - 7) / 5];
            int i = 0;

            while (i < moduleEntries.Length)
            {
                int offset = StringManipulation.GenericNumericConverter(lines[i * 5 + 8]);
                int numberOfBytes = StringManipulation.GenericNumericConverter(lines[i * 5 + 9]);

                string parameterFile = lines[i * 5 + 11];
                if (parameterFile != "NULL")
                    parameterFile = Path.GetDirectoryName(path) + "\\" + lines[i * 5 + 11];

                moduleEntries[i] = new ModuleEntry(lines[i * 5 + 7], offset, numberOfBytes, lines[i * 5 + 10], parameterFile);
                i++;
            }
        }

        public string GetData(byte[] ROM)
        {
            int padding = 3;
            string text = "";

            int[] indexes = GetIndexes(padding);
            text += GetTitle(indexes) + "\n";

            for (int i = 0; i < numberOfTableEntries; i++)
            {
                text += GetLine(ROM, indexes, i) + "\n";
            }
            return text;
        }

        private string GetLine(byte[] ROM, int[] indexes, int i)
        {
            string line;
            if (i < entrySelectorFile.Length)
            {
                line = entrySelectorFile[i];
            }
            else
            {
                line = "";
            }
            line = line.PadRight(indexes[0], ' ');
            int entryOffset = rootOffset + i * lenghtOfEntries;
            for (int u = 0; u < moduleEntries.Length; u++)
            {
                line += moduleEntries[u].ToString(tableFile, ROM, entryOffset);
                line = line.PadRight(indexes[u + 1], ' ');
            }
            return line;
        }

        /// <summary>
        /// Get's the index positions of text colums in the output string
        /// </summary>
        /// <param name="padding">Padding between the columns</param>
        /// <returns>Array of indexes</returns>
        private int[] GetIndexes(int padding)
        {
            int[] indexes = new int[moduleEntries.Length + 1];

            for (int i = 0; i < entrySelectorFile.Length; i++)
                if (entrySelectorFile[i].Length > indexes[0])
                    indexes[0] = entrySelectorFile[i].Length;

            for (int i = 0; i < moduleEntries.Length; i++)
            {
                int addLenght;
                if (moduleEntries[i].Description.Length > moduleEntries[i].MaxParameterLenght)
                    addLenght = moduleEntries[i].Description.Length;
                else
                    addLenght = moduleEntries[i].MaxParameterLenght;

                indexes[i + 1] = indexes[i] + addLenght + padding;
            }
            return indexes;
        }

        public string GetHTMLtable(byte[] ROM)
        {
            string table = "<table>\n";
            table += "<tr>";

            table += "</tr>";
            for (int i = 0; i < numberOfTableEntries; i++)
            {
                table += "<tr>";


                table += "</tr>";
            }
            return table + "</table>";
        }
/*
 <table>
 <tr><th>Food</th><th>Price</th></tr>
 <tr><td>Bread</td><td>$2.99</td></tr>
 <tr><td>Milk</td><td>$1.40</td></tr>
 </table>
 */

        private string GetTitle(int[] indexes)
        {
            string line = "";
            for (int i = 0; i < moduleEntries.Length; i++)
            {
                line = line.PadRight(indexes[i], ' ');
                line += moduleEntries[i].Description;
            }
            return line;
        }

        private class ModuleEntry
        {
            public bool TextBox
            {
                get;
                private set;
            }
            public bool HexArray
            {
                get;
                private set;
            } //not Implemented
            public bool EditBox
            {
                get;
                private set;
            }
            public bool HexaDecimal
            {
                get;
                private set;
            }
            public bool Signed
            {
                get;
                private set;
            }

            public string Description
            {
                get;
                private set;
            }

            public int Offset
            {
                get;
                private set;
            }
            public int NumberOfBytes
            {
                get;
                private set;
            }
            public int MaxParameterLenght
            {
                get;
                private set;
            }

            Dictionary<long, string> parametersD;
            //string[] parameters;
            //long[] parameterValues;

            public ModuleEntry(string description, int offset, int numberOfBytes, string type, string parameterFile)
            {
                this.Description = description;
                this.Offset = offset;
                this.NumberOfBytes = numberOfBytes;
                typeSetter(type);
                this.MaxParameterLenght = 0;
                if (parameterFile != "NULL")
                    setParameters(parameterFile);
            }

            private void setParameters(string path)
            {
                parametersD = new Dictionary<long, string>();

                StreamReader sr = new StreamReader(path);
                int amountOfLines = StringManipulation.GenericNumericConverter(sr.ReadLine());
                int i= 0;
                while (!sr.EndOfStream && i < amountOfLines)
                {
                    string line = sr.ReadLine();
                    string[] lineSplit = line.Split(new char[1] { ' ' }, 2);
                    if (lineSplit.Length == 2)
                    {
                        int value = StringManipulation.GenericNumericConverter(lineSplit[0]);
                        if (this.MaxParameterLenght < lineSplit[1].Length)
                            this.MaxParameterLenght = lineSplit[1].Length;

                        parametersD.Add(value, lineSplit[1]);
                    }
                    i++;
                }
            }

            private void typeSetter(string type)
            {
                if (type == "TEXT")
                    TextBox = true;
                else if (type == "HEXA")
                    HexArray = true;
                else
                {
                    EditBox = type[1] == 'E';
                    HexaDecimal = type[2] == 'H';
                    Signed = type[3] == 'S';
                }
            }

            public string getText(Text.table tableFile, byte[] ROM, int entryOffset)
            {
                if (!TextBox)
                    return null;
                
                byte[] data = new byte[NumberOfBytes];
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = ROM[entryOffset + Offset + i];
                }

                string text;
                if (tableFile == null)
                {
                    char[] charText = new char[data.Length];
                    for (int i = 0; i < charText.Length; i++)
                    {
                        charText[i] = (char)data[i];
                    }
                    text = new string(charText);
                }
                else
                    text = tableFile.getText(data);

                return text;
            }

            public string getValue(byte[] ROM, int entryOffset)
            {
                if (EditBox)
                    return null;

                long value = 0;

                for (int i = 0; i < NumberOfBytes; i++)
                    value += ROM[entryOffset + Offset + i] << (i * 8);

                if (parametersD.ContainsKey(value))
                {
                    return parametersD[value];
                }
                else
                {
                    return "0x" + Convert.ToString(value, 16);
                }
            }

            public long getLongValue(byte[] ROM, int entryOffset)
            {
                if (!EditBox)
                    return 0;

                long value = 0;

                for (int i = 0; i < NumberOfBytes; i++)
                    value += ROM[entryOffset + Offset + i] << (i * 8);

                return value;
            }

            public string ToString(Text.table tableFile, byte[] ROM, int entryOffset)
            {
                string text = "";
                if (HexArray)
                    throw new NotImplementedException("HEXA support not implemented.");
                else if (TextBox)
                    text += getText(tableFile, ROM, entryOffset);
                else
                {
                    if (EditBox)
                    {
                        long value = getLongValue(ROM, entryOffset);

                        if (Signed)
                        {
                            int highestBit = 0;
                            while ((value >> highestBit) != 0)
                                highestBit++;
                            if (highestBit * 8 == NumberOfBytes)
                            {
                                value -= 1 << highestBit;
                            }
                        }

                        int baseV = 10;
                        if (HexaDecimal)
                        {
                            text += "0x";
                            baseV += 6;
                        }

                        text += Convert.ToString(value, baseV);
                    }
                    else
                        text += getValue(ROM, entryOffset);
                }

                return text;
            }
        }
    }
}
