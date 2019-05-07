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
        [DataRow((byte)3)]
        [DataRow((byte)4)]
        [DataRow((byte)5)]
        [DataRow((byte)6)]
        public void FindOpcode(byte code)
        {
            var actual = Opcode.Find(code);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Opcodes.Find[code]);
        }
    }
}
