using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Tables;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.GenericImplementations;

namespace Repositories.Repositories;

public class ChapterRepository: GenericCrudRepository<Chapter>, IChapterRepository
{
    private readonly ApplicationDbContext context;
    public ChapterRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<List<Chapter>> GetChaptersByModuleIdAsync(Guid moduleId, CancellationToken cancellationToken = default)
    {
        return await this.context
            .Chapters
            .Where(c => c.ModuleId == moduleId)
            .ToListAsync(cancellationToken);
    }
}