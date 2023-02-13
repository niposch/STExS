using System.Runtime.Serialization;

namespace Common.Exceptions;

[Serializable]
public class AlreadySubmittedException: Exception
{
    public AlreadySubmittedException()
    {
    }
    
    protected AlreadySubmittedException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}