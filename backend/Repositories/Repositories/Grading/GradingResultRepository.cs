using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;

namespace Repositories.Repositories.Grading;

// TODO AUFGABE LEI Methoden implementieren + Unit Tests in Repositories.Tests/Repositories/GradingResultRepositoryTests/*
public class GradingResultRepository:IGradingResultRepository
{
    public async Task<GradingResult?> TryGetByIdAsync(Guid gradingResultId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
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