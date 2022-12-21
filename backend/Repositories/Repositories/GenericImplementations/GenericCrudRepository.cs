using Common.Models.HelperInterfaces;
using Common.RepositoryInterfaces.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories.GenericImplementations;

public class GenericCrudRepository<T> : GenericDeletableEntityRepository<T>, IGenericCrudRepository<T>
    where T : DeletableBaseEntity
{
    private readonly ApplicationDbContext context;

    public GenericCrudRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<T?> TryGetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        return context.Set<T>().FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T> AddAsync(T entity,
        CancellationToken cancellationToken = default)
    {
        context.RemoveLocalIfTracked(entity);
        await context.Set<T>().AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity,
        CancellationToken cancellationToken = default)
    {
        context.RemoveLocalIfTracked(entity);
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }
}