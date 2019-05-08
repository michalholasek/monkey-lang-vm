using System.Collections.Generic;
using System.Linq;

namespace Monkey
{
    public static class Opcode
    {
        public struct Definition
        {
            public string Name { get; set; }
            public List<int> OperandLengths { get; set; }
        }
        
        private static Dictionary<byte, Definition> Opcodes = new Dictionary<byte, Definition>
        {
            { 0, new Definition { Name = "Invalid", OperandLengths = new List<int> { 0 } }},
            { 3, new Definition { Name = "Pop", OperandLengths = new List<int> { 0 } }},
            { 1, new Definition { Name = "Constant", OperandLengths = new List<int> { 2 } }},
            { 2, new Definition { Name = "Add", OperandLengths = new List<int> { 0 } }},
            { 4, new Definition { Name = "Subtract", OperandLengths = new List<int> { 0 } }},
            { 5, new Definition { Name = "Multiply", OperandLengths = new List<int> { 0 } }},
            { 6, new Definition { Name = "Divide", OperandLengths = new List<int> { 0 } }},
            { 7, new Definition { Name = "True", OperandLengths = new List<int> { 0 } }},
            { 8, new Definition { Name = "False", OperandLengths = new List<int> { 0 } }},
            { 9, new Definition { Name = "Equal", OperandLengths = new List<int> { 0 } }},
            { 10, new Definition { Name = "NotEqual", OperandLengths = new List<int> { 0 } }},
            { 11, new Definition { Name = "GreaterThan", OperandLengths = new List<int> { 0 } }},
            { 12, new Definition { Name = "Minus", OperandLengths = new List<int> { 0 } }},
            { 13, new Definition { Name = "Bang", OperandLengths = new List<int> { 0 } }}
        };

        public static Definition Find(byte code)
        {
            var opcode = Opcodes.Where(item => item.Key == code).FirstOrDefault();
            if (!opcode.Equals(default(KeyValuePair<byte, Definition>)))
            {
                return opcode.Value;
            }

            return Opcodes[0];
        }
    }
}
