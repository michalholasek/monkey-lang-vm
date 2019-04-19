using System;
using System.Collections.Generic;

using Monkey.Shared;
using Object = Monkey.Shared.Object;

namespace Monkey
{
    public partial class Compiler
    {
        private CompilerState CompileStatements(CompilerState previousState)
        {
            var newState = Factory.CompilerState()
                    .Assign(previousState)
                    .Create();

            var statements = ((Program)newState.Node).Statements;

            statements.ForEach(statement => {
                newState = Factory.CompilerState()
                    .Assign(newState)
                    .Node(statement)
                    .Create();

                newState = CompileNode(newState);
            });

            return newState;
        }
    }
}
