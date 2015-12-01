using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nintenlord.Event_Assembler.Core.Code.Language.Expression;
using Nintenlord.Event_Assembler.Core.Code.Language.Lexer;
using Nintenlord.Event_Assembler.Core.Code.Preprocessors;
using Nintenlord.Event_Assembler.Core.Code.Templates;
using Nintenlord.Event_Assembler.Core.IO.Input;
using Nintenlord.Event_Assembler.Core.IO.Logs;
using Nintenlord.Event_Assembler.Core.IO.Output;
using Nintenlord.Parser;
using Nintenlord.Utility;
using Nintenlord.Collections;
using EAType = Nintenlord.Event_Assembler.Core.Code.Language.Types.Type;
using Nintenlord.IO;
using Nintenlord.Event_Assembler.Core.Code.Language.Expression.Tree;

namespace Nintenlord.Event_Assembler.Core.Code.Language
{
    sealed class EAExpressionAssembler
    {        
        readonly IParser<Token, IExpression<int>> parser;
        readonly ICodeTemplateStorer storer;


        public EAExpressionAssembler(ICodeTemplateStorer storer, IParser<Token, IExpression<int>> parser)
        {
            this.parser = parser;
            this.storer = storer;
        }

        public void Assemble(IPositionableInputStream input, BinaryWriter output, ILog log)
        {
            this.log = log;
            this.scopeStructure = new Dictionary<Scope<int>, Tuple<Scope<int>, Dictionary<string, int>>>();
            this.codeOffsets = new Dictionary<Code<int>, Tuple<int, ICodeTemplate>>();

            TokenScanner scanner = new TokenScanner(input);

            if (!scanner.MoveNext())
            {
                return;
            }

            Match<Token> match;
            var tree = parser.Parse(scanner, out match);
            if (!match.Success)
            {
                log.AddError(match.Error);// + " " + inputStream.PeekOriginalLine()
                return;
            }
            if (!scanner.IsAtEnd && scanner.Current.Type != TokenType.EndOfStream)
            {
                log.AddError("Didn't reach end, currently at " + scanner.Current);
                return;
            }
            currentOffset = (int)output.BaseStream.Position;
            foreach (var item in FirstPass(tree, null, null))
            {
                codeOffsets.Add(item.Item1, Tuple.Create(item.Item2, item.Item3));
            }
            currentOffset = (int)output.BaseStream.Position;
            SecondPass(output, null, tree);
        }


        ILog log;
        /// <summary>
        /// scope -> (parent scope, scope symbols)
        /// Refactor as a separate class with access modifiers like public, private etc..
        /// </summary>
        Dictionary<Scope<int>, Tuple<Scope<int>, Dictionary<string, int>>> scopeStructure;
        Dictionary<Code<int>, Tuple<int, ICodeTemplate>> codeOffsets;
        int currentOffset;

        private IEnumerable<Tuple<Code<int>, int, ICodeTemplate>>
            FirstPass(IExpression<int> expression, Scope<int> scope, Dictionary<string, int> currentLabels)
        {            
            if (expression.Type == EAExpressionType.Scope)
            {
                var newLabels = new Dictionary<string, int>();
                var newScope = (Scope<int>)expression;
                scopeStructure[newScope] = Tuple.Create(scope, newLabels);
                foreach (var item in expression.GetChildren())
                {
                    foreach (var code in FirstPass(item, newScope, newLabels))
                    {
                        yield return code;
                    }
                }
            }
            else if (expression.Type == EAExpressionType.Code)
            {
                var code = expression as Code<int>;

                if (!HandleBuiltInCodeFirstPass(code, scope))
                {
                    var paramTypes = code.Parameters.Select(GetParamType).ToArray();
                    var templateError = storer.FindTemplate(code.CodeName.Name, paramTypes);
                    if (templateError.CausedError)
                    {
                        AddError(code, templateError);
                    }
                    else
                    {
                        var template = templateError.Result;
                        int oldOffset = currentOffset;
                        currentOffset += template.GetLengthBytes(code.Parameters.ToArray());
                        yield return Tuple.Create(code, oldOffset, template);
                    }                    
                }
            }
            else if (expression.Type == EAExpressionType.Label)
            {
                currentLabels[((LabelDefinition<int>)expression).LabelName] = currentOffset;                
            }
        }
        
        private void SecondPass(BinaryWriter output, Scope<int> scope, IExpression<int> expression)
        {
            if (expression.Type == EAExpressionType.Scope)
            {
                var newScope = (Scope<int>)expression;
                foreach (var item in expression.GetChildren())
                {
                    SecondPass(output, newScope, item);
                }
            }
            else if (expression.Type == EAExpressionType.Code)
            {
                var code = expression as Code<int>;
                if (!HandleBuiltInCodeSecondPass(code, scope))
                {
                    Tuple<int, ICodeTemplate> codeData;
                    if (!codeOffsets.TryGetValue(code, out codeData))
                    {
                        return;//Template wasn't found in first pass
                    }
                    currentOffset = codeData.Item1;

                    if (output.BaseStream.Position != codeData.Item1)
                    {
                        if (!output.BaseStream.CanSeek)
                        {
                            log.AddError(code.Position + ": Stream cannot be seeked.");
                        }
                        else
                        {
                            output.BaseStream.Seek(codeData.Item1, SeekOrigin.Begin);
                        }
                    }

                    var rawData = codeData.Item2.GetData(code.Parameters, x => GetSymbolVal(scope, x));

                    if (rawData.CausedError)
                    {
                        AddError(code, rawData);
                    }
                    else
                    {
                        output.Write(rawData.Result);
                    }
                    currentOffset = (int)output.BaseStream.Position;
                }
            }
        }


        /// <summary>
        /// Turns code to text for printing.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        private string GetText(Code<int> code, Scope<int> scope)
        {
            return code.Parameters.Select(
                x => 
                {
                    if (!x.IsVector)
                    {
                        var result = Folding.Fold(x.Only, y => GetSymbolVal(scope, y));
                        if (result.CausedError)
                        {
                            return x.ToString();                        
                        }
                        else
                        {
                            return result.Result.ToHexString("0x");
                        }
                    }
                    else
                    {
                        return x.ToString(); 
                    }
                }
                ).ToElementWiseString(" ", "", "");
        }        

        private EAType GetParamType(Parameter<int> parameter)
        {
            if (parameter.IsVector)
            {
                return EAType.Vector(parameter.CompCount);
            }
            else
            {
                return EAType.Atom;
            }
        }
        
        private int? GetSymbolVal(Scope<int> scope, string symbolName)
        {
            if (symbolName == currentOffsetCode || offsetChanger == symbolName)
            {
                return currentOffset;
            }
            Tuple<Scope<int>, Dictionary<string, int>> scopeData;
            if (scopeStructure.TryGetValue(scope, out scopeData))
            {
                int value;
                if (scopeData.Item2.TryGetValue(symbolName, out value))
                {
                    return value;
                }
                else if(scopeData.Item1 != null)
                {
                    return GetSymbolVal(scopeData.Item1, symbolName);
                }
                else
                {
                    return null;//Topmost scope reached
                }
            }
            else
            {
                return null;
            }
        }
        
        private void AddError<TError>(IExpression<int> code, CanCauseError<TError> error)
        {
            log.AddError(code.Position + ": " + error.ErrorMessage);
        }

        #region Built-in codes

        private const string currentOffsetCode = "CURRENTOFFSET";
        private const string messagePrinterCode = "MESSAGE";
        private const string errorPrinterCode = "ERROR";
        private const string warningPrinterCode = "WARNING";
        private const string offsetAlinger = "ALIGN";
        private const string offsetChanger = "ORG";

        private bool HandleBuiltInCodeFirstPass(Code<int> code, Scope<int> scope)
        {
            switch (code.CodeName.Name)
            {
                case messagePrinterCode:
                case errorPrinterCode:
                case warningPrinterCode:
                    return true;
                case currentOffsetCode:
                case offsetAlinger:
                    if (code.ParameterCount.IsInRange(1, 1))
                    {
                        var align = Folding.TryFold(code[0].Only);
                        if (align.CausedError)
                        {
                            //log.AddError(align.ErrorMessage);
                        }
                        else
                        {
                            currentOffset = currentOffset.ToMod(align.Result);
                        }
                    }
                    else
                    {
                        //AddNotCorrectParameters(code, 1);
                    }
                    return true;
                case offsetChanger:
                    if (code.ParameterCount.IsInRange(1, 1))
                    {
                        var newOffset = Folding.TryFold(code[0].Only);
                        if (newOffset.CausedError)
                        {
                            //log.AddError(newOffset.ErrorMessage);
                        }
                        else
                        {
                            currentOffset = newOffset.Result;
                        }
                    }
                    else
                    {
                        //AddNotCorrectParameters(code, 1);
                    }
                    return true;
                default:
                    return false;
            }
        }

        private bool HandleBuiltInCodeSecondPass(Code<int> code, Scope<int> scope)
        {
            switch (code.CodeName.Name)
            {
                case messagePrinterCode:
                    string text = GetText(code, scope);
                    log.AddMessage(text);
                    return true;
                case errorPrinterCode:
                    text = GetText(code, scope);
                    log.AddError(text);
                    return true;
                case warningPrinterCode:
                    text = GetText(code, scope);
                    log.AddWarning(text);
                    return true;
                case currentOffsetCode:
                case offsetAlinger:
                    if (code.ParameterCount.IsInRange(1, 1))
                    {
                        var align = Folding.TryFold(code[0].Only);
                        if (align.CausedError)
                        {
                            AddError(code, align);
                        }
                        else
                        {
                            currentOffset = currentOffset.ToMod(align.Result);
                        }
                    }
                    else
                    {
                        AddNotCorrectParameters(code, 1);
                    }
                    return true;
                case offsetChanger:
                    if (code.ParameterCount.IsInRange(1, 1))
                    {
                        var newOffset = Folding.TryFold(code[0].Only);
                        if (newOffset.CausedError)
                        {
                            AddError(code, newOffset);
                        }
                        else
                        {
                            currentOffset = newOffset.Result;
                        }
                    }
                    else
                    {
                        AddNotCorrectParameters(code, 1);
                    }
                    return true;
                default:
                    return false;
            }
        }

        private void AddNotCorrectParameters(Code<int> code, int paramCount)
        {
            log.AddError("{4}: Code {0} doesn't have {2} parameters, but has {1} parameters",
                code.CodeName,
                paramCount,
                code.Parameters.Length,
                code.Position);
        }

        private void AddNotCorrectParameters(Code<int> code, int paramMin, int paramMax)
        {
            log.AddError("{4}: Code {0} doesn't have {3} parameters, but has {1}-{2} parameters",
                code.CodeName,
                paramMin,
                paramMax,
                code.Parameters.Length,
                code.Position);
        }
        
        #endregion
    }
}
