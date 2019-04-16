using System;
using System.Collections.Generic;

using Monkey.Shared.Scanner;
using Monkey.Shared.Parser.Ast;

namespace Monkey.Shared
{
    internal static class Error
    {
        private static Dictionary<AssertionErrorKind, string> AssertionErrorKindString = new Dictionary<AssertionErrorKind, string>
        {
            { AssertionErrorKind.InvalidToken, "invalid token" },
            { AssertionErrorKind.UnexpectedToken, "unexpected token" }
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
    }
}
