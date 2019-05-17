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
            var statement = (Statement)previousState.Node;
            var expressionState = CompileStatementExpression(previousState);
            var symbol = expressionState.CurrentScope.SymbolTable.Define(statement.Identifier.Literal);

            var opcode = symbol.Scope == SymbolScope.Global ? (byte)Opcode.Name.SetGlobal : (byte)Opcode.Name.SetLocal;

            return Emit(opcode, new List<int> { symbol.Index }, expressionState);
        }

        private CompilerState CompileReturnStatement(CompilerState previousState)
        {
            var expressionState = CompileStatementExpression(previousState);
            return Emit((byte)Opcode.Name.ReturnValue, new List<int>(), expressionState);
        }

        private CompilerState CompileStatementExpression(CompilerState previousState)
        {
            var expressionState = CompileExpression(previousState);

            if (expressionState.Errors.Count > 0)
            {
                return expressionState;
            }

            if (expressionState.CurrentScope.CurrentInstruction.Opcode == (byte)Opcode.Name.Pop)
            {
                // We want to keep defined values on the stack
                expressionState = RemoveLastPopInstruction(expressionState);
            }

            return expressionState;
        }
    }
}
