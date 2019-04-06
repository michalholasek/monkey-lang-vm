using System.Collections.Generic;

using Monkey.Shared.Parser;
using Monkey.Shared.Parser.Ast;
using Monkey.Shared.Scanner;

namespace Monkey.Tests.Fixtures
{
    public static partial class Expressions
    {
        internal static Dictionary<string, Program> Array = new Dictionary<string, Program>
        {
            {
                "[]",
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
                            Expression = new ArrayExpression(new List<Expression>()),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 2
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.LeftBracket, Line = 1, Literal = "[" },
                        new Token() { Column = 3, Kind = SyntaxKind.RightBracket, Line = 1, Literal = "]" },
                        new Token() { Column = 4, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "[1, 2 * 2, 3 + 3]",
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
                            Expression = new ArrayExpression(new List<Expression>
                            {
                                new IntegerExpression(1),
                                new InfixExpression
                                (
                                    left: new IntegerExpression(2),
                                    op: new Token() { Column = 8, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                                    right: new IntegerExpression(2)
                                ),
                                new InfixExpression
                                (
                                    left: new IntegerExpression(3),
                                    op: new Token() { Column = 15, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                    right: new IntegerExpression(3)
                                )
                            }),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 11
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.LeftBracket, Line = 1, Literal = "[" },
                        new Token() { Column = 3, Kind = SyntaxKind.Int, Line = 1, Literal = "1" },
                        new Token() { Column = 4, Kind = SyntaxKind.Comma, Line = 1, Literal = "," },
                        new Token() { Column = 6, Kind = SyntaxKind.Int, Line = 1, Literal = "2" },
                        new Token() { Column = 8, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                        new Token() { Column = 10, Kind = SyntaxKind.Int, Line = 1, Literal = "2" },
                        new Token() { Column = 11, Kind = SyntaxKind.Comma, Line = 1, Literal = "," },
                        new Token() { Column = 13, Kind = SyntaxKind.Int, Line = 1, Literal = "3" },
                        new Token() { Column = 15, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 17, Kind = SyntaxKind.Int, Line = 1, Literal = "3" },
                        new Token() { Column = 18, Kind = SyntaxKind.RightBracket, Line = 1, Literal = "]" },
                        new Token() { Column = 19, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "myArray[1 + 1]",
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
                            Expression = new IndexExpression
                            (
                                left: new IdentifierExpression("myArray"),
                                index: new InfixExpression
                                (
                                    left: new IntegerExpression(1),
                                    op: new Token() { Column = 12, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                    right: new IntegerExpression(1)
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
                        new Token() { Column = 2, Kind = SyntaxKind.Identifier, Line = 1, Literal = "myArray" },
                        new Token() { Column = 9, Kind = SyntaxKind.LeftBracket, Line = 1, Literal = "[" },
                        new Token() { Column = 10, Kind = SyntaxKind.Int, Line = 1, Literal = "1" },
                        new Token() { Column = 12, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 14, Kind = SyntaxKind.Int, Line = 1, Literal = "1" },
                        new Token() { Column = 15, Kind = SyntaxKind.RightBracket, Line = 1, Literal = "]" },
                        new Token() { Column = 16, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            }
        };
    }
}
