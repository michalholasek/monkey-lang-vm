using System.Collections.Generic;

using Monkey.Shared;

namespace Monkey.Tests.Fixtures
{
    public static class Compiler
    {
        public static class Expression
        {
            public static Dictionary<string, List<byte>> Boolean = new Dictionary<string, List<byte>>
            {
                {
                    "true",
                    new List<byte>
                    {
                        7,       // Opcode.True
                        3        // Pop
                    }
                },
                {
                    "false",
                    new List<byte>
                    {
                        8,       // Opcode.False
                        3        // Pop
                    }
                },
                {
                    "1 > 2",
                    new List<byte>
                    {
                        1, 1, 0, // 1
                        1, 2, 0, // 2
                        11,      // >
                        3        // Pop
                    }
                },
                {
                    "1 < 2",
                    new List<byte>
                    {
                        1, 2, 0, // 2
                        1, 1, 0, // 1
                        11,      // >
                        3        // Pop
                    }
                },
                {
                    "1 == 2",
                    new List<byte>
                    {
                        1, 1, 0, // 1
                        1, 2, 0, // 2
                        9,       // ==
                        3        // Pop
                    }
                },
                {
                    "1 != 2",
                    new List<byte>
                    {
                        1, 1, 0, // 1
                        1, 2, 0, // 2
                        10,       // !=
                        3        // Pop
                    }
                },
                {
                    "true == false",
                    new List<byte>
                    {
                        7, // true
                        8, // false
                        9, // ==
                        3  // Pop
                    }
                },
                {
                    "true != false",
                    new List<byte>
                    {
                        7, // true
                        8, // false
                        10, // !=
                        3  // Pop
                    }
                },
                {
                    "!true",
                    new List<byte>
                    {
                        7,  // true
                        13, // Bang
                        3   // Pop
                    }
                }
            };
            
            public static Dictionary<string, List<byte>> Integer = new Dictionary<string, List<byte>>
            {
                {
                    "1 + 2",
                    new List<byte>
                    {
                        1, 1, 0, // 1
                        1, 2, 0, // 2
                        2,       // +
                        3        // Pop
                    }
                },
                {
                    "2 - 1",
                    new List<byte>
                    {
                        1, 2, 0, // 2
                        1, 1, 0, // 1
                        4,       // -
                        3        // Pop
                    }
                },
                {
                    "2 * 2",
                    new List<byte>
                    {
                        1, 2, 0, // 2
                        1, 2, 0, // 2
                        5,       // *
                        3        // Pop
                    }
                },
                {
                    "4 / 2",
                    new List<byte>
                    {
                        1, 4, 0, // 4
                        1, 2, 0, // 2
                        6,       // /
                        3        // Pop
                    }
                },
                {
                    "50 / 2 * 2 + 10 - 5",
                    new List<byte>
                    {
                        1, 50, 0, // 4
                        1, 2, 0,  // 2
                        6,        // /
                        1, 2, 0,  // 2
                        5,        // *
                        1, 10, 0, // 10
                        2,        // +
                        1, 5, 0,  // 5
                        4,        // -
                        3         // Pop
                    }
                },
                {
                    "5 + 5 + 5 + 5 - 10",
                    new List<byte>
                    {
                        1, 5, 0,  // 5
                        1, 5, 0,  // 5
                        2,        // +
                        1, 5, 0,  // 5
                        2,        // +
                        1, 5, 0,  // 5
                        2,        // +
                        1, 10, 0, // 10
                        4,        // -
                        3         // Pop
                    }
                },
                {
                    "2 * 2 * 2 * 2 * 2",
                    new List<byte>
                    {
                        1, 2, 0,  // 2
                        1, 2, 0,  // 2
                        5,        // *
                        1, 2, 0,  // 2
                        5,        // *
                        1, 2, 0,  // 2
                        5,        // *
                        1, 2, 0,  // 2
                        5,        // *
                        3         // Pop
                    }
                },
                {
                    "5 * 2 + 10",
                    new List<byte>
                    {
                        1, 5, 0,  // 5
                        1, 2, 0,  // 2
                        5,        // *
                        1, 10, 0, // 10
                        2,        // +
                        3         // Pop
                    }
                },
                {
                    "5 + 2 * 10",
                    new List<byte>
                    {
                        1, 5, 0,  // 5
                        1, 2, 0,  // 2
                        1, 10, 0, // 10
                        5,        // *
                        2,        // +
                        3         // Pop
                    }
                },
                {
                    "5 * (2 + 10)",
                    new List<byte>
                    {
                        1, 5, 0,  // 5
                        1, 2, 0,  // 2
                        1, 10, 0, // 10
                        2,        // +
                        5,        // *
                        3         // Pop
                    }
                },
                {
                    "-1",
                    new List<byte>
                    {
                        1, 1, 0, // 1
                        12,      // -
                        3        // Pop
                    }
                }
            };
        }
    }
}
