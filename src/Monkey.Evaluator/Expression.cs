using System;
using System.Collections.Generic;
using System.Linq;

using Monkey.Shared;
using static Monkey.Evaluator.Utilities;
using Environment = Monkey.Shared.Environment;
using Object = Monkey.Shared.Object;

namespace Monkey
{
    public partial class Evaluator
    {
        private static Object ApplyFunction(Object obj, List<Object> args)
        {
            var functionExpression = (FunctionExpression)obj.Value;
            IEnvironment env = obj.Environment;

            if (functionExpression.Parameters != null && args != null)
            {
                env = EncloseEnvironment(functionExpression.Parameters, args, env);
            }

            if (functionExpression.Body != null)
            {
                return EvaluateStatements(functionExpression.Body.Statements, env);
            }

            return CreateObject(ObjectKind.Null, null);
        }

        private static IEnvironment EncloseEnvironment(List<Token> parameters, List<Object> args, IEnvironment env)
        {
            var newEnvironment = new EnclosedEnvironment(env);

            for (var i = 0; i < parameters.Count; i++)
            {
                newEnvironment.Set(parameters[i].Literal, args[i]);
            }

            return newEnvironment;
        }

        private static bool IsTruthy(Object obj)
        {
            switch (obj.Kind)
            {
                case ObjectKind.Boolean:
                    return (bool)obj.Value;
                case ObjectKind.Integer:
                    return (int)obj.Value != 0 ? true : false;
                case ObjectKind.String:
                    return (string)obj.Value != String.Empty ? true : false;
                default:
                    return false;
            }
        }

        private static Object EvaluateExpression(Expression expression, IEnvironment env)
        {
            switch (expression.Kind)
            {
                case ExpressionKind.Integer:
                    return EvaluateIntegerExpression(expression);
                case ExpressionKind.Boolean:
                    return EvaluateBooleanExpression(expression);
                case ExpressionKind.String:
                    return EvaluateStringExpression(expression);
                case ExpressionKind.Identifier:
                    return EvaluateIdentifierExpression(expression, env);
                case ExpressionKind.Prefix:
                    return EvaluatePrefixExpression(expression, env);
                case ExpressionKind.Infix:
                    return EvaluateInfixExpression(expression, env);
                case ExpressionKind.IfElse:
                    return EvaluateIfElseExpression(expression, env);
                case ExpressionKind.Function:
                    return EvaluateFunctionExpression(expression, env);
                case ExpressionKind.Call:
                    return EvaluateCallExpression(expression, env);
                case ExpressionKind.Array:
                    return EvaluateArrayExpression(expression, env);
                case ExpressionKind.Hash:
                    return EvaluateHashExpression(expression, env);
                case ExpressionKind.Index:
                    return EvaluateIndexExpression(expression, env);
                default:
                    return CreateObject(ObjectKind.Null, null);
            }
        }

        private static Object EvaluateArrayExpression(Expression expression, IEnvironment env)
        {
            var arrayExpression = (ArrayExpression)expression;
            return CreateObject(ObjectKind.Array, EvaluateExpressionList(arrayExpression.Elements, env));
        }

        private static Object EvaluateArrayIndexExpression(Object array, Object index)
        {
            var elements = (List<Object>)array.Value;
            var indexValue = (int)index.Value;

            if (elements.Count == 0 || indexValue < 0 || indexValue > elements.Count - 1)
            {
                return CreateObject(ObjectKind.Null, null);
            }

            return elements[indexValue];
        }

        private static Object EvaluateBangOperatorExpression(Expression expression, IEnvironment env)
        {
            var obj = EvaluateExpression(expression, env);
            
            switch (obj.Kind)
            {
                case ObjectKind.Integer:
                    return CreateObject(ObjectKind.Boolean, !((int)obj.Value != 0));
                case ObjectKind.Boolean:
                    return CreateObject(ObjectKind.Boolean, !(bool)obj.Value);
                default:
                    return CreateObject
                    (
                        ObjectKind.Error,
                        Error.CreateEvaluationError(AssertionErrorKind.InvalidType, obj.Kind)
                    );
            }
        }

        private static Object EvaluateBooleanExpression(Expression expression)
        {
            return CreateObject(ObjectKind.Boolean, ((BooleanExpression)expression).Value);
        }

        private static Object EvaluateBooleanInfixExpression(bool left, Token op, bool right)
        {
            switch (op.Kind)
            {
                case SyntaxKind.Equal:
                    return CreateObject(ObjectKind.Boolean, left == right);
                case SyntaxKind.NotEqual:
                    return CreateObject(ObjectKind.Boolean, left != right);
                default:
                    return CreateObject
                    (
                        ObjectKind.Error,
                        Error.CreateEvaluationError(AssertionErrorKind.UnknownOperator, op.Kind)
                    );
            }
        }

        private static Object EvaluateCallExpression(Expression expression, IEnvironment env)
        {
            List<Object> args = new List<Object>();
            CallExpression callExpression = (CallExpression)expression;
            Object obj;

            if (callExpression.Identifier != null)
            {
                obj = env.Get(callExpression.Identifier.Literal);
                obj = obj == null ? BuiltIn.Functions[callExpression.Identifier.Literal] : obj;
            }
            else
            {
                obj = CreateObject(ObjectKind.Function, callExpression.Function);
            }

            if (callExpression.Arguments != null)
            {
                args = EvaluateExpressionList(callExpression.Arguments, env);
            }

            if (obj.Kind == ObjectKind.BuiltIn)
            {
                var fn = (Func<List<Object>, Object>)obj.Value;
                return fn(args);
            }
            else
            {
                return ApplyFunction(obj, args);
            }
        }

        private static List<Object> EvaluateExpressionList(List<Expression> expressions, IEnvironment env)
        {
            List<Object> objects = new List<Object>();

            foreach (var expression in expressions)
            {
                objects.Add(EvaluateExpression(expression, env));
            }

            return objects;
        }

        private static Object EvaluateFunctionExpression(Expression expression, IEnvironment env)
        {
            var obj = CreateObject(ObjectKind.Function, expression);
            obj.Environment = env;
            return obj;
        }

        private static Object EvaluateHashExpression(Expression expression, IEnvironment env)
        {
            var hashExpression = (HashExpression)expression;

            var hash = new Dictionary<string, Object>();
            
            for (var i = 0; i < hashExpression.Keys.Count; i++)
            {
                var key = EvaluateExpression(hashExpression.Keys[i], env);
                var value = EvaluateExpression(hashExpression.Values[i], env);

                var previousKey = hash.Keys.Where(item => item == key.Value.ToString()).FirstOrDefault();
                if (previousKey != null)
                {
                    hash[previousKey] = CreateObject(value.Kind, value.Value);
                }
                else
                {
                    hash.Add(key.Value.ToString(), CreateObject(value.Kind, value.Value));
                }
            }

            return CreateObject(ObjectKind.Hash, hash);
        }

        private static Object EvaluateHashIndexExpression(Object hash, Object key)
        {
            if (key.Kind != ObjectKind.Integer && key.Kind != ObjectKind.Boolean && key.Kind != ObjectKind.String)
            {
                return CreateObject
                (
                    ObjectKind.Error,
                    Error.CreateEvaluationError(AssertionErrorKind.InvalidIndex, key.Kind)
                );
            }

            var hashtable = (Dictionary<string, Object>)hash.Value;
            var keyValue = key.Value.ToString();

            if (hashtable.Count == 0 || keyValue == String.Empty)
            {
                return CreateObject(ObjectKind.Null, null);
            }

            var hashKey = hashtable.Keys.Where(item => item == keyValue).FirstOrDefault();
            if (hashKey == null)
            {
                return CreateObject(ObjectKind.Null, null);
            }

            return hashtable[keyValue];
        }

        private static Object EvaluateIdentifierExpression(Expression expression, IEnvironment env)
        {
            var value = (string)((IdentifierExpression)expression).Value;
            var identifier = env.Get(value);

            if (identifier != null)
            {
                return identifier;
            }

            if (BuiltIn.Functions.Keys.Where(key => key == value).FirstOrDefault() != null)
            {
                return BuiltIn.Functions[value];
            }

            return CreateObject
            (
                ObjectKind.Error,
                Error.CreateEvaluationError(AssertionErrorKind.InvalidIdentifier, $"{value} not found")
            );
        }

        private static Object EvaluateIfElseExpression(Expression expression, IEnvironment env)
        {
            var ifElseExpression = (IfElseExpression)expression;

            var condition = EvaluateExpression(ifElseExpression.Condition, env);
            if (condition.Kind == ObjectKind.Error)
            {
                return condition;
            }

            if (IsTruthy(condition))
            {
                return EvaluateStatements(ifElseExpression.Consequence.Statements, env);
            }
            else if (ifElseExpression.Alternative != null)
            {
                return EvaluateStatements(ifElseExpression.Alternative.Statements, env);
            }
            else
            {
                return CreateObject(ObjectKind.Null, null);
            }
        }

        private static Object EvaluateIndexExpression(Expression expression, IEnvironment env)
        {
            var indexExpression = (IndexExpression)expression;

            var left = EvaluateExpression(indexExpression.Left, env);
            var index = EvaluateExpression(indexExpression.Index, env);

            switch (left.Kind)
            {
                case ObjectKind.Hash:
                    return EvaluateHashIndexExpression(left, index);
                default:
                    return EvaluateArrayIndexExpression(left, index);
            }
        }

        private static Object EvaluateInfixExpression(Expression expression, IEnvironment env)
        {
            var infixExpression = (InfixExpression)expression;

            var left = EvaluateExpression(infixExpression.Left, env);
            if (left.Kind == ObjectKind.Error)
            {
                return left;
            }

            var right = EvaluateExpression(infixExpression.Right, env);
            if (right.Kind == ObjectKind.Error)
            {
                return right;
            }

            if (left.Kind != right.Kind)
            {
                return CreateObject
                (
                    ObjectKind.Error,
                    Error.CreateEvaluationError(AssertionErrorKind.InvalidToken, actual: right.Kind, expected: left.Kind)
                );
            }

            switch (left.Kind)
            {
                case ObjectKind.Integer:
                    return EvaluateIntegerInfixExpression((int)left.Value, infixExpression.Operator, (int)right.Value);
                case ObjectKind.Boolean:
                    return EvaluateBooleanInfixExpression((bool)left.Value, infixExpression.Operator, (bool)right.Value);
                case ObjectKind.String:
                    return EvaluateStringInfixExpression((string)left.Value, infixExpression.Operator, (string)right.Value);
                default:
                    return CreateObject
                    (
                        ObjectKind.Error,
                        Error.CreateEvaluationError(AssertionErrorKind.InvalidType, left.Kind)
                    );
            }
        }

        private static Object EvaluateIntegerExpression(Expression expression)
        {
            return CreateObject(ObjectKind.Integer, ((IntegerExpression)expression).Value);
        }

        private static Object EvaluateIntegerInfixExpression(int left, Token op, int right)
        {
            switch (op.Kind)
            {
                case SyntaxKind.Plus:
                    return CreateObject(ObjectKind.Integer, left + right);
                case SyntaxKind.Minus:
                    return CreateObject(ObjectKind.Integer, left - right);
                case SyntaxKind.Asterisk:
                    return CreateObject(ObjectKind.Integer, left * right);
                case SyntaxKind.Slash:
                    return CreateObject(ObjectKind.Integer, left / right);
                case SyntaxKind.GreaterThan:
                    return CreateObject(ObjectKind.Boolean, left > right);
                case SyntaxKind.LessThan:
                    return CreateObject(ObjectKind.Boolean, left < right);
                case SyntaxKind.Equal:
                    return CreateObject(ObjectKind.Boolean, left == right);
                case SyntaxKind.NotEqual:
                    return CreateObject(ObjectKind.Boolean, left != right);
                default:
                    return CreateObject
                    (
                        ObjectKind.Error,
                        Error.CreateEvaluationError(AssertionErrorKind.UnknownOperator, op.Kind)
                    );
            }
        }

        private static Object EvaluateMinusOperatorExpression(Expression expression, IEnvironment env)
        {
            var value = EvaluateExpression(expression, env);

            if (value.Kind == ObjectKind.Error)
            {
                return value;
            }

            if (value.Kind != ObjectKind.Integer)
            {
                return CreateObject
                (
                    ObjectKind.Error,
                    Error.CreateEvaluationError
                    (
                        AssertionErrorKind.InvalidToken, actual: value.Kind, expected: ObjectKind.Integer
                    )
                );
            }

            return CreateObject(ObjectKind.Integer, -(int)value.Value);
        }

        private static Object EvaluatePrefixExpression(Expression expression, IEnvironment env)
        {
            var prefixExpression = (PrefixExpression)expression;
            var op = ((InfixExpression)prefixExpression.Left).Operator;

            switch (op.Kind)
            {
                case SyntaxKind.Bang:
                    return EvaluateBangOperatorExpression(prefixExpression.Right, env);
                case SyntaxKind.Minus:
                    return EvaluateMinusOperatorExpression(prefixExpression.Right, env);
                default:
                    return CreateObject
                    (
                        ObjectKind.Error,
                        Error.CreateEvaluationError(AssertionErrorKind.UnknownOperator, op.Kind)
                    );
            }
        }

        private static Object EvaluateStringExpression(Expression expression)
        {
            return CreateObject(ObjectKind.String, ((StringExpression)expression).Value);
        }

        private static Object EvaluateStringInfixExpression(string left, Token op, string right)
        {
            switch (op.Kind)
            {
                case SyntaxKind.Plus:
                    return CreateObject(ObjectKind.String, string.Join(String.Empty, left, right));
                case SyntaxKind.Equal:
                    return CreateObject(ObjectKind.Boolean, left == right);
                case SyntaxKind.NotEqual:
                    return CreateObject(ObjectKind.Boolean, left != right);
                default:
                    return CreateObject
                    (
                        ObjectKind.Error,
                        Error.CreateEvaluationError(AssertionErrorKind.UnknownOperator, op.Kind)
                    );
            }
        }
    }
}
