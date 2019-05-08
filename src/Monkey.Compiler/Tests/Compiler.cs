using System.Collections.Generic;
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
        public void IntegerExpression(string source)
        {
            var actual = compiler.Compile(parser.Parse(scanner.Scan(source))).Instructions;
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Compiler.Expression.Integer[source]);
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
        public void BooleanExpression(string source)
        {
            var actual = compiler.Compile(parser.Parse(scanner.Scan(source))).Instructions;
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Compiler.Expression.Boolean[source]);
        }
    }
}
