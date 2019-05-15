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
                switch (expression.Kind)
                {
                    case ExpressionKind.String:
                        return item.Value.ToString() == ((StringExpression)expression).Value;
                    default:
                        return (int)item.Value == ((IntegerExpression)expression).Value;
                }
            });

            if (index < 0)
            {
                index = previousState.Constants.Count;
            }

            return index;
        }

        private CompilerState RemoveLastPopInstruction(CompilerState previousState)
        {
            var position = previousState.Instructions.LastIndexOf((byte)Opcode.Name.Pop);
            previousState.Instructions.RemoveAt(position);

            return Factory.CompilerState()
                .Assign(previousState)
                .CurrentInstruction(previousState.PreviousInstruction)
                .Create();
        }

        private CompilerState ReplaceInstruction(int position, List<byte> instruction, CompilerState previousState)
        {
            previousState.Instructions.RemoveRange(position, instruction.Count);
            previousState.Instructions.InsertRange(position, instruction);
            return previousState;
        }
    }
}
