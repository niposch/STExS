using Application.Services.Interfaces;
using Common.Models.ExerciseSystem.Cloze;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services.Grading;

public sealed class ClozeTextSubmissionService : IClozeTextSubmissionService
{
    private readonly IApplicationRepository repository;
    private readonly ITimeTrackService timeTrackService;
    private readonly ISubmissionService submissionService;


    public ClozeTextSubmissionService(IApplicationRepository repository, ITimeTrackService timeTrackService, ISubmissionService submissionService)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.timeTrackService = timeTrackService ?? throw new ArgumentNullException(nameof(timeTrackService));
        this.submissionService = submissionService;
    }

    public async Task SubmitAsync(Guid userId, Guid exerciseId, bool isFinal, List<string> submittedAnswers, Guid timeTrackId, CancellationToken cancellationToken = default)
    {
        var submissionId = Guid.NewGuid();
        var clozeTextSubmission = new ClozeTextSubmission
        {
            Id = submissionId,
            SubmittedAnswers = submittedAnswers
                .Select((answer,i) => new ClozeTextAnswerItem{
                    ClozeTextSubmissionId = submissionId,
                    Index = i,
                    SubmittedAnswer = answer,
                })
                .ToList(),
            UserId = userId,
            ExerciseId = exerciseId,
        };

        await this.submissionService.SubmitAsync(userId, exerciseId, clozeTextSubmission, isFinal, timeTrackId, cancellationToken);
    }
}
