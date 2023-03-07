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
        //modules without any contents should be deletable
        if (entity.Chapters != null)
        {
            var exercises = entity.Chapters.SelectMany(c => c.Exercises).ToList();
            if (exercises.Count > 0)
            {
                this.context.Exercises.RemoveRange(exercises);
            }
            this.context.RemoveRange(entity.Chapters);
        }

        if (entity.ModuleParticipations != null) {
            this.context.RemoveRange(entity.ModuleParticipations);
        }


        this.context.Remove(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Module>> GetModulesUserIsOwnerOfAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        return await this.context.Modules
            .Where(m => m.OwnerId == ownerId)
            .ToListAsync(cancellationToken);
    }

    public Task<Module?> TryGetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return this.context.Modules
            .Include(m => m.Owner)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public Task<List<Module>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return this.context.Modules
            .Include(m => m.Owner)
            .ToListAsync(cancellationToken);
    }
}