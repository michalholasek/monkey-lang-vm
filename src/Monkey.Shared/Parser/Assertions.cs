using System;
using System.Collections.Generic;

namespace Monkey.Shared
{
    public partial class Parser
    {
        internal static class Assert
        {
            internal static List<AssertionError> PrefixExpressionOperator(Token op)
            {
                var errors = new List<AssertionError>();

                switch (op.Kind)
                {
                    case SyntaxKind.Bang:
                    case SyntaxKind.Minus:
                        return errors;
                    default:
                        errors.Add(Error.CreateParsingError(AssertionErrorKind.UnknownOperator, op, "expected Bang (!) or Minus (-)"));
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

                if (identifierToken == null || assignToken == null)
                {
                    var eofToken = currentState.Tokens[currentState.Tokens.Count - 1];
                    errors.Add(Error.CreateAssertionError(AssertionErrorKind.UnexpectedToken, eofToken, SyntaxKind.EOF));
                    return errors;
                }

                if (identifierToken.Kind != SyntaxKind.Identifier) {
                    errors.Add(Error.CreateAssertionError(AssertionErrorKind.InvalidToken, identifierToken, SyntaxKind.Identifier));
                }

                if (assignToken.Kind != SyntaxKind.Assign) {
                    errors.Add(Error.CreateAssertionError(AssertionErrorKind.InvalidToken, assignToken, SyntaxKind.Assign));
                }

                return errors;
            }

            private static List<AssertionError> AssertReturnStatementSyntax(StatementParseResultBuilderState currentState)
            {
                var nextToken = currentState.Tokens[currentState.Position + Skip.Return];
                var errors = new List<AssertionError>();

                if (nextToken == null)
                {
                    var eofToken = currentState.Tokens[currentState.Tokens.Count - 1];
                    errors.Add(Error.CreateAssertionError(AssertionErrorKind.UnexpectedToken, eofToken));
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
                        errors.Add(Error.CreateAssertionError(AssertionErrorKind.UnexpectedToken, nextToken));
                        return errors;
                }
            }
        }
    }
}
