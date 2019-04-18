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
                if (args == null || args.Count != 1)
                {
                    return CreateObject
                    (
                        ObjectKind.Error,
                        Error.CreateEvaluationError(AssertionErrorKind.InvalidArgument, "unexpected number of arguments")
                    );
                }

                var obj = args.First();

                if (obj.Kind != ObjectKind.Array && obj.Kind != ObjectKind.String)
                {
                    return CreateObject
                    (
                        ObjectKind.Error,
                        Error.CreateEvaluationError(AssertionErrorKind.InvalidArgument, obj.Kind)
                    );
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
                if (args == null || args.Count != 1)
                {
                    return CreateObject
                    (
                        ObjectKind.Error,
                        Error.CreateEvaluationError(AssertionErrorKind.InvalidArgument, "unexpected number of arguments")
                    );
                }

                var obj = args.First();

                if (obj.Kind != ObjectKind.Array)
                {
                    return CreateObject
                    (
                        ObjectKind.Error,
                        Error.CreateEvaluationError(AssertionErrorKind.InvalidArgument, obj.Kind)
                    );
                }

                var array = (List<Object>)obj.Value;

                return array.Count > 0 ? array.First() : CreateObject(ObjectKind.Null, null);
            };

            private static Func<List<Object>, Object> Last = (List<Object> args) =>
            {
                if (args == null || args.Count != 1)
                {
                    return CreateObject
                    (
                        ObjectKind.Error,
                        Error.CreateEvaluationError(AssertionErrorKind.InvalidArgument, "unexpected number of arguments")
                    );
                }

                var obj = args.First();

                if (obj.Kind != ObjectKind.Array)
                {
                    return CreateObject
                    (
                        ObjectKind.Error,
                        Error.CreateEvaluationError(AssertionErrorKind.InvalidArgument, obj.Kind)
                    );
                }

                var array = (List<Object>)obj.Value;

                return array.Count > 0 ? array.Last() : CreateObject(ObjectKind.Null, null);
            };

            private static Func<List<Object>, Object> Rest = (List<Object> args) =>
            {
                if (args == null || args.Count != 1)
                {
                    return CreateObject
                    (
                        ObjectKind.Error,
                        Error.CreateEvaluationError(AssertionErrorKind.InvalidArgument, "unexpected number of arguments")
                    );
                }

                var obj = args.First();

                if (obj.Kind != ObjectKind.Array)
                {
                    return CreateObject
                    (
                        ObjectKind.Error,
                        Error.CreateEvaluationError(AssertionErrorKind.InvalidArgument, obj.Kind)
                    );
                }

                var array = (List<Object>)obj.Value;

                return array.Count > 0 ?
                    CreateObject(ObjectKind.Array, array.Skip(1)) :
                    CreateObject(ObjectKind.Null, null)
                ;
            };

            private static Func<List<Object>, Object> Push = (List<Object> args) =>
            {
                if (args == null || args.Count != 2)
                {
                    return CreateObject
                    (
                        ObjectKind.Error,
                        Error.CreateEvaluationError(AssertionErrorKind.InvalidArgument, "unexpected number of arguments")
                    );
                }

                var obj = args.First();

                if (obj.Kind != ObjectKind.Array)
                {
                    return CreateObject
                    (
                        ObjectKind.Error,
                        Error.CreateEvaluationError(AssertionErrorKind.InvalidArgument, obj.Kind)
                    );
                }

                var array = (List<Object>)obj.Value;
                array.Add(args.Last());

                return CreateObject(ObjectKind.Array, array);
            };
        }
    }
}
