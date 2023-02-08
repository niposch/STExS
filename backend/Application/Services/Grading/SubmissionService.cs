using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services.Grading;

public class SubmissionService:ISubmissionService
{
    private readonly IApplicationRepository repository;
    
    private readonly ITimeTrackService timeTrackService;

    public SubmissionService(IApplicationRepository repository, ITimeTrackService timeTrackService)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.timeTrackService = timeTrackService ?? throw new ArgumentNullException(nameof(timeTrackService));
    }

    public async Task<List<BaseSubmission>> GetSubmissionsAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseSubmission> GetLastSubmissionForAnsweringAsync(Guid userId, Guid exerciseId, Guid timeTrackId, CancellationToken cancellationToken = default)
    {
        var userSubmission = await this.repository.UserSubmissions.TryGetByIdAsync(userId, exerciseId, cancellationToken);
        if (!await this.timeTrackService.IsOpenAsync(timeTrackId, cancellationToken))
        {
            throw new TimeTrackClosedException();
        }
        if(userSubmission == null)
        {
            throw new EntityNotFoundException<UserSubmission>(null);
        }
        if (userSubmission.FinalSubmissionId != null)
        {
            throw new AlreadySubmittedException();
        }
        
        return userSubmission.Submissions.OrderByDescending(s => s.CreationTime).Last();
    }

    public async Task<BaseSubmission> GetSubmissionDetailsAsync(Guid submissionId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task SubmitAsync(Guid userId,
        Guid exerciseId,
        BaseSubmission submission,
        Guid timeTrackId,
        CancellationToken cancellationToken = default)
    {
        submission.Id = Guid.NewGuid();
        submission.UserId = userId;
        submission.ExerciseId = exerciseId;
        
        var userSubmission = await this.repository.UserSubmissions.TryGetByIdAsync(userId, exerciseId, cancellationToken);
        if (!await this.timeTrackService.IsOpenAsync(timeTrackId, cancellationToken))
        {
            throw new TimeTrackClosedException();
        }
        if(userSubmission == null)
        {
            throw new EntityNotFoundException<UserSubmission>(null);
        }
        if (userSubmission.FinalSubmissionId != null)
        {
            throw new AlreadySubmittedException();
        }
        
        await this.repository.Submissions.CreateAsync(submission, cancellationToken);
    }
}
