using System.Runtime.Serialization;

namespace Common.Exceptions;

[Serializable]
public class UnsupportedActionException : Exception
{
    public UnsupportedActionException()
        : base($"The attempted action is not possible in this context.")
    {
    }
    
    public UnsupportedActionException(string message)
        : base(message)
    {
    }
    
    protected UnsupportedActionException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}