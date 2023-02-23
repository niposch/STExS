using Application.DTOs.ExercisesDTOs;
using Common.Models.ExerciseSystem;

namespace Application.Services.Interfaces;

public interface IExerciseService
{
    public Task<ExerciseDetailItem> CopyToChapterAsync(Guid existingExerciseId, Guid chapterToCopyTo, CancellationToken cancellationToken = default);
    
    public Task<List<ExerciseDetailItem>> GetByChapterIdAsync(Guid chapterId, Guid userId, CancellationToken cancellationToken = default);
    
    public Task DeleteExerciseAsync(Guid exerciseId, CancellationToken cancellationToken = default);
    
    public Task<ExerciseDetailItem> GetExerciseByIdAsync(Guid exerciseId, CancellationToken cancellationToken = default);
    
    public Task<List<ExerciseDetailItem>> GetAllAsync(CancellationToken cancellationToken = default);
    
    public Task<List<ExerciseDetailItem>> SearchAsync(string search, CancellationToken cancellationToken = default);
}