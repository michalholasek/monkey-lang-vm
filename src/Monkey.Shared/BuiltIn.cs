using System;
using System.Collections.Generic;
using System.Linq;

namespace Monkey.Shared
{
    public class BuiltIn
    {
        public Object Function { get; set; }
        public string Name { get; set; }
    }

    public static class Functions
    {
        public static List<BuiltIn> List;

        static Functions()
        {
            List = new List<BuiltIn>
            {
                new BuiltIn { Name = "len", Function = Object.Create(ObjectKind.BuiltIn, Len) },
                new BuiltIn { Name = "first", Function = Object.Create(ObjectKind.BuiltIn, First) },
                new BuiltIn { Name = "last", Function = Object.Create(ObjectKind.BuiltIn, Last) },
                new BuiltIn { Name = "rest", Function = Object.Create(ObjectKind.BuiltIn, Rest) },
                new BuiltIn { Name = "push", Function = Object.Create(ObjectKind.BuiltIn, Push) }
            };
        }

        public static Object GetByName(string name)
        {
            foreach (var obj in List)
            {
                if (obj.Name == name)
                {
                    return obj.Function;
                }
            }

            return default(Object);
        }

        private static Func<List<Object>, Object> Len = (List<Object> args) =>
        {
            ErrorInfo info;

            if (args == default(List<Object>) || args.Count != 1)
            {
                info = new ErrorInfo
                {
                    Code = ErrorCode.BuiltInLenUnexpectedNoOfArguments,
                    Kind = ErrorKind.InvalidArgument,
                    Offenders = new List<object> { "len" },
                    Source = ErrorSource.Evaluator
                };

                return Object.Create(ObjectKind.Error, Error.Create(info));
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

                return Object.Create(ObjectKind.Error, Error.Create(info));
            }

            switch (obj.Kind)
            {
                case ObjectKind.String:
                    var str = (string)obj.Value;
                    return Object.Create(ObjectKind.Integer, str.Length);
                default:
                    var array = (List<Object>)obj.Value;
                    return Object.Create(ObjectKind.Integer, array.Count);
            }
        };

        private static Func<List<Object>, Object> First = (List<Object> args) =>
        {
            ErrorInfo info;

            if (args == default(List<Object>) || args.Count != 1)
            {
                info = new ErrorInfo
                {
                    Code = ErrorCode.BuiltInFirstUnexpectedNoOfArguments,
                    Kind = ErrorKind.InvalidArgument,
                    Offenders = new List<object> { "first" },
                    Source = ErrorSource.Evaluator
                };

                return Object.Create(ObjectKind.Error, Error.Create(info));
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

                return Object.Create(ObjectKind.Error, Error.Create(info));
            }

            var array = (List<Object>)obj.Value;

            return array.Count > 0 ? array.First() : Object.Create(ObjectKind.Null, null);
        };

        private static Func<List<Object>, Object> Last = (List<Object> args) =>
        {
            ErrorInfo info;

            if (args == default(List<Object>) || args.Count != 1)
            {
                info = new ErrorInfo
                {
                    Code = ErrorCode.BuiltInLastUnexpectedNoOfArguments,
                    Kind = ErrorKind.InvalidArgument,
                    Offenders = new List<object> { "last" },
                    Source = ErrorSource.Evaluator
                };

                return Object.Create(ObjectKind.Error, Error.Create(info));
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

                return Object.Create(ObjectKind.Error, Error.Create(info));
            }

            var array = (List<Object>)obj.Value;

            return array.Count > 0 ? array.Last() : Object.Create(ObjectKind.Null, null);
        };

        private static Func<List<Object>, Object> Rest = (List<Object> args) =>
        {
            ErrorInfo info;

            if (args == default(List<Object>) || args.Count != 1)
            {
                info = new ErrorInfo
                {
                    Code = ErrorCode.BuiltInRestUnexpectedNoOfArguments,
                    Kind = ErrorKind.InvalidArgument,
                    Offenders = new List<object> { "rest" },
                    Source = ErrorSource.Evaluator
                };

                return Object.Create(ObjectKind.Error, Error.Create(info));
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

                return Object.Create(ObjectKind.Error, Error.Create(info));
            }

            var array = (List<Object>)obj.Value;

            return array.Count > 0 ?
                Object.Create(ObjectKind.Array, array.Skip(1)) :
                Object.Create(ObjectKind.Null, null)
            ;
        };

        private static Func<List<Object>, Object> Push = (List<Object> args) =>
        {
            ErrorInfo info;

            if (args == default(List<Object>) || args.Count != 2)
            {
                info = new ErrorInfo
                {
                    Code = ErrorCode.BuiltInPushUnexpectedNoOfArguments,
                    Kind = ErrorKind.InvalidArgument,
                    Offenders = new List<object> { "push", },
                    Source = ErrorSource.Evaluator
                };

                return Object.Create(ObjectKind.Error, Error.Create(info));
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

                return Object.Create(ObjectKind.Error, Error.Create(info));
            }

            var array = (List<Object>)obj.Value;
            array.Add(arg);

            return Object.Create(ObjectKind.Array, array);
        };
    }
}
