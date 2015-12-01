using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Nintenlord.Utility;
using Data_ripper;

namespace NLightmare.Nightmare_Modules
{
    class Module
    {
        ModuleVersion version;
        string description;
        int offset;
        int entryCount;
        int length;
        string[] entrySelectorFile;
        Table tableFile;
        List<Entry> entries;
        
        public Module(string path)
        {
            using (StreamReader reader = File.OpenText(path))
            {
                ReadHeader(reader);
            }
        }

        private void ReadHeader(TextReader reader)
        {
            string version = ReadNextLine(reader);
            string description = ReadNextLine(reader);
            string offset = ReadNextLine(reader);
            string entryCount = ReadNextLine(reader);
            string length = ReadNextLine(reader);
            string entryList = ReadNextLine(reader);
            string tableFile = ReadNextLine(reader);

            if (version == "1")
            {
                //Will be changed to XeldZahlman if needed
                this.version = ModuleVersion.Original;
            }
            else if (version == "2")
            {
                this.version = ModuleVersion.NLight;
            }

            this.description = description;

            this.offset = offset.GetValue();
            this.entryCount = entryCount.GetValue();
            this.length = length.GetValue();

            if (entryList != "NULL")
            {
                this.entrySelectorFile = File.ReadAllLines(entryList);
            }
            if (tableFile != "NULL")
            {
                this.tableFile = new Table(tableFile);
            }
        }
        
        private void ReadEntry(TextReader reader)
        {
            while (true)
            {
                string line = ReadNextLine(reader);
                if (line == null)
                    break;
                Entry newEntry = new Entry();
                newEntry.name = line;

                line = ReadNextLine(reader);
                newEntry.bitIndex = GetValue(line);

                line = ReadNextLine(reader);
                newEntry.bitLength = GetValue(line);
                
                line = ReadNextLine(reader);
                newEntry.SetTypeParameters(line);
                //Do type

                line = ReadNextLine(reader);
                if (line != "NULL")
                {
                    //My version will ignore the first line ;)
                    newEntry.entries = GetEntries(line, version == ModuleVersion.NLight);
                }
            }
        }


        private Dictionary<int, string> GetEntries(string path, bool ignoreFirstLine)
        {
            var result = new Dictionary<int, string>();
            using (StreamReader sr = new StreamReader(path))
            {
                if (ignoreFirstLine)
                {
                    sr.ReadLine();
                }
                int i = 0;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] lineSplit = line.Split(new char[1] { ' ' }, 2);
                    int value = lineSplit[0].GetValue();
                    if (lineSplit.Length == 2)
                    {
                        result.Add(value, lineSplit[1]);
                    }
                    else
                    {
                        result.Add(value, "");
                    }
                    i++;
                }                
            }
            return result;
        }

        private static int GetValue(string line)
        {
            int mult;
            if (line.EndsWith("b"))
            {
                mult = 1;
                line = line.TrimEnd('b');
            }
            else
            {
                mult = 8;
            }
            int val = line.GetValue() * mult;
            return val;
        }

        static string ReadNextLine(TextReader reader)
        {
            while (true)
            {
                var line = reader.ReadLine();
                if (line == null)
                {
                    return null;
                }
                if (line.ContainsNonWhiteSpace() && !IsComment(line))
                {
                    return line.Trim();
                }
            }
        }

        static bool IsComment(string line)
        {
            return line.StartsWith("#");
        }

        public class Entry
        {
            public string name;
            public int bitIndex;
            public int bitLength;
            public EntryType type;
            public bool hex;
            public int bitGrouping;
            public bool useEntries;
            public Dictionary<int, string> entries;

            public void SetTypeParameters(string line)
            {
                if (line == "TEXT")
                {
                    type = EntryType.Text;
                    hex = false;
                    bitGrouping = 0;
                    useEntries = false;
                }
                else if (line == "HEXA")
                {
                    type = EntryType.UnsignedNumber;
                    hex = true;
                    bitGrouping = 8;
                    useEntries = false;
                }
                else
                {
                    type = line[3] == 'S' ? EntryType.SignedNumber : EntryType.UnsignedNumber;
                    hex = line[2] == 'H';
                    bitGrouping = 0;
                    useEntries = line[1] != 'E';
                }
            }
        }

        public enum EntryType
        {
            Text,
            SignedNumber,
            UnsignedNumber,
            Boolean
        }
    }
}
