using Application.DTOs.ExercisesDTOs.CodeOutput;

namespace Application.Services.Interfaces;

public interface ICodeOutputExerciseService
{
    public Task<CodeOutputDetailItem> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);

    public Task<CodeOutputExerciseDetailItemWithAnswer> GetByIdWithAnswerAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);

    public Task<CodeOutputExerciseDetailItemWithAnswer> UpdateAsync(CodeOutputExerciseDetailItemWithAnswer item, CancellationToken cancellationToken = default);

    public Task<CodeOutputExerciseDetailItemWithAnswer> CreateAsync(CodeOutputExerciseCreateItem createItem, Guid userId, CancellationToken cancellationToken = default);
}