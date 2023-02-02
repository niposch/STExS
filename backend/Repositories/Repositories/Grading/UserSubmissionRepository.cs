using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;

namespace Repositories.Repositories.Grading;

// TODO AUFGABE LEI Methoden implementieren + Unit Tests in Repositories.Tests/Repositories/UserSubmissionRepositoryTests/*
public class UserSubmissionRepository:IUSerSubmissionRepository
{
    public async Task<UserSubmission?> TryGetByIdAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(UserSubmission userSubmission, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(UserSubmission userSubmission, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(UserSubmission userSubmission, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<UserSubmission>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IGrouping<Guid, UserSubmission>> GetAllByExerciseIdGroupedByUserIdAsync(Guid exerciseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}