using Common.Models.Grading;

namespace Common.RepositoryInterfaces.Generic;

public interface ISubmissionRepository
{
    public Task<Submission?> TryGetByIdAsync(Guid submissionId, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Submission submission, CancellationToken cancellationToken = default);
    public Task CreateAsync(Submission submission, CancellationToken cancellationToken = default);
    public Task DeleteAsync(Submission submission, CancellationToken cancellationToken = default);
    public Task<List<Submission>> GetAllBySubmissionIdAsync(Guid userId, CancellationToken cancellationToken = default);
}