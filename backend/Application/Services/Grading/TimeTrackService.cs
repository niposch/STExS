namespace Application.Services.Grading;

public class TimeTrackService: ITimeTrackService
{
    public async Task<Guid> CreateTimeTrackAsync()
    {
        throw new NotImplementedException();
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