using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models.ExerciseSystem.CodeOutput;
using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services.Grading;

public class CodeOutputSubmissionService: ICodeOutputSubmissionService
{
    private readonly IApplicationRepository repository;
    
    private readonly ITimeTrackService timeTrackService;
    private readonly ISubmissionService submissionService;

    public CodeOutputSubmissionService(IApplicationRepository repository, ITimeTrackService timeTrackService, ISubmissionService submissionService)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.timeTrackService = timeTrackService ?? throw new ArgumentNullException(nameof(timeTrackService));
        this.submissionService = submissionService;
    }

    public async Task SubmitAsync(Guid userId,
        Guid exerciseId,
        bool isTemp,
        string submittedAnswer,
        Guid timeTrackId,
        CancellationToken cancellationToken = default)
    {
        var codeOutputSubmission = new CodeOutputSubmission
        {
            Id = Guid.NewGuid(),
            SubmittedAnswer = submittedAnswer,
            UserId = userId,
            ExerciseId = exerciseId
        };
        
        await this.submissionService.SubmitAsync(userId, exerciseId, codeOutputSubmission, timeTrackId, cancellationToken);
    }
}