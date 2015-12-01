using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using Nintenlord.GBA.Compressions;

namespace Nintenlord.Animation_importer
{
    class AnimationHandler
    {
        struct Animation
        {
            public int[] DataOffsets, NewDataOffsets;
            public List<int> GraphicsOffsets, NewGraphicsOffsets;
            public uint[] DataHeader;
            public uint[][] Data;
            public List<byte[]> Graphics;
        }

        Animation animation;

        public AnimationHandler(uint[] DataHeader)
        {
            animation = new Animation();
            animation.DataHeader = DataHeader;
        }

        public void MainDataFinder()
        {
            animation.Data = new uint[5][];
            animation.DataOffsets = new int[5];

            for (int i = 0; i < animation.DataOffsets.Length; i++)
            {
                animation.DataOffsets[i] = (int)(animation.DataHeader[i + 3] & 0xF7FFFFFF);
            }

            for (int i = 0; i < animation.DataOffsets.Length; i++)
            {
                if (LZ77.CanBeUnCompressed(animation.DataOffsets[i], Program.Inputbr))
                {
                    animation.Data[i] = LZ77.UnCompressToUInt32(Program.Inputbr, animation.DataOffsets[i]);
                }
                else
                {
                    animation.Data[i] = ToUInt32(Program.Inputbr.ReadBytes(24 * 4));
                }
            }
        }

        public void GraphicsFinder()
        {
            animation.GraphicsOffsets = new List<int>();
            animation.Graphics = new List<byte[]>();           
            {
                int i = 2;
                foreach (uint item in animation.Data[i])
                {
                    int offset = (int)(item & 0xF7FFFFFF);

                    if (((item >> 25) == 0x04) && !(animation.GraphicsOffsets.Contains(offset)) && LZ77.CanBeUnCompressed(offset, Program.Inputbr))
                    {
                        animation.GraphicsOffsets.Add(offset);
                        animation.Graphics.Add(LZ77.UnCompress(Program.Inputbr, offset));
                    }
                }
            }
        }

        public void InsertGraphics()
        {
            animation.NewGraphicsOffsets = new List<int>();
            foreach (byte[] item in animation.Graphics)
            {
                animation.NewGraphicsOffsets.Add((int)Program.Outputbw.BaseStream.Position);
                Program.Outputbw.Write(LZ77.Compress(item));
            }
        }

        public void InsertNewGraphicsPointers()
        {
            for (int i = 0; i < animation.GraphicsOffsets.Count; i++)
            {
                for (int u = 0; u < animation.Data[2].Length; u++)
                {
                    if ((int)(animation.Data[3][u] & 0xF7FFFFFF) == animation.GraphicsOffsets[i])
                    {
                        animation.Data[2][u] = (uint)(animation.NewGraphicsOffsets[i] | 0x08000000);
                    }
                }
            }
        }

        public void InsertNewAnimation()
        {
            animation.NewDataOffsets = new int[5];

            for (int i = animation.Data.Length - 1; i >= 0; i--)
            {
                animation.NewDataOffsets[i] = (int)Program.Outputbw.BaseStream.Position;
                if (i == 0)
                {
                    Program.Outputbw.Write(FromUInt32(animation.Data[i]));
                }
                else
                {
                    Program.Outputbw.Write(LZ77.Compress(animation.Data[i]));
                }
            }
        }

        public void InsertNewAnimationHeader()
        {
            for (int i = 0; i < 3; i++)
            {
                Program.Outputbw.Write(animation.DataHeader[i]);
            }
            for (int i = 0; i < animation.NewDataOffsets.Length; i++)
            {
                Program.Outputbw.Write(animation.NewDataOffsets[i] | 0x08000000);
            }
        }

        public byte[] FromUInt32(uint[] Data)
        {
            byte[] data = new byte[Data.Length * 4];

            unsafe
            {
                fixed (uint* pt = &Data[0])
                {
                    Marshal.Copy(new IntPtr(pt), data, 0, data.Length);
                }
            }

            

            return data;
        }

        private uint[] ToUInt32(byte[] data)
        {
            uint[] Data = new uint[data.Length / 4];

            unsafe
            {
                fixed (uint* pt = &Data[0])
                {
                    Marshal.Copy(data, 0, new IntPtr(pt), data.Length);
                }
            }
            return Data;
        } 
    }
}
