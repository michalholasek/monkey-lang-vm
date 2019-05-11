﻿using System;
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
                Errors = new List<AssertionError>(),
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
                    return CompileProgramNode(previousState);
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
