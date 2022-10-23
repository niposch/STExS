namespace Common.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(Guid id, Type type)
        : base($"Entity \"{type.Name}\" Id:({id}) was not found.")
    {
    }
}