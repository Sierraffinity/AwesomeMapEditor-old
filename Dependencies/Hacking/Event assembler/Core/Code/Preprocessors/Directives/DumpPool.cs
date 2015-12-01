using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors.Directives
{
    class DumpPool : IDirective
    {
        #region IDirective Members

        public string Name
        {
            get { return "pool"; }
        }

        public bool RequireIncluding
        {
            get { return true; }
        }

        public int MinAmountOfParameters
        {
            get { return 0; }
        }

        public int MaxAmountOfParameters
        {
            get { return 0; }
        }

        public CanCauseError Apply(string[] parameters, IDirectivePreprocessor host)
        {
            host.Input.AddNewLines(host.Pool.DumpPool());
            return CanCauseError.NoError;
        }

        #endregion
    }
}
