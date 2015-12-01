using System;
using System.Collections.Generic;
using Nintenlord;

namespace Nintenlord.Compressor.Compressions
{
    public class RLE : Compression
    {
        public RLE()
            : base(CompressionOperation.None //delete unsupported abilities
                //CompressionModes.Checkeble
                //| CompressionModes.Compressable
                //CompressionModes.Decompressable
                //| CompressionModes.Lengthable
                //| CompressionModes.Scannable
            , new string[]{".gba"})
        {

        }

        public override byte[] Decompress(byte[] data)
        {
            throw new NotImplementedException();
        }

        public override byte[] Decompress(byte[] data, int offset)
        {
            throw new NotImplementedException();
        }

        public override byte[] Compress(byte[] data)
        {
            throw new NotImplementedException();
        }

        public override byte[] Compress(byte[] data, int offset)
        {
            throw new NotImplementedException();
        }

        public override byte[] Compress(byte[] data, int offset, int length)
        {
            throw new NotImplementedException();
        }

        public override int[] Scan(byte[] data, int sizeMax, int sizeMin, int sizeModulus)
        {
            throw new NotImplementedException();
        }

        public override int[] Scan(byte[] data, int offset, int sizeMax, int sizeMin, int sizeModulus)
        {
            throw new NotImplementedException();
        }

        public override int[] Scan(byte[] data, int offset, int length, int sizeMax, int sizeMin, int sizeModulus)
        {
            throw new NotImplementedException();
        }

        public override bool CanBeDecompressed(byte[] data)
        {
            throw new NotImplementedException();
        }

        public override bool CanBeDecompressed(byte[] data, int offset)
        {
            throw new NotImplementedException();
        }

        public override bool CanBeDecompressed(byte[] data, int offset, int maxLength, int minLength)
        {
            throw new NotImplementedException();
        }

        public override int CompressedLength(byte[] data)
        {
            throw new NotImplementedException();
        }

        public override int CompressedLength(byte[] data, int offset)
        {
            throw new NotImplementedException();
        }

        public override int DecompressedDataLenght(byte[] data)
        {
            throw new NotImplementedException();
        }

        public override int DecompressedDataLenght(byte[] data, int offset)
        {
            throw new NotImplementedException();
        }
    }
}
