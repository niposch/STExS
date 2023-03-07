using Common.Models.Grading;

namespace Common.RepositoryInterfaces.Tables;

public interface ITimeTrackRepository
{
    public Task<TimeTrack?> TryGetByTimeTrackIdAsync(Guid timeTrackId, CancellationToken cancellationToken = default);
    public Task<List<TimeTrack>> GetAllBySubmissionIdAsync(Guid submissionId, CancellationToken cancellationToken = default);
    public Task UpdateAsync(TimeTrack timeTrack, CancellationToken cancellationToken = default);
    public Task CreateAsync(TimeTrack timeTrack, CancellationToken cancellationToken = default);
    public Task DeleteAsync(TimeTrack timeTrack, CancellationToken cancellationToken = default);
    
    Task<List<TimeTrack>> GetAllByUserAndExerciseIdAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);
}