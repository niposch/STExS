using System.Runtime.Serialization;

namespace Common.Exceptions;

[Serializable]
public class UserModuleRegistrationException : Exception
{
    public UserModuleRegistrationException(string userName, string moduleName, bool isConfirmed)
        : base($"User {userName} is already registered for module {moduleName} with confirmation status {isConfirmed}.")
    {
    }
    
    protected UserModuleRegistrationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}

[Serializable]
public class UserModuleUnregistrationException : Exception
{
    public UserModuleUnregistrationException(string userName, string moduleName)
        : base($"User {userName} is not registered for module {moduleName}.")
    {
    }
    
    protected UserModuleUnregistrationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}