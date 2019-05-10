using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Monkey.Shared
{
    public interface IEnvironment
    {
        Object Get(string identifier);
        void Set(string identifier, Object value);
    }

    public class Environment : IEnvironment
    {
        private Dictionary<string, Object> symbols;

        public Environment()
        {
            symbols = new Dictionary<string, Object>();
        }

        public Object Get(string identifier)
        {
            return symbols.Where(item => item.Key == identifier).FirstOrDefault().Value;
        }

        public void Set(string identifier, Object value)
        {
            symbols.Add(identifier, value);
        }
    }

    public class EnclosedEnvironment : IEnvironment
    {
        private IEnvironment outer;
        private IEnvironment inner;

        public EnclosedEnvironment(IEnvironment outer)
        {
            this.outer = outer;
            this.inner = new Environment();
        }

        public Object Get(string identifier)
        {
            var value = inner.Get(identifier);
            return value != null ? value : outer.Get(identifier);
        }

        public void Set(string identifier, Object value)
        {
            inner.Set(identifier, value);
        }
    }

    public enum ObjectKind
    {
        Array,
        Boolean,
        BuiltIn,
        Error,
        Function,
        Integer,
        Let,
        Null,
        Return,
        String,
        Hash,
        Puts
    }
    
    public class Object
    {
        public IEnvironment Environment { get; set; }
        public ObjectKind Kind { get; set; }
        public object Value { get; set; }
    }
}
