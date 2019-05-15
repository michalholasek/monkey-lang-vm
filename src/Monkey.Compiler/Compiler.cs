using System;
using System.Collections.Generic;

using Monkey.Shared;
using Object = Monkey.Shared.Object;

namespace Monkey
{
    public partial class Compiler
    {
        private CompilerState internalState;

        public Compiler()
        {
            internalState = new CompilerState { SymbolTable = new SymbolTable() };
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
            return new CompilerState
            {
                Constants = new List<Object>(),
                Errors = new List<AssertionError>(),
                Instructions = new List<byte>(),
                SymbolTable = internalState.SymbolTable
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
            var position = previousState.Instructions.Count;

            return Factory.CompilerState()
                .Assign(previousState)
                .CurrentInstruction(new Instruction { Opcode = opcode, Position = position })
                .PreviousInstruction(previousState.CurrentInstruction)
                .Instructions(instruction)
                .Create();
        }
    }
}
