using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;

namespace Repositories.Repositories.Grading;

// TODO AUFGABE LEI Methoden implementieren + Unit Tests in Repositories.Tests/Repositories/TimeTrackRepositoryTests/*
public class TimeTrackRepository:ITimeTrackRepository
{
    public async Task<TimeTrack?> TryGetByTimeTrackIdAsync(Guid timeTrackId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TimeTrack>> GetAllBySubmissionIdAsync(Guid submissionId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(TimeTrack timeTrack, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(TimeTrack timeTrack, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(TimeTrack timeTrack, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}