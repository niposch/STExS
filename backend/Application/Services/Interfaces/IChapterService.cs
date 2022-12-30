using Common.Models.ExerciseSystem;

namespace Application.Services.Interfaces;

public interface IChapterService
{
    // Returns Chapter Id of the chapter that was created
    public Task<Guid> CreateChapterAsync(Guid moduleId,
        string name,
        string description,
        Guid createUserId,
        CancellationToken cancellationToken = default);

    public Task UpdateChapterAsync(Guid chapterId,
        string newName,
        string newDescription,
        Guid userId,
        CancellationToken cancellationToken = default);

    public Task DeleteChapterAsync(Guid chapterId, Guid userId, CancellationToken cancellationToken = default);
    
    public Task<Chapter> GetChapterAsync(Guid chapterId, Guid userId, CancellationToken cancellationToken = default);
    public Task<List<Chapter>> GetAllChaptersAsync(Guid userId, CancellationToken cancellationToken = default);
    public Task<List<Chapter>> GetChaptersByModuleIdAsync(Guid moduleId,
        Guid userId,
        CancellationToken cancellationToken = default);
    
    public Task ReorderModuleChaptersAsync(List<Guid> chapterIdsInNewOrder,
        Guid moduleId,
        Guid userId,
        CancellationToken cancellationToken = default);
}