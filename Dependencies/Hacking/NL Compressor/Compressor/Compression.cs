using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.Compressor.Compressions
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

        public virtual byte[] Decompress(byte[] data)
        {
            return Decompress(data, 0);
        }
        public abstract byte[] Decompress(byte[] data, int offset);
        public virtual byte[] Compress(byte[] data)
        {
            return Compress(data, 0, data.Length);
        }
        public virtual byte[] Compress(byte[] data, int offset)
        {
            return Compress(data, offset, data.Length - offset);
        }
        public abstract byte[] Compress(byte[] data, int offset, int length);
        public virtual int[] Scan(byte[] data, int sizeMax, int sizeMin, int sizeModulus)
        {
            return Scan(data, 0, data.Length, sizeMax, sizeMin, sizeModulus); 
        }
        public virtual int[] Scan(byte[] data, int offset, int sizeMax, int sizeMin, int sizeModulus)
        {
            return Scan(data, offset, data.Length - offset, sizeMax, sizeMin, sizeModulus); 
        }
        public abstract int[] Scan(byte[] data, int offset, int length, int sizeMax, int sizeMin, int sizeModulus);
        public virtual bool CanBeDecompressed(byte[] data)
        {
            return CanBeDecompressed(data, 0, int.MaxValue, 0);
        }
        public virtual bool CanBeDecompressed(byte[] data, int offset)
        {
            return CanBeDecompressed(data, offset, int.MaxValue, 0);
        }
        public abstract bool CanBeDecompressed(byte[] data, int offset, int maxLength, int minLength);
        public virtual int CompressedLength(byte[] data)
        {
            return CompressedLength(data, 0);
        }
        public abstract int CompressedLength(byte[] data, int offset);
        public virtual int DecompressedDataLenght(byte[] data)
        {
            return DecompressedDataLenght(data, 0);
        }
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
