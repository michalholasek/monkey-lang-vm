using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Monkey.Shared.Bytecode;

namespace Monkey.Tests
{
    [TestClass]
    public class Opcodes
    {
        [TestMethod]
        [DataRow((byte)0)]
        [DataRow((byte)1)]
        public void FindOpcode(byte code)
        {
            var actual = Opcode.Find(code);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Opcodes.Find[code]);
        }
    }
}
