using System;
namespace Nintenlord.Event_assembler.Collections
{
    /// <summary>
    /// Collection to keep track of changes to a array of data
    /// </summary>
    /// <typeparam name="T">Type whose array is to be changed</typeparam>
    interface IDataChange<T>
    {
        /// <summary>
        /// Returns false if Apply can't change the data, else true
        /// </summary>
        bool ChangesAnything { get; }

        /// <summary>
        /// Throws exception if ChangesAnything == false.
        /// Else returns the first offset this instance changes.
        /// </summary>
        int FirstOffset { get; }

        /// <summary>
        /// Throws exception if ChangesAnything == false.
        /// Else returns the last changed offset + 1.
        /// </summary>
        int LastOffset { get; }

        /// <summary>
        /// Adds new change. If old change and new change overlap, new overwrites
        /// </summary>
        /// <param name="offset">Non-negative offset of data</param>
        /// <param name="data">Array of data that changes at offset</param>
        void AddChangedData(int offset, T[] data);

        /// <summary>
        /// Applies changes to array. Array is rezised if necessary
        /// </summary>
        /// <param name="data">Data to apply to</param>
        /// <returns>New data where changes were applied to</returns>
        T[] Apply(T[] data);
    }
}
