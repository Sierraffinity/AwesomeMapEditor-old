using Nintenlord.ROMHacking.GBA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PGMEBackend.Entities
{
    public class Warp : Entity
    {
        public static int currentWarp = 0;
        
        public byte destWarpNum = 0;
        public byte destMapBank = 0;
        public byte destMapNum = 0;

        public Warp() : base()
        {
            rawDataOrig = new byte[0x8];
            rawData = new byte[0x8];
        }
        
        public Warp(short xPos, short yPos) : base(xPos, yPos)
        {
            rawDataOrig = new byte[0x8];
            rawData = new byte[0x8];
            Buffer.BlockCopy(BitConverter.GetBytes(xPos), 0, rawData, 0, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(yPos), 0, rawData, 2, 2);
        }
        
        public Warp(int offset, GBAROM ROM) : base(offset, ROM)
        {
            rawDataOrig = originROM.GetData(offset, 0x8);
            rawData = (byte[])rawDataOrig.Clone();
            LoadDataFromRaw();
        }

        public override void LoadDataFromRaw()
        {
            xPos = BitConverter.ToInt16(rawData, 0);
            yPos = BitConverter.ToInt16(rawData, 2);
            height = rawData[4];
            destWarpNum = rawData[5];
            destMapNum = rawData[6];
            destMapBank = rawData[7];
        }

        public override void WriteDataToRaw()
        {
            Buffer.BlockCopy(BitConverter.GetBytes(xPos), 0, rawData, 0, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(yPos), 0, rawData, 2, 2);
            rawData[4] = height;
            rawData[5] = destWarpNum;
            rawData[6] = destMapNum;
            rawData[7] = destMapBank;
        }

        public static bool Delete()
        {
            return Delete(currentWarp);
        }

        public static bool Delete(Warp warp)
        {
            if (!Program.currentMap.Warps.Remove(warp))
                return false;
            Program.isEdited = true;
            return true;
        }

        public static bool Delete(int num)
        {
            if (num >= Program.currentMap.Warps.Count)
                return false;
            Program.currentMap.Warps.RemoveAt(num);
            Program.isEdited = true;
            return true;
        }
        public override EntityType GetEnum()
        {
            return EntityType.Warp;
        }
    }
}
