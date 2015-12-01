using System;
using System.Collections.Generic;
using Nintenlord;

namespace Nintenlord.Compressor.Compressions
{
    public unsafe class LZ77 : Compression
    {
        public LZ77() : base(
              CompressionOperation.Check 
            | CompressionOperation.Compress 
            | CompressionOperation.Decompress
            | CompressionOperation.CompLength 
            | CompressionOperation.Scan
            | CompressionOperation.DecompLength,
            new string[]{".gba"})
        {

        }

        /// <summary>
        /// Decompresses LZ77 data
        /// </summary>
        /// <param name="data">Data where compressed data is</param>
        /// <returns>Decompressed data or null if decompression fails</returns>
        public override byte[] Decompress(byte[] data)
        {
            fixed (byte* ptr = &data[0])
            {
                return Decompress(ptr);
            }
        }

        /// <summary>
        /// Decompresses LZ77 data
        /// </summary>
        /// <param name="data">Data where compressed data is</param>
        /// <param name="offset">Offset of the compressed data</param>
        /// <returns>Decompressed data or null if decompression fails</returns>
        public override byte[] Decompress(byte[] data, int offset)
        {
            fixed (byte* ptr = &data[offset])
            {
                return Decompress(ptr);
            }
        }

        /// <summary>
        /// Decompresses LZ77 data
        /// </summary>
        /// <param name="source">Pointer to data to decompress</param>
        /// <returns>Decompressed data or null if decompression fails</returns>
        public byte[] Decompress(byte* source)
        {
            byte[] uncompressedData = new byte[(*(uint*)source) >> 8];
            if (uncompressedData.Length > 0)
            {
                fixed (byte* destination = &uncompressedData[0])
                {
                    if (!Decompress(source, destination))
                        uncompressedData = null;
                }
            }
            return uncompressedData;
        }

        /// <summary>
        /// Decompresses LZ77 data
        /// </summary>
        /// <param name="source">Pointer to compressed data</param>
        /// <param name="target">Pointer to where uncompressed data goes</param>
        /// <returns>True if successful, else false</returns>
        public bool Decompress(byte* source, byte* target)
        {
            return Nintenlord.ROMHacking.GBA.Compressions.LZ77.Decompress(source, target);
        }
        

        /// <summary>
        /// Compresses data with LZ77
        /// </summary>
        /// <param name="data">Data to compress</param>
        /// <returns>Compressed data</returns>
        public override byte[] Compress(byte[] data)
        {
            fixed (byte* pointer = &data[0])
            {
                return Compress(pointer, data.Length);
            }
        }

        /// <summary>
        /// Compresses data with LZ77
        /// </summary>
        /// <param name="data">Data to compress</param>
        /// <param name="index">Beginning offset of the data to compress</param>
        /// <returns>Compressed data</returns>
        public override byte[] Compress(byte[] data, int offset)
        {
            fixed (byte* pointer = &data[offset])
            {
                return Compress(pointer, data.Length - offset);
            }
        }

        /// <summary>
        /// Compresses data with LZ77
        /// </summary>
        /// <param name="data">Data to compress</param>
        /// <param name="index">Beginning offset of the data to compress</param>
        /// <param name="length">Length of the data to compress</param>
        /// <returns>Compressed data</returns>
        public override byte[] Compress(byte[] data, int offset, int length)
        {
            fixed (byte* pointer = &data[offset])
            {
                return Compress(pointer, length);
            }
        }

        /// <summary>
        /// Compresses data with LZ77
        /// </summary>
        /// <param name="source">Pointer to beginning of the data</param>
        /// <param name="lenght">Lenght of the data to compress in bytes</param>
        /// <returns>Compressed data</returns>
        public byte[] Compress(byte* source, int lenght)
        {
            return Nintenlord.ROMHacking.GBA.Compressions.LZ77.Compress(source, lenght);
        }

        /// <summary>
        /// Scans the data for potential LZ77 compressions
        /// </summary>
        /// <param name="data">Data to scan</param>
        /// <param name="sizeMax">Maximun size of uncompressed data allowed</param>
        /// <param name="sizeMin">Minimun size of uncompressed data allowed</param>
        /// <param name="sizeModulus">Modulus for size of uncompressed data</param>
        /// <returns></returns>
        public override int[] Scan(byte[] data, int sizeMax, int sizeMin, int sizeModulus)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scans the data for potential LZ77 compressions
        /// </summary>
        /// <param name="data">Data to scan</param>
        /// <param name="offset">Starting offset of are to scan</param>
        /// <param name="sizeMax">Maximun size of uncompressed data allowed</param>
        /// <param name="sizeMin">Minimun size of uncompressed data allowed</param>
        /// <param name="sizeModulus">Modulus for size of uncompressed data</param>
        /// <returns></returns>
        public override int[] Scan(byte[] data, int offset, int sizeMax, int sizeMin, int sizeModulus)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scans the data for potential LZ77 compressions
        /// </summary>
        /// <param name="data">Data to scan</param>
        /// <param name="offset">Starting offset of are to scan</param>
        /// <param name="length">Size of the area to scan</param>
        /// <param name="sizeMax">Maximun size of uncompressed data allowed</param>
        /// <param name="sizeMin">Minimun size of uncompressed data allowed</param>
        /// <param name="sizeModulus">Modulus for size of uncompressed data</param>
        /// <returns></returns>
        public override int[] Scan(byte[] data, int offset, int length, int sizeMax, int sizeMin, int sizeModulus)
        {
            return Nintenlord.ROMHacking.GBA.Compressions.LZ77.Scan(data, offset, length, sizeModulus, sizeMin, sizeMax);
        }

        /// <summary>
        /// Scans an area in memory for potential LZ77 compressions
        /// </summary>
        /// <param name="pointer">Pointer to start of area to scan</param>
        /// <param name="amount">Size of the area to scan in bytes</param>
        /// <returns>An array of offsets relative to the beginning of scan area</returns>
        public int[] Scan(byte* pointer, int amount, int sizeMultible, int minSize, int maxSize)
        {
            return Nintenlord.ROMHacking.GBA.Compressions.LZ77.Scan(pointer, amount, sizeMultible, minSize, maxSize);
        }


        /// <summary>
        /// Checks if data can be uncompressed
        /// </summary>
        /// <param name="data">Data with data to test</param>
        /// <returns>True if data can be uncompressed, else false</returns>
        public override bool CanBeDecompressed(byte[] data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if data can be uncompressed
        /// </summary>
        /// <param name="data">Data with data to test</param>
        /// <param name="offset">Offset of the data to test in data</param>
        /// <returns>True if data can be uncompressed, else false</returns>
        public override bool CanBeDecompressed(byte[] data, int offset)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if data can be uncompressed
        /// </summary>
        /// <param name="data">Data with data to test</param>
        /// <param name="offset">Offset of the data to test in data</param>
        /// <param name="maxLength">Maximun length the compressed data can have</param>
        /// <param name="minLength">Minimun length the compressed data can have</param>
        /// <returns>True if data can be uncompressed, else false</returns>
        public override bool CanBeDecompressed(byte[] data, int offset, int maxLength, int minLength)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Checks if data can be uncompressed
        /// </summary>
        /// <param name="source">Pointer to beginning of data</param>
        /// <returns>True if data can be uncompressed, else false</returns>
        public static bool CanBeDecompressed(byte* source, int minSize, int maxSize)
        {
            int lenght = GetCompressedDataLenght(source);
            return (lenght != -1) && (lenght <= maxSize);
        }


        /// <summary>
        /// Gets the lenght of the compressed data
        /// </summary>
        /// <param name="data">Data where compressed data is</param>
        /// <returns>Returns lenght or -1 if can't be uncompressed</returns>
        public override int CompressedLength(byte[] data)
        {
            fixed (byte* pointer = &data[0])
            {
                return GetCompressedDataLenght(pointer);
            }
        }

        /// <summary>
        /// Gets the lenght of the compressed data
        /// </summary>
        /// <param name="data">Data where compressed data is</param>
        /// <param name="offset">The position of the data</param>
        /// <returns>Returns lenght or -1 if can't be uncompressed</returns>
        public override int CompressedLength(byte[] data, int offset)
        {
            fixed (byte* pointer = &data[offset])
            {
                return GetCompressedDataLenght(pointer);
            }
        }
        
        /// <summary>
        /// Gets the lenght of the compressed data
        /// </summary>
        /// <param name="source">Pointer to the data to check</param>
        /// <returns>Returns lenght or -1 if can't be uncompressed</returns>
        public static int GetCompressedDataLenght(byte* source)
        {
            return Nintenlord.ROMHacking.GBA.Compressions.LZ77.GetCompressedDataLenght(source);
        }


        public override int DecompressedDataLenght(byte[] data)
        {
            return DecompressedDataLenght(data, 0);
        }

        public override int DecompressedDataLenght(byte[] data, int offset)
        {
            if (data[offset] == 0x10)
            {
                return data[offset + 1] + (data[offset + 2] << 8) + (data[offset + 3] << 0x10);
            }
            else
            {
                return -1;
            }
        }
    }
}
