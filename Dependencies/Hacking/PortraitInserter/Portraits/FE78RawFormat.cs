using System.Runtime.InteropServices;

namespace PortraitInserter.Portraits
{
    [StructLayout(LayoutKind.Sequential)]
    struct FE78RawFormat
    {
        public readonly int MainPortraitPointer;
        public readonly int MiniPortraitPointer;
        public readonly int PalettePointer;
        public readonly int MouthPointer;
        public readonly int GenericPointer;
        public readonly byte MouthXPosition;
        public readonly byte MouthYPosition;
        public readonly byte EyeXPosition;
        public readonly byte EyeYPosition;
        public readonly EyeControl EyeControl;
    }
}
