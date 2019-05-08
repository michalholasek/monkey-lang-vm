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

            var program = (Program)newState.Node;

            if (program.Errors.Count > 0)
            {
                return Factory.CompilerState()
                    .Assign(newState)
                    .Errors(program.Errors)
                    .Create();
            }

            program.Statements.ForEach(statement =>
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
