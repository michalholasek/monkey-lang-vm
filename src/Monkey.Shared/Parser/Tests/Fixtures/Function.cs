using System.Collections.Generic;

using Monkey.Shared;

namespace Monkey.Tests.Fixtures
{
    public static partial class Expressions
    {
        internal static Dictionary<string, Program> Function = new Dictionary<string, Program>
        {
            {
                "fn() { x + y; }",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 9,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new FunctionExpression
                            (
                                body: new BlockStatement
                                {
                                    Statements = new List<Statement>
                                    {
                                        new Statement(new StatementOptions
                                        {
                                            Expression = new InfixExpression
                                            (
                                                left: new IdentifierExpression("x"),
                                                op: new Token() { Column = 11, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                                right: new IdentifierExpression("y")
                                            ),
                                            Identifier = null,
                                            Kind = NodeKind.Expression,
                                            Position = 4,
                                            Range = 4
                                        })
                                    }
                                },
                                parameters: new List<Token>()
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 9
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Function, Line = 1, Literal = "fn" },
                        new Token() { Column = 4, Kind = SyntaxKind.LeftParenthesis, Line = 1, Literal = "(" },
                        new Token() { Column = 5, Kind = SyntaxKind.RightParenthesis, Line = 1, Literal = ")" },
                        new Token() { Column = 7, Kind = SyntaxKind.LeftBrace, Line = 1, Literal = "{" },
                        new Token() { Column = 9, Kind = SyntaxKind.Identifier, Line = 1, Literal = "x" },
                        new Token() { Column = 11, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 13, Kind = SyntaxKind.Identifier, Line = 1, Literal = "y" },
                        new Token() { Column = 14, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 16, Kind = SyntaxKind.RightBrace, Line = 1, Literal = "}" },
                        new Token() { Column = 17, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "fn(x, y) { x + y; }",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 12,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new FunctionExpression
                            (
                                body: new BlockStatement
                                {
                                    Statements = new List<Statement>
                                    {
                                        new Statement(new StatementOptions
                                        {
                                            Expression = new InfixExpression
                                            (
                                                left: new IdentifierExpression("x"),
                                                op: new Token() { Column = 15, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                                right: new IdentifierExpression("y")
                                            ),
                                            Identifier = null,
                                            Kind = NodeKind.Expression,
                                            Position = 7,
                                            Range = 4
                                        })
                                    }
                                },
                                parameters: new List<Token>
                                {
                                    new Token() { Column = 5, Kind = SyntaxKind.Identifier, Line = 1, Literal = "x" },
                                    new Token() { Column = 8, Kind = SyntaxKind.Identifier, Line = 1, Literal = "y" }
                                }
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 12
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Function, Line = 1, Literal = "fn" },
                        new Token() { Column = 4, Kind = SyntaxKind.LeftParenthesis, Line = 1, Literal = "(" },
                        new Token() { Column = 5, Kind = SyntaxKind.Identifier, Line = 1, Literal = "x" },
                        new Token() { Column = 6, Kind = SyntaxKind.Comma, Line = 1, Literal = "," },
                        new Token() { Column = 8, Kind = SyntaxKind.Identifier, Line = 1, Literal = "y" },
                        new Token() { Column = 9, Kind = SyntaxKind.RightParenthesis, Line = 1, Literal = ")" },
                        new Token() { Column = 11, Kind = SyntaxKind.LeftBrace, Line = 1, Literal = "{" },
                        new Token() { Column = 13, Kind = SyntaxKind.Identifier, Line = 1, Literal = "x" },
                        new Token() { Column = 15, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 17, Kind = SyntaxKind.Identifier, Line = 1, Literal = "y" },
                        new Token() { Column = 18, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 20, Kind = SyntaxKind.RightBrace, Line = 1, Literal = "}" },
                        new Token() { Column = 21, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            }
        };
    }
}
