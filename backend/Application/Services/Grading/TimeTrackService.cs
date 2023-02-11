using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models.Authentication;
using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;
using Common.RepositoryInterfaces.Tables;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Grading;

public class TimeTrackService: ITimeTrackService
{
    private readonly IApplicationRepository repository;
    
    private readonly IAccessService accessService;
    private readonly IUserSubmissionService userSubmissionService;

    public TimeTrackService(IApplicationRepository repository, IAccessService accessService, IUserSubmissionService userSubmissionService)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
        this.userSubmissionService = userSubmissionService ?? throw new ArgumentNullException(nameof(userSubmissionService));
    }

    public async Task<Guid> CreateTimeTrackAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var userSubmission = await this.userSubmissionService.GetOrCreateUserSubmissionAsync(userId, exerciseId, cancellationToken);

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
    
    public async Task ReportActivityAsync(Guid timeTrackId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task CloseTimeTrackAsync(Guid timeTrackId, CancellationToken cancellationToken = default)
    {
        var timeTrack = await repository.TimeTracks.TryGetByTimeTrackIdAsync(timeTrackId, cancellationToken);
        if (timeTrack == null)
        {
            throw new EntityNotFoundException<TimeTrack>(timeTrackId);
        }
        
        timeTrack.CloseDateTime = DateTime.Now;
        await repository.TimeTracks.UpdateAsync(timeTrack, cancellationToken);
    }

    public async Task<TimeTrackDetailItem> GetTimeTrackAsync(Guid userId, Guid timeTrackId, CancellationToken cancellationToken = default)
    {
        var timeTrack = await repository.TimeTracks.TryGetByTimeTrackIdAsync(timeTrackId, cancellationToken);
        if(!await accessService.IsModuleAdmin(userId, timeTrackId, cancellationToken))
        {
            throw new EntityNotFoundException<TimeTrack>(timeTrackId);
        }
        if (timeTrack == null)
        {
            throw new EntityNotFoundException<TimeTrack>(timeTrackId);
        }
        
        var userSubmission = await repository.UserSubmissions.TryGetByIdAsync(timeTrack.UserId, timeTrack.ExerciseId, cancellationToken);
        if (userSubmission == null)
        {
            throw new EntityNotFoundException<UserSubmission>(null);
        }
        
        return ToTimeTrackDetailItem(timeTrack, userSubmission);
    }

    public async Task<bool> IsOpenAsync(Guid timeTrackId, CancellationToken cancellationToken = default)
    {
        var timeTrack = await repository.TimeTracks.TryGetByTimeTrackIdAsync(timeTrackId, cancellationToken);
        if (timeTrack == null)
        {
            throw new EntityNotFoundException<TimeTrack>(timeTrackId);
        }

        return timeTrack.CloseDateTime == null &&
               timeTrack.Start.AddDays(2) >= DateTime.Now;
    }

    private TimeTrackState GetTimeTrackState(TimeTrack timeTrack)
    {
        if (timeTrack.CloseDateTime != null)
        {
            return TimeTrackState.ClosedSuccessfully;
        }

        if (timeTrack.LastUpdate != null && timeTrack.LastUpdate.Value.AddDays(1) < DateTime.Now)
        {
            return TimeTrackState.ClosedDueToLossOfContact;
        }

        return TimeTrackState.Open;
    }
    
    private int? GetTimeTrackTimeInSec(TimeTrack timeTrack)
    {
        if (timeTrack.CloseDateTime == null)
        {
            return null;
        }

        return (int) (timeTrack.CloseDateTime - timeTrack.Start).Value.TotalSeconds;
    }
    
    private TimeTrackDetailItem ToTimeTrackDetailItem(TimeTrack timeTrack, UserSubmission userSubmission)
    {
        return new TimeTrackDetailItem
        {
            Id = timeTrack.Id,
            Start = timeTrack.Start,
            TimeInSec = GetTimeTrackTimeInSec(timeTrack),
            LastSolutionUpdate = timeTrack.LastUpdate,
            State = GetTimeTrackState(timeTrack),
            ExerciseId = timeTrack.ExerciseId,
            UserId = timeTrack.UserId,
            User = timeTrack.User,
            FinalSubmissionId = userSubmission.FinalSubmissionId
        };
    }
}

public class TimeTrackDetailItem
{
    public Guid Id { get; set; }
    public DateTime Start { get; set; }
    public int? TimeInSec { get; set; }
    public DateTime? LastSolutionUpdate { get; set; }
    public TimeTrackState State { get; set; }
    public Guid ExerciseId { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    public Guid? FinalSubmissionId { get; set; }
}

public enum TimeTrackState
{
    Open,
    ClosedSuccessfully,
    ClosedDueToLossOfContact
}