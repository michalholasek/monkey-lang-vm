using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Monkey.Shared
{
    public static class Utilities
    {
        private static Dictionary<string, SyntaxKind> Keywords = new Dictionary<string, SyntaxKind>()
        {
            { "else", SyntaxKind.Else },
            { "false", SyntaxKind.False },
            { "fn", SyntaxKind.Function },
            { "if", SyntaxKind.If },
            { "let", SyntaxKind.Let },
            { "return", SyntaxKind.Return },
            { "true", SyntaxKind.True }
        };

        public static Token CreateToken(string literal, int column, int line)
        {
            Token token = new Token()
            {
                Column = column,
                Kind = SyntaxKind.Illegal,
                Line = line,
                Literal = literal
            };

            switch (literal) {
                case "=":
                    token.Kind = SyntaxKind.Assign;
                    break;
                case "==":
                    token.Kind = SyntaxKind.Equal;
                    break;
                case "!=":
                    token.Kind = SyntaxKind.NotEqual;
                    break;
                case "+":
                    token.Kind = SyntaxKind.Plus;
                    break;
                case "-":
                    token.Kind = SyntaxKind.Minus;
                    break;
                case "*":
                    token.Kind = SyntaxKind.Asterisk;
                    break;
                case "!":
                    token.Kind = SyntaxKind.Bang;
                    break;
                case ">":
                    token.Kind = SyntaxKind.GreaterThan;
                    break;
                case "<":
                    token.Kind = SyntaxKind.LessThan;
                    break;
                case "/":
                    token.Kind = SyntaxKind.Slash;
                    break;
                case ";":
                    token.Kind = SyntaxKind.Semicolon;
                    break;
                case ",":
                    token.Kind = SyntaxKind.Comma;
                    break;
                case "(":
                    token.Kind = SyntaxKind.LeftParenthesis;
                    break;
                case ")":
                    token.Kind = SyntaxKind.RightParenthesis;
                    break;
                case "{":
                    token.Kind = SyntaxKind.LeftBrace;
                    break;
                case "}":
                    token.Kind = SyntaxKind.RightBrace;
                    break;
                case "[":
                    token.Kind = SyntaxKind.LeftBracket;
                    break;
                case "]":
                    token.Kind = SyntaxKind.RightBracket;
                    break;
                case ":":
                    token.Kind = SyntaxKind.Colon;
                    break;
                case "":
                    token.Kind = SyntaxKind.EOF;
                    break;
                default:
                    if (IsString(literal))
                    {
                        token.Kind = SyntaxKind.String;
                        token.Literal = token.Literal.Replace("\"", String.Empty);
                    }
                    else if (IsValidLiteral(literal))
                    {
                        token.Kind = DetermineLiteralSyntaxKind(literal);
                    }
                    else if (IsNumber(literal))
                    {
                        token.Kind = SyntaxKind.Int;
                    }
                    else
                    {
                        token.Kind = SyntaxKind.Illegal;
                    }
                    break;
                }

            return token;
        }

        public static bool IsNewlineOrReturnCharacter(char literal)
        {
            return literal == '\n' || literal == '\r';
        }

        public static bool IsQuote(char literal)
        {
            return literal == '"';
        }

        public static bool IsStickyOperator(char literal)
        {
            return literal == '!' || literal == '=';
        }
        
        public static bool IsValidLetterCharacter(char literal)
        {
            return Char.IsLetter(literal) || literal == '_';
        }

        public static bool IsValidLiteral(string literal)
        {            
            return new Regex("[a-z_]", RegexOptions.IgnoreCase).Matches(literal).Count > 0;
        }

        public static bool IsValidStickyOperator(char a, char b)
        {
            var literal = String.Join(String.Empty, a, b);
            return literal == "==" || literal == "!=";
        }

        private static SyntaxKind DetermineLiteralSyntaxKind(string literal)
        {
            if (Keywords.ContainsKey(literal))
            {
                return Keywords[literal];
            }

            return SyntaxKind.Identifier;
        }

        private static bool IsNumber(string literal)
        {
            return int.TryParse(literal, out int n);
        }

        private static bool IsString(string literal)
        {
            return literal.StartsWith("\"");
        }
    }
}
