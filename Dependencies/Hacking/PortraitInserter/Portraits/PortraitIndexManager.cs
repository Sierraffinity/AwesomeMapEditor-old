using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Feditor.Core.Public_API;
using Nintenlord.Feditor.Core.MemoryManagement;
using Nintenlord.Feditor.Core.GameData;
using System.Runtime.InteropServices;
using Nintenlord.ROMHacking.GBA;
using System.Drawing;

namespace PortraitInserter.Portraits
{
    class PortraitIndexManager
    {
        /// <summary>
        /// Add support for some sort of reference counting for portraits shared resources
        /// so that editing one won't change the other
        /// also make sure that just viewing won't cause ROM memory to be moved or edited
        /// </summary>
        private struct DefaultInfo
        {
            public int tablePointerOffset;
            public int indexAmount;
            public bool fe6format;

            public int TableSize
            {
                get
                {
                    return indexAmount * IndexSize;
                }
            }
            public int IndexSize
            {
                get
                {
                    return Marshal.SizeOf(fe6format ? typeof(FE6RawFormat) : typeof(FE78RawFormat));
                }
            }
        }
        static readonly Dictionary<GameEnum, DefaultInfo> defaultInfos;
        static PortraitIndexManager()
        {
            defaultInfos = new Dictionary<GameEnum, DefaultInfo>
            {
                { GameEnum.FE6,
                    new DefaultInfo
                    {
                    tablePointerOffset = 0x007FD8,
                    indexAmount = 0xE7,
                    fe6format = true,
                    }
                },
                { GameEnum.FE6Trans,
                    new DefaultInfo
                    {
                    tablePointerOffset = 0x007FD8,
                    indexAmount = 0xE7,
                    fe6format = true,
                    }
                },
                { GameEnum.FE7U,
                    new DefaultInfo
                    {
                    tablePointerOffset = 0x006B30,
                    indexAmount = 0xE5,
                    fe6format = false,
                    }
                },
                { GameEnum.FE8U,
                    new DefaultInfo
                    {
                    tablePointerOffset = 0x005524,
                    indexAmount = 0xAD,
                    fe6format = false,
                    }
                }
            };
        }
        static bool HasDefaultInfo(GameEnum game)
        {
            return defaultInfos.ContainsKey(game);
        }

        private readonly Dictionary<int, int> refCount = new Dictionary<int, int>();
        GameEnum currentGame;
        ManagedPointer portraitDataPointerPointer;
        ManagedPointer portraitDataPointer;
        List<GeneralizedPointerTableEntry> entries;
        private readonly string portraitProperty = "PortraitCount";

        public int IndexCount
        {
            get { throw new NotImplementedException(); }
        }

        public PortraitIndexManager(IMemoryManager memoryManager, IROM rom, bool edited)
        {
            currentGame = GameEnumHelpers.GetGame(rom.GameCode);
            var defaultInfo = defaultInfos[currentGame];
            portraitDataPointerPointer = memoryManager.Reserve(defaultInfo.tablePointerOffset, 4);
            int tableRawPtr = BitConverter.ToInt32(rom.ReadData(portraitDataPointerPointer), 0);

            int tableEntryCount;
            if (edited)
            {
                string data = rom.GetCustomData(portraitProperty);
                if (data != null)
                {
                    tableEntryCount = Convert.ToInt32(data);
                }
                else
                {
                    //Let's just hope for the best...
                    tableEntryCount = defaultInfo.indexAmount;
                    rom.AddCustomData(portraitProperty, tableEntryCount.ToString());
                }
            }
            else
            {
                tableEntryCount = defaultInfo.indexAmount;
                rom.AddCustomData(portraitProperty, tableEntryCount.ToString());
            }

            portraitDataPointer =
            memoryManager.Reserve(
                Pointer.GetOffset(tableRawPtr + defaultInfo.IndexSize),
                tableEntryCount);
            
            entries = new List<GeneralizedPointerTableEntry>(tableEntryCount);
                
            LoadDataFromROM(memoryManager, rom, defaultInfo.fe6format, tableEntryCount);
            
        }

        private void LoadDataFromROM(IMemoryManager memoryManager, IROM rom, bool useFE6Format, int indexCount)
        {
            byte[] portraitData = rom.ReadData(portraitDataPointer);
            unsafe
            {
                if (useFE6Format)
                {
                    FE6RawFormat[] rawData = new FE6RawFormat[indexCount];
                    fixed (FE6RawFormat* ptr = rawData)
                    {
                        IntPtr ptr2 = (IntPtr)ptr;
                        Marshal.Copy(portraitData, 0, ptr2, portraitData.Length);
                    }
                    for (int i = 0; i < rawData.Length; i++)
                    {
                        entries.Add(new GeneralizedPointerTableEntry(
                            rom, memoryManager, rawData[i]));
                    }
                }
                else
                {
                    FE78RawFormat[] rawData = new FE78RawFormat[indexCount];
                    fixed (FE78RawFormat* ptr = rawData)
                    {
                        IntPtr ptr2 = (IntPtr)ptr;
                        Marshal.Copy(portraitData, 0, ptr2, portraitData.Length);
                    }
                    for (int i = 0; i < rawData.Length; i++)
                    {
                        entries.Add(new GeneralizedPointerTableEntry(
                            rom, memoryManager, rawData[i]));
                    }
                }
            }
        }

        public Tuple<Bitmap, Point?, EyeControl?, Point?> GetPortrait(int index)
        {
            return null;
        }
    }
}
