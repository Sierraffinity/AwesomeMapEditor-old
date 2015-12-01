using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Nintenlord.ROMHacking.Utility;

namespace Nintenlord.HackingProjectManager
{
    interface IDataHandler
    {
        string Name
        {
            get;
        }

        bool CanInsert { get; }
        bool CanExtract { get; }

        CanCauseError Extract(string[] parameters, BinaryReader reader);
        CanCauseError<int[]> Insert(string[] parameters, BinaryWriter writer);
    }
}
