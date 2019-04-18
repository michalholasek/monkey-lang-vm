namespace Monkey.Shared
{
    public enum Precedence {
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
