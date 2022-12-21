using System.Runtime.Serialization;

namespace Common.Exceptions;

[Serializable]
public class EntityNotFoundException<T> : Exception
    where T : class
{
    public EntityNotFoundException(Guid id)
        : base($"Entity \"{typeof(T).Name}\" Id:({id}) was not found.")
    {
    }
    
    protected EntityNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}