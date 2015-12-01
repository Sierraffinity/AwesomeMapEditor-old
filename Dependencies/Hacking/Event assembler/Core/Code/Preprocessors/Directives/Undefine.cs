using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors.Directives
{
    class Undefine : IDirective
    {
        #region IDirective Members

        public string Name
        {
            get { return "undef"; }
        }

        public bool RequireIncluding
        {
            get { return true; }
        }

        public int MinAmountOfParameters
        {
            get { return 1; }
        }

        public int MaxAmountOfParameters
        {
            get { return -1; }
        }

        public CanCauseError Apply(string[] parameters, IDirectivePreprocessor host)
        {
            foreach (var item in parameters)
            {
                host.DefCol.Remove(item);
            }
            return CanCauseError.NoError;
        }

        #endregion
    }
}
