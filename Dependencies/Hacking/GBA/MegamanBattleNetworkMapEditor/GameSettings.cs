using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Security.Permissions;

namespace Nintenlord.MegamanBattleNetworkMapEditor
{
    [Serializable]
    public class GameSettings
    {
        public GameSettings()
        {
            gameName = "test";
            CRC32 = 31;
            ID = new byte[10];
            IDoffset = 4;
            pointerLists = new List<ROMarea>();
            freeSpace = new List<ROMarea>();
        }

        public GameSettings(string gameName, uint CRC32, byte[] ID, int IDoffset)
        {
            this.gameName = gameName;
            this.CRC32 = CRC32;
            this.ID = ID;
            this.IDoffset = IDoffset;
            this.pointerLists = new List<ROMarea>();
            this.freeSpace = new List<ROMarea>();
        }

        string gameName;
        uint CRC32;
        byte[] ID;
        int IDoffset;
        List<ROMarea> pointerLists;
        List<ROMarea> freeSpace;

        [Serializable]
        struct ROMarea
        {
            int offset;
            int size;
        }
    }
}
