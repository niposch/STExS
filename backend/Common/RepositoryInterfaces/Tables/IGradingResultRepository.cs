using Common.Models.Grading;

namespace Common.RepositoryInterfaces.Generic;

public interface IGradingResultRepository
{
    public Task<GradingResult?> TryGetByIdAsync(Guid gradingResultId, CancellationToken cancellationToken = default);
    public Task UpdateAsync(GradingResult gradingResult, CancellationToken cancellationToken = default);
    public Task CreateAsync(GradingResult gradingResult, CancellationToken cancellationToken = default);
    public Task DeleteAsync(GradingResult gradingResult, CancellationToken cancellationToken = default);
    public Task<List<GradingResult>> GetBySubmissionIdAsync(Guid submissionId, CancellationToken cancellationToken = default);
    public Task<List<GradingResult>> GetAllAsync(CancellationToken cancellationToken = default);
}