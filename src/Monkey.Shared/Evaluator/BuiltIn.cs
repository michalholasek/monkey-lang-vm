using System;
using System.Collections.Generic;
using System.Linq;

using Monkey.Shared.Parser.Ast;
using Object = Monkey.Shared.Evaluator.Object;

namespace Monkey.Shared.Evaluator
{
    internal class BuiltIn
    {
        public static Dictionary<string, Object> Functions;

        static BuiltIn()
        {
            Functions = new Dictionary<string, Object>
            {
                { "len", Utilities.CreateObject(ObjectKind.BuiltIn, Len) },
                { "first", Utilities.CreateObject(ObjectKind.BuiltIn, First) },
                { "last", Utilities.CreateObject(ObjectKind.BuiltIn, Last) },
                { "rest", Utilities.CreateObject(ObjectKind.BuiltIn, Rest) },
                { "push", Utilities.CreateObject(ObjectKind.BuiltIn, Push) }
            };
        }

        private static Func<List<Object>, Object> Len = (List<Object> args) =>
        {
            if (args == null || args.Count != 1)
            {
                return Utilities.CreateObject
                (
                    ObjectKind.Error,
                    Error.CreateEvaluationError(AssertionErrorKind.InvalidArgument, "unexpected number of arguments")
                );
            }

            var obj = args.First();

            if (obj.Kind != ObjectKind.Array && obj.Kind != ObjectKind.String)
            {
                return Utilities.CreateObject
                (
                    ObjectKind.Error,
                    Error.CreateEvaluationError(AssertionErrorKind.InvalidArgument, obj.Kind)
                );
            }

            switch (obj.Kind)
            {
                case ObjectKind.String:
                    var str = (string)obj.Value;
                    return Utilities.CreateObject(ObjectKind.Integer, str.Length);
                default:
                    var array = (List<Object>)obj.Value;
                    return Utilities.CreateObject(ObjectKind.Integer, array.Count);
            }
        };

        private static Func<List<Object>, Object> First = (List<Object> args) =>
        {
            if (args == null || args.Count != 1)
            {
                return Utilities.CreateObject
                (
                    ObjectKind.Error,
                    Error.CreateEvaluationError(AssertionErrorKind.InvalidArgument, "unexpected number of arguments")
                );
            }

            var obj = args.First();

            if (obj.Kind != ObjectKind.Array)
            {
                return Utilities.CreateObject
                (
                    ObjectKind.Error,
                    Error.CreateEvaluationError(AssertionErrorKind.InvalidArgument, obj.Kind)
                );
            }

            var array = (List<Object>)obj.Value;

            return array.Count > 0 ? array.First() : Utilities.CreateObject(ObjectKind.Null, null);
        };

        private static Func<List<Object>, Object> Last = (List<Object> args) =>
        {
            if (args == null || args.Count != 1)
            {
                return Utilities.CreateObject
                (
                    ObjectKind.Error,
                    Error.CreateEvaluationError(AssertionErrorKind.InvalidArgument, "unexpected number of arguments")
                );
            }

            var obj = args.First();

            if (obj.Kind != ObjectKind.Array)
            {
                return Utilities.CreateObject
                (
                    ObjectKind.Error,
                    Error.CreateEvaluationError(AssertionErrorKind.InvalidArgument, obj.Kind)
                );
            }

            var array = (List<Object>)obj.Value;

            return array.Count > 0 ? array.Last() : Utilities.CreateObject(ObjectKind.Null, null);
        };

        private static Func<List<Object>, Object> Rest = (List<Object> args) =>
        {
            if (args == null || args.Count != 1)
            {
                return Utilities.CreateObject
                (
                    ObjectKind.Error,
                    Error.CreateEvaluationError(AssertionErrorKind.InvalidArgument, "unexpected number of arguments")
                );
            }

            var obj = args.First();

            if (obj.Kind != ObjectKind.Array)
            {
                return Utilities.CreateObject
                (
                    ObjectKind.Error,
                    Error.CreateEvaluationError(AssertionErrorKind.InvalidArgument, obj.Kind)
                );
            }

            var array = (List<Object>)obj.Value;

            return array.Count > 0 ?
                Utilities.CreateObject(ObjectKind.Array, array.Skip(1)) :
                Utilities.CreateObject(ObjectKind.Null, null)
            ;
        };

        private static Func<List<Object>, Object> Push = (List<Object> args) =>
        {
            if (args == null || args.Count != 2)
            {
                return Utilities.CreateObject
                (
                    ObjectKind.Error,
                    Error.CreateEvaluationError(AssertionErrorKind.InvalidArgument, "unexpected number of arguments")
                );
            }

            var obj = args.First();

            if (obj.Kind != ObjectKind.Array)
            {
                return Utilities.CreateObject
                (
                    ObjectKind.Error,
                    Error.CreateEvaluationError(AssertionErrorKind.InvalidArgument, obj.Kind)
                );
            }

            var array = (List<Object>)obj.Value;
            array.Add(args.Last());

            return Utilities.CreateObject(ObjectKind.Array, array);
        };
    }
}
