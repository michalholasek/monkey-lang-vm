using System;
using System.Collections.Generic;

using Monkey.Shared;
using Object = Monkey.Shared.Object;

namespace Monkey
{
    public partial class Compiler
    {
        internal class CompilerState
        {
            public List<Object> Constants { get; set; }
            public List<byte> Instructions { get; set; }
        }
    }
}
