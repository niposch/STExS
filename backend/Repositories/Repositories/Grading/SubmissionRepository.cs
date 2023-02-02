using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;

namespace Repositories.Repositories.Grading;

// TODO AUFGABE LEI Methoden implementieren + Unit Tests in Repositories.Tests/Repositories/SubmissionRepositoryTests/*
public class SubmissionRepository:ISubmissionRepository
{
    public async Task<Submission?> TryGetByIdAsync(Guid submissionId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Submission submission, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(Submission submission, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Submission submission, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Submission>> GetAllBySubmissionIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}