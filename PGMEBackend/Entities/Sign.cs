using Nintenlord.ROMHacking.GBA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PGMEBackend.Entities
{
    public class Sign : Entity
    {
        public static int currentSign = 0;
        
        public byte type = 0;
        public byte filler1 = 0;
        public byte filler2 = 0;

        public Sign() : base()
        {
            rawDataOrig = new byte[0xC];
            rawData = new byte[0xC];
        }
        
        public Sign(short xPos, short yPos) : base(xPos, yPos)
        {
            rawDataOrig = new byte[0xC];
            rawData = new byte[0xC];
            Buffer.BlockCopy(BitConverter.GetBytes(xPos), 0, rawData, 0, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(yPos), 0, rawData, 2, 2);
        }
        
        public Sign(int offset, GBAROM ROM) : base(offset, ROM)
        {
            rawDataOrig = originROM.GetData(offset, 0xC);
            rawData = (byte[])rawDataOrig.Clone();
            LoadDataFromRaw();
        }

        public override void LoadDataFromRaw()
        {
            xPos = BitConverter.ToInt16(rawData, 0);
            yPos = BitConverter.ToInt16(rawData, 2);
            height = rawData[4];
            type = rawData[5];
            filler1 = rawData[6];
            filler2 = rawData[7];
            scriptOffset = BitConverter.ToInt32(rawData, 8) - 0x8000000;
        }

        public override void WriteDataToRaw()
        {
            Buffer.BlockCopy(BitConverter.GetBytes(xPos), 0, rawData, 0, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(yPos), 0, rawData, 2, 2);
            rawData[4] = height;
            rawData[5] = type;
            rawData[6] = filler1;
            rawData[7] = filler2;
            Buffer.BlockCopy(BitConverter.GetBytes(scriptOffset + 0x8000000), 0, rawData, 8, 4);
        }

        public static bool Delete()
        {
            return Delete(currentSign);
        }

        public static bool Delete(Sign sign)
        {
            if (!Program.currentMap.Signs.Remove(sign))
                return false;
            Program.isEdited = true;
            return true;
        }

        public static bool Delete(int num)
        {
            if (num >= Program.currentMap.Signs.Count)
                return false;
            Program.currentMap.Signs.RemoveAt(num);
            Program.isEdited = true;
            return true;
        }

        public override EntityType GetEnum()
        {
            return EntityType.Sign;
        }
    }
}
