using System.Collections.Generic;

using Monkey.Shared.Parser.Ast;
using Monkey.Shared.Scanner;

namespace Monkey.Shared.Parser
{
    internal class ProgramOptions
    {
        public List<AssertionError> Errors { get; set; }
        public NodeKind Kind { get; set; }
        public int Position { get; set; }
        public int Range { get; set; }
        public List<Statement> Statements { get; set; }
        public List<Token> Tokens { get; set; }
    }

    internal class StatementOptions
    {
        public Expression Expression { get; set; }
        public NodeKind Kind { get; set; }
        public Token Identifier { get; set; }
        public int Position { get; set; }
        public int Range { get; set; }
    }
}
