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

    public async Task<IEnumerable<Module>> GetModulesUserIsOwnerOfAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        return await this.context.Modules
            .Where(m => m.OwnerId == ownerId)
            .ToListAsync(cancellationToken);
    }
}