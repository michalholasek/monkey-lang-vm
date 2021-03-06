using System;
using System.Collections.Generic;
using System.Linq;

using static Monkey.Shared.Parser;

namespace Monkey.Shared
{
    public partial class Parser
    {
        internal static StatementParseResult ParseStatement(StatementParseResult previousState)
        {
            return Factory.StatementParseResultBuilder()
                .Assign(previousState)
                .Errors(new List<AssertionError>())
                .Create()
                .DetermineKind()
                .DetermineTokenRange()
                .AssertSyntax()
                .ParseExpression()
                .Result();
        }

        internal static BlockStatementParseResult ParseBlockStatement(ExpressionParseResult previousState)
        {
            var statements = new List<Statement>();
            
            var errors = Assert.BlockLeftBrace(previousState.Tokens, previousState.Position - Include.Brace);

            if (errors.Count > 0)
            {
                return new BlockStatementParseResult { Errors = errors };
            }

            var newState = Factory.StatementParseResult()
                    .Errors(new List<AssertionError>())
                    .Position(previousState.Position)
                    .Token(previousState.Tokens.Skip(previousState.Position).Take(1).FirstOrDefault())
                    .Tokens(previousState.Tokens)
                    .Create();
            
            while (newState.Token != default(Token) && newState.Token.Kind != SyntaxKind.RightBrace)
            {
                if (newState.Token.Kind != SyntaxKind.Semicolon)
                {
                    newState = ParseStatement(newState);

                    if (newState.Errors.Count > 0)
                    {
                        return new BlockStatementParseResult { Errors = newState.Errors };
                    }

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

            errors = Assert.BlockRightBrace(previousState.Tokens, previousState.Position - Include.Brace);

            if (errors.Count > 0)
            {
                return new BlockStatementParseResult { Errors = errors };
            }

            var semicolon = newState.Tokens
                    .Skip(newState.Position + Skip.Brace)
                    .Take(1)
                    .Where(token => token.Kind == SyntaxKind.Semicolon)
                    .FirstOrDefault();

            return new BlockStatementParseResult
            {
                Errors = newState.Errors,
                Position = newState.Position + Skip.Brace + (semicolon != default(Token) ? Skip.Semicolon : 0),
                Statements = statements
            };
        }
    }

    internal class StatementParseResultBuilder
    {
        private StatementParseResultBuilderState internalState;

        public StatementParseResultBuilder(StatementParseResult currentState)
        {
            internalState = new StatementParseResultBuilderState
            {
                Errors = currentState.Errors,
                Position = currentState.Position,
                Tokens = currentState.Tokens
            };
        }

        public StatementParseResultBuilder AssertSyntax()
        {
            internalState.Errors.AddRange(Assert.Syntax(internalState));
            return this;
        }
        
        public StatementParseResultBuilder DetermineKind()
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

        public StatementParseResultBuilder DetermineTokenRange()
        {
            internalState.Range = DetermineTokenRangeInternal(internalState);
            return this;
        }

        public StatementParseResultBuilder ParseExpression()
        {
            if (internalState.Errors.Count > 0)
            {
                return this;
            }

            var expressionParseResult = Factory.ExpressionParseResultBuilder()
                    .Errors(internalState.Errors)
                    .Position(internalState.Position)
                    .Range(internalState.Range)
                    .StatementKind(internalState.Kind)
                    .Tokens(internalState.Tokens)
                    .Create()
                    .DetermineTokenRange()
                    .Result();

            internalState.Expression = expressionParseResult.Expression;
            internalState.Errors.AddRange(expressionParseResult.Errors);

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
        
        private int DetermineTokenRangeInternal(StatementParseResultBuilderState state)
        {
            var noOfLeftBraces = 0;
            var noOfRightBraces = 0;
            var position = state.Position;
            var ignoreSemicolon = false;

            while (position < state.Tokens.Count)
            {
                var currentToken = state.Tokens.Skip(position).Take(1).FirstOrDefault();
                
                // Enable parsing of simple expressions, eg. '1 + 1' without semicolon
                if (currentToken.Kind == SyntaxKind.EOF)
                {
                    return position;
                }
                // Top-level semicolons, eg. let a = 42;<< or if (true) { 1; };<<
                else if (currentToken.Kind == SyntaxKind.Semicolon && !ignoreSemicolon && noOfLeftBraces == noOfRightBraces)
                {
                    return position + Include.Semicolon - state.Position;
                }
                // Brace after consequence body in If-Else expression, eg. if (true) { 1 }<< else...
                if (currentToken.Kind == SyntaxKind.RightBrace && IsIfElseExpression(state, position))
                {
                    noOfRightBraces++;
                    position++;
                }
                // Inside the block, ie. { ...<< }
                else if (currentToken.Kind == SyntaxKind.RightBrace && noOfLeftBraces != noOfRightBraces)
                {
                    ignoreSemicolon = false;
                    noOfRightBraces++;
                    position++;
                }
                // Block opening, eg. {<< ... }. We ignore semicolons inside blocks
                else if (currentToken.Kind == SyntaxKind.LeftBrace)
                {
                    ignoreSemicolon = true;
                    noOfLeftBraces++;
                    position++;
                }
                // Everything else
                else
                {
                    position++;
                }
            }

            return -1;
        }

        private static bool IsIfElseExpression(StatementParseResultBuilderState currentState, int position)
        {
            var nextToken = currentState.Tokens.Skip(position + Skip.Brace).Take(1).FirstOrDefault();

            if (nextToken != null && nextToken.Kind == SyntaxKind.Else)
            {
                return true;
            }

            return false;
        }
    }
}
