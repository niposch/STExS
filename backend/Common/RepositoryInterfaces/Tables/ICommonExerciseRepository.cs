using Common.Models.ExerciseSystem;

namespace Common.RepositoryInterfaces.Tables;

public interface ICommonExerciseRepository
{
    public Task<BaseExercise?> TryGetByIdAsync(Guid exerciseId, bool asNoTracking = false, CancellationToken cancellationToken = default);
    
    public Task<BaseExercise> AddAsync(BaseExercise exercise, CancellationToken cancellationToken = default);

    public Task<List<BaseExercise>> GetAllAsync(CancellationToken cancellationToken = default);

    public Task<List<BaseExercise>> GetForChapterAsync(Guid chapterId, CancellationToken cancellationToken = default);
    
    public Task DeleteAsync(Guid exerciseId, CancellationToken cancellationToken = default);
}