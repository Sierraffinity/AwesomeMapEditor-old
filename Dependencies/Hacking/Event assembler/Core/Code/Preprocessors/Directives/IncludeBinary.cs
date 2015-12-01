using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Nintenlord.Utility;
using Nintenlord.Event_Assembler.Core.IO;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors.Directives
{
    class IncludeBinary : IDirective
    {
        #region IDirective Members

        public string Name
        {
            get { return "incbin"; }
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
            get { return 1; }
        }

        public CanCauseError Apply(string[] parameters, IDirectivePreprocessor host)
        {
            string file = Nintenlord.Event_Assembler.Core.IO.IOHelpers.FindFile(host.Input.CurrentFile, parameters[0]);
            if (file.Length > 0)
            {
                host.Input.OpenBinaryFile(file);
                return CanCauseError.NoError;
            }
            else
            {
                return CanCauseError.Error("File " + parameters[0] + " not found.");
            }
        }

        #endregion
    }
}
