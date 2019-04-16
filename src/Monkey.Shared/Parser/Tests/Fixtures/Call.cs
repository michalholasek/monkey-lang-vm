using System.Collections.Generic;

using Monkey.Shared;
using Monkey.Shared.Parser;
using Monkey.Shared.Parser.Ast;
using Monkey.Shared.Scanner;

namespace Monkey.Tests.Fixtures
{
    public static partial class Expressions
    {
        internal static Dictionary<string, Program> Call = new Dictionary<string, Program>
        {
            {
                "add(1, 2 * 3, 4 + 5)",
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
                            Expression = new CallExpression
                            (
                                identifier: new Token() { Column = 2, Kind = SyntaxKind.Identifier, Line = 1, Literal = "add" },
                                arguments: new List<Expression>
                                {
                                    new IntegerExpression(1),
                                    new InfixExpression
                                    (
                                        left: new IntegerExpression(2),
                                        op: new Token() { Column = 11, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                                        right: new IntegerExpression(3)
                                    ),
                                    new InfixExpression
                                    (
                                        left: new IntegerExpression(4),
                                        op: new Token() { Column = 18, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                        right: new IntegerExpression(5)
                                    )
                                },
                                fn: null
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 12
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Identifier, Line = 1, Literal = "add" },
                        new Token() { Column = 5, Kind = SyntaxKind.LeftParenthesis, Line = 1, Literal = "(" },
                        new Token() { Column = 6, Kind = SyntaxKind.Int, Line = 1, Literal = "1" },
                        new Token() { Column = 7, Kind = SyntaxKind.Comma, Line = 1, Literal = "," },
                        new Token() { Column = 9, Kind = SyntaxKind.Int, Line = 1, Literal = "2" },
                        new Token() { Column = 11, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
                        new Token() { Column = 13, Kind = SyntaxKind.Int, Line = 1, Literal = "3" },
                        new Token() { Column = 14, Kind = SyntaxKind.Comma, Line = 1, Literal = "," },
                        new Token() { Column = 16, Kind = SyntaxKind.Int, Line = 1, Literal = "4" },
                        new Token() { Column = 18, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 20, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 21, Kind = SyntaxKind.RightParenthesis, Line = 1, Literal = ")" },
                        new Token() { Column = 22, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "fn(x) { x; }(5)",
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
                            Expression = new CallExpression
                            (
                                identifier: null,
                                arguments: new List<Expression> { new IntegerExpression(5) },
                                fn: new FunctionExpression
                                (
                                    parameters: new List<Token> { new Token() { Column = 5, Kind = SyntaxKind.Identifier, Line = 1, Literal = "x" } },
                                    body: new BlockStatement
                                    {
                                        Statements = new List<Statement>
                                        {
                                            new Statement(new StatementOptions
                                            {
                                                Expression = new IdentifierExpression("x"),
                                                Identifier = null,
                                                Kind = NodeKind.Expression,
                                                Position = 5,
                                                Range = 2
                                            })
                                        }
                                    }
                                )
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 11
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Function, Line = 1, Literal = "fn" },
                        new Token() { Column = 4, Kind = SyntaxKind.LeftParenthesis, Line = 1, Literal = "(" },
                        new Token() { Column = 5, Kind = SyntaxKind.Identifier, Line = 1, Literal = "x" },
                        new Token() { Column = 6, Kind = SyntaxKind.RightParenthesis, Line = 1, Literal = ")" },
                        new Token() { Column = 8, Kind = SyntaxKind.LeftBrace, Line = 1, Literal = "{" },
                        new Token() { Column = 10, Kind = SyntaxKind.Identifier, Line = 1, Literal = "x" },
                        new Token() { Column = 11, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 13, Kind = SyntaxKind.RightBrace, Line = 1, Literal = "}" },
                        new Token() { Column = 14, Kind = SyntaxKind.LeftParenthesis, Line = 1, Literal = "(" },
                        new Token() { Column = 15, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
                        new Token() { Column = 16, Kind = SyntaxKind.RightParenthesis, Line = 1, Literal = ")" },
                        new Token() { Column = 17, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "fortyTwo([])",
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
                            Expression = new CallExpression
                            (
                                identifier: new Token() { Column = 2, Kind = SyntaxKind.Identifier, Line = 1, Literal = "fortyTwo" },
                                arguments: new List<Expression>
                                {
                                    new ArrayExpression(new List<Expression>())
                                },
                                fn: null
                            ),
                            Identifier = null,
                            Kind = NodeKind.Expression,
                            Position = 0,
                            Range = 5
                        })
                    },
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Identifier, Line = 1, Literal = "fortyTwo" },
                        new Token() { Column = 10, Kind = SyntaxKind.LeftParenthesis, Line = 1, Literal = "(" },
                        new Token() { Column = 11, Kind = SyntaxKind.LeftBracket, Line = 1, Literal = "[" },
                        new Token() { Column = 12, Kind = SyntaxKind.RightBracket, Line = 1, Literal = "]" },
                        new Token() { Column = 13, Kind = SyntaxKind.RightParenthesis, Line = 1, Literal = ")" },
                        new Token() { Column = 14, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            }
        };
    }
}
