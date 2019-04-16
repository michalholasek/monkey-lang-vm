using System.Collections.Generic;

using Monkey.Shared;
using Monkey.Shared.Parser;
using Monkey.Shared.Parser.Ast;
using Monkey.Shared.Scanner;

namespace Monkey.Tests.Fixtures
{
    public static partial class Expressions
    {
        internal static Dictionary<string, Program> Integer = new Dictionary<string, Program>
        {
            {
                "42",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 1,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new IntegerExpression(42),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 1
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Int, Line = 1, Literal = "42" },
                        new Token() { Column = 4, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            }
        };

        internal static Dictionary<string, Program> String = new Dictionary<string, Program>
        {
            {
                "\"foo bar\"",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 1,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new StringExpression("foo bar"),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 1
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.String, Line = 1, Literal = "foo bar" },
                        new Token() { Column = 11, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            }
        };

        internal static Dictionary<string, Program> Identifier = new Dictionary<string, Program>
        {
            {
                "foobar",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>(),
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 1,
                    Statements = new List<Statement>
                    {
                        new Statement(new StatementOptions
                        {
                            Expression = new IdentifierExpression("foobar"),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 1
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Identifier, Line = 1, Literal = "foobar" },
                        new Token() { Column = 8, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            }
        };
    }
}
