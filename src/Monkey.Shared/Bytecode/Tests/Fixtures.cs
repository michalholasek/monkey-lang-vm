using System.Collections.Generic;

using Monkey.Shared.Bytecode;

namespace Monkey.Tests.Fixtures
{
    public static class Opcodes
    {
        public static Dictionary<byte, Opcode.Definition> Find = new Dictionary<byte, Opcode.Definition>
        {
            { 0, new Opcode.Definition { Name = "Invalid", OperandLengths = new List<int> { 0 } }},
            { 1, new Opcode.Definition { Name = "Constant", OperandLengths = new List<int> { 2 }  }}
        };
    }

    public static class Bytecode
    {
        public static Dictionary<byte, List<byte>> Opcodes = new Dictionary<byte, List<byte>>
        {
            { 1, new List<byte> { 1, 255, 254} }
        };
    }
}
