using System.Collections.Generic;

using Monkey.Shared;
using Monkey.Shared.Parser;
using Monkey.Shared.Parser.Ast;
using Monkey.Shared.Scanner;

namespace Monkey.Tests.Fixtures
{
    public static partial class Expressions
    {
        internal static Dictionary<string, Program> IfElse = new Dictionary<string, Program>
        {
            {
                "if (x < y) { x; }",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 10,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new IfElseExpression
                            (
                                condition: new InfixExpression
                                (
                                    left: new IdentifierExpression("x"),
                                    op: new Token() { Column = 8, Kind = SyntaxKind.LessThan, Line = 1, Literal = "<" },
                                    right: new IdentifierExpression("y")
                                ),
                                consequence: new BlockStatement
                                {
                                    Statements = new List<Statement>
                                    {
                                        new Statement(new StatementOptions
                                        {
                                            Expression = new IdentifierExpression("x"),
                                            Identifier = null,
                                            Kind = NodeKind.Expression,
                                            Position = 7,
                                            Range = 2
                                        })
                                    }
                                },
                                alternative: null
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 10
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.If, Line = 1, Literal = "if" },
                        new Token() { Column = 5, Kind = SyntaxKind.LeftParenthesis, Line = 1, Literal = "(" },
                        new Token() { Column = 6, Kind = SyntaxKind.Identifier, Line = 1, Literal = "x" },
                        new Token() { Column = 8, Kind = SyntaxKind.LessThan, Line = 1, Literal = "<" },
                        new Token() { Column = 10, Kind = SyntaxKind.Identifier, Line = 1, Literal = "y" },
                        new Token() { Column = 11, Kind = SyntaxKind.RightParenthesis, Line = 1, Literal = ")" },
                        new Token() { Column = 13, Kind = SyntaxKind.LeftBrace, Line = 1, Literal = "{" },
                        new Token() { Column = 15, Kind = SyntaxKind.Identifier, Line = 1, Literal = "x" },
                        new Token() { Column = 16, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 18, Kind = SyntaxKind.RightBrace, Line = 1, Literal = "}" },
                        new Token() { Column = 19, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "if (x < y) { x; } else { y; }",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 15,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new IfElseExpression
                            (
                                alternative: new BlockStatement
                                {
                                    Statements = new List<Statement>
                                    {
                                        new Statement(new StatementOptions
                                        {
                                            Expression = new IdentifierExpression("y"),
                                            Identifier = null,
                                            Kind = NodeKind.Expression,
                                            Position = 12,
                                            Range = 2
                                        })
                                    }
                                },
                                condition: new InfixExpression
                                (
                                    left: new IdentifierExpression("x"),
                                    op: new Token() { Column = 8, Kind = SyntaxKind.LessThan, Line = 1, Literal = "<" },
                                    right: new IdentifierExpression("y")
                                ),
                                consequence: new BlockStatement
                                {
                                    Statements = new List<Statement>
                                    {
                                        new Statement(new StatementOptions
                                        {
                                            Expression = new IdentifierExpression("x"),
                                            Identifier = null,
                                            Kind = NodeKind.Expression,
                                            Position = 7,
                                            Range = 2
                                        })
                                    }
                                }
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 15
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.If, Line = 1, Literal = "if" },
                        new Token() { Column = 5, Kind = SyntaxKind.LeftParenthesis, Line = 1, Literal = "(" },
                        new Token() { Column = 6, Kind = SyntaxKind.Identifier, Line = 1, Literal = "x" },
                        new Token() { Column = 8, Kind = SyntaxKind.LessThan, Line = 1, Literal = "<" },
                        new Token() { Column = 10, Kind = SyntaxKind.Identifier, Line = 1, Literal = "y" },
                        new Token() { Column = 11, Kind = SyntaxKind.RightParenthesis, Line = 1, Literal = ")" },
                        new Token() { Column = 13, Kind = SyntaxKind.LeftBrace, Line = 1, Literal = "{" },
                        new Token() { Column = 15, Kind = SyntaxKind.Identifier, Line = 1, Literal = "x" },
                        new Token() { Column = 16, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 18, Kind = SyntaxKind.RightBrace, Line = 1, Literal = "}" },
                        new Token() { Column = 20, Kind = SyntaxKind.Else, Line = 1, Literal = "else" },
                        new Token() { Column = 25, Kind = SyntaxKind.LeftBrace, Line = 1, Literal = "{" },
                        new Token() { Column = 27, Kind = SyntaxKind.Identifier, Line = 1, Literal = "y" },
                        new Token() { Column = 28, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 30, Kind = SyntaxKind.RightBrace, Line = 1, Literal = "}" },
                        new Token() { Column = 31, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            }
        };
    }
}
