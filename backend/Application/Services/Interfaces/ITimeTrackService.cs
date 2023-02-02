namespace Application.Services.Grading;

public interface ITimeTrackService
{
    public Task<Guid> CreateTimeTrackAsync();
    public Task ReportActivityAsync(Guid timeTrackId);
    public Task<Guid> CloseTimeTrackAsync(Guid timeTrackId);
    public Task<TimeSpan> GetTimeSpentAsync(Guid timeTrackId);
    public Task<TimeSpan> GetTimeSpentAsync(Guid userId, Guid exerciseId);
}