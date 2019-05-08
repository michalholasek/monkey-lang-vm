using System;
using System.Collections.Generic;

namespace Monkey.Shared
{
    public class BlockStatementParseResult
    {
        public int Position { get; set; }
        public List<Statement> Statements { get; set; }
    }

    internal class ExpressionListParseResult
    {
        public List<Expression> Expressions { get; set; }
        public int Position { get; set; }
    }

    internal class FunctionParametersParseResult
    {
        public int Position { get; set; }
        public List<Token> Parameters { get; set; }
    }

    internal class ExpressionBuilderState
    {
        public List<AssertionError> Errors { get; set; }
        public Expression Expression { get; set; }
        public int Position { get; set; }
        public int Range { get; set; }
        public StatementBuilderState Statement { get; set; }
        public List<Token> Tokens { get; set; }
    }

    public class ExpressionParseResult
    {
        public List<AssertionError> Errors { get; set; }
        public Expression Expression { get; set; }
        public int Position { get; set; }
        public Precedence Precedence { get; set; }
        public List<Token> Tokens { get; set; }
    }

    internal class StatementBuilderState
    {
        public List<AssertionError> Errors { get; set; }
        public Expression Expression { get; set; }
        public NodeKind Kind { get; set; }
        public int Position { get; set; }
        public int Range { get; set; }
        public List<Token> Tokens { get; set; }
    }

    public class StatementParseResult
    {
        public List<AssertionError> Errors { get; set; }
        public int Position { get; set; }
        public Statement Statement { get; set; }
        public Token Token { get; set; }
        public List<Token> Tokens { get; set; }
    }
}
