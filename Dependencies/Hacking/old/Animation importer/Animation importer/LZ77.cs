using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace Nintenlord.GBA.Compressions
{
    static class LZ77
    {
        #region Don't edit this!!!
        private const int SlidingWindowSize = 4096;
        private const int ReadAheadBufferSize = 18;
        private const int BlockSize = 8;
        #endregion

        /// <summary>
        /// Scans the stream for potential LZ77 compressions
        /// </summary>
        /// <param name="br">Stream to scan.</param>
        /// <returns>An array of offsets.</returns>
        static public int[] Scan(BinaryReader br, int offset, int size)
        {
            if (!((offset % 4) == 0))
            {
                offset -= (offset % 4);
            }
            List<int> result = new List<int>();
            br.BaseStream.Position = offset;
            while (br.BaseStream.Position < (offset + size - 4) && br.BaseStream.Position < br.BaseStream.Length - 4)
            {
                uint header = br.ReadUInt32();
                int Size = (int)(header >> 8);

                if (((header & 0xFF) == 0x10)
                    && ((Size & 0x1F) == 0)
                    && (Size <= 0x8000)
                    && (Size > 0))
                {
                    result.Add((int)br.BaseStream.Position - 4);
                }
            }

            for (int i = 0; i < result.Count; i++)
            {
                if (!CanBeUnCompressed(result[i], br))
                {
                    result.RemoveAt(i);
                    i--;
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// Checks weather the data can be uncompressed.
        /// </summary>
        /// <param name="offset">Offset of the compressed data in the stream.</param>
        /// <param name="br">Stream where the compressed data is.</param>
        /// <returns>Returns "true" if data can be uncompressed, false if can't.</returns>
        static public bool CanBeUnCompressed(int offset, BinaryReader br)
        {
            br.BaseStream.Position = offset;
            int header = br.ReadInt32();
            if ((header & 0xFF) != 0x10)
                return false;
            int size = (header >> 8);
            int UncompressedDataSize = 0;

            while ((UncompressedDataSize < size))
            {
                if (br.BaseStream.Position + 1 > br.BaseStream.Length)
                {
                    return false;
                }
                byte isCompressed = br.ReadByte();
                int i = 0;
                while ((i < 8) && (UncompressedDataSize < size))
                {
                    if (((isCompressed >> (7 - i)) & 1) == 1)
                    {
                        if (br.BaseStream.Position + 2 > br.BaseStream.Length)
                        {
                            return false;
                        }
                        byte first = br.ReadByte();
                        byte second = br.ReadByte();

                        int amountToCopy = 3 + ((first >> 4));
                        int copyFrom = 1 + ((first & 0xF) << 8) + second;

                        if (copyFrom > UncompressedDataSize)
                        {
                            return false;
                        }

                        UncompressedDataSize += amountToCopy;
                    }
                    else
                    {
                        if (br.BaseStream.Position + 1 > br.BaseStream.Length)
                        {
                            return false;
                        }
                        br.BaseStream.Position++;
                        UncompressedDataSize++;
                    }
                    i++;
                }
            }
            return true;
        }

        /// <summary>
        /// Gets the lenght of the compressed data
        /// </summary>
        /// <param name="br">The stream with data</param>
        /// <param name="offset">The position of the data in stream</param>
        /// <returns>The lenht of the data</returns>
        static public int GetCompressedDataLenght(BinaryReader br, int offset)
        {
            br.BaseStream.Position = offset;
            int size = (int)(br.ReadUInt32() >> 8);
            int UncompSize = 0;
            while (UncompSize < size)
            {
                byte isCompressed = br.ReadByte();
                for (int i = 0; i < 8; i++)
                {
                    if (UncompSize < size)
                    {
                        if ((isCompressed & 0x80) != 0)
                        {
                            if (br.BaseStream.Position < br.BaseStream.Length - 1)
                            {
                                byte first = br.ReadByte();
                                byte second = br.ReadByte();
                                int amountToCopy = 3 + ((first >> 4));
                                UncompSize += amountToCopy;
                            }
                        }
                        else
                        {
                            if (br.BaseStream.Position < br.BaseStream.Length)
                            {
                                br.BaseStream.Position++;
                                UncompSize++;
                            }
                        }
                    }
                    isCompressed <<= 1;
                }
            }
            int Size = (int)(br.BaseStream.Position - offset);
            if (Size % 4 != 0)
                Size += 4 - Size % 4;
            return size;
        }

        /// <summary>
        /// Compresses data from stream with LZ77
        /// </summary>
        /// <param name="br">Stream with the data</param>
        /// <param name="offset">Position of the data in stream</param>
        /// <param name="size">Size of the data</param>
        /// <returns>An array of LZ77 compressed data</returns>
        static public byte[] Compress(BinaryReader br, int offset, int size)
        {
            br.BaseStream.Position = offset;
            byte[] uncompressedData;
            if (br.BaseStream.Length >= offset + size)
            {
                uncompressedData = br.ReadBytes(size);
            }
            else
            {
                MessageBox.Show("End of stream.");
                return null;
            }
            br.Close();

            byte[] CompressedData = Compress(uncompressedData);

            return CompressedData;
        }

        /// <summary>
        /// Compresses from UInt32 to byte
        /// </summary>
        /// <param name="UncompressedData">Array of uncompressed data</param>
        /// <returns>Array of compressed data</returns>
        static public byte[] Compress(uint[] UncompressedData)
        {
            byte[] newData = new byte[UncompressedData.Length * 4];

            unsafe
            {
                fixed (uint* pointer = &UncompressedData[0])
                {
                    IntPtr intPointer = new IntPtr(pointer);
                    Marshal.Copy(intPointer, newData, 0, newData.Length);
                }
            }

            byte[] compressedData = Compress(newData);
            return compressedData;
        }

        /// <summary>
        /// Compresses an array of bytes
        /// </summary>
        /// <param name="unCompressedData">Data to compress</param>
        /// <returns>Compressed data</returns>
        static public byte[] Compress(byte[] unCompressedData)
        {
            #region Header and collections
            List<byte> CompressedData = new List<byte>();
            Queue<byte> ReadAheadBuffer = new Queue<byte>(ReadAheadBufferSize);
            List<byte> SlidingWindow = new List<byte>(SlidingWindowSize);

            int position = 0;
            CompressedData.Add(0x10);
            CompressedData.AddRange(BitConverter.GetBytes(unCompressedData.Length));
            CompressedData.RemoveAt(4);
            #endregion

            while (position < ReadAheadBufferSize)
            {
                ReadAheadBuffer.Enqueue(unCompressedData[position]);
                position++;
            }

            while (ReadAheadBuffer.Count > 0)
            {
                bool[] isCompressed = new bool[BlockSize];
                List<byte[]> Data = new List<byte[]>();

                #region Block data
                for (int i = (BlockSize - 1); i >= 0; i--)
                {
                    int[] DataSeed = Search(SlidingWindow, ReadAheadBuffer.ToArray());
                    byte[] data;

                    if (DataSeed[1] > 2)
                    {
                        isCompressed[i] = true;
                        data = new byte[2];
                        data[0] = (byte)(((DataSeed[1] - 3) & 0xF) << 4);
                        data[0] += (byte)(((DataSeed[0] - 1) >> 8) & 0xF);
                        data[1] = (byte)((DataSeed[0] - 1) & 0xFF);
                        Data.Add(data);
                    }
                    else if (DataSeed[1] >= 0)
                    {
                        DataSeed[1] = 1;
                        data = new byte[1] { ReadAheadBuffer.Peek() };
                        isCompressed[i] = false;
                        Data.Add(data);
                    }
                    else
                    {
                        isCompressed[i] = false;
                    }

                    for (int u = 0; u < DataSeed[1]; u++)
                    {
                        if (SlidingWindow.Count >= SlidingWindowSize)
                        {
                            SlidingWindow.RemoveAt(0);
                        }
                        SlidingWindow.Add(ReadAheadBuffer.Dequeue());
                    }

                    while ((ReadAheadBuffer.Count < ReadAheadBufferSize) && (position < unCompressedData.Length))
                    {
                        ReadAheadBuffer.Enqueue(unCompressedData[position]);
                        position++;
                    }
                }
                #endregion

                byte BlockData = 0;
                for (int i = 0; i < BlockSize; i++)
                {
                    if (isCompressed[i])
                    {
                        BlockData += (byte)(1 << i);
                    }
                }
                CompressedData.Add(BlockData);
                foreach (byte[] var in Data)
                {
                    foreach (byte n in var)
                    {
                        CompressedData.Add(n);
                    }
                }
            }

            while ((CompressedData.Count % 4) != 0)
            {
                CompressedData.Add(0x00);
            }

            return CompressedData.ToArray();
        }
        
        static private int[] Search(List<byte> SlidingWindow, byte[] ReadAheadBuffer)
        {
            if (ReadAheadBuffer.Length == 0)
                return new int[2] { 0, -1 };

            List<int> Offsets = new List<int>();

            for (int i = 0; i < SlidingWindow.Count - 1; i++)
            {
                if (SlidingWindow[i] == ReadAheadBuffer[0])
                {
                    Offsets.Add(i);
                }
            }

            if (Offsets.Count == 0)
                return new int[2] { 0, 0 };

            for (int i = 1; i < ReadAheadBuffer.Length; i++)
            {
                for (int u = 0; u < Offsets.Count; u++)
                {
                    if (!((SlidingWindow[Offsets[u] + (i % (SlidingWindow.Count - Offsets[u]))]) == ReadAheadBuffer[i]))
                    {
                        if (Offsets.Count > 1)
                        {
                            Offsets.Remove(Offsets[u]);
                            u--;
                        }
                    }
                }
                if (Offsets.Count < 2)
                {
                    i = ReadAheadBuffer.Length;
                }
            }
            int size = 1;
            bool A = true;
            while ((ReadAheadBuffer.Length > size) && A)
            {
                if (SlidingWindow[Offsets[0] + (size % (SlidingWindow.Count - Offsets[0]))] == ReadAheadBuffer[size])
                {
                    size++;
                }
                else
                {
                    A = false;
                }

            }

            return new int[2] { SlidingWindow.Count - Offsets[0], size };
        }

        /// <summary>
        /// Uncompresser LZ77 data from a stream
        /// </summary>
        /// <param name="br">Stream with the data</param>
        /// <param name="offset">Offset of the data</param>
        /// <returns>An array of data</returns>
        static public byte[] UnCompress(BinaryReader br, int offset)
        {
            br.BaseStream.Position = offset;
            uint header = br.ReadUInt32();
            if (!((header & 0xFF) == 0x10))
            {
                MessageBox.Show("That's not LZ77 Compressed, doc.");
                return null;
            }
            int size = (int)(header >> 8);
            List<byte> UnCompressedData = new List<byte>(size);
            while ((UnCompressedData.Count < size) && (br.BaseStream.Position < br.BaseStream.Length))
            {
                byte isCompressed = br.ReadByte();
                int i = 0;
                while ((i < 8) && (UnCompressedData.Count < size))
                {
                    if ((isCompressed & 0x80) != 0)
                    {
                        byte first = br.ReadByte();
                        byte second = br.ReadByte();
                        int amountToCopy = 3 + ((first >> 4));
                        int copyFrom = 1 + ((first & 0xF) << 8) + second;
                        int position = UnCompressedData.Count;
                        for (int u = 0; u < amountToCopy; u++)
                        {
                            int index = position - copyFrom + (u % copyFrom);
                            if (index >= 0 && index < UnCompressedData.Count)
                            {
                                UnCompressedData.Add(UnCompressedData[index]);
                            }
                            else
                            {
                                string message = "Error in data.\nCan't be uncompressed\nOffset: " + Convert.ToString(offset, 16) + "\nStream position: " + Convert.ToString(br.BaseStream.Position, 16);
                                MessageBox.Show(message);
                                return null;
                            }
                        }
                    }
                    else
                    {
                        UnCompressedData.Add(br.ReadByte());
                    }
                    i++;
                    isCompressed <<= 1;
                }
            }
            while ((UnCompressedData.Count < size))
            {
                UnCompressedData.Add(0x00);
            }
            return UnCompressedData.ToArray();
        }

        /// <summary>
        /// Uncompresses the array of LZ77 data
        /// </summary>
        /// <param name="CompressedData">The LZ77 data</param>
        /// <returns>Uncompressed data</returns>
        static public byte[] UnCompress(byte[] CompressedData)
        {
            if (CompressedData[0] != 0x10)
            {
                MessageBox.Show("That's not LZ77 Compressed, doc.");
                return null;
            }

            int size = (CompressedData[1] + (CompressedData[2] << 8) + (CompressedData[3] << 16));
            List<byte> UnCompressedData = new List<byte>();
            int currentPosition = 4;
            while (UnCompressedData.Count <= size && currentPosition < CompressedData.Length)
            {
                byte isCompressed = CompressedData[currentPosition++];
                for (int i = 0; i < 8; i++)
                {
                    if ((isCompressed & 0x80) == 1)
                    {
                        int amountToCopy = 3 + ((CompressedData[currentPosition] >> 4) & 0xF);
                        int copyFrom = 1 + ((CompressedData[currentPosition++] & 0xF) << 8)
                            + CompressedData[currentPosition++];
                        int CopyPosition = UnCompressedData.Count - copyFrom;
                        for (int u = 0; u < amountToCopy; u++)
                        {
                            if ((CopyPosition + (u % copyFrom)) < UnCompressedData.Count)
                            {
                                UnCompressedData.Add(UnCompressedData[CopyPosition + (u % copyFrom)]);
                            }
                            else
                            {
                                MessageBox.Show("Error in data.\nCan't be uncompressed.");
                                return null;
                            }
                        }
                    }
                    else
                    {
                        UnCompressedData.Add(CompressedData[currentPosition++]);
                    }
                    isCompressed <<= 1;
                }
            }
            while (UnCompressedData.Count < size)
            {
                UnCompressedData.Add(0);
            }
            return UnCompressedData.ToArray();
        }

        /// <summary>
        /// Uncompresses to Uint32
        /// </summary>
        /// <param name="br">Stream where the data to uncompress is</param>
        /// <param name="offset">Offset of the data</param>
        /// <returns>Array of compressed data</returns>
        static public uint[] UnCompressToUInt32(BinaryReader br, int offset)
        {
            byte[] uncompressedData = UnCompress(br, offset);
            uint[] newUncompressedData = new uint[uncompressedData.Length / 4];

            unsafe
            {
                fixed (uint* pointer = &newUncompressedData[0])
                {
                    IntPtr intPointer = new IntPtr(pointer);
                    Marshal.Copy(uncompressedData, 0, intPointer, uncompressedData.Length);
                }
            }
            return newUncompressedData;
        }

        
    }
}
