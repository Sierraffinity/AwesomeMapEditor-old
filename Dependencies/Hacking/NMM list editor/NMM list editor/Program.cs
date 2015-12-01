using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace Nintenlord
{
    static class Program
    {
        static public void Main(string[] args)
        {
            StreamReader sr = new StreamReader(args[0]);
            string temp = sr.ReadToEnd();
            string[] lines = temp.Split(' ');

            if (lines == null)
            {
                Console.WriteLine("Null lines");
                Console.ReadLine();
                return;
            }
            for (int i = 1; i < args.Length; i++)
            {
                lines = joinLines(lines, args[i]);
            }

            StreamWriter sw = new StreamWriter(Path.ChangeExtension(args[0], null));
            for (int i = 0; i < lines.Length; i++)
			{
                sw.WriteLine(lines[i]);
			}
            sw.Close();
        } 

        static void parse(string[] args)
        {
            if (args.Length > 1 && File.Exists(args[1]))
            {
                StreamReader reader = new StreamReader(args[1]);
                string newFile = Path.GetFullPath(args[1]) +
                    Path.GetFileNameWithoutExtension(args[1]) + "1" + Path.GetExtension(args[1]);
                Console.WriteLine(newFile);
                StreamWriter writer = File.CreateText(newFile);
                switch (args[0])
                {
                    case "remove":
                        {
                            reader.ReadLine();
                            while (!reader.EndOfStream)
                            {
                                string line = reader.ReadLine();
                                writer.WriteLine(removeHexValue(line));
                            }
                        }
                        break;
                    case "add":
                        {
                            int lineNumber = 0;
                            List<string> lines = new List<string>();
                            while (!reader.EndOfStream)
                            {
                                string line = reader.ReadLine();
                                lines.Add(addHexValue(line, (byte)lineNumber));
                                lineNumber++;
                            }
                            writer.WriteLine(lineNumber);
                            foreach (string item in lines)
                            {
                                writer.WriteLine(item);
                            }
                        }
                        break;
                    default:
                        break;
                }
                reader.Close();
                writer.Close();
            }
            else
            {
                Console.WriteLine("File " + args[1] + " doesn't exist.");
            }
        }

        static string removeHexValue(string line)
        {
            return line.Remove(0, 5);
        }

        static string addHexValue(string line, byte value)
        {
            string temp = "0x" + Convert.ToString(value, 16) + " ";
            temp += line;
            return temp;
        }

        static string[] XMLParse(string path)
        {
            string beginning = "<song track=\"";
            string firstSpace = "\">";
            string end = "</song>";
            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i].Insert(0, beginning);
                    int spaceInd = lines[i].IndexOf(' ');
                    lines[i].Remove(spaceInd, 1);
                    lines[i].Insert(spaceInd, firstSpace);
                    lines[i] += end;
                }
                return lines;
            }
            return null;
        }

        static string[] joinLines(string[] lines, string file)
        {
            StreamReader sr = new StreamReader(file);
            string temp = sr.ReadToEnd();
            sr.Close();
            string[] newLines = temp.Split('\n');

            int minLength, maxLength;
            if (lines.Length > newLines.Length)
            {
                minLength = newLines.Length;
                maxLength = lines.Length;
            }
            else
            {
                minLength = lines.Length; 
                maxLength = newLines.Length;
            }

            int i;
            string[] result = new string[maxLength];
            for (i = 0; i < minLength; i++)
            {
                int index = newLines[i].IndexOf(' ');
                result[i] = lines[i] + newLines[i].Substring(index + 1);
            }

            if (lines.Length == maxLength)
                while (i < maxLength)
                    result[i] = lines[i++];
            else
                while (i < maxLength)
                    result[i] = newLines[i++];
            return result;
        }
    }
}