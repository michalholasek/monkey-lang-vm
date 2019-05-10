using System;
using System.Collections.Generic;
using System.Linq;

namespace Monkey.Shared
{
    public partial class Parser
    {
        internal static class Assert
        {
            internal static List<AssertionError> PrefixExpressionOperator(ExpressionParseResult currentState, Token op)
            {
                var errors = new List<AssertionError>();

                var info = new ErrorInfo
                {
                    Actual = op.Kind,
                    Expected = new List<SyntaxKind> { SyntaxKind.Bang, SyntaxKind.Minus },
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
                var identifierToken = currentState.Tokens[currentState.Position + Skip.Let];
                var assignToken = currentState.Tokens[currentState.Position + Skip.Let + Skip.Identifier];
                var errors = new List<AssertionError>();

                var info = new ErrorInfo
                {
                    Kind = ErrorKind.UnexpectedToken,
                    Position = currentState.Position + Skip.Let,
                    Source = ErrorSource.Parser,
                    Tokens = currentState.Tokens
                };

                if (identifierToken == null || assignToken == null)
                {
                    var eofToken = currentState.Tokens[currentState.Tokens.Count - 1];
                    info.Actual = eofToken.Kind;
                    errors.Add(Error.Create(info));
                    return errors;
                }

                if (identifierToken.Kind != SyntaxKind.Identifier)
                {
                    info.Actual = identifierToken.Kind;
                    errors.Add(Error.Create(info));
                }

                if (assignToken.Kind != SyntaxKind.Assign)
                {
                    info.Actual = assignToken.Kind;
                    info.Position = info.Position + Skip.Identifier;
                    errors.Add(Error.Create(info));
                }

                return errors;
            }

            private static List<AssertionError> AssertReturnStatementSyntax(StatementParseResultBuilderState currentState)
            {
                var nextToken = currentState.Tokens.Skip(currentState.Position + Skip.Return).Take(1).FirstOrDefault();
                var errors = new List<AssertionError>();

                var info = new ErrorInfo
                {
                    Kind = ErrorKind.UnexpectedToken,
                    Position = currentState.Position + Skip.Return,
                    Source = ErrorSource.Parser,
                    Tokens = currentState.Tokens
                };

                if (nextToken == null)
                {
                    var eofToken = currentState.Tokens[currentState.Tokens.Count - 1];
                    errors.Add(Error.Create(info));
                    return errors;
                }

                switch (nextToken.Kind)
                {
                    case SyntaxKind.Int:
                    case SyntaxKind.True:
                    case SyntaxKind.False:
                    case SyntaxKind.String:
                    case SyntaxKind.Identifier:
                    case SyntaxKind.Function:
                    case SyntaxKind.LeftBrace:
                    case SyntaxKind.LeftParenthesis:
                    case SyntaxKind.Semicolon:
                        return errors;
                    default:
                        info.Actual = nextToken.Kind;
                        errors.Add(Error.Create(info));
                        return errors;
                }
            }
        }
    }
}
