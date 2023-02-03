using Application.Services.Interfaces;
using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services.Grading;

public class TimeTrackService: ITimeTrackService
{
    private readonly IApplicationRepository repository;

    public TimeTrackService(IApplicationRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Guid> CreateTimeTrackAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var userSubmission = await GetOrCreateUserSubmissionAsync(userId, exerciseId, cancellationToken);

        var timeTrack = new TimeTrack
        {
            Id = Guid.NewGuid(),
            Start = DateTime.Now,
            UserId = userId,
            LastUpdate = null,
            CloseDateTime = null,
            ExerciseId = userSubmission.ExerciseId,
        };
        
        await repository.TimeTracks.CreateAsync(timeTrack, cancellationToken);
        
        return timeTrack.Id;
    }
    
    private async Task<UserSubmission> GetOrCreateUserSubmissionAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var existingSubmission = await repository.UserSubmissions.TryGetByIdAsync(userId, exerciseId, cancellationToken);
        if (existingSubmission != null)
        {
            return existingSubmission;
        }
        
        var userSubmission = new UserSubmission
        {
            UserId = userId,
            ExerciseId = exerciseId,
            FinalSubmissionId = null,
        };
        
        await repository.UserSubmissions.CreateAsync(userSubmission, cancellationToken);

        return userSubmission;
    }

    public async Task ReportActivityAsync(Guid timeTrackId)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> CloseTimeTrackAsync(Guid timeTrackId)
    {
        throw new NotImplementedException();
    }

    public async Task<TimeSpan> GetTimeSpentAsync(Guid timeTrackId)
    {
        throw new NotImplementedException();
    }

    public async Task<TimeSpan> GetTimeSpentAsync(Guid userId, Guid exerciseId)
    {
        throw new NotImplementedException();
    }
}