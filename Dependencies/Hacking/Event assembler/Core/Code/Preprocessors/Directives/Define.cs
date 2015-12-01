using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Utility;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors.Directives
{
    class Define : IDirective
    {
        #region Parsing helpers
        private static Dictionary<char, char> uniters;
        private static List<char> macroSeparators;
        private static List<char> parameterSeparators;
        static Define()
        {
            uniters = new Dictionary<char, char>();
            macroSeparators = new List<char>();
            parameterSeparators = new List<char>();

            uniters['"'] = '"';
            uniters['('] = ')';
            uniters['['] = ']';
            macroSeparators.Add(',');
            parameterSeparators.AddRange(" \t");
        } 
        #endregion

        #region IDirective Members

        public string Name
        {
            get { return "define"; }
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
            get { return 2; }
        }

        public CanCauseError Apply(string[] parameters, IDirectivePreprocessor host)
        {
            CanCauseError result;
            if (parameters.Length > 1)
            {
                string[] macroParam;
                string mname;
                int startIndex = parameters[0].IndexOf('(');
                int endIndex = parameters[0].LastIndexOf(')');
                if (startIndex != -1 && endIndex != -1 && startIndex < endIndex)
                {
                    string paramString = parameters[0].Substring(
                        startIndex + 1, endIndex - startIndex - 1);
                    macroParam = paramString.Split(macroSeparators, uniters);
                    mname = parameters[0].Substring(0, startIndex);
                }
                else
                {
                    macroParam = new string[0];
                    mname = parameters[0];
                }
                for (int j = 0; j < macroParam.Length; j++)
                {
                    macroParam[j] = macroParam[j].Trim();
                }


                if (mname.Equals(parameters[1]))
                {
                    //Should be a warning.
                    result = CanCauseError.Error("Defining something as itself. ");
                }
                else if (!host.DefCol.IsValidName(mname))
                {
                    result = CanCauseError.Error(mname + " is not valid name to define.");
                }
                else if (host.IsValidToDefine(mname))
                {
                    result = CanCauseError.Error(mname + " cannot be redefined.");
                }
                else
                {
                    if (host.DefCol.ContainsName(mname, macroParam))
                    {
                        //Should be a warning.
                        result = CanCauseError.Error("Redefining " + mname);
                    }
                    else
                    {
                        result = CanCauseError.NoError;
                    }
                    host.DefCol.Add(mname, parameters[1].Trim('"'), macroParam);
                }
            }
            else if (parameters.Length == 1)
            {
                host.DefCol.Add(parameters[0], "");
                result = CanCauseError.NoError;
            }
            else result = CanCauseError.NoError;
            return result;
        }

        #endregion
    }
}
