using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core
{
    public static class CoreInfo
    {
        public static string[] DefaultLines(string game, string file, int offset, int? size)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyName name = assembly.GetName();
            Version version = name.Version;

            List<string> lines = new List<string>(new string[] {
                    "Disassembled with Nintenlord's Event Assembler",
                    "Version: " + version.ToString(4),
                    "Game: " + game,
                    "File: " + file,
                    "Offset: " + offset.ToHexString("$")
                });
            if (size.HasValue)
            {
                lines.Add("Size: " + size.Value.ToHexString("0x"));
            }
            return lines.ToArray();
        }
    }
}
