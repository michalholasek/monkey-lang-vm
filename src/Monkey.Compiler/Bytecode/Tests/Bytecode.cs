using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Monkey;

namespace Monkey.Tests
{
    [TestClass]
    public class Bytecodes
    {
        [TestMethod]
        [DataRow((byte)1, 65534)]
        public void Create(byte opcode, int operand)
        {
            var actual = Bytecode.Create(opcode, new List<int> { operand });
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Bytecode.Opcodes[opcode]);
        }
    }
}
