using System;
using System.Collections.Generic;

using Monkey.Shared.Parser.Ast;
using Object = Monkey.Shared.Evaluator.Object;

namespace Monkey.Shared.Evaluator
{
    public partial class Evaluator
    {
        public Object Evaluate(Program program, IEnvironment env)
        {
            var obj = EvaluateNode(program, env);

            if (obj.Kind == ObjectKind.Return)
            {
                return (Object)obj.Value;
            }

            return obj;
        }

        private static Object EvaluateNode(Node node, IEnvironment env)
        {
            switch (node.Kind)
            {
                case NodeKind.Program:
                    return EvaluateStatements(((Program)node).Statements, env);
                case NodeKind.Let:
                    return EvaluateLetStatement((Statement)node, env);
                case NodeKind.Return:
                    return EvaluateReturnStatement((Statement)node, env);
                case NodeKind.Expression:
                    return EvaluateExpression(((Statement)node).Expression, env);
                default:
                    return Utilities.CreateObject(ObjectKind.Null, null);
            }
        }
    }
}
