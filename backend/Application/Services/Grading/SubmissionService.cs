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

    public async Task<List<BaseSubmission>> GetSubmissionsAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseSubmission> GetLastSubmission(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseSubmission> GetSubmissionDetailsAsync(Guid submissionId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task SubmitAsync(Guid userId,
        Guid exerciseId,
        BaseSubmission submission,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
