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

            foreach (var statement in statements)
            {
                newState = Factory.CompilerState()
                    .Assign(newState)
                    .Node(statement)
                    .Create();

                newState = CompileNode(newState);

                if (newState.Errors.Count > 0)
                {
                    break;
                }
            }

            return newState;
        }

        private CompilerState CompileLetStatement(CompilerState previousState)
        {
            var expressionState = CompileExpression(previousState);

            if (expressionState.Errors.Count > 0)
            {
                return expressionState;
            }

            if (expressionState.CurrentInstruction.Opcode == (byte)Opcode.Name.Pop)
            {
                // We want to keep defined values on the stack
                expressionState = RemoveLastPopInstruction(expressionState);
            }

            var statement = (Statement)expressionState.Node;
            
            var symbol = expressionState.SymbolTable.Define(statement.Identifier.Literal);

            return Emit((byte)Opcode.Name.SetGlobal, new List<int> { symbol.Index }, expressionState);
        }
    }
}
