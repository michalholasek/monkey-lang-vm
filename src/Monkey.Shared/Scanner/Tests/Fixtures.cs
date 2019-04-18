using System.Collections.Generic;

using Monkey.Shared;

namespace Monkey.Tests.Fixtures
{
    public static class Tokens
    {
        public static List<Token> NonLetter = new List<Token>()
        {
            new Token() { Column = 2, Kind = SyntaxKind.Assign, Line = 1, Literal = "=" },
            new Token() { Column = 3, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
            new Token() { Column = 4, Kind = SyntaxKind.LeftParenthesis, Line = 1, Literal = "(" },
            new Token() { Column = 5, Kind = SyntaxKind.RightParenthesis, Line = 1, Literal = ")" },
            new Token() { Column = 6, Kind = SyntaxKind.LeftBrace, Line = 1, Literal = "{" },
            new Token() { Column = 7, Kind = SyntaxKind.RightBrace, Line = 1, Literal = "}" },
            new Token() { Column = 8, Kind = SyntaxKind.Comma, Line = 1, Literal = "," },
            new Token() { Column = 9, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
            new Token() { Column = 10, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
        };

        public static List<Token> Statements = new List<Token>()
        {
            new Token() { Column = 18, Kind = SyntaxKind.Let, Line = 2, Literal = "let" },
            new Token() { Column = 22, Kind = SyntaxKind.Identifier, Line = 2, Literal = "five" },
            new Token() { Column = 27, Kind = SyntaxKind.Assign, Line = 2, Literal = "=" },
            new Token() { Column = 29, Kind = SyntaxKind.Int, Line = 2, Literal = "5" },
            new Token() { Column = 30, Kind = SyntaxKind.Semicolon, Line = 2, Literal = ";" },
            new Token() { Column = 18, Kind = SyntaxKind.Let, Line = 3, Literal = "let" },
            new Token() { Column = 22, Kind = SyntaxKind.Identifier, Line = 3, Literal = "ten" },
            new Token() { Column = 26, Kind = SyntaxKind.Assign, Line = 3, Literal = "=" },
            new Token() { Column = 28, Kind = SyntaxKind.Int, Line = 3, Literal = "10" },
            new Token() { Column = 30, Kind = SyntaxKind.Semicolon, Line = 3, Literal = ";" },
            new Token() { Column = 18, Kind = SyntaxKind.Let, Line = 4, Literal = "let" },
            new Token() { Column = 22, Kind = SyntaxKind.Identifier, Line = 4, Literal = "add" },
            new Token() { Column = 26, Kind = SyntaxKind.Assign, Line = 4, Literal = "=" },
            new Token() { Column = 28, Kind = SyntaxKind.Function, Line = 4, Literal = "fn" },
            new Token() { Column = 30, Kind = SyntaxKind.LeftParenthesis, Line = 4, Literal = "(" },
            new Token() { Column = 31, Kind = SyntaxKind.Identifier, Line = 4, Literal = "x" },
            new Token() { Column = 32, Kind = SyntaxKind.Comma, Line = 4, Literal = "," },
            new Token() { Column = 34, Kind = SyntaxKind.Identifier, Line = 4, Literal = "y" },
            new Token() { Column = 35, Kind = SyntaxKind.RightParenthesis, Line = 4, Literal = ")" },
            new Token() { Column = 37, Kind = SyntaxKind.LeftBrace, Line = 4, Literal = "{" },
            new Token() { Column = 22, Kind = SyntaxKind.Identifier, Line = 5, Literal = "x" },
            new Token() { Column = 24, Kind = SyntaxKind.Plus, Line = 5, Literal = "+" },
            new Token() { Column = 26, Kind = SyntaxKind.Identifier, Line = 5, Literal = "y" },
            new Token() { Column = 27, Kind = SyntaxKind.Semicolon, Line = 5, Literal = ";" },
            new Token() { Column = 18, Kind = SyntaxKind.RightBrace, Line = 6, Literal = "}" },
            new Token() { Column = 19, Kind = SyntaxKind.Semicolon, Line = 6, Literal = ";" },
            new Token() { Column = 18, Kind = SyntaxKind.Let, Line = 7, Literal = "let" },
            new Token() { Column = 22, Kind = SyntaxKind.Identifier, Line = 7, Literal = "_result" },
            new Token() { Column = 30, Kind = SyntaxKind.Assign, Line = 7, Literal = "=" },
            new Token() { Column = 32, Kind = SyntaxKind.Identifier, Line = 7, Literal = "add" },
            new Token() { Column = 35, Kind = SyntaxKind.LeftParenthesis, Line = 7, Literal = "(" },
            new Token() { Column = 36, Kind = SyntaxKind.Identifier, Line = 7, Literal = "five" },
            new Token() { Column = 40, Kind = SyntaxKind.Comma, Line = 7, Literal = "," },
            new Token() { Column = 42, Kind = SyntaxKind.Identifier, Line = 7, Literal = "ten" },
            new Token() { Column = 45, Kind = SyntaxKind.RightParenthesis, Line = 7, Literal = ")" },
            new Token() { Column = 46, Kind = SyntaxKind.Semicolon, Line = 7, Literal = ";" },
            new Token() { Column = 14, Kind = SyntaxKind.EOF, Line = 8, Literal = "" }
        };

        public static List<Token> Operators = new List<Token>()
        {
            new Token() { Column = 2, Kind = SyntaxKind.Bang, Line = 1, Literal = "!" },
            new Token() { Column = 4, Kind = SyntaxKind.Minus, Line = 1, Literal = "-" },
            new Token() { Column = 6, Kind = SyntaxKind.Slash, Line = 1, Literal = "/" },
            new Token() { Column = 8, Kind = SyntaxKind.Asterisk, Line = 1, Literal = "*" },
            new Token() { Column = 10, Kind = SyntaxKind.LessThan, Line = 1, Literal = "<" },
            new Token() { Column = 12, Kind = SyntaxKind.GreaterThan, Line = 1, Literal = ">" },
            new Token() { Column = 13, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
        };

        public static List<Token> Keywords = new List<Token>()
        {
            new Token() { Column = 2, Kind = SyntaxKind.True, Line = 1, Literal = "true" },
            new Token() { Column = 7, Kind = SyntaxKind.False, Line = 1, Literal = "false" },
            new Token() { Column = 13, Kind = SyntaxKind.If, Line = 1, Literal = "if" },
            new Token() { Column = 16, Kind = SyntaxKind.Else, Line = 1, Literal = "else" },
            new Token() { Column = 21, Kind = SyntaxKind.Function, Line = 1, Literal = "fn" },
            new Token() { Column = 24, Kind = SyntaxKind.Let, Line = 1, Literal = "let" },
            new Token() { Column = 28, Kind = SyntaxKind.Return, Line = 1, Literal = "return" },
            new Token() { Column = 34, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
        };

        public static List<Token> StickyOperators = new List<Token>()
        {
            new Token() { Column = 18, Kind = SyntaxKind.Int, Line = 2, Literal = "10" },
            new Token() { Column = 21, Kind = SyntaxKind.Equal, Line = 2, Literal = "==" },
            new Token() { Column = 24, Kind = SyntaxKind.Int, Line = 2, Literal = "10" },
            new Token() { Column = 26, Kind = SyntaxKind.Semicolon, Line = 2, Literal = ";" },
            new Token() { Column = 18, Kind = SyntaxKind.Int, Line = 3, Literal = "10" },
            new Token() { Column = 21, Kind = SyntaxKind.NotEqual, Line = 3, Literal = "!=" },
            new Token() { Column = 24, Kind = SyntaxKind.Int, Line = 3, Literal = "9" },
            new Token() { Column = 25, Kind = SyntaxKind.Semicolon, Line = 3, Literal = ";" },
            new Token() { Column = 14, Kind = SyntaxKind.EOF, Line = 4, Literal = "" }
        };

        public static List<Token> PrefixOperators = new List<Token>()
        {
            new Token() { Column = 2, Kind = SyntaxKind.Bang, Line = 1, Literal = "!" },
            new Token() { Column = 3, Kind = SyntaxKind.Int, Line = 1, Literal = "5" },
            new Token() { Column = 4, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
            new Token() { Column = 5, Kind = SyntaxKind.Bang, Line = 1, Literal = "!" },
            new Token() { Column = 6, Kind = SyntaxKind.True, Line = 1, Literal = "true" },
            new Token() { Column = 10, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
            new Token() { Column = 11, Kind = SyntaxKind.Bang, Line = 1, Literal = "!" },
            new Token() { Column = 12, Kind = SyntaxKind.Bang, Line = 1, Literal = "!" },
            new Token() { Column = 13, Kind = SyntaxKind.True, Line = 1, Literal = "true" },
            new Token() { Column = 17, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
            new Token() { Column = 18, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
        };

        public static List<Token> Strings = new List<Token>()
        {
            new Token() { Column = 2, Kind = SyntaxKind.String, Line = 1, Literal = "foo bar" },
            new Token() { Column = 11, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
        };

        public static List<Token> Arrays = new List<Token>()
        {
            new Token() { Column = 2, Kind = SyntaxKind.LeftBracket, Line = 1, Literal = "[" },
            new Token() { Column = 3, Kind = SyntaxKind.Int, Line = 1, Literal = "1" },
            new Token() { Column = 4, Kind = SyntaxKind.Comma, Line = 1, Literal = "," },
            new Token() { Column = 6, Kind = SyntaxKind.Int, Line = 1, Literal = "2" },
            new Token() { Column = 7, Kind = SyntaxKind.RightBracket, Line = 1, Literal = "]" },
            new Token() { Column = 8, Kind = SyntaxKind.Semicolon, Line = 1, Literal = ";" },
            new Token() { Column = 9, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
        };

        public static List<Token> Hashes = new List<Token>()
        {
            new Token() { Column = 2, Kind = SyntaxKind.LeftBrace, Line = 1, Literal = "{" },
            new Token() { Column = 4, Kind = SyntaxKind.String, Line = 1, Literal = "foo" },
            new Token() { Column = 9, Kind = SyntaxKind.Colon, Line = 1, Literal = ":" },
            new Token() { Column = 11, Kind = SyntaxKind.String, Line = 1, Literal = "bar" },
            new Token() { Column = 17, Kind = SyntaxKind.RightBrace, Line = 1, Literal = "}" },
            new Token() { Column = 18, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
        };
    }
}
