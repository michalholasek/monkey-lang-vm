using System;
using System.Collections.Generic;
using System.Linq;

using Monkey.Shared;
using Object = Monkey.Shared.Object;

namespace Monkey
{
    public partial class Compiler
    {
        private CompilerState internalState;

        public Compiler()
        {
            var globalScope = new Scope
            {
                Instructions = new List<byte>(),
                SymbolTable = new SymbolTable()
            };

            var scopes = new Stack<Scope>();

            scopes.Push(globalScope);

            internalState = new CompilerState { Scopes = scopes };
        }

        public CompilerState Compile(Node node)
        {
            internalState = InitializeState();

            var newState = Factory.CompilerState()
                    .Assign(internalState)
                    .Node(node)
                    .Create();

            return CompileNode(newState);
        }

        private CompilerState InitializeState()
        {
            var globalScope = new Scope
            {
                Instructions = new List<byte>(),
                SymbolTable = internalState.Scopes.First().SymbolTable
            };

            var scopes = new Stack<Scope>();

            scopes.Push(globalScope);

            return new CompilerState
            {
                BuiltIns = Functions.List,
                Constants = new List<Object>(),
                CurrentScope = globalScope,
                Errors = new List<AssertionError>(),
                Scopes = scopes
            };
        }

        private CompilerState CompileNode(CompilerState previousState)
        {
            switch (previousState.Node.Kind)
            {
                case NodeKind.Program:
                    return CompileProgramNode(previousState);
                case NodeKind.Let:
                    return CompileLetStatement(previousState);
                case NodeKind.Return:
                    return CompileReturnStatement(previousState);
                default:
                    return CompileExpression(previousState);
            }
        }

        private CompilerState CompileProgramNode(CompilerState previousState)
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

            return CompileStatements(program.Statements, previousState);
        }

        private CompilerState Emit(byte opcode, List<int> operands, CompilerState previousState)
        {
            var instruction = Bytecode.Create(opcode, operands);
            var position = previousState.CurrentScope.Instructions.Count;

            return Factory.CompilerState()
                .Assign(previousState)
                .CurrentInstruction(new Instruction { Opcode = opcode, Position = position })
                .PreviousInstruction(previousState.CurrentScope.CurrentInstruction)
                .Instructions(instruction)
                .Create();
        }

        private CompilerState EnterScope(CompilerState previousState)
        {
            var symbolTable = new SymbolTable(previousState.CurrentScope.SymbolTable);
            var scope = new Scope { Instructions = new List<byte>(), SymbolTable = symbolTable };

            return Factory.CompilerState()
                .Assign(previousState)
                .EnterScope(scope)
                .Create();
        }

        private CompilerState LeaveScope(CompilerState previousState)
        {
            return Factory.CompilerState()
                .Assign(previousState)
                .LeaveScope()
                .Create();
        }
    }
}
