using Application.Services.Interfaces;
using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Generic;

namespace Application.Services;

public sealed class ChapterService: IChapterService
{
    private readonly IApplicationRepository repository;

    public ChapterService(IApplicationRepository repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task CreateChapterAsync(Guid moduleId, string name, string description, Guid createUserId, CancellationToken cancellationToken = default)
    {
        var modulesInChapter = await this.GetChaptersByModuleIdAsync(moduleId, cancellationToken);
        var nextRunningNumber = modulesInChapter.Select(c => c.RunningNumber).DefaultIfEmpty(0).Max() + 1;
        var chapter = new Chapter
        {
            ModuleId = moduleId,
            OwnerId = createUserId,
            RunningNumber = nextRunningNumber
        };

        await this.repository.Chapters.AddAsync(chapter, cancellationToken);
    }

    public async Task UpdateChapterAsync(Guid chapterId, string newName, string newDescription, CancellationToken cancellationToken = default)
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

    public async Task ReorderModuleChaptersAsync(List<Guid> chapterIdsInNewOrder, Guid moduleId, CancellationToken cancellationToken = default)
    {
        var chaptersInModule = await this.GetChaptersByModuleIdAsync(moduleId, cancellationToken);
        chaptersInModule = chaptersInModule.OrderBy(c => chapterIdsInNewOrder.IndexOf(c.Id)).ToList();
        for (var i = 0; i < chaptersInModule.Count; i++)
        {
            chaptersInModule[i].RunningNumber = i + 1;
        }
        
        await this.repository.Chapters.UpdateRangeAsync(chaptersInModule, cancellationToken);
    }
}