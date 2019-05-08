using System.Collections.Generic;

using Monkey.Shared;

namespace Monkey.Tests.Fixtures
{
    public static partial class Expressions
    {
        internal static Dictionary<string, Program> Errors = new Dictionary<string, Program>
        {
            {
                "...",
                new Program(new ProgramOptions
                {
                    Errors = new List<AssertionError>
                    {
                        new AssertionError { Message = "unknown operator: got Illegal (.), expected Bang (!) or Minus (-)" }
                    },
                    Kind = NodeKind.Program,
                    Position = 0,
                    Range = 3,
                    Statements = new List<Statement> {},
                    Tokens = new List<Token>
                    {
                        new Token() { Column = 2, Kind = SyntaxKind.Illegal, Line = 1, Literal = "." },
                        new Token() { Column = 3, Kind = SyntaxKind.Illegal, Line = 1, Literal = "." },
                        new Token() { Column = 4, Kind = SyntaxKind.Illegal, Line = 1, Literal = "." },
                        new Token() { Column = 5, Kind = SyntaxKind.EOF, Line = 1, Literal = "" }
                    }
                })
            }
        };
    }
}
