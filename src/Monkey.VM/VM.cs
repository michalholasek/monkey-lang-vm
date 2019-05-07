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

        public Object LastStackElement { get { return internalState.Stack.LastElement; } }

        public void Run(List<byte> instructions, Dictionary<string, Object> constants)
        {
            internalState = new VirtualMachineState
            {
                Constants = constants,
                Instructions = instructions,
                InstructionPointer = 0,
                Stack = new VirtualMachineStack()
            };

            Object left;
            Object right;

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
                    case 3: // Opcode.Pop
                        internalState.Stack.Pop();
                        break;
                    case 2: // Opcode.Add
                    case 4: // Opcode.Subtract
                    case 5: // Opcode.Multiply
                    case 6: // Opcode.Divide
                        right = internalState.Stack.Pop();
                        left = internalState.Stack.Pop();
                        ExecuteBinaryOperation(internalState.Opcode, left, right);
                        break;
                }
            }
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

        private void ExecuteBinaryOperation(byte op, Object left, Object right)
        {
            if (left.Kind == ObjectKind.Integer && right.Kind == ObjectKind.Integer)
            {
                ExecuteBinaryIntegerOperation(op, (int)left.Value, (int)right.Value);
            }
        }

        private void ExecuteBinaryIntegerOperation(byte op, int left, int right)
        {
            switch (op)
            {
                case 2: // Opcode.Add
                    internalState.Stack.Push(CreateObject(ObjectKind.Integer, left + right));
                    break;
                case 4: // Opcode.Subtract
                    internalState.Stack.Push(CreateObject(ObjectKind.Integer, left - right));
                    break;
                case 5: // Opcode.Multiply
                    internalState.Stack.Push(CreateObject(ObjectKind.Integer, left * right));
                    break;
                case 6: // Opcode.Divide
                    internalState.Stack.Push(CreateObject(ObjectKind.Integer, left / right));
                    break;
            }
        }
    }
}
