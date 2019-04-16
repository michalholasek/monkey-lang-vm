using System;

using Object = Monkey.Shared.Evaluator.Object;

namespace Monkey.Shared.Evaluator
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
