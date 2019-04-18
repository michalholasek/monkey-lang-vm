using System;
using System.Collections.Generic;

namespace Monkey.Shared
{
    internal static class Error
    {
        private static Dictionary<AssertionErrorKind, string> AssertionErrorKindString = new Dictionary<AssertionErrorKind, string>
        {
            { AssertionErrorKind.InvalidArgument, "invalid argument" },
            { AssertionErrorKind.InvalidIdentifier, "invalid identifier" },
            { AssertionErrorKind.InvalidIndex, "invalid index" },
            { AssertionErrorKind.InvalidToken, "invalid token" },
            { AssertionErrorKind.InvalidType, "invalid type" },
            { AssertionErrorKind.UnknownOperator, "unknown operator" }
        };

        internal static AssertionError CreateEvaluationError(AssertionErrorKind kind, string message)
        {
            return new AssertionError($"{AssertionErrorKindString[kind]}: {message}");
        }

        internal static AssertionError CreateEvaluationError(AssertionErrorKind kind, SyntaxKind actual)
        {
            return new AssertionError($"{AssertionErrorKindString[kind]}: got {Enum.GetName(typeof(SyntaxKind), actual)}");
        }

        internal static AssertionError CreateEvaluationError(AssertionErrorKind kind, SyntaxKind actual, SyntaxKind expected)
        {
            string common = $"{AssertionErrorKindString[kind]}:";
            string body = $"got {Enum.GetName(typeof(SyntaxKind), actual)}, expected {Enum.GetName(typeof(SyntaxKind), expected)}";

            return new AssertionError($"{common} {body}");
        }

        internal static AssertionError CreateEvaluationError(AssertionErrorKind kind, ObjectKind actual)
        {
            return new AssertionError($"{AssertionErrorKindString[kind]}: got {Enum.GetName(typeof(ObjectKind), actual)}");
        }

        internal static AssertionError CreateEvaluationError(AssertionErrorKind kind, ObjectKind actual, ObjectKind expected)
        {
            string common = $"{AssertionErrorKindString[kind]}:";
            string body = $"got {Enum.GetName(typeof(ObjectKind), actual)}, expected {Enum.GetName(typeof(ObjectKind), expected)}";

            return new AssertionError($"{common} {body}");
        }
    }
}
