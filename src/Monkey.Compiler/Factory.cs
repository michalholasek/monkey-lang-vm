using System.Collections.Generic;
using System.Linq;

using Monkey.Shared;
using Object = Monkey.Shared.Object;

namespace Monkey
{
    public partial class Compiler
    {
        internal static class Factory
        {
            internal class CompilerStateFactory
            {
                private Dictionary<string, Object> constants;
                private Instruction currentInstruction;
                private List<AssertionError> errors;
                private Expression expression;
                private Node node;
                private List<byte> instructions;
                private Instruction previousInstruction;

                public CompilerStateFactory Assign(CompilerState previousState)
                {
                    constants = previousState.Constants;
                    currentInstruction = previousState.CurrentInstruction;
                    errors = previousState.Errors;
                    expression = previousState.Expression;
                    node = previousState.Node;
                    instructions = previousState.Instructions;
                    previousInstruction = previousState.PreviousInstruction;

                    return this;
                }

                public CompilerStateFactory Constant(string identifier, Object value)
                {
                    var key = constants.Keys.Where(item => item == identifier).FirstOrDefault();
                    if (key != null)
                    {
                        return this;
                    }

                    constants.Add(identifier, value);

                    return this;
                }

                public CompilerStateFactory CurrentInstruction(Instruction instruction)
                {
                    this.currentInstruction = instruction;
                    return this;
                }

                public CompilerStateFactory Errors(List<AssertionError> errors)
                {
                    this.errors.AddRange(errors);
                    return this;
                }

                public CompilerStateFactory Expression(Expression expression)
                {
                    this.expression = expression;
                    return this;
                }

                public CompilerStateFactory Node(Node node)
                {
                    this.node = node;
                    return this;
                }

                public CompilerStateFactory Instructions(List<byte> instructions)
                {
                    this.instructions.AddRange(instructions);
                    return this;
                }

                public CompilerStateFactory PreviousInstruction(Instruction instruction)
                {
                    this.previousInstruction = instruction;
                    return this;
                }

                public CompilerState Create()
                {
                    return new CompilerState
                    {
                        Constants = constants,
                        CurrentInstruction = currentInstruction,
                        Errors = errors,
                        Node = node,
                        Instructions = instructions,
                        PreviousInstruction = previousInstruction
                    };
                }
            }

            public static CompilerStateFactory CompilerState()
            {
                return new CompilerStateFactory();
            }
        }
    }
}
