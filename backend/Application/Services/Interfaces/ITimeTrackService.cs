namespace Application.Services.Interfaces;

public interface ITimeTrackService
{
    public Task<Guid> CreateTimeTrackAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);
    public Task ReportActivityAsync(Guid timeTrackId);
    public Task<Guid> CloseTimeTrackAsync(Guid timeTrackId);
    public Task<TimeSpan> GetTimeSpentAsync(Guid timeTrackId);
    public Task<TimeSpan> GetTimeSpentAsync(Guid userId, Guid exerciseId);
}