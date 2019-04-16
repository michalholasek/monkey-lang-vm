using System.Collections.Generic;
using System.Linq;

using Monkey.Shared;
using Monkey.Shared.Scanner;
using Monkey.Shared.Parser.Ast;

namespace Monkey.Shared.Parser
{
    public partial class Parser
    {
        public Program Parse(List<Token> tokens)
        {
            var ast = CreateProgramNode(tokens);
            var currentState = CreateInitialState(tokens);

            while (currentState.Position < currentState.Tokens.Count)
            {
                currentState.Token = currentState.Tokens[currentState.Position];

                if (currentState.Token.Kind == SyntaxKind.EOF) { break; }

                currentState = ParseStatement(currentState);

                if (currentState.Errors.Count == 0)
                {
                    ast.Statements.Add(currentState.Statement);
                }
                else
                {
                    ast.Errors.AddRange(currentState.Errors);
                }
            }

            return ast;
        }

        private StatementParseResult CreateInitialState(List<Token> tokens)
        {
            return Factory.StatementParseResult()
                .Errors(new List<AssertionError>())
                .Position(0)
                .Tokens(tokens)
                .Create();
        }

        private Program CreateProgramNode(List<Token> tokens)
        {
            return Factory.Program().Tokens(tokens).Create();
        }
    }
}
