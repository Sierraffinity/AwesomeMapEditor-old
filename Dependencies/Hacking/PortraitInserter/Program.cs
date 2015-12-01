using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Nintenlord.Feditor.Core.GameData;
using Nintenlord.Feditor.Core.MemoryManagement;
using Nintenlord.Feditor.Core.Public_API;
using Nintenlord.ROMHacking.GBA;
using PortraitInserter.Portraits;
using Nintenlord.Utility;

namespace PortraitInserter
{
    public class Program : IROMEditor
    {
        #region Static

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Program program = new Program();
            Application.Run(program.form);
        } 
        #endregion

        Form1 form;
        IROM rom;
        IMemoryManager memoryManager;

        ManagedPointer portraitDataPointerPointer;
        ManagedPointer portraitDataPointer;
        List<GeneralizedPointerTableEntry> portraitEntries;
        GameEnum game;


        public Program()
        {
            portraitEntries = new List<GeneralizedPointerTableEntry>();
            form = new Form1();
            form.Program = this;
        }

        #region IROMEditor Members

        public Form EditorForm
        {
            get { return form; }
        }

        public string Name
        {
            get { return "Portrait Inserter"; }
        }

        public string[] CreatorNames
        {
            get { return new string[] { "Nintenlord" }; }
        }

        public void ChangeROM(IMemoryManager memoryManager, IROM rom)
        {
            this.rom = rom;
            this.memoryManager = memoryManager;

            LoadDataFromROM(memoryManager, rom);
        }

        public bool SupportGame(GameEnum game)
        {
            return (GameEnum.FE6 == game) 
                || (GameEnum.FE7U == game) 
                || (GameEnum.FE8U == game);
        }

        #endregion

        private void LoadDataFromROM(IMemoryManager memoryManager, IROM rom)
        {
            int portraitPtrOffset;
            int defaultIndexAmount;
            bool FE6Format;
            switch (game = GameEnumHelpers.GetGame(rom.GameCode))
            {
                case GameEnum.FE6Trans:
                case GameEnum.FE6:
                    portraitPtrOffset = 0x007FD8;
                    defaultIndexAmount = 0xE6;
                    FE6Format = true;
                    break;
                case GameEnum.FE7U:
                    portraitPtrOffset = 0x006B30;
                    defaultIndexAmount = 0xE4;
                    FE6Format = false;
                    break;
                case GameEnum.FE8U:
                    portraitPtrOffset = 0x005524;
                    defaultIndexAmount = 0xAC;
                    FE6Format = false;
                    break;

                //Unknown
                case GameEnum.FE7J:
                case GameEnum.FE8J:
                case GameEnum.FE7E:
                case GameEnum.FE8E:
                default:
                    throw new Exception("Game not supported.");
            }

            portraitDataPointerPointer = memoryManager.Reserve(portraitPtrOffset, 4);
            int tableRawPtr = BitConverter.ToInt32(rom.ReadData(portraitDataPointerPointer), 0);

            int indexSize = Marshal.SizeOf(FE6Format ? typeof(FE6RawFormat) : typeof(FE78RawFormat));
            portraitDataPointer =
                memoryManager.Reserve(
                    Pointer.GetOffset(tableRawPtr + indexSize),
                    defaultIndexAmount * indexSize);

            byte[] portraitData = rom.ReadData(portraitDataPointer);

            portraitEntries.Clear();
            unsafe
            {
                if (FE6Format)
                {
                    FE6RawFormat[] rawData = new FE6RawFormat[defaultIndexAmount];
                    fixed (FE6RawFormat* ptr = rawData)
                    {
                        IntPtr ptr2 = (IntPtr)ptr;
                        Marshal.Copy(portraitData, 0, ptr2, portraitData.Length);
                    }
                    for (int i = 0; i < rawData.Length; i++)
                    {
                        portraitEntries.Add(new GeneralizedPointerTableEntry(
                            rom, memoryManager, rawData[i]));
                    }
                }
                else
                {
                    FE78RawFormat[] rawData = new FE78RawFormat[defaultIndexAmount];
                    fixed (FE78RawFormat* ptr = rawData)
                    {
                        IntPtr ptr2 = (IntPtr)ptr;
                        Marshal.Copy(portraitData, 0, ptr2, portraitData.Length);
                    }
                    for (int i = 0; i < rawData.Length; i++)
                    {
                        portraitEntries.Add(new GeneralizedPointerTableEntry(
                            rom, memoryManager, rawData[i]));
                    }
                }
            }
            CurrentIndex = 0;
            form.MaxIndex = portraitEntries.Count - 1;
        }
        
        public int CurrentIndex
        {
            get;
            set;
        }



        public CanCauseError InsertBitmap(Bitmap bitmap)
        {
            return CanCauseError.NoError;
            //GeneralizedPointerTableEntry newData = portraitEntries[CurrentIndex];

            //return InsertBitmapData(bitmap, newData);
        }

        public CanCauseError AddNewPortrait(Bitmap bitmap)
        {
            return CanCauseError.NoError;
            //GeneralizedPointerTableEntry newData = new GeneralizedPointerTableEntry();
            //portraitEntries.Add(newData);
            //newData.EyeControl = EyeControl.Open;
            //newData.EyePosition = new Point(3, 2);
            //newData.MouthPosition = new Point(3, 4);
            //return InsertBitmapData(bitmap, newData);
        }

        public CanCauseError AddNewPortraitFromCurrent()
        {
            return CanCauseError.NoError;
        }

        private CanCauseError InsertBitmapData(Bitmap bitmap, GeneralizedPointerTableEntry newData)
        {
            var result = PortraitFormat.WriteData(bitmap, memoryManager, rom);
            if (result.CausedError)
            {
                return (CanCauseError)result;
            }
            var ptrs = result.Result;

            //newData.MainPortraitPointer = ptrs[0];
            //newData.MiniPortraitPointer = ptrs[1];
            //newData.PalettePointer = ptrs[2];
            //newData.MouthPointer = ptrs.Length > 3 ? ptrs[3] : ManagedPointer.NullPointer;
            //newData.GenericPointer = ManagedPointer.NullPointer; //Not yet supported

            return CanCauseError.NoError;
        }


        public void InsertEyePosition(Point eyePos)
        {
            portraitEntries[CurrentIndex].EyePosition = eyePos;
        }

        public void InsertEyeControl(EyeControl eyeControl)
        {
            portraitEntries[CurrentIndex].EyeControl = eyeControl;
        }

        public void InsertMouthPosition(Point mouthPos)
        {
            portraitEntries[CurrentIndex].MouthPosition = mouthPos;
        }


        public CanCauseError<Tuple<Bitmap, Point, EyeControl, Point>> LoadPortrait()
        {
            if (rom == null)
            {
                return Tuple.Create((Bitmap)null, Point.Empty, EyeControl.Open, Point.Empty); 
            }
            var entry = portraitEntries[CurrentIndex];

            var bitmap = entry.GetPortrait();
            
            if (bitmap.CausedError)
            {
                return CanCauseError<Tuple<Bitmap,Point,EyeControl,Point>>.Error(bitmap.ErrorMessage);
            }

            return Tuple.Create(
                bitmap.Result, 
                entry.EyePosition,
                entry.EyeControl,
                entry.MouthPosition);
        }
    }
}
