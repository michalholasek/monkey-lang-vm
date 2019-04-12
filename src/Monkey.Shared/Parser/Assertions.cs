using System;
using System.Collections.Generic;

using Monkey.Shared;
using Monkey.Shared.Parser.Ast;
using Monkey.Shared.Scanner;

namespace Monkey.Shared.Parser
{
    internal static class Assert
    {
        internal static List<AssertionError> Syntax(StatementBuilderState currentState)
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

        private static List<AssertionError> AssertLetStatementSyntax(StatementBuilderState currentState)
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

        private static List<AssertionError> AssertReturnStatementSyntax(StatementBuilderState currentState)
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
