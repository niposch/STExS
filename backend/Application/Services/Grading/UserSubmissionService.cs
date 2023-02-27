using Application.Services.Interfaces;
using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services.Grading;

public sealed class UserSubmissionService:IUserSubmissionService
{
    private readonly IApplicationRepository repository;

    public UserSubmissionService(IApplicationRepository repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<UserSubmission> GetOrCreateUserSubmissionAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var existingSubmission = await repository.UserSubmissions.TryGetByIdAsync(userId, exerciseId, cancellationToken);
        if (existingSubmission != null)
        {
            return existingSubmission;
        }
        
        var userSubmission = new UserSubmission
        {
            UserId = userId,
            ExerciseId = exerciseId,
            FinalSubmissionId = null,
        };
        
        await repository.UserSubmissions.CreateAsync(userSubmission, cancellationToken);

        return userSubmission;
    }

    public async Task<bool> HasUserSolvedExerciseAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        var userSubmission = await repository.UserSubmissions.TryGetByIdAsync(userId, exerciseId, cancellationToken);
        
        return userSubmission != null && userSubmission.FinalSubmissionId != null;
    }
}