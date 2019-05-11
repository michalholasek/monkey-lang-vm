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
        private static Dictionary<object, Object> Invariants = new Dictionary<object, Object>
        {
            { true, CreateObject(ObjectKind.Boolean, true) },
            { false, CreateObject(ObjectKind.Boolean, false) },
            { "null", CreateObject(ObjectKind.Null, null) }
        };

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

            while (internalState.InstructionPointer < internalState.Instructions.Count)
            {
                internalState.Opcode = internalState.Instructions[internalState.InstructionPointer];
                internalState.InstructionPointer++;

                switch (internalState.Opcode)
                {
                    case 1:  // Opcode.Constant
                        ExecuteConstantOperation();
                        break;
                    case 3:  // Opcode.Pop
                        internalState.Stack.Pop();
                        break;
                    case 2:  // Opcode.Add
                    case 4:  // Opcode.Subtract
                    case 5:  // Opcode.Multiply
                    case 6:  // Opcode.Divide
                    case 9:  // Opcode.Equal
                    case 10: // Opcode.NotEqual
                    case 11: // Opcode.GreaterThan
                        ExecuteBinaryOperation(internalState.Opcode);
                        break;
                    case 7:  // Opcode.True
                        ExecuteBooleanOperation(internalState.Opcode);
                        break;
                    case 8:  // Opcode.False
                        ExecuteBooleanOperation(internalState.Opcode);
                        break;
                    case 12: // Opcode.Minus
                        ExecuteMinusOperation();
                        break;
                    case 13: // Opcode.Bang
                        ExecuteBangOperation();
                        break;
                    case 14: // Opcode.Jump
                        ExecuteJumpOperation();
                        break;
                    case 15: // Opcode.JumpNotTruthy
                        ExecuteJumpNotTruthyOperation();
                        break;
                    case 16: // Opcode.Null
                        ExecuteNullOperation();
                        break;
                }
            }
        }

        private int DecodeOperand(int length)
        {
            var buffer = new byte[length];

            for (var i = 0; i < length; i++)
            {
                buffer[i] = internalState.Instructions[internalState.InstructionPointer + i];
            }

            return BitConverter.ToInt16(buffer, startIndex: 0);
        }

        private void ExecuteBangOperation()
        {
            Object operand = internalState.Stack.Pop();

            switch (operand.Kind)
            {
                case ObjectKind.Boolean:
                    internalState.Stack.Push(Invariants[!(bool)operand.Value]);
                    break;
                case ObjectKind.Null:
                    internalState.Stack.Push(Invariants[true]);
                    break;
                default:
                    internalState.Stack.Push(Invariants[false]);
                    break;
            }
        }

        private void ExecuteBooleanOperation(byte op)
        {
            switch (op)
            {
                case 7: // Opcode.True
                    internalState.Stack.Push(Invariants[true]);
                    break;
                case 8: // Opcode.False
                    internalState.Stack.Push(Invariants[false]);
                    break;
            }
        }

        private void ExecuteBinaryOperation(byte op)
        {
            Object right = internalState.Stack.Pop();
            Object left = internalState.Stack.Pop();

            if (left.Kind == ObjectKind.Integer && right.Kind == ObjectKind.Integer)
            {
                ExecuteBinaryIntegerOperation(op, (int)left.Value, (int)right.Value);
            }
            else if (left.Kind == ObjectKind.Boolean && right.Kind == ObjectKind.Boolean)
            {
                ExecuteBinaryBooleanOperation(op, (bool)left.Value, (bool)right.Value);
            }
        }

        private void ExecuteBinaryBooleanOperation(byte op, bool left, bool right)
        {
            switch (op)
            {
                case 9:  // Opcode.Equal
                    internalState.Stack.Push(left == right ? Invariants[true] : Invariants[false]);
                    break;
                case 10: // Opcode.NotEqual
                    internalState.Stack.Push(left != right ? Invariants[true] : Invariants[false]);
                    break;
            }
        }

        private void ExecuteConstantOperation()
        {
            internalState.Stack.Push(internalState.Constants[DecodeOperand(2).ToString()]);
            internalState.InstructionPointer += 2;
        }

        private void ExecuteJumpOperation()
        {
            internalState.InstructionPointer = DecodeOperand(2);
        }

        private void ExecuteJumpNotTruthyOperation()
        {
            var position = DecodeOperand(2);

            internalState.InstructionPointer += 2;
            var condition = internalState.Stack.Pop();

            if (!IsTruthy(condition))
            {
                internalState.InstructionPointer = position;
            }
        }

        private void ExecuteMinusOperation()
        {
            Object operand = internalState.Stack.Pop();

            if (operand.Kind != ObjectKind.Integer)
            {
                return;
            }

            int value = (int)operand.Value;

            internalState.Stack.Push(CreateObject(ObjectKind.Integer, -value));
        }

        private void ExecuteNullOperation()
        {
            internalState.Stack.Push(Invariants["null"]);
        }

        private void ExecuteBinaryIntegerOperation(byte op, int left, int right)
        {
            switch (op)
            {
                case 2:  // Opcode.Add
                    internalState.Stack.Push(CreateObject(ObjectKind.Integer, left + right));
                    break;
                case 4:  // Opcode.Subtract
                    internalState.Stack.Push(CreateObject(ObjectKind.Integer, left - right));
                    break;
                case 5:  // Opcode.Multiply
                    internalState.Stack.Push(CreateObject(ObjectKind.Integer, left * right));
                    break;
                case 6:  // Opcode.Divide
                    internalState.Stack.Push(CreateObject(ObjectKind.Integer, left / right));
                    break;
                case 9:  // Opcode.Equal
                    internalState.Stack.Push(left == right ? Invariants[true] : Invariants[false]);
                    break;
                case 10: // Opcode.NotEqual
                    internalState.Stack.Push(left != right ? Invariants[true] : Invariants[false]);
                    break;
                case 11: // Opcode.GreaterThan
                    internalState.Stack.Push(left > right ? Invariants[true] : Invariants[false]);
                    break;
            }
        }
    }
}
