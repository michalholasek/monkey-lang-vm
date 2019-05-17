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
                private List<Object> constants;
                private Scope currentScope;
                private List<AssertionError> errors;
                private Expression expression;
                private Node node;
                private Stack<Scope> scopes;

                public CompilerStateFactory EnterScope(Scope scope)
                {
                    this.scopes.Push(scope);
                    this.currentScope = scope;
                    return this;
                }
                
                public CompilerStateFactory Assign(CompilerState previousState)
                {
                    constants = previousState.Constants;
                    currentScope = previousState.CurrentScope;
                    errors = previousState.Errors;
                    expression = previousState.Expression;
                    node = previousState.Node;
                    scopes = previousState.Scopes;

                    return this;
                }

                public CompilerStateFactory Constant(int index, Object obj)
                {
                    if (index < constants.Count)
                    {
                        return this;
                    }

                    constants.Add(obj);

                    return this;
                }

                public CompilerStateFactory CurrentInstruction(Instruction instruction)
                {
                    this.currentScope.CurrentInstruction = instruction;
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
                    this.currentScope.Instructions.AddRange(instructions);
                    return this;
                }

                public CompilerStateFactory PreviousInstruction(Instruction instruction)
                {
                    this.currentScope.PreviousInstruction = instruction;
                    return this;
                }

                public CompilerStateFactory LeaveScope()
                {
                    this.scopes.Pop();
                    this.currentScope = this.scopes.First();
                    return this;
                }

                public CompilerState Create()
                {
                    return new CompilerState
                    {
                        Constants = constants,
                        CurrentScope = currentScope,
                        Errors = errors,
                        Node = node,
                        Scopes = scopes
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
