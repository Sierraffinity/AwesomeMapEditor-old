using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ContentPipelineExtension1.Animation;

namespace Mappy_Map2D
{
    /// <summary>
    /// Represents a map from the Mappy Tool.
    /// </summary>
    public class Map2D
    {
        /* Map header structure */
        private byte mapVerHigh;	/* map version number to left of . (ie X.0). */
        private byte mapVerLow;		/* map version number to right of . (ie 0.X). */
        private byte lsb;		/* if 1, data stored LSB first, otherwise MSB first. */
        private byte mapType;	/* 0 for 32 offset still, -16 offset anim shorts in BODY added FMP0.5*/
        private Int16 mapWidth;	/* width in blocks. */
        private Int16 mapHeight;	/* height in blocks. */
        private Int16 reserved1;
        private Int16 reserved2;
        private Int16 blockWidth;	/* width of a block (tile) in pixels. */
        private Int16 blockHeight;	/* height of a block (tile) in pixels. */
        private Int16 blockDepth;	/* depth of a block (tile) in planes (ie. 256 colours is 8) */
        private Int16 blockStrSize;	/* size of a block data structure */
        private Int16 numBlockStr;	/* Number of block structures in BKDT */
        private Int16 numBlockGfx;	/* Number of 'blocks' in graphics (BODY) */
        private byte ckey8bit, ckeyRed, ckeyGreen, ckeyBlue; /* colour key values added FMP0.4*/
        /* info for non rectangular block maps added FMP0.5*/
        private Int16 blockGapX, blockGapY, blockStaggerX, blockStaggerY;
        private Int16 clickMask, pillars;

        /// <summary>
        /// Informations of the blocks; User 1, User 2, User 3, ... User7
        /// </summary>
        protected List<Block> blocks;
        protected List<Animation> animations;
        
        /// <summary>
        /// Represents the layers BODY and LYR?
        /// </summary>
        private int[][] layers;

        private Texture2D graphics;

        //protected List<Rectangle> tileSourceRectangles;
        //internal List<List<Tile>> tiles;  

        public int MapWidth
        {
            get { return mapWidth; }
        }
        public int MapHeight
        {
            get { return mapHeight; }
        }
        public int TileWidth
        {
            get { return blockWidth; }
        }
        public int TileHeight
        {
            get { return blockHeight; }
        }

        //internal List<List<Tile>> Tiles
        //{
        //    get
        //    {
        //        return this.tiles;
        //    }

        //    set
        //    {
        //        this.tiles = value;
        //    }
        //}

        //public Texture2D TileStripTexture
        //{
        //    get;
        //    internal set;
        //}

        //public List<Tile> IntersectTiles(Rectangle objDestRect)
        //{
        //    List<Tile> tiles = new List<Tile>();
        //    for (int i = 0; i < this.tiles.Count; i++)
        //    {
        //        for (int j = 0; j < this.tiles[i].Count; j++)
        //        {
        //            if (Tile.Intersects(objDestRect, this.tiles[i][j]))
        //            {
        //                tiles.Add(this.tiles[i][j]);
        //            }
        //        }
        //    }

        //    if (tiles.Count == 0)
        //        return null;

        //    return tiles;
        //}

        //public Tile? ContainsTile(Vector2 pos)
        //{
        //    List<Tile> tiles = new List<Tile>();
        //    for (int i = 0; i < this.tiles.Count; i++)
        //    {
        //        for (int j = 0; j < this.tiles[i].Count; j++)
        //        {
        //            if (Tile.Contains(pos, this.tiles[i][j]))
        //            {
        //                tiles.Add(this.tiles[i][j]);
        //            }
        //        }
        //    }

        //    if (tiles.Count == 0)
        //        return null;

        //    return tiles[0];
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id">id of the field: Tile.Id </param>
        ///// <returns></returns>
        //public Block? GetBlockInformation(int id)
        //{
        //    if (id >= 0 && id < blocks.Count)
        //    {
        //        return blocks[id];
        //    }

        //    return null;
        //}



        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="mapWidth"></param>
        /// <param name="mapHeight"></param>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        /// <param name="amountOfTileGraphics"></param>
        public Map2D(GraphicsDevice graphicsDevice, short mapWidth, short mapHeight, short tileWidth, short tileHeight, int amountOfTileGraphics)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            this.blockWidth = tileWidth;
            this.blockHeight = tileHeight;
            int graphicsWidth = tileWidth * (int)Math.Round(Math.Sqrt(amountOfTileGraphics), MidpointRounding.AwayFromZero) ;
            this.graphics = new Texture2D(graphicsDevice, graphicsWidth, graphicsWidth, 0, TextureUsage.None, SurfaceFormat.Color);
        }

        public Map2D()
        {

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="worldPosition"></param>
        /// <param name="color"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 worldPosition, Color color)
        {
            
        }
    }
}
