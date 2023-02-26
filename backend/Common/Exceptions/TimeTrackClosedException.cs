using System.Runtime.Serialization;

namespace Common.Exceptions;


[Serializable]
public class TimeTrackClosedException: Exception
{
    public TimeTrackClosedException()
    {
    }
    
    protected TimeTrackClosedException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
