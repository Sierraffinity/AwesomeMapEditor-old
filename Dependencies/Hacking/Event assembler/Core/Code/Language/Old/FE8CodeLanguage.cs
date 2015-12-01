using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Event_Assembler.Core.Code.Templates;

namespace Nintenlord.Event_Assembler.Core.Code.Language
{
    /// <summary>
    /// Code language for FE8
    /// </summary>
    public static class FE8CodeLanguage
    {
        public static readonly string Name = "FE8";
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
                new Tuple<string, List<Priority>>[]{
                    new Tuple<string, List<Priority>>("Dunno1", EACodeLanguage.MainPriorities),
                    new Tuple<string, List<Priority>>("Dunno2", EACodeLanguage.MainPriorities),
                    new Tuple<string, List<Priority>>("Dunno3", EACodeLanguage.MainPriorities),
                    new Tuple<string, List<Priority>>("Tutorial", EACodeLanguage.MainPriorities),
                },
                new Tuple<string, List<Priority>>[]{
                    new Tuple<string, List<Priority>>("Traps1",EACodeLanguage.TrapPriorities),
                    new Tuple<string, List<Priority>>("Traps2",EACodeLanguage.TrapPriorities),                
                },
                new Tuple<string, List<Priority>>[]{
                    new Tuple<string, List<Priority>>("Units1",EACodeLanguage.UnitPriorities),
                    new Tuple<string, List<Priority>>("Units2",EACodeLanguage.UnitPriorities),                
                },
                new Tuple<string, List<Priority>>[]{
                    new Tuple<string, List<Priority>>("SkirmishUnitsAlly1", EACodeLanguage.UnitPriorities),
                    new Tuple<string, List<Priority>>("SkirmishUnitsAlly2", EACodeLanguage.UnitPriorities),
                    new Tuple<string, List<Priority>>("SkirmishUnitsAlly3", EACodeLanguage.UnitPriorities),                
                },
                new Tuple<string, List<Priority>>[]{
                    new Tuple<string, List<Priority>>("SkirmishUnitsEnemy1", EACodeLanguage.UnitPriorities),
                    new Tuple<string, List<Priority>>("SkirmishUnitsEnemy2", EACodeLanguage.UnitPriorities),
                    new Tuple<string, List<Priority>>("SkirmishUnitsEnemy3", EACodeLanguage.UnitPriorities),                
                },
                new Tuple<string, List<Priority>>[]{
                    new Tuple<string, List<Priority>>("BeginningScene",EACodeLanguage.NormalPriorities),
                    new Tuple<string, List<Priority>>("EndingScene",EACodeLanguage.NormalPriorities)
                }
            };
    }
}
