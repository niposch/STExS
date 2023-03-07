namespace Application.Services.Interfaces;

public interface IClozeTextSubmissionService
{
    public Task SubmitAsync(Guid userId,
        Guid exerciseId,
        bool isFinal,
        List<string> submittedAnswers,
        Guid timeTrackId,
        CancellationToken cancellationToken = default);
}
