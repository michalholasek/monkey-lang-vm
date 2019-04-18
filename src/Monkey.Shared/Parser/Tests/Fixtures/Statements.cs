using System.Collections.Generic;

using Monkey.Shared;

namespace Monkey.Tests.Fixtures
{
    public static class Statements
    {
        internal static Program Empty = new Program(new ProgramOptions
        {
            Errors = new List<AssertionError>(),
            Kind = NodeKind.Program,
            Position = 0,
            Range = 0,
            Statements = new List<Statement>(),
            Tokens = new List<Token>()
        });

        internal static Dictionary<string, Program> Invalid = new Dictionary<string, Program>
        {
            {
                "let let = 0;",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>
                    {
                        new AssertionError("invalid token(6, 1): got Let, expected Identifier")
                    },
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 5,
                    Statements = new List<Statement>(),
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Let, Line = 1, Literal = "let" },
                        new Token() { Column = 6, Kind = SyntaxKind.Let, Line = 1, Literal = "let" },
                        new Token() { Column = 10, Kind = SyntaxKind.Assign, Line = 1, Literal = "=" },
                        new Token() { Column = 12, Kind = SyntaxKind.Int, Line = 1, Literal = "0" },
                        new Token() { Column = 13, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 14, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "return ,;",
                new Program(new ProgramOptions
                 {
                    Errors = new List<AssertionError>
                    {
                        new AssertionError("unexpected token(9, 1): got Comma")
                    },
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 3,
                    Statements = new List<Statement>(),
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Return, Line = 1, Literal = "return" },
                        new Token() { Column = 9, Kind = SyntaxKind.Comma, Line = 1, Literal = "," },
                        new Token() { Column = 10, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 11, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            }
        };

        internal static Dictionary<string, Program> Let = new Dictionary<string, Program>
        {
            {
                "let int = 5;",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 5,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new IntegerExpression(5),
                            Identifier = new Token() { Column = 6, Kind = SyntaxKind.Identifier, Line = 1, Literal = "int" },
                            Kind = NodeKind.Let,
                            Position = 0,
                            Range = 5
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Let, Line = 1, Literal = "let" },
                        new Token() { Column = 6, Kind = SyntaxKind.Identifier, Line = 1, Literal = "int" },
                        new Token() { Column = 10, Kind = SyntaxKind.Assign, Line = 1, Literal = "=" },
                        new Token() { Column = 12, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 13, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 14, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "let x = 5; let y = 10; let foobar = 838383;",
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
                            Expression = new IntegerExpression(5),
                            Identifier = new Token() { Column = 6, Kind = SyntaxKind.Identifier, Line = 1, Literal = "x" },
                            Kind = NodeKind.Let,
                            Position = 0,
                            Range = 5
                        }),
                        new Statement(new StatementOptions
                        {
                            Expression = new IntegerExpression(10),
                            Identifier = new Token() { Column = 17, Kind = SyntaxKind.Identifier, Line = 1, Literal = "y" },
                            Kind = NodeKind.Let,
                            Position = 5,
                            Range = 5
                        }),
                        new Statement(new StatementOptions
                        {
                            Expression = new IntegerExpression(838383),
                            Identifier = new Token() { Column = 29, Kind = SyntaxKind.Identifier, Line = 1, Literal = "foobar" },
                            Kind = NodeKind.Let,
                            Position = 10,
                            Range = 5
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Let, Line = 1, Literal = "let" },
                        new Token() { Column = 6, Kind = SyntaxKind.Identifier, Line = 1, Literal = "x" },
                        new Token() { Column = 8, Kind = SyntaxKind.Assign, Line = 1, Literal = "=" },
                        new Token() { Column = 10, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 11, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 13, Kind = SyntaxKind.Let, Line = 1, Literal = "let" },
                        new Token() { Column = 17, Kind = SyntaxKind.Identifier, Line = 1, Literal = "y" },
                        new Token() { Column = 19, Kind = SyntaxKind.Assign, Line = 1, Literal = "=" },
                        new Token() { Column = 21, Kind = SyntaxKind.Int, Line = 1, Literal = "10" },
                        new Token() { Column = 23, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 25, Kind = SyntaxKind.Let, Line = 1, Literal = "let" },
                        new Token() { Column = 29, Kind = SyntaxKind.Identifier, Line = 1, Literal = "foobar" },
                        new Token() { Column = 36, Kind = SyntaxKind.Assign, Line = 1, Literal = "=" },
                        new Token() { Column = 38, Kind = SyntaxKind.Int, Line = 1, Literal = "838383" },
                        new Token() { Column = 44, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 45, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            }
        };

        internal static Dictionary<string, Program> Return = new Dictionary<string, Program>
        {
            {
                "return 5; return 10; return 838383;",
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
                            Expression = new IntegerExpression(5),
                            Identifier = null,
                            Kind = NodeKind.Return,
                            Position = 0,
                            Range = 3
                        }),
                        new Statement(new StatementOptions
                        {
                            Expression = new IntegerExpression(10),
                            Identifier = null,
                            Kind = NodeKind.Return,
                            Position = 3,
                            Range = 3
                        }),
                        new Statement(new StatementOptions
                        {
                            Expression = new IntegerExpression(838383),
                            Identifier = null,
                            Kind = NodeKind.Return,
                            Position = 6,
                            Range = 3
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Return, Line = 1, Literal = "return" },
                        new Token() { Column = 9, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 10, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 12, Kind = SyntaxKind.Return, Line = 1, Literal = "return" },
                        new Token() { Column = 19, Kind = SyntaxKind.Int, Line = 1, Literal = "10" },
                        new Token() { Column = 21, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 23, Kind = SyntaxKind.Return, Line = 1, Literal = "return" },
                        new Token() { Column = 30, Kind = SyntaxKind.Int, Line = 1, Literal = "838383" },
                        new Token() { Column = 36, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 37, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "return false;",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 3,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new BooleanExpression(false),
                            Identifier = null,
                            Kind = NodeKind.Return,
                            Position = 0,
                            Range = 3
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Return, Line = 1, Literal = "return" },
                        new Token() { Column = 9, Kind = SyntaxKind.False, Line = 1, Literal = "false" },
                        new Token() { Column = 14, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 15, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "return \"Hello!\";",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 3,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new StringExpression("Hello!"),
                            Identifier = null,
                            Kind = NodeKind.Return,
                            Position = 0,
                            Range = 3
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Return, Line = 1, Literal = "return" },
                        new Token() { Column = 9, Kind = SyntaxKind.String, Line = 1, Literal = "Hello!" },
                        new Token() { Column = 17, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 18, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            }
        };
    }
}
