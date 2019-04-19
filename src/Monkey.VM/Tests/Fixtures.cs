using System.Collections.Generic;

using Monkey.Shared;
using static Monkey.Evaluator.Utilities;
using Object = Monkey.Shared.Object;

namespace Monkey.Tests.Fixtures
{
    public static class VM
    {
        public static Dictionary<string, Object> Run = new Dictionary<string, Object>
        {
            {
                "1 + 2",
                CreateObject(ObjectKind.Integer, 3)
            }
        };
    }
}
