using Nintenlord.ROMHacking.GBA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PGMEBackend.Entities
{
    public class NPC : Entity
    {
        public static int currentNPC = 0;

        public byte npcNumber = 0;
        public byte spriteNumber = 0;
        public byte replacement = 0;
        public byte filler1 = 0;
        public byte idleAnimation = 0;
        public byte xBounds = 0;
        public byte yBounds = 0;
        public byte filler2 = 0;
        public byte trainer = 0;
        public byte filler3 = 0;
        public byte viewRadius = 0;
        public byte filler4 = 0;
        public short visibilityFlag = 0;
        public byte filler5 = 0;
        public byte filler6 = 0;

        public NPC() : base()
        {
            rawDataOrig = new byte[0x18];
            rawData = new byte[0x18];
        }
        
        public NPC(short xPos, short yPos) : base(xPos, yPos)
        {
            rawDataOrig = new byte[0x18];
            rawData = new byte[0x18];
            Buffer.BlockCopy(BitConverter.GetBytes(xPos), 0, rawData, 4, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(yPos), 0, rawData, 6, 2);
        }
        
        public NPC(int offset, GBAROM ROM) : base(offset, ROM)
        {
            rawDataOrig = originROM.GetData(offset, 0x18);
            rawData = (byte[])rawDataOrig.Clone();
            LoadDataFromRaw();
        }

        public override void LoadDataFromRaw()
        {
            npcNumber = rawData[0];
            spriteNumber = rawData[1];
            replacement = rawData[2];
            filler1 = rawData[3];
            xPos = BitConverter.ToInt16(rawData, 4);
            yPos = BitConverter.ToInt16(rawData, 6);
            height = rawData[8];
            idleAnimation = rawData[9];
            xBounds = (byte)(rawData[10] & 0xF);
            yBounds = (byte)((rawData[10] & 0xF0) >> 4);
            filler2 = rawData[11];
            trainer = rawData[12];
            filler3 = rawData[13];
            viewRadius = rawData[14];
            filler4 = rawData[15];
            scriptOffset = BitConverter.ToInt32(rawData, 16) - 0x8000000;
            visibilityFlag = BitConverter.ToInt16(rawData, 20);
            filler5 = rawData[22];
            filler6 = rawData[23];
        }

        public override void WriteDataToRaw()
        {
            rawData[0] = npcNumber;
            rawData[1] = spriteNumber;
            rawData[2] = replacement;
            rawData[3] = filler1;
            Buffer.BlockCopy(BitConverter.GetBytes(xPos), 0, rawData, 4, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(yPos), 0, rawData, 6, 2);
            rawData[8] = height;
            rawData[9] = idleAnimation;
            rawData[10] = (byte)(xBounds + (yBounds << 4));
            rawData[11] = filler2;
            rawData[12] = trainer;
            rawData[13] = filler3;
            rawData[14] = viewRadius;
            rawData[15] = filler4;
            Buffer.BlockCopy(BitConverter.GetBytes(scriptOffset + 0x8000000), 0, rawData, 16, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(visibilityFlag), 0, rawData, 20, 2);
            rawData[22] = filler5;
            rawData[23] = filler6;
        }

        public static bool Delete()
        {
            return Delete(currentNPC);
        }

        public static bool Delete(NPC npc)
        {
            if (!Program.currentMap.NPCs.Remove(npc))
                return false;
            Program.isEdited = true;
            return true;
        }

        public static bool Delete(int num)
        {
            if (num >= Program.currentMap.NPCs.Count)
                return false;
            Program.currentMap.NPCs.RemoveAt(num);
            Program.isEdited = true;
            return true;
        }

        public override EntityType GetEnum()
        {
            return EntityType.NPC;
        }
    }
}
