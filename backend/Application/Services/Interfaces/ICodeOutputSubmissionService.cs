namespace Application.Services.Interfaces;

public interface ICodeOutputSubmissionService
{
    public Task SubmitAsync(Guid userId,
        Guid exerciseId,
        bool isFinal,
        string submittedAnswer,
        Guid timeTrackId,
        CancellationToken cancellationToken = default);
}