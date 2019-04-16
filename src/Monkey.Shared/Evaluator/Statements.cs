using System;
using System.Collections.Generic;

using Monkey.Shared.Parser.Ast;
using Environment = Monkey.Shared.Evaluator.Environment;
using Object = Monkey.Shared.Evaluator.Object;

namespace Monkey.Shared.Evaluator
{
    public partial class Evaluator
    {
        private static Object EvaluateStatements(List<Statement> statements, IEnvironment env)
        {
            var obj = Utilities.CreateObject(ObjectKind.Null, null);

            foreach (var statement in statements)
            {
                obj = EvaluateNode(statement, env);

                if (obj.Kind == ObjectKind.Error || obj.Kind == ObjectKind.Return)
                {
                    return obj;
                }
            }

            return obj;
        }

        private static Object EvaluateLetStatement(Statement statement, IEnvironment env)
        {
            var obj = Utilities.CreateObject(ObjectKind.Null, null);

            if (statement.Identifier == null || statement.Expression == null)
            {
                return obj;
            }

            var value = EvaluateExpression(statement.Expression, env);

            if (value.Kind == ObjectKind.Error)
            {
                return value;
            }

            env.Set(statement.Identifier.Literal, value);

            return obj;
        }

        private static Object EvaluateReturnStatement(Statement statement, IEnvironment env)
        {
            return Utilities.CreateObject(ObjectKind.Return, EvaluateExpression(statement.Expression, env));
        }
    }
}
