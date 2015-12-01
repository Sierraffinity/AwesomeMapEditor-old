using System;
using System.Text;
using Nintenlord.Event_assembler;
using Nintenlord.Event_assembler.Collections;

namespace Nintenlord.Event_assembler.Code
{
    /// <summary>
    /// Assembly language for assembling and disassembling
    /// </summary>
    interface ICodeLanguage : IDisassembler, IAssembler
    {
        /// <summary>
        /// Checks if word is reserved in this language
        /// </summary>
        /// <param name="word">Word to check</param>
        /// <returns>True if word is reserved, else false</returns>
        bool IsReserved(string word);
        /// <summary>
        /// Name of the language
        /// </summary>
        string Name { get; }
    }
}
