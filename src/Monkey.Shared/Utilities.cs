using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monkey.Shared
{
    internal static class Include
    {
        public static int Bracket { get { return 1; } }
        public static int Semicolon { get { return 1; } }
    }
    
    internal static class Skip
    {
        public static int Assign { get { return 1; } }
        public static int Brace { get { return 1; } }
        public static int Bracket { get { return 1; } }
        public static int Colon { get { return 1; } }
        public static int Comma { get { return 1; } }
        public static int Else { get { return 1; } }
        public static int EOF { get { return 1; } }
        public static int If { get { return 1; } }
        public static int Identifier { get { return 1; } }
        public static int Let { get { return 1; } }
        public static int Operator { get { return 1; } }
        public static int Parenthesis { get { return 1; } }
        public static int Return { get { return 1; } }
        public static int Semicolon { get { return 1; } }
    }

    public static class Stringify
    {
        public static string Kind(ErrorKind kind)
        {
            return Enum.GetName(typeof(ErrorKind), kind);
        }

        public static string Kind(ObjectKind kind)
        {
            return Enum.GetName(typeof(ObjectKind), kind);
        }

        public static string Kind(SyntaxKind kind)
        {
            return Enum.GetName(typeof(SyntaxKind), kind);
        }

        public static string Object(object obj)
        {
            return obj.ToString().ToLower();
        }

        public static string Object(Object obj)
        {
            switch (obj.Kind)
            {
                case ObjectKind.Array:
                    return StringifyArray(obj);
                case ObjectKind.Error:
                    return ((AssertionError)obj.Value).Message;
                case ObjectKind.Function:
                    return StringifyFunction(obj);
                case ObjectKind.Hash:
                    return StringifyHash(obj);
                case ObjectKind.Closure:
                case ObjectKind.Null:
                    return "null";
                default:
                    return obj.Value.ToString().ToLower();
                
            }
        }

        private static string StringifyArray(Object obj)
        {
            var array = (List<Object>)obj.Value;
            var sb = new StringBuilder();

            sb.Append("[");

            array.ForEach(element =>
            {
                sb.Append(Stringify.Object(element));

                if (element != array.Last())
                {
                    sb.Append(", ");
                }
            });

            sb.Append("]");

            return sb.ToString();
        }

        private static string StringifyFunction(Object obj)
        {
            var fn = obj.Value as FunctionExpression;
            var sb = new StringBuilder();

            if (fn != default(FunctionExpression))
            {
                sb.Append("fn(");

                fn.Parameters.ForEach(param =>
                {
                    sb.Append(param.Literal);

                    if (param != fn.Parameters.Last())
                    {
                        sb.Append(", ");
                    }
                });

                sb.Append(") { ... }");
            }
            else
            {
                sb.Append("null");
            }

            return sb.ToString();
        }

        private static string StringifyHash(Object obj)
        {
            var hashtable = (Dictionary<string, Object>)obj.Value;
            var sb = new StringBuilder();

            sb.Append("{ ");
            
            hashtable.Keys.ToList().ForEach(key =>
            {
                sb.Append(key);
                sb.Append(": ");
                sb.Append(Stringify.Object(hashtable[key]));
            });

            sb.Append(" }");

            return sb.ToString();
        }
    }
}
