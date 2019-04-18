using System.Collections.Generic;

using Monkey.Shared.Bytecode;
using Monkey.Shared.Parser.Ast;

namespace Monkey.Tests.Fixtures
{
    public static class Compiler
    {
        public static Dictionary<string, List<byte>> Compile = new Dictionary<string, List<byte>>
        {
            { "1 + 2", new List<byte>() }
        };
    }
}
