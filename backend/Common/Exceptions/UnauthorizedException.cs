using System.Runtime.Serialization;

namespace Common.Exceptions;

[Serializable]
public class UnauthorizedException : Exception
{
    public UnauthorizedException()
        : base($"You are not authorized to perform this action.")
    {
    }
    
    protected UnauthorizedException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}