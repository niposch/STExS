using Common.Models.Grading;

namespace Application.Services.Interfaces;

public interface ISubmissionService
{
    public Task<List<BaseSubmission>> GetSubmissionsAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);
    public Task<BaseSubmission> GetLastSubmission(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);
    public Task<BaseSubmission> GetSubmissionDetailsAsync(Guid submissionId, CancellationToken cancellationToken = default);
    public Task SubmitAsync(Guid userId, Guid exerciseId, BaseSubmission submission, CancellationToken cancellationToken = default);
}