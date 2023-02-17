using Common.Models.Grading;

namespace Common.RepositoryInterfaces.Tables;

public interface IUSerSubmissionRepository
{
    public Task<UserSubmission?> TryGetByIdAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);
    public Task UpdateAsync(UserSubmission userSubmission, CancellationToken cancellationToken = default);
    public Task CreateAsync(UserSubmission userSubmission, CancellationToken cancellationToken = default);
    public Task DeleteAsync(UserSubmission userSubmission, CancellationToken cancellationToken = default);
    public Task<List<UserSubmission>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    public Task<Dictionary<Guid, UserSubmission>> GetAllByExerciseIdGroupedByUserIdAsync(Guid exerciseId, CancellationToken cancellationToken = default);
}