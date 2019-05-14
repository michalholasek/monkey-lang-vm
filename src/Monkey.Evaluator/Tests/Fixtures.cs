using System.Collections.Generic;

using Monkey.Shared;
using Environment = Monkey.Shared.Environment;
using Object = Monkey.Shared.Object;

namespace Monkey.Tests.Fixtures
{
    public static class Evaluation
    {
        internal static Dictionary<string, Object> Integer = new Dictionary<string, Object>
        {
            {
                "5",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 5
                }
            },
            {
                "-5",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = -5
                }
            },
            {
                "5 + 5 + 5 + 5 - 10",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 10
                }
            },
            {
                "2 * 2 * 2 * 2 * 2",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 32
                }
            },
            {
                "-50 + 100 + -50",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 0
                }
            },
            {
                "5 * 2 + 10",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 20
                }
            },
            {
                "5 + 2 * 10",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 25
                }
            },
            {
                "20 + 2 * -10",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 0
                }
            },
            {
                "50 / 2 * 2 + 10",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 60
                }
            },
            {
                "2 * (5 + 10)",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 30
                }
            },
            {
                "3 * 3 * 3 + 10",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 37
                }
            },
            {
                "(5 + 10 * 2 + 15 / 3) * 2 + -10",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 50
                }
            }
        };

        internal static Dictionary<string, Object> Boolean = new Dictionary<string, Object>
        {
            {
                "true",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = true
                }
            },
            {
                "false",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = false
                }
            },
            {
                "1 < 2",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = true
                }
            },
            {
                "1 > 2",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = false
                }
            },
            {
                "1 < 1",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = false
                }
            },
            {
                "1 > 1",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = false
                }
            },
            {
                "1 == 1",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = true
                }
            },
            {
                "1 != 1",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = false
                }
            },
            {
                "1 == 2",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = false
                }
            },
            {
                "1 != 2",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = true
                }
            },
            {
                "true == true",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = true
                }
            },
            {
                "false == false",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = true
                }
            },
            {
                "true == false",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = false
                }
            },
            {
                "true != false",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = true
                }
            },
            {
                "false != true",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = true
                }
            },
            {
                "(1 < 2) == true",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = true
                }
            },
            {
                "(1 < 2) == false",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = false
                }
            },
            {
                "(1 > 2) == true",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = false
                }
            },
            {
                "(1 > 2) == false",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = true
                }
            }
        };

        internal static Dictionary<string, Object> String = new Dictionary<string, Object>
        {
            {
                "\"foo bar\"",
                new Object
                {
                    Kind = ObjectKind.String,
                    Value = "foo bar"
                }
            },
            {
                "\"foo\" + \"bar\"",
                new Object
                {
                    Kind = ObjectKind.String,
                    Value = "foobar"
                }
            }
        };

        internal static Dictionary<string, Object> Prefix = new Dictionary<string, Object>
        {
            {
                "!true",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = false
                }
            },
            {
                "!false",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = true
                }
            },
            {
                "!5",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = false
                }
            },
            {
                "!!true",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = true
                }
            },
            {
                "!!false",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = false
                }
            },
            {
                "!!5",
                new Object
                {
                    Kind = ObjectKind.Boolean,
                    Value = true
                }
            },
            {
                "![]",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("invalid type: ![]<-- , expected Boolean or Integer")
                }
            }
        };

        internal static Dictionary<string, Object> IfElse = new Dictionary<string, Object>
        {
            {
                "if (true) { 10; }",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 10
                }
            },
            {
                "if (false) { 10; }",
                new Object
                {
                    Kind = ObjectKind.Null,
                    Value = null
                }
            },
            {
                "if (1) { 10; }",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 10
                }
            },
            {
                "if (1 < 2) { 10; }",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 10
                }
            },
            {
                "if (1 > 2) { 10; }",
                new Object
                {
                    Kind = ObjectKind.Null,
                    Value = null
                }
            },
            {
                "if (1 < 2) { 10; } else { 20; }",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 10
                }
            },
            {
                "if (1 > 2) { 10; } else { 20; }",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 20
                }
            }
        };

        internal static Dictionary<string, Object> Let = new Dictionary<string, Object>
        {
            {
                "let a = 5; a;",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 5
                }
            },
            {
                "let a = 5 * 5; a;",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 25
                }
            },
            {
                "let a = 5; let b = a; b;",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 5
                }
            },
            {
                "let a = 5; let b = a; let c = a + b + 5; c;",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 15
                }
            }
        };

        internal static Dictionary<string, Object> Return = new Dictionary<string, Object>
        {
            {
                "return 10;",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 10
                }
            },
            {
                "return 10; 9;",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 10
                }
            },
            {
                "return 2 * 5; 9;",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 10
                }
            },
            {
                "9; return 2 * 5; 9;",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 10
                }
            },
            {
                "if (10 > 1) { if (10 > 1) { return 10; } return 1; }",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 10
                }
            }
        };

        internal static Dictionary<string, Object> Illegal = new Dictionary<string, Object>
        {
            {
                "5 + true;",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("invalid type: types of 5 and true do not match")
                }
            },
            {
                "5 + true; 5;",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("invalid type: types of 5 and true do not match")
                }
            },
            {
                "-true;",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("invalid type: -true<-- , operator - is not supported for type Boolean")
                }
            },
            {
                "true + false;",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("unknown operator: true +<-- false")
                }
            },
            {
                "5; true + false; 5;",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("unknown operator: true +<-- false")
                }
            },
            {
                "if (10 > 1) { true + false; }",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("unknown operator: true +<-- false")
                }
            },
            {
                "foobar",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("invalid identifier: foobar not found")
                }
            }
        };

        internal static Dictionary<string, Object> Function = new Dictionary<string, Object>
        {
            {
                "fn(x) { x + 2; };",
                new Object
                {
                    Environment = new Environment(),
                    Kind = ObjectKind.Function,
                    Value = new FunctionExpression
                    (
                        parameters: new List<Token>
                        {
                            new Token() { Column = 5, Kind = SyntaxKind.Identifier, Line = 1, Literal = "x" }
                        },
                        body: new BlockStatement
                        {
                            Statements = new List<Statement>
                            {
                                new Statement(new StatementOptions
                                {
                                    Expression = new InfixExpression
                                    (
                                        left: new IdentifierExpression("x"),
                                        op: new Token() { Column = 12, Kind = SyntaxKind.Plus, Line = 1, Literal = "+" },
                                        right: new IntegerExpression(2)   
                                    ),
                                    Identifier = null,
                                    Kind = NodeKind.Expression,
                                    Position = 5,
                                    Range = 4
                                })
                            }
                        }
                    )
                }
            }
        };

        internal static Dictionary<string, Object> Call = new Dictionary<string, Object>
        {
            {
                "let identity = fn(x) { x; }; identity(5);",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 5
                }
            },
            {
                "let identity = fn(x) { return x; }; identity(5);",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 5
                }
            },
            {
                "let double = fn(x) { x * 2; }; double(5);",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 10
                }
            },
            {
                "let add = fn(x, y) { x + y; }; add(5, 5);",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 10
                }
            },
            {
                "let add = fn(x, y) { x + y; }; add(5 + 5, add(5, 5));",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 20
                }
            },
            {
                "fn(x) { x; }(5);",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 5
                }
            },
            {
                "let newAdder = fn(x) { fn(y) { x + y }; }; let addTwo = newAdder(2); addTwo(2);",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 4
                }
            }
        };

        internal static Dictionary<string, Object> Array = new Dictionary<string, Object>
        {
            {
                "[]",
                new Object
                {
                    Kind = ObjectKind.Array,
                    Value = new List<Object>()
                }
            },
            {
                "[1, 2 * 2, 3 + 3]",
                new Object
                {
                    Kind = ObjectKind.Array,
                    Value = new List<Object>
                    {
                        new Object
                        {
                            Kind = ObjectKind.Integer,
                            Value = 1
                        },
                        new Object
                        {
                            Kind = ObjectKind.Integer,
                            Value = 4
                        },
                        new Object
                        {
                            Kind = ObjectKind.Integer,
                            Value = 6
                        }
                    }
                }
            },
            {
                "[1, 2, 3][0]",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 1
                }
            },
            {
                "[1, 2, 3][1]",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 2
                }
            },
            {
                "[1, 2, 3][2]",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 3
                }
            },
            {
                "let i = 0; [1][i];",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 1
                }
            },
            {
                "[1, 2, 3][1 + 1]",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 3
                }
            },
            {
                "let myArray = [1, 2, 3]; myArray[2];",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 3
                }
            },
            {
                "let myArray = [1, 2, 3]; myArray[0] + myArray[1] + myArray[2];",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 6
                }
            },
            {
                "let myArray = [1, 2, 3]; let i = myArray[0]; myArray[i];",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 2
                }
            },
            {
                "[1, 2, 3][3]",
                new Object
                {
                    Kind = ObjectKind.Null,
                    Value = null
                }
            },
            {
                "[1, 2, 3][-1]",
                new Object
                {
                    Kind = ObjectKind.Null,
                    Value = null
                }
            },
            {
                "[1, 2, 3][true]",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("invalid type: [1, 2, 3][true<-- ], expected Integer")
                }
            },
            {
                "true[1]",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("invalid type: true<-- [1], expected Array")
                }
            }
        };

        internal static Dictionary<string, Object> Hash = new Dictionary<string, Object>
        {
            {
                "let two = \"two\"; { \"one\": 10 - 9, two: 1 + 1, \"thr\" + \"ee\": 6 / 2, 4: 4, true: 5, false: 6 };",
                new Object
                {
                    Kind = ObjectKind.Hash,
                    Value = new Dictionary<string, Object>
                    {
                        { "one", new Object { Kind = ObjectKind.Integer, Value = 1 } },
                        { "two", new Object { Kind = ObjectKind.Integer, Value = 2 } },
                        { "three", new Object { Kind = ObjectKind.Integer, Value = 3 } },
                        { "4", new Object { Kind = ObjectKind.Integer, Value = 4 } },
                        { "True", new Object { Kind = ObjectKind.Integer, Value = 5 } },
                        { "False", new Object { Kind = ObjectKind.Integer, Value = 6 } }
                    }
                }
            },
            {
                "{ \"foo\": 5 }[\"foo\"]",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 5
                }
            },
            {
                "{ \"foo\": 5 }[\"bar\"]",
                new Object
                {
                    Kind = ObjectKind.Null,
                    Value = null
                }
            },
            {
                "let key = \"foo\"; { \"foo\": 5 }[key]",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 5
                }
            },
            {
                "{}[\"foo\"]",
                new Object
                {
                    Kind = ObjectKind.Null,
                    Value = null
                }
            },
            {
                "{ 5: 5 }[5]",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 5
                }
            },
            {
                "{ true: 5 }[true]",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 5
                }
            },
            {
                "{ false: 5 }[false]",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 5
                }
            },
            {
                "{ \"name\": \"Monkey\" }[fn(x) { x }];",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("invalid type: { name: monkey }[fn(x) { ... }<-- ], expected Integer, Boolean, or String")
                }
            }
        };

        internal static Dictionary<string, Object> BuiltIn = new Dictionary<string, Object>
        {
            {
                "len()",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("invalid argument: len(), unexpected number of arguments")
                }
            },
            {
                "len(1)",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("invalid argument: len(1<-- ), expected Array or String")
                }
            },
            {
                "len([])",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 0
                }
            },
            {
                "len([1, 2, 3])",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 3
                }
            },
            {
                "len(\"Astralis\")",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 8
                }
            },
            {
                "first()",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("invalid argument: first(), unexpected number of arguments")
                }
            },
            {
                "first(1)",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("invalid argument: first(1<-- ), expected Array")
                }
            },
            {
                "first([])",
                new Object
                {
                    Kind = ObjectKind.Null,
                    Value = null
                }
            },
            {
                "first([1, 2, 3])",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 1
                }
            },
            {
                "last()",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("invalid argument: last(), unexpected number of arguments")
                }
            },
            {
                "last(1)",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("invalid argument: last(1<-- ), expected Array")
                }
            },
            {
                "last([])",
                new Object
                {
                    Kind = ObjectKind.Null,
                    Value = null
                }
            },
            {
                "last([1, 2, 3])",
                new Object
                {
                    Kind = ObjectKind.Integer,
                    Value = 3
                }
            },
            {
                "rest()",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("invalid argument: rest(), unexpected number of arguments")
                }
            },
            {
                "rest(1)",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("invalid argument: rest(1<-- ), expected Array")
                }
            },
            {
                "rest([])",
                new Object
                {
                    Kind = ObjectKind.Null,
                    Value = null
                }
            },
            {
                "rest([1, 2, 3])",
                new Object
                {
                    Kind = ObjectKind.Array,
                    Value = new List<Object>
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
                    }
                }
            },
            {
                "push()",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("invalid argument: push(), unexpected number of arguments")
                }
            },
            {
                "push(1, 2)",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("invalid argument: push(1<-- , 2), expected Array as first argument")
                }
            },
            {
                "push([])",
                new Object
                {
                    Kind = ObjectKind.Error,
                    Value = new AssertionError("invalid argument: push(), unexpected number of arguments")
                }
            },
            {
                "push([1, 2], 3)",
                new Object
                {
                    Kind = ObjectKind.Array,
                    Value = new List<Object>
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
                    }
                }
            }
        };
    }
}
