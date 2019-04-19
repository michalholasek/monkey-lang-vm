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
                private Expression expression;
                private Node node;
                private List<byte> instructions;

                public CompilerStateFactory Assign(CompilerState previousState)
                {
                    constants = previousState.Constants;
                    expression = previousState.Expression;
                    node = previousState.Node;
                    instructions = previousState.Instructions;

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

                public CompilerState Create()
                {
                    return new CompilerState
                    {
                        Constants = constants,
                        Node = node,
                        Instructions = instructions
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
