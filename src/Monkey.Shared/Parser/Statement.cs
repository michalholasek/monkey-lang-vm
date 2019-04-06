using System;
using System.Collections.Generic;
using System.Linq;

using Monkey.Shared.Parser.Ast;
using Monkey.Shared.Scanner;

namespace Monkey.Shared.Parser
{
    internal partial class Parser
    {
        public static StatementParseResult ParseStatement(StatementParseResult previousState)
        {
            return Factory.StatementBuilder()
                .Assign(previousState)
                .Create()
                .DetermineKind()
                .DetermineTokenRange()
                .AssertSyntax()
                .ParseExpression()
                .Result();
        }

        public static BlockStatementParseResult ParseBlockStatement(ExpressionParseResult previousState)
        {
            var statements = new List<Statement>();

            var newState = Factory.StatementParseResult()
                    .Errors(new List<AssertionError>())
                    .Position(previousState.Position)
                    .Token(previousState.Tokens.Skip(previousState.Position).Take(1).FirstOrDefault())
                    .Tokens(previousState.Tokens)
                    .Create();
            
            while (newState.Token != null && newState.Token.Kind != SyntaxKind.RightBrace)
            {
                if (newState.Token.Kind != SyntaxKind.Semicolon)
                {
                    newState = ParseStatement(newState);
                    statements.Add(newState.Statement);

                    newState = Factory.StatementParseResult()
                        .Assign(newState)
                        .Token(newState.Tokens.Skip(newState.Position).Take(1).FirstOrDefault())
                        .Create();
                }
                else
                {
                    newState = Factory.StatementParseResult()
                        .Assign(newState)
                        .Position(newState.Position + Skip.Semicolon)
                        .Token(newState.Tokens.Skip(newState.Position + Skip.Semicolon).Take(1).FirstOrDefault())
                        .Create();
                }
            }

            return new BlockStatementParseResult
            {
                Position = newState.Position + Skip.Brace,
                Statements = statements
            };
        }
    }

    internal class StatementBuilder
    {
        private StatementBuilderState internalState;

        public StatementBuilder(StatementParseResult currentState)
        {
            internalState = new StatementBuilderState
            {
                Errors = currentState.Errors,
                Position = currentState.Position,
                Tokens = currentState.Tokens
            };
        }

        public StatementBuilder AssertSyntax()
        {
            internalState.Errors.AddRange(Assert.Syntax(internalState));
            return this;
        }
        
        public StatementBuilder DetermineKind()
        {
            var token = internalState.Tokens[internalState.Position];

            switch (token.Kind)
            {
                case SyntaxKind.Let:
                    internalState.Kind = NodeKind.Let;
                    break;
                case SyntaxKind.Return:
                    internalState.Kind = NodeKind.Return;
                    break;
                default:
                    internalState.Kind = NodeKind.Expression;
                    break;
            }
            
            return this;
        }

        public StatementBuilder DetermineTokenRange()
        {
            internalState.Range = DetermineTokenRangeInternal(internalState, internalState.Position, ignoreSemicolon: false);
            return this;
        }

        public StatementBuilder ParseExpression()
        {
            if (internalState.Errors.Count > 0)
            {
                return this;
            }

            internalState.Expression = Factory.ExpressionBuilder()
                .Position(internalState.Position)
                .Range(internalState.Range)
                .Tokens(internalState.Tokens)
                .Create()
                .DetermineTokenRange()
                .Result()
                .Expression;

            return this;
        }
        
        public StatementParseResult Result()
        {
            var identifier = internalState.Tokens
                    .Skip(internalState.Position)
                    .Take(internalState.Range)
                    .Where(token => internalState.Kind == NodeKind.Let && token.Kind == SyntaxKind.Identifier)
                    .FirstOrDefault();

            var statement = Factory.Statement()
                    .Expression(internalState.Expression)
                    .Identifier(identifier)
                    .Kind(internalState.Kind)
                    .Position(internalState.Position)
                    .Range(internalState.Range)
                    .Create();

            return Factory.StatementParseResult()
                .Errors(internalState.Errors)
                .Position(internalState.Position + internalState.Range)
                .Statement(statement)
                .Tokens(internalState.Tokens)
                .Create();
        }
        
        private int DetermineTokenRangeInternal(StatementBuilderState internalState, int position, bool ignoreSemicolon)
        {
            var internalPosition = position;
            Token currentToken;
            var range = 0;

            Func<StatementBuilderState, bool> HasAlternative = (state) =>
            {
                var nextToken = state.Tokens.Skip(internalPosition + Skip.Brace).Take(1).FirstOrDefault();

                if (nextToken != null && nextToken.Kind == SyntaxKind.Else)
                {
                    return true;
                }

                return false;
            };

            while (internalPosition < internalState.Tokens.Count)
            {
                currentToken = internalState.Tokens.Skip(internalPosition).Take(1).FirstOrDefault();

                if (currentToken.Kind == SyntaxKind.RightBrace && HasAlternative(internalState))
                {
                    internalPosition++;
                }
                else if (currentToken.Kind == SyntaxKind.RightBrace || (!ignoreSemicolon && currentToken.Kind == SyntaxKind.Semicolon))
                {
                    internalPosition++;
                    range = internalPosition - internalState.Position;
                    break;
                }
                else if (currentToken.Kind == SyntaxKind.LeftBrace)
                {
                    internalPosition += DetermineTokenRangeInternal(internalState, internalPosition + Skip.Brace, ignoreSemicolon: true);
                }
                else
                {
                    internalPosition++;
                }
            }

            // Enable parsing of simple expressions, eg. '1 + 1' without semicolon
            if (range == 0)
            {
                range = internalState.Tokens.Count - 1;
            }

            return range;
        }
    }
}
