using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using System.Xml;
using TiledPipelineExtensions;
using System.IO;
using System.Drawing;
using Nintenlord.Utility;
using Nintenlord.ROMHacking.GBA.Compressions;
using System.Runtime.InteropServices;
using Nintenlord.ROMHacking.GBA;
using Nintenlord.IO;

namespace TiledMapInserter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void Run(string mapPath, string ROMpath, int offset, int? mapPointerOffset, 
            bool insertMapChange, int? mapChangePointerOffset)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(mapPath);

            Map map = new Map(doc);

            if (map.TileWidth != 16 || map.TileHeight != 16)
            {
                throw new Exception("Tilesize is not 16x16");
            }

            var tileChanges = new Dictionary<int, Tuple<Rectangle, TileLayer>>(map.Layers.Count);

            int mainLayerIndex = HandleLayers(map, tileChanges);

            
            //byte[] ROMData = File.ReadAllBytes(ROMpath);

            //MemoryStream stream = new MemoryStream(ROMData);
            ChangeStream stream = new ChangeStream();

            InsertMap(offset, mapPointerOffset, insertMapChange, mapChangePointerOffset, 
                map, tileChanges, mainLayerIndex, stream);

            stream.WriteToFile(ROMpath);
            //File.WriteAllBytes(ROMpath, ROMData);
        }

        private static void InsertMap(int offset, int? mapPointerOffset, bool insertMapChange, int? mapChangePointerOffset, 
            Map map, Dictionary<int, Tuple<Rectangle, TileLayer>> tileChanges, int mainLayerIndex, Stream stream)
        {
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                //Insert map
                writer.BaseStream.Position = offset;
                TileLayer mainLayer = map.Layers[mainLayerIndex] as TileLayer;

                List<byte> data = new List<byte>()
                {
                    (byte)(mainLayer.Width),
                    (byte)(mainLayer.Height)
                };

                for (int i = 0; i < mainLayer.Data.Length; i++)
                {
                    AddTile(data, mainLayer.Data[i]);
                }
                writer.Write(LZ77.Compress(data.ToArray()));

                if (insertMapChange)
                {
                    int changeOffset = InsertMapChange(tileChanges, writer);

                    HandlePointer(changeOffset, mapChangePointerOffset, writer);
                }
                //Here to prevent offset from changing too soon.
                HandlePointer(offset, mapPointerOffset, writer);

            }
        }

        private static int InsertMapChange(Dictionary<int, Tuple<Rectangle, TileLayer>> tileChanges, BinaryWriter writer)
        {
            //Insert Map changes

            Dictionary<int, int> offsets = new Dictionary<int, int>(tileChanges.Count);

            foreach (var item in tileChanges)//Insert tiles
            {
                offsets[item.Key] = ((int)writer.BaseStream.Position);
                ushort[] tiles = item.Value.Curry(GetArea);
                foreach (var tile in tiles)
                {
                    writer.Write(tile);
                }
            }

            if ((writer.BaseStream.Position & 3) != 0)
            {
                writer.Seek(4 - (int)writer.BaseStream.Position & 3, SeekOrigin.Current);
            }

            int changeOffset = (int)writer.BaseStream.Position;
            foreach (var item in tileChanges)
            {
                writer.Write((byte)item.Key);

                //Insert area
                writer.Write((byte)item.Value.Item1.X);
                writer.Write((byte)item.Value.Item1.Y);
                writer.Write((byte)item.Value.Item1.Width);
                writer.Write((byte)item.Value.Item1.Height);

                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write((byte)0);

                writer.Write(Pointer.MakePointer(offsets[item.Key]));
            }

            writer.Write(0xFF);
            writer.Write(0);
            writer.Write(0);
            return changeOffset;
        }

        private static void HandlePointer(int offset, int? mapPointerOffset, BinaryWriter writer)
        {
            if (mapPointerOffset.HasValue)
            {
                //Insert pointer
                writer.BaseStream.Position = mapPointerOffset.Value;
                writer.Write(Pointer.MakePointer(offset));
            }
        }

        private static void AddTile(List<byte> data, int tileData)
        {
            if (tileData == 0)
            {
                throw new ArgumentException("Unassigned tile in map.");
            }
            var bytes = BitConverter.GetBytes((tileData - 1) * 4);
            data.Add(bytes[0]);
            data.Add(bytes[1]);
        }

        private static int HandleLayers(Map map, Dictionary<int, Tuple<Rectangle, TileLayer>> tileChanges)
        {
            int mainLayerIndex = -1;
            if (map.Layers.Count > 1)
            {
                for (int i = 0; i < map.Layers.Count; i++)
                {
                    Rectangle tileChangeArea = new Rectangle();
                    int ID = -1;

                    foreach (var item in map.Layers[i].Properties)
                    {
                        if (item.Name.Equals("Main", StringComparison.OrdinalIgnoreCase))
                        {
                            if (mainLayerIndex >= 0)
                            {
                                throw new Exception("Several layers marked Main");
                            }
                            mainLayerIndex = i;
                        }
                        else
                        {
                            //Tile change
                            switch (item.Name)
                            {
                                case "Width":
                                    tileChangeArea.Width = int.Parse(item.Value);
                                    break;
                                case "Height":
                                    tileChangeArea.Height = int.Parse(item.Value);
                                    break;
                                case "X":
                                    tileChangeArea.X = int.Parse(item.Value);
                                    break;
                                case "Y":
                                    tileChangeArea.Y = int.Parse(item.Value);
                                    break;
                                case "ID":
                                    ID = int.Parse(item.Value);
                                    break;
                                default:
                                    //Other properties are ignored
                                    break;
                            }
                        }
                    }
                    if (mainLayerIndex != i)
                    {
                        if (ID == -1)
                            throw new Exception("Tilechange without ID");
                        if (tileChanges.ContainsKey(ID))
                            throw new Exception("Several tilesets with the same ID");
                        if (!ID.IsInRange(byte.MinValue, byte.MaxValue))
                        {
                            throw new IndexOutOfRangeException("ID does not fit into a byte.");
                        }

                        tileChanges[ID] = new Tuple<Rectangle, TileLayer>(
                            tileChangeArea, map.Layers[i] as TileLayer);
                    }
                }
                //Check layers and determine tile changes and main layer
            }
            else mainLayerIndex = 0;
            return mainLayerIndex;
        }

        static ushort[] GetArea(Rectangle rect, TileLayer layer)
        {
            ushort[] result = new ushort[rect.Width * rect.Height];
            int index = 0;
            for (int j = rect.Top; j < rect.Bottom; j++)
            {
                for (int i = rect.Left; i < rect.Right; i++)
                {
                    int tile = layer.Data[j * layer.Width + i];
                    if (tile == 0)
                    {
                        throw new ArgumentException("Unassigned tile in map.");
                    }
                    result[index] = (ushort)((tile - 1) * 4);
                    index++;
                }
            }

            return result;
        }
    }
}
