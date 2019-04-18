using System.Collections.Generic;

using Monkey.Shared;

namespace Monkey.Tests.Fixtures
{
    public static partial class Expressions
    {
        internal static Dictionary<string, Program> OperatorPrecedences = new Dictionary<string, Program>
        {
            {
                "-a * b",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 4,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new InfixExpression
                            (
                                left: new PrefixExpression
                                (
                                    left: new InfixExpression(left: null, op: new Token() { Column = 2, Kind = SyntaxKind.Minus, Line = 1, Literal = "-" }, right: null),
                                    right: new IdentifierExpression("a")
                                ),
                                op: new Token() { Column = 5, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                                right: new IdentifierExpression("b")
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 4
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Minus, Line = 1, Literal = "-" },
                        new Token() { Column = 3, Kind = SyntaxKind.Identifier, Line = 1, Literal = "a" },
                        new Token() { Column = 5, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                        new Token() { Column = 7, Kind = SyntaxKind.Identifier, Line = 1, Literal = "b" },
                        new Token() { Column = 8, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "!-a",
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
                            Expression = new PrefixExpression
                            (
                                left: new InfixExpression(left: null, op: new Token() { Column = 2, Kind = SyntaxKind.Bang, Line = 1, Literal = "!" }, right: null),
                                right: new PrefixExpression
                                (
                                    left: new InfixExpression(left: null, op: new Token() { Column = 3, Kind = SyntaxKind.Minus, Line = 1, Literal = "-" }, right: null),
                                    right: new IdentifierExpression("a")
                                )
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 3
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Bang, Line = 1, Literal = "!" },
                        new Token() { Column = 3, Kind = SyntaxKind.Minus, Line = 1, Literal = "-" },
                        new Token() { Column = 4, Kind = SyntaxKind.Identifier, Line = 1, Literal = "a" },
                        new Token() { Column = 5, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "a + b + c",
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
                            Expression = new InfixExpression
                            (
                                left: new InfixExpression
                                (
                                    left: new IdentifierExpression("a"),
                                    op: new Token() { Column = 4, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                    right: new IdentifierExpression("b")
                                ),
                                op: new Token() { Column = 8, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                right: new IdentifierExpression("c")
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 5
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Identifier, Line = 1, Literal = "a" },
                        new Token() { Column = 4, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 6, Kind = SyntaxKind.Identifier, Line = 1, Literal = "b" },
                        new Token() { Column = 8, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 10, Kind = SyntaxKind.Identifier, Line = 1, Literal = "c" },
                        new Token() { Column = 11, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "a + b - c",
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
                            Expression = new InfixExpression
                            (
                                left: new InfixExpression
                                (
                                    left: new IdentifierExpression("a"),
                                    op: new Token() { Column = 4, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                    right: new IdentifierExpression("b")
                                ),
                                op: new Token() { Column = 8, Kind = SyntaxKind.Minus, Line = 1, Literal = "-" },
                                right: new IdentifierExpression("c")
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 5
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Identifier, Line = 1, Literal = "a" },
                        new Token() { Column = 4, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 6, Kind = SyntaxKind.Identifier, Line = 1, Literal = "b" },
                        new Token() { Column = 8, Kind = SyntaxKind.Minus, Line = 1, Literal = "-" },
                        new Token() { Column = 10, Kind = SyntaxKind.Identifier, Line = 1, Literal = "c" },
                        new Token() { Column = 11, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "a * b * c",
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
                            Expression = new InfixExpression
                            (
                                left: new InfixExpression
                                (
                                    left: new IdentifierExpression("a"),
                                    op: new Token() { Column = 4, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                                    right: new IdentifierExpression("b")
                                ),
                                op: new Token() { Column = 8, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                                right: new IdentifierExpression("c")
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 5
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Identifier, Line = 1, Literal = "a" },
                        new Token() { Column = 4, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                        new Token() { Column = 6, Kind = SyntaxKind.Identifier, Line = 1, Literal = "b" },
                        new Token() { Column = 8, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                        new Token() { Column = 10, Kind = SyntaxKind.Identifier, Line = 1, Literal = "c" },
                        new Token() { Column = 11, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "a * b / c",
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
                            Expression = new InfixExpression
                            (
                                left: new InfixExpression
                                (
                                    left: new IdentifierExpression("a"),
                                    op: new Token() { Column = 4, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                                    right: new IdentifierExpression("b")
                                ),
                                op: new Token() { Column = 8, Kind = SyntaxKind.Slash, Line = 1, Literal = "/" },
                                right: new IdentifierExpression("c")
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 5
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Identifier, Line = 1, Literal = "a" },
                        new Token() { Column = 4, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                        new Token() { Column = 6, Kind = SyntaxKind.Identifier, Line = 1, Literal = "b" },
                        new Token() { Column = 8, Kind = SyntaxKind.Slash, Line = 1, Literal = "/" },
                        new Token() { Column = 10, Kind = SyntaxKind.Identifier, Line = 1, Literal = "c" },
                        new Token() { Column = 11, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "a + b / c",
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
                            Expression = new InfixExpression
                            (
                                left: new IdentifierExpression("a"),
                                op: new Token() { Column = 4, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                right: new InfixExpression
                                (
                                    left: new IdentifierExpression("b"),
                                    op: new Token() { Column = 8, Kind = SyntaxKind.Slash, Line = 1, Literal = "/" },
                                    right: new IdentifierExpression("c")
                                )
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 5
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Identifier, Line = 1, Literal = "a" },
                        new Token() { Column = 4, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 6, Kind = SyntaxKind.Identifier, Line = 1, Literal = "b" },
                        new Token() { Column = 8, Kind = SyntaxKind.Slash, Line = 1, Literal = "/" },
                        new Token() { Column = 10, Kind = SyntaxKind.Identifier, Line = 1, Literal = "c" },
                        new Token() { Column = 11, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "a + b * c + d / e - f",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 11,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new InfixExpression
                            (
                                left: new InfixExpression
                                (
                                    left: new InfixExpression
                                    (
                                        left: new IdentifierExpression("a"),
                                        op: new Token() { Column = 4, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                        right: new InfixExpression
                                        (
                                            left: new IdentifierExpression("b"),
                                            op: new Token() { Column = 8, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                                            right: new IdentifierExpression("c")
                                        )
                                    ),
                                    op: new Token() { Column = 12, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                    right: new InfixExpression
                                    (
                                        left: new IdentifierExpression("d"),
                                        op: new Token() { Column = 16, Kind = SyntaxKind.Slash, Line = 1, Literal = "/" },
                                        right: new IdentifierExpression("e")
                                    )
                                ),
                                op: new Token() { Column = 20, Kind = SyntaxKind.Minus, Line = 1, Literal = "-" },
                                right: new IdentifierExpression("f")
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 11
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Identifier, Line = 1, Literal = "a" },
                        new Token() { Column = 4, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 6, Kind = SyntaxKind.Identifier, Line = 1, Literal = "b" },
                        new Token() { Column = 8, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                        new Token() { Column = 10, Kind = SyntaxKind.Identifier, Line = 1, Literal = "c" },
                        new Token() { Column = 12, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 14, Kind = SyntaxKind.Identifier, Line = 1, Literal = "d" },
                        new Token() { Column = 16, Kind = SyntaxKind.Slash, Line = 1, Literal = "/" },
                        new Token() { Column = 18, Kind = SyntaxKind.Identifier, Line = 1, Literal = "e" },
                        new Token() { Column = 20, Kind = SyntaxKind.Minus, Line = 1, Literal = "-" },
                        new Token() { Column = 22, Kind = SyntaxKind.Identifier, Line = 1, Literal = "f" },
                        new Token() { Column = 23, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "5 > 4 == 3 < 4",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 7,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new InfixExpression
                            (
                                left: new InfixExpression
                                (
                                    left: new IntegerExpression(5),
                                    op: new Token() { Column = 4, Kind = SyntaxKind.GreaterThan, Line = 1, Literal = ">" },
                                    right: new IntegerExpression(4)
                                ),
                                op: new Token() { Column = 8, Kind = SyntaxKind.Equal, Line = 1, Literal = "==" },
                                right: new InfixExpression
                                (
                                    left: new IntegerExpression(3),
                                    op: new Token() { Column = 13, Kind = SyntaxKind.LessThan, Line = 1, Literal = "<" },
                                    right: new IntegerExpression(4)
                                )
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 7
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 4, Kind = SyntaxKind.GreaterThan, Line = 1, Literal = ">" },
                        new Token() { Column = 6, Kind = SyntaxKind.Int, Line = 1, Literal = "4" },
                        new Token() { Column = 8, Kind = SyntaxKind.Equal, Line = 1, Literal = "==" },
                        new Token() { Column = 11, Kind = SyntaxKind.Int, Line = 1, Literal = "3" },
                        new Token() { Column = 13, Kind = SyntaxKind.LessThan, Line = 1, Literal = "<" },
                        new Token() { Column = 15, Kind = SyntaxKind.Int, Line = 1, Literal = "4" },
                        new Token() { Column = 16, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "5 < 4 != 3 > 4",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 7,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new InfixExpression
                            (
                                left: new InfixExpression
                                (
                                    left: new IntegerExpression(5),
                                    op: new Token() { Column = 4, Kind = SyntaxKind.LessThan, Line = 1, Literal = "<" },
                                    right: new IntegerExpression(4)
                                ),
                                op: new Token() { Column = 8, Kind = SyntaxKind.NotEqual, Line = 1, Literal = "!=" },
                                right: new InfixExpression
                                (
                                    left: new IntegerExpression(3),
                                    op: new Token() { Column = 13, Kind = SyntaxKind.GreaterThan, Line = 1, Literal = ">" },
                                    right: new IntegerExpression(4)
                                )
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 7
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 4, Kind = SyntaxKind.LessThan, Line = 1, Literal = "<" },
                        new Token() { Column = 6, Kind = SyntaxKind.Int, Line = 1, Literal = "4" },
                        new Token() { Column = 8, Kind = SyntaxKind.NotEqual, Line = 1, Literal = "!=" },
                        new Token() { Column = 11, Kind = SyntaxKind.Int, Line = 1, Literal = "3" },
                        new Token() { Column = 13, Kind = SyntaxKind.GreaterThan, Line = 1, Literal = ">" },
                        new Token() { Column = 15, Kind = SyntaxKind.Int, Line = 1, Literal = "4" },
                        new Token() { Column = 16, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "3 + 4 * 5 == 3 * 1 + 4 * 5",
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
                            Expression = new InfixExpression
                            (
                                left: new InfixExpression
                                (
                                    left: new IntegerExpression(3),
                                    op: new Token() { Column = 4, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                    right: new InfixExpression
                                    (
                                        left: new IntegerExpression(4),
                                        op: new Token() { Column = 8, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                                        right: new IntegerExpression(5)
                                    )
                                ),
                                op: new Token() { Column = 12, Kind = SyntaxKind.Equal, Line = 1, Literal = "==" },
                                right: new InfixExpression
                                (
                                    left: new InfixExpression
                                    (
                                        left: new IntegerExpression(3),
                                        op: new Token() { Column = 17, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                                        right: new IntegerExpression(1)
                                    ),
                                    op: new Token() { Column = 21, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                    right: new InfixExpression
                                    (
                                        left: new IntegerExpression(4),
                                        op: new Token() { Column = 25, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                                        right: new IntegerExpression(5)
                                    )
                                )
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 13
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Int, Line = 1, Literal = "3" },
                        new Token() { Column = 4, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 6, Kind = SyntaxKind.Int, Line = 1, Literal = "4" },
                        new Token() { Column = 8, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                        new Token() { Column = 10, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 12, Kind = SyntaxKind.Equal, Line = 1, Literal = "==" },
                        new Token() { Column = 15, Kind = SyntaxKind.Int, Line = 1, Literal = "3" },
                        new Token() { Column = 17, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                        new Token() { Column = 19, Kind = SyntaxKind.Int, Line = 1, Literal = "1" },
                        new Token() { Column = 21, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 23, Kind = SyntaxKind.Int, Line = 1, Literal = "4" },
                        new Token() { Column = 25, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                        new Token() { Column = 27, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 28, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "3 > 5 == false",
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
                            Expression = new InfixExpression
                            (
                                left: new InfixExpression
                                (
                                    left: new IntegerExpression(3),
                                    op: new Token() { Column = 4, Kind = SyntaxKind.GreaterThan, Line = 1, Literal = ">" },
                                    right: new IntegerExpression(5)
                                ),
                                op: new Token() { Column = 8, Kind = SyntaxKind.Equal, Line = 1, Literal = "==" },
                                right: new BooleanExpression(false)
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 5
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Int, Line = 1, Literal = "3" },
                        new Token() { Column = 4, Kind = SyntaxKind.GreaterThan, Line = 1, Literal = ">" },
                        new Token() { Column = 6, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 8, Kind = SyntaxKind.Equal, Line = 1, Literal = "==" },
                        new Token() { Column = 11, Kind = SyntaxKind.False, Line = 1, Literal = "false" },
                        new Token() { Column = 16, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "3 < 5 == true",
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
                            Expression = new InfixExpression
                            (
                                left: new InfixExpression
                                (
                                    left: new IntegerExpression(3),
                                    op: new Token() { Column = 4, Kind = SyntaxKind.LessThan, Line = 1, Literal = "<" },
                                    right: new IntegerExpression(5)
                                ),
                                op: new Token() { Column = 8, Kind = SyntaxKind.Equal, Line = 1, Literal = "==" },
                                right: new BooleanExpression(true)
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 5
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Int, Line = 1, Literal = "3" },
                        new Token() { Column = 4, Kind = SyntaxKind.LessThan, Line = 1, Literal = "<" },
                        new Token() { Column = 6, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 8, Kind = SyntaxKind.Equal, Line = 1, Literal = "==" },
                        new Token() { Column = 11, Kind = SyntaxKind.True, Line = 1, Literal = "true" },
                        new Token() { Column = 15, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "1 + (2 + 3) + 4",
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
                            Expression = new InfixExpression
                            (
                                left: new InfixExpression
                                (
                                    left: new IntegerExpression(1),
                                    op: new Token() { Column = 4, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                    right: new InfixExpression
                                    (
                                        left: new IntegerExpression(2),
                                        op: new Token() { Column = 9, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                        right: new IntegerExpression(3)
                                    )
                                ),
                                op: new Token() { Column = 14, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                right: new IntegerExpression(4)
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 9
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Int, Line = 1, Literal = "1" },
                        new Token() { Column = 4, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 6, Kind = SyntaxKind.LeftParenthesis, Line = 1, Literal = "(" },
                        new Token() { Column = 7, Kind = SyntaxKind.Int, Line = 1, Literal = "2" },
                        new Token() { Column = 9, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 11, Kind = SyntaxKind.Int, Line = 1, Literal = "3" },
                        new Token() { Column = 12, Kind = SyntaxKind.RightParenthesis, Line = 1, Literal = ")" },
                        new Token() { Column = 14, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 16, Kind = SyntaxKind.Int, Line = 1, Literal = "4" },
                        new Token() { Column = 17, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "(5 + 5) * 2",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 7,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new InfixExpression
                            (
                                left: new InfixExpression
                                (
                                    left: new IntegerExpression(5),
                                    op: new Token() { Column = 5, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                    right: new IntegerExpression(5)
                                ),
                                op: new Token() { Column = 10, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                                right: new IntegerExpression(2)
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 7
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.LeftParenthesis, Line = 1, Literal = "(" },
                        new Token() { Column = 3, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 5, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 7, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 8, Kind = SyntaxKind.RightParenthesis, Line = 1, Literal = ")" },
                        new Token() { Column = 10, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                        new Token() { Column = 12, Kind = SyntaxKind.Int, Line = 1, Literal = "2" },
                        new Token() { Column = 13, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "2 / (5 + 5)",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 7,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new InfixExpression
                            (
                                left: new IntegerExpression(2),
                                op: new Token() { Column = 4, Kind = SyntaxKind.Slash, Line = 1, Literal = "/" },
                                right: new InfixExpression
                                (
                                    left: new IntegerExpression(5),
                                    op: new Token() { Column = 9, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                    right: new IntegerExpression(5)
                                )
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 7
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Int, Line = 1, Literal = "2" },
                        new Token() { Column = 4, Kind = SyntaxKind.Slash, Line = 1, Literal = "/" },
                        new Token() { Column = 6, Kind = SyntaxKind.LeftParenthesis, Line = 1, Literal = "(" },
                        new Token() { Column = 7, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 9, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 11, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 12, Kind = SyntaxKind.RightParenthesis, Line = 1, Literal = ")" },
                        new Token() { Column = 13, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "-(5 + 5)",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 6,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new PrefixExpression
                            (
                                left: new InfixExpression(left: null, op: new Token() { Column = 2, Kind = SyntaxKind.Minus, Line = 1, Literal = "-" }, right: null),
                                right: new InfixExpression
                                (
                                    left: new IntegerExpression(5),
                                    op: new Token() { Column = 6, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                    right: new IntegerExpression(5)
                                )
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 6
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Minus, Line = 1, Literal = "-" },
                        new Token() { Column = 3, Kind = SyntaxKind.LeftParenthesis, Line = 1, Literal = "(" },
                        new Token() { Column = 4, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 6, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 8, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 9, Kind = SyntaxKind.RightParenthesis, Line = 1, Literal = ")" },
                        new Token() { Column = 10, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "!(true == true)",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 6,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new PrefixExpression
                            (
                                left: new InfixExpression(left: null, op: new Token()  { Column = 2, Kind = SyntaxKind.Bang, Line = 1, Literal = "!" }, right: null),
                                right: new InfixExpression
                                (
                                    left: new BooleanExpression(true),
                                    op: new Token() { Column = 9, Kind = SyntaxKind.Equal, Line = 1, Literal = "==" },
                                    right: new BooleanExpression(true)
                                )
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 6
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Bang, Line = 1, Literal = "!" },
                        new Token() { Column = 3, Kind = SyntaxKind.LeftParenthesis, Line = 1, Literal = "(" },
                        new Token() { Column = 4, Kind = SyntaxKind.True, Line = 1, Literal = "true" },
                        new Token() { Column = 9, Kind = SyntaxKind.Equal, Line = 1, Literal = "==" },
                        new Token() { Column = 12, Kind = SyntaxKind.True, Line = 1, Literal = "true" },
                        new Token() { Column = 16, Kind = SyntaxKind.RightParenthesis, Line = 1, Literal = ")" },
                        new Token() { Column = 17, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "a + add(b * c) + d",
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
                            Expression = new InfixExpression
                            (
                                left: new InfixExpression
                                (
                                    left: new IdentifierExpression("a"),
                                    op: new Token() { Column = 4, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                    right: new CallExpression
                                    (
                                        identifier: new Token() { Column = 6, Kind = SyntaxKind.Identifier, Line = 1, Literal = "add" },
                                        arguments: new List<Expression>
                                        {
                                            new InfixExpression
                                            (
                                                left: new IdentifierExpression("b"),
                                                op: new Token() { Column = 12, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                                                right: new IdentifierExpression("c")
                                            )
                                        },
                                        fn: null
                                    )
                                ),
                                op: new Token() { Column = 17, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                right: new IdentifierExpression("d")
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 10
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Identifier, Line = 1, Literal = "a" },
                        new Token() { Column = 4, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 6, Kind = SyntaxKind.Identifier, Line = 1, Literal = "add" },
                        new Token() { Column = 9, Kind = SyntaxKind.LeftParenthesis, Line = 1, Literal = "(" },
                        new Token() { Column = 10, Kind = SyntaxKind.Identifier, Line = 1, Literal = "b" },
                        new Token() { Column = 12, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                        new Token() { Column = 14, Kind = SyntaxKind.Identifier, Line = 1, Literal = "c" },
                        new Token() { Column = 15, Kind = SyntaxKind.RightParenthesis, Line = 1, Literal = ")" },
                        new Token() { Column = 17, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 19, Kind = SyntaxKind.Identifier, Line = 1, Literal = "d" },
                        new Token() { Column = 20, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "add(a, b, 1, 2 * 3, 4 + 5, add(6, 7 * 8))",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 25,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new CallExpression
                            (
                                identifier: new Token() { Column = 2, Kind = SyntaxKind.Identifier, Line = 1, Literal = "add" },
                                arguments: new List<Expression>
                                {
                                    new IdentifierExpression("a"),
                                    new IdentifierExpression("b"),
                                    new IntegerExpression(1),
                                    new InfixExpression
                                    (
                                        left: new IntegerExpression(2),
                                        op: new Token() { Column = 17, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                                        right: new IntegerExpression(3)
                                    ),
                                    new InfixExpression
                                    (
                                        left: new IntegerExpression(4),
                                        op: new Token() { Column = 24, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                        right: new IntegerExpression(5)
                                    ),
                                    new CallExpression
                                    (
                                        identifier: new Token() { Column = 29, Kind = SyntaxKind.Identifier, Line = 1, Literal = "add" },
                                        arguments: new List<Expression>
                                        {
                                            new IntegerExpression(6),
                                            new InfixExpression
                                            (
                                                left: new IntegerExpression(7),
                                                op: new Token() { Column = 38, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                                                right: new IntegerExpression(8)
                                            )
                                        },
                                        fn: null
                                    )
                                },
                                fn: null
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 25
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Identifier, Line = 1, Literal = "add" },
                        new Token() { Column = 5, Kind = SyntaxKind.LeftParenthesis, Line = 1, Literal = "(" },
                        new Token() { Column = 6, Kind = SyntaxKind.Identifier, Line = 1, Literal = "a" },
                        new Token() { Column = 7, Kind = SyntaxKind.Comma, Line = 1, Literal = "," },
                        new Token() { Column = 9, Kind = SyntaxKind.Identifier, Line = 1, Literal = "b" },
                        new Token() { Column = 10, Kind = SyntaxKind.Comma, Line = 1, Literal = "," },
                        new Token() { Column = 12, Kind = SyntaxKind.Int, Line = 1, Literal = "1" },
                        new Token() { Column = 13, Kind = SyntaxKind.Comma, Line = 1, Literal = "," },
                        new Token() { Column = 15, Kind = SyntaxKind.Int, Line = 1, Literal = "2" },
                        new Token() { Column = 17, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                        new Token() { Column = 19, Kind = SyntaxKind.Int, Line = 1, Literal = "3" },
                        new Token() { Column = 20, Kind = SyntaxKind.Comma, Line = 1, Literal = "," },
                        new Token() { Column = 22, Kind = SyntaxKind.Int, Line = 1, Literal = "4" },
                        new Token() { Column = 24, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 26, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 27, Kind = SyntaxKind.Comma, Line = 1, Literal = "," },
                        new Token() { Column = 29, Kind = SyntaxKind.Identifier, Line = 1, Literal = "add" },
                        new Token() { Column = 32, Kind = SyntaxKind.LeftParenthesis, Line = 1, Literal = "(" },
                        new Token() { Column = 33, Kind = SyntaxKind.Int, Line = 1, Literal = "6" },
                        new Token() { Column = 34, Kind = SyntaxKind.Comma, Line = 1, Literal = "," },
                        new Token() { Column = 36, Kind = SyntaxKind.Int, Line = 1, Literal = "7" },
                        new Token() { Column = 38, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                        new Token() { Column = 40, Kind = SyntaxKind.Int, Line = 1, Literal = "8" },
                        new Token() { Column = 41, Kind = SyntaxKind.RightParenthesis, Line = 1, Literal = ")" },
                        new Token() { Column = 42, Kind = SyntaxKind.RightParenthesis, Line = 1, Literal = ")" },
                        new Token() { Column = 43, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "add(a + b + c * d / f + g)",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 14,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new CallExpression
                            (
                                identifier: new Token() { Column = 2, Kind = SyntaxKind.Identifier, Line = 1, Literal = "add" },
                                arguments: new List<Expression>
                                {
                                    new InfixExpression
                                    (
                                        left: new InfixExpression
                                        (
                                            left: new InfixExpression
                                            (
                                                left: new IdentifierExpression("a"),
                                                op: new Token() { Column = 8, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                                right: new IdentifierExpression("b")
                                            ),
                                            op: new Token() { Column = 12, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                            right: new InfixExpression
                                            (
                                                left: new InfixExpression
                                                (
                                                    left: new IdentifierExpression("c"),
                                                    op: new Token() { Column = 16, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                                                    right: new IdentifierExpression("d")
                                                ),
                                                op: new Token() { Column = 20, Kind = SyntaxKind.Slash, Line = 1, Literal = "/" },
                                                right: new IdentifierExpression("f")
                                            )
                                        ),
                                        op: new Token() { Column = 24, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                        right: new IdentifierExpression("g")
                                    )
                                },
                                fn: null
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 14
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Identifier, Line = 1, Literal = "add" },
                        new Token() { Column = 5, Kind = SyntaxKind.LeftParenthesis, Line = 1, Literal = "(" },
                        new Token() { Column = 6, Kind = SyntaxKind.Identifier, Line = 1, Literal = "a" },
                        new Token() { Column = 8, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 10, Kind = SyntaxKind.Identifier, Line = 1, Literal = "b" },
                        new Token() { Column = 12, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 14, Kind = SyntaxKind.Identifier, Line = 1, Literal = "c" },
                        new Token() { Column = 16, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                        new Token() { Column = 18, Kind = SyntaxKind.Identifier, Line = 1, Literal = "d" },
                        new Token() { Column = 20, Kind = SyntaxKind.Slash, Line = 1, Literal = "/" },
                        new Token() { Column = 22, Kind = SyntaxKind.Identifier, Line = 1, Literal = "f" },
                        new Token() { Column = 24, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 26, Kind = SyntaxKind.Identifier, Line = 1, Literal = "g" },
                        new Token() { Column = 27, Kind = SyntaxKind.RightParenthesis, Line = 1, Literal = ")" },
                        new Token() { Column = 28, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            }
        };
    }
}
