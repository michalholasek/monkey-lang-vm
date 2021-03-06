using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Monkey;

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
        [DataRow((byte)7)]
        [DataRow((byte)8)]
        [DataRow((byte)9)]
        [DataRow((byte)10)]
        [DataRow((byte)11)]
        [DataRow((byte)12)]
        [DataRow((byte)13)]
        [DataRow((byte)14)]
        [DataRow((byte)15)]
        [DataRow((byte)16)]
        [DataRow((byte)17)]
        [DataRow((byte)18)]
        [DataRow((byte)19)]
        [DataRow((byte)20)]
        [DataRow((byte)21)]
        [DataRow((byte)22)]
        [DataRow((byte)23)]
        [DataRow((byte)24)]
        [DataRow((byte)25)]
        [DataRow((byte)26)]
        [DataRow((byte)27)]
        [DataRow((byte)28)]
        [DataRow((byte)29)]
        [DataRow((byte)30)]
        public void FindOpcode(byte code)
        {
            var actual = Opcode.Find(code);
            Utilities.Assert.AreDeeplyEqual(actual, Fixtures.Opcodes.Find[code]);
        }
    }
}
