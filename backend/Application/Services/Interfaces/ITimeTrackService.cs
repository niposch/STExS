using Application.Services.Grading;

namespace Application.Services.Interfaces;

public interface ITimeTrackService
{
    public Task<Guid> CreateTimeTrackAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);
    public Task ReportActivityAsync(Guid timeTrackId, CancellationToken cancellationToken = default);
    public Task CloseTimeTrackAsync(Guid timeTrackId, CancellationToken cancellationToken = default);
    public Task<TimeTrackDetailItem> GetTimeTrackAsync(Guid userId, Guid timeTrackId, CancellationToken cancellationToken = default);
    Task<bool> IsOpenAsync(Guid timeTrackId, CancellationToken cancellationToken = default);
    Task<List<TimeTrackEvent>> GetTimeTracksForExerciseAndUserAsync(Guid exerciseId, Guid userId, CancellationToken cancellationToken = default);
}