using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Monkey.Shared;

namespace Monkey.Tests
{
    [TestClass]
    public class Opcodes
    {
        [TestMethod]
        [DataRow((byte)0)]
        [DataRow((byte)1)]
        [DataRow((byte)2)]
        public void FindOpcode(byte code)
        {
            var actual = Opcode.Find(code);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Opcodes.Find[code]);
        }
    }
}
