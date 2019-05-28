using System;
using System.Collections.Generic;

namespace Monkey.Shared
{
    public partial class Parser
    {
        internal static class Factory
        {
            internal class CallExpressionFactory
            {
                private List<Expression> arguments { get; set; }
                private Expression function { get; set; }
                private Token identifier { get; set; }

                public CallExpressionFactory Arguments(List<Expression> arguments)
                {
                    this.arguments = arguments;
                    return this;
                }

                public CallExpressionFactory Function(Expression fn)
                {
                    this.function = fn;
                    return this;
                }

                public CallExpressionFactory Identifier(Token identifier)
                {
                    this.identifier = identifier;
                    return this;
                }

                public CallExpression Create()
                {
                    return new CallExpression(identifier, arguments, function);
                }
            }

            internal class ExpressionParseResultBuilderFactory
            {
                private List<AssertionError> errors { get; set; }
                private Expression expression { get; set; }
                private int position { get; set; }
                private int range { get; set; }
                private NodeKind statementKind { get; set; }
                private List<Token> tokens { get; set; }

                public ExpressionParseResultBuilder Create()
                {
                    var initialState = new ExpressionParseResultBuilderState
                    {
                        Errors = errors,
                        Expression = expression,
                        Position = position,
                        Range = range,
                        StatementKind = statementKind,
                        Tokens = tokens
                    };

                    return new ExpressionParseResultBuilder(initialState);
                }

                public ExpressionParseResultBuilderFactory Errors(List<AssertionError> errors)
                {
                    this.errors = errors;
                    return this;
                }

                public ExpressionParseResultBuilderFactory Expression(Expression expression)
                {
                    this.expression = expression;
                    return this;
                }

                public ExpressionParseResultBuilderFactory Position(int position)
                {
                    this.position = position;
                    return this;
                }

                public ExpressionParseResultBuilderFactory Range(int range)
                {
                    this.range = range;
                    return this;
                }

                public ExpressionParseResultBuilderFactory StatementKind(NodeKind kind)
                {
                    this.statementKind = kind;
                    return this;
                }

                public ExpressionParseResultBuilderFactory Tokens(List<Token> tokens)
                {
                    this.tokens = tokens;
                    return this;
                }
            }

            internal class ExpressionParseResultFactory
            {
                private List<AssertionError> errors { get; set; }
                private Expression expression { get; set; }
                private int position { get; set; }
                private Precedence precedence { get; set; }
                private List<Token> tokens { get; set; }

                public ExpressionParseResultFactory Assign(ExpressionParseResult previousState)
                {
                    errors = previousState.Errors;
                    expression = previousState.Expression;
                    position = previousState.Position;
                    precedence = previousState.Precedence;
                    tokens = previousState.Tokens;

                    return this;
                }

                public ExpressionParseResult Create()
                {
                    return new ExpressionParseResult
                    {
                        Errors = errors,
                        Expression = expression,
                        Position = position,
                        Precedence = precedence,
                        Tokens = tokens
                    };
                }

                public ExpressionParseResultFactory Errors(List<AssertionError> errors)
                {
                    this.errors = errors;
                    return this;
                }

                public ExpressionParseResultFactory Expression(Expression expression)
                {
                    this.expression = expression;
                    return this;
                }

                public ExpressionParseResultFactory Position(int position)
                {
                    this.position = position;
                    return this;
                }

                public ExpressionParseResultFactory Precedence(Precedence precedence)
                {
                    this.precedence = precedence;
                    return this;
                }

                public ExpressionParseResultFactory Tokens(List<Token> tokens)
                {
                    this.tokens = tokens;
                    return this;
                }
            }

            internal class FunctionExpressionFactory
            {
                private BlockStatement body { get; set; }
                private List<Token> parameters { get; set; }

                public FunctionExpressionFactory Body(BlockStatementParseResult bodyParseResult)
                {
                    body = new BlockStatement { Statements = bodyParseResult.Statements };
                    return this;
                }

                public FunctionExpressionFactory Parameters(FunctionParametersParseResult functionParametersParseResult)
                {
                    parameters = functionParametersParseResult.Parameters;
                    return this;
                }

                public FunctionExpression Create()
                {
                    return new FunctionExpression(parameters, body);
                }
            }
            
            internal class IfElseExpressionFactory
            {
                private BlockStatement alternative { get; set; }
                private Expression condition { get; set; }
                private BlockStatement consequence { get; set; }

                public IfElseExpressionFactory Alternative(BlockStatementParseResult alternativeParseResult)
                {
                    if (alternativeParseResult == default(BlockStatementParseResult))
                    {
                        return this;
                    }

                    alternative = new BlockStatement { Statements = alternativeParseResult.Statements };
                    return this;
                }

                public IfElseExpressionFactory Condition(ExpressionParseResult conditionParseResult)
                {
                    condition = conditionParseResult.Expression;
                    return this;
                }

                public IfElseExpressionFactory Consequence(BlockStatementParseResult consequenceParseResult)
                {
                    consequence = new BlockStatement { Statements = consequenceParseResult.Statements };
                    return this;
                }

                public IfElseExpression Create()
                {
                    return new IfElseExpression(condition, consequence, alternative);
                }
            }
            
            internal class ProgramFactory
            {
                private List<Token> tokens;

                public Program Create()
                {
                    var options = new ProgramOptions
                    {
                        Errors = new List<AssertionError>(),
                        Kind = NodeKind.Program,
                        Position = 0,
                        Range = tokens.Count > 0 ? tokens.Count - 1 : 0,
                        Statements = new List<Statement>(),
                        Tokens = tokens
                    };

                    return new Program(options);
                }

                public ProgramFactory Tokens(List<Token> tokens)
                {
                    this.tokens = tokens;
                    return this;
                }
            }
            
            internal class StatementFactory
            {
                private Expression expression { get; set; }
                private Token identifier { get; set; }
                private NodeKind kind { get; set; }
                private int position { get; set; }
                private int range { get; set; }

                public Statement Create()
                {
                    var options = new StatementOptions
                    {
                        Expression = expression,
                        Identifier = identifier,
                        Kind = kind,
                        Position = position,
                        Range = range
                    };

                    return new Statement(options);
                }

                public StatementFactory Expression(Expression expression)
                {
                    this.expression = expression;
                    return this;
                }

                public StatementFactory Identifier(Token identifier)
                {
                    this.identifier = identifier;
                    return this;
                }

                public StatementFactory Kind(NodeKind kind)
                {
                    this.kind = kind;
                    return this;
                }

                public StatementFactory Position(int position)
                {
                    this.position = position;
                    return this;
                }

                public StatementFactory Range(int range)
                {
                    this.range = range;
                    return this;
                }
            }
            
            internal class StatementParseResultBuilderFactory
            {
                private StatementParseResult previousState;

                public StatementParseResultBuilderFactory Assign(StatementParseResult previousState)
                {
                    this.previousState = previousState;
                    return this;
                }

                public StatementParseResultBuilder Create()
                {
                    return new StatementParseResultBuilder(previousState);
                }

                public StatementParseResultBuilderFactory Errors(List<AssertionError> errors)
                {
                    this.previousState.Errors = errors;
                    return this;
                }
            }
            
            internal class StatementParseResultFactory
            {
                private List<AssertionError> errors { get; set; }
                private int position { get; set; }
                private Statement statement { get; set; }
                private Token token { get; set; }
                private List<Token> tokens { get; set; }

                public StatementParseResultFactory Assign(StatementParseResult previousState)
                {
                    errors = previousState.Errors;
                    position = previousState.Position;
                    statement = previousState.Statement;
                    token = previousState.Token;
                    tokens = previousState.Tokens;

                    return this;
                }

                public StatementParseResult Create()
                {
                    return new StatementParseResult
                    {
                        Errors = errors,
                        Position = position,
                        Statement = statement,
                        Token = token,
                        Tokens = tokens
                    };
                }

                public StatementParseResultFactory Errors(List<AssertionError> errors)
                {
                    this.errors = errors;
                    return this;
                }

                public StatementParseResultFactory Position(int position)
                {
                    this.position = position;
                    return this;
                }

                public StatementParseResultFactory Statement(Statement statement)
                {
                    this.statement = statement;
                    return this;
                }

                public StatementParseResultFactory Token(Token token)
                {
                    this.token = token;
                    return this;
                }

                public StatementParseResultFactory Tokens(List<Token> tokens)
                {
                    this.tokens = tokens;
                    return this;
                }
            }
            
            public static CallExpressionFactory CallExpression()
            {
                return new CallExpressionFactory();
            }
            
            public static ExpressionParseResultBuilderFactory ExpressionParseResultBuilder()
            {
                return new ExpressionParseResultBuilderFactory();
            }
            
            public static ExpressionParseResultFactory ExpressionParseResult()
            {
                return new ExpressionParseResultFactory();
            }

            public static FunctionExpressionFactory FunctionExpression()
            {
                return new FunctionExpressionFactory();
            }
            
            public static IfElseExpressionFactory IfElseExpression()
            {
                return new IfElseExpressionFactory();
            }
            
            public static ProgramFactory Program()
            {
                return new ProgramFactory();
            }
            
            public static StatementFactory Statement()
            {
                return new StatementFactory();
            }
            
            public static StatementParseResultBuilderFactory StatementParseResultBuilder()
            {
                return new StatementParseResultBuilderFactory();
            }

            public static StatementParseResultFactory StatementParseResult()
            {
                return new StatementParseResultFactory();
            }
        }
    }
}
