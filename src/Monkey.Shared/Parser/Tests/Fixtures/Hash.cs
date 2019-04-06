using System.Collections.Generic;

using Monkey.Shared.Parser;
using Monkey.Shared.Parser.Ast;
using Monkey.Shared.Scanner;

namespace Monkey.Tests.Fixtures
{
    public static partial class Expressions
    {
        internal static Dictionary<string, Program> Hash = new Dictionary<string, Program>
        {
            {
                "{}",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 2,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new HashExpression(new List<Expression>(), new List<Expression>()),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 2
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.LeftBrace, Line = 1, Literal = "{" },
                        new Token() { Column = 3, Kind = SyntaxKind.RightBrace, Line = 1, Literal = "}" },
                        new Token() { Column = 4, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "{ \"one\": 1, \"two\": 2, \"three\": 3 }",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 13,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new HashExpression
                            (
                                keys: new List<Expression>
                                {
                                    new StringExpression("one"),
                                    new StringExpression("two"),
                                    new StringExpression("three")
                                },
                                values: new List<Expression>
                                {
                                    new IntegerExpression(1),
                                    new IntegerExpression(2),
                                    new IntegerExpression(3)
                                }
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 13
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.LeftBrace, Line = 1, Literal = "{" },
                        new Token() { Column = 4, Kind = SyntaxKind.String, Line = 1, Literal = "one" },
                        new Token() { Column = 9, Kind = SyntaxKind.Colon, Line = 1, Literal = ":" },
                        new Token() { Column = 11, Kind = SyntaxKind.Int, Line = 1, Literal = "1" },
                        new Token() { Column = 12, Kind = SyntaxKind.Comma, Line = 1, Literal = "," },
                        new Token() { Column = 14, Kind = SyntaxKind.String, Line = 1, Literal = "two" },
                        new Token() { Column = 19, Kind = SyntaxKind.Colon, Line = 1, Literal = ":" },
                        new Token() { Column = 21, Kind = SyntaxKind.Int, Line = 1, Literal = "2" },
                        new Token() { Column = 22, Kind = SyntaxKind.Comma, Line = 1, Literal = "," },
                        new Token() { Column = 24, Kind = SyntaxKind.String, Line = 1, Literal = "three" },
                        new Token() { Column = 31, Kind = SyntaxKind.Colon, Line = 1, Literal = ":" },
                        new Token() { Column = 33, Kind = SyntaxKind.Int, Line = 1, Literal = "3" },
                        new Token() { Column = 35, Kind = SyntaxKind.RightBrace, Line = 1, Literal = "}" },
                        new Token() { Column = 36, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "{ \"abc\": 42 }[\"abc\"]",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 8,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new IndexExpression
                            (
                                left: new HashExpression
                                (
                                    new List<Expression> { new StringExpression("abc") },
                                    new List<Expression> { new IntegerExpression(42) }
                                ),
                                index: new StringExpression("abc")
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 8
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.LeftBrace, Line = 1, Literal = "{" },
                        new Token() { Column = 4, Kind = SyntaxKind.String, Line = 1, Literal = "abc" },
                        new Token() { Column = 9, Kind = SyntaxKind.Colon, Line = 1, Literal = ":" },
                        new Token() { Column = 11, Kind = SyntaxKind.Int, Line = 1, Literal = "42" },
                        new Token() { Column = 14, Kind = SyntaxKind.RightBrace, Line = 1, Literal = "}" },
                        new Token() { Column = 15, Kind = SyntaxKind.LeftBracket, Line = 1, Literal = "[" },
                        new Token() { Column = 16, Kind = SyntaxKind.String, Line = 1, Literal = "abc" },
                        new Token() { Column = 21, Kind = SyntaxKind.RightBracket, Line = 1, Literal = "]" },
                        new Token() { Column = 22, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            }
        };
    }
}
