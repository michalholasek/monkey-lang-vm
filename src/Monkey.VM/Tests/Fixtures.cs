using System.Collections.Generic;

using Monkey.Shared;
using static Monkey.Evaluator.Utilities;
using Object = Monkey.Shared.Object;

namespace Monkey.Tests.Fixtures
{
    public static class VM
    {
        public static class Expression
        {
            public static Dictionary<string, Object> Boolean = new Dictionary<string, Object>
            {
                {
                    "true",
                    CreateObject(ObjectKind.Boolean, true)
                },
                {
                    "false",
                    CreateObject(ObjectKind.Boolean, false)
                }
            };

            public static Dictionary<string, Object> Integer = new Dictionary<string, Object>
            {
                {
                    "1 + 2",
                    CreateObject(ObjectKind.Integer, 3)
                },
                {
                    "2 - 1",
                    CreateObject(ObjectKind.Integer, 1)
                },
                {
                    "2 * 2",
                    CreateObject(ObjectKind.Integer, 4)
                },
                {
                    "4 / 2",
                    CreateObject(ObjectKind.Integer, 2)
                },
                {
                    "50 / 2 * 2 + 10 - 5",
                    CreateObject(ObjectKind.Integer, 55)
                },
                {
                    "5 + 5 + 5 + 5 - 10",
                    CreateObject(ObjectKind.Integer, 10)
                },
                {
                    "2 * 2 * 2 * 2 * 2",
                    CreateObject(ObjectKind.Integer, 32)
                },
                {
                    "5 * 2 + 10",
                    CreateObject(ObjectKind.Integer, 20)
                },
                {
                    "5 + 2 * 10",
                    CreateObject(ObjectKind.Integer, 25)
                },
                {
                    "5 * (2 + 10)",
                    CreateObject(ObjectKind.Integer, 60)
                }
            };
        }
    }
}
