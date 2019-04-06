using System.Collections.Generic;

using Monkey.Shared.Parser;
using Monkey.Shared.Parser.Ast;
using Monkey.Shared.Scanner;

namespace Monkey.Tests.Fixtures
{
    public static partial class Expressions
    {
        internal static Dictionary<string, Program> Prefix = new Dictionary<string, Program>
        {
            {
                "!5",
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
                            Expression = new PrefixExpression
                            (
                                new InfixExpression
                                (
                                    left: null,
                                    op: new Token() { Column = 2, Kind = SyntaxKind.Bang, Line = 1, Literal = "!" },
                                    right: null
                                ),
                                new IntegerExpression(5)
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 2
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Bang, Line = 1, Literal = "!" },
                        new Token() { Column = 3, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 4, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "-15",
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
                            Expression = new PrefixExpression
                            (
                                new InfixExpression
                                (
                                    left: null,
                                    op: new Token() { Column = 2, Kind = SyntaxKind.Minus, Line = 1, Literal = "-" },
                                    right: null
                                ),
                                new IntegerExpression(15)
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 2
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Minus, Line = 1, Literal = "-" },
                        new Token() { Column = 3, Kind = SyntaxKind.Int, Line = 1, Literal = "15" },
                        new Token() { Column = 5, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "!true",
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
                            Expression = new PrefixExpression
                            (
                                new InfixExpression
                                (
                                    left: null,
                                    op: new Token() { Column = 2, Kind = SyntaxKind.Bang, Line = 1, Literal = "!" },
                                    right: null
                                ),
                                new BooleanExpression(true)
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 2
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Bang, Line = 1, Literal = "!" },
                        new Token() { Column = 3, Kind = SyntaxKind.True, Line = 1, Literal = "true" },
                        new Token() { Column = 7, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "!false",
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
                            Expression = new PrefixExpression
                            (
                                new InfixExpression
                                (
                                    left: null,
                                    op: new Token() { Column = 2, Kind = SyntaxKind.Bang, Line = 1, Literal = "!" },
                                    right: null
                                ),
                                new BooleanExpression(false)
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 2
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Bang, Line = 1, Literal = "!" },
                        new Token() { Column = 3, Kind = SyntaxKind.False, Line = 1, Literal = "false" },
                        new Token() { Column = 8, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            }
        };
    }
}
