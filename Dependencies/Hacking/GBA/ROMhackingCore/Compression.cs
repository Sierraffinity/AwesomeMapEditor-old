using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.ROMHacking
{
    [Flags]
    public enum CompressionOperation
    {
        None = 0,
        Decompress = 1,
        Compress = 2,
        Scan = 4,
        Check = 8,
        CompLength = 16,
        DecompLength = 32
    }

    abstract public class Compression
    {
        public CompressionOperation supportedModes
        {
            get;
            private set;
        }
        public String[] fileExtensions
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructs a new compression
        /// </summary>
        /// <param name="supportedModes">Operations supported by the compression</param>
        /// <param name="fileExtensions">Extensions of files where this compression is usually used</param>
        public Compression(CompressionOperation supportedModes, string[] fileExtensions)
        {
            if (supportedModes == CompressionOperation.None)
            {
                throw new NotSupportedException("Compression " + this.ToString() + " doesn't support anything.");
            }
            this.supportedModes = supportedModes;
            this.fileExtensions = fileExtensions;
        }

        public abstract byte[] Decompress(byte[] data);
        public abstract byte[] Decompress(byte[] data, int offset);
        public abstract byte[] Compress(byte[] data);
        public abstract byte[] Compress(byte[] data, int offset);
        public abstract byte[] Compress(byte[] data, int offset, int length);
        public abstract int[] Scan(byte[] data, int sizeMax, int sizeMin, int sizeModulus);
        public abstract int[] Scan(byte[] data, int offset, int sizeMax, int sizeMin, int sizeModulus);
        public abstract int[] Scan(byte[] data, int offset, int length, int sizeMax, int sizeMin, int sizeModulus);
        public abstract bool CanBeDecompressed(byte[] data);
        public abstract bool CanBeDecompressed(byte[] data, int offset);
        public abstract bool CanBeDecompressed(byte[] data, int offset, int maxLength, int minLength);
        public abstract int CompressedLength(byte[] data);
        public abstract int CompressedLength(byte[] data, int offset);
        public abstract int DecompressedDataLenght(byte[] data);
        public abstract int DecompressedDataLenght(byte[] data, int offset);
        
        public bool SupportsOperation(CompressionOperation operation)
        {
            return (this.supportedModes & operation) == operation;
        }

        public override bool Equals(object obj)
        {
            return GetType() == obj.GetType();
        }

        public override int GetHashCode()
        {
            return this.GetType().GetHashCode();
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}
