using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories.Grading;

// TODO AUFGABE LEI Methoden implementieren + Unit Tests in Repositories.Tests/Repositories/SubmissionRepositoryTests/*
public class SubmissionRepository : ISubmissionRepository
{
    private readonly ApplicationDbContext context;

    public SubmissionRepository(ApplicationDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<BaseSubmission?> TryGetByIdAsync(Guid submissionId, CancellationToken cancellationToken = default)
    {
        var ex = await this.context.Submissions.FirstOrDefaultAsync(e => e.UserSubmissionId == submissionId,
            cancellationToken);
        return ex;
    }

    public async Task UpdateAsync(BaseSubmission submission, CancellationToken cancellationToken = default)
    {
        this.context.Submissions.Update(submission);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateAsync(BaseSubmission submission, CancellationToken cancellationToken = default)
    {
        await this.context.Submissions.AddAsync(submission, cancellationToken);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(BaseSubmission submission, CancellationToken cancellationToken = default)
    {
        this.context.Submissions.Remove(submission);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<BaseSubmission>> GetAllBySubmissionIdAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        var ex = await this.context.Submissions.Where(e => e.UserSubmissionId == userId).ToListAsync(cancellationToken);
        return ex;
    }
}