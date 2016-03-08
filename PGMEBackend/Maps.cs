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

using Nintenlord.ROMHacking.GBA;
using OpenTK.Graphics.OpenGL;
using PGMEBackend.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace PGMEBackend
{
    public class Map
    {
        public string name
        {
            get { return "[" + currentBank.ToString("X2") + ", " + currentMap.ToString("X2") + "] " + Program.mapNames[mapNameIndex].name; }
        }

        public byte[] rawHeaderOrig;
        public byte[] rawHeader;
        public byte[] rawEntityHeader;

        public int offset;
        public byte currentBank;
        public byte currentMap;

        public int mapDataPointer;
        public int eventDataPointer;
        public int mapScriptDataPointer;
        public int connectionsDataPointer;
        public short musicNumber;
        public short mapLayoutIndex;
        public byte mapNameIndex;
        public byte visibility;
        public byte weather;
        public byte mapType;
        public byte optionsByte1;
        public byte optionsByte2;
        public byte optionsByte3;
        public byte battleBackground;

        public bool showsName;
        public bool canRun;
        public bool canRideBike;
        public bool canEscape;

        public List<NPC> NPCs;
        public List<Warp> Warps;
        public List<Trigger> Triggers;
        public List<Sign> Signs;

        public MapLayout layout;

        public GBAROM originROM;

        public bool edited
        {
            get { return !rawHeaderOrig.SequenceEqual(rawHeader); }
        }

        public Map()
        {

        }

        public Map(int Offset, GBAROM ROM, int CurrentBank, int CurrentMap)
        {
            offset = Offset;
            originROM = ROM;
            currentBank = (byte)CurrentBank;
            currentMap = (byte)CurrentMap;
            rawHeaderOrig = originROM.GetData(offset, 0x1C);
            rawHeader = (byte[])rawHeaderOrig.Clone();

            LoadMapHeaderFromRaw();

            if (Program.mapLayouts.ContainsKey(mapLayoutIndex))
                layout = Program.mapLayouts[mapLayoutIndex];
            else
            {
                Program.mapLayoutNotFoundCount++;
                if (mapLayoutIndex > Program.maxLayout)
                {
                    Program.maxLayout = mapLayoutIndex;
                }
            }
        }

        public void Revert()
        {
            rawHeader = (byte[])rawHeaderOrig.Clone();
            LoadMapHeaderFromRaw();
            LoadEntitiesFromRaw();
        }

        public void Save()
        {
            WriteMapHeaderToRaw();
            WriteEntitiesToRaw();
            rawHeaderOrig = (byte[])rawHeader.Clone();
        }

        [Flags]
        enum FRLGOptions
        {
            None = 0x0000, CanRideBike = 0x0001, CanEscape = 0x0001, CanRun = 0x0002, ShowsName = 0x0004
        }

        [Flags]
        enum EOptions
        {
            None = 0x0000, CanRideBike = 0x0001, CanEscape = 0x0002, CanRun = 0x0004, ShowsName = 0x0008
        }

        public void LoadMapHeaderFromRaw()
        {
            mapDataPointer = BitConverter.ToInt32(rawHeader, 0x0) - 0x8000000;
            eventDataPointer = BitConverter.ToInt32(rawHeader, 0x4) - 0x8000000;
            mapScriptDataPointer = BitConverter.ToInt32(rawHeader, 0x8) - 0x8000000;
            connectionsDataPointer = BitConverter.ToInt32(rawHeader, 0xC) - 0x8000000;
            musicNumber = BitConverter.ToInt16(rawHeader, 0x10);
            mapLayoutIndex = BitConverter.ToInt16(rawHeader, 0x12);
            mapNameIndex = rawHeader[0x14];
            visibility = rawHeader[0x15];
            weather = rawHeader[0x16];
            mapType = rawHeader[0x17];
            optionsByte1 = rawHeader[0x18];
            optionsByte2 = rawHeader[0x19];
            optionsByte3 = rawHeader[0x1A];
            battleBackground = rawHeader[0x1B];
            if (Program.currentGame.RomType == "FRLG")
            {
                showsName = (optionsByte2 & (int)FRLGOptions.ShowsName) == (int)FRLGOptions.ShowsName;
                canRun = (optionsByte2 & (int)FRLGOptions.CanRun) == (int)FRLGOptions.CanRun;
                canRideBike = (optionsByte1 & (int)FRLGOptions.CanRideBike) == (int)FRLGOptions.CanRideBike;
                canEscape = (optionsByte2 & (int)FRLGOptions.CanEscape) == (int)FRLGOptions.CanEscape;
            }
            else if (Program.currentGame.RomType == "E")
            {
                showsName = (optionsByte3 & (int)EOptions.ShowsName) == (int)EOptions.ShowsName;
                canRun = (optionsByte3 & (int)EOptions.CanRun) == (int)EOptions.CanRun;
                canRideBike = (optionsByte3 & (int)EOptions.CanRideBike) == (int)EOptions.CanRideBike;
                canEscape = (optionsByte3 & (int)EOptions.CanEscape) == (int)EOptions.CanEscape;
            }

            if(eventDataPointer > 0 && eventDataPointer < 0x2000000)
                LoadEntitiesFromRaw();
        }

        public void LoadEntitiesFromRaw()
        {
            rawEntityHeader = originROM.GetData(eventDataPointer, 0x14);

            NPCs = new List<NPC>();
            int npcOffset = BitConverter.ToInt32(rawEntityHeader, 0x4) - 0x8000000;

            if (npcOffset > 0 && npcOffset < 0x2000000)
                for (int i = 0; i < rawEntityHeader[0]; i++)
                    NPCs.Add(new NPC(npcOffset + i * 0x18, originROM));

            Warps = new List<Warp>();
            int warpOffset = BitConverter.ToInt32(rawEntityHeader, 0x8) - 0x8000000;
            if (warpOffset > 0 && warpOffset < 0x2000000)
                for (int i = 0; i < rawEntityHeader[1]; i++)
                Warps.Add(new Warp(warpOffset + i * 0x8, originROM));

            Triggers = new List<Trigger>();
            int triggerOffset = BitConverter.ToInt32(rawEntityHeader, 0xC) - 0x8000000;
            if (triggerOffset > 0 && triggerOffset < 0x2000000)
                for (int i = 0; i < rawEntityHeader[2]; i++)
                    Triggers.Add(new Trigger(triggerOffset + i * 0x10, originROM));

            Signs = new List<Sign>();
            int signOffset = BitConverter.ToInt32(rawEntityHeader, 0x10) - 0x8000000;
            if (signOffset > 0 && signOffset < 0x2000000)
                for (int i = 0; i < rawEntityHeader[3]; i++)
                    Signs.Add(new Sign(signOffset + i * 0xC, originROM));
        }

        public void WriteMapHeaderToRaw()
        {
            Buffer.BlockCopy(BitConverter.GetBytes(mapDataPointer + 0x8000000), 0, rawHeader, 0x0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(eventDataPointer + 0x8000000), 0, rawHeader, 0x4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(mapScriptDataPointer + 0x8000000), 0, rawHeader, 0x8, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(connectionsDataPointer + 0x8000000), 0, rawHeader, 0xC, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(musicNumber), 0, rawHeader, 0x10, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(mapLayoutIndex), 0, rawHeader, 0x12, 2);
            rawHeader[0x14] = mapNameIndex;
            rawHeader[0x15] = visibility;
            rawHeader[0x16] = weather;
            rawHeader[0x17] = mapType;
            if (Program.currentGame.RomType == "FRLG")
            {
                optionsByte1 = (byte)((int)(canRideBike ? FRLGOptions.CanRideBike : FRLGOptions.None) | (optionsByte1 & ~((int)FRLGOptions.CanRideBike)));
                optionsByte2 = (byte)((int)((showsName ? FRLGOptions.ShowsName : FRLGOptions.None) | (canRun ? FRLGOptions.CanRun : FRLGOptions.None) | (canEscape ? FRLGOptions.CanEscape : FRLGOptions.None)) | (optionsByte2 & ~(int)(FRLGOptions.ShowsName | FRLGOptions.CanRun | FRLGOptions.CanEscape)));
            }
            else if (Program.currentGame.RomType == "E")
            {
                optionsByte3 = (byte)((int)((canRideBike ? EOptions.CanRideBike : EOptions.None) | (showsName ? EOptions.ShowsName : EOptions.None) | (canRun ? EOptions.CanRun : EOptions.None) | (canEscape ? EOptions.CanEscape : EOptions.None)) | (optionsByte3 & ~(int)(EOptions.CanRideBike | EOptions.ShowsName | EOptions.CanRun | EOptions.CanEscape)));
            }
            rawHeader[0x18] = optionsByte1;
            rawHeader[0x19] = optionsByte2;
            rawHeader[0x1A] = optionsByte3;
            rawHeader[0x1B] = battleBackground;
        }

        public void WriteEntitiesToRaw()
        {
            rawEntityHeader[0] = (byte)NPCs.Count;
            rawEntityHeader[1] = (byte)Warps.Count;
            rawEntityHeader[2] = (byte)Triggers.Count;
            rawEntityHeader[3] = (byte)Signs.Count;

            foreach (NPC npc in NPCs)
            {
               npc.WriteDataToRaw();
            }

            foreach (Warp warp in Warps)
            {
                warp.WriteDataToRaw();
            }

            foreach (Trigger trigger in Triggers)
            {
                trigger.WriteDataToRaw();
            }

            foreach (Sign sign in Signs)
            {
                sign.WriteDataToRaw();
            }
        }
    }

    class MapBank
    {
        public Dictionary<int, Map> Bank;
        public MapBank()
        {
            Bank = new Dictionary<int, Map>();
        }

        public void AddMap(int number, Map map)
        {
            Bank.Add(number, map);
        }

        public Dictionary<int, Map> GetBank()
        {
            return Bank;
        }
    }

    class MapName
    {
        public string name;
        public MapName(string mapName)
        {
            name = mapName;
        }

        public override string ToString()
        {
            return name;
        }
    }

    public class MapLayout
    {
        public byte[] rawHeaderOrig;
        public byte[] rawHeader;

        public string name
        {
            get { return "[" + layoutIndex.ToString("X4") + "] " + Program.rmInternalStrings.GetString("Layout"); }
        }

        public short layoutIndex;

        public int layoutWidth;
        public int layoutHeight;

        public int borderBlocksPointer;
        public int mapDataPointer;
        public int globalTilesetPointer;
        public int localTilesetPointer;

        public byte borderWidth;
        public byte borderHeight;

        public byte buffer1;
        public byte buffer2;

        public MapTileset globalTileset;
        public MapTileset localTileset;

        public byte[] rawLayoutOrig;
        public byte[] rawLayout;
        public short[] layout;

        public byte[] rawBorderOrig;
        public byte[] rawBorder;
        public short[] border;

        public GBAROM originROM;

        public bool edited
        {
            get { return !rawHeaderOrig.SequenceEqual(rawHeader) || !rawLayoutOrig.SequenceEqual(rawLayout) || !rawBorderOrig.SequenceEqual(rawBorder); }
        }

        public MapLayout()
        {

        }

        public MapLayout(int index, int offset, GBAROM ROM)
        {
            layoutIndex = (byte)index;
            originROM = ROM;
            if (Program.currentGame.RomType == "FRLG")
                rawHeaderOrig = originROM.GetData(offset, 0x1C);
            else
                rawHeaderOrig = originROM.GetData(offset, 0x18);
            rawHeader = (byte[])rawHeaderOrig.Clone();
            LoadLayoutHeaderFromRaw();

            if (borderBlocksPointer > 0 && borderBlocksPointer < Program.ROM.Length)
            {
                rawBorderOrig = originROM.GetData(borderBlocksPointer, borderWidth * borderHeight * 4);
                rawBorder = (byte[])rawBorderOrig.Clone();
                LoadBorderFromRaw();
            }

            if (mapDataPointer > 0 && mapDataPointer < Program.ROM.Length)
            {
                rawLayoutOrig = originROM.GetData(mapDataPointer, layoutWidth * layoutHeight * 4);
                rawLayout = (byte[])rawLayoutOrig.Clone();
                LoadLayoutFromRaw();
            }
        }

        public void Revert()
        {
            rawHeader = (byte[])rawHeaderOrig.Clone();
            rawBorder = (byte[])rawBorderOrig.Clone();
            rawLayout = (byte[])rawLayoutOrig.Clone();
            LoadLayoutHeaderFromRaw();
            LoadLayoutFromRaw();
            LoadBorderFromRaw();
        }

        public void Save()
        {
            WriteLayoutHeaderToRaw();
            WriteLayoutToRaw();
            WriteBorderToRaw();
            rawHeaderOrig = (byte[])rawHeader.Clone();
            rawBorderOrig = (byte[])rawBorder.Clone();
            rawLayoutOrig = (byte[])rawLayout.Clone();
        }

        public void LoadLayoutFromRaw()
        {
            layout = new short[rawLayout.Length / 2];
            Buffer.BlockCopy(rawLayout, 0, layout, 0, rawLayout.Length);
        }

        public void WriteLayoutToRaw()
        {
            rawLayout = new byte[layout.Length * 2];
            Buffer.BlockCopy(layout, 0, rawLayout, 0, rawLayout.Length);
        }

        public void LoadBorderFromRaw()
        {
            border = new short[rawBorder.Length / 2];
            Buffer.BlockCopy(rawBorder, 0, border, 0, rawBorder.Length);
        }

        public void WriteBorderToRaw()
        {
            rawBorder = new byte[border.Length * 2];
            Buffer.BlockCopy(border, 0, rawBorder, 0, rawBorder.Length);
        }

        public void PaintBlocksToMap(short[] blockArray, int x, int y, int w, int h)
        {
            Program.isEdited = true;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if ((x + j < layoutWidth) && (y + i < layoutHeight))
                        layout[(x + (y * layoutWidth)) + (i * layoutWidth) + j] = blockArray[(i * w) + j];
                }
            }
        }

        public void LoadLayoutHeaderFromRaw()
        {
            layoutWidth = BitConverter.ToInt32(rawHeader, 0);
            layoutHeight = BitConverter.ToInt32(rawHeader, 4);
            borderBlocksPointer = BitConverter.ToInt32(rawHeader, 0x8) - 0x8000000;
            mapDataPointer = BitConverter.ToInt32(rawHeader, 0xC) - 0x8000000;
            globalTilesetPointer = BitConverter.ToInt32(rawHeader, 0x10) - 0x8000000;
            localTilesetPointer = BitConverter.ToInt32(rawHeader, 0x14) - 0x8000000;
            if (Program.currentGame.RomType == "FRLG")
            {
                borderWidth = (byte)((rawHeader[0x18] <= 8) ? rawHeader[0x18] : 8);
                borderHeight = (byte)((rawHeader[0x19] <= 8) ? rawHeader[0x19] : 8);
                buffer1 = rawHeader[0x1A];
                buffer2 = rawHeader[0x1B];
            }
            else
            {
                borderWidth = 2;
                borderHeight = 2;
                buffer1 = 0;
                buffer2 = 0;
            }

            try
            {
                if (globalTilesetPointer != -0x8000000)
                {
                    if (!Program.mapTilesets.ContainsKey(globalTilesetPointer))
                        Program.mapTilesets.Add(globalTilesetPointer, new MapTileset(globalTilesetPointer, originROM));
                    globalTileset = Program.mapTilesets[globalTilesetPointer];
                }
            }
            catch (Exception)
            {
                Program.loadExceptions.Add(new TilesetLoadErrorException(string.Format(Program.rmInternalStrings.GetString("CouldNotReadTileset"), Program.rmInternalStrings.GetString("GlobalTileset"), Config.settings.HexPrefix + layoutIndex.ToString("X4"), globalTilesetPointer.ToString("X"))));
            }
            try
            {
                if (localTilesetPointer != -0x8000000)
                {
                    if (!Program.mapTilesets.ContainsKey(localTilesetPointer))
                        Program.mapTilesets.Add(localTilesetPointer, new MapTileset(localTilesetPointer, originROM));
                    localTileset = Program.mapTilesets[localTilesetPointer];
                }
            }
            catch (Exception)
            {
                Program.loadExceptions.Add(new TilesetLoadErrorException(string.Format(Program.rmInternalStrings.GetString("CouldNotReadTileset"), Program.rmInternalStrings.GetString("LocalTileset"), Config.settings.HexPrefix + layoutIndex.ToString("X4"), globalTilesetPointer.ToString("X"))));
            }
        }

        public void WriteLayoutHeaderToRaw()
        {
            if (layoutWidth != BitConverter.ToInt32(rawHeader, 0) || layoutHeight != BitConverter.ToInt32(rawHeader, 4))
                ResizeLayout(layoutWidth, layoutHeight);
            Buffer.BlockCopy(BitConverter.GetBytes(layoutWidth), 0, rawHeader, 0x0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(layoutHeight), 0, rawHeader, 0x4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(borderBlocksPointer + 0x8000000), 0, rawHeader, 0x8, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(mapDataPointer + 0x8000000), 0, rawHeader, 0xC, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(globalTilesetPointer + 0x8000000), 0, rawHeader, 0x10, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(localTilesetPointer + 0x8000000), 0, rawHeader, 0x14, 4);
            if (Program.currentGame.RomType == "FRLG")
            {
                rawHeader[0x18] = borderWidth;
                rawHeader[0x19] = borderHeight;
                rawHeader[0x1A] = buffer1;
                rawHeader[0x1B] = buffer2;
            }
        }

        public void ResizeLayout(int newWidth, int newHeight)
        {
            short[] newLayout = new short[newWidth * newHeight];
            for(int i = 0; i < (newHeight < BitConverter.ToInt32(rawHeader, 4) ? newHeight : BitConverter.ToInt32(rawHeader, 4)); i++)
            {
                for (int j = 0; j < (newWidth < BitConverter.ToInt32(rawHeader, 0) ? newWidth : BitConverter.ToInt32(rawHeader, 0)); j++)
                {
                    newLayout[j + (i * newWidth)] = layout[j + (i * BitConverter.ToInt32(rawHeader, 0))];
                }
            }

            layout = newLayout;
            WriteLayoutToRaw();
            Program.mainGUI.SetGLMapEditorSize(newWidth * 16, newHeight * 16);
            Program.mainGUI.SetGLEntityEditorSize(newWidth * 16, newHeight * 16);
            Program.isEdited = true;
            drawTiles = null;
        }

        public class VisualMapTile {
            public FrameBuffer buffer;
            public int xpos = 0;
            public int ypos = 0;
            public int Width = 64;
            public int Height = 64;
            public bool Redraw = true;
        }

        public void Unload()
        {
            if (drawTiles != null)
            {
                foreach (var v in drawTiles)
                    v.buffer.Dispose();
                drawTiles.Clear();
            }
        }

        public List<VisualMapTile> drawTiles;

        // TODO: Refactor to utilize FBO thing
        public void RefreshChunks(Spritesheet[] globalSheets, Spritesheet[] localSheets, int xPos, int yPos, double scale)
        {
            if (drawTiles == null || drawTiles.Count == 0)
            {
                drawTiles = new List<VisualMapTile>();
                int tilesx = Math.Max(layoutWidth / 16, 1);
                int tilesy = Math.Max(layoutHeight / 16, 1);
                tilesx += layoutWidth % tilesx;
                tilesy += layoutWidth % tilesy;

                int tileWidth = layoutWidth / tilesx;
                int tileHeight = layoutHeight / tilesy;
                int xtiles = layoutWidth / tileWidth;
                int ytiles = layoutHeight / tileHeight;
                for (int y = 0; y < ytiles; y++)
                {
                    for (int x = 0; x < xtiles; x++)
                    {
                        var tile = new VisualMapTile();
                        tile.Width = tileWidth;
                        tile.Height = tileHeight;
                        tile.buffer = new FrameBuffer(tileWidth * 16, tileHeight * 16);
                        tile.xpos = x * tileWidth;
                        tile.ypos = y * tileHeight;
                        drawTiles.Add(tile);
                    }
                }

                int xremain = layoutWidth - xtiles * tileWidth;
                int yremain = layoutHeight - ytiles * tileHeight;
                if (xremain > 0)
                    for (int y = 0; y < ytiles; y++)
                    {
                        var tile = new VisualMapTile();
                        tile.Width = xremain;
                        tile.Height = tileHeight;
                        tile.buffer = new FrameBuffer(tile.Width * 16, tile.Height * 16);
                        tile.xpos = xtiles * tileWidth;
                        tile.ypos = y * tileHeight;
                        drawTiles.Add(tile);
                    }

                if (yremain > 0)
                    for (int x = 0; x < xtiles; x++)
                    {
                        var tile = new VisualMapTile();
                        tile.Width = tileWidth;
                        tile.Height = yremain;
                        tile.buffer = new FrameBuffer(tile.Width * 16, tile.Height * 16);
                        tile.xpos = x * tileWidth;
                        tile.ypos = ytiles * tileHeight;
                        drawTiles.Add(tile);
                    }

                if ((xremain > 0) && (yremain > 0))
                {
                    var tile = new VisualMapTile();
                    tile.Width = xremain;
                    tile.Height = yremain;
                    tile.buffer = new FrameBuffer(tile.Width * 16, tile.Height * 16);
                    tile.xpos = xtiles * tileWidth;
                    tile.ypos = ytiles * tileHeight;
                    drawTiles.Add(tile);
                }
            }

            foreach (var v in drawTiles)
            {
                if (v.Redraw)
                {
                    FrameBuffer.Active = v.buffer;
                    GL.PushMatrix();
                    {
                        int xoff = v.xpos * 16;
                        int yoff = v.ypos * 16;
                        for (int i = v.ypos; i < v.ypos + v.Height; i++)
                        {
                            for (int j = v.xpos; j < v.xpos + v.Width; j++)
                            {
                                short block = layout[i * layoutWidth + j];
                                short blockIndex = (short)(block & 0x3FF);
                                byte movementPerm = (byte)((block & 0xFC00) >> 10);

                                if (blockIndex < Program.currentGame.MainTSBlocks)
                                {
                                    if (globalTileset != null)
                                        globalTileset.blockSet.blocks[blockIndex].Draw(globalSheets, localSheets, xPos + j * 16 - xoff, yPos + i * 16 - yoff, scale);
                                    else
                                        Surface.DrawRect(xPos - xoff, yPos - yoff, 16, 16, Color.Black);
                                }
                                else
                                {
                                    if (localTileset != null)
                                        localTileset.blockSet.blocks[blockIndex - Program.currentGame.MainTSBlocks].Draw(globalSheets, localSheets, xPos + j * 16 - xoff, yPos + i * 16 - yoff, scale);
                                    else
                                        Surface.DrawRect(xPos - xoff, yPos - yoff, 16, 16, Color.Black);
                                }

                                if (Program.showingPerms)
                                {
                                    Program.glMapEditor.movementPerms.Draw(movementPerm, xPos + j * 16 - xoff, yPos + i * 16 - yoff, scale, (((Program.mainGUI.PermTransPreviewValue() >= 0) ? Program.mainGUI.PermTransPreviewValue() : Config.settings.PermissionTranslucency) * 255) / 100);
                                }
                            }
                        }

                        GL.PopMatrix();
                    }
                    FrameBuffer.Active = null;
                    v.Redraw = false;
                }
            }
        }

        public void Draw(Spritesheet[] globalSheets, Spritesheet[] localSheets, int xPos, int yPos, double scale)
        {
            GL.Disable(EnableCap.Blend);
            foreach (var v in drawTiles)
            {
                Surface.SetColor(Color.White);
                Surface.SetTexture(v.buffer.ColorTexture);
                Surface.DrawRect(v.xpos * 16, v.ypos * 16, v.buffer.Width, v.buffer.Height);
            }

            GL.Enable(EnableCap.Blend);

            /*
            foreach (var v in drawTiles)
            {
                Surface.SetTexture(null);
                Surface.SetColor(Color.Cyan);
                Surface.DrawOutlineRect(v.xpos * 16, v.ypos * 16, v.buffer.Width, v.buffer.Height);
            }
            // */

            Surface.SetTexture(null);
        }
        
        public void DrawBorder(Spritesheet[] globalSheets, Spritesheet[] localSheets, int xPos, int yPos, double scale)
        {
            for (int i = 0; i < borderHeight; i++)
            {
                for (int j = 0; j < borderWidth; j++)
                {
                    short block = border[i * borderWidth + j];
                    short blockIndex = (short)(block & 0x3FF);

                    if (blockIndex < Program.currentGame.MainTSBlocks)
                    {
                        if (globalTileset != null)
                            globalTileset.blockSet.blocks[blockIndex].Draw(globalSheets, localSheets, xPos + j * 16, yPos + i * 16, scale);
                        else
                            Surface.DrawRect(xPos, yPos, 16, 16, Color.Black);
                    }
                    else
                    {
                        if (localTileset != null)
                            localTileset.blockSet.blocks[blockIndex - Program.currentGame.MainTSBlocks].Draw(globalSheets, localSheets, xPos + j * 16, yPos + i * 16, scale);
                        else
                            Surface.DrawRect(xPos, yPos, 16, 16, Color.Black);
                    }
                }
            }

            Surface.SetTexture(null);
        }
    }

    public class MapLayoutLoadErrorException : Exception
    {
        public MapLayoutLoadErrorException()
        {

        }

        public MapLayoutLoadErrorException(string message)
            : base(message)
        {

        }

        public MapLayoutLoadErrorException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }

    public class TilesetLoadErrorException : Exception
    {
        public TilesetLoadErrorException()
        {

        }

        public TilesetLoadErrorException(string message)
            : base(message)
        {

        }

        public TilesetLoadErrorException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
