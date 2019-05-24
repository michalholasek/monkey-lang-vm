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
        public void CreateWithOneOperand(byte opcode, int operand)
        {
            var actual = Bytecode.Create(opcode, new List<int> { operand });
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Bytecode.Opcodes[opcode]);
        }

        [TestMethod]
        [DataRow((byte)28, 65534, 255)]
        public void CreateWithTwoOperands(byte opcode, int operand1, int operand2)
        {
            var actual = Bytecode.Create(opcode, new List<int> { operand1, operand2 });
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Bytecode.Opcodes[opcode]);
        }
    }
}
