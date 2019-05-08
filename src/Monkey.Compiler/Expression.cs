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
            return Factory.CompilerState()
                .Assign(expressionState)
                .Instructions(Bytecode.Create(3, new List<int>()))
                .Create();
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
            }

            return previousState;
        }

        private CompilerState CompileBooleanExpression(Expression expression, CompilerState previousState)
        {
            var expressionValue = (bool)((BooleanExpression)expression).Value;
            var instruction = Bytecode.Create(expressionValue == true ? (byte)7 : (byte)8, new List<int> {});

            return Factory.CompilerState()
                .Assign(previousState)
                .Instructions(instruction)
                .Create();
        }

        private CompilerState CompileInfixExpression(Expression expression, CompilerState previousState)
        {
            var infixExpression = (InfixExpression)expression;

            CompilerState leftExpressionState;
            CompilerState rightExpressionState;
            
            List<byte> op;

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
                    op = Bytecode.Create(2, new List<int>());
                    break;
                case SyntaxKind.Minus:
                    op = Bytecode.Create(4, new List<int>());
                    break;
                case SyntaxKind.Asterisk:
                    op = Bytecode.Create(5, new List<int>());
                    break;
                case SyntaxKind.Slash:
                    op = Bytecode.Create(6, new List<int>());
                    break;
                case SyntaxKind.Equal:
                    op = Bytecode.Create(9, new List<int>());
                    break;
                case SyntaxKind.NotEqual:
                    op = Bytecode.Create(10, new List<int>());
                    break;
                case SyntaxKind.GreaterThan:
                case SyntaxKind.LessThan:
                    op = Bytecode.Create(11, new List<int>());
                    break;
                default:
                    op = new List<byte>();
                    break;
            }

            return Factory.CompilerState()
                .Assign(rightExpressionState)
                .Instructions(op)
                .Create();
        }

        private CompilerState CompileIntegerExpression(Expression expression, CompilerState previousState)
        {
            var integerExpression = (IntegerExpression)expression;
            var instruction = Bytecode.Create(1, new List<int> { integerExpression.Value });

            return Factory.CompilerState()
                .Assign(previousState)
                .Constant(integerExpression.Value.ToString(), CreateObject(ObjectKind.Integer, integerExpression.Value))
                .Instructions(instruction)
                .Create();
        }

        private CompilerState CompilePrefixExpression(Expression expression, CompilerState previousState)
        {
            var prefixExpression = (PrefixExpression)expression;
            var rightExpressionState = CompileExpressionInner(prefixExpression.Right, previousState);
            var op = ((InfixExpression)prefixExpression.Left).Operator;

            List<byte> operatorInstruction;
            
            switch (op.Kind)
            {
                case SyntaxKind.Minus:
                    operatorInstruction = Bytecode.Create(12, new List<int> {});
                    break;
                case SyntaxKind.Bang:
                    operatorInstruction = Bytecode.Create(13, new List<int> {});
                    break;
                default:
                    operatorInstruction = new List<byte>();
                    break;
            }

            return Factory.CompilerState()
                .Assign(rightExpressionState)
                .Instructions(operatorInstruction)
                .Create();
        }
    }
}
