using System;
using System.Collections.Generic;

using Monkey.Shared;
using Object = Monkey.Shared.Object;

namespace Monkey
{
    public partial class Compiler
    {
        public class CompilerState
        {
            public List<AssertionError> Errors { get; set; }
            public Dictionary<string, Object> Constants { get; set; }
            public Expression Expression { get; set; }
            public Node Node { get; set; }
            public List<byte> Instructions { get; set; }
        }
    }
}
