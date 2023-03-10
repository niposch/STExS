using Application.Services.Grading;
using Common.Models.Grading;

namespace Application.Services.Interfaces;

public interface ISubmissionService
{
    public Task<List<BaseSubmission>> GetSubmissionsAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);
    public Task<BaseSubmission?> GetLastSubmissionForAnsweringAsync(Guid userId,
        Guid exerciseId,
        Guid? timeTrackId,
        CancellationToken cancellationToken = default);
    public Task<BaseSubmission> GetSubmissionDetailsAsync(Guid submissionId, CancellationToken cancellationToken = default);
    public Task SubmitAsync(Guid userId,
        Guid exerciseId,
        BaseSubmission submission,
        bool isFinal,
        Guid timeTrackId,
        CancellationToken cancellationToken = default);

    Task<List<SubmissionDetailItem>> GetSubmissionsForUserAndExerciseAsync(Guid exerciseId, Guid userId, CancellationToken cancellationToken = default);
    
    Task<BaseSubmission> GetBySubmissionIdAsync(Guid submissionId, CancellationToken cancellationToken = default);
}