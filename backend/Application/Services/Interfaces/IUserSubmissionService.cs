using Common.Models.Grading;

namespace Application.Services.Interfaces;

public interface IUserSubmissionService
{
    Task<UserSubmission> GetOrCreateUserSubmissionAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);
    Task<bool> HasUserSolvedExerciseAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);
}