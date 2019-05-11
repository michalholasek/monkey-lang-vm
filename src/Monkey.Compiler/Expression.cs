using System;
using System.Collections.Generic;

using Monkey.Shared;
using static Monkey.Evaluator.Utilities;
using Object = Monkey.Shared.Object;

namespace Monkey
{
    public partial class Compiler
    {
        private CompilerState CompileExpression(CompilerState previousState)
        {
            var expression = ((Statement)previousState.Node).Expression;
            var expressionState = CompileExpressionInner(expression, previousState);

            // Add Pop instruction after every expression to clean up the stack
            return Emit(3, new List<int>(), expressionState);
        }

        private CompilerState CompileExpressionInner(Expression expression, CompilerState previousState)
        {
            switch (expression.Kind)
            {
                case ExpressionKind.Integer:
                    return CompileIntegerExpression(expression, previousState);
                case ExpressionKind.Boolean:
                    return CompileBooleanExpression(expression, previousState);
                case ExpressionKind.Prefix:
                    return CompilePrefixExpression(expression, previousState);
                case ExpressionKind.Infix:
                    return CompileInfixExpression(expression, previousState);
                case ExpressionKind.IfElse:
                    return CompileIfElseExpression(expression, previousState);
            }

            return previousState;
        }

        private CompilerState CompileBooleanExpression(Expression expression, CompilerState previousState)
        {
            var expressionValue = (bool)((BooleanExpression)expression).Value;
            var opcode = expressionValue == true ? (byte)7 : (byte)8;

            return Emit(opcode, new List<int> {}, previousState);
        }

        private CompilerState CompileIfElseExpression(Expression expression, CompilerState previousState)
        {
            var ifElseExpression = (IfElseExpression)expression;
            var conditionState = CompileExpressionInner(ifElseExpression.Condition, previousState);

            // Create Opcode.JumpNotTruthy with transient offset, we are going
            // to change it to correct one later
            var jumpNotTruthyInstructionState = Emit(15, new List<int> { 9999 }, conditionState);
            var jumpNotTruthyInstructionPosition = jumpNotTruthyInstructionState.CurrentInstruction.Position;

            var consequenceState = CompileStatements(ifElseExpression.Consequence.Statements, jumpNotTruthyInstructionState);

            // We want to leave last expression value on the stack, hence we
            // we need to check whether there is a Pop instruction after last
            // expression. If so, remove it
            if (consequenceState.CurrentInstruction.Opcode == 3)
            {
                consequenceState = RemoveLastPopInstruction(consequenceState);
            }

            // Create Opcode.Jump with transient offset, we are going
            // to change it to correct one later
            var jumpInstructionState = Emit(14, new List<int> { 9999 }, consequenceState);
            var jumpInstructionPosition = jumpInstructionState.CurrentInstruction.Position;

            var afterJumpInstructionState = ReplaceInstruction(
                jumpNotTruthyInstructionPosition,
                Bytecode.Create(15, new List<int> { jumpInstructionState.Instructions.Count }),
                jumpInstructionState
            );

            CompilerState alternativeState;

            if (ifElseExpression.Alternative == null)
            {
                // Emit Opcode.Null as alternative branch
                alternativeState = Emit(16, new List<int>(), afterJumpInstructionState);
            }
            else
            {
                alternativeState = CompileStatements(ifElseExpression.Alternative.Statements, afterJumpInstructionState);
                
                if (alternativeState.CurrentInstruction.Opcode == 3)
                {
                    alternativeState = RemoveLastPopInstruction(alternativeState);
                }
            }

            // Set Opcode.Jump operand to correct offset and return compiled expression
            return ReplaceInstruction(
                jumpInstructionPosition,
                Bytecode.Create(14, new List<int> { alternativeState.Instructions.Count }),
                alternativeState
            );
        }

        private CompilerState CompileInfixExpression(Expression expression, CompilerState previousState)
        {
            var infixExpression = (InfixExpression)expression;

            CompilerState leftExpressionState;
            CompilerState rightExpressionState;
            
            byte op;

            // Handle special cases, such as 1 < 2, where we are switching
            // operands and turning it into 2 > 1 (GreaterThan) expression
            // to keep the instruction set small
            switch (infixExpression.Operator.Kind)
            {
                case SyntaxKind.LessThan:
                    rightExpressionState = CompileExpressionInner(infixExpression.Right, previousState);
                    leftExpressionState = CompileExpressionInner(infixExpression.Left, rightExpressionState);
                    break;
                default:
                    leftExpressionState = CompileExpressionInner(infixExpression.Left, previousState);
                    rightExpressionState = CompileExpressionInner(infixExpression.Right, leftExpressionState);
                    break;
            }

            switch (infixExpression.Operator.Kind)
            {
                case SyntaxKind.Plus:
                    op = 2;
                    break;
                case SyntaxKind.Minus:
                    op = 4;
                    break;
                case SyntaxKind.Asterisk:
                    op = 5;
                    break;
                case SyntaxKind.Slash:
                    op = 6;
                    break;
                case SyntaxKind.Equal:
                    op = 9;
                    break;
                case SyntaxKind.NotEqual:
                    op = 10;
                    break;
                case SyntaxKind.GreaterThan:
                case SyntaxKind.LessThan:
                    op = 11;
                    break;
                default:
                    return rightExpressionState;
            }

            return Emit(op, new List<int>(), rightExpressionState);
        }

        private CompilerState CompileIntegerExpression(Expression expression, CompilerState previousState)
        {
            var integerExpression = (IntegerExpression)expression;

            return Factory.CompilerState()
                .Assign(Emit(1, new List<int> { integerExpression.Value }, previousState))
                .Constant(integerExpression.Value.ToString(), CreateObject(ObjectKind.Integer, integerExpression.Value))
                .Create();
        }

        private CompilerState CompilePrefixExpression(Expression expression, CompilerState previousState)
        {
            var prefixExpression = (PrefixExpression)expression;
            var rightExpressionState = CompileExpressionInner(prefixExpression.Right, previousState);
            var op = ((InfixExpression)prefixExpression.Left).Operator;

            byte opcode;
            
            switch (op.Kind)
            {
                case SyntaxKind.Minus:
                    opcode = 12;
                    break;
                case SyntaxKind.Bang:
                    opcode = 13;
                    break;
                default:
                    return rightExpressionState;
            }

            return Emit(opcode, new List<int>(), rightExpressionState);
        }
    }
}
