using Common.Models.Grading;

namespace Application.Services.Interfaces;

public interface ISubmissionService
{
    public Task<List<Submission>> GetSubmissionsAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);
    public Task<Submission> GetLastSubmission(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);
    public Task<Submission> GetSubmissionDetailsAsync(Guid submissionId, CancellationToken cancellationToken = default);
    public Task SubmitAsync(Guid userId, Guid exerciseId, Submission submission, CancellationToken cancellationToken = default);
}