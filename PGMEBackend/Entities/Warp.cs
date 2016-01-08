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

        public byte[] rawDataOrig;
        public byte[] rawData;
        public int offset = 0;
        
        public byte destWarpNum = 0;
        public byte destMapBank = 0;
        public byte destMapNum = 0;

        public Warp()
        {

        }

        public Warp(int Offset, GBAROM ROM)
        {
            offset = Offset;
            rawDataOrig = ROM.GetData(offset, 0x8);
            rawData = (byte[])rawDataOrig.Clone();
            LoadWarpFromRaw();
        }

        public void LoadWarpFromRaw()
        {
            xPos = BitConverter.ToInt16(rawData, 0);
            yPos = BitConverter.ToInt16(rawData, 2);
            height = rawData[4];
            destWarpNum = rawData[5];
            destMapNum = rawData[6];
            destMapBank = rawData[7];
        }

        public void LoadRawFromWarp()
        {
            Buffer.BlockCopy(BitConverter.GetBytes(xPos), 0, rawData, 0, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(yPos), 0, rawData, 2, 2);
            rawData[4] = height;
            rawData[5] = destWarpNum;
            rawData[6] = destMapNum;
            rawData[7] = destMapBank;
        }

        public static void DeleteWarp(int currentWarp)
        {
            Warp[] newWarps = new Warp[Program.currentMap.Warps.Length - 1];
            for (int i = 0; i < Program.currentMap.Warps.Length - 1; i++)
            {
                if (i < currentWarp)
                    newWarps[i] = Program.currentMap.Warps[i];
                else
                    newWarps[i] = Program.currentMap.Warps[i + 1];
            }
            Program.currentMap.Warps = newWarps;
            Program.isEdited = true;
        }
    }
}
