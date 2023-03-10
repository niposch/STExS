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

    public SubmissionService(IApplicationRepository repository, ITimeTrackService timeTrackService, IUserSubmissionService userSubmissionService,
        IGradingService gradingService)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.timeTrackService = timeTrackService ?? throw new ArgumentNullException(nameof(timeTrackService));
        this.userSubmissionService = userSubmissionService ?? throw new ArgumentNullException(nameof(userSubmissionService));
        this.gradingService = gradingService ?? throw new ArgumentNullException(nameof(gradingService));
    }

    public async Task<List<BaseSubmission>> GetSubmissionsAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseSubmission?> GetLastSubmissionForAnsweringAsync(Guid userId, Guid exerciseId, Guid? timeTrackId, CancellationToken cancellationToken = default)
    {
        var userSubmission = await this.userSubmissionService.GetOrCreateUserSubmissionAsync(userId, exerciseId, cancellationToken);
        if(userSubmission == null)
        {
            throw new EntityNotFoundException<UserSubmission>(null);
        }
        if (userSubmission.FinalSubmissionId != null)
        {
            return await this.repository.Submissions.TryGetByIdAsync(userSubmission.FinalSubmissionId.Value, cancellationToken);
        }
        if(timeTrackId == null)
        {
            throw new ArgumentNullException(nameof(timeTrackId));
        }
        if (!await this.timeTrackService.IsOpenAsync(timeTrackId.Value, cancellationToken))
        {
            throw new TimeTrackClosedException();
        }
        
        return userSubmission.Submissions.OrderByDescending(s => s.CreationTime).FirstOrDefault();
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
        
        await this.gradingService.RunAutomaticGradingForExerciseAsync(submission);
    }

    public async Task<List<SubmissionDetailItem>> GetSubmissionsForUserAndExerciseAsync(Guid exerciseId, Guid userId, CancellationToken cancellationToken = default)
    {
        var result = await this.repository.Submissions.GetAllByUserIdAndExerciseId(userId, exerciseId, cancellationToken);
        var userSubmission = await this.userSubmissionService.GetOrCreateUserSubmissionAsync(userId, exerciseId, cancellationToken);

        return result.Select(s => SubmissionDetailItem.ToSubmissionDetailItem(s, userSubmission))
            .OrderByDescending(s => s.IsFinalSubmission)
            .ThenByDescending(s => s.CreationTime)
            .ToList();
    }

    public async Task<BaseSubmission> GetBySubmissionIdAsync(Guid submissionId, CancellationToken cancellationToken = default)
    {
        var res = await this.repository.Submissions.TryGetByIdAsync(submissionId, cancellationToken);
        if (res == null)
        {
            throw new EntityNotFoundException<BaseSubmission>(submissionId);
        }
        
        return res;
    }
}
