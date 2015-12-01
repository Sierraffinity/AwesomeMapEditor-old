using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace Mappy_Map2D
{
    public struct Block
	{
        public int bgoff, fgoff;
        public int fgoff2, fgoff3;
      
        public uint User1, User2;	/* user long data */
        public ushort User3, User4;	/* user short data */
       
        public byte User5, User6, User7;	/* user byte data */

        public byte miscData;

        public bool TopLeftColl
        {
            get { return (miscData | 1) == 1; }
            set
            {
                if (value)
                    miscData |= 1;
                else
                    miscData &= byte.MaxValue - 1;
            }
        }
        public bool TopRigthColl
        {
            get { return (miscData | 2) == 2; }
            set
            {
                if (value)
                    miscData |= 2;
                else
                    miscData &= byte.MaxValue - 2;
            }
        }
        public bool BottomLeftColl
        {
            get { return (miscData | 4) == 4; }
            set
            {
                if (value)
                    miscData |= 4;
                else
                    miscData &= byte.MaxValue - 4;
            }
        }
        public bool BottomRigthColl
        {
            get { return (miscData | 8) == 8; }
            set
            {
                if (value)
                    miscData |= 8;
                else
                    miscData &= byte.MaxValue - 8;
            }
        }
        public bool Trigger
        {
            get { return (miscData | 0x10) == 0x10; }
            set
            {
                if (value)
                    miscData |= 0x10;
                else
                    miscData &= byte.MaxValue - 0x10;
            }
        }

        public override string ToString()
        {
            string text = "";

            text = string.Format(@"Block Informations:" +
"\nUser1: {0}\n" +
"User2: {1}\n", this.User1, this.User2);

            return text;
        }
    };
}
