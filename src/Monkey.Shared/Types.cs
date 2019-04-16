using Newtonsoft.Json;

namespace Monkey.Shared
{
    public struct AssertionError
    {
        [JsonProperty]
        string Message { get; set; }

        public AssertionError(string message)
        {
            Message = message;
        }
    }
    
    public enum AssertionErrorKind
    {
        InvalidArgument,
        InvalidIdentifier,
        InvalidIndex,
        InvalidToken,
        InvalidType,
        UnexpectedToken,
        UnknownOperator
    }
}
