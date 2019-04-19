using System;
using System.Collections.Generic;
using System.Linq;

using Monkey;
using Monkey.Shared;
using static Monkey.Evaluator.Utilities;
using Object = Monkey.Shared.Object;

namespace Monkey
{
    public class VirtualMachine
    {
        private VirtualMachineState internalState;

        public void Run(List<byte> instructions, Dictionary<string, Object> constants)
        {
            internalState = new VirtualMachineState
            {
                Constants = constants,
                Instructions = instructions,
                InstructionPointer = 0,
                Stack = new Stack<Object>()
            };

            while (internalState.InstructionPointer < internalState.Instructions.Count)
            {
                internalState.Opcode = internalState.Instructions[internalState.InstructionPointer];
                internalState.InstructionPointer++;

                switch (internalState.Opcode)
                {
                    case 1: // Opcode.Constant
                        internalState.Stack.Push(internalState.Constants[DecodeOperand(2)]);
                        internalState.InstructionPointer += 2;
                        break;
                    case 2: // Opcode.Add
                        var rightValue = (int)internalState.Stack.Pop().Value;
                        var leftValue = (int)internalState.Stack.Pop().Value;
                        internalState.Stack.Push(CreateObject(ObjectKind.Integer, leftValue + rightValue));
                        break;
                }
            }
        }

        public Object StackTop()
        {
            if (internalState.Stack.Count == 0)
            {
                return null;
            }

            return internalState.Stack.Pop();
        }

        private string DecodeOperand(int length)
        {
            var buffer = new byte[length];

            for (var i = 0; i < length; i++)
            {
                buffer[i] = internalState.Instructions[internalState.InstructionPointer + i];
            }

            return BitConverter.ToInt16(buffer, startIndex: 0).ToString();
        }
    }
}
