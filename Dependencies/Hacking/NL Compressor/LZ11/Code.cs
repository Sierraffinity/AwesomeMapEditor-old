using System;
using System.Collections.Generic;
using Nintenlord;
using Nintenlord.Compressor.Compressions;
using System.IO;

namespace LZ11
{
    /// <summary>
    /// Compression documentation from
    /// http://code.google.com/p/dsdecmp/source/browse/trunk/CSharp/DSDecmp/Formats/Nitro/LZ11.cs
    /// </summary>
    public class LZ11 : Compression
    {
        public LZ11()
            : base(
            //  CompressionOperation.Check 
                CompressionOperation.Compress 
              | CompressionOperation.Decompress,
            //| CompressionOperation.CompLength 
            //| CompressionOperation.Scan
            //| CompressionOperation.DecompLength,
            new string[] { })
        {

        }

        public override byte[] Decompress(byte[] data, int offset)
        {
            byte[] result;
            using (MemoryStream input = new MemoryStream(data, offset, data.Length - offset))
            {
                using (MemoryStream output = new MemoryStream(data.Length * 2))
                {
                    var resultRet = DSDecmp.Formats.Nitro.LZ11.Decompress(input, output);

                    result = resultRet == -1 ? null : output.ToArray();
                }    
            }
            return result;
        }

        public byte[] Decompress(Stream input)
        {
            throw new NotImplementedException();
            BinaryReader reader = new BinaryReader(input);
            
            int header = reader.ReadInt32();
            if ((header & 0xFF) != 0x11)
            {
                return null;
            }
            long decompLength;
            if ((header >> 8) == 0)
            {
                decompLength = reader.ReadInt32();
            }
            else
            {
                decompLength >>= 8;
            }

            List<byte> output = new List<byte>();
            while (output.Count < decompLength)
            {
                byte flags = reader.ReadByte();

                for (int i = 0; i < 8; i++)
                {
                    if ((flags & 0x80) == 0)
                    {
                        //Copy raw byte
                        output.Add(reader.ReadByte());
                    }
                    else
                    {
                        byte first = reader.ReadByte();
                        byte second, third, fourth;

                        int indicator = (first & 0xF0) >> 4;
                        if (indicator == 0)
                        {
                            second = reader.ReadByte();
                            third = reader.ReadByte();
                        }
                        else if (indicator == 1)
                        {
                            second = reader.ReadByte();
                            third = reader.ReadByte();
                            fourth = reader.ReadByte();

                        }
                        else //indicator > 1
                        {
                            second = reader.ReadByte();
                            int length = indicator;
                            int dispMSBcount = first & 0xF;
                            int dispLSBcount = second;


                        }
                    }
                    flags >>= 1;
                }
            }

            return output.ToArray();
        }
        
        public override byte[] Compress(byte[] data, int offset, int length)
        {
            byte[] result;
            using (MemoryStream input = new MemoryStream(data, offset, data.Length - offset))
            {
                using (MemoryStream output = new MemoryStream(data.Length * 2))
                {
                    var resultRet = DSDecmp.Formats.Nitro.LZ11.CompressWithLA(input, length, output);

                    result = resultRet == -1 ? null : output.ToArray();
                }    
            }
            return result;
        }

        public override int[] Scan(byte[] data, int offset, int length, int sizeMax, int sizeMin, int sizeModulus)
        {
            throw new NotImplementedException();
        }

        public override bool CanBeDecompressed(byte[] data, int offset, int maxLength, int minLength)
        {
            throw new NotImplementedException();
        }

        public override int CompressedLength(byte[] data, int offset)
        {
            throw new NotImplementedException();
        }

        public override int DecompressedDataLenght(byte[] data, int offset)
        {
            throw new NotImplementedException();
        }
    }
}
