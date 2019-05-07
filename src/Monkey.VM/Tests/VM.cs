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
        public void Compile(string source)
        {
            var compilationResult = compiler.Compile(parser.Parse(scanner.Scan(source)));

            vm.Run(compilationResult.Instructions, compilationResult.Constants);

            Utilities.Assert.AreDeeplyEqual(vm.LastStackElement, Fixtures.VM.Run[source]);
        }
    }
}
