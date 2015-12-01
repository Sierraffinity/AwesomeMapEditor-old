using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors.Directives
{
    interface IDirective : INamed<string>, IParameterized
    {
        bool RequireIncluding { get; }

        CanCauseError Apply(string[] parameters, IDirectivePreprocessor host);
    }
}
