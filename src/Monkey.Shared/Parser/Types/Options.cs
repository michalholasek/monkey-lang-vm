using System.Collections.Generic;

namespace Monkey.Shared
{
    public class ProgramOptions
    {
        public List<AssertionError> Errors { get; set; }
        public NodeKind Kind { get; set; }
        public int Position { get; set; }
        public int Range { get; set; }
        public List<Statement> Statements { get; set; }
        public List<Token> Tokens { get; set; }
    }

    public class StatementOptions
    {
        public Expression Expression { get; set; }
        public NodeKind Kind { get; set; }
        public Token Identifier { get; set; }
        public int Position { get; set; }
        public int Range { get; set; }
    }
}
