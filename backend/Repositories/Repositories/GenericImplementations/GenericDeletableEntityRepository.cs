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

    public Task<List<TModel>> GetAllActiveAsync(CancellationToken cancellationToken = default)
    {
        return context.Set<TModel>().Where(e => e.DeletedDate == null).ToListAsync(cancellationToken);
    }

    public Task<List<TModel>> GetAllDeletedAsync(CancellationToken cancellationToken = default)
    {
        return context.Set<TModel>().Where(e => e.DeletedDate != null).ToListAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        var entityToDelete = await context.Set<TModel>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (entityToDelete == null) throw new EntityNotFoundException<TModel>(id);
        entityToDelete.IsDeleted = true;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UndeleteAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        var entityToUndelete =
            await context.Set<TModel>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (entityToUndelete == null) throw new EntityNotFoundException<TModel>(id);
        entityToUndelete.IsDeleted = false;
        await context.SaveChangesAsync(cancellationToken);
    }
}