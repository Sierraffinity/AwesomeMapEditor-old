// -----------------------------------------------------------------------
// <copyright file="GenericFE8Template.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Event_Assembler.Core.Code.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nintenlord.Utility;
    using Nintenlord.Event_Assembler.Core.Code.Language.Expression;

    /// <summary>
    /// Generic FE8 code to help disassembly
    /// </summary>
    public class GenericFE8Template : ICodeTemplate
    {
        #region ICodeTemplate Members

        public int MaxRepetition
        {
            get { return 1; }
        }

        public bool EndingCode
        {
            get { return false; }
        }

        public int OffsetMod
        {
            get { return 2; }
        }

        public int AmountOfFixedCode
        {
            get { return 0; }
        }


        public bool Matches(byte[] data, int offset)
        {
            return data[offset + 1] > 1;
        }

        public int GetLengthBytes(byte[] data, int offset)
        {
            return (data[offset] >> 4) * 2;
        }

        public CanCauseError<string[]> GetAssembly(byte[] data, int offset)
        {
            var length = (data[offset] >> 4);
            List<string> code = new List<string>();
            code.Add(this.Name);
            for (int i = 0; i < length; i++)
            {
                code.Add("0x" 
                    + data[offset + i * 2 + 1].ToString("X2") 
                    + data[offset + i * 2].ToString("X2"));
            }
            return code.ToArray();
        }
        

        public bool Matches(Language.Types.Type[] code)
        {
            throw new NotImplementedException();
        }

        public int GetLengthBytes(Parameter<int>[] code)
        {
            throw new NotImplementedException();
        }

        public CanCauseError<byte[]> GetData(Parameter<int>[] code, Func<string, int?> getSymbolValue)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region INamed<string> Members

        public string Name
        {
            get { return "FE8Code"; }
        }

        #endregion

        #region IParameterized Members

        public int MinAmountOfParameters
        {
            get { return -1; }
        }

        public int MaxAmountOfParameters
        {
            get { return -1; }
        }

        #endregion
    }

    public class GenericFE8Ender : ICodeTemplate
    {

        #region ICodeTemplate Members

        public int MaxRepetition
        {
            get { return 1; }
        }

        public bool EndingCode
        {
            get { return true; }
        }

        public int OffsetMod
        {
            get { return 2; }
        }

        public int AmountOfFixedCode
        {
            get { return 0; }
        }



        public bool Matches(byte[] data, int offset)
        {
            return data[offset + 1] == 0x1;
        }

        public int GetLengthBytes(byte[] data, int offset)
        {
            return (data[offset] >> 4) * 2;
        }

        public CanCauseError<string[]> GetAssembly(byte[] data, int offset)
        {
            return new string[] { this.Name };
        }
        
        
        public bool Matches(Language.Types.Type[] code)
        {
            throw new NotImplementedException();
        }

        public int GetLengthBytes(Parameter<int>[] code)
        {
            throw new NotImplementedException();
        }

        public CanCauseError<byte[]> GetData(Parameter<int>[] code, Func<string, int?> getSymbolValue)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region INamed<string> Members

        public string Name
        {
            get { return "FE8End"; }
        }

        #endregion

        #region IParameterized Members

        public int MinAmountOfParameters
        {
            get { return 0; }
        }

        public int MaxAmountOfParameters
        {
            get { return 0; }
        }

        #endregion
    }
}
