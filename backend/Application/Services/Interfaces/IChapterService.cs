using Common.Models.ExerciseSystem;

namespace Application.Services.Interfaces;

public interface IChapterService
{
    public Task CreateChapterAsync(Guid moduleId,
        string name,
        string description,
        Guid createUserId,
        CancellationToken cancellationToken = default);

    public Task UpdateChapterAsync(Guid chapterId,
        string newName,
        string newDescription,
        CancellationToken cancellationToken = default);
    
    public Task ReorderChapterExercises(List<Guid> exerciseIdsInNewOrder, Guid chapterId, CancellationToken cancellationToken = default);
    
    public Task DeleteChapterAsync(Guid chapterId, CancellationToken cancellationToken = default);
    
    public Task<Chapter> GetChapterAsync(Guid chapterId, CancellationToken cancellationToken = default);
    public Task<List<Chapter>> GetAllChaptersAsync(CancellationToken cancellationToken = default);
    public Task<List<Chapter>> GetChaptersByModuleIdAsync(Guid moduleId, CancellationToken cancellationToken = default);
    
    public Task ReorderModuleChaptersAsync(List<Guid> chapterIdsInNewOrder, Guid moduleId, CancellationToken cancellationToken = default);
}