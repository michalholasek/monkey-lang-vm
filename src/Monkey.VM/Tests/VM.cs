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

            vm.Run(compilationResult.Instructions, compilationResult.Constants);

            Utilities.Assert.AreDeeplyEqual(vm.LastStackElement, Fixtures.VM.Expression.Integer[source]);
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
        public void BooleanExpressions(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));

            vm.Run(compilationResult.Instructions, compilationResult.Constants);

            Utilities.Assert.AreDeeplyEqual(vm.LastStackElement, Fixtures.VM.Expression.Boolean[source]);
        }
    }
}
