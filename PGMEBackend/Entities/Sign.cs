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

        public byte[] rawDataOrig;
        public byte[] rawData;
        public int offset = 0;
        
        public byte type = 0;
        public byte filler1 = 0;
        public byte filler2 = 0;

        public Sign()
        {

        }

        public Sign(int Offset, GBAROM ROM)
        {
            offset = Offset;
            rawDataOrig = ROM.GetData(offset, 12);
            rawData = (byte[])rawDataOrig.Clone();
            LoadSignFromRaw();
        }

        public void LoadSignFromRaw()
        {
            xPos = BitConverter.ToInt16(rawData, 0);
            yPos = BitConverter.ToInt16(rawData, 2);
            height = rawData[4];
            type = rawData[5];
            filler1 = rawData[6];
            filler2 = rawData[7];
            scriptOffset = BitConverter.ToInt32(rawData, 8) - 0x8000000;
        }

        public void LoadRawFromSign()
        {
            Buffer.BlockCopy(BitConverter.GetBytes(xPos), 0, rawData, 0, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(yPos), 0, rawData, 2, 2);
            rawData[4] = height;
            rawData[5] = type;
            rawData[6] = filler1;
            rawData[7] = filler2;
            Buffer.BlockCopy(BitConverter.GetBytes(scriptOffset + 0x8000000), 0, rawData, 8, 4);
        }

        static public void DeleteSign(int currentSign)
        {
            Sign[] newSigns = new Sign[Program.currentMap.Signs.Length - 1];
            for (int i = 0; i < Program.currentMap.Signs.Length - 1; i++)
            {
                if (i < currentSign)
                    newSigns[i] = Program.currentMap.Signs[i];
                else
                    newSigns[i] = Program.currentMap.Signs[i + 1];
            }
            Program.currentMap.Signs = newSigns;
        }
    }
}
