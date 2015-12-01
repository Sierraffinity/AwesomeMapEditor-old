using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors.Directives
{
    class Else : IDirective
    {
        #region IDirective Members

        public string Name
        {
            get { return "else"; }
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
                bool toFlip = host.Include.Pop();
                host.Include.Push(!toFlip);
                return CanCauseError.NoError;
            }
            else
            {
                return CanCauseError.Error("#else before any #ifdef or #ifndef.");
            }
        }

        #endregion
    }
}
