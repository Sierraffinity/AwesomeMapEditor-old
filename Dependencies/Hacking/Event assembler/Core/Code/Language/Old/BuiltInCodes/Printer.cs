using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Code.Language.BuiltInCodes
{
    class Printer : IBuiltInCode
    {
        string name;
        Action<string> printerAction;

        public Action<string> PrinterAction
        {
            get { return printerAction; }
            set { printerAction = value; }
        }

        public Printer(string name)
        {
            this.name = name;
        }

        #region IBuiltInCode Members

        public string Name
        {
            get { return name; }
        }

        public int MinAmountOfParameters
        {
            get { return -1; }
        }

        public int MaxAmountOfParameters
        {
            get { return -1; }
        }

        public CanCauseError FirstPass(string[] code, Context context)
        {
            StringBuilder bldr = new StringBuilder();

            for (int i = 1; i < code.Length; i++)
            {
                string param;
                if (code[i].Equals(EACodeLanguageAssembler.currentOffsetCode, 
                    StringComparison.OrdinalIgnoreCase))
                    param = context.Offset.ToHexString("$");
                else
                    param = code[i];

                bldr.AppendFormat("{0} ", param);
            }
            printerAction(bldr.ToString());

            return CanCauseError.NoError;
        }

        public CanCauseError<bool> SecondPass(string[] code, Context context)
        {
            return false;
        }

        #endregion
    }
}
