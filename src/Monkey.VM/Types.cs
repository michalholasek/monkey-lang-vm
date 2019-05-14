using System;
using System.Collections.Generic;

using Monkey.Shared;
using Object = Monkey.Shared.Object;

namespace Monkey
{
    internal class VirtualMachineState
    {
        public Dictionary<string, Object> Constants { get; set; }
        public List<Object> Globals { get; set; }
        public List<byte> Instructions { get; set; }
        public int InstructionPointer { get; set; }
        public byte Opcode { get; set; }
        public VirtualMachineStack Stack { get; set; }
    }

    internal class VirtualMachineStack
    {
        public Object LastElement { get; private set; }
        private Stack<Object> Stack { get; set; }

        public VirtualMachineStack()
        {
            Stack = new Stack<Object>();
        }

        public void Push(Object obj)
        {
            Stack.Push(obj);
        }

        public Object Pop()
        {
            LastElement = Stack.Pop();
            return LastElement;
        }
    }
}
