using System;
using System.Collections.Generic;
using System.Linq;

using Monkey.Shared;

namespace Monkey
{
    public partial class Compiler
    {
        private int DetermineConstantIndex(Expression expression, CompilerState previousState)
        {
            var index = previousState.Constants.FindIndex(item =>
            {
                if (item.Kind == ObjectKind.Integer && expression.Kind == ExpressionKind.Integer)
                {
                    return (int)item.Value == ((IntegerExpression)expression).Value;
                }
                if (item.Kind == ObjectKind.String && expression.Kind == ExpressionKind.String)
                {
                    return item.Value.ToString() == ((StringExpression)expression).Value;
                }
                if (item.Kind == ObjectKind.Function && expression.Kind == ExpressionKind.Function)
                {
                    return item.Value.GetHashCode() == previousState.CurrentScope.Instructions.GetHashCode();
                }

                return false;
            });

            if (index < 0)
            {
                index = previousState.Constants.Count;
            }

            return index;
        }

        private CompilerState RemoveLastPopInstruction(CompilerState previousState)
        {
            var position = previousState.CurrentScope.Instructions.LastIndexOf((byte)Opcode.Name.Pop);
            previousState.CurrentScope.Instructions.RemoveAt(position);

            return Factory.CompilerState()
                .Assign(previousState)
                .CurrentInstruction(previousState.CurrentScope.PreviousInstruction)
                .Create();
        }

        private CompilerState ReplaceInstruction(int position, List<byte> instruction, CompilerState previousState)
        {
            previousState.CurrentScope.Instructions.RemoveRange(position, instruction.Count);
            previousState.CurrentScope.Instructions.InsertRange(position, instruction);
            
            return Factory.CompilerState()
                .Assign(previousState)
                .CurrentInstruction(new Instruction { Opcode = instruction.First(), Position = position })
                .Create();
        }
    }
}
