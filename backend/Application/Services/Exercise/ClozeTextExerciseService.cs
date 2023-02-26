using Application.DTOs.ExercisesDTOs.ClozeText;
using Application.Services.Interfaces;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services.Exercise;

public sealed class ClozeTextExerciseService: IClozeTextExerciseService
{
    private readonly IApplicationRepository repository;

    public ClozeTextExerciseService(IApplicationRepository repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<ClozeTextExerciseDetailItem> GetByIdAsync(Guid exerciseId,
        Guid userId,
        bool withAnswers,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(ClozeTextExerciseDetailItem updateItem, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> CreateAsync(ClozeTextExerciseCreateItem createItem, Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}