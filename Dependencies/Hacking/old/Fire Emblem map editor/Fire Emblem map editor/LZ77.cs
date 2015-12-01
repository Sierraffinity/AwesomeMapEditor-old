using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Nintenlord.GBA.Compressions
{
    class LZ77
    {
        BinaryReader binaryReader;

        public int[] Scan(BinaryReader br)
        {
            List<int> result = new List<int>();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                uint header = br.ReadUInt32();
                if (((header & 0xFF) == 0x10) 
                    && (((header >> 8) & 0x1F) == 0)
                    && ((header >> 8) <= 0x8000))
                {
                    result.Add((int)br.BaseStream.Position - 4);
                }
            }
            
            //for (int o = 0; o < result.Count; o++)
            //{
            //    br.BaseStream.Position = result[o];
            //    int size = (br.ReadInt32() >> 8);
            //    int unCompressedSize = 0;
            //    bool isComprssable = true;
            //    while (isComprssable && (unCompressedSize < size) && (br.BaseStream.Position < br.BaseStream.Length))
            //    {
            //        byte isCompressed = br.ReadByte();
            //        for (int i = 0; i < 8; i++)
            //        {
            //            if ((((isCompressed >> (7 - i)) & 1) == 1) && (br.BaseStream.Length > br.BaseStream.Position+1))
            //            {
            //                byte first = br.ReadByte();
            //                byte second = br.ReadByte();
            //                int amountToCopy = 3 + ((first >> 4));
            //                int copyFrom = 1 + ((first & 0xF) << 8) + second;
            //                unCompressedSize += amountToCopy;
            //                isComprssable = (copyFrom < unCompressedSize);
            //            }
            //            else if (br.BaseStream.Length > br.BaseStream.Position)
            //            {
            //                br.BaseStream.Position++;
            //                unCompressedSize++;
            //            }
            //            else
            //            {
            //                isComprssable = false;
            //            }
            //        }
            //    }
            //    if (!isComprssable)
            //    {
            //        result.Remove(result[o]);
            //        o--;
            //    }
            //}
            return result.ToArray();
        }

        public byte[] Compress(byte[] unCompressedData)
        {


            List<byte> CompressedData = new List<byte>();
            CompressedData.Add(0x10);
            CompressedData.Add((byte)(unCompressedData.Length & 0xFF));
            CompressedData.Add((byte)(unCompressedData.Length >> 8 & 0xFF));
            CompressedData.Add((byte)(unCompressedData.Length >> 16 & 0xFF));

            for (int n = 0; (n<<3) < unCompressedData.Length; n++)
            {
                CompressedData.Add(0x00);
                CompressedData.Add(unCompressedData[n << 3]);
                CompressedData.Add(unCompressedData[(n << 3)+1]);
                CompressedData.Add(unCompressedData[(n << 3)+2]);
                CompressedData.Add(unCompressedData[(n << 3)+3]);
                CompressedData.Add(unCompressedData[(n << 3)+4]);
                CompressedData.Add(unCompressedData[(n << 3)+5]);
                CompressedData.Add(unCompressedData[(n << 3)+6]);
                CompressedData.Add(unCompressedData[(n << 3)+7]);
            }

            return CompressedData.ToArray();
        }
    	public byte[] Compress(BinaryReader br, int offset, int size)
        {
            #region Header Writing
            br.BaseStream.Position = offset;
            List<byte> CompressedData = new List<byte>();

            int SlidingWindowSize = 4096;
            int ReadAheadBufferSize = 18;
            int BlockSize = 8;
            Queue<byte> ReadAheadBuffer = new Queue<byte>(ReadAheadBufferSize);
            List<byte> SlidingWindow = new List<byte>(SlidingWindowSize);

            CompressedData.Add((byte)0x10);
            CompressedData.Add((byte)(size & 0xFF));
            CompressedData.Add((byte)((size >> 8) & 0xFF));
            CompressedData.Add((byte)((size >> 16) & 0xFF));

            for (int i = 0; i < ReadAheadBufferSize; i++)
            {
                ReadAheadBuffer.Enqueue(br.ReadByte());
            }
            #endregion

            while ((br.BaseStream.Position < (offset + size)) && (br.BaseStream.Position < br.BaseStream.Length))
            {
                bool[] isCompressed = new bool[BlockSize];
                List<byte[]> Data = new List<byte[]>();

                for (int i = 0; i < BlockSize; i++)
                {
                    if (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        int[] compressedSeed = Search(SlidingWindow, ReadAheadBuffer.ToArray());
                        if (compressedSeed[1] > 2)
                        {
                            isCompressed[i] = true;
                            byte[] data = new byte[2];
                            data[0] = (byte)(((compressedSeed[1] - 3) & 0xF) << 4);
                            data[0] += (byte)(((compressedSeed[0] - 1) >> 8) & 0xF);
                            data[1] = (byte)((compressedSeed[0] - 1) & 0xFF);
                            Data.Add(data);
                            for (int u = 0; u < compressedSeed[1]; u++)
                            {
                                if (SlidingWindow.Count == SlidingWindowSize)
                                {
                                    SlidingWindow.RemoveAt(0);
                                }
                                SlidingWindow.Add(ReadAheadBuffer.Dequeue());
                                ReadAheadBuffer.Enqueue(br.ReadByte());

                                if (br.BaseStream.Position >= br.BaseStream.Length)
                                {
                                    u = compressedSeed[1];
                                }
                            }
                        }
                        else
                        {
                            if (SlidingWindow.Count == SlidingWindowSize)
                            {
                                SlidingWindow.RemoveAt(0);
                            }

                            SlidingWindow.Add(ReadAheadBuffer.Peek());
                            isCompressed[i] = false;
                            Data.Add(new byte[1] { ReadAheadBuffer.Dequeue() });
                            ReadAheadBuffer.Enqueue(br.ReadByte());
                        }
                    }
                    else
                    {
                        isCompressed[i] = false;
                        Data.Add(new byte[1] { 0 });
                    }
                }

                byte BlockData = 0;
                for (int i = 0; i < BlockSize; i++)
                {
                    if (isCompressed[i])
                    {
                        BlockData += (byte)(1 << (BlockSize - 1 - i));
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

        private int[] Search(List<byte> SlidingWindow, byte[] ReadAheadBuffer)
        {
            if (!SlidingWindow.Contains(ReadAheadBuffer[0]))
            {
                return new int[2] { 0, 0 };
            }

            int Last = SlidingWindow.LastIndexOf(ReadAheadBuffer[0]);
            List<int> Offsets = new List<int>();
            Offsets.Add(SlidingWindow.IndexOf(ReadAheadBuffer[0]));

            while (Offsets[Offsets.Count - 1] < Last)
            {
                Offsets.Add(SlidingWindow.IndexOf(ReadAheadBuffer[0], Offsets[Offsets.Count - 1] + 1));
            }

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


        public byte[] UnCompress(byte[] CompressedData)
        {
            int size =(CompressedData[1] + (CompressedData[2] << 8) + (CompressedData[3] << 16));
            List<byte> UnCompressedData = new List<byte>();
            int currentPosition = 4;
            while (UnCompressedData.Count < size + 1 && currentPosition < CompressedData.Length)
            {
                int forward = 1;
                for (int i = 0; i < 8; i++)
                {
                    if (((CompressedData[currentPosition] >> (7 - i)) & 1) == 1)
                    {
                        int amountToCopy = 3 + ((CompressedData[currentPosition + forward] >> 4) & 0xF);
                        int copyFrom = 1 + ((CompressedData[currentPosition + forward] & 0xF) << 8) 
                            + CompressedData[currentPosition + forward + 1];
                        int CopyPosition = UnCompressedData.Count - copyFrom;
                        for (int u = 0; u < amountToCopy; u++)
                        {                            
                            UnCompressedData.Add(UnCompressedData[CopyPosition + (u % copyFrom)]);
                        }
                        forward += 2;
                    }
                    else
                    {
                        UnCompressedData.Add(CompressedData[currentPosition + forward]);
                        forward++;
                    }                    
                }
                currentPosition += forward;
            }
            while (UnCompressedData.Count < size)
            {
                UnCompressedData.Add(0);
            }
            return UnCompressedData.ToArray();
        }
        
        public byte[] UnCompress(BinaryReader br, int offset)
        {
            br.BaseStream.Position = offset;
            int header = br.ReadInt32();
            if (!((header & 0xFF) == 0x10))
            {
                //MessageBox.Show("That's not LZ77 Compressed, doc.");
            }
            int size = (header >> 8);
            List<byte> UnCompressedData = new List<byte>(size);
            while ((UnCompressedData.Count < size) && (br.BaseStream.Position < br.BaseStream.Length))
            {
                byte isCompressed = br.ReadByte();
                int i = 0;
                while ((i < 8) && (UnCompressedData.Count < size))
                {
                    if (((isCompressed >> (7 - i)) & 1) == 1)
                    {
                        byte first = br.ReadByte();
                        byte second = br.ReadByte();
                        int amountToCopy = 3 + ((first >> 4));
                        int copyFrom = 1 + ((first & 0xF) << 8) + second;
                        int position = UnCompressedData.Count;
                        for (int u = 0; u < amountToCopy; u++)
                        {
                            UnCompressedData.Add(UnCompressedData[position - copyFrom + (u % copyFrom)]);
                        }
                    }
                    else
                    {
                        UnCompressedData.Add(br.ReadByte());
                    }
                    i++;
                }
            }
            while ((UnCompressedData.Count < size))
            {
                UnCompressedData.Add(0x00);
            }
            return UnCompressedData.ToArray();
        }
    }
}
