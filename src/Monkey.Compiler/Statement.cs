using System;
using System.Collections.Generic;

using Monkey.Shared;
using Object = Monkey.Shared.Object;

namespace Monkey
{
    public partial class Compiler
    {
        private CompilerState CompileStatements(List<Statement> statements, CompilerState previousState)
        {
            var newState = Factory.CompilerState()
                    .Assign(previousState)
                    .Create();

            statements.ForEach(statement =>
            {
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
