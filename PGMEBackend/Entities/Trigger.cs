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

        public byte[] rawDataOrig;
        public byte[] rawData;
        public int offset = 0;
        
        public byte filler1 = 0;
        public short variable = 0;
        public short value = 0;
        public byte filler2 = 0;
        public byte filler3 = 0;

        public Trigger()
        {

        }

        public Trigger(int Offset, GBAROM ROM)
        {
            offset = Offset;
            rawDataOrig = ROM.GetData(offset, 16);
            rawData = (byte[])rawDataOrig.Clone();
            LoadTriggerFromRaw();
        }

        public void LoadTriggerFromRaw()
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

        public void LoadRawFromTrigger()
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

        static public void DeleteTrigger(int currentTrigger)
        {
            Trigger[] newTriggers = new Trigger[Program.currentMap.Triggers.Length - 1];
            for (int i = 0; i < Program.currentMap.Triggers.Length - 1; i++)
            {
                if (i < currentTrigger)
                    newTriggers[i] = Program.currentMap.Triggers[i];
                else
                    newTriggers[i] = Program.currentMap.Triggers[i + 1];
            }
            Program.currentMap.Triggers = newTriggers;
        }
    }
}
