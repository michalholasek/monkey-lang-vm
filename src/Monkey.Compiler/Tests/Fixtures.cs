using System.Collections.Generic;

using Monkey.Shared;

namespace Monkey.Tests.Fixtures
{
    public static class Compiler
    {
        public static Dictionary<string, List<byte>> Compile = new Dictionary<string, List<byte>>
        {
            {
                "1 + 2",
                new List<byte>
                {
                    1, 1, 0,
                    1, 2, 0,
                    2
                }
            }
        };
    }
}
