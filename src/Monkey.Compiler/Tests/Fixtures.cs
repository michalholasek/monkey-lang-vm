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
                        1, 0, 0, // 1
                        1, 1, 0, // 2
                        11,      // >
                        3        // Pop
                    }
                },
                {
                    "1 < 2",
                    new List<byte>
                    {
                        1, 0, 0, // 2
                        1, 1, 0, // 1
                        11,      // >
                        3        // Pop
                    }
                },
                {
                    "1 == 2",
                    new List<byte>
                    {
                        1, 0, 0, // 1
                        1, 1, 0, // 2
                        9,       // ==
                        3        // Pop
                    }
                },
                {
                    "1 != 2",
                    new List<byte>
                    {
                        1, 0, 0, // 1
                        1, 1, 0, // 2
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
                        1, 0, 0, // 1
                        1, 1, 0, // 2
                        2,       // +
                        3        // Pop
                    }
                },
                {
                    "2 - 1",
                    new List<byte>
                    {
                        1, 0, 0, // 2
                        1, 1, 0, // 1
                        4,       // -
                        3        // Pop
                    }
                },
                {
                    "2 * 2",
                    new List<byte>
                    {
                        1, 0, 0, // 2
                        1, 0, 0, // 2
                        5,       // *
                        3        // Pop
                    }
                },
                {
                    "4 / 2",
                    new List<byte>
                    {
                        1, 0, 0, // 4
                        1, 1, 0, // 2
                        6,       // /
                        3        // Pop
                    }
                },
                {
                    "50 / 2 * 2 + 10 - 5",
                    new List<byte>
                    {
                        1, 0, 0, // 50
                        1, 1, 0, // 2
                        6,       // /
                        1, 1, 0, // 2
                        5,       // *
                        1, 2, 0, // 10
                        2,       // +
                        1, 3, 0, // 5
                        4,       // -
                        3        // Pop
                    }
                },
                {
                    "5 + 5 + 5 + 5 - 10",
                    new List<byte>
                    {
                        1, 0, 0, // 5
                        1, 0, 0, // 5
                        2,       // +
                        1, 0, 0, // 5
                        2,       // +
                        1, 0, 0, // 5
                        2,       // +
                        1, 1, 0, // 10
                        4,       // -
                        3        // Pop
                    }
                },
                {
                    "2 * 2 * 2 * 2 * 2",
                    new List<byte>
                    {
                        1, 0, 0,  // 2
                        1, 0, 0,  // 2
                        5,        // *
                        1, 0, 0,  // 2
                        5,        // *
                        1, 0, 0,  // 2
                        5,        // *
                        1, 0, 0,  // 2
                        5,        // *
                        3         // Pop
                    }
                },
                {
                    "5 * 2 + 10",
                    new List<byte>
                    {
                        1, 0, 0, // 5
                        1, 1, 0, // 2
                        5,       // *
                        1, 2, 0, // 10
                        2,       // +
                        3        // Pop
                    }
                },
                {
                    "5 + 2 * 10",
                    new List<byte>
                    {
                        1, 0, 0,  // 5
                        1, 1, 0,  // 2
                        1, 2, 0,  // 10
                        5,        // *
                        2,        // +
                        3         // Pop
                    }
                },
                {
                    "5 * (2 + 10)",
                    new List<byte>
                    {
                        1, 0, 0, // 5
                        1, 1, 0, // 2
                        1, 2, 0, // 10
                        2,       // +
                        5,       // *
                        3        // Pop
                    }
                },
                {
                    "-1",
                    new List<byte>
                    {
                        1, 0, 0, // 1
                        12,      // -
                        3        // Pop
                    }
                }
            };

            public static Dictionary<string, List<byte>> IfElse = new Dictionary<string, List<byte>>
            {
                {
                    "if (true) { 10; }; 3333;",
                    new List<byte>
                    {
                        7,         // true
                        15, 10, 0, // JumpNotTruthy, 10
                        1, 0, 0,   // 10
                        14, 11, 0, // Jump, 11
                        16,        // Null
                        3,         // Pop
                        1, 1, 0,   // 3333
                        3          // Pop
                    }
                },
                {
                    "if (true) { 10; } else { 20; }; 3333;",
                    new List<byte>
                    {
                        7,         // true
                        15, 10, 0, // JumpNotTruthy, 10
                        1, 0, 0,   // 10
                        14, 13, 0, // Jump, 13
                        1, 1, 0,   // 20
                        3,         // Pop
                        1, 2, 0,   // 3333
                        3          // Pop
                    }
                }
            };

            public static Dictionary<string, List<byte>> String = new Dictionary<string, List<byte>>
            {
                {
                    "\"monkey\"",
                    new List<byte>
                    {
                        1, 0, 0, // Constant "monkey"
                        3        // Pop
                    }
                },
                {
                    "\"mon\" + \"key\"",
                    new List<byte>
                    {
                        1, 0, 0, // Constant "mon"
                        1, 1, 0, // Constant "key"
                        2,       // Add
                        3        // Pop
                    }
                }
            };

            public static Dictionary<string, List<byte>> Array = new Dictionary<string, List<byte>>
            {
                {
                    "[]",
                    new List<byte>
                    {
                        19, 0, 0, // Array with 0 elements
                        3         // Pop
                    }
                },
                {
                    "[1, 2, 3]",
                    new List<byte>
                    {
                        1, 0, 0,  // 1
                        1, 1, 0,  // 2
                        1, 2, 0,  // 3
                        19, 3, 0, // Array, 3 elements
                        3         // Pop
                    }
                },
                {
                    "[1 + 2, 3 - 4, 5 * 6]",
                    new List<byte>
                    {
                        1, 0, 0,  // 1
                        1, 1, 0,  // 2
                        2,        // Add
                        1, 2, 0,  // 3
                        1, 3, 0,  // 4
                        4,        // -
                        1, 4, 0,  // 5
                        1, 5, 0,  // 6
                        5,        // *
                        19, 3, 0, // Array, 3 elements
                        3         // Pop
                    }
                }
            };

            public static Dictionary<string, List<byte>> Hash = new Dictionary<string, List<byte>>
            {
                {
                    "{}",
                    new List<byte>
                    {
                        20, 0, 0, // Hash with 0 key/value pairs
                        3         // Pop
                    }
                },
                {
                    "{ 1: 2, 3: 4, 5: 6 }",
                    new List<byte>
                    {
                        1, 0, 0,  // 1
                        1, 1, 0,  // 2
                        1, 2, 0,  // 3
                        1, 3, 0,  // 4
                        1, 4, 0,  // 5
                        1, 5, 0,  // 6
                        20, 3, 0, // Hash, 3 key/value pairs
                        3         // Pop
                    }
                },
                {
                    "{ 1: 2 + 3, 4: 5 * 6 }",
                    new List<byte>
                    {
                        1, 0, 0,  // 1
                        1, 1, 0,  // 2
                        1, 2, 0,  // 3
                        2,        // Add
                        1, 3, 0,  // 4
                        1, 4, 0,  // 5
                        1, 5, 0,  // 6
                        5,        // Multiply
                        20, 2, 0, // Hash, 2 key/value pairs
                        3         // Pop
                    }
                }
            };
        }

        public static class Statement
        {
            public static Dictionary<string, List<byte>> Let = new Dictionary<string, List<byte>>
            {
                {
                    "let one = 1; let two = 2;",
                    new List<byte>
                    {
                        1, 0, 0,  // 1
                        17, 0, 0, // SetGlobal 1
                        1, 1, 0,  // 2
                        17, 1, 0  // SetGlobal 2
                    }
                },
                {
                    "let one = 1; one;",
                    new List<byte>
                    {
                        1, 0, 0,  // 1
                        17, 0, 0, // SetGlobal 1
                        18, 0, 0, // GetGlobal 1
                        3         // Pop
                    }
                },
                {
                    "let one = 1; let two = one; two;",
                    new List<byte>
                    {
                        1, 0, 0,  // 1
                        17, 0, 0, // SetGlobal 1
                        18, 0, 0, // GetGlobal 1
                        17, 1, 0, // SetGlobal 1
                        18, 1, 0, // GetGlobal 1
                        3         // Pop
                    }
                }
            };

            public static Dictionary<string, List<AssertionError>> Error = new Dictionary<string, List<AssertionError>>
            {
                { "a;", new List<AssertionError> { new AssertionError { Message = "undefined variable: a" } } }
            };
        }
    }
}
