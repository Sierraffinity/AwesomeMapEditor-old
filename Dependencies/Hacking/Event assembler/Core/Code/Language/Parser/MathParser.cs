// -----------------------------------------------------------------------
// <copyright file="MathParser.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Event_Assembler.Core.Code.Language.Parser
{
    using System;
    using Nintenlord.Event_Assembler.Core.Code.Language.Expression;
    using Nintenlord.Event_Assembler.Core.Code.Language.Expression.Tree;
    using Nintenlord.Event_Assembler.Core.Code.Language.Lexer;
    using Nintenlord.IO.Scanners;
    using Nintenlord.Parser;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    sealed class MathParser<T> : Parser<Token, IExpression<T>>
    {
        readonly private Func<string, T> evaluate;

        public MathParser(Func<string, T> evaluate)
        {
            this.evaluate = evaluate;                
        }

        protected override IExpression<T> ParseMain(IScanner<Token> scanner, out Match<Token> match)
        {
            return ParseB(scanner, out match);
        }


        private IExpression<T> ParseB(IScanner<Token> scanner, out Match<Token> match)
        {
            IExpression<T> temp = ParseS(scanner, out match);
            if (match.Success)
            {
                Match<Token> secondMatch;
                var result = ParseBopt(scanner, temp, out secondMatch);
                match += secondMatch;
                return result;
            }
            else return null;
        }

        private IExpression<T> ParseBopt(IScanner<Token> scanner, IExpression<T> expression, out Match<Token> match)
        {
            Match<Token> tempMatch;
            var token = scanner.Current;
            switch (token.Type)
            {
                case TokenType.MathOperator:
                    if (token.Value == "^")
                    {
                        match = new Match<Token>(scanner, 1);
                        scanner.MoveNext();
                        var second = ParseS(scanner, out tempMatch);
                        match += tempMatch;
                        if (match.Success)
                        {
                            expression = new BitwiseXor<T>(expression, second, token.Position);
                            var opt = ParseBopt(scanner, expression, out tempMatch);
                            match += tempMatch;
                            return opt;
                        }
                    }
                    else if (token.Value == "|")
                    {
                        match = new Match<Token>(scanner, 1);
                        scanner.MoveNext();
                        var second = ParseS(scanner, out tempMatch);
                        match += tempMatch;
                        if (match.Success)
                        {
                            expression = new BitwiseOr<T>(expression, second, token.Position);
                            var opt = ParseBopt(scanner, expression, out tempMatch);
                            match += tempMatch;
                            return opt;
                        }
                    }
                    else if (token.Value == "&")
                    {
                        match = new Match<Token>(scanner, 1);
                        scanner.MoveNext();
                        var second = ParseS(scanner, out tempMatch);
                        match += tempMatch;
                        if (match.Success)
                        {
                            expression = new BitwiseAnd<T>(expression, second, token.Position);
                            var opt = ParseBopt(scanner, expression, out tempMatch);
                            match += tempMatch;
                            return opt;
                        }
                    }
                    else
                    {
                        match = new Match<Token>(scanner, 0);
                    }
                    break;
                default:
                    match = new Match<Token>(scanner, 0);
                    break;
            }
            return expression;
        }
        

        private IExpression<T> ParseS(IScanner<Token> scanner, out Match<Token> match)
        {
            IExpression<T> temp = ParseE(scanner, out match);
            if (match.Success)
            {
                Match<Token> secondMatch;
                var result = ParseSopt(scanner, temp, out secondMatch);
                match += secondMatch;
                return result;
            }
            else return null;
        }
        
        private IExpression<T> ParseSopt(IScanner<Token> scanner, IExpression<T> expression, out Match<Token> match)
        {
            Match<Token> tempMatch;
            var token = scanner.Current;
            switch (token.Type)
            {
                case TokenType.MathOperator:
                    if (token.Value == "<<")
                    {
                        match = new Match<Token>(scanner, 1);
                        scanner.MoveNext();
                        var second = ParseE(scanner, out tempMatch);
                        match += tempMatch;
                        if (match.Success)
                        {
                            expression = new BitShiftLeft<T>(expression, second, token.Position);
                            var opt = ParseSopt(scanner, expression, out tempMatch);
                            match += tempMatch;
                            return opt;
                        }
                    }
                    else if (token.Value == ">>")
                    {
                        match = new Match<Token>(scanner, 1);
                        scanner.MoveNext();
                        var second = ParseE(scanner, out tempMatch);
                        match += tempMatch;
                        if (match.Success)
                        {
                            expression = new BitShiftRight<T>(expression, second, token.Position);
                            var opt = ParseSopt(scanner, expression, out tempMatch);
                            match += tempMatch;
                            return opt;
                        }
                    }
                    else
                    {
                        match = new Match<Token>(scanner, 0);
                    }
                    break;
                default:
                    match = new Match<Token>(scanner, 0);
                    break;
            }
            return expression;
        }
        

        private IExpression<T> ParseE(IScanner<Token> scanner, out Match<Token> match)
        {
            IExpression<T> temp = ParseT(scanner, out match);
            if (match.Success)
            {
                Match<Token> secondMatch;
                var result = ParseEopt(scanner, temp, out secondMatch);
                match += secondMatch;
                return result;
            }
            else return null;
        }

        private IExpression<T> ParseEopt(IScanner<Token> scanner, IExpression<T> expression, out Match<Token> match)
        {
            Match<Token> tempMatch;
            var token = scanner.Current;
            switch (token.Type)
            {
                case TokenType.MathOperator:
                    if (token.Value == "+")
                    {
                        match = new Match<Token>(scanner, 1);
                        scanner.MoveNext();
                        var second = ParseT(scanner, out tempMatch);
                        match += tempMatch;
                        if (match.Success)
                        {
                            expression = new Sum<T>(expression, second, token.Position);
                            var opt = ParseEopt(scanner, expression, out tempMatch);
                            match += tempMatch;
                            return opt;
                        }
                    }
                    else if (token.Value == "-")
                    {
                        match = new Match<Token>(scanner, 1);
                        scanner.MoveNext();
                        var second = ParseT(scanner, out tempMatch);
                        match += tempMatch;
                        if (match.Success)
                        {
                            expression = new Minus<T>(expression, second, token.Position);
                            var opt = ParseEopt(scanner, expression, out tempMatch);
                            match += tempMatch;
                            return opt;
                        }
                    }
                    else
                    {
                        match = new Match<Token>(scanner, 0);
                    }
                    break;
                default:
                    match = new Match<Token>(scanner, 0);
                    break;
            }
            return expression;
        }


        private IExpression<T> ParseT(IScanner<Token> scanner, out Match<Token> match)
        {
            IExpression<T> temp = ParseF(scanner, out match);
            if (match.Success)
            {
                Match<Token> tempMatch;
                var result = ParseTopt(scanner, temp, out tempMatch);
                match += tempMatch;
                return result;
            }
            else return null;
        }

        private IExpression<T> ParseTopt(IScanner<Token> scanner, IExpression<T> expression, out Match<Token> match)
        {
            Match<Token> tempMatch;
            var token = scanner.Current;
            switch (token.Type)
            {
                case TokenType.MathOperator:
                    if (token.Value == "*")
                    {
                        match = new Match<Token>(scanner, 1);
                        scanner.MoveNext();
                        var second = ParseF(scanner, out tempMatch);
                        match += tempMatch;
                        if (match.Success)
                        {
                            expression = new Multiply<T>(expression, second, token.Position);
                            var opt = ParseTopt(scanner, expression, out tempMatch);
                            match += tempMatch;
                            return opt;
                        }
                        else return null;
                    }
                    else if (token.Value == "/")
                    {
                        match = new Match<Token>(scanner, 1);
                        scanner.MoveNext();
                        var second = ParseF(scanner, out tempMatch);
                        match += tempMatch;
                        if (match.Success)
                        {
                            expression = new Division<T>(expression, second, token.Position);
                            var opt = ParseTopt(scanner, expression, out tempMatch);
                            match += tempMatch;
                            return opt;
                        }
                        else return null;
                    }
                    else if (token.Value == "%")
                    {
                        match = new Match<Token>(scanner, 1);
                        scanner.MoveNext();
                        var second = ParseF(scanner, out tempMatch);
                        match += tempMatch;
                        if (match.Success)
                        {
                            expression = new Multiply<T>(expression, second, token.Position);
                            var opt = ParseTopt(scanner, expression, out tempMatch);
                            match += tempMatch;
                            return opt;
                        }
                        else return null;
                    }
                    else
                    {
                        match = new Match<Token>(scanner, 0);
                    }
                    break;
                default:
                    match = new Match<Token>(scanner, 0);
                    break;
            }
            return expression;
        }


        private IExpression<T> ParseF(IScanner<Token> scanner, out Match<Token> match)
        {
            bool negative = false;
            match = new Match<Token>(scanner, 0);
            var token = scanner.Current;
            switch (token.Type)
            {
                case TokenType.MathOperator:
                    if (token.Value == "-")
                    {
                        scanner.MoveNext(); match++;
                        token = scanner.Current;
                        negative = true;
                        goto case TokenType.IntegerLiteral;
                    }
                    else if (token.Value == "+")
                    {
                        scanner.MoveNext(); match++;
                        token = scanner.Current;
                        negative = false;
                        goto case TokenType.IntegerLiteral;
                    }
                    else
                    {
                        goto default;
                    }
                case TokenType.Symbol:
                    scanner.MoveNext();match++;
                    return new Symbol<T>(token.Value, token.Position);
                    
                case TokenType.IntegerLiteral:
                    scanner.MoveNext();match++;
                    if (negative)
                    {
                        return new ValueExpression<T>(evaluate("-" + token.Value), token.Position);
                    }
                    else
                    {
                        return new ValueExpression<T>(evaluate(token.Value), token.Position);
                    }
                    
                case TokenType.LeftParenthesis:
                    scanner.MoveNext(); match++;

                    Match<Token> tempMatch;
                    var res = ParseE(scanner, out tempMatch);
                    match += tempMatch;
                    if (match.Success)
                    {
                        if (scanner.Current.Type == TokenType.RightParenthesis)
                        {
                            scanner.MoveNext();
                            match++;
                            return res;
                        }
                        else
                        {
                            match = new Match<Token>(scanner, "Unclosed parenthesis");
                        }
                    }
                    return null;

                default:
                    match = new Match<Token>(scanner, "Operator instead of math value");
                    return null;
            }
        }
    }
}
