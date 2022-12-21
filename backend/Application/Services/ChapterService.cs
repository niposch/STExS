using Application.Services.Interfaces;
using Common.Models.ExerciseSystem;

namespace Application.Services;

public sealed class ChapterService: IChapterService
{
    public async Task CreateChapterAsync(Chapter chapterCreateItem, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateChapterAsync(Chapter chapterUpdateItem, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task ReorderChapterExercises(List<Guid> exerciseIdsInNewOrder, Guid chapterId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteChapterAsync(Guid chapterId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Chapter> GetChapterAsync(Guid chapterId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Chapter>> GetAllChaptersAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Chapter>> GetChaptersByModuleIdAsync(Guid moduleId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}