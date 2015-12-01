using System;
using System.Collections.Generic;
 

namespace Nintenlord.Event_Assembler.Core.Collections
{
    /// <summary>
    /// Collection for storing definitions and macros.
    /// </summary>
    public interface IDefineCollection
    {
        /// <summary>
        /// Adds a new item with name original and parameters and with replacer.
        /// </summary>
        /// <param name="original"></param>
        /// <param name="replacer"></param>
        /// <param name="parameters"></param>
        void Add(string name, string replacer, params string[] parameters);
        /// <summary>
        /// Adds a new item with name original with 0 parameters and with replacer.
        /// </summary>
        /// <param name="original"></param>
        /// <param name="replacer"></param>
        void Add(string name, string replacer);
        /// <summary>
        /// Checks if this collection contains an item with specified number of parameters.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="numberOfParameters"></param>
        /// <returns></returns>
        bool ContainsName(string name, params string[] parameters);
        /// <summary>
        /// Checks if this collection contains an item with 0 parameters.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool ContainsName(string name);
        /// <summary>
        /// Removes the defined original with the parameters.
        /// </summary>
        /// <param name="original"></param>
        /// <param name="parameters"></param>
        void Remove(string name, params string[] parameters);
        /// <summary>
        /// Removes the defined original with 0 parameters.
        /// </summary>
        /// <param name="original"></param>
        void Remove(string name);
        /// <summary>
        /// Applies the defines on the original and returns the result.
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        bool ApplyDefines(string original, out string newOriginal);

        bool IsValidName(string name);
    }
}
