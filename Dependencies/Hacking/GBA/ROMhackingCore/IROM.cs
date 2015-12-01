using System;
using System.IO;

namespace Nintenlord.ROMHacking
{
    interface IROM
    {
        int Length { get; set; }
        bool Opened { get; }
        bool Edited { get; }
        string ROMPath { get; }

        byte[] GetData(int offset, int length);

        void InsertData(int offset, int value);
        void InsertData(int offset, short value);
        void InsertData(int offset, byte value);
        void InsertData(int offset, byte[] data, int index, int length);
        void InsertData(int offset, byte[] data, int index);
        void InsertData(int offset, byte[] data);

        void OpenROM(Stream stream);
        void OpenROM(string path);
        void CloseROM();
        void SaveROM(Stream stream);
        void SaveROM(string path);
        void SaveROM();
        void SaveBackup();

        int[] SearchForValue(short value, int offset, int area);
        int[] SearchForValue(byte[] value);
        int[] SearchForValue(byte[] value, int offset, int area);
        int[] SearchForValue(int value);
        int[] SearchForValue(int value, int offset, int area);
        int[] SearchForValue(short value);
    }
}
