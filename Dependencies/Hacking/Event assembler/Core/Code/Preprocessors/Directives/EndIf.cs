using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors.Directives
{
    class EndIf : IDirective
    {
        #region IDirective Members

        public string Name
        {
            get { return "endif"; }
        }

        public bool RequireIncluding
        {
            get { return false; }
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
            if (host.Include.Count > 0)
            {
                host.Include.Pop();
                return CanCauseError.NoError;
            }
            else
            {
                return CanCauseError.Error("#endif used without #ifdef or #ifndef.");
            }
        }

        #endregion
    }
}
