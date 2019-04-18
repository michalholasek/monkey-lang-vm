using System.Collections.Generic;

namespace Monkey.Shared
{
    public enum SyntaxKind
    {
        Illegal,
        EOF,

        // Identifiers and literals
        Identifier,
        Int,
        String,

        // Operators
        Assign,
        Plus,
        Minus,
        Bang,
        Asterisk,
        Slash,
        LessThan,
        GreaterThan,
        Equal,
        NotEqual,

        // Delimiters
        Comma,
        Semicolon,
        Colon,

        // Keywords
        Function,
        Let,
        True,
        False,
        If,
        Else,
        Return,

        LeftParenthesis,
        RightParenthesis,
        LeftBrace,
        RightBrace,
        LeftBracket,
        RightBracket
    }

    public class Token
    {
        public int Column { get; set; }
        public SyntaxKind Kind { get; set; }
        public string Literal { get; set; }
        public int Line { get; set; }
    }
}
