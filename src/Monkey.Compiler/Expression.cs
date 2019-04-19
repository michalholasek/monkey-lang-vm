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
            return CompileExpressionInner(expression, previousState);
        }

        private CompilerState CompileExpressionInner(Expression expression, CompilerState previousState)
        {
            switch (expression.Kind)
            {
                case ExpressionKind.Integer:
                    return CompileIntegerExpression(expression, previousState);
                case ExpressionKind.Infix:
                    return CompileInfixExpression(expression, previousState);
            }

            return previousState;
        }

        private CompilerState CompileInfixExpression(Expression expression, CompilerState previousState)
        {
            var infixExpression = (InfixExpression)expression;
            var leftExpressionState = CompileExpressionInner(infixExpression.Left, previousState);
            var rightExpressionState = CompileExpressionInner(infixExpression.Right, leftExpressionState);
            var instructions = new List<byte>();

            return rightExpressionState;
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
    }
}
