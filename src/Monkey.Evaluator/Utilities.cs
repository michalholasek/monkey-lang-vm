using System;

using Object = Monkey.Shared.Object;

namespace Monkey.Shared
{
    public partial class Evaluator
    {
        internal static class Utilities
        {        
            public static Object CreateObject(ObjectKind kind, object value)
            {
                return new Object
                {
                    Kind = kind,
                    Value = value
                };
            }
        }
    }
}
