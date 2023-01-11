using Application.DTOs.Exercises;
using Common.Models.ExerciseSystem;

namespace Application.Services.Interfaces;

public interface IExerciseService
{
    public Task<BaseExercise> CopyToChapterAsync(Guid existingExerciseId, Guid chapterToCopyTo, CancellationToken cancellationToken = default);
    
    public Task<List<ExerciseDetailItem>> GetByChapterIdAsync(Guid chapterId, CancellationToken cancellationToken = default);
    
    public Task DeleteExerciseAsync(Guid exerciseId, CancellationToken cancellationToken = default);
    
    public Task<ExerciseDetailItem> GetExerciseByIdAsync(Guid exerciseId, CancellationToken cancellationToken = default);
    
    public Task<List<ExerciseDetailItem>> GetAllAsync(CancellationToken cancellationToken = default);
}