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
            return Emit((byte)Opcode.Name.Pop, new List<int>(), expressionState);
        }

        private CompilerState CompileExpressionInner(Expression expression, CompilerState previousState)
        {
            switch (expression.Kind)
            {
                case ExpressionKind.Integer:
                    return CompileIntegerExpression(expression, previousState);
                case ExpressionKind.Boolean:
                    return CompileBooleanExpression(expression, previousState);
                case ExpressionKind.String:
                    return CompileStringExpression(expression, previousState);
                case ExpressionKind.Identifier:
                    return CompileIdentifierExpression(expression, previousState);
                case ExpressionKind.Prefix:
                    return CompilePrefixExpression(expression, previousState);
                case ExpressionKind.Infix:
                    return CompileInfixExpression(expression, previousState);
                case ExpressionKind.IfElse:
                    return CompileIfElseExpression(expression, previousState);
                case ExpressionKind.Array:
                    return CompileArrayExpression(expression, previousState);
            }

            return previousState;
        }

        private CompilerState CompileArrayExpression(Expression expression, CompilerState previousState)
        {
            var arrayExpression = (ArrayExpression)expression;
            var arrayExpressionState = previousState;

            foreach (var element in arrayExpression.Elements)
            {
                arrayExpressionState = CompileExpressionInner(element, previousState);

                if (arrayExpressionState.Errors.Count > 0)
                {
                    return arrayExpressionState;
                }
            }

            return Emit((byte)Opcode.Name.Array, new List<int> { arrayExpression.Elements.Count }, arrayExpressionState);
        }

        private CompilerState CompileBooleanExpression(Expression expression, CompilerState previousState)
        {
            var expressionValue = (bool)((BooleanExpression)expression).Value;
            var opcode = expressionValue == true ? (byte)Opcode.Name.True : (byte)Opcode.Name.False;

            return Emit(opcode, new List<int> {}, previousState);
        }

        private CompilerState CompileIdentifierExpression(Expression expression, CompilerState previousState)
        {
            var identifier = ((IdentifierExpression)expression).Value;
            
            var symbol = previousState.SymbolTable.Resolve(identifier);

            if (symbol != Symbol.Undefined)
            {
                return Emit((byte)Opcode.Name.GetGlobal, new List<int> { symbol.Index }, previousState);
            }

            var info = new ErrorInfo
            {
                Code = ErrorCode.UndefinedVariable,
                Kind = ErrorKind.UndefinedVariable,
                Offenders = new List<object> { identifier },
                Source = ErrorSource.Compiler
            };

            return Factory.CompilerState()
                .Assign(previousState)
                .Errors(new List<AssertionError> { Error.Create(info) })
                .Create();
        }

        private CompilerState CompileIfElseExpression(Expression expression, CompilerState previousState)
        {
            var ifElseExpression = (IfElseExpression)expression;
            var conditionState = CompileExpressionInner(ifElseExpression.Condition, previousState);

            // Create Opcode.JumpNotTruthy with transient offset, we are going
            // to change it to correct one later
            var jumpNotTruthyInstructionState = Emit((byte)Opcode.Name.JumpNotTruthy, new List<int> { 9999 }, conditionState);
            var jumpNotTruthyInstructionPosition = jumpNotTruthyInstructionState.CurrentInstruction.Position;

            var consequenceState = CompileStatements(ifElseExpression.Consequence.Statements, jumpNotTruthyInstructionState);

            // We want to leave last expression value on the stack, hence we
            // we need to check whether there is a Pop instruction after last
            // expression. If so, remove it
            if (consequenceState.CurrentInstruction.Opcode == (byte)Opcode.Name.Pop)
            {
                consequenceState = RemoveLastPopInstruction(consequenceState);
            }

            // Create Opcode.Jump with transient offset, we are going
            // to change it to correct one later
            var jumpInstructionState = Emit((byte)Opcode.Name.Jump, new List<int> { 9999 }, consequenceState);
            var jumpInstructionPosition = jumpInstructionState.CurrentInstruction.Position;

            var afterJumpInstructionState = ReplaceInstruction(
                jumpNotTruthyInstructionPosition,
                Bytecode.Create((byte)Opcode.Name.JumpNotTruthy, new List<int> { jumpInstructionState.Instructions.Count }),
                jumpInstructionState
            );

            CompilerState alternativeState;

            if (ifElseExpression.Alternative == null)
            {
                // Emit Opcode.Null as alternative branch
                alternativeState = Emit((byte)Opcode.Name.Null, new List<int>(), afterJumpInstructionState);
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
                Bytecode.Create((byte)Opcode.Name.Jump, new List<int> { alternativeState.Instructions.Count }),
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
                    op = (byte)Opcode.Name.Add;
                    break;
                case SyntaxKind.Minus:
                    op = (byte)Opcode.Name.Subtract;
                    break;
                case SyntaxKind.Asterisk:
                    op = (byte)Opcode.Name.Multiply;
                    break;
                case SyntaxKind.Slash:
                    op = (byte)Opcode.Name.Divide;
                    break;
                case SyntaxKind.Equal:
                    op = (byte)Opcode.Name.Equal;
                    break;
                case SyntaxKind.NotEqual:
                    op = (byte)Opcode.Name.NotEqual;
                    break;
                case SyntaxKind.GreaterThan:
                case SyntaxKind.LessThan:
                    op = (byte)Opcode.Name.GreaterThan;
                    break;
                default:
                    return rightExpressionState;
            }

            return Emit(op, new List<int>(), rightExpressionState);
        }

        private CompilerState CompileIntegerExpression(Expression expression, CompilerState previousState)
        {
            var integerExpression = (IntegerExpression)expression;

            var index = DetermineConstantIndex(expression, previousState);

            return Factory.CompilerState()
                .Assign(Emit((byte)Opcode.Name.Constant, new List<int> { index }, previousState))
                .Constant(index, CreateObject(ObjectKind.Integer, integerExpression.Value))
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
                    opcode = (byte)Opcode.Name.Minus;
                    break;
                case SyntaxKind.Bang:
                    opcode = (byte)Opcode.Name.Bang;
                    break;
                default:
                    return rightExpressionState;
            }

            return Emit(opcode, new List<int>(), rightExpressionState);
        }

        private CompilerState CompileStringExpression(Expression expression, CompilerState previousState)
        {
            var stringExpression = (StringExpression)expression;

            var index = DetermineConstantIndex(expression, previousState);

            return Factory.CompilerState()
                .Assign(Emit((byte)Opcode.Name.Constant, new List<int> { index }, previousState))
                .Constant(index, CreateObject(ObjectKind.String, stringExpression.Value))
                .Create();
        }

        private int DetermineConstantIndex(Expression expression, CompilerState previousState)
        {
            var index = previousState.Constants.FindIndex(item =>
            {
                switch (expression.Kind)
                {
                    case ExpressionKind.String:
                        return item.Value.ToString() == ((StringExpression)expression).Value;
                    default:
                        return (int)item.Value == ((IntegerExpression)expression).Value;
                }
            });

            if (index < 0)
            {
                index = previousState.Constants.Count;
            }

            return index;
        }
    }
}
