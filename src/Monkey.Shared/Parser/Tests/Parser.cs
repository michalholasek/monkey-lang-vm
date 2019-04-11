using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Monkey.Shared.Scanner;
using Monkey.Shared.Parser;

namespace Monkey.Tests
{
    [TestClass]
    public class Parsing
    {
        [TestMethod]
        public void EmptyAst()
        {
            var actual = new Parser().Parse(new List<Token>());
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Statements.Empty);
        }

        [TestMethod]
        [DataRow("let let = 0;")]
        [DataRow("return ,;")]
        public void InvalidStatements(string source)
        {
            var tokens = new Scanner().Scan(source);
            var actual = new Parser().Parse(tokens);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Statements.Invalid[source]);
        }

        [TestMethod]
        [DataRow("let int = 5;")]
        [DataRow("let x = 5; let y = 10; let foobar = 838383;")]
        public void LetStatements(string source)
        {
            var tokens = new Scanner().Scan(source);
            var actual = new Parser().Parse(tokens);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Statements.Let[source]);
        }

        [TestMethod]
        [DataRow("return 5; return 10; return 838383;")]
        [DataRow("return false;")]
        [DataRow("return \"Hello!\";")]
        public void ReturnStatements(string source)
        {
            var tokens = new Scanner().Scan(source);
            var actual = new Parser().Parse(tokens);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Statements.Return[source]);
        }

        [TestMethod]
        [DataRow("foobar")]
        public void IdentifierExpression(string source)
        {
            var tokens = new Scanner().Scan(source);
            var actual = new Parser().Parse(tokens);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Expressions.Identifier[source]);
        }

        [TestMethod]
        [DataRow("42")]
        public void IntegerExpression(string source)
        {
            var tokens = new Scanner().Scan(source);
            var actual = new Parser().Parse(tokens);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Expressions.Integer[source]);
        }

        [TestMethod]
        [DataRow("\"foo bar\"")]
        public void StringExpression(string source)
        {
            var tokens = new Scanner().Scan(source);
            var actual = new Parser().Parse(tokens);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Expressions.String[source]);
        }

        [TestMethod]
        [DataRow("!5")]
        [DataRow("-15")]
        [DataRow("!true")]
        [DataRow("!false")]
        public void PrefixExpression(string source)
        {
            var tokens = new Scanner().Scan(source);
            var actual = new Parser().Parse(tokens);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Expressions.Prefix[source]);
        }

        [TestMethod]
        [DataRow("5 + 5")]
        [DataRow("5 - 5")]
        [DataRow("5 * 5")]
        [DataRow("5 / 5")]
        [DataRow("5 > 5")]
        [DataRow("5 < 5")]
        [DataRow("5 == 5")]
        [DataRow("5 != 5")]
        [DataRow("true == true")]
        [DataRow("true != false")]
        [DataRow("false == false")]
        public void InfixExpression(string source)
        {
            var tokens = new Scanner().Scan(source);
            var actual = new Parser().Parse(tokens);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Expressions.Infix[source]);
        }

        [TestMethod]
        [DataRow("-a * b")]
        [DataRow("!-a")]
        [DataRow("a + b + c")]
        [DataRow("a + b - c")]
        [DataRow("a * b * c")]
        [DataRow("a * b / c")]
        [DataRow("a + b / c")]
        [DataRow("a + b * c + d / e - f")]
        [DataRow("5 > 4 == 3 < 4")]
        [DataRow("5 < 4 != 3 > 4")]
        [DataRow("3 + 4 * 5 == 3 * 1 + 4 * 5")]
        [DataRow("3 > 5 == false")]
        [DataRow("3 < 5 == true")]
        [DataRow("1 + (2 + 3) + 4")]
        [DataRow("(5 + 5) * 2")]
        [DataRow("2 / (5 + 5)")]
        [DataRow("-(5 + 5)")]
        [DataRow("!(true == true)")]
        [DataRow("a + add(b * c) + d")]
        [DataRow("add(a, b, 1, 2 * 3, 4 + 5, add(6, 7 * 8))")]
        [DataRow("add(a + b + c * d / f + g)")]
        public void OperatorPrecedences(string source)
        {
            var tokens = new Scanner().Scan(source);
            var actual = new Parser().Parse(tokens);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Expressions.OperatorPrecedences[source]);
        }

        [TestMethod]
        [DataRow("if (x < y) { x; }")]
        [DataRow("if (x < y) { x; } else { y; }")]
        public void IfElseExpression(string source)
        {
            var tokens = new Scanner().Scan(source);
            var actual = new Parser().Parse(tokens);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Expressions.IfElse[source]);
        }

        [TestMethod]
        [DataRow("fn() { x + y; }")]
        [DataRow("fn(x, y) { x + y; }")]
        public void FunctionExpression(string source)
        {
            var tokens = new Scanner().Scan(source);
            var actual = new Parser().Parse(tokens);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Expressions.Function[source]);
        }

        [TestMethod]
        [DataRow("add(1, 2 * 3, 4 + 5)")]
        [DataRow("fn(x) { x; }(5)")]
        [DataRow("fortyTwo([])")]
        public void CallExpression(string source)
        {
            var tokens = new Scanner().Scan(source);
            var actual = new Parser().Parse(tokens);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Expressions.Call[source]);
        }

        [TestMethod]
        [DataRow("[]")]
        [DataRow("[1, 2 * 2, 3 + 3]")]
        [DataRow("myArray[1 + 1]")]
        public void ArrayExpression(string source)
        {
            var tokens = new Scanner().Scan(source);
            var actual = new Parser().Parse(tokens);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Expressions.Array[source]);
        }

        [TestMethod]
        [DataRow("{}")]
        [DataRow("{ \"one\": 1, \"two\": 2, \"three\": 3 }")]
        [DataRow("{ \"abc\": 42 }[\"abc\"]")]
        public void HashExpression(string source)
        {
            var tokens = new Scanner().Scan(source);
            var actual = new Parser().Parse(tokens);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Expressions.Hash[source]);
        }
    }
}
