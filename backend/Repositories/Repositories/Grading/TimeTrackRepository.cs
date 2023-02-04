using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories.Grading;

// TODO AUFGABE LEI Methoden implementieren + Unit Tests in Repositories.Tests/Repositories/TimeTrackRepositoryTests/*
public class TimeTrackRepository : ITimeTrackRepository
{
    private readonly ApplicationDbContext context;

    public TimeTrackRepository(ApplicationDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<TimeTrack?> TryGetByTimeTrackIdAsync(Guid timeTrackId,
        CancellationToken cancellationToken = default)
    {
        var ex = await this.context.TimeTracks.FirstOrDefaultAsync(e => e.Id == timeTrackId, cancellationToken);
        return ex;
    }

    public async Task<List<TimeTrack>> GetAllBySubmissionIdAsync(Guid submissionId,
        CancellationToken cancellationToken = default)
    {
        var ex = await this.context.TimeTracks.Where(e => e.SubmissionId == submissionId)
            .ToListAsync(cancellationToken);
        return ex;
    }

    public async Task UpdateAsync(TimeTrack timeTrack, CancellationToken cancellationToken = default)
    {
        this.context.TimeTracks.Update(timeTrack);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateAsync(TimeTrack timeTrack, CancellationToken cancellationToken = default)
    {
        await this.context.TimeTracks.AddAsync(timeTrack, cancellationToken);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TimeTrack timeTrack, CancellationToken cancellationToken = default)
    {
        this.context.TimeTracks.Remove(timeTrack);
        await this.context.SaveChangesAsync(cancellationToken);
    }
}