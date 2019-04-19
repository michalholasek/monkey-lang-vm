using System;
using System.Collections.Generic;

using Monkey.Shared;
using Object = Monkey.Shared.Object;

namespace Monkey
{
    public partial class Compiler
    {
        private readonly CompilerState initialState;

        public Compiler()
        {
            initialState = new CompilerState
            {
                Constants = new Dictionary<string, Object>(),
                Instructions = new List<byte>()
            };
        }

        public CompilerState Compile(Node node)
        {
            var newState = Factory.CompilerState()
                    .Assign(initialState)
                    .Node(node)
                    .Create();

            return CompileNode(newState);
        }

        private CompilerState CompileNode(CompilerState previousState)
        {
            switch (previousState.Node.Kind)
            {
                case NodeKind.Program:
                    return CompileStatements(previousState);
                default:
                    return CompileExpression(previousState);
            }
        }
    }
}
