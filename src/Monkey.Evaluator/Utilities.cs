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
        }
    }
}
