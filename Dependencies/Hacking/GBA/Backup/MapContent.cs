using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Mappy_Map2D;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace ContentPipelineExtension_MapFromMappy.Mappy
{
    public class MapContent
    {
        private List<ushort> offsets;
        private List<Block> blocks;        
        private MPHD informations;
        List<List<Tile>> tiles;
        public ExternalReference<TextureContent> TextureRef { get; set; }
        
        public int TileRows { get; set; }
        public int TileColumns { get; set; }
        public int TotalTiles { get; set; }


        public List<Block> Blocks
        {
            get
            {
                return this.blocks;
            }
        }

        public List<List<Tile>> Tiles
        {
            get
            {
                return this.tiles;
            }

            private set
            {
                this.tiles = value;
            }
        }

        public MPHD Informations
        {
            get
            {
                return this.informations;
            }
        }

        public MapContent()
        {
            this.offsets = new List<ushort>();
            this.blocks = new List<Block>();
            this.Tiles = new List<List<Tile>>();
        }

        ~MapContent()
        {
            this.offsets.Clear();
            this.blocks.Clear();
            this.offsets = null;
            this.blocks = null;
        }

        public bool LoadMap(string fileName)
        {
            FileStream fileStream = File.Open(fileName, FileMode.Open);            

            BinaryReader binaryReader =
                new BinaryReader(fileStream);

            bool mSuccess = true;

            if (binaryReader != null)
            {
                mSuccess = this.GetHeader(binaryReader);

                while (binaryReader.PeekChar() != -1)
                {
                    this.ProcessChunkHeader(binaryReader);
                }

                this.ReadTiles();

                binaryReader.Close();
            }
            else
            {
                mSuccess = false;
            }

            return mSuccess;
        }

        private bool ReadTiles()
        {
            List<Tile> tileColumns;          

            Tile tile;

            this.Tiles.Capacity = this.offsets.Count;
            
            for (int x = 0; x < this.informations.MapWidth; x++)
            {
                tileColumns = new List<Tile>();

                for (int y = 0; y < this.informations.MapHeight; y++)
                {
                    tile = new Tile();

                    tile.Width = this.informations.BlockWidth;
                    tile.Height = this.informations.BlockHeight;
                    tile.Id = this.offsets[(ushort)y * this.informations.MapWidth + x] / 32;

                    tile.Walkable = Convert.ToBoolean(this.blocks[(int)tile.Id].User1);                    

                    tileColumns.Add(tile);
                }

                this.Tiles.Add(tileColumns);
            }

            return true;
        }

        private string MappyGetStr(BinaryReader pFilestream, int pLen)
        {            
            string tStr = "";

            for (int i = 0; i < pLen; i++)
            {
                int valor = pFilestream.ReadByte();

                if ((char)valor != '\0' && valor != -1)
                {
                    tStr += (char)valor;
                }
                else
                {
                    break;
                }
            }

            return tStr;
        }

        private string MappyDecodeStr(BinaryReader pFilestream)
        {
            string tStr = "";

            int valor = pFilestream.ReadByte();

            while (valor != -1)
            {
                tStr += (char)valor;

                valor = pFilestream.ReadByte();
            }

            return tStr;
        }

        private void Swap(ref byte x, ref byte y)
        {
            byte temp;

            temp = x;
            x = y;
            y = temp;
        }

        /*
         * It was eliminated at version 2.2
        private uint ByteSwap(uint mapSize)
        {
               unsafe
               {
                   byte* x = (byte*)&mapSize;

                   int i = 0;
                   int j = 4 - 1;

                   while (i < j)
                   {
                       this.Swap(ref x[i], ref x[j]);

                       i++;
                       j--;
                   }
               }

               return mapSize;
        }   
         */     

        private bool GetHeader(BinaryReader pFilestream)
        {
            string mMapHeader = "";

            mMapHeader = this.MappyGetStr(pFilestream, 4);

            byte[] bytes = new byte[4];

            uint mapSize;

            mapSize = (uint)pFilestream.Read(bytes, 0, 4);

            byte[] mapSizeBytes = BitConverter.GetBytes(mapSize);

            Swap(ref mapSizeBytes[0], ref mapSizeBytes[3]);

            Swap(ref mapSizeBytes[1], ref mapSizeBytes[2]);

            mapSize = BitConverter.ToUInt32(mapSizeBytes, 0);

            if (mMapHeader == "FORM")
            {
                string mMapID = "";
                mMapID = this.MappyGetStr(pFilestream, 4);

                if (mMapID == "FMAP")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }            
        }

        private bool ProcessAuthorChunk(BinaryReader pFilestream, int pchunkSize)
        {
            List<string> authorInfo = new List<string>();

            int tchunkSize = pchunkSize;

            while (tchunkSize > 0)
            {
                if (pFilestream.ReadByte() != 0)
                {
                    string tStr = this.MappyDecodeStr(pFilestream);

                    tchunkSize -= tStr.Length + 1;

                    authorInfo.Add(tStr);
                }
                else
                {
                    tchunkSize--;
                }

            }
            return true;
        }

        private bool ProcessBlockChunk(BinaryReader pFilestream, int pchunkSize)
        {
            Block tempBlock = new Block();

            int tchunkSize = pchunkSize;

            while (tchunkSize > 0)
            {                
                tempBlock.bgoff = pFilestream.ReadInt32();
                tempBlock.fgoff = pFilestream.ReadInt32();
                tempBlock.fgoff2 = pFilestream.ReadInt32();
                tempBlock.fgoff3 = pFilestream.ReadInt32();
                
                tempBlock.User1 = pFilestream.ReadUInt32();
                tempBlock.User2 = pFilestream.ReadUInt32();

                tempBlock.User3 = pFilestream.ReadUInt16();
                tempBlock.User4 = pFilestream.ReadUInt16();

                tempBlock.User5 = pFilestream.ReadByte();
                tempBlock.User6 = pFilestream.ReadByte();
                tempBlock.User7 = pFilestream.ReadByte();

                /* Read a char(1 byte or 8 bits) to advance the archive´s pointer.
                 * It happens because the 8 bits of informations(BLKSTR) are not being used
                 * */
                pFilestream.ReadChar();           
                
                this.blocks.Add(tempBlock);
                                
                tchunkSize -= System.Runtime.InteropServices.Marshal.SizeOf(typeof(Block));
            }

            return true;
        }

        private bool ProcessMapHeader(BinaryReader pFilestream, int pchunkSize)
        {
            this.informations.MapVerHigh = pFilestream.ReadByte();
            this.informations.MapVerLow = pFilestream.ReadByte();
            this.informations.Lsb = pFilestream.ReadByte();
            this.informations.MapType = pFilestream.ReadByte();
            this.informations.MapWidth = pFilestream.ReadInt16();
            this.informations.MapHeight = pFilestream.ReadInt16();
            this.informations.Reserved1 = pFilestream.ReadInt16();
            this.informations.Reserved2 = pFilestream.ReadInt16();
            this.informations.BlockWidth = pFilestream.ReadInt16();
            this.informations.BlockHeight = pFilestream.ReadInt16();
            this.informations.BlockDepth = pFilestream.ReadInt16();
            this.informations.BlockStrSize = pFilestream.ReadInt16();
            this.informations.NumBlockStr = pFilestream.ReadInt16();
            this.informations.NumBlockGfx = pFilestream.ReadInt16();
            this.informations.Ckey8bit = pFilestream.ReadByte();
            this.informations.CkeyRed = pFilestream.ReadByte();
            this.informations.CkeyGreen = pFilestream.ReadByte();
            this.informations.CkeyBlue = pFilestream.ReadByte();
            this.informations.BlockGapX = pFilestream.ReadInt16();
            this.informations.BlockGapY = pFilestream.ReadInt16();
            this.informations.BlockStaggerX = pFilestream.ReadInt16();
            this.informations.BlockStaggerY = pFilestream.ReadInt16();
            this.informations.ClickMask = pFilestream.ReadInt16();
            this.informations.Pillars = pFilestream.ReadInt16();

            return true;
        }

        private bool ProcessLayerChunk(BinaryReader pFilestream, int pchunkSize)
        {
            int tchunkSize = pchunkSize;

            while (tchunkSize > 0)
            {
                ushort tShort = 0;

                tShort = pFilestream.ReadUInt16();

                this.offsets.Add(tShort);
                
                tchunkSize -= System.Runtime.InteropServices.Marshal.SizeOf(typeof(ushort));
            }

            return true;
        }

        private bool ProcessChunkHeader(BinaryReader pFilestream)
        {
            string chunkID = this.MappyGetStr(pFilestream, 4);

            uint chunkSize;

            chunkSize = pFilestream.ReadUInt32();

            byte[] chunkSizeBytes = BitConverter.GetBytes(chunkSize);

            Swap(ref chunkSizeBytes[0], ref chunkSizeBytes[3]);
            Swap(ref chunkSizeBytes[1], ref chunkSizeBytes[2]);

            chunkSize = BitConverter.ToUInt32(chunkSizeBytes, 0);

            if (chunkID == "ATHR")
            {
                this.ProcessAuthorChunk(pFilestream, (int)chunkSize);
            }
            else if (chunkID == "MPHD")
            {
                this.ProcessMapHeader(pFilestream, (int)chunkSize);
            }
            else if (chunkID == "BODY")
            {
                this.ProcessLayerChunk(pFilestream, (int)chunkSize);
            }
            else if (chunkID == "BKDT")
            {
                this.ProcessBlockChunk(pFilestream, (int)chunkSize);
            }
            else
            {
                pFilestream.BaseStream.Seek((int)chunkSize, SeekOrigin.Current);
            }

            return true;
        }
    }
}
