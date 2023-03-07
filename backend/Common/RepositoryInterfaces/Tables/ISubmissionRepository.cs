using Common.Models.Grading;

namespace Common.RepositoryInterfaces.Tables;

public interface ISubmissionRepository
{
    public Task<BaseSubmission?> TryGetByIdAsync(Guid submissionId, CancellationToken cancellationToken = default);
    public Task UpdateAsync(BaseSubmission submission, CancellationToken cancellationToken = default);
    public Task CreateAsync(BaseSubmission submission, CancellationToken cancellationToken = default);
    public Task DeleteAsync(BaseSubmission submission, CancellationToken cancellationToken = default);
    public Task<List<BaseSubmission>> GetAllBySubmissionIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<List<BaseSubmission>> GetAllByUserIdAndExerciseId(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);
}