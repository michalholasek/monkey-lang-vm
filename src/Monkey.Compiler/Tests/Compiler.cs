using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Monkey;
using Monkey.Shared;

namespace Monkey.Tests
{
    [TestClass]
    public class Compiling
    {
        Scanner scanner = new Scanner();
        Parser parser = new Parser();
        Compiler compiler = new Compiler();

        [TestMethod]
        [DataRow("1 + 2")]
        [DataRow("2 - 1")]
        [DataRow("2 * 2")]
        [DataRow("4 / 2")]
        [DataRow("50 / 2 * 2 + 10 - 5")]
        [DataRow("5 + 5 + 5 + 5 - 10")]
        [DataRow("2 * 2 * 2 * 2 * 2")]
        [DataRow("5 * 2 + 10")]
        [DataRow("5 + 2 * 10")]
        [DataRow("5 * (2 + 10)")]
        [DataRow("-1")]
        public void IntegerExpression(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));
            Utilities.Assert.AreDeeplyEqual(compilationResult.CurrentScope.Instructions, Fixtures.Compiler.Expression.Integer[source]);
        }

        [TestMethod]
        [DataRow("true")]
        [DataRow("false")]
        [DataRow("1 > 2")]
        [DataRow("1 < 2")]
        [DataRow("1 == 2")]
        [DataRow("1 != 2")]
        [DataRow("true == false")]
        [DataRow("true != false")]
        [DataRow("!true")]
        public void BooleanExpression(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));
            Utilities.Assert.AreDeeplyEqual(compilationResult.CurrentScope.Instructions, Fixtures.Compiler.Expression.Boolean[source]);
        }

        [TestMethod]
        [DataRow("if (true) { 10; }; 3333;")]
        [DataRow("if (true) { 10; } else { 20; }; 3333;")]
        public void IfElseExpression(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));
            Utilities.Assert.AreDeeplyEqual(compilationResult.CurrentScope.Instructions, Fixtures.Compiler.Expression.IfElse[source]);
        }

        [TestMethod]
        [DataRow("\"monkey\"")]
        [DataRow("\"mon\" + \"key\"")]
        public void StringExpression(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));
            Utilities.Assert.AreDeeplyEqual(compilationResult.CurrentScope.Instructions, Fixtures.Compiler.Expression.String[source]);
        }

        [TestMethod]
        [DataRow("[]")]
        [DataRow("[1, 2, 3]")]
        [DataRow("[1 + 2, 3 - 4, 5 * 6]")]
        [DataRow("[1, 2, 3][1 + 1]")]
        public void ArrayExpression(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));
            Utilities.Assert.AreDeeplyEqual(compilationResult.CurrentScope.Instructions, Fixtures.Compiler.Expression.Array[source]);
        }

        [TestMethod]
        [DataRow("{}")]
        [DataRow("{ 1: 2, 3: 4, 5: 6 }")]
        [DataRow("{ 1: 2 + 3, 4: 5 * 6 }")]
        [DataRow("{ 1: 2 }[2 - 1]")]
        public void HashExpression(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));
            Utilities.Assert.AreDeeplyEqual(compilationResult.CurrentScope.Instructions, Fixtures.Compiler.Expression.Hash[source]);
        }

        [TestMethod]
        [DataRow("fn() { return 5 + 10; }")]
        [DataRow("fn() { 5 + 10; }")]
        [DataRow("fn() { 1; 2; }")]
        [DataRow("fn() { }")]
        [DataRow("fn() { 24; }();")]
        [DataRow("let noArg = fn() { 24; }; noArg();")]
        [DataRow("let num = 55; fn() { num; };")]
        [DataRow("fn() { let num = 55; num; };")]
        [DataRow("fn() { let a = 55; let b = 77; a + b; };")]
        [DataRow("let oneArg = fn(a) { a; }; oneArg(24);")]
        [DataRow("let manyArgs = fn(a, b, c) { a; b; c; }; manyArgs(24, 25, 26);")]
        public void FunctionExpression(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));
            var functionInstructions = compilationResult.Constants.Where(item => item.Kind == ObjectKind.Function).SelectMany(item => (List<byte>)item.Value);
            var instructions = compilationResult.Scopes.SelectMany(scope => scope.Instructions).Concat(functionInstructions);
            Utilities.Assert.AreDeeplyEqual(instructions, Fixtures.Compiler.Expression.Function[source]);
        }

        [TestMethod]
        [DataRow("let one = 1; let two = 2;")]
        [DataRow("let one = 1; one;")]
        [DataRow("let one = 1; let two = one; two;")]
        public void LetStatement(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));
            Utilities.Assert.AreDeeplyEqual(compilationResult.CurrentScope.Instructions, Fixtures.Compiler.Statement.Let[source]);
        }

        [TestMethod]
        [DataRow("a;")]
        public void UndefinedVariable(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));
            Utilities.Assert.AreDeeplyEqual(compilationResult.Errors, Fixtures.Compiler.Statement.Error[source]);
        }
    }
}
