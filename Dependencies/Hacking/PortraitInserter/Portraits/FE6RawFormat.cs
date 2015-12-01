using System.Runtime.InteropServices;

namespace PortraitInserter.Portraits
{
    [StructLayout(LayoutKind.Sequential)]
    struct FE6RawFormat
    {
        public readonly int MainPortraitPointer;
        public readonly int MiniPortraitPointer;
        public readonly int PalettePointer;
        public readonly byte MouthXPosition;
        public readonly byte MouthYPosition;
        public readonly byte Unknown1;
        public readonly byte Unknown2;
    }
}
