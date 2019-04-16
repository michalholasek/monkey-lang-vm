using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Monkey.Shared.Scanner;
using Monkey.Shared.Parser;
using Monkey.Shared.Evaluator;

namespace Monkey.Tests
{
    [TestClass]
    public class Evaluation
    {
        Scanner scanner = new Scanner();
        Parser parser = new Parser();
        Evaluator evaluator = new Evaluator();

        [TestMethod]
        [DataRow("5")]
        [DataRow("-5")]
        [DataRow("5 + 5 + 5 + 5 - 10")]
        [DataRow("2 * 2 * 2 * 2 * 2")]
        [DataRow("-50 + 100 + -50")]
        [DataRow("5 * 2 + 10")]
        [DataRow("5 + 2 * 10")]
        [DataRow("20 + 2 * -10")]
        [DataRow("50 / 2 * 2 + 10")]
        [DataRow("2 * (5 + 10)")]
        [DataRow("3 * 3 * 3 + 10")]
        [DataRow("(5 + 10 * 2 + 15 / 3) * 2 + -10")]
        public void IntegerExpression(string source)
        {
            var env = new Environment();
            var actual = evaluator.Evaluate(parser.Parse(scanner.Scan(source)), env);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Evaluation.Integer[source]);
        }

        [TestMethod]
        [DataRow("true")]
        [DataRow("false")]
        [DataRow("1 < 2")]
        [DataRow("1 > 2")]
        [DataRow("1 < 1")]
        [DataRow("1 > 1")]
        [DataRow("1 == 1")]
        [DataRow("1 != 1")]
        [DataRow("1 == 2")]
        [DataRow("1 != 2")]
        [DataRow("true == true")]
        [DataRow("false == false")]
        [DataRow("true == false")]
        [DataRow("true != false")]
        [DataRow("false != true")]
        [DataRow("(1 < 2) == true")]
        [DataRow("(1 < 2) == false")]
        [DataRow("(1 > 2) == true")]
        [DataRow("(1 > 2) == false")]
        public void BooleanExpression(string source)
        {
            var env = new Environment();
            var actual = evaluator.Evaluate(parser.Parse(scanner.Scan(source)), env);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Evaluation.Boolean[source]);
        }

        [TestMethod]
        [DataRow("\"foo bar\"")]
        [DataRow("\"foo\" + \"bar\"")]
        public void StringExpression(string source)
        {
            var env = new Environment();
            var actual = evaluator.Evaluate(parser.Parse(scanner.Scan(source)), env);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Evaluation.String[source]);
        }

        [TestMethod]
        [DataRow("!true")]
        [DataRow("!false")]
        [DataRow("!5")]
        [DataRow("!!true")]
        [DataRow("!!false")]
        [DataRow("!!5")]
        public void PrefixExpression(string source)
        {
            var env = new Environment();
            var actual = evaluator.Evaluate(parser.Parse(scanner.Scan(source)), env);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Evaluation.Prefix[source]);
        }

        [TestMethod]
        [DataRow("if (true) { 10; }")]
        [DataRow("if (false) { 10; }")]
        [DataRow("if (1) { 10; }")]
        [DataRow("if (1 < 2) { 10; }")]
        [DataRow("if (1 > 2) { 10; }")]
        [DataRow("if (1 < 2) { 10; } else { 20; }")]
        [DataRow("if (1 > 2) { 10; } else { 20; }")]
        public void IfElseExpression(string source)
        {
            var env = new Environment();
            var actual = evaluator.Evaluate(parser.Parse(scanner.Scan(source)), env);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Evaluation.IfElse[source]);
        }

        [TestMethod]
        [DataRow("let a = 5; a;")]
        [DataRow("let a = 5 * 5; a;")]
        [DataRow("let a = 5; let b = a; b;")]
        [DataRow("let a = 5; let b = a; let c = a + b + 5; c;")]
        public void LetStatement(string source)
        {
            var env = new Environment();
            var actual = evaluator.Evaluate(parser.Parse(scanner.Scan(source)), env);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Evaluation.Let[source]);
        }

        [TestMethod]
        [DataRow("return 10;")]
        [DataRow("return 10; 9;")]
        [DataRow("return 2 * 5; 9;")]
        [DataRow("9; return 2 * 5; 9;")]
        [DataRow("if (10 > 1) { if (10 > 1) { return 10; } return 1; }")]
        public void ReturnStatement(string source)
        {
            var env = new Environment();
            var actual = evaluator.Evaluate(parser.Parse(scanner.Scan(source)), env);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Evaluation.Return[source]);
        }

        [TestMethod]
        [DataRow("5 + true;")]
        [DataRow("5 + true; 5;")]
        [DataRow("-true;")]
        [DataRow("true + false;")]
        [DataRow("5; true + false; 5;")]
        [DataRow("if (10 > 1) { true + false; }")]
        [DataRow("foobar")]
        public void IllegalExpression(string source)
        {
            var env = new Environment();
            var actual = evaluator.Evaluate(parser.Parse(scanner.Scan(source)), env);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Evaluation.Illegal[source]);
        }

        [TestMethod]
        [DataRow("fn(x) { x + 2; };")]
        public void FunctionExpression(string source)
        {
            var env = new Environment();
            var actual = evaluator.Evaluate(parser.Parse(scanner.Scan(source)), env);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Evaluation.Function[source]);
        }

        [TestMethod]
        [DataRow("let identity = fn(x) { x; }; identity(5);")]
        [DataRow("let identity = fn(x) { return x; }; identity(5);")]
        [DataRow("let double = fn(x) { x * 2; }; double(5);")]
        [DataRow("let add = fn(x, y) { x + y; }; add(5, 5);")]
        [DataRow("let add = fn(x, y) { x + y; }; add(5 + 5, add(5, 5));")]
        [DataRow("fn(x) { x; }(5);")]
        [DataRow("let newAdder = fn(x) { fn(y) { x + y }; }; let addTwo = newAdder(2); addTwo(2);")]
        public void CallExpression(string source)
        {
            var env = new Environment();
            var actual = evaluator.Evaluate(parser.Parse(scanner.Scan(source)), env);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Evaluation.Call[source]);
        }

        [TestMethod]
        [DataRow("len()")]
        [DataRow("len(1)")]
        [DataRow("len([])")]
        [DataRow("len([1, 2, 3])")]
        [DataRow("len(\"Astralis\")")]
        [DataRow("first()")]
        [DataRow("first(1)")]
        [DataRow("first([])")]
        [DataRow("first([1, 2, 3])")]
        [DataRow("last()")]
        [DataRow("last(1)")]
        [DataRow("last([])")]
        [DataRow("last([1, 2, 3])")]
        [DataRow("rest()")]
        [DataRow("rest(1)")]
        [DataRow("rest([])")]
        [DataRow("rest([1, 2, 3])")]
        [DataRow("push()")]
        [DataRow("push(1, 2)")]
        [DataRow("push([])")]
        [DataRow("push([1, 2], 3)")]
        public void BuiltInExpression(string source)
        {
            var env = new Environment();
            var actual = evaluator.Evaluate(parser.Parse(scanner.Scan(source)), env);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Evaluation.BuiltIn[source]);
        }

        [TestMethod]
        [DataRow("[]")]
        [DataRow("[1, 2 * 2, 3 + 3]")]
        [DataRow("[1, 2, 3][0]")]
        [DataRow("[1, 2, 3][1]")]
        [DataRow("[1, 2, 3][2]")]
        [DataRow("let i = 0; [1][i];")]
        [DataRow("[1, 2, 3][1 + 1]")]
        [DataRow("let myArray = [1, 2, 3]; myArray[2];")]
        [DataRow("let myArray = [1, 2, 3]; myArray[0] + myArray[1] + myArray[2];")]
        [DataRow("let myArray = [1, 2, 3]; let i = myArray[0]; myArray[i];")]
        [DataRow("[1, 2, 3][3]")]
        [DataRow("[1, 2, 3][-1]")]
        public void ArrayExpression(string source)
        {
            var env = new Environment();
            var actual = evaluator.Evaluate(parser.Parse(scanner.Scan(source)), env);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Evaluation.Array[source]);
        }

        [TestMethod]
        [DataRow("let two = \"two\"; { \"one\": 10 - 9, two: 1 + 1, \"thr\" + \"ee\": 6 / 2, 4: 4, true: 5, false: 6 };")]
        [DataRow("{ \"foo\": 5 }[\"foo\"]")]
        [DataRow("{ \"foo\": 5 }[\"bar\"]")]
        [DataRow("let key = \"foo\"; { \"foo\": 5 }[key]")]
        [DataRow("{}[\"foo\"]")]
        [DataRow("{ 5: 5 }[5]")]
        [DataRow("{ true: 5 }[true]")]
        [DataRow("{ false: 5 }[false]")]
        [DataRow("{ \"name\": \"Monkey\" }[fn(x) { x }];")]
        public void HashExpression(string source)
        {
            var env = new Environment();
            var actual = evaluator.Evaluate(parser.Parse(scanner.Scan(source)), env);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Evaluation.Hash[source]);
        }
    }
}
