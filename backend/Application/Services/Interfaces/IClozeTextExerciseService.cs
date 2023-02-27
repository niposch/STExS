using Application.DTOs.ExercisesDTOs.ClozeText;

namespace Application.Services.Interfaces;

public interface IClozeTextExerciseService
{
    Task<ClozeTextExerciseDetailItem> GetByIdAsync(Guid exerciseId, Guid userId, bool withAnswers, CancellationToken cancellationToken = default);
    
    Task UpdateAsync(ClozeTextExerciseDetailItem updateItem, CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(ClozeTextExerciseCreateItem createItem, Guid userId, CancellationToken cancellationToken = default);
}