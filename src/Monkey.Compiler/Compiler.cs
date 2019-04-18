using System;
using System.Collections.Generic;

using Monkey.Shared;
using Object = Monkey.Shared.Object;

namespace Monkey
{
    public class Compiler
    {
        private CompilerState internalState;

        public Compiler()
        {
            internalState = new CompilerState
            {
                Constants = new List<Object>(),
                Instructions = new List<byte>()
            };
        }

        public List<byte> Compile(Node node)
        {
            return new List<byte>();
        }
    }
}
