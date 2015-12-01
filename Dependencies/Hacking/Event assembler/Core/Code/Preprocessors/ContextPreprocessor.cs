using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Nintenlord.Event_Assembler.Collections;
using Nintenlord.Event_Assembler.Utility;
using Nintenlord.Event_Assembler.IO;

namespace Nintenlord.Event_Assembler.Core.Code.Preprocessors
{
    //Under construction
    class ContextPreprocessor
    {
        static Dictionary<string, PreprocessorDirective> directives;

        static ContextPreprocessor()
        {
            directives = new Dictionary<string, PreprocessorDirective>();
            PreprocessorDirective define = new PreprocessorDirective();
            define.minAmountOfParameters = 1;
            define.maxAmountOfParameters = 2;
            define.function = Define;
            define.name = "define";
            directives[define.name] = define;

            PreprocessorDirective include = new PreprocessorDirective();
            include.minAmountOfParameters = 1;
            include.maxAmountOfParameters = 1;
            include.function = Include;
            include.name = "include";
            directives[define.name] = include;

            PreprocessorDirective incbin = new PreprocessorDirective();
            incbin.minAmountOfParameters = 1;
            incbin.maxAmountOfParameters = 1;
            incbin.function = IncludeBinary;
            incbin.name = "incbin";
            directives[define.name] = incbin;
        }

        static private void Define(string[] parameters, ref PreprocessingContext context)
        {
            if (context.CanInclude)
            {
                string name;
                string[] mParam;
                name = GetMacro(parameters[0], out mParam);

                if (!context.CanBeDefined(name, mParam))
                {
                    context.messageLog.AddError(context.currentFile, context.currentLine, 
                        parameters[0] + " is already defined.");
                    return;
                }

                if (parameters.Length == 1)
                {
                    context.defCol.Add(name, "", mParam);
                }
                else
                {
                    context.defCol.Add(name, parameters[1], mParam);
                }
            }
        }
        
        static private void UnDefine(string[] parameters, ref PreprocessingContext context)
        {
            if (context.CanInclude)
            {
                string name;
                string[] mParam;
                name = GetMacro(parameters[0], out mParam);

                if (context.defCol.ContainsName(name, mParam))
                {
                    context.defCol.Remove(name, mParam);
                }
            }
        }

        static private void Include(string[] parameters, ref PreprocessingContext context)
        {
            if (context.CanInclude)
            {

                //Remove comments
            }
        }

        static private void IncludeBinary(string[] parameters, ref PreprocessingContext context)
        {
            if (context.CanInclude)
            {
                //Read binary
            }
        }

        static private void IfDefined(string[] parameters, ref PreprocessingContext context)
        {
            bool predefined = context.predefined.Contains(parameters[0]);
            bool defined = context.defCol.ContainsName(parameters[0]);

            context.include.Push(predefined || defined);
        }

        static private void IfNotDefined(string[] parameters, ref PreprocessingContext context)
        {
            bool predefined = context.predefined.Contains(parameters[0]);
            bool defined = context.defCol.ContainsName(parameters[0]);

            context.include.Push(!predefined && !defined);
        }
 
        static private void Else(string[] parameters, ref PreprocessingContext context)
        {
            bool flag = context.include.Pop();
            context.include.Push(!flag);
        }

        static private void EndIf(string[] parameters, ref PreprocessingContext context)
        {
            context.include.Pop();
        }

        static string GetMacro(string text, out string[] parameters)
        {
            Dictionary<char, char> uniters = new Dictionary<char,char>();
            List<char> macroSeparators = new List<char>();
            uniters['('] = ')';
            uniters['['] = ']';
            macroSeparators.Add(',');

            string mname;
            int startIndex = text.IndexOf('(');
            int endIndex = text.LastIndexOf(')');
            if (startIndex != -1 && endIndex != -1 && startIndex < endIndex)
            {
                string paramString = text.Substring(
                    startIndex + 1, endIndex - startIndex - 1);
                parameters = paramString.Split(macroSeparators, uniters);
                mname = parameters[0].Substring(0, startIndex);
            }
            else
            {
                parameters = new string[0];
                mname = parameters[0];
            }
            for (int j = 0; j < parameters.Length; j++)
            {
                parameters[j] = parameters[j].Trim();
            }
            return mname;
        }
    }

    public delegate void Preprocess(string[] parameters, ref PreprocessingContext context);

    public struct PreprocessorDirective
    {
        public int minAmountOfParameters;
        public int maxAmountOfParameters;
        public Preprocess function;
        public string name;

        public int AmountOfParameters
        {
            set 
            {
                minAmountOfParameters = value;
                maxAmountOfParameters = value;
            }
        }

    }
}
