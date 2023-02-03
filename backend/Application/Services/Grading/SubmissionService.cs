using Application.Services.Interfaces;
using Common.Models.Grading;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services.Grading;

public class SubmissionService:ISubmissionService
{
    private readonly IApplicationRepository repository;

    public SubmissionService(IApplicationRepository repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<List<Submission>> GetSubmissionsAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Submission> GetLastSubmission(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Submission> GetSubmissionDetailsAsync(Guid submissionId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task SubmitAsync(Guid userId,
        Guid exerciseId,
        Submission submission,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}