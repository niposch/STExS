using Common.Exceptions;
using Common.Models.ExerciseSystem;
using Common.RepositoryInterfaces.Tables;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.GenericImplementations;

namespace Repositories.Repositories;

public class ModuleRepository: GenericCrudAndArchiveRepository<Module>, IModuleRepository
{
    private readonly ApplicationDbContext context;
    public ModuleRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public new async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await this.context.Modules.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        if (entity == null)
        {
            throw new EntityNotFoundException<Module>(id);
        }
        
        // TODO exercises need to be deleted as well, once they are implemented
        // this.context.RemoveRange(entity.Chapters.Select(c => c.ParsonExercises));
        this.context.RemoveRange(entity.Chapters);
        
        this.context.RemoveRange(entity.ModuleParticipations);
        this.context.Remove(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Module>> GetModulesUserIsOwnerOfAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        return await this.context.Modules
            .Where(m => m.OwnerId == ownerId)
            .ToListAsync(cancellationToken);
    }
}