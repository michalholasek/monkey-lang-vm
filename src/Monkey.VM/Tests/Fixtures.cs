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
                },
                {
                    "1 < 2",
                    CreateObject(ObjectKind.Boolean, true)
                },
                {
                    "1 > 2",
                    CreateObject(ObjectKind.Boolean, false)
                },
                {
                    "1 < 1",
                    CreateObject(ObjectKind.Boolean, false)
                },
                {
                    "1 > 1",
                    CreateObject(ObjectKind.Boolean, false)
                },
                {
                    "1 == 1",
                    CreateObject(ObjectKind.Boolean, true)
                },
                {
                    "1 != 1",
                    CreateObject(ObjectKind.Boolean, false)
                },
                {
                    "1 == 2",
                    CreateObject(ObjectKind.Boolean, false)
                },
                {
                    "1 != 2",
                    CreateObject(ObjectKind.Boolean, true)
                },
                {
                    "true == true",
                    CreateObject(ObjectKind.Boolean, true)
                },
                {
                    "false == false",
                    CreateObject(ObjectKind.Boolean, true)
                },
                {
                    "true == false",
                    CreateObject(ObjectKind.Boolean, false)
                },
                {
                    "true != false",
                    CreateObject(ObjectKind.Boolean, true)
                },
                {
                    "false != true",
                    CreateObject(ObjectKind.Boolean, true)
                },
                {
                    "(1 < 2) == true",
                    CreateObject(ObjectKind.Boolean, true)
                },
                {
                    "(1 < 2) == false",
                    CreateObject(ObjectKind.Boolean, false)
                },
                {
                    "(1 > 2) == true",
                    CreateObject(ObjectKind.Boolean, false)
                },
                {
                    "(1 > 2) == false",
                    CreateObject(ObjectKind.Boolean, true)
                },
                {
                    "!true",
                    CreateObject(ObjectKind.Boolean, false)
                },
                {
                    "!false",
                    CreateObject(ObjectKind.Boolean, true)
                },
                {
                    "!5",
                    CreateObject(ObjectKind.Boolean, false)
                },
                {
                    "!!true",
                    CreateObject(ObjectKind.Boolean, true)
                },
                {
                    "!!false",
                    CreateObject(ObjectKind.Boolean, false)
                },
                {
                    "!!5",
                    CreateObject(ObjectKind.Boolean, true)
                },
                {
                    "!(if (false) { 5; })",
                    CreateObject(ObjectKind.Boolean, true)
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
                },
                {
                    "-5",
                    CreateObject(ObjectKind.Integer, -5)
                },
                {
                    "-10",
                    CreateObject(ObjectKind.Integer, -10)
                },
                {
                    "-50 + 100 + -50",
                    CreateObject(ObjectKind.Integer, 0)
                },
                {
                    "(5 + 10 * 2 + 15 / 3) * 2 + -10",
                    CreateObject(ObjectKind.Integer, 50)
                }
            };

            public static Dictionary<string, Object> IfElse = new Dictionary<string, Object>
            {
                {
                    "if (true) { 10; }",
                    CreateObject(ObjectKind.Integer, 10)
                },
                {
                    "if (true) { 10; } else { 20; }",
                    CreateObject(ObjectKind.Integer, 10)
                },
                {
                    "if (false) { 10; } else { 20; }",
                    CreateObject(ObjectKind.Integer, 20)
                },
                {
                    "if (1) { 10; }",
                    CreateObject(ObjectKind.Integer, 10)
                },
                {
                    "if (1 < 2) { 10; }",
                    CreateObject(ObjectKind.Integer, 10)
                },
                {
                    "if (1 < 2) { 10; } else { 20; }",
                    CreateObject(ObjectKind.Integer, 10)
                },
                {
                    "if (1 > 2) { 10; } else { 20; }",
                    CreateObject(ObjectKind.Integer, 20)
                },
                {
                    "if (1 > 2) { 10; }",
                    CreateObject(ObjectKind.Null, null)
                },
                {
                    "if (false) { 10; }",
                    CreateObject(ObjectKind.Null, null)
                },
                {
                    "if ((if (false) { 10; })) { 10; } else { 20; }",
                    CreateObject(ObjectKind.Integer, 20)
                }
            };

            public static Dictionary<string, Object> String = new Dictionary<string, Object>
            {
                {
                    "\"monkey\"",
                    CreateObject(ObjectKind.String, "monkey")
                },
                {
                    "\"mon\" + \"key\"",
                    CreateObject(ObjectKind.String, "monkey")
                },
                {
                    "\"mon\" + \"key\" + \"banana\"",
                    CreateObject(ObjectKind.String, "monkeybanana")
                }
            };

            public static Dictionary<string, Object> Array = new Dictionary<string, Object>
            {
                {
                    "[]",
                    CreateObject(ObjectKind.Array, new List<Object>())
                },
                {
                    "[1, 2, 3]",
                    CreateObject(ObjectKind.Array, new List<Object>
                    {
                        new Object
                        {
                            Kind = ObjectKind.Integer,
                            Value = 1
                        },
                        new Object
                        {
                            Kind = ObjectKind.Integer,
                            Value = 2
                        },
                        new Object
                        {
                            Kind = ObjectKind.Integer,
                            Value = 3
                        }
                    })
                },
                {
                    "[1 + 2, 3 * 4, 5 + 6]",
                    CreateObject(ObjectKind.Array, new List<Object>
                    {
                        new Object
                        {
                            Kind = ObjectKind.Integer,
                            Value = 3
                        },
                        new Object
                        {
                            Kind = ObjectKind.Integer,
                            Value = 12
                        },
                        new Object
                        {
                            Kind = ObjectKind.Integer,
                            Value = 11
                        }
                    })
                },
                {
                    "[1, 2, 3][1]",
                    CreateObject(ObjectKind.Integer, 2)
                },
                {
                    "[1, 2, 3][0 + 2]",
                    CreateObject(ObjectKind.Integer, 3)
                },
                {
                    "[[1, 1, 1]][0][0]",
                    CreateObject(ObjectKind.Integer, 1)
                },
                {
                    "[][0]",
                    CreateObject(ObjectKind.Null, null)
                },
                {
                    "[1, 2, 3][99]",
                    CreateObject(ObjectKind.Null, null)
                },
                {
                    "[1][-1]",
                    CreateObject(ObjectKind.Null, null)
                }
            };

            public static Dictionary<string, Object> Hash = new Dictionary<string, Object>
            {
                {
                    "{}",
                    CreateObject(ObjectKind.Hash, new Dictionary<string, Object>())
                },
                {
                    "{ 1: 2, 2: 3 }",
                    CreateObject(ObjectKind.Hash, new Dictionary<string, Object>()
                    {
                        { "1", CreateObject(ObjectKind.Integer, 2) },
                        { "2", CreateObject(ObjectKind.Integer, 3) }
                    })
                },
                {
                    "{ 1 + 1: 2 * 2, 3 + 3: 4 * 4 }",
                    CreateObject(ObjectKind.Hash, new Dictionary<string, Object>()
                    {
                        { "2", CreateObject(ObjectKind.Integer, 4) },
                        { "6", CreateObject(ObjectKind.Integer, 16) }
                    })
                },
                {
                    "{ 1: 1, 2: 2 }[1]",
                    CreateObject(ObjectKind.Integer, 1)
                },
                {
                    "{ 1: 1, 2: 2 }[2]",
                    CreateObject(ObjectKind.Integer, 2)
                },
                {
                    "{ 1: 1 }[0]",
                    CreateObject(ObjectKind.Null, null)
                },
                {
                    "{}[0]",
                    CreateObject(ObjectKind.Null, null)
                }
            };

            public static Dictionary<string, Object> Function = new Dictionary<string, Object>
            {
                {
                    "let fivePlusTen = fn() { 5 + 10; }; fivePlusTen();",
                    CreateObject(ObjectKind.Integer, 15)
                },
                {
                    "let one = fn() { 1; }; let two = fn() { 2; }; one() + two();",
                    CreateObject(ObjectKind.Integer, 3)
                },
                {
                    "let a = fn() { 1; }; let b = fn() { a() + 1; }; let c = fn() { b() + 1; }; c();",
                    CreateObject(ObjectKind.Integer, 3)
                },
                {
                    "let earlyExit = fn() { return 99; 100; }; earlyExit();",
                    CreateObject(ObjectKind.Integer, 99)
                },
                {
                    "let earlyExit = fn() { return 99; return 100; }; earlyExit();",
                    CreateObject(ObjectKind.Integer, 99)
                },
                {
                    "let noReturn = fn() { }; noReturn();",
                    CreateObject(ObjectKind.Null, null)
                },
                {
                    "let noReturn = fn() { }; let noReturnTwo = fn() { noReturn(); }; noReturn(); noReturnTwo();",
                    CreateObject(ObjectKind.Null, null)
                },
                {
                    "let returnsOne = fn() { 1; }; let returnsOneReturner = fn() { returnsOne; }; returnsOneReturner()();",
                    CreateObject(ObjectKind.Integer, 1)
                },
                {
                    "let one = fn() { let one = 1; one }; one();",
                    CreateObject(ObjectKind.Integer, 1)
                },
                {
                    "let oneAndTwo = fn() { let one = 1; let two = 2; one + two; }; oneAndTwo();",
                    CreateObject(ObjectKind.Integer, 3)
                },
                {
                    "let oneAndTwo = fn() { let one = 1; let two = 2; one + two; }; let threeAndFour = fn() { let three = 3; let four = 4; three + four; }; oneAndTwo() + threeAndFour();",
                    CreateObject(ObjectKind.Integer, 10)
                },
                {
                    "let firstFoobar = fn() { let foobar = 50; foobar; }; let secondFoobar = fn() { let foobar = 100; foobar; }; firstFoobar() + secondFoobar();",
                    CreateObject(ObjectKind.Integer, 150)
                },
                {
                    "let globalSeed = 50; let minusOne = fn() { let num = 1; globalSeed - num; }; let minusTwo = fn() { let num = 2; globalSeed - num; }; minusOne() + minusTwo();",
                    CreateObject(ObjectKind.Integer, 97)
                },
                {
                    "let returnsOneReturner = fn() { let returnsOne = fn() { 1; }; returnsOne; }; returnsOneReturner()();",
                    CreateObject(ObjectKind.Integer, 1)
                },
                {
                    "let identity = fn(a) { a; }; identity(4);",
                    CreateObject(ObjectKind.Integer, 4)
                },
                {
                    "let sum = fn(a, b) { a + b; }; sum(1, 2);",
                    CreateObject(ObjectKind.Integer, 3)
                },
                {
                    "let sum = fn(a, b) { let c = a + b; c; }; sum(1, 2);",
                    CreateObject(ObjectKind.Integer, 3)
                },
                {
                    "let sum = fn(a, b) { let c = a + b; c; }; sum(1, 2) + sum(3, 4);",
                    CreateObject(ObjectKind.Integer, 10)
                },
                {
                    "let sum = fn(a, b) { let c = a + b; c; }; let outer = fn() { sum(1, 2) + sum(3, 4); }; outer();",
                    CreateObject(ObjectKind.Integer, 10)
                },
                {
                    "let globalNum = 10; let sum = fn(a, b) { let c = a + b; c + globalNum; }; let outer = fn() { sum(1, 2) + sum(3, 4) + globalNum; }; outer() + globalNum;",
                    CreateObject(ObjectKind.Integer, 50)
                }
            };
        }

        public static class Statement
        {
            public static Dictionary<string, Object> Let = new Dictionary<string, Object>
            {
                {
                    "let one = 1; one;",
                    CreateObject(ObjectKind.Integer, 1)
                },
                {
                    "let one = 1; let two = 2; one + two;",
                    CreateObject(ObjectKind.Integer, 3)
                },
                {
                    "let one = 1; let two = one + one; one + two;",
                    CreateObject(ObjectKind.Integer, 3)
                }
            };
        }
    }
}
