using System;
using System.Collections.Generic;
using System.Linq;

using Monkey.Shared;
using Object = Monkey.Shared.Object;
using static Monkey.Evaluator.Utilities;

namespace Monkey
{
    public partial class Evaluator
    {
        internal class BuiltIn
        {
            public static Dictionary<string, Object> Functions;

            static BuiltIn()
            {
                Functions = new Dictionary<string, Object>
                {
                    { "len", CreateObject(ObjectKind.BuiltIn, Len) },
                    { "first", CreateObject(ObjectKind.BuiltIn, First) },
                    { "last", CreateObject(ObjectKind.BuiltIn, Last) },
                    { "rest", CreateObject(ObjectKind.BuiltIn, Rest) },
                    { "push", CreateObject(ObjectKind.BuiltIn, Push) }
                };
            }

            private static Func<List<Object>, Object> Len = (List<Object> args) =>
            {
                ErrorInfo info;

                if (args == null || args.Count != 1)
                {
                    info = new ErrorInfo
                    {
                        Code = ErrorCode.BuiltInLenUnexpectedNoOfArguments,
                        Kind = ErrorKind.InvalidArgument,
                        Offenders = new List<object> { "len" },
                        Source = ErrorSource.Evaluator
                    };

                    return CreateObject(ObjectKind.Error, Error.Create(info));
                }

                var obj = args.First();

                if (obj.Kind != ObjectKind.Array && obj.Kind != ObjectKind.String)
                {
                    info = new ErrorInfo
                    {
                        Code = ErrorCode.BuiltInLenInvalidArgument,
                        Kind = ErrorKind.InvalidArgument,
                        Offenders = new List<object> { "len", obj },
                        Source = ErrorSource.Evaluator
                    };

                    return CreateObject(ObjectKind.Error, Error.Create(info));
                }

                switch (obj.Kind)
                {
                    case ObjectKind.String:
                        var str = (string)obj.Value;
                        return CreateObject(ObjectKind.Integer, str.Length);
                    default:
                        var array = (List<Object>)obj.Value;
                        return CreateObject(ObjectKind.Integer, array.Count);
                }
            };

            private static Func<List<Object>, Object> First = (List<Object> args) =>
            {
                ErrorInfo info;

                if (args == null || args.Count != 1)
                {
                    info = new ErrorInfo
                    {
                        Code = ErrorCode.BuiltInFirstUnexpectedNoOfArguments,
                        Kind = ErrorKind.InvalidArgument,
                        Offenders = new List<object> { "first" },
                        Source = ErrorSource.Evaluator
                    };

                    return CreateObject(ObjectKind.Error, Error.Create(info));
                }

                var obj = args.First();

                if (obj.Kind != ObjectKind.Array)
                {
                    info = new ErrorInfo
                    {
                        Code = ErrorCode.BuiltInFirstInvalidArgument,
                        Kind = ErrorKind.InvalidArgument,
                        Offenders = new List<object> { "first", obj },
                        Source = ErrorSource.Evaluator
                    };

                    return CreateObject(ObjectKind.Error, Error.Create(info));
                }

                var array = (List<Object>)obj.Value;

                return array.Count > 0 ? array.First() : CreateObject(ObjectKind.Null, null);
            };

            private static Func<List<Object>, Object> Last = (List<Object> args) =>
            {
                ErrorInfo info;

                if (args == null || args.Count != 1)
                {
                    info = new ErrorInfo
                    {
                        Code = ErrorCode.BuiltInLastUnexpectedNoOfArguments,
                        Kind = ErrorKind.InvalidArgument,
                        Offenders = new List<object> { "last" },
                        Source = ErrorSource.Evaluator
                    };

                    return CreateObject(ObjectKind.Error, Error.Create(info));
                }

                var obj = args.First();

                if (obj.Kind != ObjectKind.Array)
                {
                    info = new ErrorInfo
                    {
                        Code = ErrorCode.BuiltInLastInvalidArgument,
                        Kind = ErrorKind.InvalidArgument,
                        Offenders = new List<object> { "last", obj },
                        Source = ErrorSource.Evaluator
                    };

                    return CreateObject(ObjectKind.Error, Error.Create(info));
                }

                var array = (List<Object>)obj.Value;

                return array.Count > 0 ? array.Last() : CreateObject(ObjectKind.Null, null);
            };

            private static Func<List<Object>, Object> Rest = (List<Object> args) =>
            {
                ErrorInfo info;

                if (args == null || args.Count != 1)
                {
                    info = new ErrorInfo
                    {
                        Code = ErrorCode.BuiltInRestUnexpectedNoOfArguments,
                        Kind = ErrorKind.InvalidArgument,
                        Offenders = new List<object> { "rest" },
                        Source = ErrorSource.Evaluator
                    };

                    return CreateObject(ObjectKind.Error, Error.Create(info));
                }

                var obj = args.First();

                if (obj.Kind != ObjectKind.Array)
                {
                    info = new ErrorInfo
                    {
                        Code = ErrorCode.BuiltInRestInvalidArgument,
                        Kind = ErrorKind.InvalidArgument,
                        Offenders = new List<object> { "rest", obj },
                        Source = ErrorSource.Evaluator
                    };

                    return CreateObject(ObjectKind.Error, Error.Create(info));
                }

                var array = (List<Object>)obj.Value;

                return array.Count > 0 ?
                    CreateObject(ObjectKind.Array, array.Skip(1)) :
                    CreateObject(ObjectKind.Null, null)
                ;
            };

            private static Func<List<Object>, Object> Push = (List<Object> args) =>
            {
                ErrorInfo info;

                if (args == null || args.Count != 2)
                {
                    info = new ErrorInfo
                    {
                        Code = ErrorCode.BuiltInPushUnexpectedNoOfArguments,
                        Kind = ErrorKind.InvalidArgument,
                        Offenders = new List<object> { "push", },
                        Source = ErrorSource.Evaluator
                    };

                    return CreateObject(ObjectKind.Error, Error.Create(info));
                }

                var obj = args.First();
                var arg = args.Last();

                if (obj.Kind != ObjectKind.Array)
                {
                    info = new ErrorInfo
                    {
                        Code = ErrorCode.BuiltInPushInvalidArgument,
                        Kind = ErrorKind.InvalidArgument,
                        Offenders = new List<object> { "push", obj, arg },
                        Source = ErrorSource.Evaluator
                    };

                    return CreateObject(ObjectKind.Error, Error.Create(info));
                }

                var array = (List<Object>)obj.Value;
                array.Add(arg);

                return CreateObject(ObjectKind.Array, array);
            };
        }
    }
}
