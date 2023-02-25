using Application.DTOs.ExercisesDTOs;
using Application.DTOs.ExercisesDTOs.CodeOutput;
using Application.DTOs.ExercisesDTOs.Parson;

namespace Application.Services.Interfaces;

public interface IParsonExerciseService
{
    public Task<ParsonDetailItem> GetByIdAsync(Guid exerciseId, Guid userId, CancellationToken cancellationToken = default);
    
    public Task<ParsonExerciseDetailItemWithAnswer> GetByIdWithAnswerAsync(Guid exerciseId, Guid userId, CancellationToken cancellationToken = default);
    
    public Task UpdateAsync(ParsonExerciseDetailItemWithAnswer item, CancellationToken cancellationToken = default);
    
    public Task<Guid> CreateAsync(ParsonExerciseCreateItem createItem, Guid userId, CancellationToken cancellationToken = default);
}

public class ParsonExerciseDetailItemWithAnswer : ExerciseDetailItem
{
    public List<ParsonExerciseLineDetailItem> ExpectedAnswer { get; set; }
}