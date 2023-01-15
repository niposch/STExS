using Application.DTOs.ExercisesDTOs.CodeOutput;
using Common.Models.ExerciseSystem.CodeOutput;

namespace Application.Services.Interfaces;

public interface ICodeOutputExerciseService
{
    public Task<CodeOutputDetailItem> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    public Task<CodeOutputExerciseDetailItemWithAnswer> GetByIdWithAnswerAsync(Guid id, CancellationToken cancellationToken = default);
    
    public Task<CodeOutputExerciseDetailItemWithAnswer> UpdateAsync(CodeOutputExerciseDetailItemWithAnswer item, CancellationToken cancellationToken = default);
    
    public Task<CodeOutputExerciseDetailItemWithAnswer> CreateAsync(CodeOutputExerciseCreateItem createItem, Guid userId, CancellationToken cancellationToken = default);
}