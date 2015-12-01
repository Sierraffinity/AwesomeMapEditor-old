using System;
using Nintenlord.Event_Assembler.Core.Code.Language.Expression;
using Nintenlord.Utility;
using EAType = Nintenlord.Event_Assembler.Core.Code.Language.Types.Type;

namespace Nintenlord.Event_Assembler.Core.Code.Templates
{
    /// <summary>
    /// Template for assembly code
    /// </summary>
    public interface ICodeTemplate : INamed<string>, IParameterized
    {
        /// <summary>
        /// Max repetition of this template as parameters
        /// </summary>
        int MaxRepetition { get; }
        /// <summary>
        /// If this template should end disassembling 
        /// </summary>
        bool EndingCode { get; }
        /// <summary>
        /// The modulus where the offset of this template should be 0
        /// </summary>
        int OffsetMod { get; }
        /// <summary>
        /// Amount of fixed code in this template.
        /// </summary>
        int AmountOfFixedCode { get; }

        /// <summary>
        /// Checks if code split to parameters matches this template
        /// </summary>
        /// <param name="parameterTypes">Code split to parameters.</param>
        /// <returns>True if code fits this template, else false</returns>
        /// <exception cref="NullReferanceException">If code is null or any of items in code are null.</exception>
        bool Matches(EAType[] parameterTypes);
        /// <summary>
        /// Gets the length of code that matches this template in bytes
        /// </summary>
        /// <param name="parameters">Code whose lenght to match</param>
        /// <returns>Lenght of the code in bytes</returns>
        int GetLengthBytes(Parameter<int>[] parameters);
        /// <summary>
        /// Gets the binary data of code
        /// </summary>
        /// <param name="parameters">Code that matches this template</param>
        /// <param name="messageHandler">Message handling class</param>
        /// <returns>Binary data of the code</returns>
        CanCauseError<byte[]> GetData(Parameter<int>[] parameters, Func<string, int?> getSymbolValue);

        /// <summary>
        /// Checks if binary data matches the
        /// </summary>
        /// <param name="data">Binary data</param>
        /// <param name="offset">Position on the data to match</param>
        /// <returns>True if data at offset matches this template, else false</returns>
        /// <exception cref="IndexOutOfRange">If offset is smaller than 0 or larger or equal than data.Lenght</exception>
        bool Matches(byte[] data, int offset);
        /// <summary>
        /// Gets the length of binary data that matches this template in bytes
        /// </summary>
        /// <param name="code">Binary data</param>
        /// <param name="offset">Position on the data to match</param>
        /// <returns>Lenght of the code in bytes</returns>
        /// <exception cref="IndexOutOfRange">If offset is smaller than 0 or larger or equal than data.Lenght</exception>
        int GetLengthBytes(byte[] data, int offset);
        /// <summary>
        /// Gets the assembly code of the binary data matching this template
        /// </summary>
        /// <param name="code">Binary data</param>
        /// <param name="offset">Position of the code on the data</param>
        /// <param name="messageHandler">Message handling class</param>
        /// <returns>Assembly code matching the binary data</returns>
        /// <exception cref="IndexOutOfRange">If offset is smaller than 0 or larger or equal than data.Lenght</exception>
        CanCauseError<string[]> GetAssembly(byte[] data, int offset);
    }
}
