using Common.Exceptions;
using Common.Models.HelperInterfaces;
using Common.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories.GenericImplementations;

public class GenericDeletableEntityRepository<TModel> : IDeletableEntityRepository<TModel>
    where TModel : class, IBaseEntity, IDeletable
{
    private readonly ApplicationDbContext context;

    public GenericDeletableEntityRepository(ApplicationDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<List<TModel>> GetAllActive(CancellationToken cancellationToken = default)
    {
        return context.Set<TModel>().Where(e => e.DeletedDate == null).ToListAsync(cancellationToken);
    }

    public Task<List<TModel>> GetAllDeleted(CancellationToken cancellationToken = default)
    {
        return context.Set<TModel>().Where(e => e.DeletedDate != null).ToListAsync(cancellationToken);
    }

    public async Task Delete(Guid id,
        CancellationToken cancellationToken = default)
    {
        var entityToDelete = await context.Set<TModel>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (entityToDelete == null) throw new EntityNotFoundException(id, typeof(TModel));
        entityToDelete.IsDeleted = true;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Undelete(Guid id,
        CancellationToken cancellationToken = default)
    {
        var entityToUndelete =
            await context.Set<TModel>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (entityToUndelete == null) throw new EntityNotFoundException(id, typeof(TModel));
        entityToUndelete.IsDeleted = false;
        await context.SaveChangesAsync(cancellationToken);
    }
}