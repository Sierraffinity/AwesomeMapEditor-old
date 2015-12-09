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
using System;
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
        public string name;

        public byte[] rawDataOrig;
        public byte[] rawData;
        public int headerPointer;
        public int mapDataPointer;
        public int eventDataPointer;
        public int mapScriptDataPointer;
        public int connectionsDataPointer;
        public int musicNumber;
        public int mapLayoutIndex;
        public int mapNameIndex;
        public int visibility;
        public int weather;
        public int mapType;
        public int optionsByte1;
        public int optionsByte2;
        public int optionsByte3;
        public int battleTransition;

        public bool showsName;
        public bool canRun;
        public bool canRideBike;
        public bool canEscape;

        public bool edited
        {
            get { return !rawDataOrig.SequenceEqual(rawData); }
        }

        public MapLayout layout;

        public Map(int pointer, GBAROM ROM, int currentBank, int currentMap)
        {
            headerPointer = ROM.ReadPointer(pointer);
            rawDataOrig = ROM.GetData(headerPointer, 0x1C);
            rawData = (byte[])rawDataOrig.Clone();

            LoadMapHeaderFromRaw();
            name = "[" + currentBank.ToString("X2") + ", " + currentMap.ToString("X2") + "] " + Program.mapNames[mapNameIndex].Name;

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
            mapDataPointer = BitConverter.ToInt32(rawData, 0x0) - 0x8000000;
            eventDataPointer = BitConverter.ToInt32(rawData, 0x4) - 0x8000000;
            mapScriptDataPointer = BitConverter.ToInt32(rawData, 0x8) - 0x8000000;
            connectionsDataPointer = BitConverter.ToInt32(rawData, 0xC) - 0x8000000;
            musicNumber = BitConverter.ToInt16(rawData, 0x10);
            mapLayoutIndex = BitConverter.ToInt16(rawData, 0x12);
            mapNameIndex = rawData[0x14];
            visibility = rawData[0x15];
            weather = rawData[0x16];
            mapType = rawData[0x17];
            optionsByte1 = rawData[0x18];
            optionsByte2 = rawData[0x19];
            optionsByte3 = rawData[0x1A];
            battleTransition = rawData[0x1B];
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
        }

        public void LoadRawFromMapHeader()
        {
            Buffer.BlockCopy(BitConverter.GetBytes(mapDataPointer + 0x8000000), 0, rawData, 0x0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(eventDataPointer + 0x8000000), 0, rawData, 0x4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(mapScriptDataPointer + 0x8000000), 0, rawData, 0x8, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(connectionsDataPointer + 0x8000000), 0, rawData, 0xC, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(musicNumber), 0, rawData, 0x10, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(mapLayoutIndex), 0, rawData, 0x12, 2);
            rawData[0x14] = (byte)mapNameIndex;
            rawData[0x15] = (byte)visibility;
            rawData[0x16] = (byte)weather;
            rawData[0x17] = (byte)mapType;
            if (Program.currentGame.RomType == "FRLG")
            {
                optionsByte1 = (int)(canRideBike ? FRLGOptions.CanRideBike : FRLGOptions.None) | (optionsByte1 & ~((int)FRLGOptions.CanRideBike));
                optionsByte2 = (int)((showsName ? FRLGOptions.ShowsName : FRLGOptions.None) | (canRun ? FRLGOptions.CanRun : FRLGOptions.None) | (canEscape ? FRLGOptions.CanEscape : FRLGOptions.None)) | (optionsByte2 & ~(int)(FRLGOptions.ShowsName | FRLGOptions.CanRun | FRLGOptions.CanEscape));
            }
            else if (Program.currentGame.RomType == "E")
            {
                optionsByte3 = (int)((canRideBike ? EOptions.CanRideBike : EOptions.None) | (showsName ? EOptions.ShowsName : EOptions.None) | (canRun ? EOptions.CanRun : EOptions.None) | (canEscape ? EOptions.CanEscape : EOptions.None)) | (optionsByte3 & ~(int)(EOptions.CanRideBike | EOptions.ShowsName | EOptions.CanRun | EOptions.CanEscape));
            }
            rawData[0x18] = (byte)optionsByte1;
            rawData[0x19] = (byte)optionsByte2;
            rawData[0x1A] = (byte)optionsByte3;
            rawData[0x1B] = (byte)battleTransition;
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
        public string Name;
        public MapName(string mapName)
        {
            Name = mapName;
        }
    }

    public class MapLayout
    {
        public byte[] rawDataOrig;
        public byte[] rawData;

        public string name;
        public int layoutIndex;

        public int layoutWidth;
        public int layoutHeight;

        public int borderBlocksPointer;
        public int mapDataPointer;
        public int globalTilesetPointer;
        public int localTilesetPointer;

        public int borderWidth;
        public int borderHeight;

        public int buffer1;
        public int buffer2;

        public MapTileset globalTileset;
        public MapTileset localTileset;

        public byte[] rawLayout;
        public short[] layout;

        public bool edited
        {
            get { return !rawDataOrig.SequenceEqual(rawData); }
        }

        public MapLayout(int index, int offset, GBAROM ROM)
        {
            layoutIndex = index;
            name = "[" + layoutIndex.ToString("X4") + "]";
            if (Program.currentGame.RomType == "FRLG")
                rawDataOrig = ROM.GetData(offset, 0x1C);
            else
                rawDataOrig = ROM.GetData(offset, 0x18);
            rawData = (byte[])rawDataOrig.Clone();
            LoadLayoutHeaderFromRaw(ROM);

            if (mapDataPointer > 0 && mapDataPointer < Program.ROM.Length)
            {
                rawLayout = ROM.GetData(mapDataPointer, layoutWidth * layoutHeight * 4);
                layout = new short[rawLayout.Length / 2];
                LoadLayoutFromRaw();
            }
        }

        public void LoadLayoutFromRaw()
        {
            Buffer.BlockCopy(rawLayout, 0, layout, 0, rawLayout.Length);
        }

        public void WriteLayoutToRaw()
        {
            Buffer.BlockCopy(layout, 0, rawLayout, 0, rawLayout.Length);
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

        public void LoadLayoutHeaderFromRaw(GBAROM ROM)
        {
            layoutWidth = BitConverter.ToInt32(rawData, 0);
            layoutHeight = BitConverter.ToInt32(rawData, 4);
            borderBlocksPointer = BitConverter.ToInt32(rawData, 0x8) - 0x8000000;
            mapDataPointer = BitConverter.ToInt32(rawData, 0xC) - 0x8000000;
            globalTilesetPointer = BitConverter.ToInt32(rawData, 0x10) - 0x8000000;
            localTilesetPointer = BitConverter.ToInt32(rawData, 0x14) - 0x8000000;
            if (Program.currentGame.RomType == "FRLG")
            {
                borderWidth = rawData[0x18];
                borderHeight = rawData[0x19];
                buffer1 = rawData[0x1A];
                buffer2 = rawData[0x1B];
            }

            try
            {
                if (globalTilesetPointer != -0x8000000)
                {
                    if (!Program.mapTilesets.ContainsKey(globalTilesetPointer))
                        Program.mapTilesets.Add(globalTilesetPointer, new MapTileset(globalTilesetPointer, ROM));
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
                        Program.mapTilesets.Add(localTilesetPointer, new MapTileset(localTilesetPointer, ROM));
                    localTileset = Program.mapTilesets[localTilesetPointer];
                }
            }
            catch (Exception e)
            {
                Program.loadExceptions.Add(new TilesetLoadErrorException(string.Format(Program.rmInternalStrings.GetString("CouldNotReadTileset"), Program.rmInternalStrings.GetString("LocalTileset"), Config.settings.HexPrefix + layoutIndex.ToString("X4"), globalTilesetPointer.ToString("X"))));
            }
        }

        public void LoadRawFromLayoutHeader()
        {
            Buffer.BlockCopy(BitConverter.GetBytes(layoutWidth), 0, rawData, 0x0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(layoutHeight), 0, rawData, 0x4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(borderBlocksPointer + 0x8000000), 0, rawData, 0x8, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(mapDataPointer + 0x8000000), 0, rawData, 0xC, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(globalTilesetPointer + 0x8000000), 0, rawData, 0x10, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(localTilesetPointer + 0x8000000), 0, rawData, 0x14, 4);
            if (Program.currentGame.RomType == "FRLG")
            {
                rawData[0x18] = (byte)borderWidth;
                rawData[0x19] = (byte)borderHeight;
                rawData[0x1A] = (byte)buffer1;
                rawData[0x1B] = (byte)buffer2;
            }
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
            foreach (var v in drawTiles)
                v.buffer.Dispose();
            drawTiles.Clear();
        }

        public List<VisualMapTile> drawTiles;

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
                    OpenTK.Graphics.OpenGL.GL.PushMatrix();
                    {
                        int xoff = v.xpos * 16;
                        int yoff = v.ypos * 16;
                        for (int i = v.ypos; i < v.ypos + v.Height; i++)
                        {
                            for (int j = v.xpos; j < v.xpos + v.Width; j++)
                            {
                                short block = layout[i * layoutWidth + j];
                                short blockIndex = (short)(block & 0x3FF);

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
                            }
                        }

                        OpenTK.Graphics.OpenGL.GL.PopMatrix();
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
