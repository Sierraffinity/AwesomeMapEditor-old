using Nintenlord.ROMHacking.GBA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PGMEBackend.Entities
{
    public abstract class Entity
    {
        public byte[] rawDataOrig;
        public byte[] rawData;
        public int offset = 0;

        public short xPos = 0;
        public short yPos = 0;
        public byte height = 0;
        public int scriptOffset = 0;

        public bool edited
        {
            get { return !rawDataOrig.SequenceEqual(rawData); }
        }

        public Entity()
        {

        }

        public Entity(short x, short y)
        {
            xPos = x;
            yPos = y;
        }

        public Entity(int Offset, GBAROM ROM)
        {
            offset = Offset;
            rawDataOrig = ROM.GetData(offset, 0x18);
            rawData = (byte[])rawDataOrig.Clone();
            LoadDataFromRaw();
        }

        public abstract void LoadDataFromRaw();

        public abstract void LoadRawFromData();
    }
}
