using Common.Models.Grading;

namespace Common.RepositoryInterfaces.Tables;

public interface ISubmissionRepository
{
    public Task<BaseSubmission?> TryGetByIdAsync(Guid submissionId, CancellationToken cancellationToken = default);
    public Task UpdateAsync(BaseSubmission submission, CancellationToken cancellationToken = default);
    public Task CreateAsync(BaseSubmission submission, CancellationToken cancellationToken = default);
    public Task DeleteAsync(BaseSubmission submission, CancellationToken cancellationToken = default);
    public Task<List<BaseSubmission>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}