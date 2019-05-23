using System;
using System.Collections.Generic;
using System.Linq;

namespace Monkey.Shared
{
    public partial class Parser
    {
        internal static class Assert
        {
            internal static List<AssertionError> ExpressionOperand(ExpressionParseResult currentState, Token token)
            {
                var errors = new List<AssertionError>();

                var info = new ErrorInfo
                {
                    Code = ErrorCode.InvalidToken,
                    Kind = ErrorKind.InvalidToken,
                    Position = currentState.Position,
                    Source = ErrorSource.Parser,
                    Tokens = currentState.Tokens
                };

                if (IsMissingExpressionToken(token))
                {
                    info.Code = ErrorCode.MissingExpressionToken;
                    info.Kind = ErrorKind.MissingToken;
                    errors.Add(Error.Create(info));
                    return errors;
                }

                if (!IsExpressionToken(token))
                {
                    info.Offenders = new List<object> { token.Literal };
                    errors.Add(Error.Create(info));
                    return errors;
                }

                return errors;
            }
            
            internal static List<AssertionError> PrefixExpressionOperator(ExpressionParseResult currentState, Token op)
            {
                var errors = new List<AssertionError>();

                var info = new ErrorInfo
                {
                    Kind = ErrorKind.UnknownOperator,
                    Position = currentState.Position,
                    Source = ErrorSource.Parser,
                    Tokens = currentState.Tokens
                };

                switch (op.Kind)
                {
                    case SyntaxKind.Bang:
                    case SyntaxKind.Minus:
                        return errors;
                    default:
                        errors.Add(Error.Create(info));
                        return errors;
                }
            }

            internal static List<AssertionError> Syntax(StatementParseResultBuilderState currentState)
            {
                switch (currentState.Kind)
                {
                    case NodeKind.Let:
                        return AssertLetStatementSyntax(currentState);
                    case NodeKind.Return:
                        return AssertReturnStatementSyntax(currentState);
                    default:
                        return new List<AssertionError>();
                }
            }

            private static List<AssertionError> AssertLetStatementSyntax(StatementParseResultBuilderState currentState)
            {
                var identifierToken = currentState.Tokens
                        .Skip(currentState.Position + Skip.Let)
                        .Take(1)
                        .FirstOrDefault();

                var assignToken = currentState.Tokens
                        .Skip(currentState.Position + Skip.Let + Skip.Identifier)
                        .Take(1)
                        .FirstOrDefault();

                var expressionToken = currentState.Tokens
                        .Skip(currentState.Position + Skip.Let + Skip.Identifier + Skip.Assign)
                        .Take(1)
                        .FirstOrDefault();

                var errors = new List<AssertionError>();

                var info = new ErrorInfo
                {
                    Kind = ErrorKind.UnexpectedToken,
                    Position = currentState.Position + Skip.Let,
                    Source = ErrorSource.Parser,
                    Tokens = currentState.Tokens
                };

                if (identifierToken == default(Token) || identifierToken.Kind == SyntaxKind.EOF)
                {
                    info.Code = ErrorCode.MissingLetIdentifierToken;
                    info.Kind = ErrorKind.MissingToken;
                    errors.Add(Error.Create(info));
                    return errors;
                } 

                if (assignToken == default(Token) || assignToken.Kind == SyntaxKind.EOF)
                {
                    info.Code = ErrorCode.MissingLetAssignToken;
                    info.Kind = ErrorKind.MissingToken;
                    info.Position = currentState.Position + Skip.Let + Skip.Identifier;
                    errors.Add(Error.Create(info));
                    return errors;
                }              

                if (identifierToken.Kind != SyntaxKind.Identifier)
                {
                    info.Code = ErrorCode.InvalidLetIdentifierToken;
                    info.Kind = ErrorKind.InvalidToken;
                    errors.Add(Error.Create(info));
                }

                if (assignToken.Kind != SyntaxKind.Assign)
                {
                    info.Code = ErrorCode.InvalidLetAssignToken;
                    info.Kind = ErrorKind.InvalidToken;
                    info.Position = info.Position + Skip.Identifier;
                    errors.Add(Error.Create(info));
                }

                if (expressionToken == default(Token) || expressionToken.Kind == SyntaxKind.EOF)
                {
                    info.Code = ErrorCode.MissingExpressionToken;
                    info.Kind = ErrorKind.MissingToken;
                    info.Position = currentState.Position + Skip.Let + Skip.Identifier + Skip.Assign;
                    errors.Add(Error.Create(info));
                    return errors;
                }

                if (!IsExpressionToken(expressionToken))
                {
                    info.Code = ErrorCode.InvalidLetExpression;
                    info.Kind = ErrorKind.InvalidToken;
                    info.Position = currentState.Position + Skip.Let + Skip.Identifier + Skip.Assign;
                    errors.Add(Error.Create(info));
                }

                return errors;
            }

            private static List<AssertionError> AssertReturnStatementSyntax(StatementParseResultBuilderState currentState)
            {
                var nextToken = currentState.Tokens.Skip(currentState.Position + Skip.Return).Take(1).FirstOrDefault();
                var errors = new List<AssertionError>();

                if (nextToken == default(Token) || nextToken.Kind == SyntaxKind.Semicolon || nextToken.Kind == SyntaxKind.EOF)
                {
                    return errors;
                }

                var info = new ErrorInfo
                {
                    Kind = ErrorKind.InvalidToken,
                    Position = currentState.Position + Skip.Return,
                    Source = ErrorSource.Parser,
                    Tokens = currentState.Tokens
                };

                if (!IsExpressionToken(nextToken))
                {
                    info.Code = ErrorCode.InvalidReturnExpression;
                    info.Kind = ErrorKind.InvalidToken;
                    errors.Add(Error.Create(info));
                }

                return errors;
            }

            private static bool IsExpressionToken(Token token)
            {
                switch (token.Kind)
                {
                    case SyntaxKind.Int:
                    case SyntaxKind.True:
                    case SyntaxKind.False:
                    case SyntaxKind.String:
                    case SyntaxKind.Identifier:
                    case SyntaxKind.LeftParenthesis:
                    case SyntaxKind.If:
                    case SyntaxKind.Function:
                    case SyntaxKind.LeftBracket:
                    case SyntaxKind.LeftBrace:
                    case SyntaxKind.Bang:
                    case SyntaxKind.Minus:
                        return true;
                    default:
                        return false;
                }
            }
        }

        private static bool IsMissingExpressionToken(Token token)
        {
            if (token == default(Token)) return true;

            switch (token.Kind)
            {
                case SyntaxKind.EOF:
                case SyntaxKind.RightBracket:
                case SyntaxKind.RightBrace:
                case SyntaxKind.Colon:
                case SyntaxKind.Comma:
                    return true;
                default:
                    return false;
            }
        }
    }
}
