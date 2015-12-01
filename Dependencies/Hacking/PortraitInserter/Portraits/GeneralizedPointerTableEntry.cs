using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Nintenlord.Feditor.Core.GameData;
using Nintenlord.Feditor.Core.MemoryManagement;
using Nintenlord.Utility;

namespace PortraitInserter.Portraits
{
    internal sealed class GeneralizedPointerTableEntry : ICloneable
    {
        private readonly IROM rom;
        private readonly IMemoryManager manager;
        private readonly bool fromFE6Format;

        public ManagedPointer MainPortraitPointer;
        public ManagedPointer MiniPortraitPointer;
        public ManagedPointer PalettePointer;
        public ManagedPointer MouthPointer;
        public ManagedPointer GenericPointer;
        public int MainPortraitOffset;
        public int MiniPortraitOffset;
        public int PaletteOffset;
        public int MouthOffset;
        public int GenericOffset;

        public Point MouthPosition;
        public Point EyePosition;
        public EyeControl EyeControl;

        public GeneralizedPointerTableEntry(IROM rom, IMemoryManager manager, FE78RawFormat rawFormat)
        {
            this.rom = rom;
            this.manager = manager;
            this.fromFE6Format = false;
            MainPortraitOffset = GetPtr(rawFormat.MainPortraitPointer);
            MiniPortraitOffset = GetPtr(rawFormat.MiniPortraitPointer);
            PaletteOffset = GetPtr(rawFormat.PalettePointer);
            MouthOffset = GetPtr(rawFormat.MouthPointer);
            GenericOffset = GetPtr(rawFormat.GenericPointer);
            MouthPosition = new Point(rawFormat.MouthXPosition, rawFormat.MouthYPosition);
            EyePosition = new Point(rawFormat.EyeXPosition, rawFormat.EyeYPosition);
            EyeControl = rawFormat.EyeControl;
        }
        
        public GeneralizedPointerTableEntry(IROM rom, IMemoryManager manager, FE6RawFormat rawFormat)
        {
            this.rom = rom;
            this.manager = manager;
            this.fromFE6Format = true;
            MainPortraitOffset = GetPtr(rawFormat.MainPortraitPointer);
            MiniPortraitOffset = GetPtr(rawFormat.MiniPortraitPointer);
            PaletteOffset = GetPtr(rawFormat.PalettePointer);
            MouthOffset = 0;
            GenericOffset = 0;
            MouthPosition = new Point(rawFormat.MouthXPosition, rawFormat.MouthYPosition);
            EyePosition = Point.Empty;
            EyeControl = EyeControl.Open;
        }

        private GeneralizedPointerTableEntry(GeneralizedPointerTableEntry copyMe)
        {
            this.MainPortraitPointer = copyMe.MainPortraitPointer;
            this.MiniPortraitPointer = copyMe.MiniPortraitPointer;
            this.PalettePointer = copyMe.PalettePointer;
            this.MouthPointer = copyMe.MouthPointer;
            this.GenericPointer = copyMe.GenericPointer;
            this.MainPortraitOffset = copyMe.MainPortraitOffset;
            this.MiniPortraitOffset = copyMe.MiniPortraitOffset;
            this.PaletteOffset = copyMe.PaletteOffset;
            this.MouthOffset = copyMe.MouthOffset;
            this.GenericOffset = copyMe.GenericOffset;

            this.MouthPosition = copyMe.MouthPosition;
            this.EyePosition = copyMe.EyePosition;
            this.EyeControl = copyMe.EyeControl;
        }

        private static int GetPtr(int pointer)
        {
            return pointer == 0 ? 0 : Nintenlord.ROMHacking.GBA.Pointer.GetOffset(pointer);
        }

        public CanCauseError<Bitmap> GetPortrait()
        {
            return PortraitFormat.GetPortrait(this, rom);
        }

        #region ICloneable Members

        public object Clone()
        {
            return new GeneralizedPointerTableEntry(this);
        }

        #endregion

        public static explicit operator FE6RawFormat(GeneralizedPointerTableEntry toConvert)
        {
            if (!toConvert.fromFE6Format)
            {
                throw new InvalidCastException();
            }

            return new FE6RawFormat();
        }

        public static explicit operator FE78RawFormat(GeneralizedPointerTableEntry toConvert)
        {
            if (toConvert.fromFE6Format)
            {
                throw new InvalidCastException();
            }

            return new FE78RawFormat();
        }

    }
}
