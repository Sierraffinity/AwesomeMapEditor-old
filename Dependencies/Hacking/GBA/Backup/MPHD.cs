using System;
using System.Collections.Generic;
using System.Text;

namespace ContentPipelineExtension_MapFromMappy.Mappy
{
    	public struct MPHD
	    {		
	        /* Map header structure */
            public byte MapVerHigh;	/* map version number to left of . (ie X.0). */
            public byte MapVerLow;		/* map version number to right of . (ie 0.X). */
            public byte Lsb;		/* if 1, data stored LSB first, otherwise MSB first. */
            public byte MapType;	/* 0 for 32 offset still, -16 offset anim shorts in BODY added FMP0.5*/
            public Int16 MapWidth;	/* width in blocks. */
            public Int16 MapHeight;	/* height in blocks. */
            public Int16 Reserved1;
            public Int16 Reserved2;
            public Int16 BlockWidth;	/* width of a block (tile) in pixels. */
            public Int16 BlockHeight;	/* height of a block (tile) in pixels. */
            public Int16 BlockDepth;	/* depth of a block (tile) in planes (ie. 256 colours is 8) */
            public Int16 BlockStrSize;	/* size of a block data structure */
            public Int16 NumBlockStr;	/* Number of block structures in BKDT */
            public Int16 NumBlockGfx;	/* Number of 'blocks' in graphics (BODY) */
            public byte Ckey8bit, CkeyRed, CkeyGreen, CkeyBlue; /* colour key values added FMP0.4*/
            /* info for non rectangular block maps added FMP0.5*/
            public Int16 BlockGapX, BlockGapY, BlockStaggerX, BlockStaggerY;
            public Int16 ClickMask, Pillars;
	    };
}
