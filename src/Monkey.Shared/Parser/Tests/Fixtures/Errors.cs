using System.Collections.Generic;

using Monkey.Shared;

namespace Monkey.Tests.Fixtures
{
    public static partial class Expressions
    {
        internal static Dictionary<string, Program> Errors = new Dictionary<string, Program>
        {
            {
                "...",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>
                    {
                        new AssertionError { Message = "invalid token: .<-- . ." }
                    },
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 3,
                    Statements = new List<Statement> {},
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Illegal, Line = 1, Literal = "." },
                        new Token() { Column = 3, Kind = SyntaxKind.Illegal, Line = 1, Literal = "." },
                        new Token() { Column = 4, Kind = SyntaxKind.Illegal, Line = 1, Literal = "." },
                        new Token() { Column = 5, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "1 +",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>
                    {
                        new AssertionError { Message = "missing token: 1 + <expression><--, expected expression" }
                    },
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 2,
                    Statements = new List<Statement> {},
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Int, Line = 1, Literal = "1" },
                        new Token() { Column = 4, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 5, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "1 + $",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>
                    {
                        new AssertionError { Message = "invalid token: 1 + $<--" }
                    },
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 3,
                    Statements = new List<Statement> {},
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Int, Line = 1, Literal = "1" },
                        new Token() { Column = 4, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                        new Token() { Column = 6, Kind = SyntaxKind.Illegal, Line = 1, Literal = "$" },
                        new Token() { Column = 7, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "let",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>
                    {
                        new AssertionError { Message = "missing token: let <identifier><-- = <expression>;" }
                    },
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 1,
                    Statements = new List<Statement> {},
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Let, Line = 1, Literal = "let" },
                        new Token() { Column = 5, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "let one",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>
                    {
                        new AssertionError { Message = "missing token: let one <assign><-- <expression>;" }
                    },
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 2,
                    Statements = new List<Statement> {},
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Let, Line = 1, Literal = "let" },
                        new Token() { Column = 6, Kind = SyntaxKind.Identifier, Line = 1, Literal = "one" },
                        new Token() { Column = 9, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "let one =",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>
                    {
                        new AssertionError { Message = "missing token: let one = <expression><--, expected expression" }
                    },
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 3,
                    Statements = new List<Statement> {},
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Let, Line = 1, Literal = "let" },
                        new Token() { Column = 6, Kind = SyntaxKind.Identifier, Line = 1, Literal = "one" },
                        new Token() { Column = 10, Kind = SyntaxKind.Assign, Line = 1, Literal = "=" },
                        new Token() { Column = 11, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "let one = $;",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>
                    {
                        new AssertionError { Message = "invalid token: let one = $<-- ;" }
                    },
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 5,
                    Statements = new List<Statement> {},
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Let, Line = 1, Literal = "let" },
                        new Token() { Column = 6, Kind = SyntaxKind.Identifier, Line = 1, Literal = "one" },
                        new Token() { Column = 10, Kind = SyntaxKind.Assign, Line = 1, Literal = "=" },
                        new Token() { Column = 12, Kind = SyntaxKind.Illegal, Line = 1, Literal = "$" },
                        new Token() { Column = 13, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 14, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "let 1 = 1;",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>
                    {
                        new AssertionError { Message = "invalid token: let 1<-- = 1;" }
                    },
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 5,
                    Statements = new List<Statement> {},
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Let, Line = 1, Literal = "let" },
                        new Token() { Column = 6, Kind = SyntaxKind.Int, Line = 1, Literal = "1" },
                        new Token() { Column = 8, Kind = SyntaxKind.Assign, Line = 1, Literal = "=" },
                        new Token() { Column = 10, Kind = SyntaxKind.Int, Line = 1, Literal = "1" },
                        new Token() { Column = 11, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 12, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "let one . 1;",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>
                    {
                        new AssertionError { Message = "invalid token: let one .<-- 1;" }
                    },
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 5,
                    Statements = new List<Statement> {},
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Let, Line = 1, Literal = "let" },
                        new Token() { Column = 6, Kind = SyntaxKind.Identifier, Line = 1, Literal = "one" },
                        new Token() { Column = 10, Kind = SyntaxKind.Illegal, Line = 1, Literal = "." },
                        new Token() { Column = 12, Kind = SyntaxKind.Int, Line = 1, Literal = "1" },
                        new Token() { Column = 13, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 14, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "return $;",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>
                    {
                        new AssertionError { Message = "invalid token: return $<-- ;" }
                    },
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 3,
                    Statements = new List<Statement> {},
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Return, Line = 1, Literal = "return" },
                        new Token() { Column = 9, Kind = SyntaxKind.Illegal, Line = 1, Literal = "$" },
                        new Token() { Column = 10, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 11, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "[1 2];",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>
                    {
                        new AssertionError { Message = "invalid token: [ 1 <comma><-- 2 ];, missing comma" }
                    },
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 5,
                    Statements = new List<Statement> {},
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.LeftBracket, Line = 1, Literal = "[" },
                        new Token() { Column = 3, Kind = SyntaxKind.Int, Line = 1, Literal = "1" },
                        new Token() { Column = 5, Kind = SyntaxKind.Int, Line = 1, Literal = "2" },
                        new Token() { Column = 6, Kind = SyntaxKind.RightBracket, Line = 1, Literal = "]" },
                        new Token() { Column = 7, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 8, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "[ , 2];",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>
                    {
                        new AssertionError { Message = "missing token: [ <expression><-- , 2 ];, expected expression" }
                    },
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 5,
                    Statements = new List<Statement> {},
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.LeftBracket, Line = 1, Literal = "[" },
                        new Token() { Column = 4, Kind = SyntaxKind.Comma, Line = 1, Literal = "," },
                        new Token() { Column = 6, Kind = SyntaxKind.Int, Line = 1, Literal = "2" },
                        new Token() { Column = 7, Kind = SyntaxKind.RightBracket, Line = 1, Literal = "]" },
                        new Token() { Column = 8, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 9, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "[1, 2",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>
                    {
                        new AssertionError { Message = "missing token: [ 1 , 2 <bracket><--, expected RightBracket" }
                    },
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 4,
                    Statements = new List<Statement> {},
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.LeftBracket, Line = 1, Literal = "[" },
                        new Token() { Column = 3, Kind = SyntaxKind.Int, Line = 1, Literal = "1" },
                        new Token() { Column = 4, Kind = SyntaxKind.Comma, Line = 1, Literal = "," },
                        new Token() { Column = 6, Kind = SyntaxKind.Int, Line = 1, Literal = "2" },
                        new Token() { Column = 7, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "{ 1$ 2 };",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>
                    {
                        new AssertionError { Message = "invalid token: { 1 $<-- 2 };, expected colon" }
                    },
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 6,
                    Statements = new List<Statement> {},
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.LeftBrace, Line = 1, Literal = "{" },
                        new Token() { Column = 4, Kind = SyntaxKind.Int, Line = 1, Literal = "1" },
                        new Token() { Column = 5, Kind = SyntaxKind.Illegal, Line = 1, Literal = "$" },
                        new Token() { Column = 7, Kind = SyntaxKind.Int, Line = 1, Literal = "2" },
                        new Token() { Column = 9, Kind = SyntaxKind.RightBrace, Line = 1, Literal = "}" },
                        new Token() { Column = 10, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 11, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "{ 1: 2 $ 2: 3 };",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>
                    {
                        new AssertionError { Message = "invalid token: { 1 : 2 $<-- 2 : 3 };, expected comma" }
                    },
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 10,
                    Statements = new List<Statement> {},
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.LeftBrace, Line = 1, Literal = "{" },
                        new Token() { Column = 4, Kind = SyntaxKind.Int, Line = 1, Literal = "1" },
                        new Token() { Column = 5, Kind = SyntaxKind.Colon, Line = 1, Literal = ":" },
                        new Token() { Column = 7, Kind = SyntaxKind.Int, Line = 1, Literal = "2" },
                        new Token() { Column = 9, Kind = SyntaxKind.Illegal, Line = 1, Literal = "$" },
                        new Token() { Column = 11, Kind = SyntaxKind.Int, Line = 1, Literal = "2" },
                        new Token() { Column = 12, Kind = SyntaxKind.Colon, Line = 1, Literal = ":" },
                        new Token() { Column = 14, Kind = SyntaxKind.Int, Line = 1, Literal = "3" },
                        new Token() { Column = 16, Kind = SyntaxKind.RightBrace, Line = 1, Literal = "}" },
                        new Token() { Column = 17, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
                        new Token() { Column = 18, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            },
            {
                "{ 1: 2",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>
                    {
                        new AssertionError { Message = "missing token: { 1 : 2 <brace><--, expected RightBrace" }
                    },
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 4,
                    Statements = new List<Statement> {},
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.LeftBrace, Line = 1, Literal = "{" },
                        new Token() { Column = 4, Kind = SyntaxKind.Int, Line = 1, Literal = "1" },
                        new Token() { Column = 5, Kind = SyntaxKind.Colon, Line = 1, Literal = ":" },
                        new Token() { Column = 7, Kind = SyntaxKind.Int, Line = 1, Literal = "2" },
                        new Token() { Column = 8, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            }
        };
    }
}
