using System.IO;
using Nintenlord.MemoryManagement;
using Nintenlord.Utility;

namespace FETextEditor.TextFormatter
{
    interface IMetadataHandler
    {
        void AddMetadata(int index, IMemoryPointer textPointer, TextWriter output);
    }

    class NoMetadata : IMetadataHandler
    {

        #region IMetadata Members

        public void AddMetadata(int index, IMemoryPointer textPointer, TextWriter output)
        {
            output.WriteLine("##Index: {0}", index.ToHexString("0x"));
            output.WriteLine("##Memory: {0}", textPointer);
            output.WriteLine();
        }

        #endregion
    }
}
