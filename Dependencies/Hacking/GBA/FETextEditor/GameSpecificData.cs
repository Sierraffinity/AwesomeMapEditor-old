using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Utility;

namespace FETextEditor
{   
    public class GameSpecificData : INamed<string>
    {
        readonly int huffmanTreePtrOffset;
        readonly int huffmanTreeEndPtrOffset;
        readonly int textPointerTableOffset;
        readonly int textPointerTableLength;
        readonly Dictionary<int, int> defaultFreeSpace;
        readonly uint defaultCRC32;
        readonly string gameCode;

        public uint DefaultCRC32
        {
            get { return defaultCRC32; }
        } 
        public string GameCode
        {
            get { return gameCode; }
        }
        public IDictionary<int, int> DefaultFreeSpace
        {
            get { return defaultFreeSpace; }
        }
        public int HuffmanTreePtrOffset
        {
            get { return huffmanTreePtrOffset; }
        }
        public int HuffmanTreeEndPtrOffset
        {
            get { return huffmanTreeEndPtrOffset; }
        }
        public int TextPointerTableOffset
        {
            get { return textPointerTableOffset; }
        }
        public int TextPointerTableLength
        {
            get { return textPointerTableLength; }
        }

        public GameSpecificData(
            string gameCode, 
            uint defaultCRC32, 
            Dictionary<int, int> defaultFreeSpace, 
            int huffmanTreePtrOffset, 
            int huffmanTreeEndPtrOffset, 
            int textPointerTableOffset, 
            int textPointerTableLength)
        {
            this.huffmanTreePtrOffset = huffmanTreePtrOffset;
            this.huffmanTreeEndPtrOffset = huffmanTreeEndPtrOffset;
            this.textPointerTableOffset = textPointerTableOffset;
            this.textPointerTableLength = textPointerTableLength;
            this.defaultCRC32 = defaultCRC32;
            this.defaultFreeSpace = defaultFreeSpace;
            this.gameCode = gameCode;
        }

        public static readonly GameSpecificData FE6DataJ;
        public static readonly GameSpecificData FE7DataJ;
        public static readonly GameSpecificData FE7DataU;
        public static readonly GameSpecificData FE8DataJ;
        public static readonly GameSpecificData FE8DataU;
        static readonly IDictionary<string, GameSpecificData> datas;

        static GameSpecificData()
        {
            var FE6FreeSpace = new Dictionary<int, int>
            {
                { 0x7E9960, 0x6A0 },
                { 0x7FB980, 0x680 },
			    { 0x7FF0B0, 0xF50 }
            };
            var FE7JFreeSpace = new Dictionary<int, int>
            {
                { 0xDCC200, 0x33E00 },
                { 0xFA5100, 0x1AF00 },
                { 0xFD2E00, 0x5200 },
                { 0xFE4000, 0x1B000 },
                { 0xFDC000, 0x4000 }
            };
            var FE7FreeSpace = new Dictionary<int, int>
            {
                { 0xD00000, 0x100000 },
                { 0xFA5000, 0x1B000 },
                { 0xFD3E00, 0x5200 },
                { 0xFDC000, 0x4000 },
                { 0xFE3100, 0x19F00 },
                { 0xFFE000, 0x1140 }
            };
            var FE8JFreeSpace = new Dictionary<int, int> 
            {
                { 0xBC3A00, 0x3C600 },
                { 0xE47200, 0x98E01 },
                { 0xEF3000, 0x5000 },
                { 0xEFB300, 0xE4D00 },
                { 0xFE4000, 0x1B000}
            };
            var FE8FreeSpace = new Dictionary<int, int>
            {
                { 0xB2A700, 0xD5900 },
                { 0xE47200, 0x98E00 },                
                { 0xEF3000, 0x5000 },
                { 0xEFB00, 0xE4D00 },
                { 0xFE4000, 0x1B000 },
                { 0xB2A610, 0x0D59F0 }
            };

            FE6DataJ = new GameSpecificData("AFEJ", 0xD38763E1, FE6FreeSpace, 0x6E0, 0x6DC, 0x0F635C, 0x3438);
            FE7DataJ = new GameSpecificData("AE7J", 0xF0C10E72, FE7JFreeSpace, 0x6E0, 0x6DC, 0xBBB370, 0x48D4);
            FE7DataU = new GameSpecificData("AE7E", 0x2A524221, FE7FreeSpace, 0x6BC, 0x6B8, 0xB808AC, 0x4CF8);
            FE8DataJ = new GameSpecificData("BE8J", 0x9D76826F, FE8JFreeSpace, 0x6E0, 0x6DC, 0x14D088, 0x3430);
            FE8DataU = new GameSpecificData("BE8E", 0x916777D4, FE8FreeSpace, 0x6E0, 0x6DC, 0x15D48C, 0x3530);
            
            datas = (new GameSpecificData[]
                {
                    FE6DataJ,
                    FE7DataJ,
                    FE7DataU,
                    FE8DataJ,
                    FE8DataU
                }).GetDictionary<string, GameSpecificData>();
        }

        public static GameSpecificData GetData(string code)
        {
            return datas[code];
        }

        public static bool DataExist(string code)
        {
            return datas.ContainsKey(code);
        }

        #region INamed<string> Members

        string INamed<string>.Name
        {
            get { return gameCode; }
        }

        #endregion
    }
}
/*
fe6TextData = TextData { 
                  huffmanTreeOffset = 0x0F300C,
                  huffmanTreeTopPointerOffset = 0x0F6358,
                  huffmanTreeLength = 0x334C,
                  textPointerTableOffset = 0x0F635C,
                  textPointerTableLength = 0x3438
              }

fe7TextData = TextData { 
                  huffmanTreeOffset = 0xB7D71C,
                  huffmanTreeTopPointerOffset = 0xB808A8,
                  huffmanTreeLength = 0x318C,
                  textPointerTableOffset = 0xB808AC,
                  textPointerTableLength = 0x4CF8
              }

fe8TextData = TextData { 
                  huffmanTreeOffset = 0x15A72C,
                  huffmanTreeTopPointerOffset = 0x15D488,
                  huffmanTreeLength = 0x2D5C,
                  textPointerTableOffset = 0x15D48C,
                  textPointerTableLength = 0x3530
              }
 */