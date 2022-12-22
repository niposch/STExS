using Common.Exceptions;
using Common.Models.HelperInterfaces;
using Common.RepositoryInterfaces.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories.GenericImplementations;

public class GenericDeletableEntityRepository<TModel> : IDeletableEntityRepository<TModel>
    where TModel : DeletableBaseEntity
{
    private readonly ApplicationDbContext context;

    public GenericDeletableEntityRepository(ApplicationDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task DeleteAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        var entityToDelete = await context.Set<TModel>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (entityToDelete == null) throw new EntityNotFoundException<TModel>(id);
        this.context.Remove(entityToDelete);
        await context.SaveChangesAsync(cancellationToken);
    }
}