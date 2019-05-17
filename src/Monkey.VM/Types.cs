using System;
using System.Collections.Generic;
using System.Linq;

using Monkey.Shared;
using Object = Monkey.Shared.Object;

namespace Monkey
{
    internal class Frame
    {
        public int Base { get; set; }
        public List<Object> Locals { get; set; }
        public List<byte> Instructions { get; set; }
        public int InstructionPointer { get; set; }

        public Frame(List<byte> instructions, int basePointer)
        {
            Base = basePointer;
            Locals = new List<Object>();
            Instructions = instructions;
            InstructionPointer = 0;
        }
    }

    internal class VirtualMachineState
    {
        public List<Object> Constants { get; set; }
        public Frame CurrentFrame { get; set; }
        public Stack<Frame> Frames { get; set; }
        public List<Object> Globals { get; set; }
        public byte Opcode { get; set; }
        public VirtualMachineStack Stack { get; set; }
    }

    internal class VirtualMachineStack
    {
        public Object LastElement { get; private set; }
        private List<Object> Stack { get; set; }

        public VirtualMachineStack()
        {
            Stack = new List<Object>();
        }

        public int Count { get { return Stack.Count; } }

        public Object this[int index]
        {
            get { return Stack[index]; }
        }

        public void Push(Object obj)
        {
            Stack.Add(obj);
        }

        public Object Pop()
        {
            LastElement = Stack.Last();
            Stack.RemoveAt(Stack.Count - 1);
            return LastElement;
        }

        public void ResetTo(int position)
        {
            Stack.RemoveRange(position, Stack.Count - position);
        }
    }
}
