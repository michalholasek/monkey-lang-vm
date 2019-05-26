using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Monkey;
using Monkey.Shared;

namespace Monkey.Tests
{
    [TestClass]
    public class VM
    {
        Scanner scanner = new Scanner();
        Parser parser = new Parser();
        Compiler compiler = new Compiler();
        VirtualMachine vm = new VirtualMachine();

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
        [DataRow("-5")]
        [DataRow("-10")]
        [DataRow("-50 + 100 + -50")]
        [DataRow("(5 + 10 * 2 + 15 / 3) * 2 + -10")]
        public void IntegerExpressions(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));

            vm.Run(compilationResult.CurrentScope.Instructions, compilationResult.Constants, compilationResult.BuiltIns);

            Utilities.Assert.AreDeeplyEqual(vm.StackTop, Fixtures.VM.Expression.Integer[source]);
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
        [DataRow("!true")]
        [DataRow("!false")]
        [DataRow("!5")]
        [DataRow("!!true")]
        [DataRow("!!false")]
        [DataRow("!!5")]
        [DataRow("!(if (false) { 5; })")]
        public void BooleanExpressions(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));

            vm.Run(compilationResult.CurrentScope.Instructions, compilationResult.Constants, compilationResult.BuiltIns);

            Utilities.Assert.AreDeeplyEqual(vm.StackTop, Fixtures.VM.Expression.Boolean[source]);
        }

        [TestMethod]
        [DataRow("if (true) { 10; }")]
        [DataRow("if (true) { 10; } else { 20; }")]
        [DataRow("if (false) { 10; } else { 20; }")]
        [DataRow("if (1) { 10; }")]
        [DataRow("if (1 < 2) { 10; }")]
        [DataRow("if (1 < 2) { 10; } else { 20; }")]
        [DataRow("if (1 > 2) { 10; } else { 20; }")]
        [DataRow("if (1 > 2) { 10; }")]
        [DataRow("if (false) { 10; }")]
        [DataRow("if ((if (false) { 10; })) { 10; } else { 20; }")]
        public void IfElseExpressions(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));

            vm.Run(compilationResult.CurrentScope.Instructions, compilationResult.Constants, compilationResult.BuiltIns);

            Utilities.Assert.AreDeeplyEqual(vm.StackTop, Fixtures.VM.Expression.IfElse[source]);
        }

        [TestMethod]
        [DataRow("\"monkey\"")]
        [DataRow("\"mon\" + \"key\"")]
        [DataRow("\"mon\" + \"key\" + \"banana\"")]
        public void StringExpressions(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));

            vm.Run(compilationResult.CurrentScope.Instructions, compilationResult.Constants, compilationResult.BuiltIns);

            Utilities.Assert.AreDeeplyEqual(vm.StackTop, Fixtures.VM.Expression.String[source]);
        }

        [TestMethod]
        [DataRow("[]")]
        [DataRow("[1, 2, 3]")]
        [DataRow("[1 + 2, 3 * 4, 5 + 6]")]
        [DataRow("[1, 2, 3][1]")]
        [DataRow("[1, 2, 3][0 + 2]")]
        [DataRow("[[1, 1, 1]][0][0]")]
        [DataRow("[][0]")]
        [DataRow("[1, 2, 3][99]")]
        [DataRow("[1][-1]")]
        public void ArrayExpressions(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));

            vm.Run(compilationResult.CurrentScope.Instructions, compilationResult.Constants, compilationResult.BuiltIns);

            Utilities.Assert.AreDeeplyEqual(vm.StackTop, Fixtures.VM.Expression.Array[source]);
        }

        [TestMethod]
        [DataRow("{}")]
        [DataRow("{ 1: 2, 2: 3 }")]
        [DataRow("{ 1 + 1: 2 * 2, 3 + 3: 4 * 4 }")]
        [DataRow("{ 1: 1, 2: 2 }[1]")]
        [DataRow("{ 1: 1, 2: 2 }[2]")]
        [DataRow("{ 1: 1 }[0]")]
        [DataRow("{}[0]")]
        public void HashExpressions(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));

            vm.Run(compilationResult.CurrentScope.Instructions, compilationResult.Constants, compilationResult.BuiltIns);

            Utilities.Assert.AreDeeplyEqual(vm.StackTop, Fixtures.VM.Expression.Hash[source]);
        }

        [TestMethod]
        [DataRow("let fivePlusTen = fn() { 5 + 10; }; fivePlusTen();")]
        [DataRow("let one = fn() { 1; }; let two = fn() { 2; }; one() + two();")]
        [DataRow("let a = fn() { 1; }; let b = fn() { a() + 1; }; let c = fn() { b() + 1; }; c();")]
        [DataRow("let earlyExit = fn() { return 99; 100; }; earlyExit();")]
        [DataRow("let earlyExit = fn() { return 99; return 100; }; earlyExit();")]
        [DataRow("let noReturn = fn() { }; noReturn();")]
        [DataRow("let noReturn = fn() { }; let noReturnTwo = fn() { noReturn(); }; noReturn(); noReturnTwo();")]
        [DataRow("let returnsOne = fn() { 1; }; let returnsOneReturner = fn() { returnsOne; }; returnsOneReturner()();")]
        [DataRow("let one = fn() { let one = 1; one }; one();")]
        [DataRow("let oneAndTwo = fn() { let one = 1; let two = 2; one + two; }; oneAndTwo();")]
        [DataRow("let oneAndTwo = fn() { let one = 1; let two = 2; one + two; }; let threeAndFour = fn() { let three = 3; let four = 4; three + four; }; oneAndTwo() + threeAndFour();")]
        [DataRow("let firstFoobar = fn() { let foobar = 50; foobar; }; let secondFoobar = fn() { let foobar = 100; foobar; }; firstFoobar() + secondFoobar();")]
        [DataRow("let globalSeed = 50; let minusOne = fn() { let num = 1; globalSeed - num; }; let minusTwo = fn() { let num = 2; globalSeed - num; }; minusOne() + minusTwo();")]
        [DataRow("let returnsOneReturner = fn() { let returnsOne = fn() { 1; }; returnsOne; }; returnsOneReturner()();")]
        [DataRow("let identity = fn(a) { a; }; identity(4);")]
        [DataRow("let sum = fn(a, b) { a + b; }; sum(1, 2);")]
        [DataRow("let sum = fn(a, b) { let c = a + b; c; }; sum(1, 2);")]
        [DataRow("let sum = fn(a, b) { let c = a + b; c; }; sum(1, 2) + sum(3, 4);")]
        [DataRow("let sum = fn(a, b) { let c = a + b; c; }; let outer = fn() { sum(1, 2) + sum(3, 4); }; outer();")]
        [DataRow("let globalNum = 10; let sum = fn(a, b) { let c = a + b; c + globalNum; }; let outer = fn() { sum(1, 2) + sum(3, 4) + globalNum; }; outer() + globalNum;")]
        [DataRow("let newClosure = fn(a) { fn() { a; }; }; let closure = newClosure(99); closure();")]
        [DataRow("let newAdder = fn(a, b) { fn(c) { a + b + c; }; }; let adder = newAdder(1, 2); adder(8);")]
        [DataRow("let newAdder = fn(a, b) { let c = a + b; fn(d) { c + d; }; }; let adder = newAdder(1, 2); adder(8);")]
        [DataRow("let a = 1; let newAdderOuter = fn(b) { fn(c) { fn(d) { a + b + c + d }; }; }; let newAdderInner = newAdderOuter(2); let adder = newAdderInner(3); adder(8);")]
        [DataRow("let newClosure = fn(a, b) { let one = fn() { a; }; let two = fn() { b; }; fn() { one() + two(); }; }; let closure = newClosure(9, 90); closure();")]
        public void FunctionExpressions(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));

            vm.Run(compilationResult.CurrentScope.Instructions, compilationResult.Constants, compilationResult.BuiltIns);

            Utilities.Assert.AreDeeplyEqual(vm.StackTop, Fixtures.VM.Expression.Function[source]);
        }

        [DataRow("len(\"\");")]
        [DataRow("len(\"four\");")]
        [DataRow("len(\"hello world\");")]
        [DataRow("len(1);")]
        [DataRow("len(\"one\", \"two\");")]
        [DataRow("len([1, 2, 3]);")]
        [DataRow("len([]);")]
        [DataRow("first([1, 2, 3]);")]
        [DataRow("first([]);")]
        [DataRow("first(1);")]
        [DataRow("last([1, 2, 3]);")]
        [DataRow("last([]);")]
        [DataRow("last(1);")]
        [DataRow("rest([1, 2, 3]);")]
        [DataRow("rest([]);")]
        [DataRow("rest(1);")]
        [DataRow("push([1, 2], 3);")]
        [DataRow("push([]);")]
        [DataRow("push(1, 2);")]
        public void BuiltIns(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));

            vm.Run(compilationResult.CurrentScope.Instructions, compilationResult.Constants, compilationResult.BuiltIns);

            Utilities.Assert.AreDeeplyEqual(vm.StackTop, Fixtures.VM.Expression.BuiltIn[source]);
        }

        [TestMethod]
        [DataRow("let one = 1; one;")]
        [DataRow("let one = 1; let two = 2; one + two;")]
        [DataRow("let one = 1; let two = one + one; one + two;")]
        public void LetStatements(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));

            vm.Run(compilationResult.CurrentScope.Instructions, compilationResult.Constants, compilationResult.BuiltIns);

            Utilities.Assert.AreDeeplyEqual(vm.StackTop, Fixtures.VM.Statement.Let[source]);
        }

        [TestMethod]
        [DataRow("return;")]
        [DataRow("return 42;")]
        public void ReturnStatements(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));

            vm.Run(compilationResult.CurrentScope.Instructions, compilationResult.Constants, compilationResult.BuiltIns);

            Utilities.Assert.AreDeeplyEqual(vm.StackTop, Fixtures.VM.Statement.Return[source]);
        }
    }
}
