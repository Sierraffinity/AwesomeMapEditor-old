// Awesome Map Editor
// A map editor for GBA Pokémon games.

// Copyright (C) 2015 Diegoisawesome

// This file is part of Awesome Map Editor.
// Awesome Map Editor is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Awesome Map Editor is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with Awesome Map Editor. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using static PGMEBackend.Program;

namespace PGMEBackend
{
    public static class Config
    {
        public static AMESettings settings;
        public static GameList gameList;
        public static CharTable charTable;
        public static BehaviorLists behaviorLists;
        public static MusicLists musicLists;

        static string configFile = AppDomain.CurrentDomain.BaseDirectory + @"AME.yaml";
        static string gamesFile = AppDomain.CurrentDomain.BaseDirectory + @"Games.yaml";
        static string tableFile = AppDomain.CurrentDomain.BaseDirectory + @"Table.yaml";
        static string behaviorBytesFile = AppDomain.CurrentDomain.BaseDirectory + @"BehaviorBytes.yaml";
        static string musicListFile = AppDomain.CurrentDomain.BaseDirectory + @"Music.yaml";

        public static int ReadConfig()
        {
            var deserializer = new Deserializer();
            try {
                using (StreamReader input = new StreamReader(configFile))
                {
                    settings = deserializer.Deserialize<AMESettings>(input);
                }
            }
            catch (FileNotFoundException ex)
            {
                FileNotFound(ex);
                return -1;
            }
            catch (YamlException ex)
            {
                
                ShowMessageBox(rmInternalStrings.GetString("UnableToReadConfig") + " " + configFile + ".\n\n" + ex.Message, rmInternalStrings.GetString("UnableToReadConfigTitle"), "OK", "Error");
                return -1;
            }
            try
            {
                using (StreamReader input = new StreamReader(gamesFile))
                {
                    gameList = deserializer.Deserialize<GameList>(input);
                }
            }
            catch (FileNotFoundException ex)
            {
                FileNotFound(ex);
                return -1;
            }
            catch (YamlException ex)
            {
                
                ShowMessageBox(rmInternalStrings.GetString("UnableToReadConfig") + " " + gamesFile + ".\n\n" + ex.Message, rmInternalStrings.GetString("UnableToReadConfigTitle"), "OK", "Error");
                return -1;
            }
            try
            {
                using (StreamReader input = new StreamReader(tableFile))
                {
                    charTable = deserializer.Deserialize<CharTable>(input);
                }
            }
            catch (FileNotFoundException ex)
            {
                FileNotFound(ex);
                return -1;
            }
            catch (YamlException ex)
            {
                
                ShowMessageBox(rmInternalStrings.GetString("UnableToReadConfig") + " " + tableFile + ".\n\n" + ex.Message, rmInternalStrings.GetString("UnableToReadConfigTitle"), "OK", "Error");
                return -1;
            }
            try
            {
                using (StreamReader input = new StreamReader(behaviorBytesFile))
                {
                    behaviorLists = deserializer.Deserialize<BehaviorLists>(input);
                }
            }
            catch (FileNotFoundException ex)
            {
                FileNotFound(ex);
                return -1;
            }
            catch (YamlException ex)
            {
                
                ShowMessageBox(rmInternalStrings.GetString("UnableToReadConfig") + " " + behaviorBytesFile + ".\n\n" + ex.Message, rmInternalStrings.GetString("UnableToReadConfigTitle"), "OK", "Error");
                return -1;
            }
            try
            {
                using (StreamReader input = new StreamReader(musicListFile))
                {
                    musicLists = deserializer.Deserialize<MusicLists>(input);
                }
            }
            catch (FileNotFoundException ex)
            {
                FileNotFound(ex);
                return -1;
            }
            catch (YamlException ex)
            {
                
                ShowMessageBox(rmInternalStrings.GetString("UnableToReadConfig") + " " + musicListFile + ".\n\n" + ex.Message, rmInternalStrings.GetString("UnableToReadConfigTitle"), "OK", "Error");
                return -1;
            }
            return 0;
        }

        public static void WriteConfig()
        {
            var serializer = new Serializer();
            
            // Serialize the document
            using(StreamWriter writer = new StreamWriter(File.Open(configFile, FileMode.Create, FileAccess.Write)))
            {
                writer.WriteLine("---");
                serializer.Serialize(writer, settings);
                writer.WriteLine("...");
            }
        }
        
        public class AMESettings
        {
            public bool ShowSprites { get; set; }
            public bool ShowGrid { get; set; }
            public bool UsePlugins { get; set; }
            public string ScriptEditor { get; set; }
            public int PermissionTranslucency { get; set; }
            public string Language { get; set; }
            public bool CreateBackups { get; set; }
            public string MapSortOrder { get; set; }
            private string _hexPrefix;
            public string HexPrefix { get { return _hexPrefix; }
                                      set { _hexPrefix = value.Substring(0, (value.Length >= 2) ? 2 : value.Length); } }
            public bool ShowRawMapHeader { get; set; }
            public bool ShowRawLayoutHeader { get; set; }
            public LinkedList<string> RecentlyUsedFiles { get; set; }
        }

        public class BehaviorLists
        {
            public Dictionary<string, SortedDictionary<int, string>> Behaviors { get; set; }
        }

        public class MusicLists
        {
            public Dictionary<string, SortedDictionary<int, string>> Songs { get; set; }
        }

        public class GameList
        {
            public Dictionary<string, Game> Games { get; set; }
        }


        public class CharTable
        {
            public Dictionary<int, string> Table { get; set; }
        }

        public class Game
        {
            public string RomCode { get; set; }
            public string RomType { get; set; }
            public string Name { get; set; }
            public string Language { get; set; }

            public short PokemonCount { get; set; }
            public int PokemonNames { get; set; }
            public int PokemonPics { get; set; }
            public int PokemonPals { get; set; }
            public short PokemonPicCount { get; set; }

            public int ItemNames { get; set; }

            public int MapBanks { get; set; }
            public int MapLayouts { get; set; }
            public short MapLayoutCount { get; set; }
            public int MapNames { get; set; }
            public int MapNameStart;
            public int MapNameCount { get; set; }
            public int MapNameTotal { get; set; }
            public byte MapBankCount { get; set; }
            public byte[] MapBankSizes { get; set; }

            public int WildPokemon { get; set; }

            public int SpriteBase { get; set; }
            public int SpriteColors { get; set; }
            public int SpriteNormalSet { get; set; }
            public int SpriteSmallSet { get; set; }
            public int SpriteLargeSet { get; set; }
            public byte NumSprites { get; set; }
            
            public int MainTSPalCount { get; set; }
            public int MainTSBlocks { get; set; }
            public int LocalTSBlocks { get; set; }
            public short MainTSSize { get; set; }
            public short LocalTSSize { get; set; }

            public byte WorldMapCount { get; set; }
            public int[] WorldMapGFX { get; set; }
            public int[] WorldMapTileMap { get; set; }
            public int[] WorldMapPal { get; set; }

            public int FreespaceStart { get; set; }
            public byte FreespaceByte { get; set; }
            
            public IDictionary<int, string> Songs;

            public Game DereferencePointers()
            {
                var game = (Game)MemberwiseClone();
                game.PokemonNames = ROM.ReadPointer(game.PokemonNames);
                game.PokemonPics = ROM.ReadPointer(game.PokemonPics);
                game.PokemonPals = ROM.ReadPointer(game.PokemonPals);
                game.ItemNames = ROM.ReadPointer(game.ItemNames);
                game.MapBanks = ROM.ReadPointer(game.MapBanks);
                game.MapLayouts = ROM.ReadPointer(game.MapLayouts);
                game.MapNames = ROM.ReadPointer(game.MapNames);
                game.MapNameTotal = ROM.GetData(game.MapNameTotal, 1)[0];
                if (game.RomType == "FRLG")
                    game.MapNameTotal--;
                if (game.MapNameCount != 0)
                    game.MapNameCount = ROM.GetData(game.MapNameCount, 1)[0];
                else
                    game.MapNameCount = game.MapNameTotal;
                game.MapNameStart = (byte)(game.MapNameTotal - game.MapNameCount);
                game.WildPokemon = ROM.ReadPointer(game.WildPokemon);
                game.SpriteBase = ROM.ReadPointer(game.SpriteBase);
                game.SpriteColors = ROM.ReadPointer(game.SpriteColors);
                game.SpriteNormalSet = ROM.ReadPointer(game.SpriteNormalSet);
                game.SpriteSmallSet = ROM.ReadPointer(game.SpriteSmallSet);
                game.SpriteLargeSet = ROM.ReadPointer(game.SpriteLargeSet);
                return game;
            }
        }
    }
}
