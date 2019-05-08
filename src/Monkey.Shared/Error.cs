using System;
using System.Collections.Generic;

namespace Monkey.Shared
{
    internal static class Error
    {
        private static Dictionary<AssertionErrorKind, string> AssertionErrorKindString = new Dictionary<AssertionErrorKind, string>
        {
            { AssertionErrorKind.InvalidToken, "invalid token" },
            { AssertionErrorKind.UnexpectedToken, "unexpected token" },
            { AssertionErrorKind.UnknownOperator, "unknown operator" }
        };
        
        internal static AssertionError CreateAssertionError(AssertionErrorKind kind, Token actual, SyntaxKind expected = SyntaxKind.Illegal)
        {
            string common = $"{AssertionErrorKindString[kind]}({actual.Column}, {actual.Line}):";
            string body;

            switch (kind)
            {
                case AssertionErrorKind.InvalidToken:
                    body = $"got {Enum.GetName(typeof(SyntaxKind), actual.Kind)}, expected {Enum.GetName(typeof(SyntaxKind), expected)}";
                    break;
                default:
                    body = $"got {Enum.GetName(typeof(SyntaxKind), actual.Kind)}";
                    break;
            }

            return new AssertionError($"{common} {body}");
        }

        internal static AssertionError CreateParsingError(AssertionErrorKind kind, Token actual, string message)
        {
            string common = $"{AssertionErrorKindString[kind]}:";
            string body;

            switch (kind)
            {
                case AssertionErrorKind.UnknownOperator:
                    body = $"got {Enum.GetName(typeof(SyntaxKind), actual.Kind)} ({actual.Literal}), {message}";
                    break;
                default:
                    body = $"got {Enum.GetName(typeof(SyntaxKind), actual.Kind)}";
                    break;
            }

            return new AssertionError($"{common} {body}");
        }
    }
}
