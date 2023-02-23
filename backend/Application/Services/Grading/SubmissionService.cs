using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services.Grading;

public class SubmissionService:ISubmissionService
{
    private readonly IApplicationRepository repository;
    
    private readonly ITimeTrackService timeTrackService;
    private readonly IUserSubmissionService userSubmissionService;
    private readonly IGradingService gradingService;

    public SubmissionService(IApplicationRepository repository, ITimeTrackService timeTrackService, IUserSubmissionService userSubmissionService)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.timeTrackService = timeTrackService ?? throw new ArgumentNullException(nameof(timeTrackService));
        this.userSubmissionService = userSubmissionService ?? throw new ArgumentNullException(nameof(userSubmissionService));
    }

    public async Task<List<BaseSubmission>> GetSubmissionsAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseSubmission?> GetLastSubmissionForAnsweringAsync(Guid userId, Guid exerciseId, Guid timeTrackId, CancellationToken cancellationToken = default)
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
            return await this.repository.Submissions.TryGetByIdAsync(userSubmission.FinalSubmissionId.Value, cancellationToken);
        }
        
        return userSubmission.Submissions.OrderByDescending(s => s.CreationTime).First();
    }

    public async Task<BaseSubmission> GetSubmissionDetailsAsync(Guid submissionId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task SubmitAsync(Guid userId,
        Guid exerciseId,
        BaseSubmission submission,
        bool isFinal,
        Guid timeTrackId,
        CancellationToken cancellationToken = default)
    {
        submission.Id = Guid.NewGuid();
        submission.UserId = userId;
        submission.ExerciseId = exerciseId;
        
        var userSubmission = await this.userSubmissionService.GetOrCreateUserSubmissionAsync(userId, exerciseId, cancellationToken);
        if (!await this.timeTrackService.IsOpenAsync(timeTrackId, cancellationToken))
        {
            throw new TimeTrackClosedException();
        }
        if (userSubmission.FinalSubmissionId != null)
        {
            throw new AlreadySubmittedException();
        }
        
        await this.repository.Submissions.CreateAsync(submission, cancellationToken);
        if (isFinal)
        {
            userSubmission.FinalSubmissionId = submission.Id;
            await this.repository.UserSubmissions.UpdateAsync(userSubmission, cancellationToken);
        }
        
    }
}
