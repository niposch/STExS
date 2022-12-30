using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Generic;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public sealed class ChapterService: IChapterService
{
    private readonly IApplicationRepository repository;
    private readonly IAccessService accessService;

    public ChapterService(IApplicationRepository repository, IAccessService accessService)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.accessService = accessService ?? throw new ArgumentNullException(nameof(accessService));
    }

    public async Task<Guid> CreateChapterAsync(Guid moduleId, string name, string description, Guid createUserId, CancellationToken cancellationToken = default)
    {
        if (!await this.accessService.IsModuleAdmin(moduleId, createUserId))
        {
            throw new UnauthorizedException();
        }
        
        var modulesInChapter = await this.GetChaptersByModuleIdAsync(moduleId, createUserId, cancellationToken);
        var nextRunningNumber = modulesInChapter.Select(c => c.RunningNumber).DefaultIfEmpty(0).Max() + 1;
        var chapter = new Chapter
        {
            ModuleId = moduleId,
            OwnerId = createUserId,
            RunningNumber = nextRunningNumber,
            ChapterName = name,
            ChapterDescription = description
        };

        await this.repository.Chapters.AddAsync(chapter, cancellationToken);
        return chapter.Id;
    }

    public async Task UpdateChapterAsync(Guid chapterId,
        string newName,
        string newDescription,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var chapter = await this.repository.Chapters.TryGetByIdAsync(chapterId, cancellationToken);
        
        if (chapter is null)
        {
            throw new EntityNotFoundException<Chapter>(chapterId);
        }

        if (!await this.accessService.IsModuleAdmin(chapter.ModuleId, userId, cancellationToken))
        {
            throw new UnauthorizedException();
        }
        
        chapter.ChapterName = newName;
        chapter.ChapterDescription = newDescription;
        await this.repository.Chapters.UpdateAsync(chapter, cancellationToken);
    }

    public async Task DeleteChapterAsync(Guid chapterId, Guid userId, CancellationToken cancellationToken = default)
    {
        var chapter = await this.repository.Chapters.TryGetByIdAsync(chapterId, cancellationToken);
        if(chapter is null)
        {
            throw new EntityNotFoundException<Chapter>(chapterId);
        }
        
        if(! await  this.accessService.IsModuleAdmin(chapter.ModuleId, userId, cancellationToken))
        {
            throw new UnauthorizedException();
        }
        
        await this.repository.Chapters.DeleteAsync(chapterId, cancellationToken);
    }

    public async Task<Chapter> GetChapterAsync(Guid chapterId, Guid userId, CancellationToken cancellationToken = default)
    {
        var chapter = await this.repository.Chapters.TryGetByIdAsync(chapterId, cancellationToken);
        if (chapter is null)
        {
            throw new EntityNotFoundException<Chapter>(chapterId);
        }

        if (!await this.accessService.CanViewModule(chapter.ModuleId, userId, cancellationToken))
        {
            throw new UnauthorizedException();
        }
        return await this.repository.Chapters.TryGetByIdAsync(chapterId, cancellationToken)
               ?? throw new EntityNotFoundException<Chapter>(chapterId);
    }

    public async Task<List<Chapter>> GetAllChaptersAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var allChapters = await this.repository.Chapters.GetAllAsync(cancellationToken);
        var chapters = await this.accessService.FilterChapterVisibility(allChapters, userId, cancellationToken);
        return chapters.ToList();
    }

    public async Task<List<Chapter>> GetChaptersByModuleIdAsync(Guid moduleId, Guid userId, CancellationToken cancellationToken = default)
    {
        if (!await this.accessService.CanViewModule(moduleId, userId, cancellationToken))
        {
            throw new UnauthorizedException();
        }
        return await this.repository.Chapters.GetChaptersByModuleIdAsync(moduleId, cancellationToken);
    }

    public async Task ReorderModuleChaptersAsync(List<Guid> chapterIdsInNewOrder,
        Guid moduleId,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        if (!await this.accessService.IsModuleAdmin(moduleId, userId, cancellationToken))
        {
            throw new UnauthorizedException();
        }
        var chaptersInModule = await this.GetChaptersByModuleIdAsync(moduleId, userId, cancellationToken);
        chaptersInModule = chaptersInModule.OrderBy(c => chapterIdsInNewOrder.IndexOf(c.Id)).ToList();
        for (var i = 0; i < chaptersInModule.Count; i++)
        {
            chaptersInModule[i].RunningNumber = i + 1;
        }
        
        await this.repository.Chapters.UpdateRangeAsync(chaptersInModule, cancellationToken);
    }
}