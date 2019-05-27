using System;
using System.Collections.Generic;
using System.Linq;

using static Monkey.Shared.Parser;

namespace Monkey.Shared
{
    public partial class Parser
    {
        private static Dictionary<SyntaxKind, Func<ExpressionParseResult, ExpressionParseResult>> ParsingFunctions;

        static Parser()
        {
            ParsingFunctions = new Dictionary<SyntaxKind, Func<ExpressionParseResult, ExpressionParseResult>>
            {
                { SyntaxKind.Int, ParseValueExpression },
                { SyntaxKind.True, ParseValueExpression },
                { SyntaxKind.False, ParseValueExpression },
                { SyntaxKind.String, ParseValueExpression },
                { SyntaxKind.Identifier, ParseValueExpression },
                { SyntaxKind.LeftParenthesis, ParseGroupedExpression },
                { SyntaxKind.If, ParseIfElseExpression },
                { SyntaxKind.Function, ParseFunctionExpression },
                { SyntaxKind.LeftBracket, ParseArrayExpression },
                { SyntaxKind.LeftBrace, ParseHashExpression }
            };
        }

        internal static ExpressionParseResult ParseExpression(ExpressionParseResult currentState)
        {
            var newState = Factory.ExpressionParseResult()
                    .Assign(ParsePrefixExpression(currentState))
                    .Create();

            if (newState.Errors.Count > 0)
            {
                return newState;
            }

            while (newState.Position < currentState.Tokens.Count && currentState.Precedence < newState.Precedence)
            {
                newState = Factory.ExpressionParseResult()
                    .Assign(ParseInfixExpression(newState))
                    .Create();

                if (newState.Errors.Count > 0)
                {
                    return newState;
                }
            }

            return newState;
        }

        private static Expression CreateExpression(Token token)
        {
            switch (token.Kind)
            {
                case SyntaxKind.Int:
                    return new IntegerExpression(int.Parse(token.Literal));
                case SyntaxKind.True:
                case SyntaxKind.False:
                    return new BooleanExpression(token.Kind == SyntaxKind.True ? true : false);
                case SyntaxKind.String:
                    return new StringExpression(token.Literal);
                default:
                    return new IdentifierExpression(token.Literal);
            }
        }

        private static Precedence DetermineOperatorPrecedence(Token op)
        {
            if (op == null) return Precedence.Lowest;

            switch (op.Kind)
            {
                case SyntaxKind.Equal:
                case SyntaxKind.NotEqual:
                    return Precedence.Equals;
                case SyntaxKind.LessThan:
                case SyntaxKind.GreaterThan:
                    return Precedence.LessGreater;
                case SyntaxKind.Plus:
                case SyntaxKind.Minus:
                    return Precedence.Sum;
                case SyntaxKind.Asterisk:
                case SyntaxKind.Slash:
                    return Precedence.Product;
                case SyntaxKind.LeftParenthesis:
                    return Precedence.Call;
                case SyntaxKind.LeftBracket:
                    return Precedence.Index;
                default:
                    return Precedence.Lowest;
            }
        }

        private static ExpressionParseResult ExpandPrefixExpression(ExpressionParseResult currentState)
        {
            var op = currentState.Tokens.Skip(currentState.Position).Take(1).First();

            var errors = Assert.PrefixExpressionOperator(currentState, op);

            if (errors.Count > 0)
            {
                return Factory.ExpressionParseResult()
                    .Assign(currentState)
                    .Errors(errors)
                    .Create();
            }

            var newState = Factory.ExpressionParseResult()
                    .Assign(currentState)
                    .Position(currentState.Position + Skip.Operator)
                    .Precedence(Precedence.Prefix)
                    .Create();

            newState = ParseExpression(newState);

            var prefix = new InfixExpression(left: null, op: op, right: null);
            var expression = new PrefixExpression(left: prefix, right: newState.Expression);

            return Factory.ExpressionParseResult()
                .Assign(newState)
                .Expression(expression)
                .Position(newState.Position)
                .Precedence(newState.Precedence)
                .Create();
        }

        private static Token GetToken(ExpressionParseResult state, int position)
        {
            return state.Tokens.Skip(position).Take(1).FirstOrDefault();
        }
        
        private static ExpressionParseResult ParseArrayExpression(ExpressionParseResult currentState)
        {
            var nextToken = GetToken(currentState, currentState.Position + Skip.Bracket);

            if (nextToken != default(Token) && nextToken.Kind == SyntaxKind.RightBracket)
            {
                nextToken = GetToken(currentState, currentState.Position + Skip.Bracket + 1);

                return Factory.ExpressionParseResult()
                    .Assign(currentState)
                    .Expression(new ArrayExpression(new List<Expression>()))
                    .Position(currentState.Position + Skip.Bracket + 1)
                    .Precedence(DetermineOperatorPrecedence(nextToken))
                    .Create();
            }

            var newState = Factory.ExpressionParseResult()
                    .Assign(currentState)
                    .Position(currentState.Position + Skip.Bracket)
                    .Create();

            var elementsParseResult = ParseExpressionList(newState, SyntaxKind.RightBracket);

            if (elementsParseResult.Errors.Count > 0)
            {
                return Factory.ExpressionParseResult()
                    .Assign(newState)
                    .Errors(elementsParseResult.Errors)
                    .Create();
            }

            nextToken = GetToken(newState, elementsParseResult.Position + Skip.Bracket);

            return Factory.ExpressionParseResult()
                    .Assign(newState)
                    .Expression(new ArrayExpression(elementsParseResult.Expressions))
                    .Position(elementsParseResult.Position + Skip.Bracket)
                    .Precedence(DetermineOperatorPrecedence(nextToken))
                    .Create();
        }

        private static ExpressionParseResult ParseCallExpression(ExpressionParseResult currentState)
        {
            var argumentListParseResult = ParseExpressionList(currentState, SyntaxKind.RightParenthesis);

            if (argumentListParseResult.Errors.Count > 0)
            {
                return Factory.ExpressionParseResult()
                    .Assign(currentState)
                    .Errors(argumentListParseResult.Errors)
                    .Create();
            }

            Expression fn = default(Expression);

            var identifier = currentState.Tokens
                    .Skip(currentState.Position - Skip.Identifier - Skip.Parenthesis)
                    .Take(1)
                    .Where(token => token.Kind == SyntaxKind.Identifier)
                    .FirstOrDefault();

            if (currentState.Expression.Kind == ExpressionKind.Function || currentState.Expression.Kind == ExpressionKind.Call)
            {
                fn = currentState.Expression;
            }

            var expression = Factory.CallExpression()
                .Arguments(argumentListParseResult.Expressions)
                .Function(fn)
                .Identifier(identifier)
                .Create();

            var nextToken = currentState.Tokens.Skip(argumentListParseResult.Position + 1).Take(1).FirstOrDefault();

            return Factory.ExpressionParseResult()
                .Assign(currentState)
                .Expression(expression)
                .Position(argumentListParseResult.Position + Skip.Parenthesis)
                .Precedence(DetermineOperatorPrecedence(nextToken))
                .Create();
        }

        private static ExpressionListParseResult ParseExpressionList(ExpressionParseResult currentState, SyntaxKind end)
        {   
            List<Expression> expressions = new List<Expression>();

            var closingToken = currentState.Tokens.Where(token => token.Kind == end).FirstOrDefault();
            if (closingToken == default(Token))
            {
                var info = new ErrorInfo
                {
                    Code = ErrorCode.MissingClosingToken,
                    Kind = ErrorKind.MissingToken,
                    Offenders = new List<object> { end },
                    Position = currentState.Tokens.Count,
                    Source = ErrorSource.Parser,
                    Tokens = currentState.Tokens
                };

                return new ExpressionListParseResult
                {
                    Errors = new List<AssertionError> { Error.Create(info) }
                };
            }

            var newState = Factory.ExpressionParseResult()
                    .Assign(currentState)
                    .Precedence(Precedence.Lowest)
                    .Create();

            var currentToken = GetToken(newState, newState.Position);

            while (currentToken != default(Token) && currentToken.Kind != end)
            {
                if (currentToken.Kind == SyntaxKind.Comma)
                {
                    var info = new ErrorInfo
                    {
                        Code = ErrorCode.MissingExpressionToken,
                        Kind = ErrorKind.MissingToken,
                        Position = newState.Position,
                        Source = ErrorSource.Parser,
                        Tokens = newState.Tokens
                    };

                    return new ExpressionListParseResult
                    {
                        Errors = new List<AssertionError> { Error.Create(info) }
                    };
                }

                newState = ParseExpression(newState);
                expressions.Add(newState.Expression);

                currentToken = GetToken(newState, newState.Position);

                if (currentToken == default(Token) || currentToken.Kind == end) break;

                if (currentToken.Kind != SyntaxKind.Comma)
                {
                    var info = new ErrorInfo
                    {
                        Code = ErrorCode.MissingComma,
                        Kind = ErrorKind.InvalidToken,
                        Position = newState.Position,
                        Source = ErrorSource.Parser,
                        Tokens = newState.Tokens
                    };

                    return new ExpressionListParseResult
                    {
                        Errors = new List<AssertionError> { Error.Create(info) }
                    };
                }

                newState = Factory.ExpressionParseResult()
                    .Assign(newState)
                    .Position(newState.Position + Skip.Comma)
                    .Precedence(Precedence.Lowest)
                    .Create();

                currentToken = GetToken(newState, newState.Position);
            }

            return new ExpressionListParseResult
            {
                Errors = new List<AssertionError> { },
                Expressions = expressions,
                Position = newState.Position
            };
        }

        private static ExpressionParseResult ParseFunctionExpression(ExpressionParseResult currentState)
        {
            var functionParametersParseResult = ParseFunctionParameters(currentState);

            var newState = Factory.ExpressionParseResult()
                    .Assign(currentState)
                    .Position(functionParametersParseResult.Position + Skip.Brace)
                    .Create();

            var bodyParseResult = ParseBlockStatement(newState);

            var expression = Factory.FunctionExpression()
                    .Parameters(functionParametersParseResult)
                    .Body(bodyParseResult)
                    .Create();

            var nextToken = currentState.Tokens.Skip(bodyParseResult.Position).Take(1).FirstOrDefault();

            return Factory.ExpressionParseResult()
                .Assign(currentState)
                .Position(bodyParseResult.Position)
                .Precedence(DetermineOperatorPrecedence(nextToken))
                .Expression(expression)
                .Create();
        }

        private static FunctionParametersParseResult ParseFunctionParameters(ExpressionParseResult currentState)
        {
            var position = currentState.Position;
            var currentToken = GetToken(currentState, position);
            var parameters = new List<Token>();

            if (currentToken != null && currentToken.Kind == SyntaxKind.RightParenthesis)
            {
                return new FunctionParametersParseResult
                {
                    Parameters = parameters,
                    Position = position + Skip.Parenthesis
                };
            }

            while (position < currentState.Tokens.Count && currentToken.Kind != SyntaxKind.RightParenthesis)
            {
                if (currentToken.Kind == SyntaxKind.Identifier)
                {
                    parameters.Add(currentToken);
                }
                position++;
                currentToken = GetToken(currentState, position);
            }

            return new FunctionParametersParseResult
            {
                Parameters = parameters,
                Position = position + Skip.Parenthesis
            };
        }

        private static ExpressionParseResult ParseGroupedExpression(ExpressionParseResult currentState)
        {
            var newState = Factory.ExpressionParseResult()
                    .Assign(currentState)
                    .Position(currentState.Position + Skip.Parenthesis)
                    .Precedence(Precedence.Lowest)
                    .Create();

            newState = ParseExpression(newState);

            if (newState.Errors.Count > 0)
            {
                return newState;
            }

            var nextToken = GetToken(newState, newState.Position);

            while (nextToken != null && nextToken.Kind == SyntaxKind.RightParenthesis)
            {
                newState = Factory.ExpressionParseResult()
                    .Assign(newState)
                    .Position(newState.Position + 1)
                    .Create();

                nextToken = GetToken(newState, newState.Position);
            }

            return Factory.ExpressionParseResult()
                .Assign(newState)
                .Precedence(DetermineOperatorPrecedence(nextToken))
                .Create();
        }

        private static ExpressionParseResult ParseHashExpression(ExpressionParseResult currentState)
        {
            var keys = new List<Expression>();
            var values = new List<Expression>();

            var nextToken = GetToken(currentState, currentState.Position + Skip.Brace);
            var position = 0;

            if (nextToken != default(Token) && nextToken.Kind == SyntaxKind.RightBrace)
            {
                nextToken = GetToken(currentState, currentState.Position + Skip.Brace + 1);

                return Factory.ExpressionParseResult()
                    .Assign(currentState)
                    .Expression(new HashExpression(keys, values))
                    .Position(currentState.Position + Skip.Brace + 1)
                    .Precedence(DetermineOperatorPrecedence(nextToken))
                    .Create();
            }

            var hashParseResultState = Factory.ExpressionParseResult()
                    .Assign(currentState)
                    .Position(currentState.Position + Skip.Brace)
                    .Precedence(Precedence.Lowest)
                    .Create();

            while (hashParseResultState.Position < hashParseResultState.Tokens.Count && nextToken.Kind != SyntaxKind.RightBrace)
            {
                var keyParseResult = ParseExpression(hashParseResultState);

                if (keyParseResult.Errors.Count > 0)
                {
                    return keyParseResult;
                }

                nextToken = GetToken(keyParseResult, keyParseResult.Position);

                if (nextToken == default(Token))
                {
                    var info = new ErrorInfo
                    {
                        Code = ErrorCode.MissingColon,
                        Kind = ErrorKind.MissingToken,
                        Position = keyParseResult.Position,
                        Source = ErrorSource.Parser,
                        Tokens = keyParseResult.Tokens
                    };

                    return Factory.ExpressionParseResult()
                        .Assign(keyParseResult)
                        .Errors(new List<AssertionError> { Error.Create(info) })
                        .Create();
                }

                if (nextToken.Kind != SyntaxKind.Colon)
                {
                    var info = new ErrorInfo
                    {
                        Code = ErrorCode.ExpectedColonToken,
                        Kind = ErrorKind.InvalidToken,
                        Position = keyParseResult.Position,
                        Source = ErrorSource.Parser,
                        Tokens = keyParseResult.Tokens
                    };

                    return Factory.ExpressionParseResult()
                        .Assign(keyParseResult)
                        .Errors(new List<AssertionError> { Error.Create(info) })
                        .Create();
                }

                hashParseResultState = Factory.ExpressionParseResult()
                    .Assign(hashParseResultState)
                    .Position(keyParseResult.Position + Skip.Colon)
                    .Precedence(Precedence.Lowest)
                    .Create();

                var valueParseResult = ParseExpression(hashParseResultState);

                if (valueParseResult.Errors.Count > 0)
                {
                    return valueParseResult;
                }

                keys.Add(keyParseResult.Expression);
                values.Add(valueParseResult.Expression);

                nextToken = GetToken(valueParseResult, valueParseResult.Position);

                if (nextToken == default(Token))
                {
                    var info = new ErrorInfo
                    {
                        Code = ErrorCode.MissingClosingToken,
                        Kind = ErrorKind.MissingToken,
                        Offenders = new List<object> { SyntaxKind.RightBrace },
                        Position = valueParseResult.Position,
                        Source = ErrorSource.Parser,
                        Tokens = valueParseResult.Tokens
                    };

                    return Factory.ExpressionParseResult()
                        .Assign(valueParseResult)
                        .Errors(new List<AssertionError> { Error.Create(info) })
                        .Create();
                }

                if (nextToken.Kind != SyntaxKind.Comma && nextToken.Kind != SyntaxKind.RightBrace)
                {
                    var info = new ErrorInfo
                    {
                        Code = ErrorCode.ExpectedCommaToken,
                        Kind = ErrorKind.InvalidToken,
                        Position = valueParseResult.Position,
                        Source = ErrorSource.Parser,
                        Tokens = valueParseResult.Tokens
                    };

                    return Factory.ExpressionParseResult()
                        .Assign(valueParseResult)
                        .Errors(new List<AssertionError> { Error.Create(info) })
                        .Create();
                }
                else
                {
                    var rightBraceToken = GetToken(valueParseResult, valueParseResult.Position + Skip.Comma);

                    // Trailing comma
                    if (rightBraceToken != default(Token) && rightBraceToken.Kind == SyntaxKind.RightBrace)
                    {
                        nextToken = rightBraceToken;
                        position = valueParseResult.Position + Skip.Comma + Skip.Brace;
                    }
                    else if (nextToken.Kind == SyntaxKind.Comma) // Next round
                    {
                        position = valueParseResult.Position + Skip.Comma;
                    }
                    else // Closing brace
                    {
                        position = valueParseResult.Position;
                    }
                }

                hashParseResultState = Factory.ExpressionParseResult()
                    .Assign(valueParseResult)
                    .Position(position)
                    .Precedence(Precedence.Lowest)
                    .Create();
            }

            nextToken = GetToken(hashParseResultState, hashParseResultState.Position + Skip.Brace);

            return Factory.ExpressionParseResult()
                .Assign(hashParseResultState)
                .Expression(new HashExpression(keys, values))
                .Position(hashParseResultState.Position + Skip.Brace)
                .Precedence(DetermineOperatorPrecedence(nextToken))
                .Create();
        }

        private static ExpressionParseResult ParseIfElseExpression(ExpressionParseResult currentState)
        {
            BlockStatementParseResult alternativeParseResult = null;

            var conditionParseResult = ParseExpression(Factory.ExpressionParseResult()
                    .Assign(currentState)
                    .Position(currentState.Position + Skip.If)
                    .Precedence(Precedence.Lowest)
                    .Create()
            );

            if (conditionParseResult.Errors.Count > 0)
            {
                return conditionParseResult;
            }

            var consequenceParseResult = ParseBlockStatement(Factory.ExpressionParseResult()
                    .Assign(conditionParseResult)
                    .Position(conditionParseResult.Position + Skip.Brace)
                    .Create()
            );

            if (consequenceParseResult.Errors.Count > 0)
            {
                return Factory.ExpressionParseResult()
                    .Assign(currentState)
                    .Errors(consequenceParseResult.Errors)
                    .Create();
            }

            var elseToken = currentState.Tokens.Skip(consequenceParseResult.Position).Take(1).FirstOrDefault();

            if (elseToken != null && elseToken.Kind == SyntaxKind.Else)
            {
                alternativeParseResult = ParseBlockStatement(Factory.ExpressionParseResult()
                    .Assign(currentState)
                    .Position(consequenceParseResult.Position + Skip.Else + Skip.Brace)
                    .Create()
                );

                if (alternativeParseResult.Errors.Count > 0)
                {
                    return Factory.ExpressionParseResult()
                        .Assign(currentState)
                        .Errors(alternativeParseResult.Errors)
                        .Create();
                }
            }

            var expression = Factory.IfElseExpression()
                    .Condition(conditionParseResult)
                    .Consequence(consequenceParseResult)
                    .Alternative(alternativeParseResult)
                    .Create();

            var position = alternativeParseResult != null ? alternativeParseResult.Position : consequenceParseResult.Position;

            return Factory.ExpressionParseResult()
                .Assign(currentState)
                .Expression(expression)
                .Position(position)
                .Precedence(Precedence.Lowest)
                .Create();
        }
        
        private static ExpressionParseResult ParseIndexExpression(ExpressionParseResult currentState)
        {
            var newState = Factory.ExpressionParseResult()
                    .Assign(currentState)
                    .Precedence(Precedence.Lowest)
                    .Create();


            var errors = Assert.IndexExpression(newState);

            if (errors.Count > 0)
            {
                return Factory.ExpressionParseResult()
                    .Assign(newState)
                    .Errors(errors)
                    .Create();
            }
            
            var indexExpresionParseResult = ParseExpression(newState);

            if (indexExpresionParseResult.Errors.Count > 0)
            {
                return indexExpresionParseResult;
            }

            var expression = new IndexExpression(left: newState.Expression, index: indexExpresionParseResult.Expression);

            var nextToken = GetToken(indexExpresionParseResult, indexExpresionParseResult.Position + Skip.Bracket);

            return Factory.ExpressionParseResult()
                .Assign(newState)
                .Expression(expression)
                .Position(indexExpresionParseResult.Position + Skip.Bracket)
                .Precedence(DetermineOperatorPrecedence(nextToken))
                .Create();
        }
        
        private static ExpressionParseResult ParseInfixExpression(ExpressionParseResult currentState)
        {
            var op = currentState.Tokens.Skip(currentState.Position).Take(1).First();
            var left = currentState.Expression;

            var newState = Factory.ExpressionParseResult()
                    .Assign(currentState)
                    .Position(currentState.Position + Skip.Operator)
                    .Precedence(DetermineOperatorPrecedence(op))
                    .Create();

            switch (op.Kind)
            {
                case SyntaxKind.LeftParenthesis:
                    return ParseCallExpression(newState);
                case SyntaxKind.LeftBracket:
                    return ParseIndexExpression(newState);
            }

            newState = ParseExpression(newState);

            if (newState.Errors.Count > 0)
            {
                return newState;
            }

            return Factory.ExpressionParseResult()
                .Assign(newState)
                .Expression(new InfixExpression(left: left, op: op, right: newState.Expression))
                .Create();
        }

        private static ExpressionParseResult ParsePrefixExpression(ExpressionParseResult currentState)
        {
            var currentToken = currentState.Tokens.Skip(currentState.Position).Take(1).FirstOrDefault();

            var errors = Assert.ExpressionOperand(currentState, currentToken);

            if (errors.Count > 0)
            {
                return Factory.ExpressionParseResult()
                    .Assign(currentState)
                    .Errors(errors)
                    .Create();
            }

            switch (currentToken.Kind)
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
                    return ParsingFunctions[currentToken.Kind](currentState);
                default:
                    return ExpandPrefixExpression(currentState);
            }
        }

        private static ExpressionParseResult ParseValueExpression(ExpressionParseResult currentState)
        {
            var currentToken = currentState.Tokens.Skip(currentState.Position).Take(1).First();
            var nextToken = currentState.Tokens.Skip(currentState.Position + 1).Take(1).FirstOrDefault();

            return Factory.ExpressionParseResult()
                .Assign(currentState)
                .Expression(CreateExpression(currentToken))
                .Position(currentState.Position + 1)
                .Precedence(DetermineOperatorPrecedence(nextToken))
                .Create();
        }
    }
    
    internal class ExpressionParseResultBuilder
    {
        private ExpressionParseResultBuilderState internalState;

        public ExpressionParseResultBuilder(ExpressionParseResultBuilderState currentState)
        {
            internalState = currentState;
        }

        public ExpressionParseResultBuilder DetermineTokenRange()
        {
            int position = 0;
            int range = 0;
            
            switch (internalState.StatementKind)
            {
                case NodeKind.Let:
                    position = internalState.Position + Skip.Let + Skip.Identifier + Skip.Assign;
                    range = internalState.Range - Skip.Let - Skip.Identifier - Skip.Assign;
                    break;
                case NodeKind.Return:
                    position = internalState.Position + Skip.Return;
                    range = internalState.Range - Skip.Return;
                    break;
                default:
                    position = internalState.Position;
                    range = internalState.Range;
                    break;
            }

            internalState.Tokens = internalState.Tokens
                .Skip(position)
                .Take(range)
                .ToList();

            // Eg. return;
            var onlyToken = internalState.Tokens.FirstOrDefault();
            if (onlyToken != default(Token) && onlyToken.Kind == SyntaxKind.Semicolon)
            {
                internalState.Tokens = new List<Token>();
            } 

            // Since we sliced tokens to contain only the ones of current expression,
            // reset cursor position back to the beginning
            internalState.Position = 0;
            
            return this;
        }

        public ExpressionParseResult Result()
        {
            var initialState = Factory.ExpressionParseResult()
                    .Errors(internalState.Errors)
                    .Position(internalState.Position)
                    .Precedence(Precedence.Lowest)
                    .Tokens(internalState.Tokens)
                    .Create();

            if (initialState.Tokens.Count == 0)
            {
                return initialState;
            }
            
            return Parser.ParseExpression(initialState);
        }
    }
}
