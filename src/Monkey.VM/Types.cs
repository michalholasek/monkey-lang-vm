using System;
using System.Collections.Generic;

using Monkey.Shared;
using Object = Monkey.Shared.Object;

namespace Monkey
{
    internal class VirtualMachineState
    {
        public Dictionary<string, Object> Constants { get; set; }
        public List<byte> Instructions { get; set; }
        public int InstructionPointer { get; set; }
        public byte Opcode { get; set; }
        public Stack<Object> Stack { get; set; }
    }
}
