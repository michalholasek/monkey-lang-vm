using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monkey.Shared
{
    public struct AssertionError
    {
        public string Message { get; set; }

        public AssertionError(string message)
        {
            Message = message;
        }
    }

    public enum ErrorCode
    {
        ArrayExpressionEvaluation,
        ArrayIndexExpressionEvaluation,
        BangOperatorExpressionEvaluation,
        BooleanInfixExpressionEvaluation,
        BuiltInLenInvalidArgument,
        BuiltInLenUnexpectedNoOfArguments,
        BuiltInFirstInvalidArgument,
        BuiltInFirstUnexpectedNoOfArguments,
        BuiltInLastInvalidArgument,
        BuiltInLastUnexpectedNoOfArguments,
        BuiltInRestInvalidArgument,
        BuiltInRestUnexpectedNoOfArguments,
        BuiltInPushInvalidArgument,
        BuiltInPushUnexpectedNoOfArguments,
        HashIndexExpressionEvaluation,
        IdentifierExpressionEvaluation,
        InfixExpressionEvaluation,
        InfixExpressionOperatorEvaluation,
        InvalidLetExpression,
        InvalidLetIdentifierToken,
        InvalidLetAssignToken,
        InvalidReturnExpression,
        InvalidToken,
        MissingClosingToken,
        MissingComma,
        MissingExpressionToken,
        MissingLetIdentifierToken,
        MissingLetAssignToken,
        MinusOperatorExpressionEvaluation,
        StringExpressionOperatorEvaluation,
        UndefinedVariable,
        UnexpectedNumberOfArguments,
        UnknownOperator,
    }

    public enum ErrorKind
    {
        InvalidArgument,
        InvalidIdentifier,
        InvalidIndex,
        InvalidToken,
        InvalidType,
        MissingToken,
        UndefinedVariable,
        UnexpectedToken,
        UnknownOperator
    }

    public enum ErrorSource
    {
        Compiler,
        Evaluator,
        Parser
    }

    public class ErrorInfo
    {
        public ErrorCode Code { get; set; }
        public ErrorKind Kind { get; set; }
        public List<object> Offenders { get; set;}
        public int Position { get; set; }
        public ErrorSource Source { get; set; }
        public List<Token> Tokens { get; set; }
    }

    public static class Error
    {   
        private static Dictionary<ErrorKind, string> ErrorKindString = new Dictionary<ErrorKind, string>
        {
            { ErrorKind.InvalidArgument, "invalid argument" },
            { ErrorKind.InvalidIdentifier, "invalid identifier" },
            { ErrorKind.InvalidIndex, "invalid index" },
            { ErrorKind.InvalidToken, "invalid token" },
            { ErrorKind.InvalidType, "invalid type" },
            { ErrorKind.MissingToken, "missing token" },
            { ErrorKind.UndefinedVariable, "undefined variable" },
            { ErrorKind.UnexpectedToken, "unexpected token" },
            { ErrorKind.UnknownOperator, "unknown operator" }
        };

        private static Dictionary<ErrorCode, string> ErrorMessages = new Dictionary<ErrorCode, string>
        {
            { ErrorCode.ArrayExpressionEvaluation, "@0<-- [@1], expected Array" },
            { ErrorCode.ArrayIndexExpressionEvaluation, "@0[@1<-- ], expected Integer" },
            { ErrorCode.BangOperatorExpressionEvaluation, "!@0<-- , expected Boolean or Integer" },
            { ErrorCode.BooleanInfixExpressionEvaluation, "@0 @1<-- @2" },
            { ErrorCode.BuiltInLenUnexpectedNoOfArguments, "@0(), unexpected number of arguments" },
            { ErrorCode.BuiltInLenInvalidArgument, "len(@1<-- ), expected Array or String" },
            { ErrorCode.BuiltInFirstUnexpectedNoOfArguments, "@0(), unexpected number of arguments" },
            { ErrorCode.BuiltInFirstInvalidArgument, "first(@1<-- ), expected Array" },
            { ErrorCode.BuiltInLastUnexpectedNoOfArguments, "@0(), unexpected number of arguments" },
            { ErrorCode.BuiltInLastInvalidArgument, "last(@1<-- ), expected Array" },
            { ErrorCode.BuiltInRestUnexpectedNoOfArguments, "@0(), unexpected number of arguments" },
            { ErrorCode.BuiltInRestInvalidArgument, "rest(@1<-- ), expected Array" },
            { ErrorCode.BuiltInPushUnexpectedNoOfArguments, "@0(), unexpected number of arguments" },
            { ErrorCode.BuiltInPushInvalidArgument, "push(@1<-- , @2), expected Array as first argument" },
            { ErrorCode.HashIndexExpressionEvaluation, "@0[@1<-- ], expected Integer, Boolean, or String" },
            { ErrorCode.IdentifierExpressionEvaluation, "@0 not found" },
            { ErrorCode.InfixExpressionEvaluation, "types of @0 and @1 do not match" },
            { ErrorCode.InfixExpressionOperatorEvaluation, "operator @1 is not supported for operands of type @2" },
            { ErrorCode.InvalidLetExpression, "@0" },
            { ErrorCode.InvalidLetIdentifierToken, "@0" },
            { ErrorCode.InvalidLetAssignToken, "@0" },
            { ErrorCode.InvalidReturnExpression, "@0" },
            { ErrorCode.InvalidToken, "@0" },
            { ErrorCode.MissingClosingToken, "@0, missing ]" },
            { ErrorCode.MissingComma, "@0, missing comma" },
            { ErrorCode.MissingLetIdentifierToken, "let <identifier><-- = <expression>;" },
            { ErrorCode.MissingLetAssignToken, "@0 <assign><-- <expression>;" },
            { ErrorCode.MissingExpressionToken, "@0, missing expression" },
            { ErrorCode.StringExpressionOperatorEvaluation, "operator @1 is not supported for operands of type @2" },
            { ErrorCode.MinusOperatorExpressionEvaluation, "-@0<-- , operator - is not supported for type @1" },
            { ErrorCode.UnknownOperator, "operator @0 is not supported" },
            { ErrorCode.UnexpectedNumberOfArguments, "unexpected number of arguments" }
        };

        public static AssertionError Create(ErrorInfo info)
        {
            switch (info.Source)
            {
                case ErrorSource.Compiler:
                    return CreateCompilerError(info);
                case ErrorSource.Parser:
                    return CreateParseError(info);
                default:
                    return CreateEvaluationError(info);
            }
        }

        private static AssertionError CreateCompilerError(ErrorInfo info)
        {
            var sb = new StringBuilder();

            sb.Append(ErrorKindString[info.Kind]);
            sb.Append(": ");

            switch (info.Code)
            {
                case ErrorCode.UndefinedVariable:
                    sb.Append(info.Offenders.First());
                    break;
            }

            return new AssertionError(sb.ToString());
        }

        private static AssertionError CreateEvaluationError(ErrorInfo info)
        {
            List<string> offenders = new List<string>();
            var sb = new StringBuilder();

            sb.Append(ErrorKindString[info.Kind]);
            sb.Append(": ");

            switch (info.Code)
            {
                case ErrorCode.ArrayExpressionEvaluation:
                case ErrorCode.ArrayIndexExpressionEvaluation:
                case ErrorCode.HashIndexExpressionEvaluation:
                case ErrorCode.InfixExpressionEvaluation:
                    offenders = new List<String>
                    {
                        Stringify.Object((Object)info.Offenders.First()),
                        Stringify.Object((Object)info.Offenders.Last())
                    };
                    break;
                case ErrorCode.BangOperatorExpressionEvaluation:
                    offenders = new List<String> { Stringify.Object((Object)info.Offenders.First()) };
                    break;
                case ErrorCode.IdentifierExpressionEvaluation:
                    offenders = new List<String> { Stringify.Object(info.Offenders.First()) };
                    break;
                case ErrorCode.MinusOperatorExpressionEvaluation:
                    offenders = new List<String>
                    {
                        Stringify.Object((Object)info.Offenders.First()),
                        Stringify.Kind(((Object)info.Offenders.First()).Kind)
                    };
                    break;
                case ErrorCode.InfixExpressionOperatorEvaluation:
                    offenders = new List<String>
                    {
                        Stringify.Object((Object)info.Offenders.First()),
                        ((Token)info.Offenders[1]).Literal,
                        Stringify.Object((Object)info.Offenders.Last())
                    };
                    break;
                case ErrorCode.BooleanInfixExpressionEvaluation:
                case ErrorCode.StringExpressionOperatorEvaluation:
                    offenders = new List<String>
                    {
                        Stringify.Object(info.Offenders.First()),
                        ((Token)info.Offenders[1]).Literal,
                        Stringify.Object(info.Offenders.Last())
                    };
                    break;
                case ErrorCode.BuiltInLenUnexpectedNoOfArguments:
                case ErrorCode.BuiltInFirstUnexpectedNoOfArguments:
                case ErrorCode.BuiltInLastUnexpectedNoOfArguments:
                case ErrorCode.BuiltInRestUnexpectedNoOfArguments:
                case ErrorCode.BuiltInPushUnexpectedNoOfArguments:
                    offenders = new List<String> { info.Offenders.First().ToString() };
                    break;
                case ErrorCode.BuiltInLenInvalidArgument:
                case ErrorCode.BuiltInFirstInvalidArgument:
                case ErrorCode.BuiltInLastInvalidArgument:
                case ErrorCode.BuiltInRestInvalidArgument:
                    offenders = new List<String>
                    {
                        info.Offenders.First().ToString(),
                        Stringify.Object((Object)info.Offenders.Last())
                    };
                    break;
                case ErrorCode.BuiltInPushInvalidArgument:
                    offenders = new List<String>
                    {
                        info.Offenders.First().ToString(),
                        Stringify.Object((Object)info.Offenders[1]),
                        Stringify.Object((Object)info.Offenders.Last())
                    };
                    break;
                case ErrorCode.UnknownOperator:
                    offenders = new List<string> { ((Token)info.Offenders.First()).Literal };
                    break;
            }

            sb.Append(ComposeErrorMessage(offenders, ErrorMessages[info.Code]));

            return new AssertionError(sb.ToString());
        }

        private static AssertionError CreateParseError(ErrorInfo info)
        {
            var sb = new StringBuilder();
            string expression;

            sb.Append(ErrorKindString[info.Kind]);
            sb.Append(": ");

            switch (info.Code)
            {
                case ErrorCode.MissingExpressionToken:
                    expression = ComposeExpression(info, arrow: false, placeholder: DeterminePlaceholder(SyntaxKind.Int));
                    break;
                case ErrorCode.MissingLetAssignToken:
                    expression = ComposeExpression(info, arrow: false, placeholder: default(string));
                    break;
                case ErrorCode.MissingComma:
                    expression = ComposeExpression(info, arrow: false, placeholder: DeterminePlaceholder(SyntaxKind.Comma));
                    break;
                case ErrorCode.MissingClosingToken:
                    expression = ComposeExpression(info, arrow: false, placeholder: DeterminePlaceholder((SyntaxKind)info.Offenders.First()));
                    break;
                case ErrorCode.InvalidLetExpression:
                case ErrorCode.InvalidLetIdentifierToken:
                case ErrorCode.InvalidLetAssignToken:
                case ErrorCode.InvalidReturnExpression:
                case ErrorCode.InvalidToken:
                    expression = ComposeExpression(info, arrow: true, placeholder: default(string));
                    break;
                default:
                    expression = String.Empty;
                    break;
            }

            List<string> offenders = new List<string> { expression };

            sb.Append(ComposeErrorMessage(offenders, ErrorMessages[info.Code]));

            return new AssertionError(sb.ToString());
        }

        private static string ComposeErrorMessage(List<string> offenders, string template)
        {
            var message = template;

            for (var i = 0; i < offenders.Count; i++)
            {
                message = message.Replace($"@{i}", offenders[i]);
            }

            return message;
        }

        private static string ComposeExpression(ErrorInfo info, bool arrow, string placeholder)
        {
            Token token = info.Tokens.Take(1).FirstOrDefault();
            var sb = new StringBuilder();
            var whitespace = false;

            for (var i = 0; i < info.Tokens.Count; i++)
            {
                if (placeholder != default(string) && i == info.Position)
                {
                    sb.Append(placeholder);
                }

                sb.Append(token.Literal);

                if (arrow && i == info.Position)
                {
                    sb.Append(placeholder ?? String.Empty);
                    sb.Append("<--");
                    whitespace = true;
                }

                token = info.Tokens.Skip(i + 1).Take(1).FirstOrDefault();

                if (whitespace || (token != default(Token) && token.Kind != SyntaxKind.Semicolon))
                {
                    sb.Append(" ");
                }

                whitespace = false;
            }

            if (placeholder != default(string) && info.Tokens.Count <= info.Position)
            {
                sb.Append(" ");
                sb.Append(placeholder);
            }

            return sb.ToString().Trim();
        }

        private static string DeterminePlaceholder(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.Int:
                    return "<expression><-- ";
                case SyntaxKind.Comma:
                    return "<comma><-- ";
                case SyntaxKind.RightBracket:
                    return "<bracket><-- ";
                default:
                    return String.Empty;
            }
        }
    }
}
