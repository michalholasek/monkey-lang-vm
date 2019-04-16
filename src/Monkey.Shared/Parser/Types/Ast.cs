using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using Monkey.Shared.Scanner;

namespace Monkey.Shared.Parser.Ast
{
    public struct AssertionError
    {
        [JsonProperty]
        string Message { get; set; }

        public AssertionError(string message)
        {
            Message = message;
        }
    }

    public enum AssertionErrorKind
    {
        InvalidArgument,
        InvalidIdentifier,
        InvalidIndex,
        InvalidToken,
        InvalidType,
        UnexpectedToken,
        UnknownOperator
    }

    public enum NodeKind
    {
        Illegal,

        // Let and Return ordering matches SyntaxKind
        Let = 17,
        Return = 22,
        Expression = 100,
        Program = 101
    }
    
    public enum ExpressionKind
    {
        Illegal,
        Integer,
        Boolean,
        Prefix,
        Infix,
        IfElse,
        Identifier,
        Function,
        Call,
        String,
        Array,
        Index,
        Hash
    }

    public class Expression
    {
        public ExpressionKind Kind { get; set; }
    }

    public class ArrayExpression : Expression
    {
        public List<Expression> Elements { get; set; }

        public ArrayExpression(List<Expression> elements)
        {
            Elements = elements;
            Kind = ExpressionKind.Array;
        }
    }

    public class BooleanExpression : Expression
    {
        public bool Value { get; set; }

        public BooleanExpression(bool value)
        {
            Kind = ExpressionKind.Boolean;
            Value = value;
        }
    }

    public class CallExpression : Expression
    {
        public List<Expression> Arguments { get; set; }
        public FunctionExpression Function { get; set; }
        public Token Identifier { get; set; }

        public CallExpression(Token identifier, List<Expression> arguments, FunctionExpression fn)
        {
            Arguments = arguments;
            Function = fn;
            Identifier = identifier;
            Kind = ExpressionKind.Call;
        }
    }

    public class FunctionExpression : Expression
    {
        public BlockStatement Body { get; set; }
        public List<Token> Parameters { get; set; }

        public FunctionExpression(List<Token> parameters, BlockStatement body)
        {
            Body = body;
            Kind = ExpressionKind.Function;
            Parameters = parameters;
        }
    }

    public class HashExpression : Expression
    {
        public List<Expression> Keys { get; set; }
        public List<Expression> Values { get; set; }

        public HashExpression(List<Expression> keys, List<Expression> values)
        {
            Keys = keys;
            Kind = ExpressionKind.Hash;
            Values = values;
        }
    }

    public class IndexExpression : Expression
    {
        public Expression Index { get; set; }
        public Expression Left { get; set; }

        public IndexExpression(Expression left, Expression index)
        {
            Index = index;
            Kind = ExpressionKind.Index;
            Left = left;
        }
    }

    public class IdentifierExpression : Expression
    {
        public string Value { get; set; }

        public IdentifierExpression(string value)
        {
            Kind = ExpressionKind.Identifier;
            Value = value;
        }
    }

    public class IfElseExpression : Expression
    {
        public BlockStatement Alternative { get; set; }
        public Expression Condition { get; set; }
        public BlockStatement Consequence { get; set; }

        public IfElseExpression(Expression condition, BlockStatement consequence, BlockStatement alternative)
        {
            Alternative = alternative;
            Condition = condition;
            Consequence = consequence;
            Kind = ExpressionKind.IfElse;
        }
    }

    public class InfixExpression : Expression
    {
        public Expression Left { get; set; }
        public Token Operator { get; set; }
        public Expression Right { get; set; }

        public InfixExpression(Expression left, Token op, Expression right)
        {
            Kind = ExpressionKind.Infix;
            Left = left;
            Operator = op;
            Right = right;
        }
    }

    public class IntegerExpression : Expression
    {
        public int Value { get; set; }

        public IntegerExpression(int value)
        {
            Kind = ExpressionKind.Integer;
            Value = value;
        }
    }

    public class PrefixExpression : Expression
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }

        public PrefixExpression(Expression left, Expression right)
        {
            Kind = ExpressionKind.Prefix;
            Left = left;
            Right = right;
        }
    }

    public class StringExpression : Expression
    {
        public string Value { get; set; }

        public StringExpression(string value)
        {
            Kind = ExpressionKind.String;
            Value = value;
        }
    }

    public class Node
    {
        public NodeKind Kind { get; set; }
        public int Position { get; set; }
        public int Range { get; set; }

        public Node(NodeKind kind, int position, int range)
        {
            Kind = kind;
            Position = position;
            Range = range;
        }
    }

    public class Program : Node
    {
        public List<AssertionError> Errors { get; set; }
        public List<Statement> Statements { get; set; }
        public List<Token> Tokens { get; set; }

        public Program(ProgramOptions opts) : base(opts.Kind, opts.Position, opts.Range)
        {
            Errors = opts.Errors;
            Statements = opts.Statements;
            Tokens = opts.Tokens;
        }
    }

    public class Statement : Node
    {
        public Expression Expression { get; set; }
        public Token Identifier { get; set; }

        public Statement(StatementOptions opts) : base(opts.Kind, opts.Position, opts.Range)
        {
            Expression = opts.Expression;
            Identifier = opts.Identifier;
        }
    }

    public class BlockStatement
    {
        public List<Statement> Statements { get; set; }
    }
}
