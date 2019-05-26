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
                    Object.Create(ObjectKind.Boolean, true)
                },
                {
                    "false",
                    Object.Create(ObjectKind.Boolean, false)
                },
                {
                    "1 < 2",
                    Object.Create(ObjectKind.Boolean, true)
                },
                {
                    "1 > 2",
                    Object.Create(ObjectKind.Boolean, false)
                },
                {
                    "1 < 1",
                    Object.Create(ObjectKind.Boolean, false)
                },
                {
                    "1 > 1",
                    Object.Create(ObjectKind.Boolean, false)
                },
                {
                    "1 == 1",
                    Object.Create(ObjectKind.Boolean, true)
                },
                {
                    "1 != 1",
                    Object.Create(ObjectKind.Boolean, false)
                },
                {
                    "1 == 2",
                    Object.Create(ObjectKind.Boolean, false)
                },
                {
                    "1 != 2",
                    Object.Create(ObjectKind.Boolean, true)
                },
                {
                    "true == true",
                    Object.Create(ObjectKind.Boolean, true)
                },
                {
                    "false == false",
                    Object.Create(ObjectKind.Boolean, true)
                },
                {
                    "true == false",
                    Object.Create(ObjectKind.Boolean, false)
                },
                {
                    "true != false",
                    Object.Create(ObjectKind.Boolean, true)
                },
                {
                    "false != true",
                    Object.Create(ObjectKind.Boolean, true)
                },
                {
                    "(1 < 2) == true",
                    Object.Create(ObjectKind.Boolean, true)
                },
                {
                    "(1 < 2) == false",
                    Object.Create(ObjectKind.Boolean, false)
                },
                {
                    "(1 > 2) == true",
                    Object.Create(ObjectKind.Boolean, false)
                },
                {
                    "(1 > 2) == false",
                    Object.Create(ObjectKind.Boolean, true)
                },
                {
                    "!true",
                    Object.Create(ObjectKind.Boolean, false)
                },
                {
                    "!false",
                    Object.Create(ObjectKind.Boolean, true)
                },
                {
                    "!5",
                    Object.Create(ObjectKind.Boolean, false)
                },
                {
                    "!!true",
                    Object.Create(ObjectKind.Boolean, true)
                },
                {
                    "!!false",
                    Object.Create(ObjectKind.Boolean, false)
                },
                {
                    "!!5",
                    Object.Create(ObjectKind.Boolean, true)
                },
                {
                    "!(if (false) { 5; })",
                    Object.Create(ObjectKind.Boolean, true)
                }
            };

            public static Dictionary<string, Object> Integer = new Dictionary<string, Object>
            {
                {
                    "1 + 2",
                    Object.Create(ObjectKind.Integer, 3)
                },
                {
                    "2 - 1",
                    Object.Create(ObjectKind.Integer, 1)
                },
                {
                    "2 * 2",
                    Object.Create(ObjectKind.Integer, 4)
                },
                {
                    "4 / 2",
                    Object.Create(ObjectKind.Integer, 2)
                },
                {
                    "50 / 2 * 2 + 10 - 5",
                    Object.Create(ObjectKind.Integer, 55)
                },
                {
                    "5 + 5 + 5 + 5 - 10",
                    Object.Create(ObjectKind.Integer, 10)
                },
                {
                    "2 * 2 * 2 * 2 * 2",
                    Object.Create(ObjectKind.Integer, 32)
                },
                {
                    "5 * 2 + 10",
                    Object.Create(ObjectKind.Integer, 20)
                },
                {
                    "5 + 2 * 10",
                    Object.Create(ObjectKind.Integer, 25)
                },
                {
                    "5 * (2 + 10)",
                    Object.Create(ObjectKind.Integer, 60)
                },
                {
                    "-5",
                    Object.Create(ObjectKind.Integer, -5)
                },
                {
                    "-10",
                    Object.Create(ObjectKind.Integer, -10)
                },
                {
                    "-50 + 100 + -50",
                    Object.Create(ObjectKind.Integer, 0)
                },
                {
                    "(5 + 10 * 2 + 15 / 3) * 2 + -10",
                    Object.Create(ObjectKind.Integer, 50)
                }
            };

            public static Dictionary<string, Object> IfElse = new Dictionary<string, Object>
            {
                {
                    "if (true) { 10; }",
                    Object.Create(ObjectKind.Integer, 10)
                },
                {
                    "if (true) { 10; } else { 20; }",
                    Object.Create(ObjectKind.Integer, 10)
                },
                {
                    "if (false) { 10; } else { 20; }",
                    Object.Create(ObjectKind.Integer, 20)
                },
                {
                    "if (1) { 10; }",
                    Object.Create(ObjectKind.Integer, 10)
                },
                {
                    "if (1 < 2) { 10; }",
                    Object.Create(ObjectKind.Integer, 10)
                },
                {
                    "if (1 < 2) { 10; } else { 20; }",
                    Object.Create(ObjectKind.Integer, 10)
                },
                {
                    "if (1 > 2) { 10; } else { 20; }",
                    Object.Create(ObjectKind.Integer, 20)
                },
                {
                    "if (1 > 2) { 10; }",
                    Object.Create(ObjectKind.Null, null)
                },
                {
                    "if (false) { 10; }",
                    Object.Create(ObjectKind.Null, null)
                },
                {
                    "if ((if (false) { 10; })) { 10; } else { 20; }",
                    Object.Create(ObjectKind.Integer, 20)
                }
            };

            public static Dictionary<string, Object> String = new Dictionary<string, Object>
            {
                {
                    "\"monkey\"",
                    Object.Create(ObjectKind.String, "monkey")
                },
                {
                    "\"mon\" + \"key\"",
                    Object.Create(ObjectKind.String, "monkey")
                },
                {
                    "\"mon\" + \"key\" + \"banana\"",
                    Object.Create(ObjectKind.String, "monkeybanana")
                }
            };

            public static Dictionary<string, Object> Array = new Dictionary<string, Object>
            {
                {
                    "[]",
                    Object.Create(ObjectKind.Array, new List<Object>())
                },
                {
                    "[1, 2, 3]",
                    Object.Create(ObjectKind.Array, new List<Object>
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
                    Object.Create(ObjectKind.Array, new List<Object>
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
                    Object.Create(ObjectKind.Integer, 2)
                },
                {
                    "[1, 2, 3][0 + 2]",
                    Object.Create(ObjectKind.Integer, 3)
                },
                {
                    "[[1, 1, 1]][0][0]",
                    Object.Create(ObjectKind.Integer, 1)
                },
                {
                    "[][0]",
                    Object.Create(ObjectKind.Null, null)
                },
                {
                    "[1, 2, 3][99]",
                    Object.Create(ObjectKind.Null, null)
                },
                {
                    "[1][-1]",
                    Object.Create(ObjectKind.Null, null)
                }
            };

            public static Dictionary<string, Object> Hash = new Dictionary<string, Object>
            {
                {
                    "{}",
                    Object.Create(ObjectKind.Hash, new Dictionary<string, Object>())
                },
                {
                    "{ 1: 2, 2: 3 }",
                    Object.Create(ObjectKind.Hash, new Dictionary<string, Object>()
                    {
                        { "1", Object.Create(ObjectKind.Integer, 2) },
                        { "2", Object.Create(ObjectKind.Integer, 3) }
                    })
                },
                {
                    "{ 1 + 1: 2 * 2, 3 + 3: 4 * 4 }",
                    Object.Create(ObjectKind.Hash, new Dictionary<string, Object>()
                    {
                        { "2", Object.Create(ObjectKind.Integer, 4) },
                        { "6", Object.Create(ObjectKind.Integer, 16) }
                    })
                },
                {
                    "{ 1: 1, 2: 2 }[1]",
                    Object.Create(ObjectKind.Integer, 1)
                },
                {
                    "{ 1: 1, 2: 2 }[2]",
                    Object.Create(ObjectKind.Integer, 2)
                },
                {
                    "{ 1: 1 }[0]",
                    Object.Create(ObjectKind.Null, null)
                },
                {
                    "{}[0]",
                    Object.Create(ObjectKind.Null, null)
                }
            };

            public static Dictionary<string, Object> Function = new Dictionary<string, Object>
            {
                {
                    "let fivePlusTen = fn() { 5 + 10; }; fivePlusTen();",
                    Object.Create(ObjectKind.Integer, 15)
                },
                {
                    "let one = fn() { 1; }; let two = fn() { 2; }; one() + two();",
                    Object.Create(ObjectKind.Integer, 3)
                },
                {
                    "let a = fn() { 1; }; let b = fn() { a() + 1; }; let c = fn() { b() + 1; }; c();",
                    Object.Create(ObjectKind.Integer, 3)
                },
                {
                    "let earlyExit = fn() { return 99; 100; }; earlyExit();",
                    Object.Create(ObjectKind.Integer, 99)
                },
                {
                    "let earlyExit = fn() { return 99; return 100; }; earlyExit();",
                    Object.Create(ObjectKind.Integer, 99)
                },
                {
                    "let noReturn = fn() { }; noReturn();",
                    Object.Create(ObjectKind.Null, null)
                },
                {
                    "let noReturn = fn() { }; let noReturnTwo = fn() { noReturn(); }; noReturn(); noReturnTwo();",
                    Object.Create(ObjectKind.Null, null)
                },
                {
                    "let returnsOne = fn() { 1; }; let returnsOneReturner = fn() { returnsOne; }; returnsOneReturner()();",
                    Object.Create(ObjectKind.Integer, 1)
                },
                {
                    "let one = fn() { let one = 1; one }; one();",
                    Object.Create(ObjectKind.Integer, 1)
                },
                {
                    "let oneAndTwo = fn() { let one = 1; let two = 2; one + two; }; oneAndTwo();",
                    Object.Create(ObjectKind.Integer, 3)
                },
                {
                    "let oneAndTwo = fn() { let one = 1; let two = 2; one + two; }; let threeAndFour = fn() { let three = 3; let four = 4; three + four; }; oneAndTwo() + threeAndFour();",
                    Object.Create(ObjectKind.Integer, 10)
                },
                {
                    "let firstFoobar = fn() { let foobar = 50; foobar; }; let secondFoobar = fn() { let foobar = 100; foobar; }; firstFoobar() + secondFoobar();",
                    Object.Create(ObjectKind.Integer, 150)
                },
                {
                    "let globalSeed = 50; let minusOne = fn() { let num = 1; globalSeed - num; }; let minusTwo = fn() { let num = 2; globalSeed - num; }; minusOne() + minusTwo();",
                    Object.Create(ObjectKind.Integer, 97)
                },
                {
                    "let returnsOneReturner = fn() { let returnsOne = fn() { 1; }; returnsOne; }; returnsOneReturner()();",
                    Object.Create(ObjectKind.Integer, 1)
                },
                {
                    "let identity = fn(a) { a; }; identity(4);",
                    Object.Create(ObjectKind.Integer, 4)
                },
                {
                    "let sum = fn(a, b) { a + b; }; sum(1, 2);",
                    Object.Create(ObjectKind.Integer, 3)
                },
                {
                    "let sum = fn(a, b) { let c = a + b; c; }; sum(1, 2);",
                    Object.Create(ObjectKind.Integer, 3)
                },
                {
                    "let sum = fn(a, b) { let c = a + b; c; }; sum(1, 2) + sum(3, 4);",
                    Object.Create(ObjectKind.Integer, 10)
                },
                {
                    "let sum = fn(a, b) { let c = a + b; c; }; let outer = fn() { sum(1, 2) + sum(3, 4); }; outer();",
                    Object.Create(ObjectKind.Integer, 10)
                },
                {
                    "let globalNum = 10; let sum = fn(a, b) { let c = a + b; c + globalNum; }; let outer = fn() { sum(1, 2) + sum(3, 4) + globalNum; }; outer() + globalNum;",
                    Object.Create(ObjectKind.Integer, 50)
                },
                {
                    "let newClosure = fn(a) { fn() { a; }; }; let closure = newClosure(99); closure();",
                    Object.Create(ObjectKind.Integer, 99)
                },
                {
                    "let newAdder = fn(a, b) { fn(c) { a + b + c; }; }; let adder = newAdder(1, 2); adder(8);",
                    Object.Create(ObjectKind.Integer, 11)
                },
                {
                    "let newAdder = fn(a, b) { let c = a + b; fn(d) { c + d; }; }; let adder = newAdder(1, 2); adder(8);",
                    Object.Create(ObjectKind.Integer, 11)
                },
                {
                    "let newAdderOuter = fn(a, b) { let c = a + b; fn(d) { let e = d + c; fn(f) { e + f; }; }; }; let newAdderInner = newAdderOuter(1, 2); let adder = newAdderInner(3); adder(8);",
                    Object.Create(ObjectKind.Integer, 14)
                },
                {
                    "let a = 1; let newAdderOuter = fn(b) { fn(c) { fn(d) { a + b + c + d }; }; }; let newAdderInner = newAdderOuter(2); let adder = newAdderInner(3); adder(8);",
                    Object.Create(ObjectKind.Integer, 14)
                },
                {
                    "let newClosure = fn(a, b) { let one = fn() { a; }; let two = fn() { b; }; fn() { one() + two(); }; }; let closure = newClosure(9, 90); closure();",
                    Object.Create(ObjectKind.Integer, 99)
                },
                {
                    "let countDown = fn(x) { if (x == 0) { return 0; } else { countDown(x - 1); } }; countDown(1);",
                    Object.Create(ObjectKind.Integer, 0)
                },
                {
                    "let countDown = fn(x) { if (x == 0) { return 0; } else { countDown(x - 1); } }; let wrapper = fn() { countDown(1); }; wrapper();",
                    Object.Create(ObjectKind.Integer, 0)
                },
                {
                    "let wrapper = fn() { let countDown = fn(x) { if (x == 0) { return 0; } else { countDown(x - 1); } }; countDown(1); }; wrapper();",
                    Object.Create(ObjectKind.Integer, 0)
                },
                {
                    "let fibonacci = fn(x) { if (x == 0) { return 0; } else { if (x == 1) { return 1; } else { fibonacci(x - 1) + fibonacci(x - 2); } } }; fibonacci(15);",
                    Object.Create(ObjectKind.Integer, 610)
                }
            };

            public static Dictionary<string, Object> BuiltIn = new Dictionary<string, Object>
            {
                {
                    "len(\"\");",
                    Object.Create(ObjectKind.Integer, 0)
                },
                {
                    "len(\"four\");",
                    Object.Create(ObjectKind.Integer, 4)
                },
                {
                    "len(\"hello world\");",
                    Object.Create(ObjectKind.Integer, 11)
                },
                {
                    "len(1);",
                    Object.Create(ObjectKind.Error, new AssertionError("invalid argument: len(1<-- ), expected Array or String"))
                },
                {
                    "len(\"one\", \"two\");",
                    Object.Create(ObjectKind.Error, new AssertionError("invalid argument: len(), unexpected number of arguments"))
                },
                {
                    "len([1, 2, 3]);",
                    Object.Create(ObjectKind.Integer, 3)
                },
                {
                    "len([]);",
                    Object.Create(ObjectKind.Integer, 0)
                },
                {
                    "first([1, 2, 3]);",
                    Object.Create(ObjectKind.Integer, 1)
                },
                {
                    "first([]);",
                    Object.Create(ObjectKind.Null, null)
                },
                {
                    "first(1);",
                    Object.Create(ObjectKind.Error, new AssertionError("invalid argument: first(1<-- ), expected Array"))
                },
                {
                    "last([1, 2, 3]);",
                    Object.Create(ObjectKind.Integer, 3)
                },
                {
                    "last([]);",
                    Object.Create(ObjectKind.Null, null)
                },
                {
                    "last(1);",
                    Object.Create(ObjectKind.Error, new AssertionError("invalid argument: last(1<-- ), expected Array"))
                },
                {
                    "rest([1, 2, 3]);",
                    Object.Create(ObjectKind.Array, new List<Object>
                    {
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
                    "rest([]);",
                    Object.Create(ObjectKind.Null, null)
                },
                {
                    "rest(1);",
                    Object.Create(ObjectKind.Error, new AssertionError("invalid argument: rest(1<-- ), expected Array"))
                },
                {
                    "push([1, 2], 3);",
                    Object.Create(ObjectKind.Array, new List<Object>
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
                    "push([]);",
                    Object.Create(ObjectKind.Error, new AssertionError("invalid argument: push(), unexpected number of arguments"))
                },
                {
                    "push(1, 2);",
                    Object.Create(ObjectKind.Error, new AssertionError("invalid argument: push(1<-- , 2), expected Array as first argument"))
                }
            };
        }

        public static class Statement
        {
            public static Dictionary<string, Object> Let = new Dictionary<string, Object>
            {
                {
                    "let one = 1; one;",
                    Object.Create(ObjectKind.Integer, 1)
                },
                {
                    "let one = 1; let two = 2; one + two;",
                    Object.Create(ObjectKind.Integer, 3)
                },
                {
                    "let one = 1; let two = one + one; one + two;",
                    Object.Create(ObjectKind.Integer, 3)
                }
            };

            public static Dictionary<string, Object> Return = new Dictionary<string, Object>
            {
                {
                    "return;",
                    Object.Create(ObjectKind.Null, null)
                },
                {
                    "return 42;",
                    Object.Create(ObjectKind.Integer, 42)
                }
            };
        }
    }
}
