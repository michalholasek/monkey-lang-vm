using System;
using System.Collections.Generic;
using System.Linq;

using Monkey.Shared;
using Object = Monkey.Shared.Object;

namespace Monkey
{
    public class Closure
    {
        public List<Object> Frees { get; set; }
        public List<byte> Instructions { get; set; }

        public Closure(List<byte> instructions, List<Object> frees)
        {
            Frees = frees;
            Instructions = instructions;
        }
    }

    internal class Frame
    {
        public int Base { get; set; }
        public Closure Closure { get; set; }
        public List<Object> Locals { get; set; }
        public int InstructionPointer { get; set; }

        public Frame(Closure closure, int basePointer)
        {
            Base = basePointer;
            Closure = closure;
            Locals = new List<Object>();
            InstructionPointer = 0;
        }
    }

    internal class VirtualMachineState
    {
        public List<BuiltIn> BuiltIns { get; set; }
        public List<Object> Constants { get; set; }
        public Frame CurrentFrame { get; set; }
        public Stack<Frame> Frames { get; set; }
        public List<Object> Globals { get; set; }
        public byte Opcode { get; set; }
        public VirtualMachineStack Stack { get; set; }
    }

    internal class VirtualMachineStack
    {
        private List<Object> Stack { get; set; }

        public VirtualMachineStack()
        {
            Stack = new List<Object>();
        }

        public int Count { get { return Stack.Count; } }
        public Object Top { get; private set; }

        public Object this[int index]
        {
            get { return Stack[index]; }
        }

        public void Push(Object obj)
        {
            Top = obj;
            Stack.Add(obj);
        }

        public Object Pop()
        {
            Top = Stack.Last();
            Stack.RemoveAt(Stack.Count - 1);
            return Top;
        }

        public void ResetTo(int position)
        {
            Stack.RemoveRange(position, Stack.Count - position);
        }
    }
}
