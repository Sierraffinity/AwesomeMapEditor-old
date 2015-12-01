using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Compressor.Compressions;
using System.IO;

namespace Nintenlord.Compressor
{
    interface IUserInterface
    {
        event Action<CompressionOperation> OnRun;
        event Action<int> OnCompressionChanged;

        string InputFile { get; set; }
        string OutputFile { get; set; }

        int InputOffset { get; set; }
        int InputLength { get; set; }
        int OutputOffset { get; set; }

        bool DecompressEnabled { get; set; }
        bool CompressEnabled { get; set; }
        bool ScanEnabled { get; set; }
        bool CheckEnabled { get; set; }
        bool LengthEnabled { get; set; }
        bool DecompLenghtEnabled { get; set; }

        void SetDecompressionNames(string[] names);
        void SetFileExtensions(string[] extensions);
    }
}
