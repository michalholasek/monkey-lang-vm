using System;
using System.Collections.Generic;
using System.Linq;

using Monkey.Shared;
using Object = Monkey.Shared.Object;

namespace Monkey
{
    public partial class Compiler
    {
        public class CompilerState
        {
            public List<BuiltIn> BuiltIns { get; set; }
            public List<Object> Constants { get; set; }
            public Scope CurrentScope { get; set; }
            public List<AssertionError> Errors { get; set; }
            public Expression Expression { get; set; }
            public Node Node { get; set; }
            public Stack<Scope> Scopes { get; set; }
        }

        public class Instruction
        {
            public byte Opcode { get; set; }
            public int Position { get; set; }
        }

        public class Scope
        {
            public Instruction CurrentInstruction { get; set; }
            public List<byte> Instructions { get; set; }
            public Instruction PreviousInstruction { get; set; }
            public SymbolTable SymbolTable { get; set; }
        }

        public class Symbol
        {
            public int Index { get; set; }
            public string Name { get; set; }
            public SymbolScope Scope { get; set; }

            public static Symbol Undefined { get; private set; }

            static Symbol()
            {
                Undefined = new Symbol { Index = -1, Name = String.Empty, Scope = SymbolScope.None };
            }
        }

        public enum SymbolScope
        {
            None,
            Global,
            Local,
            Free,
            Function
        }

        public class SymbolTable
        {
            private SymbolTable Outer { get; set; }
            private Dictionary<string, Symbol> Store { get; set; }

            public Dictionary<string, Symbol> Frees { get; set; }

            public SymbolTable()
            {
                Frees = new Dictionary<string, Symbol>();
                Store = new Dictionary<string, Symbol>();
            }

            public SymbolTable(SymbolTable outer)
            {
                Frees = new Dictionary<string, Symbol>();
                Outer = outer;
                Store = new Dictionary<string, Symbol>();
            }

            public int Count { get { return Store.Where(symbol => symbol.Value.Scope != SymbolScope.Function).Count(); } }

            public Symbol Define(string identifier)
            {
                var symbol = new Symbol
                {
                    Index = Store.Where(item => item.Value.Scope != SymbolScope.Function).Count(),
                    Name = identifier,
                    Scope = Outer == default(SymbolTable) ? SymbolScope.Global : SymbolScope.Local
                };

                Store[symbol.Name] = symbol;

                return symbol;
            }

            public Symbol Define(string identifier, SymbolScope scope)
            {
                var symbol = new Symbol
                {
                    Index = Store.Where(item => item.Value.Scope != SymbolScope.Function).Count(),
                    Name = identifier,
                    Scope = scope
                };

                Store[symbol.Name] = symbol;

                return symbol;
            }

            public Symbol Resolve(string identifier)
            {
                var symbol = Store.Where(pair => pair.Key == identifier).FirstOrDefault();

                if (!symbol.Equals(default(KeyValuePair<string, Symbol>)))
                {
                    return symbol.Value;
                }

                if (Outer != default(SymbolTable))
                {
                    var outerSymbol = Outer.Resolve(identifier);

                    if (outerSymbol == Symbol.Undefined || outerSymbol.Scope == SymbolScope.Global)
                    {
                        return outerSymbol;
                    }
                    else
                    {
                        var free = new Symbol { Name = outerSymbol.Name, Index = Frees.Count, Scope = SymbolScope.Free };

                        Store[outerSymbol.Name] = free;
                        Frees[free.Name] = outerSymbol;

                        return free;
                    }
                }

                return Symbol.Undefined;
            }
        }
    }
}
