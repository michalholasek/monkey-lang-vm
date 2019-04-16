using System.Collections.Generic;

using Monkey.Shared;
using Monkey.Shared.Parser;
using Monkey.Shared.Parser.Ast;
using Monkey.Shared.Scanner;

namespace Monkey.Tests.Fixtures
{
    public static partial class Expressions
    {
        internal static Dictionary<string, Program> Infix = new Dictionary<string, Program>
        {
            {
                "5 + 5",
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
                            Expression = new InfixExpression
                            (
                                left: new IntegerExpression(5),
                                op: new Token() { Column = 4, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                right: new IntegerExpression(5)
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 3
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 4, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 6, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 7, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "5 - 5",
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
                            Expression = new InfixExpression
                            (
                                left: new IntegerExpression(5),
                                op: new Token() { Column = 4, Kind = SyntaxKind.Minus, Line = 1, Literal = "-" },
                                right: new IntegerExpression(5)
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 3
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 4, Kind = SyntaxKind.Minus, Line = 1, Literal = "-" },
                        new Token() { Column = 6, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 7, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "5 * 5",
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
                            Expression = new InfixExpression
                            (
                                left: new IntegerExpression(5),
                                op: new Token() { Column = 4, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                                right: new IntegerExpression(5)
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 3
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 4, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                        new Token() { Column = 6, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 7, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "5 / 5",
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
                            Expression = new InfixExpression
                            (
                                left: new IntegerExpression(5),
                                op: new Token() { Column = 4, Kind = SyntaxKind.Slash, Line = 1, Literal = "/" },
                                right: new IntegerExpression(5)
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 3
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 4, Kind = SyntaxKind.Slash, Line = 1, Literal = "/" },
                        new Token() { Column = 6, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 7, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "5 > 5",
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
                            Expression = new InfixExpression
                            (
                                left: new IntegerExpression(5),
                                op: new Token() { Column = 4, Kind = SyntaxKind.GreaterThan, Line = 1, Literal = ">" },
                                right: new IntegerExpression(5)
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 3
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 4, Kind = SyntaxKind.GreaterThan, Line = 1, Literal = ">" },
                        new Token() { Column = 6, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 7, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "5 < 5",
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
                            Expression = new InfixExpression
                            (
                                left: new IntegerExpression(5),
                                op: new Token() { Column = 4, Kind = SyntaxKind.LessThan, Line = 1, Literal = "<" },
                                right: new IntegerExpression(5)
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 3
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 4, Kind = SyntaxKind.LessThan, Line = 1, Literal = "<" },
                        new Token() { Column = 6, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 7, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "5 == 5",
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
                            Expression = new InfixExpression
                            (
                                left: new IntegerExpression(5),
                                op: new Token() { Column = 4, Kind = SyntaxKind.Equal, Line = 1, Literal = "==" },
                                right: new IntegerExpression(5)
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 3
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 4, Kind = SyntaxKind.Equal, Line = 1, Literal = "==" },
                        new Token() { Column = 7, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 8, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "5 != 5",
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
                            Expression = new InfixExpression
                            (
                                left: new IntegerExpression(5),
                                op: new Token() { Column = 4, Kind = SyntaxKind.NotEqual, Line = 1, Literal = "!=" },
                                right: new IntegerExpression(5)
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 3
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 4, Kind = SyntaxKind.NotEqual, Line = 1, Literal = "!=" },
                        new Token() { Column = 7, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 8, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "true == true",
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
                            Expression = new InfixExpression
                            (
                                left: new BooleanExpression(true),
                                op: new Token() { Column = 7, Kind = SyntaxKind.Equal, Line = 1, Literal = "==" },
                                right: new BooleanExpression(true)
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 3
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.True, Line = 1, Literal = "true" },
                        new Token() { Column = 7, Kind = SyntaxKind.Equal, Line = 1, Literal = "==" },
                        new Token() { Column = 10, Kind = SyntaxKind.True, Line = 1, Literal = "true" },
                        new Token() { Column = 14, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "true != false",
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
                            Expression = new InfixExpression
                            (
                                left: new BooleanExpression(true),
                                op: new Token() { Column = 7, Kind = SyntaxKind.NotEqual, Line = 1, Literal = "!=" },
                                right: new BooleanExpression(false)
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 3
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.True, Line = 1, Literal = "true" },
                        new Token() { Column = 7, Kind = SyntaxKind.NotEqual, Line = 1, Literal = "!=" },
                        new Token() { Column = 10, Kind = SyntaxKind.False, Line = 1, Literal = "false" },
                        new Token() { Column = 15, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "false == false",
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
                            Expression = new InfixExpression
                            (
                                left: new BooleanExpression(false),
                                op: new Token() { Column = 8, Kind = SyntaxKind.Equal, Line = 1, Literal = "==" },
                                right: new BooleanExpression(false)
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 3
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.False, Line = 1, Literal = "false" },
                        new Token() { Column = 8, Kind = SyntaxKind.Equal, Line = 1, Literal = "==" },
                        new Token() { Column = 11, Kind = SyntaxKind.False, Line = 1, Literal = "false" },
                        new Token() { Column = 16, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            }
        };
    }
}
