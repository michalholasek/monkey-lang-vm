using System;
using System.Collections.Generic;
using System.Linq;

namespace Monkey
{
    public partial class Compiler
    {
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
