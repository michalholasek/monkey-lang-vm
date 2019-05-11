using System;

using Monkey.Shared;
using Object = Monkey.Shared.Object;

namespace Monkey
{
    public partial class Evaluator
    {
        public static class Utilities
        {        
            public static Object CreateObject(ObjectKind kind, object value)
            {
                return new Object
                {
                    Kind = kind,
                    Value = value
                };
            }

            public static bool IsTruthy(Object obj)
            {
                switch (obj.Kind)
                {
                    case ObjectKind.Boolean:
                        return (bool)obj.Value;
                    case ObjectKind.Integer:
                        return (int)obj.Value != 0 ? true : false;
                    case ObjectKind.String:
                        return (string)obj.Value != String.Empty ? true : false;
                    case ObjectKind.Null:
                        return false;
                    default:
                        return false;
                }
            }
        }
    }
}
