using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Event_Assembler.Core.Code.Templates;

namespace Nintenlord.Event_Assembler.Core.Code.Language
{
    /// <summary>
    /// Code language for FE6
    /// </summary>
    public static class FE6CodeLanguage
    {
        public static readonly string Name = "FE6";
        public static readonly Tuple<string, List<Priority>>[][] PointerList =
            new Tuple<string, List<Priority>>[][]
            {
                new Tuple<string, List<Priority>>[]{
                    new Tuple<string, List<Priority>>("TurnBasedEvents", EACodeLanguage.MainPriorities)
                },
                new  Tuple<string, List<Priority>>[]{
                    new Tuple<string, List<Priority>>("CharacterBasedEvents", EACodeLanguage.MainPriorities),
                },
                new  Tuple<string, List<Priority>>[]{
                    new Tuple<string, List<Priority>>("LocationBasedEvents", EACodeLanguage.MainPriorities),
                },
                new  Tuple<string, List<Priority>>[]{
                    new Tuple<string, List<Priority>>("MiscBasedEvents", EACodeLanguage.MainPriorities),
                },
                new  Tuple<string, List<Priority>>[]{
                    new Tuple<string, List<Priority>>("EnemyUnits", EACodeLanguage.UnitPriorities),
                    new Tuple<string, List<Priority>>("AllyUnits", EACodeLanguage.UnitPriorities),
                },
                new  Tuple<string, List<Priority>>[]{
                    new Tuple<string, List<Priority>>("EndingScene", EACodeLanguage.NormalPriorities)
                }
            };
    }
}
