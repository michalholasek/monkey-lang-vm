using System.Collections.Generic;
using System.IO;
using System.Text;

using static Monkey.Shared.Scanner.Utilities;

namespace Monkey.Shared
{
    public partial class Scanner
    {
        private class ScannerState
        {
            public StringBuilder Buffer { get; set; }
            public StringReader Characters { get; set; }
            public int Column { get; set; }
            public char CurrentCharacter { get; set; }
            public int Line { get; set; }
            public List<Token> Tokens { get; set; }
        }
    }
    
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

        public static Token Create(string literal, int column, int line)
        {
            return CreateToken(literal, column, line);
        }
    }
}
