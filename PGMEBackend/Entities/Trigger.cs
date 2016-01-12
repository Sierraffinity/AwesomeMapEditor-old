using Nintenlord.ROMHacking.GBA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PGMEBackend.Entities
{
    public class Trigger : Entity
    {
        public static int currentTrigger = 0;
        
        public byte filler1 = 0;
        public short variable = 0;
        public short value = 0;
        public byte filler2 = 0;
        public byte filler3 = 0;

        public Trigger() : base()
        {
            rawDataOrig = new byte[0x10];
            rawData = new byte[0x10];
        }
        
        public Trigger(short xPos, short yPos) : base(xPos, yPos)
        {
            rawDataOrig = new byte[0x10];
            rawData = new byte[0x10];
            Buffer.BlockCopy(BitConverter.GetBytes(xPos), 0, rawData, 0, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(yPos), 0, rawData, 2, 2);
        }
        
        public Trigger(int offset, GBAROM ROM) : base(offset, ROM)
        {
            rawDataOrig = originROM.GetData(offset, 0x10);
            rawData = (byte[])rawDataOrig.Clone();
            LoadDataFromRaw();
        }

        public override void LoadDataFromRaw()
        {
            xPos = BitConverter.ToInt16(rawData, 0);
            yPos = BitConverter.ToInt16(rawData, 2);
            height = rawData[4];
            filler1 = rawData[5];
            variable = BitConverter.ToInt16(rawData, 6);
            value = BitConverter.ToInt16(rawData, 8);
            filler2 = rawData[10];
            filler3 = rawData[11];
            scriptOffset = BitConverter.ToInt32(rawData, 12) - 0x8000000;
        }

        public override void WriteDataToRaw()
        {
            Buffer.BlockCopy(BitConverter.GetBytes(xPos), 0, rawData, 0, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(yPos), 0, rawData, 2, 2);
            rawData[4] = height;
            rawData[5] = filler1;
            Buffer.BlockCopy(BitConverter.GetBytes(variable), 0, rawData, 6, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(value), 0, rawData, 8, 2);
            rawData[10] = filler2;
            rawData[11] = filler3;
            Buffer.BlockCopy(BitConverter.GetBytes(scriptOffset + 0x8000000), 0, rawData, 12, 4);
        }

        public static bool Delete()
        {
            return Delete(currentTrigger);
        }

        public static bool Delete(Trigger trigger)
        {
            if (!Program.currentMap.Triggers.Remove(trigger))
                return false;
            Program.isEdited = true;
            return true;
        }

        public static bool Delete(int num)
        {
            if (num >= Program.currentMap.Triggers.Count)
                return false;
            Program.currentMap.Triggers.RemoveAt(num);
            Program.isEdited = true;
            return true;
        }
        public override EntityType GetEnum()
        {
            return EntityType.Trigger;
        }
    }
}
