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
        MinusOperatorExpressionEvaluation,
        StringExpressionOperatorEvaluation,
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
        UnexpectedToken,
        UnknownOperator
    }

    public enum ErrorSource
    {
        Evaluator,
        Parser
    }

    public class ErrorInfo
    {
        public SyntaxKind Actual { get; set; }
        public ErrorCode Code { get; set; }
        public List<SyntaxKind> Expected { get; set; }
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
            { ErrorKind.UnexpectedToken, "unexpected token" },
            { ErrorKind.UnknownOperator, "unknown operator" }
        };

        private static Dictionary<ErrorCode, string> ErrorMessages = new Dictionary<ErrorCode, string>
        {
            { ErrorCode.UnexpectedNumberOfArguments, "unexpected number of arguments" }
        };

        public static AssertionError Create(ErrorInfo info)
        {
            switch (info.Source)
            {
                case ErrorSource.Parser:
                    return CreateParseError(info);
                default:
                    return CreateEvaluationError(info);
            }
        }

        private static AssertionError CreateEvaluationError(ErrorInfo info)
        {
            var sb = new StringBuilder();

            sb.Append($"{ErrorKindString[info.Kind]}: ");

            switch (info.Code)
            {
                case ErrorCode.ArrayExpressionEvaluation:
                    sb.Append(Stringify.Object((Object)info.Offenders.First()));
                    sb.Append("<-- ");
                    sb.Append("[");
                    sb.Append(Stringify.Object((Object)info.Offenders.Last()));
                    sb.Append("]");
                    sb.Append(", expected ");
                    sb.Append(Stringify.Kind(ObjectKind.Array));
                    break;
                case ErrorCode.ArrayIndexExpressionEvaluation:
                    sb.Append(Stringify.Object((Object)info.Offenders.First()));
                    sb.Append("[");
                    sb.Append(Stringify.Object((Object)info.Offenders.Last()));
                    sb.Append("<-- ");
                    sb.Append("]");
                    sb.Append(", expected ");
                    sb.Append(Stringify.Kind(ObjectKind.Integer));
                    break;
                case ErrorCode.BangOperatorExpressionEvaluation:
                    sb.Append("!");
                    sb.Append(Stringify.Object((Object)info.Offenders.First()));
                    sb.Append("<-- , expected ");
                    sb.Append(Stringify.Kind(ObjectKind.Boolean));
                    sb.Append(" or ");
                    sb.Append(Stringify.Kind(ObjectKind.Integer));
                    break;
                case ErrorCode.BooleanInfixExpressionEvaluation:
                    sb.Append(Stringify.Object(info.Offenders.First()));
                    sb.Append(" ");
                    sb.Append(((Token)info.Offenders[1]).Literal);
                    sb.Append("<-- ");
                    sb.Append(Stringify.Object(info.Offenders.Last()));
                    break;
                case ErrorCode.BuiltInLenUnexpectedNoOfArguments:
                    sb.Append("len(), ");
                    sb.Append(ErrorMessages[ErrorCode.UnexpectedNumberOfArguments]);
                    break;
                case ErrorCode.BuiltInLenInvalidArgument:
                    sb.Append("len(");
                    sb.Append(Stringify.Object((Object)info.Offenders.First()));
                    sb.Append("), expected Array or String");
                    break;
                case ErrorCode.BuiltInFirstUnexpectedNoOfArguments:
                    sb.Append("first(), ");
                    sb.Append(ErrorMessages[ErrorCode.UnexpectedNumberOfArguments]);
                    break;
                case ErrorCode.BuiltInFirstInvalidArgument:
                    sb.Append("first(");
                    sb.Append(Stringify.Object((Object)info.Offenders.First()));
                    sb.Append("), expected Array");
                    break;
                case ErrorCode.BuiltInLastUnexpectedNoOfArguments:
                    sb.Append("last(), ");
                    sb.Append(ErrorMessages[ErrorCode.UnexpectedNumberOfArguments]);
                    break;
                case ErrorCode.BuiltInLastInvalidArgument:
                    sb.Append("last(");
                    sb.Append(Stringify.Object((Object)info.Offenders.First()));
                    sb.Append("), expected Array");
                    break;
                case ErrorCode.BuiltInRestUnexpectedNoOfArguments:
                    sb.Append("rest(), ");
                    sb.Append(ErrorMessages[ErrorCode.UnexpectedNumberOfArguments]);
                    break;
                case ErrorCode.BuiltInRestInvalidArgument:
                    sb.Append("rest(");
                    sb.Append(Stringify.Object((Object)info.Offenders.First()));
                    sb.Append("), expected Array");
                    break;
                case ErrorCode.BuiltInPushUnexpectedNoOfArguments:
                    sb.Append("push(), ");
                    sb.Append(ErrorMessages[ErrorCode.UnexpectedNumberOfArguments]);
                    break;
                case ErrorCode.BuiltInPushInvalidArgument:
                    sb.Append("push(");
                    sb.Append(Stringify.Object((Object)info.Offenders.First()));
                    sb.Append(", ");
                    sb.Append(Stringify.Object((Object)info.Offenders.Last()));
                    sb.Append("), expected Array as first argument");
                    break;
                case ErrorCode.HashIndexExpressionEvaluation:
                    sb.Append(Stringify.Object((Object)info.Offenders.First()));
                    sb.Append("[");
                    sb.Append(Stringify.Object((Object)info.Offenders.Last()));
                    sb.Append("<-- ");
                    sb.Append("]");
                    sb.Append(", expected ");
                    sb.Append(Stringify.Kind(ObjectKind.Integer));
                    sb.Append(", ");
                    sb.Append(Stringify.Kind(ObjectKind.Boolean));
                    sb.Append(", or ");
                    sb.Append(Stringify.Kind(ObjectKind.String));
                    break;
                case ErrorCode.IdentifierExpressionEvaluation:
                    sb.Append(info.Offenders.First());
                    sb.Append(" not found");
                    break;
                case ErrorCode.InfixExpressionEvaluation:
                    sb.Append("types of ");
                    sb.Append(Stringify.Object((Object)info.Offenders.First()));
                    sb.Append(" and ");
                    sb.Append(Stringify.Object((Object)info.Offenders.Last()));
                    sb.Append(" do not match");
                    break;
                case ErrorCode.InfixExpressionOperatorEvaluation:
                case ErrorCode.StringExpressionOperatorEvaluation:
                    sb.Append("operator ");
                    sb.Append(((Token)info.Offenders[1]).Literal);
                    sb.Append(" is not supported for operands of type ");
                    sb.Append(Stringify.Kind(((Object)info.Offenders.Last()).Kind));
                    break;
                case ErrorCode.MinusOperatorExpressionEvaluation:
                    sb.Append("-");
                    sb.Append(Stringify.Object((Object)info.Offenders.First()));
                    sb.Append("<-- , operator Minus is not supported for type ");
                    sb.Append(Stringify.Kind(((Object)info.Offenders.First()).Kind));
                    break;
                case ErrorCode.UnknownOperator:
                    sb.Append("operator ");
                    sb.Append(((Token)info.Offenders.First()).Literal);
                    sb.Append(" is not supported");
                    break;
            }

            return new AssertionError(sb.ToString());
        }

        private static AssertionError CreateParseError(ErrorInfo info)
        {
            return new AssertionError($"{ErrorKindString[info.Kind]}: {ComposeExpressionString(info)}");
        }

        private static string ComposeExpressionString(ErrorInfo info)
        {
            var sb = new StringBuilder();
            Token token;

            for (var i = 0; i < info.Tokens.Count; i++)
            {
                token = info.Tokens[i];
                sb.Append(token.Literal);

                if (i == info.Position)
                {
                    sb.Append("<-- ");
                    continue;
                }

                if (i + 1 == info.Tokens.Count)
                {
                    break;
                }

                token = info.Tokens[i + 1];

                switch (token.Kind)
                {
                    case SyntaxKind.Semicolon:
                    case SyntaxKind.EOF:
                        break;
                    default:
                        sb.Append(" ");
                        break;
                }
            }

            return sb.ToString().Trim();
        }
    }
}
