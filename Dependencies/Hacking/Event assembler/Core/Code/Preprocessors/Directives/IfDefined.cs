using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors.Directives
{
    class IfDefined : IDirective
    {
        #region IDirective Members

        public string Name
        {
            get { return "ifdef"; }
        }

        public bool RequireIncluding
        {
            get { return false; }
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
            bool toPush = false;
            foreach (var item in parameters)
            {
                if (host.DefCol.ContainsName(item) || host.IsPredefined(item))
                {
                    toPush = true;
                    break;
                }
            }
            host.Include.Push(toPush);
            return CanCauseError.NoError;
        }

        #endregion
    }
}
