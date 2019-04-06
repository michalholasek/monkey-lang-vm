namespace Monkey.Shared.Parser
{
    internal enum Precedence {
        Lowest = 1,
        Equals,
        LessGreater,
        Sum,
        Product,
        Prefix,
        Call,
        Index
    }
}
