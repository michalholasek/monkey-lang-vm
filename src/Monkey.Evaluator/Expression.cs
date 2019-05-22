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

            return Object.Create(ObjectKind.Null, null);
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
                    return Object.Create(ObjectKind.Null, null);
            }
        }

        private static Object EvaluateArrayExpression(Expression expression, IEnvironment env)
        {
            var arrayExpression = (ArrayExpression)expression;
            return Object.Create(ObjectKind.Array, EvaluateExpressionList(arrayExpression.Elements, env));
        }

        private static Object EvaluateArrayIndexExpression(Object array, Object index)
        {
            if (array.Kind != ObjectKind.Array)
            {
                return Object.Create(ObjectKind.Error, Error.Create(new ErrorInfo
                {
                    Code = ErrorCode.ArrayExpressionEvaluation,
                    Offenders = new List<object> { array, index },
                    Kind = ErrorKind.InvalidType,
                    Source = ErrorSource.Evaluator
                }));
            }
            
            if (index.Kind != ObjectKind.Integer)
            {
                return Object.Create(ObjectKind.Error, Error.Create(new ErrorInfo
                {
                    Code = ErrorCode.ArrayIndexExpressionEvaluation,
                    Offenders = new List<object> { array, index },
                    Kind = ErrorKind.InvalidType,
                    Source = ErrorSource.Evaluator
                }));
            }
            
            var elements = (List<Object>)array.Value;
            var indexValue = (int)index.Value;

            if (elements.Count == 0 || indexValue < 0 || indexValue > elements.Count - 1)
            {
                return Object.Create(ObjectKind.Null, null);
            }

            return elements[indexValue];
        }

        private static Object EvaluateBangOperatorExpression(Expression expression, IEnvironment env)
        {
            var obj = EvaluateExpression(expression, env);
            
            switch (obj.Kind)
            {
                case ObjectKind.Integer:
                    return Object.Create(ObjectKind.Boolean, !((int)obj.Value != 0));
                case ObjectKind.Boolean:
                    return Object.Create(ObjectKind.Boolean, !(bool)obj.Value);
                default:
                    return Object.Create(ObjectKind.Error, Error.Create(new ErrorInfo
                    {
                        Code = ErrorCode.BangOperatorExpressionEvaluation,
                        Offenders = new List<object> { obj },
                        Kind = ErrorKind.InvalidType,
                        Source = ErrorSource.Evaluator
                    }));
            }
        }

        private static Object EvaluateBooleanExpression(Expression expression)
        {
            return Object.Create(ObjectKind.Boolean, ((BooleanExpression)expression).Value);
        }

        private static Object EvaluateBooleanInfixExpression(bool left, Token op, bool right)
        {
            switch (op.Kind)
            {
                case SyntaxKind.Equal:
                    return Object.Create(ObjectKind.Boolean, left == right);
                case SyntaxKind.NotEqual:
                    return Object.Create(ObjectKind.Boolean, left != right);
                default:
                    return Object.Create(ObjectKind.Error, Error.Create(new ErrorInfo
                    {
                        Code = ErrorCode.BooleanInfixExpressionEvaluation,
                        Offenders = new List<object> { left, op, right },
                        Kind = ErrorKind.UnknownOperator,
                        Source = ErrorSource.Evaluator
                    }));
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
                obj = obj == null ? Functions.GetByName(callExpression.Identifier.Literal) : obj;
            }
            else
            {
                obj = Object.Create(ObjectKind.Function, callExpression.Function);
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
            var obj = Object.Create(ObjectKind.Function, expression);
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
                    hash[previousKey] = Object.Create(value.Kind, value.Value);
                }
                else
                {
                    hash.Add(key.Value.ToString(), Object.Create(value.Kind, value.Value));
                }
            }

            return Object.Create(ObjectKind.Hash, hash);
        }

        private static Object EvaluateHashIndexExpression(Object hash, Object key)
        {
            if (key.Kind != ObjectKind.Integer && key.Kind != ObjectKind.Boolean && key.Kind != ObjectKind.String)
            {
                return Object.Create(ObjectKind.Error, Error.Create(new ErrorInfo
                {
                    Code = ErrorCode.HashIndexExpressionEvaluation,
                    Offenders = new List<object> { hash, key },
                    Kind = ErrorKind.InvalidType,
                    Source = ErrorSource.Evaluator
                }));
            }

            var hashtable = (Dictionary<string, Object>)hash.Value;
            var keyValue = key.Value.ToString();

            if (hashtable.Count == 0 || keyValue == String.Empty)
            {
                return Object.Create(ObjectKind.Null, null);
            }

            var hashKey = hashtable.Keys.Where(item => item == keyValue).FirstOrDefault();
            if (hashKey == null)
            {
                return Object.Create(ObjectKind.Null, null);
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

            var builtIn = Functions.GetByName(value);
            if (builtIn != default(Object))
            {
                return builtIn;
            }

            return Object.Create(ObjectKind.Error, Error.Create(new ErrorInfo
            {
                Code = ErrorCode.IdentifierExpressionEvaluation,
                Offenders = new List<object> { value },
                Kind = ErrorKind.InvalidIdentifier,
                Source = ErrorSource.Evaluator
            }));
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
                return Object.Create(ObjectKind.Null, null);
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
                return Object.Create(ObjectKind.Error, Error.Create(new ErrorInfo
                {
                    Code = ErrorCode.InfixExpressionEvaluation,
                    Offenders = new List<object> { left, right },
                    Kind = ErrorKind.InvalidType,
                    Source = ErrorSource.Evaluator
                }));
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
                    return Object.Create(ObjectKind.Error, Error.Create(new ErrorInfo
                    {
                        Code = ErrorCode.InfixExpressionOperatorEvaluation,
                        Offenders = new List<object> { left, infixExpression.Operator, right },
                        Kind = ErrorKind.UnknownOperator,
                        Source = ErrorSource.Evaluator
                    }));
            }
        }

        private static Object EvaluateIntegerExpression(Expression expression)
        {
            return Object.Create(ObjectKind.Integer, ((IntegerExpression)expression).Value);
        }

        private static Object EvaluateIntegerInfixExpression(int left, Token op, int right)
        {
            switch (op.Kind)
            {
                case SyntaxKind.Plus:
                    return Object.Create(ObjectKind.Integer, left + right);
                case SyntaxKind.Minus:
                    return Object.Create(ObjectKind.Integer, left - right);
                case SyntaxKind.Asterisk:
                    return Object.Create(ObjectKind.Integer, left * right);
                case SyntaxKind.Slash:
                    return Object.Create(ObjectKind.Integer, left / right);
                case SyntaxKind.GreaterThan:
                    return Object.Create(ObjectKind.Boolean, left > right);
                case SyntaxKind.LessThan:
                    return Object.Create(ObjectKind.Boolean, left < right);
                case SyntaxKind.Equal:
                    return Object.Create(ObjectKind.Boolean, left == right);
                case SyntaxKind.NotEqual:
                    return Object.Create(ObjectKind.Boolean, left != right);
                default:
                    return Object.Create(ObjectKind.Error, Error.Create(new ErrorInfo
                    {
                        Code = ErrorCode.InfixExpressionOperatorEvaluation,
                        Offenders = new List<object> { left, op, right },
                        Kind = ErrorKind.UnknownOperator,
                        Source = ErrorSource.Evaluator
                    }));
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
                return Object.Create(ObjectKind.Error, Error.Create(new ErrorInfo
                {
                    Code = ErrorCode.MinusOperatorExpressionEvaluation,
                    Offenders = new List<object> { value },
                    Kind = ErrorKind.InvalidType,
                    Source = ErrorSource.Evaluator
                }));
            }

            return Object.Create(ObjectKind.Integer, -(int)value.Value);
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
                    return Object.Create(ObjectKind.Error, Error.Create(new ErrorInfo
                    {
                        Code = ErrorCode.UnknownOperator,
                        Offenders = new List<object> { op },
                        Kind = ErrorKind.UnknownOperator,
                        Source = ErrorSource.Evaluator
                    }));
            }
        }

        private static Object EvaluateStringExpression(Expression expression)
        {
            return Object.Create(ObjectKind.String, ((StringExpression)expression).Value);
        }

        private static Object EvaluateStringInfixExpression(string left, Token op, string right)
        {
            switch (op.Kind)
            {
                case SyntaxKind.Plus:
                    return Object.Create(ObjectKind.String, string.Join(String.Empty, left, right));
                case SyntaxKind.Equal:
                    return Object.Create(ObjectKind.Boolean, left == right);
                case SyntaxKind.NotEqual:
                    return Object.Create(ObjectKind.Boolean, left != right);
                default:
                    return Object.Create(ObjectKind.Error, Error.Create(new ErrorInfo
                    {
                        Code = ErrorCode.StringExpressionOperatorEvaluation,
                        Offenders = new List<object> { left, op, right },
                        Kind = ErrorKind.UnknownOperator,
                        Source = ErrorSource.Evaluator
                    }));
            }
        }
    }
}
