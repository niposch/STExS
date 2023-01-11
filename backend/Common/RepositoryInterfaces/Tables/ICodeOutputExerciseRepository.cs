using Common.Models.ExerciseSystem.CodeOutput;

namespace Common.RepositoryInterfaces.Tables;

public interface ICodeOutputExerciseRepository
{
    Task<CodeOutputExercise?> TryGetById(Guid id, CancellationToken cancellationToken = default);
    
    Task<CodeOutputExercise> UpdateAsync(CodeOutputExercise exercise, CancellationToken cancellationToken);
}