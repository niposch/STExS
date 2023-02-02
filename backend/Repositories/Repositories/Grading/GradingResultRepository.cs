using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories.Grading;

// TODO AUFGABE LEI Methoden implementieren + Unit Tests in Repositories.Tests/Repositories/GradingResultRepositoryTests/*
public class GradingResultRepository:IGradingResultRepository
{
    private readonly ApplicationDbContext context;

    public GradingResultRepository(ApplicationDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GradingResult?> TryGetByIdAsync(Guid gradingResultId, CancellationToken cancellationToken = default)
    {
        var ex = await this.context.GradingResults.FirstOrDefaultAsync(e => e.Id == gradingResultId, cancellationToken);

        return ex;
    }

    public async Task UpdateAsync(GradingResult gradingResult, CancellationToken cancellationToken = default)
    {
   
        throw new NotImplementedException();
    }

    public async Task CreateAsync(GradingResult gradingResult, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(GradingResult gradingResult, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<GradingResult>> GetBySubmissionIdAsync(Guid submissionId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<GradingResult>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}