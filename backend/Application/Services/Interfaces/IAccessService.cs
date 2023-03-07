using Common.Models.ExerciseSystem;

namespace Application.Services.Interfaces;

public interface IAccessService
{
    Task<bool> CanViewModule(Guid moduleId, Guid userId, CancellationToken cancellationToken = default);
    Task<bool> IsModuleAdmin(Guid moduleId, Guid userId, CancellationToken cancellationToken = default);
    Task<bool> IsChapterAdmin(Guid chapterId, Guid userId, CancellationToken cancellationToken = default);
    Task<bool> IsSystemAdmin(Guid userId);
    Task<IEnumerable<Chapter>> FilterChapterVisibility(IEnumerable<Chapter> chapters, Guid userId, CancellationToken cancellationToken = default);
    Task<bool> IsExerciseAdminAsync(Guid exerciseId, Guid getUserId, CancellationToken cancellationToken = default);
}