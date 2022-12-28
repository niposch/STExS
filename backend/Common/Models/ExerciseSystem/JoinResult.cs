namespace Common.Models.ExerciseSystem;

public enum JoinResult
{
    JoinedSucessfully = 1,
    AlreadyJoined = 2,
    VerificationPending = 3,
    ModuleIsFull = 4,
    ModuleIsArchived = 5,
    ModuleDoesNotExist = 6,
    UserIsOwner = 7,
}