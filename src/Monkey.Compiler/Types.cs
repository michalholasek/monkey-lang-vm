using System;
using System.Collections.Generic;

using Monkey.Shared.Bytecode;
using Object = Monkey.Shared.Evaluator.Object;

namespace Monkey
{
    internal class CompilerState
    {
        public List<Object> Constants { get; set; }
        public List<byte> Instructions { get; set; }
    }
}
