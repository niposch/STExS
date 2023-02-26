using Common.Models.HelperInterfaces;
using Common.RepositoryInterfaces.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories.GenericImplementations;

public class GenericCrudRepository<T> : GenericDeletableEntityRepository<T>, IGenericCrudRepository<T>
    where T : DeletableBaseEntity
{
    protected readonly ApplicationDbContext context;

    public GenericCrudRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<T?> TryGetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        return this.context.Set<T>().FirstOrDefaultAsync(module => module.Id == id, cancellationToken);
    }

    public Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return this.context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T> AddAsync(T entity,
        CancellationToken cancellationToken = default)
    {
        this.context.RemoveLocalIfTracked(entity);
        await this.context.Set<T>().AddAsync(entity, cancellationToken);
        await this.context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity,
        CancellationToken cancellationToken = default)
    {
        this.context.RemoveLocalIfTracked(entity);
        this.context.Set<T>().Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<List<T>> UpdateRangeAsync(List<T> entities,
        CancellationToken cancellationToken = default)
    {
        this.context.Set<T>().UpdateRange(entities);
        await this.context.SaveChangesAsync(cancellationToken);
        return entities;
    }
}