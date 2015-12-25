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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nintenlord.ROMHacking.GBA;
using Nintenlord.ROMHacking.GBA.Compressions;
using Nintenlord.ROMHacking.GBA.Graphics;
using System.Resources;

namespace PGMEBackend
{
    static class Program
    {
        static internal GBAROM ROM;
        static public Config.Game currentGame;
        public static UIInteractionLayer mainGUI;
        static string programTitle;
        public static IDictionary<int, string> FRLGBehaviorBytes;
        public static IDictionary<int, MapBank> mapBanks;
        public static IDictionary<int, MapName> mapNames;
        public static IDictionary<int, MapLayout> mapLayouts;
        public static IDictionary<int, MapTileset> mapTilesets;

        public static List<Exception> loadExceptions;

        public static int maxLayout;
        public static int mapLayoutNotFoundCount;
        public static bool extraLayoutsLoaded;
        public static bool isEdited = false;
        public static int timeOfDay = 2;

        static string currentFilePath;
        static string currentFileName;

        public static ResourceManager rmInternalStrings = InternalStrings.ResourceManager;
        public static ResourceManager rmResources = Properties.Resources.ResourceManager;

        public static Map currentMap;
        public static MapLayout currentLayout;

        public static GLControls.GLMapEditor glMapEditor;
        public static GLControls.GLBlockChooser glBlockChooser;
        public static GLControls.GLPermsChooser glPermsChooser;
        public static GLControls.GLBorderBlocks glBorderBlocks;
        public static GLControls.GLEntityEditor glEntityEditor;

        public static bool showingPerms = false;

        static void Main()
        {
            //Initialize();
        }

        public static void Initialize(UIInteractionLayer guiMain)
        {
            mainGUI = guiMain;
            ROM = new GBAROM();
            FRLGBehaviorBytes = new Dictionary<int, string>();
        }

        public static void SetMainGUITitle(string title)
        {
            programTitle = title;
        }

        public static void LoadROM()
        {
            string filename = mainGUI.ShowFileOpenDialog("Open ROM", "GBA ROM Files|*.gba|Binary Files|*.bin|All Files|*", false);
            if (filename.Length > 0)
            {
                LoadROM(filename);
            }
        }

        public static void ReloadROM()
        {
            LoadROM(currentFilePath);
        }

        public static void LoadROM(string filename)
        {
            if (ROM.Edited || isEdited)
            {
                string result = ShowMessageBox(rmInternalStrings.GetString("UnsavedChanges"), rmInternalStrings.GetString("UnsavedChangesTitle"), "YesNoCancel", "Warning");
                if (result == "Yes")
                    SaveROM();
                else if (result == "Cancel")
                    return;
            }

            Stopwatch loadTime = new Stopwatch();
            loadTime.Start();

            //Put all loading code between here and "Stop timer" comment

            if (OpenROM(filename) == 0)
            {
                mainGUI.AddRecentFile(filename);
                currentFilePath = filename;
                currentFileName = Path.GetFileName(filename);

                // You gotta initialize it all backwards so you can reference it
                loadExceptions = new List<Exception>();
                maxLayout = 0;
                mapLayoutNotFoundCount = 0;
                mainGUI.ClearMapNodes();
                extraLayoutsLoaded = false;
                currentLayout = null;

                mapTilesets = new SortedDictionary<int, MapTileset>();
                mapLayouts = new Dictionary<int, MapLayout>();
                LoadMapLayouts();
                LoadMapNames();
                LoadMapBanks();

                mainGUI.EnableControlsOnROMLoad();
                mainGUI.LoadMapNodes();
                mainGUI.LoadMapDropdowns();
                currentGame.Songs = Config.musicLists.Songs[currentGame.RomType];
                mainGUI.LoadMusicDropdowns();

                //Stop timer
                loadTime.Stop();

                TimeSpan ts = loadTime.Elapsed;
                string elapsedTime = ts.Seconds + "." + (ts.Milliseconds / 10);

                mainGUI.SetTitleText(programTitle + " | " + currentFileName);
                mainGUI.SetLoadingStatus(string.Format(rmInternalStrings.GetString("ROMLoadedStatus"), currentFileName, elapsedTime));

                //Oh goodie, all the errors

                foreach (Exception ex in loadExceptions)
                {
                    if (ex is TilesetLoadErrorException)
                        ShowMessageBox(ex.Message, rmInternalStrings.GetString("CouldNotReadTilesetTitle"), "OK", "Warning");
                    else if (ex is MapLayoutLoadErrorException)
                        ShowMessageBox(ex.Message, rmInternalStrings.GetString("CouldNotReadTilesetTitle"), "OK", "Warning");
                }

                if (mapLayoutNotFoundCount > 0)
                    ShowMessageBox(string.Format(rmInternalStrings.GetString("CouldNotFindLayout"), mapLayoutNotFoundCount), rmInternalStrings.GetString("CouldNotFindLayoutTitle"), "OK", "Warning");

            }
            else
            {
                loadTime.Stop();
            }
        }

        public static int OpenROM(string path)
        {
            
            if (ROM.Opened)
            {
                //WriteDatas(rawGraphics.Edited, rawPalette.Edited, rawTSA.Edited);
            }

            try
            {
                ROM.OpenROM(path);
            }
            catch (IOException ex)
            {
                IOException(ex);
                return -1;
            }
            try
            {
                currentGame = Config.gameList.Games[ROM.GameCode].DereferencePointers();
            }
            catch(KeyNotFoundException)
            {
                ShowMessageBox(string.Format(rmInternalStrings.GetString("ROMCodeNotFound"), ROM.GameCode), rmInternalStrings.GetString("ROMCodeNotFoundTitle"), "OK", "Warning");
                return -1;
            }
            return 0;
        }

        public static void SaveROM()
        {

        }

        public static void IOException(Exception ex)
        {
            
            ShowMessageBox(ex.Message, rmInternalStrings.GetString("IOExceptionTitle"), "OK", "Error");
        }

        public static void FileNotFound(Exception ex)
        {
            
            ShowMessageBox(ex.Message, rmInternalStrings.GetString("FileNotFoundTitle"), "OK", "Error");
        }
        
        public static string ShowMessageBox(string body, string title)
        {
            return mainGUI.ShowMessageBox(body, title);
        }

        public static string ShowMessageBox(string body, string title, string buttons)
        {
            return mainGUI.ShowMessageBox(body, title, buttons);
        }

        public static string ShowMessageBox(string body, string title, string buttons, string icon)
        {
            return mainGUI.ShowMessageBox(body, title, buttons, icon);
        }

        private static void LoadMapBanks()
        {
            
            mapBanks = new Dictionary<int, MapBank>();
            try
            {
                int romMapBank = currentGame.MapBanks;
                int mapBankCount = currentGame.MapBankCount;
                byte[] mapBankSizes = currentGame.MapBankSizes;
                for (int currentBank = 0; currentBank < mapBankCount; currentBank++)
                {
                    mapBanks.Add(currentBank, new MapBank());
                    for (int currentMap = 0, bankScan = ROM.ReadPointer(romMapBank); currentMap < mapBankSizes[currentBank]; currentMap++)
                    {
                        mapBanks[currentBank].AddMap(currentMap, new Map(bankScan, ROM, currentBank, currentMap));
                        bankScan += 4;
                    }
                    romMapBank += 4;
                }
            }
            catch (ArgumentException)
            {
                throw;
            }

            if (maxLayout > 0)
                TryToLoadExtraLayouts();
        }

        public static bool TryToLoadExtraLayouts()
        {
            mapLayouts = new Dictionary<int, MapLayout>();
            int layoutCount = 0;
            for (int layouts = currentGame.MapLayouts; layoutCount < maxLayout; layouts += 4, layoutCount++)
            {
                int pointer = ROM.ReadPointer(layouts);
                byte[] sequence = ROM.GetData(layouts, 0x4);
                if (pointer != 0 && pointer < 0x200000 && pointer > 0x2000000)
                    break;
                if (sequence.SequenceEqual(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }))
                    break;
                if (sequence.SequenceEqual(new byte[] { 0x7F, 0x7F, 0x7F, 0x7F }))
                    break;
            }
            if (layoutCount <= currentGame.MapLayoutCount)
                return false;
            currentGame.MapLayoutCount = (short)(layoutCount);
            LoadMapLayouts();
            foreach (MapBank bank in mapBanks.Values)
            {
                foreach (Map map in bank.GetBank().Values)
                {
                    if (map.layout == null && mapLayouts.ContainsKey(map.mapLayoutIndex))
                    {
                        mapLayoutNotFoundCount--;
                        map.layout = mapLayouts[map.mapLayoutIndex];
                    }
                }
            }
            extraLayoutsLoaded = true;
            return true;
        }

        public static int LoadMap(object map)
        {
            if (isEdited)
            {
                string result = UnsavedChangesDialog();
                if (result == "Yes")
                    SaveMap();
                else if (result == "No")
                    currentLayout.LoadLayoutHeaderFromRaw(ROM);
                else if (result == "Cancel")
                    return 1;
                isEdited = false;
            }

            Stopwatch loadTime = new Stopwatch();
            UndoManager.Clear();
            loadTime.Start();

            mainGUI.EnableControlsOnMapLoad();
            if (currentLayout != null)
            {
                currentLayout.Unload();
            }

            if (map is Map)
            {
                currentMap = (Map)map;
                currentLayout = ((Map)map).layout;
            }
            else if (map is MapLayout)
            {
                currentMap = null;
                currentLayout = (MapLayout)map;
            }

            mainGUI.LoadMap(map);

            //Stop timer
            loadTime.Stop();

            TimeSpan ts = loadTime.Elapsed;
            string elapsedTime = ts.Seconds + "." + (ts.Milliseconds / 10);

            mainGUI.SetTitleText(programTitle + " | " + currentFileName + " | " + ((currentMap != null) ? currentMap.name : currentLayout.name));
            mainGUI.SetLoadingStatus(string.Format(rmInternalStrings.GetString("MapLoadedStatus"), (currentMap != null) ? currentMap.name : currentLayout.name, elapsedTime));
            return 0;
        }

        public static void SaveMap()
        {
            currentLayout.WriteLayoutToRaw();
            isEdited = false;
        }
        
        private static void LoadMapNames()
        {
            mapNames = new Dictionary<int, MapName>();
            try
            {
                int mapTotal = currentGame.MapNameTotal;
                int currMap = currentGame.MapNameStart;
                int romMapNames = currentGame.MapNames;
                if (currentGame.RomType == "E")
                    romMapNames += 4;
                while (currMap <= mapTotal)
                {
                    mapNames.Add(currMap, new MapName(ROMCharactersToString(ROM.ReadPointer(romMapNames))));
                    if (currentGame.RomType == "FRLG")
                        romMapNames += 4;
                    else if (currentGame.RomType == "E")
                        romMapNames += 8;
                    currMap++;
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        private static void LoadMapLayouts()
        {
            try
            {
                for (int currLayout = 0, layoutCount = currentGame.MapLayoutCount, layouts = currentGame.MapLayouts; currLayout < layoutCount; currLayout++, layouts += 4)
                {
                    if(ROM.ReadPointer(layouts) != 0 && !mapLayouts.ContainsKey(currLayout + 1))
                        mapLayouts.Add(currLayout + 1, new MapLayout(currLayout + 1, ROM.ReadPointer(layouts), ROM));
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        private static string ROMCharactersToString(int baseLocation)
        {
            string s = "";
            byte character = 0;
            while(character != 0xFF)
            {
                character = ROM.GetData(baseLocation++, 1)[0];
                switch (character)
                {
                    /*
                    case 0x53:
                        s += "PK";
                        break;
                    case 0x54:
                        s += "MN";
                        break;
                    */
                    case 0xFD:
                        int bufferNum = ROM.GetData(baseLocation++, 1)[0];
                        switch (bufferNum)
                        {
                            case 0x1:
                                s += "[player]";
                                break;
                            case 0x6:
                                s += "[rival]";
                                break;
                            case 0x7:
                                s += "[game]";
                                break;
                            case 0x8:
                                s += "[team]";
                                break;
                            case 0x9:
                                s += "[otherteam]";
                                break;
                            case 0xA:
                                s += "[teamleader]";
                                break;
                            case 0xB:
                                s += "[otherteamleader]";
                                break;
                            case 0xC:
                                s += "[legend]";
                                break;
                            case 0xD:
                                s += "[otherlegend]";
                                break;
                            default:
                                s += "[buffer" + bufferNum + "]";
                                break;
                        }
                        break;
                    case 0xFF:
                        break;
                    default:
                        s += InterpretChar(character);
                        break;
                }
            }
            return (s != string.Empty) ? s : "[empty string]";
        }

        private static string ROMCharactersToString(int baseLocation, int maxLength)
        {
            string s = "";
            for (int j = 0; j < maxLength; j++)
            {
                byte character = ROM.GetData(baseLocation + j++, 1)[0];
                if (character != 0xFF)
                {
                    switch (character)
                    {
                        /*
                        case 0x53:
                            s += "PK";
                            break;
                        case 0x54:
                            s += "MN";
                            break;
                        */
                        case 0xFD:
                            int bufferNum = ROM.GetData(baseLocation++, 1)[0];
                            switch (bufferNum)
                            {
                                case 0x1:
                                    s += "[player]";
                                    break;
                                case 0x6:
                                    s += "[rival]";
                                    break;
                                case 0x7:
                                    s += "[game]";
                                    break;
                                case 0x8:
                                    s += "[team]";
                                    break;
                                case 0x9:
                                    s += "[otherteam]";
                                    break;
                                case 0xA:
                                    s += "[teamleader]";
                                    break;
                                case 0xB:
                                    s += "[otherteamleader]";
                                    break;
                                case 0xC:
                                    s += "[legend]";
                                    break;
                                case 0xD:
                                    s += "[otherlegend]";
                                    break;
                                default:
                                    s += "[buffer" + bufferNum + "]";
                                    break;
                            }
                            break;
                        case 0xFF:
                            break;
                        default:
                            s += InterpretChar(character);
                            break;
                    }
                }
                else
                {
                    break;
                }
            }
            return s;
        }

        private static string InterpretChar(byte character)
        {
            string temp;
            if (Config.charTable.Table.TryGetValue(character, out temp))
                return temp;
            else
            {
                return '[' + character.ToString("X2") + ']';
            }
        }
        
        public static string UnsavedChangesQuitDialog()
        {
            return ShowMessageBox(rmInternalStrings.GetString("UnsavedChangesExit"), rmInternalStrings.GetString("UnsavedChangesTitle"), "YesNoCancel", "Warning");
        }

        public static string UnsavedChangesDialog()
        {
            return ShowMessageBox(rmInternalStrings.GetString("UnsavedChanges"), rmInternalStrings.GetString("UnsavedChangesTitle"), "YesNoCancel", "Warning");
        }

        public static void ChangePermsVisibility(bool showPerms)
        {
            showingPerms = showPerms;
            glMapEditor.RedrawAllChunks();
        }
    }
    
    public enum MapEditorTools
    {
        None,
        Pencil,
        Eyedropper,
        Fill,
        FillAll
    }
}
